using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("Layout - Use Other Transforms")]
public class LayoutUse : UIBehaviour, ILayoutSelfController, ILayoutController
{
	// Token: 0x0600119E RID: 4510 RVA: 0x0000F05D File Offset: 0x0000D25D
	public void SetLayoutHorizontal()
	{
		if (this.useHorizontal == null)
		{
			return;
		}
		this.UpdateRectTransform();
	}

	// Token: 0x0600119F RID: 4511 RVA: 0x0000F074 File Offset: 0x0000D274
	public void SetLayoutVertical()
	{
		if (this.useVertical == null)
		{
			return;
		}
		this.UpdateRectTransform();
	}

	// Token: 0x060011A0 RID: 4512 RVA: 0x0000F08B File Offset: 0x0000D28B
	protected override void OnRectTransformDimensionsChange()
	{
		this.UpdateRectTransform();
	}

	// Token: 0x060011A1 RID: 4513 RVA: 0x00058C10 File Offset: 0x00056E10
	private void UpdateRectTransform()
	{
		if (this.rectTransform == null)
		{
			this.rectTransform = base.GetComponent<RectTransform>();
		}
		if (this.useVertical != null)
		{
			this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.useVertical.rect.height);
		}
		if (this.useHorizontal != null)
		{
			this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.useHorizontal.rect.height);
		}
	}

	private RectTransform rectTransform;

	public RectTransform useHorizontal;

	public RectTransform useVertical;
}
