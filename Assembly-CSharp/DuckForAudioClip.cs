using System;
using UnityEngine;

public class DuckForAudioClip : MonoBehaviour
{
	// Token: 0x06000063 RID: 99 RVA: 0x000036FB File Offset: 0x000018FB
	private void OnValidate()
	{
		if (this.audioSource == null)
		{
			this.audioSource = base.GetComponent<AudioSource>();
		}
		if (this.audioSource != null)
		{
			this.audioSource.playOnAwake = false;
		}
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00003731 File Offset: 0x00001931
	private void OnEnable()
	{
		MusicStateManager.m.DuckMusic(0.25f + this.audioSource.clip.length + this.extraDelayTime);
		this.audioSource.PlayDelayed(0.25f + this.extraDelayTime);
	}

	public AudioSource audioSource;

	public float extraDelayTime;
}
