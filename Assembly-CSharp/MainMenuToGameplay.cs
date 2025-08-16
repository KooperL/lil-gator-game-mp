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
		base.gameObject.AddComponent<InjectButtonToMainMenu>();
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x000096FF File Offset: 0x000078FF
	public void StartFreshNewGame(int index = 0)
	{
		GameData.g.SetSaveFile(index);
		GameData.g.LoadSaveData(this.newGameData.gameSaveData);
		this.StartNewGame();
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x00009727 File Offset: 0x00007927
	public void StartNewGamePlus(int index = 0)
	{
		GameData.g.LoadSaveFile(index);
		this.StartNewGame();
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x0003B788 File Offset: 0x00039988
	public void StartNewGame()
	{
		this.game.enabled = true;
		GameData.g.loadOnStart = true;
		GameData.g.save = true;
		Game.State = GameState.Play;
		this.mainMenuObject.SetActive(false);
		global::UnityEngine.Object.Destroy(this.mainMenuSettings);
		this.introSequence.StartSequence();
		this.gameplayObject.SetActive(true);
		if (SpeedrunData.IsSpeedrunMode)
		{
			SpeedrunData.StartNewRun();
		}
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x0003B7F8 File Offset: 0x000399F8
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

	// Token: 0x060009D4 RID: 2516 RVA: 0x0000973A File Offset: 0x0000793A
	[ContextMenu("Start Slideshow")]
	public void StartSlideshow()
	{
		global::UnityEngine.Object.Instantiate<GameObject>(this.slideshow);
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x00009748 File Offset: 0x00007948
	[ContextMenu("Start Indieland Slideshow")]
	public void StartILSlideshow()
	{
		global::UnityEngine.Object.Instantiate<GameObject>(this.ilSlideshow);
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
