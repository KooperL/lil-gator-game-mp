using System;
using UnityEngine;

public class UIFillBar : MonoBehaviour
{
	// Token: 0x0600120E RID: 4622 RVA: 0x0000F592 File Offset: 0x0000D792
	private void Awake()
	{
		this.rectTransform = base.transform as RectTransform;
		this.SetFillPercentage(0f);
	}

	// Token: 0x0600120F RID: 4623 RVA: 0x0005A304 File Offset: 0x00058504
	public void SetFillPercentage(float fillPercentage)
	{
		this.fillTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, this.rectTransform.rect.width * (1f - fillPercentage), this.rectTransform.rect.width * fillPercentage);
	}

	private RectTransform rectTransform;

	public RectTransform fillTransform;
}
