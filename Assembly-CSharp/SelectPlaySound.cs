using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200038B RID: 907
public class SelectPlaySound : MonoBehaviour, ISelectHandler, IEventSystemHandler
{
	// Token: 0x06001148 RID: 4424 RVA: 0x00056A0C File Offset: 0x00054C0C
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

	// Token: 0x06001149 RID: 4425 RVA: 0x00056A6C File Offset: 0x00054C6C
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

	// Token: 0x04001632 RID: 5682
	public static float lastSoundTime;

	// Token: 0x04001633 RID: 5683
	private const float minVolume = 0.1f;

	// Token: 0x04001634 RID: 5684
	private const float minDelay = 0.05f;

	// Token: 0x04001635 RID: 5685
	private const float maxDelay = 0.25f;

	// Token: 0x04001636 RID: 5686
	public AudioSource audioSource;

	// Token: 0x04001637 RID: 5687
	public AudioSourceVariance audioSourceVariance;

	// Token: 0x04001638 RID: 5688
	private float sourceVolume = -1f;
}
