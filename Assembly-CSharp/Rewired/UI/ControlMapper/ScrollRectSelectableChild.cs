using System;
using Rewired.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000328 RID: 808
	[AddComponentMenu("")]
	[RequireComponent(typeof(Selectable))]
	public class ScrollRectSelectableChild : MonoBehaviour, ISelectHandler, IEventSystemHandler
	{
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x0005E656 File Offset: 0x0005C856
		private RectTransform parentScrollRectContentTransform
		{
			get
			{
				return this.parentScrollRect.content;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x0005E664 File Offset: 0x0005C864
		private Selectable selectable
		{
			get
			{
				Selectable selectable;
				if ((selectable = this._selectable) == null)
				{
					selectable = (this._selectable = base.GetComponent<Selectable>());
				}
				return selectable;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x0005E68A File Offset: 0x0005C88A
		private RectTransform rectTransform
		{
			get
			{
				return base.transform as RectTransform;
			}
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x0005E697 File Offset: 0x0005C897
		private void Start()
		{
			this.parentScrollRect = base.transform.GetComponentInParent<ScrollRect>();
			if (this.parentScrollRect == null)
			{
				Debug.LogError("Rewired Control Mapper: No ScrollRect found! This component must be a child of a ScrollRect!");
				return;
			}
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x0005E6C4 File Offset: 0x0005C8C4
		public void OnSelect(BaseEventData eventData)
		{
			if (this.parentScrollRect == null)
			{
				return;
			}
			if (!(eventData is AxisEventData))
			{
				return;
			}
			RectTransform rectTransform = this.parentScrollRect.transform as RectTransform;
			Rect rect = MathTools.TransformRect(this.rectTransform.rect, this.rectTransform, rectTransform);
			Rect rect2 = rectTransform.rect;
			Rect rect3 = rectTransform.rect;
			float height;
			if (this.useCustomEdgePadding)
			{
				height = this.customEdgePadding;
			}
			else
			{
				height = rect.height;
			}
			rect3.yMax -= height;
			rect3.yMin += height;
			if (MathTools.RectContains(rect3, rect))
			{
				return;
			}
			Vector2 vector;
			if (!MathTools.GetOffsetToContainRect(rect3, rect, out vector))
			{
				return;
			}
			Vector2 anchoredPosition = this.parentScrollRectContentTransform.anchoredPosition;
			anchoredPosition.x = Mathf.Clamp(anchoredPosition.x + vector.x, 0f, Mathf.Abs(rect2.width - this.parentScrollRectContentTransform.sizeDelta.x));
			anchoredPosition.y = Mathf.Clamp(anchoredPosition.y + vector.y, 0f, Mathf.Abs(rect2.height - this.parentScrollRectContentTransform.sizeDelta.y));
			this.parentScrollRectContentTransform.anchoredPosition = anchoredPosition;
		}

		// Token: 0x040018BC RID: 6332
		public bool useCustomEdgePadding;

		// Token: 0x040018BD RID: 6333
		public float customEdgePadding = 50f;

		// Token: 0x040018BE RID: 6334
		private ScrollRect parentScrollRect;

		// Token: 0x040018BF RID: 6335
		private Selectable _selectable;
	}
}
