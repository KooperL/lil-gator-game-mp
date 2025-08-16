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
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x00014BFE File Offset: 0x00012DFE
		// (set) Token: 0x06001ACB RID: 6859 RVA: 0x00014C06 File Offset: 0x00012E06
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

		// (get) Token: 0x06001ACC RID: 6860 RVA: 0x00014C0F File Offset: 0x00012E0F
		// (set) Token: 0x06001ACD RID: 6861 RVA: 0x00014C17 File Offset: 0x00012E17
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

		// (get) Token: 0x06001ACE RID: 6862 RVA: 0x00014C20 File Offset: 0x00012E20
		// (set) Token: 0x06001ACF RID: 6863 RVA: 0x00014C28 File Offset: 0x00012E28
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

		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x00014C31 File Offset: 0x00012E31
		// (set) Token: 0x06001AD1 RID: 6865 RVA: 0x00014C39 File Offset: 0x00012E39
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

		// (get) Token: 0x06001AD2 RID: 6866 RVA: 0x00014C42 File Offset: 0x00012E42
		// (set) Token: 0x06001AD3 RID: 6867 RVA: 0x00014C4A File Offset: 0x00012E4A
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

		// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x00014C53 File Offset: 0x00012E53
		// (set) Token: 0x06001AD5 RID: 6869 RVA: 0x00014C5B File Offset: 0x00012E5B
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

		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x00014C64 File Offset: 0x00012E64
		// (set) Token: 0x06001AD7 RID: 6871 RVA: 0x00014C6C File Offset: 0x00012E6C
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

		// (get) Token: 0x06001AD8 RID: 6872 RVA: 0x00014AB9 File Offset: 0x00012CB9
		private bool isDisabled
		{
			get
			{
				return !this.IsInteractable();
			}
		}

		// (add) Token: 0x06001AD9 RID: 6873 RVA: 0x0006E648 File Offset: 0x0006C848
		// (remove) Token: 0x06001ADA RID: 6874 RVA: 0x0006E680 File Offset: 0x0006C880
		private event UnityAction _CancelEvent;

		// (add) Token: 0x06001ADB RID: 6875 RVA: 0x00014C75 File Offset: 0x00012E75
		// (remove) Token: 0x06001ADC RID: 6876 RVA: 0x00014C7E File Offset: 0x00012E7E
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

		// Token: 0x06001ADD RID: 6877 RVA: 0x0006E6B8 File Offset: 0x0006C8B8
		public override Selectable FindSelectableOnLeft()
		{
			if ((base.navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None || this._autoNavLeft)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.left);
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0006E6F8 File Offset: 0x0006C8F8
		public override Selectable FindSelectableOnRight()
		{
			if ((base.navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None || this._autoNavRight)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.right);
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0006E738 File Offset: 0x0006C938
		public override Selectable FindSelectableOnUp()
		{
			if ((base.navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None || this._autoNavUp)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.up);
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0006E778 File Offset: 0x0006C978
		public override Selectable FindSelectableOnDown()
		{
			if ((base.navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None || this._autoNavDown)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.down);
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x00014C87 File Offset: 0x00012E87
		protected override void OnCanvasGroupChanged()
		{
			base.OnCanvasGroupChanged();
			if (EventSystem.current == null)
			{
				return;
			}
			this.EvaluateHightlightDisabled(EventSystem.current.currentSelectedGameObject == base.gameObject);
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x0006E7B8 File Offset: 0x0006C9B8
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

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0006E460 File Offset: 0x0006C660
		private void StartColorTween(Color targetColor, bool instant)
		{
			if (base.targetGraphic == null)
			{
				return;
			}
			base.targetGraphic.CrossFadeColor(targetColor, instant ? 0f : base.colors.fadeDuration, true, true);
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00014B07 File Offset: 0x00012D07
		private void DoSpriteSwap(Sprite newSprite)
		{
			if (base.image == null)
			{
				return;
			}
			base.image.overrideSprite = newSprite;
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x0006E840 File Offset: 0x0006CA40
		private void TriggerAnimation(string triggername)
		{
			if (base.animator == null || !base.animator.enabled || !base.animator.isActiveAndEnabled || base.animator.runtimeAnimatorController == null || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			base.animator.ResetTrigger(this._disabledHighlightedTrigger);
			base.animator.SetTrigger(triggername);
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x00014CB8 File Offset: 0x00012EB8
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.EvaluateHightlightDisabled(true);
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00014CC8 File Offset: 0x00012EC8
		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			this.EvaluateHightlightDisabled(false);
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x0006E8B0 File Offset: 0x0006CAB0
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

		// Token: 0x06001AE9 RID: 6889 RVA: 0x00014CD8 File Offset: 0x00012ED8
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
