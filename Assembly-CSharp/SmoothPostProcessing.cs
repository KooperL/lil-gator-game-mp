using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SmoothPostProcessing : MonoBehaviour
{
	// Token: 0x06000C7F RID: 3199 RVA: 0x0003CD68 File Offset: 0x0003AF68
	public static void FadeOutLayer(SmoothPostProcessing.PostProcessLayer layer, float speed)
	{
		SmoothPostProcessing smoothPostProcessing = SmoothPostProcessing.smoothLayers[(int)layer];
		if (smoothPostProcessing != null)
		{
			smoothPostProcessing.FadeOut(speed);
		}
	}

	// Token: 0x06000C80 RID: 3200 RVA: 0x0003CD90 File Offset: 0x0003AF90
	public static void FadeInLayer(SmoothPostProcessing.PostProcessLayer layer, float speed)
	{
		SmoothPostProcessing smoothPostProcessing = SmoothPostProcessing.smoothLayers[(int)layer];
		if (smoothPostProcessing != null)
		{
			smoothPostProcessing.FadeIn(speed);
		}
	}

	// Token: 0x06000C81 RID: 3201 RVA: 0x0003CDB8 File Offset: 0x0003AFB8
	private void Awake()
	{
		this.volume = base.GetComponent<PostProcessVolume>();
		this.volume.weight = 0f;
		if (SmoothPostProcessing.smoothLayers == null)
		{
			SmoothPostProcessing.smoothLayers = new SmoothPostProcessing[1];
		}
		SmoothPostProcessing.smoothLayers[(int)this.thisLayer] = this;
		base.enabled = false;
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x0003CE13 File Offset: 0x0003B013
	protected void FadeOut(float speed)
	{
		this.depth--;
		if ((float)this.depth == 0f)
		{
			this.speed = speed;
		}
		base.enabled = true;
	}

	// Token: 0x06000C83 RID: 3203 RVA: 0x0003CE3F File Offset: 0x0003B03F
	protected void FadeIn(float speed)
	{
		this.depth++;
		this.speed = speed;
		base.gameObject.SetActive(true);
		base.enabled = true;
	}

	// Token: 0x06000C84 RID: 3204 RVA: 0x0003CE6C File Offset: 0x0003B06C
	private void Update()
	{
		float num = ((this.depth > 0) ? 1f : 0f);
		this.fade = Mathf.MoveTowards(this.fade, num, Time.deltaTime * this.speed);
		this.volume.weight = this.fade;
		if (this.fade == num)
		{
			if (this.depth == 0)
			{
				base.gameObject.SetActive(false);
			}
			base.enabled = false;
		}
	}

	private static SmoothPostProcessing[] smoothLayers;

	public SmoothPostProcessing.PostProcessLayer thisLayer;

	private float fade;

	private int depth;

	private float speed;

	private PostProcessVolume volume;

	public enum PostProcessLayer
	{
		Memory,
		LENGTH
	}
}
