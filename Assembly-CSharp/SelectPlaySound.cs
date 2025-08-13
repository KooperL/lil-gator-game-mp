using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020002AE RID: 686
public class SelectPlaySound : MonoBehaviour, ISelectHandler, IEventSystemHandler
{
	// Token: 0x06000E78 RID: 3704 RVA: 0x000451C4 File Offset: 0x000433C4
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

	// Token: 0x06000E79 RID: 3705 RVA: 0x00045224 File Offset: 0x00043424
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

	// Token: 0x040012CA RID: 4810
	public static float lastSoundTime;

	// Token: 0x040012CB RID: 4811
	private const float minVolume = 0.1f;

	// Token: 0x040012CC RID: 4812
	private const float minDelay = 0.05f;

	// Token: 0x040012CD RID: 4813
	private const float maxDelay = 0.25f;

	// Token: 0x040012CE RID: 4814
	public AudioSource audioSource;

	// Token: 0x040012CF RID: 4815
	public AudioSourceVariance audioSourceVariance;

	// Token: 0x040012D0 RID: 4816
	private float sourceVolume = -1f;
}
