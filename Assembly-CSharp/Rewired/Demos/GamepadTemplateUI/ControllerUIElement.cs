using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.Demos.GamepadTemplateUI
{
	// Token: 0x0200034C RID: 844
	[RequireComponent(typeof(Image))]
	public class ControllerUIElement : MonoBehaviour
	{
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x00065C9A File Offset: 0x00063E9A
		private bool hasEffects
		{
			get
			{
				return this._positiveUIEffect != null || this._negativeUIEffect != null;
			}
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00065CB8 File Offset: 0x00063EB8
		private void Awake()
		{
			this._image = base.GetComponent<Image>();
			this._origColor = this._image.color;
			this._color = this._origColor;
			this.ClearLabels();
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00065CEC File Offset: 0x00063EEC
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

		// Token: 0x060017E3 RID: 6115 RVA: 0x00065DE0 File Offset: 0x00063FE0
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

		// Token: 0x060017E4 RID: 6116 RVA: 0x00065E88 File Offset: 0x00064088
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

		// Token: 0x060017E5 RID: 6117 RVA: 0x00065F14 File Offset: 0x00064114
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

		// Token: 0x060017E6 RID: 6118 RVA: 0x00065FB4 File Offset: 0x000641B4
		private void RedrawImage()
		{
			this._image.color = this._color;
		}

		// Token: 0x04001996 RID: 6550
		[SerializeField]
		private Color _highlightColor = Color.white;

		// Token: 0x04001997 RID: 6551
		[SerializeField]
		private ControllerUIEffect _positiveUIEffect;

		// Token: 0x04001998 RID: 6552
		[SerializeField]
		private ControllerUIEffect _negativeUIEffect;

		// Token: 0x04001999 RID: 6553
		[SerializeField]
		private Text _label;

		// Token: 0x0400199A RID: 6554
		[SerializeField]
		private Text _positiveLabel;

		// Token: 0x0400199B RID: 6555
		[SerializeField]
		private Text _negativeLabel;

		// Token: 0x0400199C RID: 6556
		[SerializeField]
		private ControllerUIElement[] _childElements = new ControllerUIElement[0];

		// Token: 0x0400199D RID: 6557
		private Image _image;

		// Token: 0x0400199E RID: 6558
		private Color _color;

		// Token: 0x0400199F RID: 6559
		private Color _origColor;

		// Token: 0x040019A0 RID: 6560
		private bool _isActive;

		// Token: 0x040019A1 RID: 6561
		private float _highlightAmount;
	}
}
