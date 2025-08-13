using System;
using UnityEngine;

// Token: 0x02000227 RID: 551
[Serializable]
public class SoundEffect
{
	// Token: 0x170000FE RID: 254
	// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00009F47 File Offset: 0x00008147
	public float Volume
	{
		get
		{
			return Mathf.Lerp(this.minVolume, this.maxVolume, Random.value);
		}
	}

	// Token: 0x170000FF RID: 255
	// (get) Token: 0x06000A5B RID: 2651 RVA: 0x00009F5F File Offset: 0x0000815F
	public float Pitch
	{
		get
		{
			return Mathf.Lerp(this.minPitch, this.maxPitch, Random.value);
		}
	}

	// Token: 0x04000CEA RID: 3306
	public AudioClip audioClip;

	// Token: 0x04000CEB RID: 3307
	public float maxVolume = 1f;

	// Token: 0x04000CEC RID: 3308
	public float minVolume = 1f;

	// Token: 0x04000CED RID: 3309
	public float maxPitch = 1f;

	// Token: 0x04000CEE RID: 3310
	public float minPitch = 1f;
}
