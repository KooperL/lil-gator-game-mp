using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

// Token: 0x02000174 RID: 372
public class Game : MonoBehaviour
{
	// Token: 0x14000002 RID: 2
	// (add) Token: 0x06000701 RID: 1793 RVA: 0x00032E1C File Offset: 0x0003101C
	// (remove) Token: 0x06000702 RID: 1794 RVA: 0x00032E50 File Offset: 0x00031050
	public static event Action onEnterDialogue;

	// Token: 0x170000AD RID: 173
	// (get) Token: 0x06000703 RID: 1795 RVA: 0x00032E84 File Offset: 0x00031084
	// (set) Token: 0x06000704 RID: 1796 RVA: 0x000071EE File Offset: 0x000053EE
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

	// Token: 0x170000AE RID: 174
	// (get) Token: 0x06000705 RID: 1797 RVA: 0x00007216 File Offset: 0x00005416
	public static bool AllowedToSave
	{
		get
		{
			return Game.g != null && Game.g.enabled && Game.WorldState != WorldState.Prologue && Game.State != GameState.Dialogue;
		}
	}

	// Token: 0x170000AF RID: 175
	// (get) Token: 0x06000706 RID: 1798 RVA: 0x00007245 File Offset: 0x00005445
	// (set) Token: 0x06000707 RID: 1799 RVA: 0x00007257 File Offset: 0x00005457
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

	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x06000708 RID: 1800 RVA: 0x00007264 File Offset: 0x00005464
	public static bool IsInDialogue
	{
		get
		{
			return Game.State == GameState.Dialogue || DialogueManager.d.IsInImportantDialogue;
		}
	}

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x06000709 RID: 1801 RVA: 0x0000727A File Offset: 0x0000547A
	public static bool HasControl
	{
		get
		{
			return Game.State == GameState.Play && Time.time - Game.setStatePlayTime > 0.1f;
		}
	}

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x0600070A RID: 1802 RVA: 0x00007297 File Offset: 0x00005497
	// (set) Token: 0x0600070B RID: 1803 RVA: 0x00032ED8 File Offset: 0x000310D8
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

	// Token: 0x0600070C RID: 1804 RVA: 0x000072A3 File Offset: 0x000054A3
	private void Awake()
	{
		Game.g = this;
		this.dialogueDepth = 0;
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x000072B2 File Offset: 0x000054B2
	private void OnEnable()
	{
		Game.g = this;
		if (this.autoChangeWorldState)
		{
			this.SetWorldState((WorldState)GameData.g.ReadInt("WorldState", 10), true, false);
		}
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x00002229 File Offset: 0x00000429
	private void Start()
	{
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x000072DB File Offset: 0x000054DB
	public void SetToStory()
	{
		this.SetWorldState(WorldState.Story);
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x000072E5 File Offset: 0x000054E5
	public void SetToFlashback()
	{
		this.SetWorldState(WorldState.Flashback);
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x000072EF File Offset: 0x000054EF
	public void SetWorldState(WorldState newWorldState)
	{
		this.SetWorldState(newWorldState, false, false);
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x00032F30 File Offset: 0x00031130
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

	// Token: 0x0400096A RID: 2410
	public const string versionID = "1.0.2";

	// Token: 0x0400096B RID: 2411
	public const int versionInt = 34;

	// Token: 0x0400096D RID: 2413
	public static bool ignoreDialogueDepth = false;

	// Token: 0x0400096E RID: 2414
	public static float setStatePlayTime = -1f;

	// Token: 0x0400096F RID: 2415
	private const float giveControlDelay = 0.1f;

	// Token: 0x04000970 RID: 2416
	public static Game g;

	// Token: 0x04000971 RID: 2417
	public GameState state;

	// Token: 0x04000972 RID: 2418
	public WorldState worldState;

	// Token: 0x04000973 RID: 2419
	public int dialogueDepth;

	// Token: 0x04000974 RID: 2420
	public bool autoChangeWorldState = true;

	// Token: 0x04000975 RID: 2421
	public GameObject storyPlayer;

	// Token: 0x04000976 RID: 2422
	public GameObject flashbackRoot;

	// Token: 0x04000977 RID: 2423
	public Game.SceneID sceneID = Game.SceneID.Other;

	// Token: 0x04000978 RID: 2424
	public AssetReference mainScene;

	// Token: 0x02000175 RID: 373
	public enum SceneID
	{
		// Token: 0x0400097A RID: 2426
		Prologue,
		// Token: 0x0400097B RID: 2427
		MainScene,
		// Token: 0x0400097C RID: 2428
		Other
	}
}
