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
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x00014D30 File Offset: 0x00012F30
		// (set) Token: 0x06001AEC RID: 6892 RVA: 0x00014D38 File Offset: 0x00012F38
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

		// (get) Token: 0x06001AED RID: 6893 RVA: 0x00014D41 File Offset: 0x00012F41
		// (set) Token: 0x06001AEE RID: 6894 RVA: 0x00014D49 File Offset: 0x00012F49
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

		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x00014D52 File Offset: 0x00012F52
		// (set) Token: 0x06001AF0 RID: 6896 RVA: 0x00014D5A File Offset: 0x00012F5A
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

		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x00014D63 File Offset: 0x00012F63
		// (set) Token: 0x06001AF2 RID: 6898 RVA: 0x00014D6B File Offset: 0x00012F6B
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

		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x00014D74 File Offset: 0x00012F74
		// (set) Token: 0x06001AF4 RID: 6900 RVA: 0x00014D7C File Offset: 0x00012F7C
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

		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x00014D85 File Offset: 0x00012F85
		// (set) Token: 0x06001AF6 RID: 6902 RVA: 0x00014D8D File Offset: 0x00012F8D
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

		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x00014D96 File Offset: 0x00012F96
		// (set) Token: 0x06001AF8 RID: 6904 RVA: 0x00014D9E File Offset: 0x00012F9E
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

		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x00014AD8 File Offset: 0x00012CD8
		private bool isDisabled
		{
			get
			{
				return !this.IsInteractable();
			}
		}

		// (add) Token: 0x06001AFA RID: 6906 RVA: 0x0006EA74 File Offset: 0x0006CC74
		// (remove) Token: 0x06001AFB RID: 6907 RVA: 0x0006EAAC File Offset: 0x0006CCAC
		private event UnityAction _CancelEvent;

		// (add) Token: 0x06001AFC RID: 6908 RVA: 0x00014DA7 File Offset: 0x00012FA7
		// (remove) Token: 0x06001AFD RID: 6909 RVA: 0x00014DB0 File Offset: 0x00012FB0
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

		// Token: 0x06001AFE RID: 6910 RVA: 0x0006EAE4 File Offset: 0x0006CCE4
		public override Selectable FindSelectableOnLeft()
		{
			if ((base.navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None || this._autoNavLeft)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.left);
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x0006EB24 File Offset: 0x0006CD24
		public override Selectable FindSelectableOnRight()
		{
			if ((base.navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None || this._autoNavRight)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.right);
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x0006EB64 File Offset: 0x0006CD64
		public override Selectable FindSelectableOnUp()
		{
			if ((base.navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None || this._autoNavUp)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.up);
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x0006EBA4 File Offset: 0x0006CDA4
		public override Selectable FindSelectableOnDown()
		{
			if ((base.navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None || this._autoNavDown)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.down);
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x00014DB9 File Offset: 0x00012FB9
		protected override void OnCanvasGroupChanged()
		{
			base.OnCanvasGroupChanged();
			if (EventSystem.current == null)
			{
				return;
			}
			this.EvaluateHightlightDisabled(EventSystem.current.currentSelectedGameObject == base.gameObject);
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0006EBE4 File Offset: 0x0006CDE4
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

		// Token: 0x06001B04 RID: 6916 RVA: 0x0006E5D0 File Offset: 0x0006C7D0
		private void StartColorTween(Color targetColor, bool instant)
		{
			if (base.targetGraphic == null)
			{
				return;
			}
			base.targetGraphic.CrossFadeColor(targetColor, instant ? 0f : base.colors.fadeDuration, true, true);
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x00014B26 File Offset: 0x00012D26
		private void DoSpriteSwap(Sprite newSprite)
		{
			if (base.image == null)
			{
				return;
			}
			base.image.overrideSprite = newSprite;
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x0006EC6C File Offset: 0x0006CE6C
		private void TriggerAnimation(string triggername)
		{
			if (base.animator == null || !base.animator.enabled || !base.animator.isActiveAndEnabled || base.animator.runtimeAnimatorController == null || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			base.animator.ResetTrigger(this._disabledHighlightedTrigger);
			base.animator.SetTrigger(triggername);
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x00014DEA File Offset: 0x00012FEA
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.EvaluateHightlightDisabled(true);
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x00014DFA File Offset: 0x00012FFA
		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			this.EvaluateHightlightDisabled(false);
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0006ECDC File Offset: 0x0006CEDC
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

		// Token: 0x06001B0A RID: 6922 RVA: 0x00014E0A File Offset: 0x0001300A
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
