using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x0200010C RID: 268
public class FadeGameVolume : MonoBehaviour
{
	// Token: 0x17000048 RID: 72
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

	// Token: 0x04000789 RID: 1929
	private static float volumeMultiplier = 1f;

	// Token: 0x0400078A RID: 1930
	private static IEnumerator coroutine;

	// Token: 0x0400078B RID: 1931
	private static AudioMixer audioMixer;

	// Token: 0x0400078C RID: 1932
	private const float speed = 2f;
}
