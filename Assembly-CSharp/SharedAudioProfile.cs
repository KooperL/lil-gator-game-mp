using System;
using UnityEngine;

[CreateAssetMenu]
public class SharedAudioProfile : ScriptableObject
{
	// (get) Token: 0x0600024B RID: 587 RVA: 0x00003E27 File Offset: 0x00002027
	public float MinDistance
	{
		get
		{
			return this.minimumRatio * this.radius;
		}
	}

	public float radius = 15f;

	public float minimumRatio = 0.25f;

	public AudioClip audioClip;

	[Range(0f, 1f)]
	public float volume = 1f;

	[Range(0f, 2f)]
	public float pitch = 1f;

	[Range(0f, 1f)]
	public float spacialBlend = 1f;
}
