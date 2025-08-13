using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200026E RID: 622
public class SmoothAudioFade : MonoBehaviour
{
	// Token: 0x06000D5A RID: 3418 RVA: 0x00040828 File Offset: 0x0003EA28
	private void Awake()
	{
		this.volume = this.audioSource.volume;
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x0004083C File Offset: 0x0003EA3C
	private void OnEnable()
	{
		this.target = 1f;
		this.audioSource.volume = this.volume * this.t;
		if (!this.audioSource.isPlaying)
		{
			this.audioSource.Play();
		}
		if (this.currentlyRunning == null)
		{
			this.currentlyRunning = this.RunFade();
			CoroutineUtil.Start(this.currentlyRunning);
		}
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x000408A4 File Offset: 0x0003EAA4
	private void OnDisable()
	{
		if (this.audioSource == null)
		{
			return;
		}
		this.target = 0f;
		this.audioSource.volume = this.volume * this.t;
		if (!this.audioSource.isPlaying)
		{
			this.audioSource.Play();
		}
		if (this.currentlyRunning == null)
		{
			this.currentlyRunning = this.RunFade();
			CoroutineUtil.Start(this.currentlyRunning);
		}
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x0004091B File Offset: 0x0003EB1B
	private IEnumerator RunFade()
	{
		yield return null;
		if (this.audioSource != null)
		{
			this.audioSource.transform.parent = null;
		}
		while (this.t != this.target)
		{
			yield return null;
			this.t = Mathf.MoveTowards(this.t, this.target, ((this.target > this.t) ? this.fadeInSpeed : this.fadeOutSpeed) * Time.deltaTime);
			if (this.audioSource != null)
			{
				this.audioSource.volume = this.volume * this.t;
			}
		}
		this.currentlyRunning = null;
		if (this.t == 0f && this.audioSource != null)
		{
			this.audioSource.transform.parent = base.transform;
		}
		yield break;
	}

	// Token: 0x0400119C RID: 4508
	public AudioSource audioSource;

	// Token: 0x0400119D RID: 4509
	private float t;

	// Token: 0x0400119E RID: 4510
	private float target;

	// Token: 0x0400119F RID: 4511
	public float fadeInSpeed;

	// Token: 0x040011A0 RID: 4512
	public float fadeOutSpeed;

	// Token: 0x040011A1 RID: 4513
	private float volume;

	// Token: 0x040011A2 RID: 4514
	private IEnumerator currentlyRunning;
}
