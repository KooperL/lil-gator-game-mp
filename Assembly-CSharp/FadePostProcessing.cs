using System;
using UnityEngine;

// Token: 0x02000166 RID: 358
public class FadePostProcessing : MonoBehaviour
{
	// Token: 0x060006B5 RID: 1717 RVA: 0x00006D74 File Offset: 0x00004F74
	public void OnEnable()
	{
		SmoothPostProcessing.FadeInLayer(this.layer, this.fadeInSpeed);
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x00006D87 File Offset: 0x00004F87
	public void OnDisable()
	{
		SmoothPostProcessing.FadeOutLayer(this.layer, this.fadeOutSpeed);
	}

	// Token: 0x040008FC RID: 2300
	public SmoothPostProcessing.PostProcessLayer layer;

	// Token: 0x040008FD RID: 2301
	public float fadeInSpeed = 1f;

	// Token: 0x040008FE RID: 2302
	public float fadeOutSpeed = 1f;
}
