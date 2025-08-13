using System;
using UnityEngine;

// Token: 0x020000A0 RID: 160
[CreateAssetMenu]
public class SharedAudioProfile : ScriptableObject
{
	// Token: 0x17000026 RID: 38
	// (get) Token: 0x0600023E RID: 574 RVA: 0x00003D3B File Offset: 0x00001F3B
	public float MinDistance
	{
		get
		{
			return this.minimumRatio * this.radius;
		}
	}

	// Token: 0x04000331 RID: 817
	public float radius = 15f;

	// Token: 0x04000332 RID: 818
	public float minimumRatio = 0.25f;

	// Token: 0x04000333 RID: 819
	public AudioClip audioClip;

	// Token: 0x04000334 RID: 820
	[Range(0f, 1f)]
	public float volume = 1f;

	// Token: 0x04000335 RID: 821
	[Range(0f, 2f)]
	public float pitch = 1f;

	// Token: 0x04000336 RID: 822
	[Range(0f, 1f)]
	public float spacialBlend = 1f;
}
