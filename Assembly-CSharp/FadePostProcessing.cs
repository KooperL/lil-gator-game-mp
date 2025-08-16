using System;
using UnityEngine;

public class FadePostProcessing : MonoBehaviour
{
	// Token: 0x060006EF RID: 1775 RVA: 0x0000703A File Offset: 0x0000523A
	public void OnEnable()
	{
		SmoothPostProcessing.FadeInLayer(this.layer, this.fadeInSpeed);
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x0000704D File Offset: 0x0000524D
	public void OnDisable()
	{
		SmoothPostProcessing.FadeOutLayer(this.layer, this.fadeOutSpeed);
	}

	public SmoothPostProcessing.PostProcessLayer layer;

	public float fadeInSpeed = 1f;

	public float fadeOutSpeed = 1f;
}
