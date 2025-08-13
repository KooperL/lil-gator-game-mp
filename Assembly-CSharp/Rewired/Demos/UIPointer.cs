using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.Demos
{
	// Token: 0x020004A0 RID: 1184
	[AddComponentMenu("")]
	[RequireComponent(typeof(RectTransform))]
	public sealed class UIPointer : UIBehaviour
	{
		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x000167F5 File Offset: 0x000149F5
		// (set) Token: 0x06001D73 RID: 7539 RVA: 0x000167FD File Offset: 0x000149FD
		public bool autoSort
		{
			get
			{
				return this._autoSort;
			}
			set
			{
				if (value == this._autoSort)
				{
					return;
				}
				this._autoSort = value;
				if (value)
				{
					base.transform.SetAsLastSibling();
				}
			}
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x00073798 File Offset: 0x00071998
		protected override void Awake()
		{
			base.Awake();
			Graphic[] componentsInChildren = base.GetComponentsInChildren<Graphic>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].raycastTarget = false;
			}
			if (this._hideHardwarePointer)
			{
				Cursor.visible = false;
			}
			if (this._autoSort)
			{
				base.transform.SetAsLastSibling();
			}
			this.GetDependencies();
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x0001681E File Offset: 0x00014A1E
		private void Update()
		{
			if (this._autoSort && base.transform.GetSiblingIndex() < base.transform.parent.childCount - 1)
			{
				base.transform.SetAsLastSibling();
			}
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x00016852 File Offset: 0x00014A52
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this.GetDependencies();
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x00016860 File Offset: 0x00014A60
		protected override void OnCanvasGroupChanged()
		{
			base.OnCanvasGroupChanged();
			this.GetDependencies();
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x000737F0 File Offset: 0x000719F0
		public void OnScreenPositionChanged(Vector2 screenPosition)
		{
			if (this._canvas == null)
			{
				return;
			}
			Camera camera = null;
			RenderMode renderMode = this._canvas.renderMode;
			if (renderMode != null && renderMode - 1 <= 1)
			{
				camera = this._canvas.worldCamera;
			}
			Vector2 vector;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(base.transform.parent as RectTransform, screenPosition, camera, ref vector);
			base.transform.localPosition = new Vector3(vector.x, vector.y, base.transform.localPosition.z);
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x0001686E File Offset: 0x00014A6E
		private void GetDependencies()
		{
			this._canvas = base.transform.root.GetComponentInChildren<Canvas>();
		}

		// Token: 0x04001ECD RID: 7885
		[Tooltip("Should the hardware pointer be hidden?")]
		[SerializeField]
		private bool _hideHardwarePointer = true;

		// Token: 0x04001ECE RID: 7886
		[Tooltip("Sets the pointer to the last sibling in the parent hierarchy. Do not enable this on multiple UIPointers under the same parent transform or they will constantly fight each other for dominance.")]
		[SerializeField]
		private bool _autoSort = true;

		// Token: 0x04001ECF RID: 7887
		private Canvas _canvas;
	}
}
