using System;
using Rewired.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000468 RID: 1128
	[AddComponentMenu("")]
	[RequireComponent(typeof(Selectable))]
	public class ScrollRectSelectableChild : MonoBehaviour, ISelectHandler, IEventSystemHandler
	{
		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x00015553 File Offset: 0x00013753
		private RectTransform parentScrollRectContentTransform
		{
			get
			{
				return this.parentScrollRect.content;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x0006E004 File Offset: 0x0006C204
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

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x00015560 File Offset: 0x00013760
		private RectTransform rectTransform
		{
			get
			{
				return base.transform as RectTransform;
			}
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x0001556D File Offset: 0x0001376D
		private void Start()
		{
			this.parentScrollRect = base.transform.GetComponentInParent<ScrollRect>();
			if (this.parentScrollRect == null)
			{
				Debug.LogError("Rewired Control Mapper: No ScrollRect found! This component must be a child of a ScrollRect!");
				return;
			}
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x0006E02C File Offset: 0x0006C22C
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
			if (!MathTools.GetOffsetToContainRect(rect3, rect, ref vector))
			{
				return;
			}
			Vector2 anchoredPosition = this.parentScrollRectContentTransform.anchoredPosition;
			anchoredPosition.x = Mathf.Clamp(anchoredPosition.x + vector.x, 0f, Mathf.Abs(rect2.width - this.parentScrollRectContentTransform.sizeDelta.x));
			anchoredPosition.y = Mathf.Clamp(anchoredPosition.y + vector.y, 0f, Mathf.Abs(rect2.height - this.parentScrollRectContentTransform.sizeDelta.y));
			this.parentScrollRectContentTransform.anchoredPosition = anchoredPosition;
		}

		// Token: 0x04001DAE RID: 7598
		public bool useCustomEdgePadding;

		// Token: 0x04001DAF RID: 7599
		public float customEdgePadding = 50f;

		// Token: 0x04001DB0 RID: 7600
		private ScrollRect parentScrollRect;

		// Token: 0x04001DB1 RID: 7601
		private Selectable _selectable;
	}
}
