using System;
using UnityEngine;

// Token: 0x020003A4 RID: 932
public class UIFillBar : MonoBehaviour
{
	// Token: 0x060011AE RID: 4526 RVA: 0x0000F1A9 File Offset: 0x0000D3A9
	private void Awake()
	{
		this.rectTransform = base.transform as RectTransform;
		this.SetFillPercentage(0f);
	}

	// Token: 0x060011AF RID: 4527 RVA: 0x00058340 File Offset: 0x00056540
	public void SetFillPercentage(float fillPercentage)
	{
		this.fillTransform.SetInsetAndSizeFromParentEdge(1, this.rectTransform.rect.width * (1f - fillPercentage), this.rectTransform.rect.width * fillPercentage);
	}

	// Token: 0x040016D2 RID: 5842
	private RectTransform rectTransform;

	// Token: 0x040016D3 RID: 5843
	public RectTransform fillTransform;
}
