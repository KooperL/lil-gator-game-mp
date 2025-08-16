using System;
using UnityEngine;

public class UIFillBar : MonoBehaviour
{
	// Token: 0x0600120E RID: 4622 RVA: 0x0000F57D File Offset: 0x0000D77D
	private void Awake()
	{
		this.rectTransform = base.transform as RectTransform;
		this.SetFillPercentage(0f);
	}

	// Token: 0x0600120F RID: 4623 RVA: 0x0005A170 File Offset: 0x00058370
	public void SetFillPercentage(float fillPercentage)
	{
		this.fillTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, this.rectTransform.rect.width * (1f - fillPercentage), this.rectTransform.rect.width * fillPercentage);
	}

	private RectTransform rectTransform;

	public RectTransform fillTransform;
}
