using System;
using UnityEngine;

// Token: 0x020000A2 RID: 162
public class AudioSourceVariance : MonoBehaviour
{
	// Token: 0x06000248 RID: 584 RVA: 0x00003E3D File Offset: 0x0000203D
	private void Awake()
	{
		this.audioSource = base.GetComponent<AudioSource>();
	}

	// Token: 0x06000249 RID: 585 RVA: 0x00003E4B File Offset: 0x0000204B
	private void OnEnable()
	{
		if (this.playOnAwake)
		{
			if (this.delay == 0f)
			{
				base.Invoke("Play", 0.0001f);
				return;
			}
			base.Invoke("Play", this.delay);
		}
	}

	// Token: 0x0600024A RID: 586 RVA: 0x00003E84 File Offset: 0x00002084
	[ContextMenu("Play")]
	public void Play()
	{
		this.PlayModified(1f, 1f);
	}

	// Token: 0x0600024B RID: 587 RVA: 0x0001F0A0 File Offset: 0x0001D2A0
	public void PlayModified(float volumeMod, float pitchMod)
	{
		int num = Mathf.FloorToInt(Random.value * (float)this.audioClips.Length);
		if (AudioSourceVariance.lastPlayedIndex == num && this.audioClips.Length > 1)
		{
			num++;
			if (num == this.audioClips.Length)
			{
				num = 0;
			}
		}
		AudioSourceVariance.lastPlayedIndex = num;
		AudioClip audioClip = this.audioClips[num];
		float num2 = Random.Range(this.minVolume, this.maxVolume) * volumeMod;
		float num3 = Random.Range(this.minPitch, this.maxPitch) * pitchMod;
		if (this.audioSource != null)
		{
			this.audioSource.clip = audioClip;
			this.audioSource.volume = num2;
			this.audioSource.pitch = num3;
			if (this.randomizeStart)
			{
				this.audioSource.time = Random.value * audioClip.length;
			}
			this.audioSource.priority = this.priority;
			this.audioSource.Play();
			return;
		}
		PlayAudio.p.PlayAtPoint(audioClip, this.playHere ? base.transform.position : MainCamera.t.TransformPoint(new Vector3(0f, 0f, 8f)), num2, num3, this.priority);
	}

	// Token: 0x0400033B RID: 827
	private static int lastPlayedIndex = -1;

	// Token: 0x0400033C RID: 828
	public AudioClip[] audioClips;

	// Token: 0x0400033D RID: 829
	[Range(0f, 2f)]
	public float maxVolume = 1f;

	// Token: 0x0400033E RID: 830
	[Range(0f, 2f)]
	public float minVolume = 1f;

	// Token: 0x0400033F RID: 831
	[Range(0f, 2f)]
	public float maxPitch = 1f;

	// Token: 0x04000340 RID: 832
	[Range(0f, 2f)]
	public float minPitch = 1f;

	// Token: 0x04000341 RID: 833
	private AudioSource audioSource;

	// Token: 0x04000342 RID: 834
	public bool playOnAwake;

	// Token: 0x04000343 RID: 835
	public float delay;

	// Token: 0x04000344 RID: 836
	public bool playHere = true;

	// Token: 0x04000345 RID: 837
	[Range(0f, 256f)]
	public int priority = 128;

	// Token: 0x04000346 RID: 838
	public bool randomizeStart;
}
