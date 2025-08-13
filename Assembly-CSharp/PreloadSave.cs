using System;
using UnityEngine;

// Token: 0x0200005A RID: 90
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

	// Token: 0x040001D6 RID: 470
	public static GameSaveData preloadedSaveData;

	// Token: 0x040001D7 RID: 471
	public GameSaveWrapper preloadedSaveFile;
}
