using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x020001AB RID: 427
public class PlayAudio : MonoBehaviour
{
	// Token: 0x060008C8 RID: 2248 RVA: 0x00029723 File Offset: 0x00027923
	private void OnEnable()
	{
		PlayAudio.p = this;
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x0002972B File Offset: 0x0002792B
	public void Play(AudioClip audioClip, float volume = 1f, float pitch = 1f)
	{
		this.PlayAtPoint(audioClip, Player.Position, volume, pitch, 128);
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x00029740 File Offset: 0x00027940
	public void PlayAtPoint(SoundEffect soundEffect, Vector3 point)
	{
		if (soundEffect == null || soundEffect.audioClip == null)
		{
			return;
		}
		this.PlayAtPoint(soundEffect.audioClip, point, soundEffect.Volume, soundEffect.Pitch, 128);
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x00029772 File Offset: 0x00027972
	public void PlayAtPoint(AudioClip audioClip, Vector3 point, float volume = 1f, float pitch = 1f, int priority = 128)
	{
		this.PlayAtPoint(audioClip, point, this.defaultGroup, volume, pitch, priority);
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x00029788 File Offset: 0x00027988
	public void PlayAtPoint(AudioClip audioClip, Vector3 point, AudioMixerGroup mixerGroup, float volume = 1f, float pitch = 1f, int priority = 128)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.prefab);
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

	// Token: 0x060008CD RID: 2253 RVA: 0x000297F2 File Offset: 0x000279F2
	private IEnumerator DestroyAudioSource(GameObject gameObject, float clipLength)
	{
		yield return new WaitForSeconds(clipLength);
		Object.Destroy(gameObject);
		yield break;
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x00029808 File Offset: 0x00027A08
	public void PlayVoice(Vector3 position, float pitchMultiplier = 1f, float varianceMultiplier = 1f)
	{
		this.PlayVoice(this.voiceProfile, position, pitchMultiplier, varianceMultiplier);
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x0002981C File Offset: 0x00027A1C
	public void PlayVoice(VoiceProfile voiceProfile, Vector3 position, float pitchMultiplier = 1f, float varianceMultiplier = 1f)
	{
		if (Time.time < this.allowNextVoiceTime)
		{
			return;
		}
		AudioClip audioClip = voiceProfile.voiceClips.RandomValue<AudioClip>();
		float num = voiceProfile.pitch * pitchMultiplier;
		float num2 = voiceProfile.variance * varianceMultiplier;
		num *= Random.Range(1f - num2, 1f + num2);
		AudioSource component = Object.Instantiate<GameObject>(this.voicePrefab, position, Quaternion.identity, base.transform).GetComponent<AudioSource>();
		component.clip = audioClip;
		component.pitch = num;
		component.Play();
		this.allowNextVoiceTime = Time.time + 0.4f * audioClip.length;
		base.StartCoroutine(this.DestroyAudioSource(component.gameObject, audioClip.length));
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x000298CE File Offset: 0x00027ACE
	public void PlayQuestSting(int index)
	{
		this.Play(this.questStings[index], 0.35f, 1f);
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x000298E8 File Offset: 0x00027AE8
	public void PlayQuestEndSting()
	{
		if (Time.time - this.lastQuestEndStingTime < 0.5f)
		{
			return;
		}
		this.lastQuestEndStingTime = Time.time;
		this.Play(this.questEndSting, 0.4f, 1f);
	}

	// Token: 0x04000ADB RID: 2779
	public static PlayAudio p;

	// Token: 0x04000ADC RID: 2780
	public GameObject prefab;

	// Token: 0x04000ADD RID: 2781
	public AudioMixerGroup defaultGroup;

	// Token: 0x04000ADE RID: 2782
	public VoiceProfile voiceProfile;

	// Token: 0x04000ADF RID: 2783
	public GameObject voicePrefab;

	// Token: 0x04000AE0 RID: 2784
	public AudioMixerGroup footstepMixer;

	// Token: 0x04000AE1 RID: 2785
	public AudioClip[] questStings;

	// Token: 0x04000AE2 RID: 2786
	public AudioClip questEndSting;

	// Token: 0x04000AE3 RID: 2787
	private float allowNextVoiceTime = -1f;

	// Token: 0x04000AE4 RID: 2788
	private float voiceCutoff = 0.1f;

	// Token: 0x04000AE5 RID: 2789
	private float lastQuestEndStingTime = -10f;
}
