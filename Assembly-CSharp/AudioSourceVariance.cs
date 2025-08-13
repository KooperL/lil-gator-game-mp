using System;
using UnityEngine;

// Token: 0x02000080 RID: 128
public class AudioSourceVariance : MonoBehaviour
{
	// Token: 0x06000210 RID: 528 RVA: 0x0000B612 File Offset: 0x00009812
	private void Awake()
	{
		this.audioSource = base.GetComponent<AudioSource>();
	}

	// Token: 0x06000211 RID: 529 RVA: 0x0000B620 File Offset: 0x00009820
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

	// Token: 0x06000212 RID: 530 RVA: 0x0000B659 File Offset: 0x00009859
	[ContextMenu("Play")]
	public void Play()
	{
		this.PlayModified(1f, 1f);
	}

	// Token: 0x06000213 RID: 531 RVA: 0x0000B66C File Offset: 0x0000986C
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

	// Token: 0x040002AF RID: 687
	private static int lastPlayedIndex = -1;

	// Token: 0x040002B0 RID: 688
	public AudioClip[] audioClips;

	// Token: 0x040002B1 RID: 689
	[Range(0f, 2f)]
	public float maxVolume = 1f;

	// Token: 0x040002B2 RID: 690
	[Range(0f, 2f)]
	public float minVolume = 1f;

	// Token: 0x040002B3 RID: 691
	[Range(0f, 2f)]
	public float maxPitch = 1f;

	// Token: 0x040002B4 RID: 692
	[Range(0f, 2f)]
	public float minPitch = 1f;

	// Token: 0x040002B5 RID: 693
	private AudioSource audioSource;

	// Token: 0x040002B6 RID: 694
	public bool playOnAwake;

	// Token: 0x040002B7 RID: 695
	public float delay;

	// Token: 0x040002B8 RID: 696
	public bool playHere = true;

	// Token: 0x040002B9 RID: 697
	[Range(0f, 256f)]
	public int priority = 128;

	// Token: 0x040002BA RID: 698
	public bool randomizeStart;
}
