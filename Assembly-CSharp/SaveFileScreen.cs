using System;
using UnityEngine;

// Token: 0x02000078 RID: 120
public class SaveFileScreen : MonoBehaviour
{
	// Token: 0x06000187 RID: 391 RVA: 0x000034FB File Offset: 0x000016FB
	private void OnEnable()
	{
		this.currentState = SaveFileScreen.State.Standard;
		this.UpdateState();
	}

	// Token: 0x06000188 RID: 392 RVA: 0x0000350A File Offset: 0x0000170A
	public void StartCopy()
	{
		this.currentState = SaveFileScreen.State.CopyFrom;
		this.UpdateState();
	}

	// Token: 0x06000189 RID: 393 RVA: 0x00003519 File Offset: 0x00001719
	public void StartErase()
	{
		this.currentState = SaveFileScreen.State.Erase;
		this.UpdateState();
	}

	// Token: 0x0600018A RID: 394 RVA: 0x0001C650 File Offset: 0x0001A850
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
		if (state != SaveFileScreen.State.ConfirmErase)
		{
			return;
		}
		FileUtil.EraseGameSaveData(this.eraseTarget);
		this.currentState = SaveFileScreen.State.Standard;
		this.UpdateState();
	}

	// Token: 0x0600018B RID: 395 RVA: 0x000034FB File Offset: 0x000016FB
	public void Cancel()
	{
		this.currentState = SaveFileScreen.State.Standard;
		this.UpdateState();
	}

	// Token: 0x0600018C RID: 396 RVA: 0x0001C6A4 File Offset: 0x0001A8A4
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
			this.toGameplay.StartFreshNewGame(index);
			return;
		case SaveFileScreen.State.CopyFrom:
			if (FileUtil.IsSaveFileStarted(index))
			{
				this.currentState = SaveFileScreen.State.CopyTo;
				this.copyFrom = index;
				this.UpdateState();
				return;
			}
			break;
		case SaveFileScreen.State.CopyTo:
			if (FileUtil.IsSaveFileStarted(index))
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
			break;
		case SaveFileScreen.State.Erase:
			if (FileUtil.IsSaveFileStarted(index))
			{
				this.currentState = SaveFileScreen.State.ConfirmErase;
				this.eraseTarget = index;
				this.UpdateState();
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x0600018D RID: 397 RVA: 0x0001C770 File Offset: 0x0001A970
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
		this.standardButtons.SetActive(this.currentState == SaveFileScreen.State.Standard);
		this.fileSelectRoot.SetActive(this.currentState != SaveFileScreen.State.ConfirmCopy && this.currentState != SaveFileScreen.State.ConfirmErase);
		this.copyConfirmRoot.SetActive(this.currentState == SaveFileScreen.State.ConfirmCopy);
		this.eraseConfirmRoot.SetActive(this.currentState == SaveFileScreen.State.ConfirmErase);
		this.copyFromWindow.SetActive(this.currentState == SaveFileScreen.State.CopyFrom);
		this.copyToWindow.SetActive(this.currentState == SaveFileScreen.State.CopyTo);
		this.eraseWindow.SetActive(this.currentState == SaveFileScreen.State.Erase);
		if (this.currentState != SaveFileScreen.State.CopyFrom && this.currentState != SaveFileScreen.State.CopyTo)
		{
			bool flag = this.currentState == SaveFileScreen.State.Erase;
		}
		for (int j = 0; j < 3; j++)
		{
			this.displays[j].SetIconState(this.currentState == SaveFileScreen.State.CopyFrom || this.currentState == SaveFileScreen.State.CopyTo, this.currentState == SaveFileScreen.State.Erase);
			switch (this.currentState)
			{
			case SaveFileScreen.State.Standard:
				this.displays[j].SetButton(true, Color.white, true);
				break;
			case SaveFileScreen.State.CopyFrom:
				if (FileUtil.IsSaveFileStarted(j))
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
				if (FileUtil.IsSaveFileStarted(j))
				{
					this.displays[j].SetButton(true, Color.white, true);
				}
				else
				{
					this.displays[j].SetButton(false, this.newGameColor, false);
				}
				break;
			}
		}
	}

	// Token: 0x04000261 RID: 609
	public MainMenuToGameplay toGameplay;

	// Token: 0x04000262 RID: 610
	public GameSaveWrapper newGameFile;

	// Token: 0x04000263 RID: 611
	public SaveFileDisplay[] displays;

	// Token: 0x04000264 RID: 612
	public GameObject standardButtons;

	// Token: 0x04000265 RID: 613
	public GameObject copyFromWindow;

	// Token: 0x04000266 RID: 614
	public GameObject copyToWindow;

	// Token: 0x04000267 RID: 615
	public GameObject eraseWindow;

	// Token: 0x04000268 RID: 616
	public GameObject fileSelectRoot;

	// Token: 0x04000269 RID: 617
	public GameObject copyConfirmRoot;

	// Token: 0x0400026A RID: 618
	public SaveFileDisplay copyFromDisplay;

	// Token: 0x0400026B RID: 619
	public SaveFileDisplay copyToDisplay;

	// Token: 0x0400026C RID: 620
	public GameObject eraseConfirmRoot;

	// Token: 0x0400026D RID: 621
	public SaveFileDisplay eraseDisplay;

	// Token: 0x0400026E RID: 622
	public Color copyFromColor;

	// Token: 0x0400026F RID: 623
	public Color newGameColor;

	// Token: 0x04000270 RID: 624
	private int copyFrom;

	// Token: 0x04000271 RID: 625
	private int copyTo;

	// Token: 0x04000272 RID: 626
	private int eraseTarget;

	// Token: 0x04000273 RID: 627
	private SaveFileScreen.State currentState;

	// Token: 0x02000079 RID: 121
	private enum State
	{
		// Token: 0x04000275 RID: 629
		Standard,
		// Token: 0x04000276 RID: 630
		CopyFrom,
		// Token: 0x04000277 RID: 631
		CopyTo,
		// Token: 0x04000278 RID: 632
		ConfirmCopy,
		// Token: 0x04000279 RID: 633
		Erase,
		// Token: 0x0400027A RID: 634
		ConfirmErase
	}
}
