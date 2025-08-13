using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x020002FE RID: 766
public class SmoothPostProcessing : MonoBehaviour
{
	// Token: 0x06000F2B RID: 3883 RVA: 0x0004FE78 File Offset: 0x0004E078
	public static void FadeOutLayer(SmoothPostProcessing.PostProcessLayer layer, float speed)
	{
		SmoothPostProcessing smoothPostProcessing = SmoothPostProcessing.smoothLayers[(int)layer];
		if (smoothPostProcessing != null)
		{
			smoothPostProcessing.FadeOut(speed);
		}
	}

	// Token: 0x06000F2C RID: 3884 RVA: 0x0004FEA0 File Offset: 0x0004E0A0
	public static void FadeInLayer(SmoothPostProcessing.PostProcessLayer layer, float speed)
	{
		SmoothPostProcessing smoothPostProcessing = SmoothPostProcessing.smoothLayers[(int)layer];
		if (smoothPostProcessing != null)
		{
			smoothPostProcessing.FadeIn(speed);
		}
	}

	// Token: 0x06000F2D RID: 3885 RVA: 0x0004FEC8 File Offset: 0x0004E0C8
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

	// Token: 0x06000F2E RID: 3886 RVA: 0x0000D314 File Offset: 0x0000B514
	protected void FadeOut(float speed)
	{
		this.depth--;
		if ((float)this.depth == 0f)
		{
			this.speed = speed;
		}
		base.enabled = true;
	}

	// Token: 0x06000F2F RID: 3887 RVA: 0x0000D340 File Offset: 0x0000B540
	protected void FadeIn(float speed)
	{
		this.depth++;
		this.speed = speed;
		base.gameObject.SetActive(true);
		base.enabled = true;
	}

	// Token: 0x06000F30 RID: 3888 RVA: 0x0004FF24 File Offset: 0x0004E124
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

	// Token: 0x0400137F RID: 4991
	private static SmoothPostProcessing[] smoothLayers;

	// Token: 0x04001380 RID: 4992
	public SmoothPostProcessing.PostProcessLayer thisLayer;

	// Token: 0x04001381 RID: 4993
	private float fade;

	// Token: 0x04001382 RID: 4994
	private int depth;

	// Token: 0x04001383 RID: 4995
	private float speed;

	// Token: 0x04001384 RID: 4996
	private PostProcessVolume volume;

	// Token: 0x020002FF RID: 767
	public enum PostProcessLayer
	{
		// Token: 0x04001386 RID: 4998
		Memory,
		// Token: 0x04001387 RID: 4999
		LENGTH
	}
}
