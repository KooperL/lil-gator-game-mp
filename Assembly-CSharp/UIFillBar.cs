using System;
using UnityEngine;

// Token: 0x020002C2 RID: 706
public class UIFillBar : MonoBehaviour
{
	// Token: 0x06000ED6 RID: 3798 RVA: 0x00046EF2 File Offset: 0x000450F2
	private void Awake()
	{
		this.rectTransform = base.transform as RectTransform;
		this.SetFillPercentage(0f);
	}

	// Token: 0x06000ED7 RID: 3799 RVA: 0x00046F10 File Offset: 0x00045110
	public void SetFillPercentage(float fillPercentage)
	{
		this.fillTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, this.rectTransform.rect.width * (1f - fillPercentage), this.rectTransform.rect.width * fillPercentage);
	}

	// Token: 0x0400135C RID: 4956
	private RectTransform rectTransform;

	// Token: 0x0400135D RID: 4957
	public RectTransform fillTransform;
}
