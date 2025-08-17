using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class FadeGameVolume : MonoBehaviour
{
	// (get) Token: 0x060006D2 RID: 1746 RVA: 0x00006F46 File Offset: 0x00005146
	public static bool IsFaded
	{
		get
		{
			return FadeGameVolume.volumeMultiplier < 1f;
		}
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x00032BAC File Offset: 0x00030DAC
	public static void FadeOutGameVolume()
	{
		if (FadeGameVolume.audioMixer == null && Settings.s != null)
		{
			FadeGameVolume.audioMixer = Settings.s.volumeMixer;
		}
		if (FadeGameVolume.coroutine != null)
		{
			CoroutineUtil.Stop(FadeGameVolume.coroutine);
		}
		FadeGameVolume.coroutine = FadeGameVolume.DoFadeGameVolume(0f);
		CoroutineUtil.Start(FadeGameVolume.coroutine);
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x00032C10 File Offset: 0x00030E10
	public static void FadeInGameVolume()
	{
		if (FadeGameVolume.audioMixer == null && Settings.s != null)
		{
			FadeGameVolume.audioMixer = Settings.s.volumeMixer;
		}
		if (FadeGameVolume.coroutine != null)
		{
			CoroutineUtil.Stop(FadeGameVolume.coroutine);
		}
		FadeGameVolume.coroutine = FadeGameVolume.DoFadeGameVolume(1f);
		CoroutineUtil.Start(FadeGameVolume.coroutine);
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x00006F54 File Offset: 0x00005154
	private static IEnumerator DoFadeGameVolume(float target)
	{
		while (FadeGameVolume.volumeMultiplier != target)
		{
			FadeGameVolume.volumeMultiplier = Mathf.MoveTowards(FadeGameVolume.volumeMultiplier, target, Time.unscaledDeltaTime * 2f);
			FadeGameVolume.UpdateGameVolume();
			yield return null;
		}
		FadeGameVolume.coroutine = null;
		yield break;
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x00032C74 File Offset: 0x00030E74
	public static void UpdateGameVolume()
	{
		float num = Mathf.Lerp(Settings.gameVolume, -80f, 1f - FadeGameVolume.volumeMultiplier);
		FadeGameVolume.audioMixer.SetFloat("GameVolume", num);
	}

	private static float volumeMultiplier = 1f;

	private static IEnumerator coroutine;

	private static AudioMixer audioMixer;

	private const float speed = 2f;
}
