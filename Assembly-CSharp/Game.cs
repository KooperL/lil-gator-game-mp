using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Game : MonoBehaviour
{
	// (add) Token: 0x0600073F RID: 1855
	// (remove) Token: 0x06000740 RID: 1856
	public static event Action onEnterDialogue;

	// (get) Token: 0x06000741 RID: 1857
	// (set) Token: 0x06000742 RID: 1858
	public static GameState State
	{
		get
		{
			GameState gameState = Game.g.state;
			if (Game.DialogueDepth > 0 && !Game.ignoreDialogueDepth)
			{
				gameState = GameState.Dialogue;
			}
			if (gameState == GameState.Play && Game.setStatePlayTime < 0f)
			{
				Game.setStatePlayTime = Time.time;
			}
			else if (gameState != GameState.Play)
			{
				Game.setStatePlayTime = -1f;
			}
			return gameState;
		}
		set
		{
			if (value == GameState.Dialogue && Game.g.state != GameState.Dialogue)
			{
				Game.onEnterDialogue();
			}
			Game.g.state = value;
		}
	}

	// (get) Token: 0x06000743 RID: 1859
	public static bool AllowedToSave
	{
		get
		{
			return Game.g != null && Game.g.enabled && Game.WorldState != WorldState.Prologue && Game.State != GameState.Dialogue;
		}
	}

	// (get) Token: 0x06000744 RID: 1860
	// (set) Token: 0x06000745 RID: 1861
	public static WorldState WorldState
	{
		get
		{
			return (WorldState)GameData.g.ReadInt("WorldState", 0);
		}
		set
		{
			Game.g.SetWorldState(value);
		}
	}

	// (get) Token: 0x06000746 RID: 1862
	public static int NewGameIndex
	{
		get
		{
			return GameData.g.ReadInt("NewGameIndex", 0);
		}
	}

	// (get) Token: 0x06000747 RID: 1863
	public static bool IsNewGamePlus
	{
		get
		{
			return Game.NewGameIndex != 0;
		}
	}

	// (get) Token: 0x06000748 RID: 1864
	public static bool IsInDialogue
	{
		get
		{
			return Game.State == GameState.Dialogue || DialogueManager.d.IsInImportantDialogue;
		}
	}

	// (get) Token: 0x06000749 RID: 1865
	public static bool HasControl
	{
		get
		{
			return Game.State == GameState.Play && Time.time - Game.setStatePlayTime > 0.1f;
		}
	}

	// (get) Token: 0x0600074A RID: 1866
	// (set) Token: 0x0600074B RID: 1867
	public static int DialogueDepth
	{
		get
		{
			return Game.g.dialogueDepth;
		}
		set
		{
			if (value > 0 && Game.g.dialogueDepth == 0 && Game.State != GameState.Dialogue)
			{
				Game.onEnterDialogue();
			}
			Game.g.dialogueDepth = Mathf.Max(value, 0);
			if (SpeedrunData.IsSpeedrunMode)
			{
				Application.targetFrameRate = ((value > 0) ? 60 : (-1));
			}
		}
	}

	private void Awake()
	{
		Game.g = this;
		this.dialogueDepth = 0;
		MultiplayerConfigLoader multiplayerConfigLoader = MultiplayerConfigLoader.Load("lggmp_config.ini");
		Debug.Log("[LGG-MP] Session Key: " + multiplayerConfigLoader.SessionKey);
		Debug.Log("[LGG-MP] Display Name: " + multiplayerConfigLoader.DisplayName);
		Debug.Log("[LGG-MP] Server Host: " + multiplayerConfigLoader.ServerHost);
	}

	private void OnEnable()
	{
		Game.g = this;
		if (this.autoChangeWorldState)
		{
			this.SetWorldState((WorldState)GameData.g.ReadInt("WorldState", 10), true, false);
		}
	}

	private void Start()
	{
		MultiplayerCommunicationService.Instance.initConnection();
		base.gameObject.AddComponent<MultiplayerPlayerFrameStreamer>();
		GameObject gameObject = new GameObject("MultiplayerPlayerManager");
		MultiplayerNetworkBootstrap.manager = gameObject.AddComponent<MultiplayerPlayerManager>();
		global::UnityEngine.Object.DontDestroyOnLoad(gameObject);
	}

	public void SetToStory()
	{
		this.SetWorldState(WorldState.Story);
	}

	public void SetToFlashback()
	{
		this.SetWorldState(WorldState.Flashback);
	}

	public void SetWorldState(WorldState newWorldState)
	{
		this.SetWorldState(newWorldState, false, false);
	}

	public void SetWorldState(WorldState newWorldState, bool forceChange = false, bool delaySceneChange = false)
	{
		if (newWorldState != this.worldState || forceChange)
		{
			this.worldState = newWorldState;
			GameData.g.Write("WorldState", (int)newWorldState);
			if (this.storyPlayer != null)
			{
				this.storyPlayer.SetActive(this.worldState != WorldState.Flashback);
			}
			if (this.flashbackRoot != null)
			{
				this.flashbackRoot.SetActive(Game.WorldState == WorldState.Flashback);
			}
			if (delaySceneChange)
			{
				return;
			}
			if (this.sceneID == Game.SceneID.Prologue && this.worldState != WorldState.Prologue)
			{
				LoadSceneSequence.LoadScene(this.mainScene, LoadSceneSequence.LoadType.LoadingScreen);
			}
			if (this.sceneID == Game.SceneID.MainScene && this.worldState == WorldState.Prologue)
			{
				LoadSceneSequence.LoadScene(0, LoadSceneSequence.LoadType.LoadingScreen);
			}
		}
	}

	public void Update()
	{
		if (MultiplayerConfigLoader.Instance.logTemp.Contains("F"))
		{
			foreach (GameObject go in global::UnityEngine.Object.FindObjectsOfType<GameObject>())
			{
				if (go.name.Contains("Hero") || go.name.Contains("Sis"))
				{
					if (go.transform.root != go.transform)
					{
						Debug.Log(string.Concat(new string[]
						{
							"Possible prefab instance: ",
							go.name,
							" (Parent: ",
							go.transform.root.name,
							")"
						}));
					}
					Transform parent = go.transform.parent;
					while (parent != null)
					{
						Debug.Log(go.name + " hierarchy: " + parent.name);
						parent = parent.parent;
					}
				}
			}
		}
		if (MultiplayerConfigLoader.Instance.logTemp.Contains("I"))
		{
			foreach (GameObject go2 in global::UnityEngine.Object.FindObjectsOfType<GameObject>())
			{
				Debug.Log("=== PLAYER CONTAINER: " + go2.name + " ===");
				Debug.Log(string.Format("Position: {0}", go2.transform.position));
				Debug.Log(string.Format("Active: {0}", go2.activeInHierarchy));
				foreach (Component c in go2.GetComponents<Component>())
				{
					Debug.Log(string.Format("    Container Component: {0}", c.GetType()));
				}
				Debug.Log("Children:");
				for (int i = 0; i < go2.transform.childCount; i++)
				{
					Transform child = go2.transform.GetChild(i);
					Debug.Log("    Child: " + child.name);
					if (child.name.Contains("Hero"))
					{
						foreach (Component c2 in child.GetComponents<Component>())
						{
							Debug.Log(string.Format("        Child Component: {0}", c2.GetType()));
						}
					}
				}
				Debug.Log("=== END ===\n");
			}
		}
	}

	public const string versionID = "1.0.3";

	public const int versionInt = 35;

	public static bool ignoreDialogueDepth = false;

	public static float setStatePlayTime = -1f;

	private const float giveControlDelay = 0.1f;

	public static Game g;

	public GameState state;

	public WorldState worldState;

	public int dialogueDepth;

	public bool autoChangeWorldState = true;

	public GameObject storyPlayer;

	public GameObject flashbackRoot;

	public Game.SceneID sceneID = Game.SceneID.Other;

	public AssetReference mainScene;

	public enum SceneID
	{
		Prologue,
		MainScene,
		Other
	}
}
