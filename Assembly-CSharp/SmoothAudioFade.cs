using System;
using System.Collections;
using UnityEngine;

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

	public AudioSource audioSource;

	private float t;

	private float target;

	public float fadeInSpeed;

	public float fadeOutSpeed;

	private float volume;

	private IEnumerator currentlyRunning;
}
