using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x0200023B RID: 571
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

	// Token: 0x0400106C RID: 4204
	private static SmoothPostProcessing[] smoothLayers;

	// Token: 0x0400106D RID: 4205
	public SmoothPostProcessing.PostProcessLayer thisLayer;

	// Token: 0x0400106E RID: 4206
	private float fade;

	// Token: 0x0400106F RID: 4207
	private int depth;

	// Token: 0x04001070 RID: 4208
	private float speed;

	// Token: 0x04001071 RID: 4209
	private PostProcessVolume volume;

	// Token: 0x02000421 RID: 1057
	public enum PostProcessLayer
	{
		// Token: 0x04001D47 RID: 7495
		Memory,
		// Token: 0x04001D48 RID: 7496
		LENGTH
	}
}
