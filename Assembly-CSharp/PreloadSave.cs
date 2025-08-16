using System;
using UnityEngine;

public class PreloadSave : MonoBehaviour
{
	// Token: 0x06000188 RID: 392 RVA: 0x0000350B File Offset: 0x0000170B
	public void Preload()
	{
		if (this.preloadedSaveFile != null)
		{
			PreloadSave.preloadedSaveData = this.preloadedSaveFile.gameSaveData;
		}
	}

	public static GameSaveData preloadedSaveData;

	public GameSaveWrapper preloadedSaveFile;
}
