using System;
using UnityEngine;

public class SaveFileScreen1 : MonoBehaviour
{
	// Token: 0x06000198 RID: 408 RVA: 0x000035DA File Offset: 0x000017DA
	private void OnEnable()
	{
		this.UpdateState();
	}

	// Token: 0x06000199 RID: 409 RVA: 0x000035E2 File Offset: 0x000017E2
	public void PressSaveFileButton(int index)
	{
		GameData.g.SetSaveFile(index);
		GameData.g.LoadSaveData(PreloadSave.preloadedSaveData);
		this.toGameplay.LoadGameplay();
	}

	// Token: 0x0600019A RID: 410 RVA: 0x0001D3FC File Offset: 0x0001B5FC
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
