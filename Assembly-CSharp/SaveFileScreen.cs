using System;
using UnityEngine;

public class SaveFileScreen : MonoBehaviour
{
	// Token: 0x06000156 RID: 342 RVA: 0x00008066 File Offset: 0x00006266
	private void OnEnable()
	{
		this.currentState = SaveFileScreen.State.Standard;
		this.UpdateState();
	}

	// Token: 0x06000157 RID: 343 RVA: 0x00008075 File Offset: 0x00006275
	public void StartCopy()
	{
		this.currentState = SaveFileScreen.State.CopyFrom;
		this.UpdateState();
	}

	// Token: 0x06000158 RID: 344 RVA: 0x00008084 File Offset: 0x00006284
	public void StartErase()
	{
		this.currentState = SaveFileScreen.State.Erase;
		this.UpdateState();
	}

	// Token: 0x06000159 RID: 345 RVA: 0x00008093 File Offset: 0x00006293
	public void StartNGP()
	{
		this.currentState = SaveFileScreen.State.NGPSource;
		this.UpdateState();
	}

	// Token: 0x0600015A RID: 346 RVA: 0x000080A4 File Offset: 0x000062A4
	public void Confirm()
	{
		SaveFileScreen.State state = this.currentState;
		if (state == SaveFileScreen.State.ConfirmCopy)
		{
			FileUtil.CopyGameSaveData(this.copyFrom, this.copyTo);
			this.currentState = SaveFileScreen.State.Standard;
			this.UpdateState();
			return;
		}
		if (state == SaveFileScreen.State.ConfirmErase)
		{
			FileUtil.EraseGameSaveData(this.eraseTarget);
			this.currentState = SaveFileScreen.State.Standard;
			this.UpdateState();
			return;
		}
		if (state != SaveFileScreen.State.NGPConfirm)
		{
			return;
		}
		FileUtil.CreateNewGamePlusSaveData(this.copyFrom, this.copyTo);
		this.currentState = SaveFileScreen.State.Standard;
		this.UpdateState();
	}

	// Token: 0x0600015B RID: 347 RVA: 0x0000811B File Offset: 0x0000631B
	public void Cancel()
	{
		this.currentState = SaveFileScreen.State.Standard;
		this.UpdateState();
	}

	// Token: 0x0600015C RID: 348 RVA: 0x0000812C File Offset: 0x0000632C
	public void PressSaveFileButton(int index)
	{
		switch (this.currentState)
		{
		case SaveFileScreen.State.Standard:
			if (FileUtil.IsSaveFileStarted(index))
			{
				GameData.g.LoadSaveFile(index);
				this.toGameplay.LoadGameplay();
				return;
			}
			if (FileUtil.IsSaveFileReal(index))
			{
				this.toGameplay.StartNewGamePlus(index);
				return;
			}
			this.toGameplay.StartFreshNewGame(index);
			return;
		case SaveFileScreen.State.CopyFrom:
			if (FileUtil.IsSaveFileReal(index))
			{
				this.currentState = SaveFileScreen.State.CopyTo;
				this.copyFrom = index;
				this.UpdateState();
				return;
			}
			break;
		case SaveFileScreen.State.CopyTo:
			if (FileUtil.IsSaveFileReal(index))
			{
				this.currentState = SaveFileScreen.State.ConfirmCopy;
				this.copyTo = index;
				this.UpdateState();
				return;
			}
			FileUtil.CopyGameSaveData(this.copyFrom, index);
			this.currentState = SaveFileScreen.State.Standard;
			this.UpdateState();
			return;
		case SaveFileScreen.State.ConfirmCopy:
		case SaveFileScreen.State.ConfirmErase:
			break;
		case SaveFileScreen.State.Erase:
			if (FileUtil.IsSaveFileReal(index))
			{
				this.currentState = SaveFileScreen.State.ConfirmErase;
				this.eraseTarget = index;
				this.UpdateState();
				return;
			}
			break;
		case SaveFileScreen.State.NGPSource:
			if (FileUtil.IsSaveFileCompleted(index))
			{
				this.currentState = SaveFileScreen.State.NGPDestination;
				this.copyFrom = index;
				this.UpdateState();
				return;
			}
			break;
		case SaveFileScreen.State.NGPDestination:
			if (FileUtil.IsSaveFileReal(index))
			{
				this.currentState = SaveFileScreen.State.NGPConfirm;
				this.copyTo = index;
				this.UpdateState();
				return;
			}
			FileUtil.CreateNewGamePlusSaveData(this.copyFrom, index);
			this.currentState = SaveFileScreen.State.Standard;
			this.UpdateState();
			break;
		default:
			return;
		}
	}

	// Token: 0x0600015D RID: 349 RVA: 0x00008270 File Offset: 0x00006470
	private void UpdateState()
	{
		if (this.currentState == SaveFileScreen.State.Standard)
		{
			for (int i = 0; i < this.displays.Length; i++)
			{
				this.displays[i].Load(FileUtil.gameSaveDataInfo[i], i);
			}
		}
		if (this.currentState == SaveFileScreen.State.ConfirmCopy)
		{
			this.copyFromDisplay.Load(FileUtil.gameSaveDataInfo[this.copyFrom], this.copyFrom);
			this.copyToDisplay.Load(FileUtil.gameSaveDataInfo[this.copyTo], this.copyTo);
		}
		if (this.currentState == SaveFileScreen.State.ConfirmErase)
		{
			this.eraseDisplay.Load(FileUtil.gameSaveDataInfo[this.eraseTarget], this.eraseTarget);
		}
		if (this.currentState == SaveFileScreen.State.NGPConfirm)
		{
			this.ngpFromDisplay.Load(FileUtil.gameSaveDataInfo[this.copyFrom], this.copyFrom);
			this.ngpToDisplay.Load(FileUtil.gameSaveDataInfo[this.copyTo], this.copyTo);
		}
		this.standardButtons.SetActive(this.currentState == SaveFileScreen.State.Standard);
		this.fileSelectRoot.SetActive(this.currentState != SaveFileScreen.State.ConfirmCopy && this.currentState != SaveFileScreen.State.ConfirmErase && this.currentState != SaveFileScreen.State.NGPConfirm);
		this.copyConfirmRoot.SetActive(this.currentState == SaveFileScreen.State.ConfirmCopy);
		this.eraseConfirmRoot.SetActive(this.currentState == SaveFileScreen.State.ConfirmErase);
		this.ngpConfirmRoot.SetActive(this.currentState == SaveFileScreen.State.NGPConfirm);
		this.copyFromWindow.SetActive(this.currentState == SaveFileScreen.State.CopyFrom);
		this.copyToWindow.SetActive(this.currentState == SaveFileScreen.State.CopyTo);
		this.eraseWindow.SetActive(this.currentState == SaveFileScreen.State.Erase);
		this.ngpSourceWindow.SetActive(this.currentState == SaveFileScreen.State.NGPSource);
		this.ngpDestinationWindow.SetActive(this.currentState == SaveFileScreen.State.NGPDestination);
		if (this.currentState != SaveFileScreen.State.CopyFrom && this.currentState != SaveFileScreen.State.CopyTo && this.currentState != SaveFileScreen.State.Erase && this.currentState != SaveFileScreen.State.NGPSource)
		{
			bool flag = this.currentState == SaveFileScreen.State.NGPDestination;
		}
		for (int j = 0; j < 3; j++)
		{
			this.displays[j].SetIconState(this.currentState == SaveFileScreen.State.CopyFrom || this.currentState == SaveFileScreen.State.CopyTo || this.currentState == SaveFileScreen.State.NGPSource || this.currentState == SaveFileScreen.State.NGPDestination, this.currentState == SaveFileScreen.State.Erase);
			switch (this.currentState)
			{
			case SaveFileScreen.State.Standard:
				this.displays[j].SetButton(true, Color.white, true);
				break;
			case SaveFileScreen.State.CopyFrom:
				if (FileUtil.IsSaveFileReal(j))
				{
					this.displays[j].SetButton(true, Color.white, true);
				}
				else
				{
					this.displays[j].SetButton(false, this.newGameColor, false);
				}
				break;
			case SaveFileScreen.State.CopyTo:
				if (j == this.copyFrom)
				{
					this.displays[j].SetButton(false, this.copyFromColor, true);
				}
				else
				{
					this.displays[j].SetButton(true, Color.white, true);
				}
				break;
			case SaveFileScreen.State.Erase:
				if (FileUtil.IsSaveFileReal(j))
				{
					this.displays[j].SetButton(true, Color.white, true);
				}
				else
				{
					this.displays[j].SetButton(false, this.newGameColor, false);
				}
				break;
			case SaveFileScreen.State.NGPSource:
				if (FileUtil.IsSaveFileCompleted(j))
				{
					this.displays[j].SetButton(true, Color.white, true);
				}
				else
				{
					this.displays[j].SetButton(false, this.newGameColor, false);
				}
				break;
			case SaveFileScreen.State.NGPDestination:
				if (j == this.copyFrom)
				{
					this.displays[j].SetButton(false, this.copyFromColor, true);
				}
				else
				{
					this.displays[j].SetButton(true, Color.white, true);
				}
				break;
			}
		}
	}

	public MainMenuToGameplay toGameplay;

	public GameSaveWrapper newGameFile;

	public SaveFileDisplay[] displays;

	public GameObject standardButtons;

	public GameObject copyFromWindow;

	public GameObject copyToWindow;

	public GameObject eraseWindow;

	public GameObject ngpSourceWindow;

	public GameObject ngpDestinationWindow;

	public GameObject fileSelectRoot;

	public GameObject copyConfirmRoot;

	public SaveFileDisplay copyFromDisplay;

	public SaveFileDisplay copyToDisplay;

	public GameObject eraseConfirmRoot;

	public SaveFileDisplay eraseDisplay;

	public GameObject ngpConfirmRoot;

	public SaveFileDisplay ngpFromDisplay;

	public SaveFileDisplay ngpToDisplay;

	public Color copyFromColor;

	public Color newGameColor;

	private int copyFrom;

	private int copyTo;

	private int eraseTarget;

	private SaveFileScreen.State currentState;

	private enum State
	{
		Standard,
		CopyFrom,
		CopyTo,
		ConfirmCopy,
		Erase,
		ConfirmErase,
		NGPSource,
		NGPDestination,
		NGPConfirm
	}
}
