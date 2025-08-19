using System;
using System.Collections;
using UnityEngine;

public class SmoothAudioFade : MonoBehaviour
{
	// Token: 0x06001063 RID: 4195 RVA: 0x0000E13E File Offset: 0x0000C33E
	private void Awake()
	{
		this.volume = this.audioSource.volume;
	}

	// Token: 0x06001064 RID: 4196 RVA: 0x00054CBC File Offset: 0x00052EBC
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

	// Token: 0x06001065 RID: 4197 RVA: 0x00054D24 File Offset: 0x00052F24
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

	// Token: 0x06001066 RID: 4198 RVA: 0x0000E151 File Offset: 0x0000C351
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
