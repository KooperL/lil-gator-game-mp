using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectPlaySound : MonoBehaviour, ISelectHandler, IEventSystemHandler
{
	// Token: 0x060011A8 RID: 4520 RVA: 0x000589A8 File Offset: 0x00056BA8
	private float ModifyVolume(float volume)
	{
		if (this.sourceVolume < 0f)
		{
			this.sourceVolume = volume;
		}
		volume = this.sourceVolume * Mathf.Lerp(0.1f, 1f, Mathf.InverseLerp(0.05f, 0.25f, Time.time - SelectPlaySound.lastSoundTime));
		SelectPlaySound.lastSoundTime = Time.time;
		return volume;
	}

	// Token: 0x060011A9 RID: 4521 RVA: 0x00058A08 File Offset: 0x00056C08
	public void OnSelect(BaseEventData eventData)
	{
		if (this.audioSource != null)
		{
			this.audioSource.volume = this.ModifyVolume(this.audioSource.volume);
			this.audioSource.Play();
		}
		if (this.audioSourceVariance != null)
		{
			this.audioSourceVariance.maxVolume = (this.audioSourceVariance.minVolume = this.ModifyVolume(this.audioSourceVariance.maxVolume));
			this.audioSourceVariance.Play();
		}
	}

	public static float lastSoundTime;

	private const float minVolume = 0.1f;

	private const float minDelay = 0.05f;

	private const float maxDelay = 0.25f;

	public AudioSource audioSource;

	public AudioSourceVariance audioSourceVariance;

	private float sourceVolume = -1f;
}
