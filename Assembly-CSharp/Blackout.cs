using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Blackout : MonoBehaviour
{
	// Token: 0x06001174 RID: 4468 RVA: 0x00002229 File Offset: 0x00000429
	private static void MarkDialogueDepth()
	{
	}

	// Token: 0x06001175 RID: 4469 RVA: 0x00002229 File Offset: 0x00000429
	private static void ClearDialogueDepth()
	{
	}

	// Token: 0x06001176 RID: 4470 RVA: 0x0000EEE2 File Offset: 0x0000D0E2
	public static Coroutine FadeInAndOut()
	{
		Blackout.MarkDialogueDepth();
		Blackout.b.gameObject.SetActive(true);
		Blackout.b.fadeIn = true;
		return CoroutineUtil.Start(Blackout.b.WaitForFadeOut());
	}

	// Token: 0x06001177 RID: 4471 RVA: 0x0000EF13 File Offset: 0x0000D113
	public static Coroutine FadeIn()
	{
		Blackout.MarkDialogueDepth();
		Blackout.b.gameObject.SetActive(true);
		return CoroutineUtil.Start(Blackout.b.WaitForFadeIn());
	}

	// Token: 0x06001178 RID: 4472 RVA: 0x0000EF39 File Offset: 0x0000D139
	public static Coroutine FadeOut()
	{
		if (!Blackout.b.gameObject.activeSelf)
		{
			return null;
		}
		Blackout.MarkDialogueDepth();
		return CoroutineUtil.Start(Blackout.b.WaitForFadeOut());
	}

	// Token: 0x06001179 RID: 4473 RVA: 0x0005820C File Offset: 0x0005640C
	public static void Set(float alpha)
	{
		Color color = Blackout.b.image.color;
		color.a = alpha;
		Blackout.b.image.color = color;
		Blackout.b.gameObject.SetActive(alpha > 0f);
	}

	// Token: 0x0600117A RID: 4474 RVA: 0x00058258 File Offset: 0x00056458
	private void Awake()
	{
		this.fadeSpeed = 1f / this.fadeTime;
		this.image = base.GetComponent<RawImage>();
		Blackout.b = this;
		if (this.fadeOutAtStart)
		{
			this.alpha = 1f;
			this.fadeOut = true;
			this.UpdateAlpha();
		}
	}

	// Token: 0x0600117B RID: 4475 RVA: 0x0000EF62 File Offset: 0x0000D162
	private void OnEnable()
	{
		Blackout.b = this;
	}

	// Token: 0x0600117C RID: 4476 RVA: 0x0000EF6A File Offset: 0x0000D16A
	private void OnDisable()
	{
		Blackout.ClearDialogueDepth();
	}

	// Token: 0x0600117D RID: 4477 RVA: 0x0000EF6A File Offset: 0x0000D16A
	private void OnDestroy()
	{
		Blackout.ClearDialogueDepth();
	}

	// Token: 0x0600117E RID: 4478 RVA: 0x000582AC File Offset: 0x000564AC
	private void Update()
	{
		if (this.fadeIn)
		{
			this.alpha = Mathf.MoveTowards(this.alpha, 1f, this.fadeSpeed * Time.deltaTime);
			this.UpdateAlpha();
			if (this.alpha == 1f)
			{
				this.fadeIn = false;
				TriggerGC.TriggerGarbageCollection(true, true);
				return;
			}
		}
		else if (this.fadeOut)
		{
			this.alpha = Mathf.MoveTowards(this.alpha, 0f, this.fadeSpeed * Time.deltaTime);
			this.UpdateAlpha();
			if (this.alpha == 0f)
			{
				this.fadeOut = false;
				base.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x0600117F RID: 4479 RVA: 0x00058358 File Offset: 0x00056558
	private void UpdateAlpha()
	{
		Color color = this.image.color;
		color.a = Mathf.Clamp01(this.alpha);
		this.image.color = color;
	}

	// Token: 0x06001180 RID: 4480 RVA: 0x0000EF71 File Offset: 0x0000D171
	public IEnumerator WaitForFadeIn()
	{
		this.fadeIn = true;
		while (this.fadeIn)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001181 RID: 4481 RVA: 0x0000EF80 File Offset: 0x0000D180
	public IEnumerator WaitForFadeOut()
	{
		this.fadeOut = true;
		while (this.fadeOut)
		{
			yield return null;
		}
		yield break;
	}

	private static bool hasDialogueDepth;

	public static Blackout b;

	internal RawImage image;

	public float fadeTime = 0.5f;

	private float fadeSpeed;

	public bool fadeOutAtStart = true;

	private float alpha;

	private bool fadeIn;

	private bool fadeOut;
}
