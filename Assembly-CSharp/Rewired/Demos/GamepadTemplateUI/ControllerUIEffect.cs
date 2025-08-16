using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.Demos.GamepadTemplateUI
{
	[RequireComponent(typeof(Image))]
	public class ControllerUIEffect : MonoBehaviour
	{
		// Token: 0x06001E3F RID: 7743 RVA: 0x000171EC File Offset: 0x000153EC
		private void Awake()
		{
			this._image = base.GetComponent<Image>();
			this._origColor = this._image.color;
			this._color = this._origColor;
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x00076B98 File Offset: 0x00074D98
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

		// Token: 0x06001E41 RID: 7745 RVA: 0x00017217 File Offset: 0x00015417
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

		// Token: 0x06001E42 RID: 7746 RVA: 0x00017246 File Offset: 0x00015446
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
