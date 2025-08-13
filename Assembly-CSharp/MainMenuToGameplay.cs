using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

// Token: 0x0200018B RID: 395
public class MainMenuToGameplay : MonoBehaviour
{
	// Token: 0x06000819 RID: 2073
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
		MultiplayerConfigLoader config = MultiplayerConfigLoader.Load("config.ini");
		Debug.Log("Session Key: " + config.SessionKey);
		Debug.Log("Display Name: " + config.DisplayName);
		Debug.Log("Server Host: " + config.ServerHost);
		base.gameObject.AddComponent<InjectButtonToMainMenu>();
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x00026EA6 File Offset: 0x000250A6
	public void StartFreshNewGame(int index = 0)
	{
		GameData.g.SetSaveFile(index);
		GameData.g.LoadSaveData(this.newGameData.gameSaveData);
		this.StartNewGame();
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x00026ECE File Offset: 0x000250CE
	public void StartNewGamePlus(int index = 0)
	{
		GameData.g.LoadSaveFile(index);
		this.StartNewGame();
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x00026EE4 File Offset: 0x000250E4
	public void StartNewGame()
	{
		this.game.enabled = true;
		GameData.g.loadOnStart = true;
		GameData.g.save = true;
		Game.State = GameState.Play;
		this.mainMenuObject.SetActive(false);
		Object.Destroy(this.mainMenuSettings);
		this.introSequence.StartSequence();
		this.gameplayObject.SetActive(true);
		if (SpeedrunData.IsSpeedrunMode)
		{
			SpeedrunData.StartNewRun();
		}
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x00026F54 File Offset: 0x00025154
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

	// Token: 0x0600081E RID: 2078 RVA: 0x00026FD1 File Offset: 0x000251D1
	[ContextMenu("Start Slideshow")]
	public void StartSlideshow()
	{
		Object.Instantiate<GameObject>(this.slideshow);
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x00026FDF File Offset: 0x000251DF
	[ContextMenu("Start Indieland Slideshow")]
	public void StartILSlideshow()
	{
		Object.Instantiate<GameObject>(this.ilSlideshow);
	}

	// Token: 0x04000A43 RID: 2627
	public Game game;

	// Token: 0x04000A44 RID: 2628
	public GameObject mainMenuObject;

	// Token: 0x04000A45 RID: 2629
	public GameObject gameplayObject;

	// Token: 0x04000A46 RID: 2630
	public GameObject playerObject;

	// Token: 0x04000A47 RID: 2631
	public AssetReference mainScene;

	// Token: 0x04000A48 RID: 2632
	public bool preloadMainScene = true;

	// Token: 0x04000A49 RID: 2633
	public DialogueSequencer introSequence;

	// Token: 0x04000A4A RID: 2634
	public GameObject slideshow;

	// Token: 0x04000A4B RID: 2635
	public GameSaveWrapper newGameData;

	// Token: 0x04000A4C RID: 2636
	public GameObject ilSlideshow;

	// Token: 0x04000A4D RID: 2637
	public GameSaveWrapper newILGameData;

	// Token: 0x04000A4E RID: 2638
	public UnityEvent onILStart;

	// Token: 0x04000A4F RID: 2639
	public GameObject mainMenuSettings;
}
