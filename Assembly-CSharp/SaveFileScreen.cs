using System;
using UnityEngine;

// Token: 0x0200005C RID: 92
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

	// Token: 0x040001F4 RID: 500
	public MainMenuToGameplay toGameplay;

	// Token: 0x040001F5 RID: 501
	public GameSaveWrapper newGameFile;

	// Token: 0x040001F6 RID: 502
	public SaveFileDisplay[] displays;

	// Token: 0x040001F7 RID: 503
	public GameObject standardButtons;

	// Token: 0x040001F8 RID: 504
	public GameObject copyFromWindow;

	// Token: 0x040001F9 RID: 505
	public GameObject copyToWindow;

	// Token: 0x040001FA RID: 506
	public GameObject eraseWindow;

	// Token: 0x040001FB RID: 507
	public GameObject ngpSourceWindow;

	// Token: 0x040001FC RID: 508
	public GameObject ngpDestinationWindow;

	// Token: 0x040001FD RID: 509
	public GameObject fileSelectRoot;

	// Token: 0x040001FE RID: 510
	public GameObject copyConfirmRoot;

	// Token: 0x040001FF RID: 511
	public SaveFileDisplay copyFromDisplay;

	// Token: 0x04000200 RID: 512
	public SaveFileDisplay copyToDisplay;

	// Token: 0x04000201 RID: 513
	public GameObject eraseConfirmRoot;

	// Token: 0x04000202 RID: 514
	public SaveFileDisplay eraseDisplay;

	// Token: 0x04000203 RID: 515
	public GameObject ngpConfirmRoot;

	// Token: 0x04000204 RID: 516
	public SaveFileDisplay ngpFromDisplay;

	// Token: 0x04000205 RID: 517
	public SaveFileDisplay ngpToDisplay;

	// Token: 0x04000206 RID: 518
	public Color copyFromColor;

	// Token: 0x04000207 RID: 519
	public Color newGameColor;

	// Token: 0x04000208 RID: 520
	private int copyFrom;

	// Token: 0x04000209 RID: 521
	private int copyTo;

	// Token: 0x0400020A RID: 522
	private int eraseTarget;

	// Token: 0x0400020B RID: 523
	private SaveFileScreen.State currentState;

	// Token: 0x0200036B RID: 875
	private enum State
	{
		// Token: 0x04001A41 RID: 6721
		Standard,
		// Token: 0x04001A42 RID: 6722
		CopyFrom,
		// Token: 0x04001A43 RID: 6723
		CopyTo,
		// Token: 0x04001A44 RID: 6724
		ConfirmCopy,
		// Token: 0x04001A45 RID: 6725
		Erase,
		// Token: 0x04001A46 RID: 6726
		ConfirmErase,
		// Token: 0x04001A47 RID: 6727
		NGPSource,
		// Token: 0x04001A48 RID: 6728
		NGPDestination,
		// Token: 0x04001A49 RID: 6729
		NGPConfirm
	}
}
