using System;
using UnityEngine;

[Serializable]
public class SoundEffect
{
	// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x0000A27B File Offset: 0x0000847B
	public float Volume
	{
		get
		{
			return Mathf.Lerp(this.minVolume, this.maxVolume, global::UnityEngine.Random.value);
		}
	}

	// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x0000A293 File Offset: 0x00008493
	public float Pitch
	{
		get
		{
			return Mathf.Lerp(this.minPitch, this.maxPitch, global::UnityEngine.Random.value);
		}
	}

	public AudioClip audioClip;

	public float maxVolume = 1f;

	public float minVolume = 1f;

	public float maxPitch = 1f;

	public float minPitch = 1f;
}
