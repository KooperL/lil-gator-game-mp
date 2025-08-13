using System;
using UnityEngine;

// Token: 0x0200001D RID: 29
public class DuckForAudioClip : MonoBehaviour
{
	// Token: 0x0600005C RID: 92 RVA: 0x00002499 File Offset: 0x00000699
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

	// Token: 0x0600005D RID: 93 RVA: 0x000024CF File Offset: 0x000006CF
	private void OnEnable()
	{
		MusicStateManager.m.DuckMusic(0.25f + this.audioSource.clip.length + this.extraDelayTime);
		this.audioSource.PlayDelayed(0.25f + this.extraDelayTime);
	}

	// Token: 0x04000073 RID: 115
	public AudioSource audioSource;

	// Token: 0x04000074 RID: 116
	public float extraDelayTime;
}
