using System;
using UnityEngine;

public class SaveFileScreen1 : MonoBehaviour
{
	// Token: 0x0600015F RID: 351 RVA: 0x0000863C File Offset: 0x0000683C
	private void OnEnable()
	{
		this.UpdateState();
	}

	// Token: 0x06000160 RID: 352 RVA: 0x00008644 File Offset: 0x00006844
	public void PressSaveFileButton(int index)
	{
		GameData.g.SetSaveFile(index);
		GameData.g.LoadSaveData(PreloadSave.preloadedSaveData);
		this.toGameplay.LoadGameplay();
	}

	// Token: 0x06000161 RID: 353 RVA: 0x0000866C File Offset: 0x0000686C
	private void UpdateState()
	{
		for (int i = 0; i < this.displays.Length; i++)
		{
			this.displays[i].Load(FileUtil.gameSaveDataInfo[i], i);
		}
	}

	public MainMenuToGameplay toGameplay;

	public SaveFileDisplay[] displays;
}
