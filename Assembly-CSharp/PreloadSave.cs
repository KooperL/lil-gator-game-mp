using System;
using UnityEngine;

public class PreloadSave : MonoBehaviour
{
	// Token: 0x0600014F RID: 335 RVA: 0x00007D3B File Offset: 0x00005F3B
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
