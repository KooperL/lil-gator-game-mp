using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200031F RID: 799
	[AddComponentMenu("")]
	public class CustomSlider : Slider, ICustomSelectable, ICancelHandler, IEventSystemHandler
	{
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x0005C21B File Offset: 0x0005A41B
		// (set) Token: 0x06001549 RID: 5449 RVA: 0x0005C223 File Offset: 0x0005A423
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

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x0005C22C File Offset: 0x0005A42C
		// (set) Token: 0x0600154B RID: 5451 RVA: 0x0005C234 File Offset: 0x0005A434
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

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x0005C23D File Offset: 0x0005A43D
		// (set) Token: 0x0600154D RID: 5453 RVA: 0x0005C245 File Offset: 0x0005A445
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

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x0005C24E File Offset: 0x0005A44E
		// (set) Token: 0x0600154F RID: 5455 RVA: 0x0005C256 File Offset: 0x0005A456
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

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x0005C25F File Offset: 0x0005A45F
		// (set) Token: 0x06001551 RID: 5457 RVA: 0x0005C267 File Offset: 0x0005A467
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

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x0005C270 File Offset: 0x0005A470
		// (set) Token: 0x06001553 RID: 5459 RVA: 0x0005C278 File Offset: 0x0005A478
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

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x0005C281 File Offset: 0x0005A481
		// (set) Token: 0x06001555 RID: 5461 RVA: 0x0005C289 File Offset: 0x0005A489
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

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x0005C292 File Offset: 0x0005A492
		private bool isDisabled
		{
			get
			{
				return !this.IsInteractable();
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06001557 RID: 5463 RVA: 0x0005C2A0 File Offset: 0x0005A4A0
		// (remove) Token: 0x06001558 RID: 5464 RVA: 0x0005C2D8 File Offset: 0x0005A4D8
		private event UnityAction _CancelEvent;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06001559 RID: 5465 RVA: 0x0005C30D File Offset: 0x0005A50D
		// (remove) Token: 0x0600155A RID: 5466 RVA: 0x0005C316 File Offset: 0x0005A516
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

		// Token: 0x0600155B RID: 5467 RVA: 0x0005C320 File Offset: 0x0005A520
		public override Selectable FindSelectableOnLeft()
		{
			if ((base.navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None || this._autoNavLeft)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.left);
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x0005C360 File Offset: 0x0005A560
		public override Selectable FindSelectableOnRight()
		{
			if ((base.navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None || this._autoNavRight)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.right);
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0005C3A0 File Offset: 0x0005A5A0
		public override Selectable FindSelectableOnUp()
		{
			if ((base.navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None || this._autoNavUp)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.up);
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x0005C3E0 File Offset: 0x0005A5E0
		public override Selectable FindSelectableOnDown()
		{
			if ((base.navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None || this._autoNavDown)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.down);
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x0005C41F File Offset: 0x0005A61F
		protected override void OnCanvasGroupChanged()
		{
			base.OnCanvasGroupChanged();
			if (EventSystem.current == null)
			{
				return;
			}
			this.EvaluateHightlightDisabled(EventSystem.current.currentSelectedGameObject == base.gameObject);
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0005C450 File Offset: 0x0005A650
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

		// Token: 0x06001561 RID: 5473 RVA: 0x0005C4D8 File Offset: 0x0005A6D8
		private void StartColorTween(Color targetColor, bool instant)
		{
			if (base.targetGraphic == null)
			{
				return;
			}
			base.targetGraphic.CrossFadeColor(targetColor, instant ? 0f : base.colors.fadeDuration, true, true);
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0005C51A File Offset: 0x0005A71A
		private void DoSpriteSwap(Sprite newSprite)
		{
			if (base.image == null)
			{
				return;
			}
			base.image.overrideSprite = newSprite;
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0005C538 File Offset: 0x0005A738
		private void TriggerAnimation(string triggername)
		{
			if (base.animator == null || !base.animator.enabled || !base.animator.isActiveAndEnabled || base.animator.runtimeAnimatorController == null || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			base.animator.ResetTrigger(this._disabledHighlightedTrigger);
			base.animator.SetTrigger(triggername);
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0005C5A6 File Offset: 0x0005A7A6
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.EvaluateHightlightDisabled(true);
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x0005C5B6 File Offset: 0x0005A7B6
		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			this.EvaluateHightlightDisabled(false);
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0005C5C8 File Offset: 0x0005A7C8
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

		// Token: 0x06001567 RID: 5479 RVA: 0x0005C61A File Offset: 0x0005A81A
		public void OnCancel(BaseEventData eventData)
		{
			if (this._CancelEvent != null)
			{
				this._CancelEvent();
			}
		}

		// Token: 0x0400181A RID: 6170
		[SerializeField]
		private Sprite _disabledHighlightedSprite;

		// Token: 0x0400181B RID: 6171
		[SerializeField]
		private Color _disabledHighlightedColor;

		// Token: 0x0400181C RID: 6172
		[SerializeField]
		private string _disabledHighlightedTrigger;

		// Token: 0x0400181D RID: 6173
		[SerializeField]
		private bool _autoNavUp = true;

		// Token: 0x0400181E RID: 6174
		[SerializeField]
		private bool _autoNavDown = true;

		// Token: 0x0400181F RID: 6175
		[SerializeField]
		private bool _autoNavLeft = true;

		// Token: 0x04001820 RID: 6176
		[SerializeField]
		private bool _autoNavRight = true;

		// Token: 0x04001821 RID: 6177
		private bool isHighlightDisabled;
	}
}
