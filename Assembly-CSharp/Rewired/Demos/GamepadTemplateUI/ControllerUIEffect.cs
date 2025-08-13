using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.Demos.GamepadTemplateUI
{
	// Token: 0x0200034B RID: 843
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

		// Token: 0x04001990 RID: 6544
		[SerializeField]
		private Color _highlightColor = Color.white;

		// Token: 0x04001991 RID: 6545
		private Image _image;

		// Token: 0x04001992 RID: 6546
		private Color _color;

		// Token: 0x04001993 RID: 6547
		private Color _origColor;

		// Token: 0x04001994 RID: 6548
		private bool _isActive;

		// Token: 0x04001995 RID: 6549
		private float _highlightAmount;
	}
}
