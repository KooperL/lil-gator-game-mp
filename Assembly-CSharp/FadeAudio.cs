using System;
using UnityEngine;

public class FadeAudio : MonoBehaviour
{
	// Token: 0x0600004D RID: 77 RVA: 0x00003344 File Offset: 0x00001544
	public void FadeIn()
	{
		base.enabled = true;
		this.fadeTarget = 1f;
		this.audioSource.volume = this.fade * this.volume;
		if (!this.audioSource.isPlaying)
		{
			this.audioSource.Play();
		}
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00003393 File Offset: 0x00001593
	public void FadeOut()
	{
		base.enabled = true;
		this.fadeTarget = 0f;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x000033A8 File Offset: 0x000015A8
	private void Update()
	{
		this.fade = Mathf.MoveTowards(this.fade, this.fadeTarget, Time.deltaTime / ((this.fadeTarget > this.fade) ? this.fadeInTime : this.fadeOutTime));
		this.audioSource.volume = this.fade * this.volume;
		if (this.fade == this.fadeTarget)
		{
			if (this.fadeTarget == 0f)
			{
				this.audioSource.Stop();
			}
			base.enabled = false;
		}
	}

	public AudioSource audioSource;

	public float volume = 1f;

	public float fadeInTime = 0.2f;

	public float fadeOutTime = 1f;

	private float fade;

	private float fadeTarget;
}
