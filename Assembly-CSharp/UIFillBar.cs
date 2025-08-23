using System;
using UnityEngine;

public class UIFillBar : MonoBehaviour
{
	// Token: 0x0600120F RID: 4623 RVA: 0x0000F59C File Offset: 0x0000D79C
	private void Awake()
	{
		this.rectTransform = base.transform as RectTransform;
		this.SetFillPercentage(0f);
	}

	// Token: 0x06001210 RID: 4624 RVA: 0x0005A5CC File Offset: 0x000587CC
	public void SetFillPercentage(float fillPercentage)
	{
		this.fillTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, this.rectTransform.rect.width * (1f - fillPercentage), this.rectTransform.rect.width * fillPercentage);
	}

	private RectTransform rectTransform;

	public RectTransform fillTransform;
}
