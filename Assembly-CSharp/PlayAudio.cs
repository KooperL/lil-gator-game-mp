using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x02000225 RID: 549
public class PlayAudio : MonoBehaviour
{
	// Token: 0x06000A49 RID: 2633 RVA: 0x00009E2B File Offset: 0x0000802B
	private void OnEnable()
	{
		PlayAudio.p = this;
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x00009E33 File Offset: 0x00008033
	public void Play(AudioClip audioClip, float volume = 1f, float pitch = 1f)
	{
		this.PlayAtPoint(audioClip, Player.Position, volume, pitch, 128);
	}

	// Token: 0x06000A4B RID: 2635 RVA: 0x00009E48 File Offset: 0x00008048
	public void PlayAtPoint(SoundEffect soundEffect, Vector3 point)
	{
		if (soundEffect == null || soundEffect.audioClip == null)
		{
			return;
		}
		this.PlayAtPoint(soundEffect.audioClip, point, soundEffect.Volume, soundEffect.Pitch, 128);
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x00009E7A File Offset: 0x0000807A
	public void PlayAtPoint(AudioClip audioClip, Vector3 point, float volume = 1f, float pitch = 1f, int priority = 128)
	{
		this.PlayAtPoint(audioClip, point, this.defaultGroup, volume, pitch, priority);
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x0003BF74 File Offset: 0x0003A174
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

	// Token: 0x06000A4E RID: 2638 RVA: 0x00009E8F File Offset: 0x0000808F
	private IEnumerator DestroyAudioSource(GameObject gameObject, float clipLength)
	{
		yield return new WaitForSeconds(clipLength);
		Object.Destroy(gameObject);
		yield break;
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x00009EA5 File Offset: 0x000080A5
	public void PlayVoice(Vector3 position, float pitchMultiplier = 1f, float varianceMultiplier = 1f)
	{
		this.PlayVoice(this.voiceProfile, position, pitchMultiplier, varianceMultiplier);
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x0003BFE0 File Offset: 0x0003A1E0
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

	// Token: 0x06000A51 RID: 2641 RVA: 0x00009EB6 File Offset: 0x000080B6
	public void PlayQuestSting(int index)
	{
		this.Play(this.questStings[index], 0.35f, 1f);
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x00009ED0 File Offset: 0x000080D0
	public void PlayQuestEndSting()
	{
		if (Time.time - this.lastQuestEndStingTime < 0.5f)
		{
			return;
		}
		this.lastQuestEndStingTime = Time.time;
		this.Play(this.questEndSting, 0.4f, 1f);
	}

	// Token: 0x04000CDB RID: 3291
	public static PlayAudio p;

	// Token: 0x04000CDC RID: 3292
	public GameObject prefab;

	// Token: 0x04000CDD RID: 3293
	public AudioMixerGroup defaultGroup;

	// Token: 0x04000CDE RID: 3294
	public VoiceProfile voiceProfile;

	// Token: 0x04000CDF RID: 3295
	public GameObject voicePrefab;

	// Token: 0x04000CE0 RID: 3296
	public AudioMixerGroup footstepMixer;

	// Token: 0x04000CE1 RID: 3297
	public AudioClip[] questStings;

	// Token: 0x04000CE2 RID: 3298
	public AudioClip questEndSting;

	// Token: 0x04000CE3 RID: 3299
	private float allowNextVoiceTime = -1f;

	// Token: 0x04000CE4 RID: 3300
	private float voiceCutoff = 0.1f;

	// Token: 0x04000CE5 RID: 3301
	private float lastQuestEndStingTime = -10f;
}
