using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x02000162 RID: 354
public class FadeGameVolume : MonoBehaviour
{
	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x06000698 RID: 1688 RVA: 0x00006C80 File Offset: 0x00004E80
	public static bool IsFaded
	{
		get
		{
			return FadeGameVolume.volumeMultiplier < 1f;
		}
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x000314B0 File Offset: 0x0002F6B0
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

	// Token: 0x0600069A RID: 1690 RVA: 0x00031514 File Offset: 0x0002F714
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

	// Token: 0x0600069B RID: 1691 RVA: 0x00006C8E File Offset: 0x00004E8E
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

	// Token: 0x0600069C RID: 1692 RVA: 0x00031578 File Offset: 0x0002F778
	public static void UpdateGameVolume()
	{
		float num = Mathf.Lerp(Settings.gameVolume, -80f, 1f - FadeGameVolume.volumeMultiplier);
		FadeGameVolume.audioMixer.SetFloat("GameVolume", num);
	}

	// Token: 0x040008E0 RID: 2272
	private static float volumeMultiplier = 1f;

	// Token: 0x040008E1 RID: 2273
	private static IEnumerator coroutine;

	// Token: 0x040008E2 RID: 2274
	private static AudioMixer audioMixer;

	// Token: 0x040008E3 RID: 2275
	private const float speed = 2f;
}
