using System;
using UnityEngine;

public class DuckForAudioClip : MonoBehaviour
{
	// Token: 0x06000064 RID: 100 RVA: 0x000024FD File Offset: 0x000006FD
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

	// Token: 0x06000065 RID: 101 RVA: 0x00002533 File Offset: 0x00000733
	private void OnEnable()
	{
		MusicStateManager.m.DuckMusic(0.25f + this.audioSource.clip.length + this.extraDelayTime);
		this.audioSource.PlayDelayed(0.25f + this.extraDelayTime);
	}

	public AudioSource audioSource;

	public float extraDelayTime;
}
