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
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x0005C653 File Offset: 0x0005A853
		// (set) Token: 0x0600156A RID: 5482 RVA: 0x0005C65B File Offset: 0x0005A85B
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

		// (get) Token: 0x0600156B RID: 5483 RVA: 0x0005C664 File Offset: 0x0005A864
		// (set) Token: 0x0600156C RID: 5484 RVA: 0x0005C66C File Offset: 0x0005A86C
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

		// (get) Token: 0x0600156D RID: 5485 RVA: 0x0005C675 File Offset: 0x0005A875
		// (set) Token: 0x0600156E RID: 5486 RVA: 0x0005C67D File Offset: 0x0005A87D
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

		// (get) Token: 0x0600156F RID: 5487 RVA: 0x0005C686 File Offset: 0x0005A886
		// (set) Token: 0x06001570 RID: 5488 RVA: 0x0005C68E File Offset: 0x0005A88E
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

		// (get) Token: 0x06001571 RID: 5489 RVA: 0x0005C697 File Offset: 0x0005A897
		// (set) Token: 0x06001572 RID: 5490 RVA: 0x0005C69F File Offset: 0x0005A89F
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

		// (get) Token: 0x06001573 RID: 5491 RVA: 0x0005C6A8 File Offset: 0x0005A8A8
		// (set) Token: 0x06001574 RID: 5492 RVA: 0x0005C6B0 File Offset: 0x0005A8B0
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

		// (get) Token: 0x06001575 RID: 5493 RVA: 0x0005C6B9 File Offset: 0x0005A8B9
		// (set) Token: 0x06001576 RID: 5494 RVA: 0x0005C6C1 File Offset: 0x0005A8C1
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

		// (get) Token: 0x06001577 RID: 5495 RVA: 0x0005C6CA File Offset: 0x0005A8CA
		private bool isDisabled
		{
			get
			{
				return !this.IsInteractable();
			}
		}

		// (add) Token: 0x06001578 RID: 5496 RVA: 0x0005C6D8 File Offset: 0x0005A8D8
		// (remove) Token: 0x06001579 RID: 5497 RVA: 0x0005C710 File Offset: 0x0005A910
		private event UnityAction _CancelEvent;

		// (add) Token: 0x0600157A RID: 5498 RVA: 0x0005C745 File Offset: 0x0005A945
		// (remove) Token: 0x0600157B RID: 5499 RVA: 0x0005C74E File Offset: 0x0005A94E
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

		// Token: 0x0600157C RID: 5500 RVA: 0x0005C758 File Offset: 0x0005A958
		public override Selectable FindSelectableOnLeft()
		{
			if ((base.navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None || this._autoNavLeft)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.left);
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0005C798 File Offset: 0x0005A998
		public override Selectable FindSelectableOnRight()
		{
			if ((base.navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None || this._autoNavRight)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.right);
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0005C7D8 File Offset: 0x0005A9D8
		public override Selectable FindSelectableOnUp()
		{
			if ((base.navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None || this._autoNavUp)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.up);
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0005C818 File Offset: 0x0005AA18
		public override Selectable FindSelectableOnDown()
		{
			if ((base.navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None || this._autoNavDown)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.down);
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0005C857 File Offset: 0x0005AA57
		protected override void OnCanvasGroupChanged()
		{
			base.OnCanvasGroupChanged();
			if (EventSystem.current == null)
			{
				return;
			}
			this.EvaluateHightlightDisabled(EventSystem.current.currentSelectedGameObject == base.gameObject);
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0005C888 File Offset: 0x0005AA88
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

		// Token: 0x06001582 RID: 5506 RVA: 0x0005C910 File Offset: 0x0005AB10
		private void StartColorTween(Color targetColor, bool instant)
		{
			if (base.targetGraphic == null)
			{
				return;
			}
			base.targetGraphic.CrossFadeColor(targetColor, instant ? 0f : base.colors.fadeDuration, true, true);
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0005C952 File Offset: 0x0005AB52
		private void DoSpriteSwap(Sprite newSprite)
		{
			if (base.image == null)
			{
				return;
			}
			base.image.overrideSprite = newSprite;
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x0005C970 File Offset: 0x0005AB70
		private void TriggerAnimation(string triggername)
		{
			if (base.animator == null || !base.animator.enabled || !base.animator.isActiveAndEnabled || base.animator.runtimeAnimatorController == null || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			base.animator.ResetTrigger(this._disabledHighlightedTrigger);
			base.animator.SetTrigger(triggername);
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x0005C9DE File Offset: 0x0005ABDE
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.EvaluateHightlightDisabled(true);
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x0005C9EE File Offset: 0x0005ABEE
		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			this.EvaluateHightlightDisabled(false);
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x0005CA00 File Offset: 0x0005AC00
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

		// Token: 0x06001588 RID: 5512 RVA: 0x0005CA52 File Offset: 0x0005AC52
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
