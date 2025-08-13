using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

// Token: 0x0200011C RID: 284
public class Game : MonoBehaviour
{
	// Token: 0x14000002 RID: 2
	// (add) Token: 0x060005DB RID: 1499
	// (remove) Token: 0x060005DC RID: 1500
	public static event Action onEnterDialogue;

	// Token: 0x1700004B RID: 75
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

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x060005DF RID: 1503
	public static bool AllowedToSave
	{
		get
		{
			return Game.g != null && Game.g.enabled && Game.WorldState != WorldState.Prologue && Game.State != GameState.Dialogue;
		}
	}

	// Token: 0x1700004D RID: 77
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

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x060005E2 RID: 1506
	public static int NewGameIndex
	{
		get
		{
			return GameData.g.ReadInt("NewGameIndex", 0);
		}
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x060005E3 RID: 1507
	public static bool IsNewGamePlus
	{
		get
		{
			return Game.NewGameIndex != 0;
		}
	}

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x060005E4 RID: 1508
	public static bool IsInDialogue
	{
		get
		{
			return Game.State == GameState.Dialogue || DialogueManager.d.IsInImportantDialogue;
		}
	}

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x060005E5 RID: 1509
	public static bool HasControl
	{
		get
		{
			return Game.State == GameState.Play && Time.time - Game.setStatePlayTime > 0.1f;
		}
	}

	// Token: 0x17000052 RID: 82
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

	// Token: 0x060005E8 RID: 1512
	private void Awake()
	{
		Game.g = this;
		this.dialogueDepth = 0;
	}

	// Token: 0x060005E9 RID: 1513
	private void OnEnable()
	{
		Game.g = this;
		if (this.autoChangeWorldState)
		{
			this.SetWorldState((WorldState)GameData.g.ReadInt("WorldState", 10), true, false);
		}
	}

	// Token: 0x060005EA RID: 1514
	private void Start()
	{
		base.gameObject.AddComponent<PlayerPositionStreamer>();
	}

	// Token: 0x060005EB RID: 1515
	public void SetToStory()
	{
		this.SetWorldState(WorldState.Story);
	}

	// Token: 0x060005EC RID: 1516
	public void SetToFlashback()
	{
		this.SetWorldState(WorldState.Flashback);
	}

	// Token: 0x060005ED RID: 1517
	public void SetWorldState(WorldState newWorldState)
	{
		this.SetWorldState(newWorldState, false, false);
	}

	// Token: 0x060005EE RID: 1518
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

	// Token: 0x06001EB4 RID: 7860
	private void Update()
	{
	}

	// Token: 0x0400080E RID: 2062
	public const string versionID = "1.0.3";

	// Token: 0x0400080F RID: 2063
	public const int versionInt = 35;

	// Token: 0x04000811 RID: 2065
	public static bool ignoreDialogueDepth = false;

	// Token: 0x04000812 RID: 2066
	public static float setStatePlayTime = -1f;

	// Token: 0x04000813 RID: 2067
	private const float giveControlDelay = 0.1f;

	// Token: 0x04000814 RID: 2068
	public static Game g;

	// Token: 0x04000815 RID: 2069
	public GameState state;

	// Token: 0x04000816 RID: 2070
	public WorldState worldState;

	// Token: 0x04000817 RID: 2071
	public int dialogueDepth;

	// Token: 0x04000818 RID: 2072
	public bool autoChangeWorldState = true;

	// Token: 0x04000819 RID: 2073
	public GameObject storyPlayer;

	// Token: 0x0400081A RID: 2074
	public GameObject flashbackRoot;

	// Token: 0x0400081B RID: 2075
	public Game.SceneID sceneID = Game.SceneID.Other;

	// Token: 0x0400081C RID: 2076
	public AssetReference mainScene;

	// Token: 0x020003B0 RID: 944
	public enum SceneID
	{
		// Token: 0x04001B79 RID: 7033
		Prologue,
		// Token: 0x04001B7A RID: 7034
		MainScene,
		// Token: 0x04001B7B RID: 7035
		Other
	}
}
