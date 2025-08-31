using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Game : MonoBehaviour
{
	// (add) Token: 0x060005DB RID: 1499
	// (remove) Token: 0x060005DC RID: 1500
	public static event Action onEnterDialogue;

	// (get) Token: 0x060005DD RID: 1501
	// (set) Token: 0x060005DE RID: 1502
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

	// (get) Token: 0x060005DF RID: 1503
	public static bool AllowedToSave
	{
		get
		{
			return Game.g != null && Game.g.enabled && Game.WorldState != WorldState.Prologue && Game.State != GameState.Dialogue;
		}
	}

	// (get) Token: 0x060005E0 RID: 1504
	// (set) Token: 0x060005E1 RID: 1505
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

	// (get) Token: 0x060005E2 RID: 1506
	public static int NewGameIndex
	{
		get
		{
			return GameData.g.ReadInt("NewGameIndex", 0);
		}
	}

	// (get) Token: 0x060005E3 RID: 1507
	public static bool IsNewGamePlus
	{
		get
		{
			return Game.NewGameIndex != 0;
		}
	}

	// (get) Token: 0x060005E4 RID: 1508
	public static bool IsInDialogue
	{
		get
		{
			return Game.State == GameState.Dialogue || DialogueManager.d.IsInImportantDialogue;
		}
	}

	// (get) Token: 0x060005E5 RID: 1509
	public static bool HasControl
	{
		get
		{
			return Game.State == GameState.Play && Time.time - Game.setStatePlayTime > 0.1f;
		}
	}

	// (get) Token: 0x060005E6 RID: 1510
	// (set) Token: 0x060005E7 RID: 1511
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
		Game.g = this;
		this.dialogueDepth = 0;
		MultiplayerConfigLoader multiplayerConfigLoader = MultiplayerConfigLoader.Load("lggmp_config.ini");
		Debug.Log("[LGG-MP] Session Key: " + multiplayerConfigLoader.SessionKey);
		Debug.Log("[LGG-MP] Display Name: " + multiplayerConfigLoader.DisplayName);
		Debug.Log("[LGG-MP] Server Host: " + multiplayerConfigLoader.ServerHost);
		if (MultiplayerCommunicationService.Instance.connectionState == MultiplayerCommunicationService.ConnectionState.None)
		{
			MultiplayerCommunicationService.Instance.initConnection();
		}
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
		GameObject gameObject = new GameObject("MultiplayerPlayerManager");
		MultiplayerNetworkBootstrap.manager = gameObject.AddComponent<MultiplayerPlayerManager>();
		Object.DontDestroyOnLoad(gameObject);
		base.gameObject.AddComponent<MultiplayerPlayerFrameStreamer>();
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

	private void Update()
	{
		if (MultiplayerConfigLoader.Instance.logTemp.Contains("F"))
		{
			foreach (GameObject gameObject in Object.FindObjectsOfType<GameObject>())
			{
				if (gameObject.name.Contains("Hero") || gameObject.name.Contains("Sis"))
				{
					if (gameObject.transform.root != gameObject.transform)
					{
						Debug.Log(string.Concat(new string[]
						{
							"Possible prefab instance: ",
							gameObject.name,
							" (Parent: ",
							gameObject.transform.root.name,
							")"
						}));
					}
					Transform transform = gameObject.transform.parent;
					while (transform != null)
					{
						Debug.Log(gameObject.name + " hierarchy: " + transform.name);
						transform = transform.parent;
					}
				}
			}
		}
		if (MultiplayerConfigLoader.Instance.logTemp.Contains("I"))
		{
			foreach (GameObject gameObject2 in Object.FindObjectsOfType<GameObject>())
			{
				Debug.Log("=== PLAYER CONTAINER: " + gameObject2.name + " ===");
				Debug.Log(string.Format("Position: {0}", gameObject2.transform.position));
				Debug.Log(string.Format("Active: {0}", gameObject2.activeInHierarchy));
				foreach (Component component in gameObject2.GetComponents<Component>())
				{
					Debug.Log(string.Format("    Container Component: {0}", component.GetType()));
				}
				Debug.Log("Children:");
				for (int i = 0; i < gameObject2.transform.childCount; i++)
				{
					Transform child = gameObject2.transform.GetChild(i);
					Debug.Log("    Child: " + child.name);
					if (child.name.Contains("Hero"))
					{
						foreach (Component component2 in child.GetComponents<Component>())
						{
							Debug.Log(string.Format("        Child Component: {0}", component2.GetType()));
						}
					}
				}
				Debug.Log("=== END ===\n");
			}
		}
		if (MultiplayerConfigLoader.Instance.logTemp.Contains("S"))
		{
			foreach (Shader shader in Resources.FindObjectsOfTypeAll<Shader>())
			{
				Debug.Log("Found shader: " + shader.name);
			}
		}
		if (MultiplayerConfigLoader.Instance.logTemp.Contains("A"))
		{
			GameObject playerGO = Player.gameObject;
			if (playerGO == null)
			{
				return;
			}
			Transform playerGOT = playerGO.transform.Find("Heroboy");
			if (base.transform == null)
			{
				return;
			}
			Animator animator = playerGOT.GetComponent<Animator>();
			if (animator != null)
			{
				try
				{
					foreach (AnimatorControllerParameter param in animator.parameters)
					{
						Debug.Log(string.Format("Found animator parameter: {0} ({1})", param.name, param.type));
					}
				}
				catch (Exception e)
				{
					Debug.LogWarning("Could not reset animator: " + e.Message);
				}
			}
		}
		if (MultiplayerConfigLoader.Instance.logTemp.Contains("M"))
		{
			for (int j = 0; j < ItemManager.i.items.Length; j++)
			{
				Debug.Log("Loading " + ItemManager.i.items[j].name);
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
