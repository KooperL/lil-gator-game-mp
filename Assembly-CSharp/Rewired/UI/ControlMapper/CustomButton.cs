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
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x00014A57 File Offset: 0x00012C57
		// (set) Token: 0x06001AA0 RID: 6816 RVA: 0x00014A5F File Offset: 0x00012C5F
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

		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x00014A68 File Offset: 0x00012C68
		// (set) Token: 0x06001AA2 RID: 6818 RVA: 0x00014A70 File Offset: 0x00012C70
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

		// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x00014A79 File Offset: 0x00012C79
		// (set) Token: 0x06001AA4 RID: 6820 RVA: 0x00014A81 File Offset: 0x00012C81
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

		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x00014A8A File Offset: 0x00012C8A
		// (set) Token: 0x06001AA6 RID: 6822 RVA: 0x00014A92 File Offset: 0x00012C92
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

		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x00014A9B File Offset: 0x00012C9B
		// (set) Token: 0x06001AA8 RID: 6824 RVA: 0x00014AA3 File Offset: 0x00012CA3
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

		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x00014AAC File Offset: 0x00012CAC
		// (set) Token: 0x06001AAA RID: 6826 RVA: 0x00014AB4 File Offset: 0x00012CB4
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

		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x00014ABD File Offset: 0x00012CBD
		// (set) Token: 0x06001AAC RID: 6828 RVA: 0x00014AC5 File Offset: 0x00012CC5
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

		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x00014ACE File Offset: 0x00012CCE
		private bool isDisabled
		{
			get
			{
				return !this.IsInteractable();
			}
		}

		// (add) Token: 0x06001AAE RID: 6830 RVA: 0x0006E3FC File Offset: 0x0006C5FC
		// (remove) Token: 0x06001AAF RID: 6831 RVA: 0x0006E434 File Offset: 0x0006C634
		private event UnityAction _CancelEvent;

		// (add) Token: 0x06001AB0 RID: 6832 RVA: 0x00014AD9 File Offset: 0x00012CD9
		// (remove) Token: 0x06001AB1 RID: 6833 RVA: 0x00014AE2 File Offset: 0x00012CE2
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

		// Token: 0x06001AB2 RID: 6834 RVA: 0x0006E46C File Offset: 0x0006C66C
		public override Selectable FindSelectableOnLeft()
		{
			if ((base.navigation.mode & 1) != null || this._autoNavLeft)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.left);
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x0006E4AC File Offset: 0x0006C6AC
		public override Selectable FindSelectableOnRight()
		{
			if ((base.navigation.mode & 1) != null || this._autoNavRight)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.right);
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x0006E4EC File Offset: 0x0006C6EC
		public override Selectable FindSelectableOnUp()
		{
			if ((base.navigation.mode & 2) != null || this._autoNavUp)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.up);
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x0006E52C File Offset: 0x0006C72C
		public override Selectable FindSelectableOnDown()
		{
			if ((base.navigation.mode & 2) != null || this._autoNavDown)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.down);
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x00014AEB File Offset: 0x00012CEB
		protected override void OnCanvasGroupChanged()
		{
			base.OnCanvasGroupChanged();
			if (EventSystem.current == null)
			{
				return;
			}
			this.EvaluateHightlightDisabled(EventSystem.current.currentSelectedGameObject == base.gameObject);
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x0006E56C File Offset: 0x0006C76C
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

		// Token: 0x06001AB8 RID: 6840 RVA: 0x0006E5F4 File Offset: 0x0006C7F4
		private void StartColorTween(Color targetColor, bool instant)
		{
			if (base.targetGraphic == null)
			{
				return;
			}
			base.targetGraphic.CrossFadeColor(targetColor, instant ? 0f : base.colors.fadeDuration, true, true);
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x00014B1C File Offset: 0x00012D1C
		private void DoSpriteSwap(Sprite newSprite)
		{
			if (base.image == null)
			{
				return;
			}
			base.image.overrideSprite = newSprite;
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x0006E638 File Offset: 0x0006C838
		private void TriggerAnimation(string triggername)
		{
			if (base.animator == null || !base.animator.enabled || !base.animator.isActiveAndEnabled || base.animator.runtimeAnimatorController == null || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			base.animator.ResetTrigger(this._disabledHighlightedTrigger);
			base.animator.SetTrigger(triggername);
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x00014B39 File Offset: 0x00012D39
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.EvaluateHightlightDisabled(true);
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x00014B49 File Offset: 0x00012D49
		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			this.EvaluateHightlightDisabled(false);
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x00014B59 File Offset: 0x00012D59
		private void Press()
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			base.onClick.Invoke();
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x0006E6A8 File Offset: 0x0006C8A8
		public override void OnPointerClick(PointerEventData eventData)
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			if (eventData.button != null)
			{
				return;
			}
			this.Press();
			if (!this.IsActive() || !this.IsInteractable())
			{
				this.isHighlightDisabled = true;
				this.DoStateTransition(4, false);
			}
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00014B77 File Offset: 0x00012D77
		public override void OnSubmit(BaseEventData eventData)
		{
			this.Press();
			if (!this.IsActive() || !this.IsInteractable())
			{
				this.isHighlightDisabled = true;
				this.DoStateTransition(4, false);
				return;
			}
			this.DoStateTransition(2, false);
			base.StartCoroutine(this.OnFinishSubmit());
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00014BB4 File Offset: 0x00012DB4
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

		// Token: 0x06001AC1 RID: 6849 RVA: 0x0006E6F4 File Offset: 0x0006C8F4
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

		// Token: 0x06001AC2 RID: 6850 RVA: 0x00014BC3 File Offset: 0x00012DC3
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
