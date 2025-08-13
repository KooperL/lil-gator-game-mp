using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

// Token: 0x02000202 RID: 514
public class MainMenuToGameplay : MonoBehaviour
{
	// Token: 0x06000989 RID: 2441 RVA: 0x000093B6 File Offset: 0x000075B6
	private void Start()
	{
		Game.State = GameState.Menu;
		if (this.preloadMainScene)
		{
			LoadSceneSequence.StartPreloadScene(this.mainScene);
		}
		GameData.g.loadOnStart = false;
		GameData.g.save = false;
		if (SpeedrunData.IsSpeedrunMode)
		{
			SpeedrunData.ResetRun();
		}
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x000093F3 File Offset: 0x000075F3
	public void StartFreshNewGame(int index = 0)
	{
		GameData.g.SetSaveFile(index);
		GameData.g.LoadSaveData(this.newGameData.gameSaveData);
		this.StartNewGame();
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x00039EB4 File Offset: 0x000380B4
	public void StartNewGame()
	{
		this.game.enabled = true;
		GameData.g.loadOnStart = true;
		GameData.g.save = true;
		Game.State = GameState.Play;
		this.mainMenuObject.SetActive(false);
		this.introSequence.StartSequence();
		this.gameplayObject.SetActive(true);
		if (SpeedrunData.IsSpeedrunMode)
		{
			SpeedrunData.StartNewRun();
		}
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x00039F1C File Offset: 0x0003811C
	public void LoadGameplay()
	{
		this.game.enabled = true;
		GameData.g.loadOnStart = true;
		GameData.g.save = true;
		this.playerObject.SetActive(false);
		if (Game.WorldState == WorldState.Prologue)
		{
			this.playerObject.GetComponent<PlayerMovement>().persistentTransform = true;
			this.playerObject.SetActive(true);
			Game.State = GameState.Play;
		}
		this.mainMenuObject.SetActive(false);
		this.gameplayObject.SetActive(true);
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x0000941B File Offset: 0x0000761B
	[ContextMenu("Start Slideshow")]
	public void StartSlideshow()
	{
		Object.Instantiate<GameObject>(this.slideshow);
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x00009429 File Offset: 0x00007629
	[ContextMenu("Start Indieland Slideshow")]
	public void StartILSlideshow()
	{
		Object.Instantiate<GameObject>(this.ilSlideshow);
	}

	// Token: 0x04000C30 RID: 3120
	public Game game;

	// Token: 0x04000C31 RID: 3121
	public GameObject mainMenuObject;

	// Token: 0x04000C32 RID: 3122
	public GameObject gameplayObject;

	// Token: 0x04000C33 RID: 3123
	public GameObject playerObject;

	// Token: 0x04000C34 RID: 3124
	public AssetReference mainScene;

	// Token: 0x04000C35 RID: 3125
	public bool preloadMainScene = true;

	// Token: 0x04000C36 RID: 3126
	public DialogueSequencer introSequence;

	// Token: 0x04000C37 RID: 3127
	public GameObject slideshow;

	// Token: 0x04000C38 RID: 3128
	public GameSaveWrapper newGameData;

	// Token: 0x04000C39 RID: 3129
	public GameObject ilSlideshow;

	// Token: 0x04000C3A RID: 3130
	public GameSaveWrapper newILGameData;

	// Token: 0x04000C3B RID: 3131
	public UnityEvent onILStart;
}
