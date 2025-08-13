using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002A4 RID: 676
public class Blackout : MonoBehaviour
{
	// Token: 0x06000E50 RID: 3664 RVA: 0x00044A33 File Offset: 0x00042C33
	private static void MarkDialogueDepth()
	{
	}

	// Token: 0x06000E51 RID: 3665 RVA: 0x00044A35 File Offset: 0x00042C35
	private static void ClearDialogueDepth()
	{
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x00044A37 File Offset: 0x00042C37
	public static Coroutine FadeInAndOut()
	{
		Blackout.MarkDialogueDepth();
		Blackout.b.gameObject.SetActive(true);
		Blackout.b.fadeIn = true;
		return CoroutineUtil.Start(Blackout.b.WaitForFadeOut());
	}

	// Token: 0x06000E53 RID: 3667 RVA: 0x00044A68 File Offset: 0x00042C68
	public static Coroutine FadeIn()
	{
		Blackout.MarkDialogueDepth();
		Blackout.b.gameObject.SetActive(true);
		return CoroutineUtil.Start(Blackout.b.WaitForFadeIn());
	}

	// Token: 0x06000E54 RID: 3668 RVA: 0x00044A8E File Offset: 0x00042C8E
	public static Coroutine FadeOut()
	{
		if (!Blackout.b.gameObject.activeSelf)
		{
			return null;
		}
		Blackout.MarkDialogueDepth();
		return CoroutineUtil.Start(Blackout.b.WaitForFadeOut());
	}

	// Token: 0x06000E55 RID: 3669 RVA: 0x00044AB8 File Offset: 0x00042CB8
	public static void Set(float alpha)
	{
		Color color = Blackout.b.image.color;
		color.a = alpha;
		Blackout.b.image.color = color;
		Blackout.b.gameObject.SetActive(alpha > 0f);
	}

	// Token: 0x06000E56 RID: 3670 RVA: 0x00044B04 File Offset: 0x00042D04
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

	// Token: 0x06000E57 RID: 3671 RVA: 0x00044B55 File Offset: 0x00042D55
	private void OnEnable()
	{
		Blackout.b = this;
	}

	// Token: 0x06000E58 RID: 3672 RVA: 0x00044B5D File Offset: 0x00042D5D
	private void OnDisable()
	{
		Blackout.ClearDialogueDepth();
	}

	// Token: 0x06000E59 RID: 3673 RVA: 0x00044B64 File Offset: 0x00042D64
	private void OnDestroy()
	{
		Blackout.ClearDialogueDepth();
	}

	// Token: 0x06000E5A RID: 3674 RVA: 0x00044B6C File Offset: 0x00042D6C
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

	// Token: 0x06000E5B RID: 3675 RVA: 0x00044C18 File Offset: 0x00042E18
	private void UpdateAlpha()
	{
		Color color = this.image.color;
		color.a = Mathf.Clamp01(this.alpha);
		this.image.color = color;
	}

	// Token: 0x06000E5C RID: 3676 RVA: 0x00044C4F File Offset: 0x00042E4F
	public IEnumerator WaitForFadeIn()
	{
		this.fadeIn = true;
		while (this.fadeIn)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000E5D RID: 3677 RVA: 0x00044C5E File Offset: 0x00042E5E
	public IEnumerator WaitForFadeOut()
	{
		this.fadeOut = true;
		while (this.fadeOut)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x040012A9 RID: 4777
	private static bool hasDialogueDepth;

	// Token: 0x040012AA RID: 4778
	public static Blackout b;

	// Token: 0x040012AB RID: 4779
	internal RawImage image;

	// Token: 0x040012AC RID: 4780
	public float fadeTime = 0.5f;

	// Token: 0x040012AD RID: 4781
	private float fadeSpeed;

	// Token: 0x040012AE RID: 4782
	public bool fadeOutAtStart = true;

	// Token: 0x040012AF RID: 4783
	private float alpha;

	// Token: 0x040012B0 RID: 4784
	private bool fadeIn;

	// Token: 0x040012B1 RID: 4785
	private bool fadeOut;
}
