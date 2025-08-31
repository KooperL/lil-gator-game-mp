using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class FadeGameVolume : MonoBehaviour
{
	// (get) Token: 0x0600057A RID: 1402 RVA: 0x0001CF0F File Offset: 0x0001B10F
	public static bool IsFaded
	{
		get
		{
			return FadeGameVolume.volumeMultiplier < 1f;
		}
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x0001CF20 File Offset: 0x0001B120
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

	// Token: 0x0600057C RID: 1404 RVA: 0x0001CF84 File Offset: 0x0001B184
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

	// Token: 0x0600057D RID: 1405 RVA: 0x0001CFE5 File Offset: 0x0001B1E5
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

	// Token: 0x0600057E RID: 1406 RVA: 0x0001CFF4 File Offset: 0x0001B1F4
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
