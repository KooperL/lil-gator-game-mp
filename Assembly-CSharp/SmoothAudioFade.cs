using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000336 RID: 822
public class SmoothAudioFade : MonoBehaviour
{
	// Token: 0x06001008 RID: 4104 RVA: 0x0000DDCB File Offset: 0x0000BFCB
	private void Awake()
	{
		this.volume = this.audioSource.volume;
	}

	// Token: 0x06001009 RID: 4105 RVA: 0x00052DBC File Offset: 0x00050FBC
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

	// Token: 0x0600100A RID: 4106 RVA: 0x00052E24 File Offset: 0x00051024
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

	// Token: 0x0600100B RID: 4107 RVA: 0x0000DDDE File Offset: 0x0000BFDE
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

	// Token: 0x040014C2 RID: 5314
	public AudioSource audioSource;

	// Token: 0x040014C3 RID: 5315
	private float t;

	// Token: 0x040014C4 RID: 5316
	private float target;

	// Token: 0x040014C5 RID: 5317
	public float fadeInSpeed;

	// Token: 0x040014C6 RID: 5318
	public float fadeOutSpeed;

	// Token: 0x040014C7 RID: 5319
	private float volume;

	// Token: 0x040014C8 RID: 5320
	private IEnumerator currentlyRunning;
}
