using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class PlayAudio : MonoBehaviour
{
	// Token: 0x06000A93 RID: 2707 RVA: 0x0000A14A File Offset: 0x0000834A
	private void OnEnable()
	{
		PlayAudio.p = this;
	}

	// Token: 0x06000A94 RID: 2708 RVA: 0x0000A152 File Offset: 0x00008352
	public void Play(AudioClip audioClip, float volume = 1f, float pitch = 1f)
	{
		this.PlayAtPoint(audioClip, Player.Position, volume, pitch, 128);
	}

	// Token: 0x06000A95 RID: 2709 RVA: 0x0000A167 File Offset: 0x00008367
	public void PlayAtPoint(SoundEffect soundEffect, Vector3 point)
	{
		if (soundEffect == null || soundEffect.audioClip == null)
		{
			return;
		}
		this.PlayAtPoint(soundEffect.audioClip, point, soundEffect.Volume, soundEffect.Pitch, 128);
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x0000A199 File Offset: 0x00008399
	public void PlayAtPoint(AudioClip audioClip, Vector3 point, float volume = 1f, float pitch = 1f, int priority = 128)
	{
		this.PlayAtPoint(audioClip, point, this.defaultGroup, volume, pitch, priority);
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x0003D88C File Offset: 0x0003BA8C
	public void PlayAtPoint(AudioClip audioClip, Vector3 point, AudioMixerGroup mixerGroup, float volume = 1f, float pitch = 1f, int priority = 128)
	{
		GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.prefab);
		gameObject.transform.position = point;
		AudioSource component = gameObject.GetComponent<AudioSource>();
		component.clip = audioClip;
		component.volume = volume;
		component.pitch = pitch;
		component.outputAudioMixerGroup = mixerGroup;
		component.priority = priority;
		component.Play();
		base.StartCoroutine(this.DestroyAudioSource(gameObject, audioClip.length));
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x0000A1AE File Offset: 0x000083AE
	private IEnumerator DestroyAudioSource(GameObject gameObject, float clipLength)
	{
		yield return new WaitForSeconds(clipLength);
		global::UnityEngine.Object.Destroy(gameObject);
		yield break;
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x0000A1C4 File Offset: 0x000083C4
	public void PlayVoice(Vector3 position, float pitchMultiplier = 1f, float varianceMultiplier = 1f)
	{
		this.PlayVoice(this.voiceProfile, position, pitchMultiplier, varianceMultiplier);
	}

	// Token: 0x06000A9A RID: 2714 RVA: 0x0003D8F8 File Offset: 0x0003BAF8
	public void PlayVoice(VoiceProfile voiceProfile, Vector3 position, float pitchMultiplier = 1f, float varianceMultiplier = 1f)
	{
		if (Time.time < this.allowNextVoiceTime)
		{
			return;
		}
		AudioClip audioClip = voiceProfile.voiceClips.RandomValue<AudioClip>();
		float num = voiceProfile.pitch * pitchMultiplier;
		float num2 = voiceProfile.variance * varianceMultiplier;
		num *= global::UnityEngine.Random.Range(1f - num2, 1f + num2);
		AudioSource component = global::UnityEngine.Object.Instantiate<GameObject>(this.voicePrefab, position, Quaternion.identity, base.transform).GetComponent<AudioSource>();
		component.clip = audioClip;
		component.pitch = num;
		component.Play();
		this.allowNextVoiceTime = Time.time + 0.4f * audioClip.length;
		base.StartCoroutine(this.DestroyAudioSource(component.gameObject, audioClip.length));
	}

	// Token: 0x06000A9B RID: 2715 RVA: 0x0000A1D5 File Offset: 0x000083D5
	public void PlayQuestSting(int index)
	{
		this.Play(this.questStings[index], 0.35f, 1f);
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x0000A1EF File Offset: 0x000083EF
	public void PlayQuestEndSting()
	{
		if (Time.time - this.lastQuestEndStingTime < 0.5f)
		{
			return;
		}
		this.lastQuestEndStingTime = Time.time;
		this.Play(this.questEndSting, 0.4f, 1f);
	}

	public static PlayAudio p;

	public GameObject prefab;

	public AudioMixerGroup defaultGroup;

	public VoiceProfile voiceProfile;

	public GameObject voicePrefab;

	public AudioMixerGroup footstepMixer;

	public AudioClip[] questStings;

	public AudioClip questEndSting;

	private float allowNextVoiceTime = -1f;

	private float voiceCutoff = 0.1f;

	private float lastQuestEndStingTime = -10f;
}
