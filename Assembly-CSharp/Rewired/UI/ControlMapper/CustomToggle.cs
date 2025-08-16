using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class CustomToggle : Toggle, ICustomSelectable, ICancelHandler, IEventSystemHandler
	{
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x00014D11 File Offset: 0x00012F11
		// (set) Token: 0x06001AEC RID: 6892 RVA: 0x00014D19 File Offset: 0x00012F19
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

		// (get) Token: 0x06001AED RID: 6893 RVA: 0x00014D22 File Offset: 0x00012F22
		// (set) Token: 0x06001AEE RID: 6894 RVA: 0x00014D2A File Offset: 0x00012F2A
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

		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x00014D33 File Offset: 0x00012F33
		// (set) Token: 0x06001AF0 RID: 6896 RVA: 0x00014D3B File Offset: 0x00012F3B
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

		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x00014D44 File Offset: 0x00012F44
		// (set) Token: 0x06001AF2 RID: 6898 RVA: 0x00014D4C File Offset: 0x00012F4C
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

		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x00014D55 File Offset: 0x00012F55
		// (set) Token: 0x06001AF4 RID: 6900 RVA: 0x00014D5D File Offset: 0x00012F5D
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

		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x00014D66 File Offset: 0x00012F66
		// (set) Token: 0x06001AF6 RID: 6902 RVA: 0x00014D6E File Offset: 0x00012F6E
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

		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x00014D77 File Offset: 0x00012F77
		// (set) Token: 0x06001AF8 RID: 6904 RVA: 0x00014D7F File Offset: 0x00012F7F
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

		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x00014AB9 File Offset: 0x00012CB9
		private bool isDisabled
		{
			get
			{
				return !this.IsInteractable();
			}
		}

		// (add) Token: 0x06001AFA RID: 6906 RVA: 0x0006E904 File Offset: 0x0006CB04
		// (remove) Token: 0x06001AFB RID: 6907 RVA: 0x0006E93C File Offset: 0x0006CB3C
		private event UnityAction _CancelEvent;

		// (add) Token: 0x06001AFC RID: 6908 RVA: 0x00014D88 File Offset: 0x00012F88
		// (remove) Token: 0x06001AFD RID: 6909 RVA: 0x00014D91 File Offset: 0x00012F91
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

		// Token: 0x06001AFE RID: 6910 RVA: 0x0006E974 File Offset: 0x0006CB74
		public override Selectable FindSelectableOnLeft()
		{
			if ((base.navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None || this._autoNavLeft)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.left);
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x0006E9B4 File Offset: 0x0006CBB4
		public override Selectable FindSelectableOnRight()
		{
			if ((base.navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None || this._autoNavRight)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.right);
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x0006E9F4 File Offset: 0x0006CBF4
		public override Selectable FindSelectableOnUp()
		{
			if ((base.navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None || this._autoNavUp)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.up);
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x0006EA34 File Offset: 0x0006CC34
		public override Selectable FindSelectableOnDown()
		{
			if ((base.navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None || this._autoNavDown)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.down);
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x00014D9A File Offset: 0x00012F9A
		protected override void OnCanvasGroupChanged()
		{
			base.OnCanvasGroupChanged();
			if (EventSystem.current == null)
			{
				return;
			}
			this.EvaluateHightlightDisabled(EventSystem.current.currentSelectedGameObject == base.gameObject);
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0006EA74 File Offset: 0x0006CC74
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
					case Selectable.Transition.ColorTint:
						this.StartColorTween(disabledHighlightedColor * base.colors.colorMultiplier, instant);
						return;
					case Selectable.Transition.SpriteSwap:
						this.DoSpriteSwap(disabledHighlightedSprite);
						return;
					case Selectable.Transition.Animation:
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

		// Token: 0x06001B04 RID: 6916 RVA: 0x0006E460 File Offset: 0x0006C660
		private void StartColorTween(Color targetColor, bool instant)
		{
			if (base.targetGraphic == null)
			{
				return;
			}
			base.targetGraphic.CrossFadeColor(targetColor, instant ? 0f : base.colors.fadeDuration, true, true);
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x00014B07 File Offset: 0x00012D07
		private void DoSpriteSwap(Sprite newSprite)
		{
			if (base.image == null)
			{
				return;
			}
			base.image.overrideSprite = newSprite;
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x0006EAFC File Offset: 0x0006CCFC
		private void TriggerAnimation(string triggername)
		{
			if (base.animator == null || !base.animator.enabled || !base.animator.isActiveAndEnabled || base.animator.runtimeAnimatorController == null || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			base.animator.ResetTrigger(this._disabledHighlightedTrigger);
			base.animator.SetTrigger(triggername);
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x00014DCB File Offset: 0x00012FCB
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.EvaluateHightlightDisabled(true);
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x00014DDB File Offset: 0x00012FDB
		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			this.EvaluateHightlightDisabled(false);
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0006EB6C File Offset: 0x0006CD6C
		private void EvaluateHightlightDisabled(bool isSelected)
		{
			if (!isSelected)
			{
				if (this.isHighlightDisabled)
				{
					this.isHighlightDisabled = false;
					Selectable.SelectionState selectionState = (this.isDisabled ? Selectable.SelectionState.Disabled : base.currentSelectionState);
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
				this.DoStateTransition(Selectable.SelectionState.Disabled, false);
			}
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x00014DEB File Offset: 0x00012FEB
		public void OnCancel(BaseEventData eventData)
		{
			if (this._CancelEvent != null)
			{
				this._CancelEvent();
			}
		}

		[SerializeField]
		private Sprite _disabledHighlightedSprite;

		[SerializeField]
		private Color _disabledHighlightedColor;

		[SerializeField]
		private string _disabledHighlightedTrigger;

		[SerializeField]
		private bool _autoNavUp = true;

		[SerializeField]
		private bool _autoNavDown = true;

		[SerializeField]
		private bool _autoNavLeft = true;

		[SerializeField]
		private bool _autoNavRight = true;

		private bool isHighlightDisabled;
	}
}
