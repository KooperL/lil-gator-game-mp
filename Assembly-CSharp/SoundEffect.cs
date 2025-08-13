using System;
using UnityEngine;

// Token: 0x020001AC RID: 428
[Serializable]
public class SoundEffect
{
	// Token: 0x1700007C RID: 124
	// (get) Token: 0x060008D3 RID: 2259 RVA: 0x00029948 File Offset: 0x00027B48
	public float Volume
	{
		get
		{
			return Mathf.Lerp(this.minVolume, this.maxVolume, Random.value);
		}
	}

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x060008D4 RID: 2260 RVA: 0x00029960 File Offset: 0x00027B60
	public float Pitch
	{
		get
		{
			return Mathf.Lerp(this.minPitch, this.maxPitch, Random.value);
		}
	}

	// Token: 0x04000AE6 RID: 2790
	public AudioClip audioClip;

	// Token: 0x04000AE7 RID: 2791
	public float maxVolume = 1f;

	// Token: 0x04000AE8 RID: 2792
	public float minVolume = 1f;

	// Token: 0x04000AE9 RID: 2793
	public float maxPitch = 1f;

	// Token: 0x04000AEA RID: 2794
	public float minPitch = 1f;
}
