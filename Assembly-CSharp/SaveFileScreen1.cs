using System;
using UnityEngine;

// Token: 0x0200007A RID: 122
public class SaveFileScreen1 : MonoBehaviour
{
	// Token: 0x0600018F RID: 399 RVA: 0x00003528 File Offset: 0x00001728
	private void OnEnable()
	{
		this.UpdateState();
	}

	// Token: 0x06000190 RID: 400 RVA: 0x00003530 File Offset: 0x00001730
	public void PressSaveFileButton(int index)
	{
		GameData.g.SetSaveFile(index);
		GameData.g.LoadSaveData(PreloadSave.preloadedSaveData);
		this.toGameplay.LoadGameplay();
	}

	// Token: 0x06000191 RID: 401 RVA: 0x0001CA00 File Offset: 0x0001AC00
	private void UpdateState()
	{
		for (int i = 0; i < this.displays.Length; i++)
		{
			this.displays[i].Load(FileUtil.gameSaveDataInfo[i], i);
		}
	}

	// Token: 0x0400027B RID: 635
	public MainMenuToGameplay toGameplay;

	// Token: 0x0400027C RID: 636
	public SaveFileDisplay[] displays;
}
