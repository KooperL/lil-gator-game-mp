using System;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class FadeAudio : MonoBehaviour
{
	// Token: 0x06000046 RID: 70 RVA: 0x00017DA8 File Offset: 0x00015FA8
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

	// Token: 0x06000047 RID: 71 RVA: 0x00002365 File Offset: 0x00000565
	public void FadeOut()
	{
		base.enabled = true;
		this.fadeTarget = 0f;
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00017DF8 File Offset: 0x00015FF8
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

	// Token: 0x0400005D RID: 93
	public AudioSource audioSource;

	// Token: 0x0400005E RID: 94
	public float volume = 1f;

	// Token: 0x0400005F RID: 95
	public float fadeInTime = 0.2f;

	// Token: 0x04000060 RID: 96
	public float fadeOutTime = 1f;

	// Token: 0x04000061 RID: 97
	private float fade;

	// Token: 0x04000062 RID: 98
	private float fadeTarget;
}
