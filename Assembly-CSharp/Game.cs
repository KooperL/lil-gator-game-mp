using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Game : MonoBehaviour
{
	// (add) Token: 0x0600073F RID: 1855 RVA: 0x000346A8 File Offset: 0x000328A8
	// (remove) Token: 0x06000740 RID: 1856 RVA: 0x000346DC File Offset: 0x000328DC
	public static event Action onEnterDialogue;

	// (get) Token: 0x06000741 RID: 1857 RVA: 0x00034710 File Offset: 0x00032910
	// (set) Token: 0x06000742 RID: 1858 RVA: 0x000074EF File Offset: 0x000056EF
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

	// (get) Token: 0x06000743 RID: 1859 RVA: 0x00007517 File Offset: 0x00005717
	public static bool AllowedToSave
	{
		get
		{
			return Game.g != null && Game.g.enabled && Game.WorldState != WorldState.Prologue && Game.State != GameState.Dialogue;
		}
	}

	// (get) Token: 0x06000744 RID: 1860 RVA: 0x00007546 File Offset: 0x00005746
	// (set) Token: 0x06000745 RID: 1861 RVA: 0x00007558 File Offset: 0x00005758
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

	// (get) Token: 0x06000746 RID: 1862 RVA: 0x00007565 File Offset: 0x00005765
	public static int NewGameIndex
	{
		get
		{
			return GameData.g.ReadInt("NewGameIndex", 0);
		}
	}

	// (get) Token: 0x06000747 RID: 1863 RVA: 0x00007577 File Offset: 0x00005777
	public static bool IsNewGamePlus
	{
		get
		{
			return Game.NewGameIndex != 0;
		}
	}

	// (get) Token: 0x06000748 RID: 1864 RVA: 0x00007581 File Offset: 0x00005781
	public static bool IsInDialogue
	{
		get
		{
			return Game.State == GameState.Dialogue || DialogueManager.d.IsInImportantDialogue;
		}
	}

	// (get) Token: 0x06000749 RID: 1865 RVA: 0x00007597 File Offset: 0x00005797
	public static bool HasControl
	{
		get
		{
			return Game.State == GameState.Play && Time.time - Game.setStatePlayTime > 0.1f;
		}
	}

	// (get) Token: 0x0600074A RID: 1866 RVA: 0x000075B4 File Offset: 0x000057B4
	// (set) Token: 0x0600074B RID: 1867 RVA: 0x00034764 File Offset: 0x00032964
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

	// Token: 0x0600074C RID: 1868 RVA: 0x000347BC File Offset: 0x000329BC
	private void Awake()
	{
		Game.g = this;
		this.dialogueDepth = 0;
		MultiplayerConfigLoader multiplayerConfigLoader = MultiplayerConfigLoader.Load("lggmp_config.ini");
		Debug.Log("[LGG-MP] Session Key: " + multiplayerConfigLoader.SessionKey);
		Debug.Log("[LGG-MP] Display Name: " + multiplayerConfigLoader.DisplayName);
		Debug.Log("[LGG-MP] Server Host: " + multiplayerConfigLoader.ServerHost);
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x000075C0 File Offset: 0x000057C0
	private void OnEnable()
	{
		Game.g = this;
		if (this.autoChangeWorldState)
		{
			this.SetWorldState((WorldState)GameData.g.ReadInt("WorldState", 10), true, false);
		}
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x000075E9 File Offset: 0x000057E9
	private void Start()
	{
		MultiplayerCommunicationService.Instance.initConnection();
		base.gameObject.AddComponent<MultiplayerPlayerFrameStreamer>();
		GameObject gameObject = new GameObject("MultiplayerPlayerManager");
		MultiplayerNetworkBootstrap.manager = gameObject.AddComponent<MultiplayerPlayerManager>();
		global::UnityEngine.Object.DontDestroyOnLoad(gameObject);
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x0000761B File Offset: 0x0000581B
	public void SetToStory()
	{
		this.SetWorldState(WorldState.Story);
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x00007625 File Offset: 0x00005825
	public void SetToFlashback()
	{
		this.SetWorldState(WorldState.Flashback);
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x0000762F File Offset: 0x0000582F
	public void SetWorldState(WorldState newWorldState)
	{
		this.SetWorldState(newWorldState, false, false);
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x00034820 File Offset: 0x00032A20
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

	// Token: 0x06000755 RID: 1877 RVA: 0x000348D8 File Offset: 0x00032AD8
	public void Update()
	{
		if (MultiplayerConfigLoader.Instance.logTemp.Contains("F"))
		{
			foreach (GameObject gameObject in global::UnityEngine.Object.FindObjectsOfType<GameObject>())
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
			foreach (GameObject gameObject2 in global::UnityEngine.Object.FindObjectsOfType<GameObject>())
			{
				Debug.Log("=== PLAYER CONTAINER: " + gameObject2.name + " ===");
				Debug.Log(string.Format("Position: {0}", gameObject2.transform.position));
				Debug.Log(string.Format("Active: {0}", gameObject2.activeInHierarchy));
				foreach (Component component in gameObject2.GetComponents<Component>())
				{
					Debug.Log(string.Format("    Container Component: {0}", component.GetType()));
				}
				Debug.Log("Children:");
				for (int k = 0; k < gameObject2.transform.childCount; k++)
				{
					Transform child = gameObject2.transform.GetChild(k);
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
