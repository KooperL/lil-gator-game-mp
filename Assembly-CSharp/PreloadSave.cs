using System;
using UnityEngine;

// Token: 0x02000076 RID: 118
public class PreloadSave : MonoBehaviour
{
	// Token: 0x06000180 RID: 384 RVA: 0x00003468 File Offset: 0x00001668
	public void Preload()
	{
		if (this.preloadedSaveFile != null)
		{
			PreloadSave.preloadedSaveData = this.preloadedSaveFile.gameSaveData;
		}
	}

	// Token: 0x04000244 RID: 580
	public static GameSaveData preloadedSaveData;

	// Token: 0x04000245 RID: 581
	public GameSaveWrapper preloadedSaveFile;
}
