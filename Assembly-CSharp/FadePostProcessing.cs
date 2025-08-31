using System;
using UnityEngine;

public class FadePostProcessing : MonoBehaviour
{
	// Token: 0x06000591 RID: 1425 RVA: 0x0001D4C5 File Offset: 0x0001B6C5
	public void OnEnable()
	{
		SmoothPostProcessing.FadeInLayer(this.layer, this.fadeInSpeed);
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x0001D4D8 File Offset: 0x0001B6D8
	public void OnDisable()
	{
		SmoothPostProcessing.FadeOutLayer(this.layer, this.fadeOutSpeed);
	}

	public SmoothPostProcessing.PostProcessLayer layer;

	public float fadeInSpeed = 1f;

	public float fadeOutSpeed = 1f;
}
