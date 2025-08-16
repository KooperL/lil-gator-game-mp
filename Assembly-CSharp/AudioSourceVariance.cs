using System;
using UnityEngine;

public class AudioSourceVariance : MonoBehaviour
{
	// Token: 0x06000255 RID: 597 RVA: 0x00003F29 File Offset: 0x00002129
	private void Awake()
	{
		this.audioSource = base.GetComponent<AudioSource>();
	}

	// Token: 0x06000256 RID: 598 RVA: 0x00003F37 File Offset: 0x00002137
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

	// Token: 0x06000257 RID: 599 RVA: 0x00003F70 File Offset: 0x00002170
	[ContextMenu("Play")]
	public void Play()
	{
		this.PlayModified(1f, 1f);
	}

	// Token: 0x06000258 RID: 600 RVA: 0x0001F968 File Offset: 0x0001DB68
	public void PlayModified(float volumeMod, float pitchMod)
	{
		int num = Mathf.FloorToInt(global::UnityEngine.Random.value * (float)this.audioClips.Length);
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
		float num2 = global::UnityEngine.Random.Range(this.minVolume, this.maxVolume) * volumeMod;
		float num3 = global::UnityEngine.Random.Range(this.minPitch, this.maxPitch) * pitchMod;
		if (this.audioSource != null)
		{
			this.audioSource.clip = audioClip;
			this.audioSource.volume = num2;
			this.audioSource.pitch = num3;
			if (this.randomizeStart)
			{
				this.audioSource.time = global::UnityEngine.Random.value * audioClip.length;
			}
			this.audioSource.priority = this.priority;
			this.audioSource.Play();
			return;
		}
		PlayAudio.p.PlayAtPoint(audioClip, this.playHere ? base.transform.position : MainCamera.t.TransformPoint(new Vector3(0f, 0f, 8f)), num2, num3, this.priority);
	}

	private static int lastPlayedIndex = -1;

	public AudioClip[] audioClips;

	[Range(0f, 2f)]
	public float maxVolume = 1f;

	[Range(0f, 2f)]
	public float minVolume = 1f;

	[Range(0f, 2f)]
	public float maxPitch = 1f;

	[Range(0f, 2f)]
	public float minPitch = 1f;

	private AudioSource audioSource;

	public bool playOnAwake;

	public float delay;

	public bool playHere = true;

	[Range(0f, 256f)]
	public int priority = 128;

	public bool randomizeStart;
}
