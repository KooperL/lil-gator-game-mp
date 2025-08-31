using System;
using UnityEngine;

[Serializable]
public class SoundEffect
{
	// (get) Token: 0x060008D3 RID: 2259 RVA: 0x00029948 File Offset: 0x00027B48
	public float Volume
	{
		get
		{
			return Mathf.Lerp(this.minVolume, this.maxVolume, Random.value);
		}
	}

	// (get) Token: 0x060008D4 RID: 2260 RVA: 0x00029960 File Offset: 0x00027B60
	public float Pitch
	{
		get
		{
			return Mathf.Lerp(this.minPitch, this.maxPitch, Random.value);
		}
	}

	public AudioClip audioClip;

	public float maxVolume = 1f;

	public float minVolume = 1f;

	public float maxPitch = 1f;

	public float minPitch = 1f;
}
