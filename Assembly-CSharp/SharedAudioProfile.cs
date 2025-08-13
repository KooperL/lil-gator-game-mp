using System;
using UnityEngine;

// Token: 0x0200007E RID: 126
[CreateAssetMenu]
public class SharedAudioProfile : ScriptableObject
{
	// Token: 0x17000015 RID: 21
	// (get) Token: 0x06000206 RID: 518 RVA: 0x0000B3F7 File Offset: 0x000095F7
	public float MinDistance
	{
		get
		{
			return this.minimumRatio * this.radius;
		}
	}

	// Token: 0x040002A5 RID: 677
	public float radius = 15f;

	// Token: 0x040002A6 RID: 678
	public float minimumRatio = 0.25f;

	// Token: 0x040002A7 RID: 679
	public AudioClip audioClip;

	// Token: 0x040002A8 RID: 680
	[Range(0f, 1f)]
	public float volume = 1f;

	// Token: 0x040002A9 RID: 681
	[Range(0f, 2f)]
	public float pitch = 1f;

	// Token: 0x040002AA RID: 682
	[Range(0f, 1f)]
	public float spacialBlend = 1f;
}
