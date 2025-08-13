using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.Demos.GamepadTemplateUI
{
	// Token: 0x020004B3 RID: 1203
	[RequireComponent(typeof(Image))]
	public class ControllerUIEffect : MonoBehaviour
	{
		// Token: 0x06001DDF RID: 7647 RVA: 0x00016DCB File Offset: 0x00014FCB
		private void Awake()
		{
			this._image = base.GetComponent<Image>();
			this._origColor = this._image.color;
			this._color = this._origColor;
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x00074D80 File Offset: 0x00072F80
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

		// Token: 0x06001DE1 RID: 7649 RVA: 0x00016DF6 File Offset: 0x00014FF6
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

		// Token: 0x06001DE2 RID: 7650 RVA: 0x00016E25 File Offset: 0x00015025
		private void RedrawImage()
		{
			this._image.color = this._color;
			this._image.enabled = this._isActive;
		}

		// Token: 0x04001F26 RID: 7974
		[SerializeField]
		private Color _highlightColor = Color.white;

		// Token: 0x04001F27 RID: 7975
		private Image _image;

		// Token: 0x04001F28 RID: 7976
		private Color _color;

		// Token: 0x04001F29 RID: 7977
		private Color _origColor;

		// Token: 0x04001F2A RID: 7978
		private bool _isActive;

		// Token: 0x04001F2B RID: 7979
		private float _highlightAmount;
	}
}
