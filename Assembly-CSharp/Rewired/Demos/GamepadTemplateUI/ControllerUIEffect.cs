using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.Demos.GamepadTemplateUI
{
	[RequireComponent(typeof(Image))]
	public class ControllerUIEffect : MonoBehaviour
	{
		// Token: 0x060017DB RID: 6107 RVA: 0x00065BB1 File Offset: 0x00063DB1
		private void Awake()
		{
			this._image = base.GetComponent<Image>();
			this._origColor = this._image.color;
			this._color = this._origColor;
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00065BDC File Offset: 0x00063DDC
		public void Activate(float amount)
		{
			amount = Mathf.Clamp01(amount);
			if (this._isActive && amount == this._highlightAmount)
			{
				return;
			}
			this._highlightAmount = amount;
			this._color = Color.Lerp(this._origColor, this._highlightColor, this._highlightAmount);
			this._isActive = true;
			this.RedrawImage();
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00065C34 File Offset: 0x00063E34
		public void Deactivate()
		{
			if (!this._isActive)
			{
				return;
			}
			this._color = this._origColor;
			this._highlightAmount = 0f;
			this._isActive = false;
			this.RedrawImage();
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00065C63 File Offset: 0x00063E63
		private void RedrawImage()
		{
			this._image.color = this._color;
			this._image.enabled = this._isActive;
		}

		[SerializeField]
		private Color _highlightColor = Color.white;

		private Image _image;

		private Color _color;

		private Color _origColor;

		private bool _isActive;

		private float _highlightAmount;
	}
}
