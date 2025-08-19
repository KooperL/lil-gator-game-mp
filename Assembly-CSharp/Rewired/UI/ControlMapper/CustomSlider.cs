using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class CustomSlider : Slider, ICustomSelectable, ICancelHandler, IEventSystemHandler
	{
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x00014C1D File Offset: 0x00012E1D
		// (set) Token: 0x06001ACB RID: 6859 RVA: 0x00014C25 File Offset: 0x00012E25
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

		// (get) Token: 0x06001ACC RID: 6860 RVA: 0x00014C2E File Offset: 0x00012E2E
		// (set) Token: 0x06001ACD RID: 6861 RVA: 0x00014C36 File Offset: 0x00012E36
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

		// (get) Token: 0x06001ACE RID: 6862 RVA: 0x00014C3F File Offset: 0x00012E3F
		// (set) Token: 0x06001ACF RID: 6863 RVA: 0x00014C47 File Offset: 0x00012E47
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

		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x00014C50 File Offset: 0x00012E50
		// (set) Token: 0x06001AD1 RID: 6865 RVA: 0x00014C58 File Offset: 0x00012E58
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

		// (get) Token: 0x06001AD2 RID: 6866 RVA: 0x00014C61 File Offset: 0x00012E61
		// (set) Token: 0x06001AD3 RID: 6867 RVA: 0x00014C69 File Offset: 0x00012E69
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

		// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x00014C72 File Offset: 0x00012E72
		// (set) Token: 0x06001AD5 RID: 6869 RVA: 0x00014C7A File Offset: 0x00012E7A
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

		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x00014C83 File Offset: 0x00012E83
		// (set) Token: 0x06001AD7 RID: 6871 RVA: 0x00014C8B File Offset: 0x00012E8B
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

		// (get) Token: 0x06001AD8 RID: 6872 RVA: 0x00014AD8 File Offset: 0x00012CD8
		private bool isDisabled
		{
			get
			{
				return !this.IsInteractable();
			}
		}

		// (add) Token: 0x06001AD9 RID: 6873 RVA: 0x0006E7B8 File Offset: 0x0006C9B8
		// (remove) Token: 0x06001ADA RID: 6874 RVA: 0x0006E7F0 File Offset: 0x0006C9F0
		private event UnityAction _CancelEvent;

		// (add) Token: 0x06001ADB RID: 6875 RVA: 0x00014C94 File Offset: 0x00012E94
		// (remove) Token: 0x06001ADC RID: 6876 RVA: 0x00014C9D File Offset: 0x00012E9D
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

		// Token: 0x06001ADD RID: 6877 RVA: 0x0006E828 File Offset: 0x0006CA28
		public override Selectable FindSelectableOnLeft()
		{
			if ((base.navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None || this._autoNavLeft)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.left);
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0006E868 File Offset: 0x0006CA68
		public override Selectable FindSelectableOnRight()
		{
			if ((base.navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None || this._autoNavRight)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.right);
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0006E8A8 File Offset: 0x0006CAA8
		public override Selectable FindSelectableOnUp()
		{
			if ((base.navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None || this._autoNavUp)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.up);
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0006E8E8 File Offset: 0x0006CAE8
		public override Selectable FindSelectableOnDown()
		{
			if ((base.navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None || this._autoNavDown)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.down);
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x00014CA6 File Offset: 0x00012EA6
		protected override void OnCanvasGroupChanged()
		{
			base.OnCanvasGroupChanged();
			if (EventSystem.current == null)
			{
				return;
			}
			this.EvaluateHightlightDisabled(EventSystem.current.currentSelectedGameObject == base.gameObject);
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x0006E928 File Offset: 0x0006CB28
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

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0006E5D0 File Offset: 0x0006C7D0
		private void StartColorTween(Color targetColor, bool instant)
		{
			if (base.targetGraphic == null)
			{
				return;
			}
			base.targetGraphic.CrossFadeColor(targetColor, instant ? 0f : base.colors.fadeDuration, true, true);
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00014B26 File Offset: 0x00012D26
		private void DoSpriteSwap(Sprite newSprite)
		{
			if (base.image == null)
			{
				return;
			}
			base.image.overrideSprite = newSprite;
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x0006E9B0 File Offset: 0x0006CBB0
		private void TriggerAnimation(string triggername)
		{
			if (base.animator == null || !base.animator.enabled || !base.animator.isActiveAndEnabled || base.animator.runtimeAnimatorController == null || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			base.animator.ResetTrigger(this._disabledHighlightedTrigger);
			base.animator.SetTrigger(triggername);
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x00014CD7 File Offset: 0x00012ED7
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.EvaluateHightlightDisabled(true);
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00014CE7 File Offset: 0x00012EE7
		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			this.EvaluateHightlightDisabled(false);
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x0006EA20 File Offset: 0x0006CC20
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

		// Token: 0x06001AE9 RID: 6889 RVA: 0x00014CF7 File Offset: 0x00012EF7
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
