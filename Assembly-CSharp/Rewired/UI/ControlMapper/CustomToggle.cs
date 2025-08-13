using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000458 RID: 1112
	[AddComponentMenu("")]
	public class CustomToggle : Toggle, ICustomSelectable, ICancelHandler, IEventSystemHandler
	{
		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x00014929 File Offset: 0x00012B29
		// (set) Token: 0x06001A8C RID: 6796 RVA: 0x00014931 File Offset: 0x00012B31
		public Sprite disabledHighlightedSprite
		{
			get
			{
				return this._disabledHighlightedSprite;
			}
			set
			{
				this._disabledHighlightedSprite = value;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x0001493A File Offset: 0x00012B3A
		// (set) Token: 0x06001A8E RID: 6798 RVA: 0x00014942 File Offset: 0x00012B42
		public Color disabledHighlightedColor
		{
			get
			{
				return this._disabledHighlightedColor;
			}
			set
			{
				this._disabledHighlightedColor = value;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x0001494B File Offset: 0x00012B4B
		// (set) Token: 0x06001A90 RID: 6800 RVA: 0x00014953 File Offset: 0x00012B53
		public string disabledHighlightedTrigger
		{
			get
			{
				return this._disabledHighlightedTrigger;
			}
			set
			{
				this._disabledHighlightedTrigger = value;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x0001495C File Offset: 0x00012B5C
		// (set) Token: 0x06001A92 RID: 6802 RVA: 0x00014964 File Offset: 0x00012B64
		public bool autoNavUp
		{
			get
			{
				return this._autoNavUp;
			}
			set
			{
				this._autoNavUp = value;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x0001496D File Offset: 0x00012B6D
		// (set) Token: 0x06001A94 RID: 6804 RVA: 0x00014975 File Offset: 0x00012B75
		public bool autoNavDown
		{
			get
			{
				return this._autoNavDown;
			}
			set
			{
				this._autoNavDown = value;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001A95 RID: 6805 RVA: 0x0001497E File Offset: 0x00012B7E
		// (set) Token: 0x06001A96 RID: 6806 RVA: 0x00014986 File Offset: 0x00012B86
		public bool autoNavLeft
		{
			get
			{
				return this._autoNavLeft;
			}
			set
			{
				this._autoNavLeft = value;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x0001498F File Offset: 0x00012B8F
		// (set) Token: 0x06001A98 RID: 6808 RVA: 0x00014997 File Offset: 0x00012B97
		public bool autoNavRight
		{
			get
			{
				return this._autoNavRight;
			}
			set
			{
				this._autoNavRight = value;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x000146D1 File Offset: 0x000128D1
		private bool isDisabled
		{
			get
			{
				return !this.IsInteractable();
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06001A9A RID: 6810 RVA: 0x0006CA70 File Offset: 0x0006AC70
		// (remove) Token: 0x06001A9B RID: 6811 RVA: 0x0006CAA8 File Offset: 0x0006ACA8
		private event UnityAction _CancelEvent;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06001A9C RID: 6812 RVA: 0x000149A0 File Offset: 0x00012BA0
		// (remove) Token: 0x06001A9D RID: 6813 RVA: 0x000149A9 File Offset: 0x00012BA9
		public event UnityAction CancelEvent
		{
			add
			{
				this._CancelEvent += value;
			}
			remove
			{
				this._CancelEvent -= value;
			}
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x0006CAE0 File Offset: 0x0006ACE0
		public override Selectable FindSelectableOnLeft()
		{
			if ((base.navigation.mode & 1) != null || this._autoNavLeft)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.left);
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x0006CB20 File Offset: 0x0006AD20
		public override Selectable FindSelectableOnRight()
		{
			if ((base.navigation.mode & 1) != null || this._autoNavRight)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.right);
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x0006CB60 File Offset: 0x0006AD60
		public override Selectable FindSelectableOnUp()
		{
			if ((base.navigation.mode & 2) != null || this._autoNavUp)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.up);
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x0006CBA0 File Offset: 0x0006ADA0
		public override Selectable FindSelectableOnDown()
		{
			if ((base.navigation.mode & 2) != null || this._autoNavDown)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.down);
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x000149B2 File Offset: 0x00012BB2
		protected override void OnCanvasGroupChanged()
		{
			base.OnCanvasGroupChanged();
			if (EventSystem.current == null)
			{
				return;
			}
			this.EvaluateHightlightDisabled(EventSystem.current.currentSelectedGameObject == base.gameObject);
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x0006CBE0 File Offset: 0x0006ADE0
		protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
		{
			if (this.isHighlightDisabled)
			{
				Color disabledHighlightedColor = this._disabledHighlightedColor;
				Sprite disabledHighlightedSprite = this._disabledHighlightedSprite;
				string disabledHighlightedTrigger = this._disabledHighlightedTrigger;
				if (base.gameObject.activeInHierarchy)
				{
					switch (base.transition)
					{
					case 1:
						this.StartColorTween(disabledHighlightedColor * base.colors.colorMultiplier, instant);
						return;
					case 2:
						this.DoSpriteSwap(disabledHighlightedSprite);
						return;
					case 3:
						this.TriggerAnimation(disabledHighlightedTrigger);
						return;
					default:
						return;
					}
				}
			}
			else
			{
				base.DoStateTransition(state, instant);
			}
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x0006C5CC File Offset: 0x0006A7CC
		private void StartColorTween(Color targetColor, bool instant)
		{
			if (base.targetGraphic == null)
			{
				return;
			}
			base.targetGraphic.CrossFadeColor(targetColor, instant ? 0f : base.colors.fadeDuration, true, true);
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x0001471F File Offset: 0x0001291F
		private void DoSpriteSwap(Sprite newSprite)
		{
			if (base.image == null)
			{
				return;
			}
			base.image.overrideSprite = newSprite;
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x0006CC68 File Offset: 0x0006AE68
		private void TriggerAnimation(string triggername)
		{
			if (base.animator == null || !base.animator.enabled || !base.animator.isActiveAndEnabled || base.animator.runtimeAnimatorController == null || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			base.animator.ResetTrigger(this._disabledHighlightedTrigger);
			base.animator.SetTrigger(triggername);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x000149E3 File Offset: 0x00012BE3
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.EvaluateHightlightDisabled(true);
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x000149F3 File Offset: 0x00012BF3
		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			this.EvaluateHightlightDisabled(false);
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x0006CCD8 File Offset: 0x0006AED8
		private void EvaluateHightlightDisabled(bool isSelected)
		{
			if (!isSelected)
			{
				if (this.isHighlightDisabled)
				{
					this.isHighlightDisabled = false;
					Selectable.SelectionState selectionState = (this.isDisabled ? 4 : base.currentSelectionState);
					this.DoStateTransition(selectionState, false);
					return;
				}
			}
			else
			{
				if (!this.isDisabled)
				{
					return;
				}
				this.isHighlightDisabled = true;
				this.DoStateTransition(4, false);
			}
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x00014A03 File Offset: 0x00012C03
		public void OnCancel(BaseEventData eventData)
		{
			if (this._CancelEvent != null)
			{
				this._CancelEvent.Invoke();
			}
		}

		// Token: 0x04001CF9 RID: 7417
		[SerializeField]
		private Sprite _disabledHighlightedSprite;

		// Token: 0x04001CFA RID: 7418
		[SerializeField]
		private Color _disabledHighlightedColor;

		// Token: 0x04001CFB RID: 7419
		[SerializeField]
		private string _disabledHighlightedTrigger;

		// Token: 0x04001CFC RID: 7420
		[SerializeField]
		private bool _autoNavUp = true;

		// Token: 0x04001CFD RID: 7421
		[SerializeField]
		private bool _autoNavDown = true;

		// Token: 0x04001CFE RID: 7422
		[SerializeField]
		private bool _autoNavLeft = true;

		// Token: 0x04001CFF RID: 7423
		[SerializeField]
		private bool _autoNavRight = true;

		// Token: 0x04001D00 RID: 7424
		private bool isHighlightDisabled;
	}
}
