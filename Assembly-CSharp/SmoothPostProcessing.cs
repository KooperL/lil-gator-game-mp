using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SmoothPostProcessing : MonoBehaviour
{
	// Token: 0x06000F87 RID: 3975 RVA: 0x00051C08 File Offset: 0x0004FE08
	public static void FadeOutLayer(SmoothPostProcessing.PostProcessLayer layer, float speed)
	{
		SmoothPostProcessing smoothPostProcessing = SmoothPostProcessing.smoothLayers[(int)layer];
		if (smoothPostProcessing != null)
		{
			smoothPostProcessing.FadeOut(speed);
		}
	}

	// Token: 0x06000F88 RID: 3976 RVA: 0x00051C30 File Offset: 0x0004FE30
	public static void FadeInLayer(SmoothPostProcessing.PostProcessLayer layer, float speed)
	{
		SmoothPostProcessing smoothPostProcessing = SmoothPostProcessing.smoothLayers[(int)layer];
		if (smoothPostProcessing != null)
		{
			smoothPostProcessing.FadeIn(speed);
		}
	}

	// Token: 0x06000F89 RID: 3977 RVA: 0x00051C58 File Offset: 0x0004FE58
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

	// Token: 0x06000F8A RID: 3978 RVA: 0x0000D6A7 File Offset: 0x0000B8A7
	protected void FadeOut(float speed)
	{
		this.depth--;
		if ((float)this.depth == 0f)
		{
			this.speed = speed;
		}
		base.enabled = true;
	}

	// Token: 0x06000F8B RID: 3979 RVA: 0x0000D6D3 File Offset: 0x0000B8D3
	protected void FadeIn(float speed)
	{
		this.depth++;
		this.speed = speed;
		base.gameObject.SetActive(true);
		base.enabled = true;
	}

	// Token: 0x06000F8C RID: 3980 RVA: 0x00051CB4 File Offset: 0x0004FEB4
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
