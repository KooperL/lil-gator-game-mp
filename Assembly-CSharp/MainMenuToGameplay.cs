using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

public class MainMenuToGameplay : MonoBehaviour
{
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
		base.gameObject.AddComponent<MultiplayerInjectButtonToMainMenu>();
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

	public Game game;

	public GameObject mainMenuObject;

	public GameObject gameplayObject;

	public GameObject playerObject;

	public AssetReference mainScene;

	public bool preloadMainScene = true;

	public DialogueSequencer introSequence;

	public GameObject slideshow;

	public GameSaveWrapper newGameData;

	public GameObject ilSlideshow;

	public GameSaveWrapper newILGameData;

	public UnityEvent onILStart;

	public GameObject mainMenuSettings;
}
