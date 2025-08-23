using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.Demos.GamepadTemplateUI
{
	[RequireComponent(typeof(Image))]
	public class ControllerUIElement : MonoBehaviour
	{
		// (get) Token: 0x06001E45 RID: 7749 RVA: 0x0001729C File Offset: 0x0001549C
		private bool hasEffects
		{
			get
			{
				return this._positiveUIEffect != null || this._negativeUIEffect != null;
			}
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x000172BA File Offset: 0x000154BA
		private void Awake()
		{
			this._image = base.GetComponent<Image>();
			this._origColor = this._image.color;
			this._color = this._origColor;
			this.ClearLabels();
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x0007704C File Offset: 0x0007524C
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

		// Token: 0x06001E48 RID: 7752 RVA: 0x00077140 File Offset: 0x00075340
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

		// Token: 0x06001E49 RID: 7753 RVA: 0x000771E8 File Offset: 0x000753E8
		public void SetLabel(string text, AxisRange labelType)
		{
			Text text2;
			switch (labelType)
			{
			case AxisRange.Full:
				text2 = this._label;
				break;
			case AxisRange.Positive:
				text2 = this._positiveLabel;
				break;
			case AxisRange.Negative:
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

		// Token: 0x06001E4A RID: 7754 RVA: 0x00077274 File Offset: 0x00075474
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

		// Token: 0x06001E4B RID: 7755 RVA: 0x000172EB File Offset: 0x000154EB
		private void RedrawImage()
		{
			this._image.color = this._color;
		}

		[SerializeField]
		private Color _highlightColor = Color.white;

		[SerializeField]
		private ControllerUIEffect _positiveUIEffect;

		[SerializeField]
		private ControllerUIEffect _negativeUIEffect;

		[SerializeField]
		private Text _label;

		[SerializeField]
		private Text _positiveLabel;

		[SerializeField]
		private Text _negativeLabel;

		[SerializeField]
		private ControllerUIElement[] _childElements = new ControllerUIElement[0];

		private Image _image;

		private Color _color;

		private Color _origColor;

		private bool _isActive;

		private float _highlightAmount;
	}
}
