using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000388 RID: 904
[AddComponentMenu("Layout - Use Other Transforms")]
public class LayoutUse : UIBehaviour, ILayoutSelfController, ILayoutController
{
	// Token: 0x0600113D RID: 4413 RVA: 0x0000EC6A File Offset: 0x0000CE6A
	public void SetLayoutHorizontal()
	{
		if (this.useHorizontal == null)
		{
			return;
		}
		this.UpdateRectTransform();
	}

	// Token: 0x0600113E RID: 4414 RVA: 0x0000EC81 File Offset: 0x0000CE81
	public void SetLayoutVertical()
	{
		if (this.useVertical == null)
		{
			return;
		}
		this.UpdateRectTransform();
	}

	// Token: 0x0600113F RID: 4415 RVA: 0x0000EC98 File Offset: 0x0000CE98
	protected override void OnRectTransformDimensionsChange()
	{
		this.UpdateRectTransform();
	}

	// Token: 0x06001140 RID: 4416 RVA: 0x00056988 File Offset: 0x00054B88
	private void UpdateRectTransform()
	{
		if (this.rectTransform == null)
		{
			this.rectTransform = base.GetComponent<RectTransform>();
		}
		if (this.useVertical != null)
		{
			this.rectTransform.SetSizeWithCurrentAnchors(1, this.useVertical.rect.height);
		}
		if (this.useHorizontal != null)
		{
			this.rectTransform.SetSizeWithCurrentAnchors(0, this.useHorizontal.rect.height);
		}
	}

	// Token: 0x0400162C RID: 5676
	private RectTransform rectTransform;

	// Token: 0x0400162D RID: 5677
	public RectTransform useHorizontal;

	// Token: 0x0400162E RID: 5678
	public RectTransform useVertical;
}
