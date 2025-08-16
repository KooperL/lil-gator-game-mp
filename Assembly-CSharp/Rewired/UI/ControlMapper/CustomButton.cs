using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class CustomButton : Button, ICustomSelectable, ICancelHandler, IEventSystemHandler
	{
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x00014A42 File Offset: 0x00012C42
		// (set) Token: 0x06001AA0 RID: 6816 RVA: 0x00014A4A File Offset: 0x00012C4A
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

		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x00014A53 File Offset: 0x00012C53
		// (set) Token: 0x06001AA2 RID: 6818 RVA: 0x00014A5B File Offset: 0x00012C5B
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

		// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x00014A64 File Offset: 0x00012C64
		// (set) Token: 0x06001AA4 RID: 6820 RVA: 0x00014A6C File Offset: 0x00012C6C
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

		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x00014A75 File Offset: 0x00012C75
		// (set) Token: 0x06001AA6 RID: 6822 RVA: 0x00014A7D File Offset: 0x00012C7D
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

		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x00014A86 File Offset: 0x00012C86
		// (set) Token: 0x06001AA8 RID: 6824 RVA: 0x00014A8E File Offset: 0x00012C8E
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

		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x00014A97 File Offset: 0x00012C97
		// (set) Token: 0x06001AAA RID: 6826 RVA: 0x00014A9F File Offset: 0x00012C9F
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

		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x00014AA8 File Offset: 0x00012CA8
		// (set) Token: 0x06001AAC RID: 6828 RVA: 0x00014AB0 File Offset: 0x00012CB0
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

		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x00014AB9 File Offset: 0x00012CB9
		private bool isDisabled
		{
			get
			{
				return !this.IsInteractable();
			}
		}

		// (add) Token: 0x06001AAE RID: 6830 RVA: 0x0006E268 File Offset: 0x0006C468
		// (remove) Token: 0x06001AAF RID: 6831 RVA: 0x0006E2A0 File Offset: 0x0006C4A0
		private event UnityAction _CancelEvent;

		// (add) Token: 0x06001AB0 RID: 6832 RVA: 0x00014AC4 File Offset: 0x00012CC4
		// (remove) Token: 0x06001AB1 RID: 6833 RVA: 0x00014ACD File Offset: 0x00012CCD
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

		// Token: 0x06001AB2 RID: 6834 RVA: 0x0006E2D8 File Offset: 0x0006C4D8
		public override Selectable FindSelectableOnLeft()
		{
			if ((base.navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None || this._autoNavLeft)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.left);
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x0006E318 File Offset: 0x0006C518
		public override Selectable FindSelectableOnRight()
		{
			if ((base.navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None || this._autoNavRight)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.right);
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x0006E358 File Offset: 0x0006C558
		public override Selectable FindSelectableOnUp()
		{
			if ((base.navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None || this._autoNavUp)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.up);
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x0006E398 File Offset: 0x0006C598
		public override Selectable FindSelectableOnDown()
		{
			if ((base.navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None || this._autoNavDown)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.down);
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x00014AD6 File Offset: 0x00012CD6
		protected override void OnCanvasGroupChanged()
		{
			base.OnCanvasGroupChanged();
			if (EventSystem.current == null)
			{
				return;
			}
			this.EvaluateHightlightDisabled(EventSystem.current.currentSelectedGameObject == base.gameObject);
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x0006E3D8 File Offset: 0x0006C5D8
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

		// Token: 0x06001AB8 RID: 6840 RVA: 0x0006E460 File Offset: 0x0006C660
		private void StartColorTween(Color targetColor, bool instant)
		{
			if (base.targetGraphic == null)
			{
				return;
			}
			base.targetGraphic.CrossFadeColor(targetColor, instant ? 0f : base.colors.fadeDuration, true, true);
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x00014B07 File Offset: 0x00012D07
		private void DoSpriteSwap(Sprite newSprite)
		{
			if (base.image == null)
			{
				return;
			}
			base.image.overrideSprite = newSprite;
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x0006E4A4 File Offset: 0x0006C6A4
		private void TriggerAnimation(string triggername)
		{
			if (base.animator == null || !base.animator.enabled || !base.animator.isActiveAndEnabled || base.animator.runtimeAnimatorController == null || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			base.animator.ResetTrigger(this._disabledHighlightedTrigger);
			base.animator.SetTrigger(triggername);
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x00014B24 File Offset: 0x00012D24
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.EvaluateHightlightDisabled(true);
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x00014B34 File Offset: 0x00012D34
		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			this.EvaluateHightlightDisabled(false);
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x00014B44 File Offset: 0x00012D44
		private void Press()
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			base.onClick.Invoke();
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x0006E514 File Offset: 0x0006C714
		public override void OnPointerClick(PointerEventData eventData)
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.Press();
			if (!this.IsActive() || !this.IsInteractable())
			{
				this.isHighlightDisabled = true;
				this.DoStateTransition(Selectable.SelectionState.Disabled, false);
			}
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00014B62 File Offset: 0x00012D62
		public override void OnSubmit(BaseEventData eventData)
		{
			this.Press();
			if (!this.IsActive() || !this.IsInteractable())
			{
				this.isHighlightDisabled = true;
				this.DoStateTransition(Selectable.SelectionState.Disabled, false);
				return;
			}
			this.DoStateTransition(Selectable.SelectionState.Pressed, false);
			base.StartCoroutine(this.OnFinishSubmit());
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00014B9F File Offset: 0x00012D9F
		private IEnumerator OnFinishSubmit()
		{
			float fadeTime = base.colors.fadeDuration;
			float elapsedTime = 0f;
			while (elapsedTime < fadeTime)
			{
				elapsedTime += Time.unscaledDeltaTime;
				yield return null;
			}
			this.DoStateTransition(base.currentSelectionState, false);
			yield break;
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x0006E560 File Offset: 0x0006C760
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

		// Token: 0x06001AC2 RID: 6850 RVA: 0x00014BAE File Offset: 0x00012DAE
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
