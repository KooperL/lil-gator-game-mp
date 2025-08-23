using System;
using Rewired.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	[RequireComponent(typeof(Selectable))]
	public class ScrollRectSelectableChild : MonoBehaviour, ISelectHandler, IEventSystemHandler
	{
		// (get) Token: 0x06001C22 RID: 7202 RVA: 0x00015993 File Offset: 0x00013B93
		private RectTransform parentScrollRectContentTransform
		{
			get
			{
				return this.parentScrollRect.content;
			}
		}

		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x00070278 File Offset: 0x0006E478
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

		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x000159A0 File Offset: 0x00013BA0
		private RectTransform rectTransform
		{
			get
			{
				return base.transform as RectTransform;
			}
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x000159AD File Offset: 0x00013BAD
		private void Start()
		{
			this.parentScrollRect = base.transform.GetComponentInParent<ScrollRect>();
			if (this.parentScrollRect == null)
			{
				Debug.LogError("Rewired Control Mapper: No ScrollRect found! This component must be a child of a ScrollRect!");
				return;
			}
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x000702A0 File Offset: 0x0006E4A0
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

		public bool useCustomEdgePadding;

		public float customEdgePadding = 50f;

		private ScrollRect parentScrollRect;

		private Selectable _selectable;
	}
}
