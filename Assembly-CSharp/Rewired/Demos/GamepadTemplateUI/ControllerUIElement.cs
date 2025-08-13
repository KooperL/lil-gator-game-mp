using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.Demos.GamepadTemplateUI
{
	// Token: 0x020004B4 RID: 1204
	[RequireComponent(typeof(Image))]
	public class ControllerUIElement : MonoBehaviour
	{
		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001DE4 RID: 7652 RVA: 0x00016E5C File Offset: 0x0001505C
		private bool hasEffects
		{
			get
			{
				return this._positiveUIEffect != null || this._negativeUIEffect != null;
			}
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x00016E7A File Offset: 0x0001507A
		private void Awake()
		{
			this._image = base.GetComponent<Image>();
			this._origColor = this._image.color;
			this._color = this._origColor;
			this.ClearLabels();
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x00074DD8 File Offset: 0x00072FD8
		public void Activate(float amount)
		{
			amount = Mathf.Clamp(amount, -1f, 1f);
			if (this.hasEffects)
			{
				if (amount < 0f && this._negativeUIEffect != null)
				{
					this._negativeUIEffect.Activate(Mathf.Abs(amount));
				}
				if (amount > 0f && this._positiveUIEffect != null)
				{
					this._positiveUIEffect.Activate(Mathf.Abs(amount));
				}
			}
			else
			{
				if (this._isActive && amount == this._highlightAmount)
				{
					return;
				}
				this._highlightAmount = amount;
				this._color = Color.Lerp(this._origColor, this._highlightColor, this._highlightAmount);
			}
			this._isActive = true;
			this.RedrawImage();
			if (this._childElements.Length != 0)
			{
				for (int i = 0; i < this._childElements.Length; i++)
				{
					if (!(this._childElements[i] == null))
					{
						this._childElements[i].Activate(amount);
					}
				}
			}
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x00074ECC File Offset: 0x000730CC
		public void Deactivate()
		{
			if (!this._isActive)
			{
				return;
			}
			this._color = this._origColor;
			this._highlightAmount = 0f;
			if (this._positiveUIEffect != null)
			{
				this._positiveUIEffect.Deactivate();
			}
			if (this._negativeUIEffect != null)
			{
				this._negativeUIEffect.Deactivate();
			}
			this._isActive = false;
			this.RedrawImage();
			if (this._childElements.Length != 0)
			{
				for (int i = 0; i < this._childElements.Length; i++)
				{
					if (!(this._childElements[i] == null))
					{
						this._childElements[i].Deactivate();
					}
				}
			}
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x00074F74 File Offset: 0x00073174
		public void SetLabel(string text, AxisRange labelType)
		{
			Text text2;
			switch (labelType)
			{
			case 0:
				text2 = this._label;
				break;
			case 1:
				text2 = this._positiveLabel;
				break;
			case 2:
				text2 = this._negativeLabel;
				break;
			default:
				text2 = null;
				break;
			}
			if (text2 != null)
			{
				text2.text = text;
			}
			if (this._childElements.Length != 0)
			{
				for (int i = 0; i < this._childElements.Length; i++)
				{
					if (!(this._childElements[i] == null))
					{
						this._childElements[i].SetLabel(text, labelType);
					}
				}
			}
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x00075000 File Offset: 0x00073200
		public void ClearLabels()
		{
			if (this._label != null)
			{
				this._label.text = string.Empty;
			}
			if (this._positiveLabel != null)
			{
				this._positiveLabel.text = string.Empty;
			}
			if (this._negativeLabel != null)
			{
				this._negativeLabel.text = string.Empty;
			}
			if (this._childElements.Length != 0)
			{
				for (int i = 0; i < this._childElements.Length; i++)
				{
					if (!(this._childElements[i] == null))
					{
						this._childElements[i].ClearLabels();
					}
				}
			}
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x00016EAB File Offset: 0x000150AB
		private void RedrawImage()
		{
			this._image.color = this._color;
		}

		// Token: 0x04001F2C RID: 7980
		[SerializeField]
		private Color _highlightColor = Color.white;

		// Token: 0x04001F2D RID: 7981
		[SerializeField]
		private ControllerUIEffect _positiveUIEffect;

		// Token: 0x04001F2E RID: 7982
		[SerializeField]
		private ControllerUIEffect _negativeUIEffect;

		// Token: 0x04001F2F RID: 7983
		[SerializeField]
		private Text _label;

		// Token: 0x04001F30 RID: 7984
		[SerializeField]
		private Text _positiveLabel;

		// Token: 0x04001F31 RID: 7985
		[SerializeField]
		private Text _negativeLabel;

		// Token: 0x04001F32 RID: 7986
		[SerializeField]
		private ControllerUIElement[] _childElements = new ControllerUIElement[0];

		// Token: 0x04001F33 RID: 7987
		private Image _image;

		// Token: 0x04001F34 RID: 7988
		private Color _color;

		// Token: 0x04001F35 RID: 7989
		private Color _origColor;

		// Token: 0x04001F36 RID: 7990
		private bool _isActive;

		// Token: 0x04001F37 RID: 7991
		private float _highlightAmount;
	}
}
