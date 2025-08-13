using System;
using UnityEngine;

// Token: 0x0200010F RID: 271
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

	// Token: 0x040007A2 RID: 1954
	public SmoothPostProcessing.PostProcessLayer layer;

	// Token: 0x040007A3 RID: 1955
	public float fadeInSpeed = 1f;

	// Token: 0x040007A4 RID: 1956
	public float fadeOutSpeed = 1f;
}
