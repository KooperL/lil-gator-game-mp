using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("Layout - Use Other Transforms")]
public class LayoutUse : UIBehaviour, ILayoutSelfController, ILayoutController
{
	// Token: 0x06000E6D RID: 3693 RVA: 0x00045087 File Offset: 0x00043287
	public void SetLayoutHorizontal()
	{
		if (this.useHorizontal == null)
		{
			return;
		}
		this.UpdateRectTransform();
	}

	// Token: 0x06000E6E RID: 3694 RVA: 0x0004509E File Offset: 0x0004329E
	public void SetLayoutVertical()
	{
		if (this.useVertical == null)
		{
			return;
		}
		this.UpdateRectTransform();
	}

	// Token: 0x06000E6F RID: 3695 RVA: 0x000450B5 File Offset: 0x000432B5
	protected override void OnRectTransformDimensionsChange()
	{
		this.UpdateRectTransform();
	}

	// Token: 0x06000E70 RID: 3696 RVA: 0x000450C0 File Offset: 0x000432C0
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
