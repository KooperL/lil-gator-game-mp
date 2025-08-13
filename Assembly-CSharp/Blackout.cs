using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200037F RID: 895
public class Blackout : MonoBehaviour
{
	// Token: 0x06001114 RID: 4372 RVA: 0x00002229 File Offset: 0x00000429
	private static void MarkDialogueDepth()
	{
	}

	// Token: 0x06001115 RID: 4373 RVA: 0x00002229 File Offset: 0x00000429
	private static void ClearDialogueDepth()
	{
	}

	// Token: 0x06001116 RID: 4374 RVA: 0x0000EB0E File Offset: 0x0000CD0E
	public static Coroutine FadeInAndOut()
	{
		Blackout.MarkDialogueDepth();
		Blackout.b.gameObject.SetActive(true);
		Blackout.b.fadeIn = true;
		return CoroutineUtil.Start(Blackout.b.WaitForFadeOut());
	}

	// Token: 0x06001117 RID: 4375 RVA: 0x0000EB3F File Offset: 0x0000CD3F
	public static Coroutine FadeIn()
	{
		Blackout.MarkDialogueDepth();
		Blackout.b.gameObject.SetActive(true);
		return CoroutineUtil.Start(Blackout.b.WaitForFadeIn());
	}

	// Token: 0x06001118 RID: 4376 RVA: 0x0000EB65 File Offset: 0x0000CD65
	public static Coroutine FadeOut()
	{
		if (!Blackout.b.gameObject.activeSelf)
		{
			return null;
		}
		Blackout.MarkDialogueDepth();
		return CoroutineUtil.Start(Blackout.b.WaitForFadeOut());
	}

	// Token: 0x06001119 RID: 4377 RVA: 0x000563E0 File Offset: 0x000545E0
	public static void Set(float alpha)
	{
		Color color = Blackout.b.image.color;
		color.a = alpha;
		Blackout.b.image.color = color;
		Blackout.b.gameObject.SetActive(alpha > 0f);
	}

	// Token: 0x0600111A RID: 4378 RVA: 0x0005642C File Offset: 0x0005462C
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

	// Token: 0x0600111B RID: 4379 RVA: 0x0000EB8E File Offset: 0x0000CD8E
	private void OnEnable()
	{
		Blackout.b = this;
	}

	// Token: 0x0600111C RID: 4380 RVA: 0x0000EB96 File Offset: 0x0000CD96
	private void OnDisable()
	{
		Blackout.ClearDialogueDepth();
	}

	// Token: 0x0600111D RID: 4381 RVA: 0x0000EB96 File Offset: 0x0000CD96
	private void OnDestroy()
	{
		Blackout.ClearDialogueDepth();
	}

	// Token: 0x0600111E RID: 4382 RVA: 0x00056480 File Offset: 0x00054680
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

	// Token: 0x0600111F RID: 4383 RVA: 0x0005652C File Offset: 0x0005472C
	private void UpdateAlpha()
	{
		Color color = this.image.color;
		color.a = Mathf.Clamp01(this.alpha);
		this.image.color = color;
	}

	// Token: 0x06001120 RID: 4384 RVA: 0x0000EB9D File Offset: 0x0000CD9D
	public IEnumerator WaitForFadeIn()
	{
		this.fadeIn = true;
		while (this.fadeIn)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001121 RID: 4385 RVA: 0x0000EBAC File Offset: 0x0000CDAC
	public IEnumerator WaitForFadeOut()
	{
		this.fadeOut = true;
		while (this.fadeOut)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x0400160B RID: 5643
	private static bool hasDialogueDepth;

	// Token: 0x0400160C RID: 5644
	public static Blackout b;

	// Token: 0x0400160D RID: 5645
	internal RawImage image;

	// Token: 0x0400160E RID: 5646
	public float fadeTime = 0.5f;

	// Token: 0x0400160F RID: 5647
	private float fadeSpeed;

	// Token: 0x04001610 RID: 5648
	public bool fadeOutAtStart = true;

	// Token: 0x04001611 RID: 5649
	private float alpha;

	// Token: 0x04001612 RID: 5650
	private bool fadeIn;

	// Token: 0x04001613 RID: 5651
	private bool fadeOut;
}
