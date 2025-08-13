using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000455 RID: 1109
	[AddComponentMenu("")]
	public class CustomButton : Button, ICustomSelectable, ICancelHandler, IEventSystemHandler
	{
		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x0001465A File Offset: 0x0001285A
		// (set) Token: 0x06001A40 RID: 6720 RVA: 0x00014662 File Offset: 0x00012862
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

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x0001466B File Offset: 0x0001286B
		// (set) Token: 0x06001A42 RID: 6722 RVA: 0x00014673 File Offset: 0x00012873
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

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001A43 RID: 6723 RVA: 0x0001467C File Offset: 0x0001287C
		// (set) Token: 0x06001A44 RID: 6724 RVA: 0x00014684 File Offset: 0x00012884
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

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001A45 RID: 6725 RVA: 0x0001468D File Offset: 0x0001288D
		// (set) Token: 0x06001A46 RID: 6726 RVA: 0x00014695 File Offset: 0x00012895
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

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001A47 RID: 6727 RVA: 0x0001469E File Offset: 0x0001289E
		// (set) Token: 0x06001A48 RID: 6728 RVA: 0x000146A6 File Offset: 0x000128A6
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

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001A49 RID: 6729 RVA: 0x000146AF File Offset: 0x000128AF
		// (set) Token: 0x06001A4A RID: 6730 RVA: 0x000146B7 File Offset: 0x000128B7
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

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001A4B RID: 6731 RVA: 0x000146C0 File Offset: 0x000128C0
		// (set) Token: 0x06001A4C RID: 6732 RVA: 0x000146C8 File Offset: 0x000128C8
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

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001A4D RID: 6733 RVA: 0x000146D1 File Offset: 0x000128D1
		private bool isDisabled
		{
			get
			{
				return !this.IsInteractable();
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06001A4E RID: 6734 RVA: 0x0006C3D4 File Offset: 0x0006A5D4
		// (remove) Token: 0x06001A4F RID: 6735 RVA: 0x0006C40C File Offset: 0x0006A60C
		private event UnityAction _CancelEvent;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06001A50 RID: 6736 RVA: 0x000146DC File Offset: 0x000128DC
		// (remove) Token: 0x06001A51 RID: 6737 RVA: 0x000146E5 File Offset: 0x000128E5
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

		// Token: 0x06001A52 RID: 6738 RVA: 0x0006C444 File Offset: 0x0006A644
		public override Selectable FindSelectableOnLeft()
		{
			if ((base.navigation.mode & 1) != null || this._autoNavLeft)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.left);
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x0006C484 File Offset: 0x0006A684
		public override Selectable FindSelectableOnRight()
		{
			if ((base.navigation.mode & 1) != null || this._autoNavRight)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.right);
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x0006C4C4 File Offset: 0x0006A6C4
		public override Selectable FindSelectableOnUp()
		{
			if ((base.navigation.mode & 2) != null || this._autoNavUp)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.up);
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x0006C504 File Offset: 0x0006A704
		public override Selectable FindSelectableOnDown()
		{
			if ((base.navigation.mode & 2) != null || this._autoNavDown)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.down);
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x000146EE File Offset: 0x000128EE
		protected override void OnCanvasGroupChanged()
		{
			base.OnCanvasGroupChanged();
			if (EventSystem.current == null)
			{
				return;
			}
			this.EvaluateHightlightDisabled(EventSystem.current.currentSelectedGameObject == base.gameObject);
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x0006C544 File Offset: 0x0006A744
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

		// Token: 0x06001A58 RID: 6744 RVA: 0x0006C5CC File Offset: 0x0006A7CC
		private void StartColorTween(Color targetColor, bool instant)
		{
			if (base.targetGraphic == null)
			{
				return;
			}
			base.targetGraphic.CrossFadeColor(targetColor, instant ? 0f : base.colors.fadeDuration, true, true);
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x0001471F File Offset: 0x0001291F
		private void DoSpriteSwap(Sprite newSprite)
		{
			if (base.image == null)
			{
				return;
			}
			base.image.overrideSprite = newSprite;
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x0006C610 File Offset: 0x0006A810
		private void TriggerAnimation(string triggername)
		{
			if (base.animator == null || !base.animator.enabled || !base.animator.isActiveAndEnabled || base.animator.runtimeAnimatorController == null || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			base.animator.ResetTrigger(this._disabledHighlightedTrigger);
			base.animator.SetTrigger(triggername);
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x0001473C File Offset: 0x0001293C
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.EvaluateHightlightDisabled(true);
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x0001474C File Offset: 0x0001294C
		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			this.EvaluateHightlightDisabled(false);
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x0001475C File Offset: 0x0001295C
		private void Press()
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			base.onClick.Invoke();
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x0006C680 File Offset: 0x0006A880
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

		// Token: 0x06001A5F RID: 6751 RVA: 0x0001477A File Offset: 0x0001297A
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

		// Token: 0x06001A60 RID: 6752 RVA: 0x000147B7 File Offset: 0x000129B7
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

		// Token: 0x06001A61 RID: 6753 RVA: 0x0006C6CC File Offset: 0x0006A8CC
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

		// Token: 0x06001A62 RID: 6754 RVA: 0x000147C6 File Offset: 0x000129C6
		public void OnCancel(BaseEventData eventData)
		{
			if (this._CancelEvent != null)
			{
				this._CancelEvent.Invoke();
			}
		}

		// Token: 0x04001CE2 RID: 7394
		[SerializeField]
		private Sprite _disabledHighlightedSprite;

		// Token: 0x04001CE3 RID: 7395
		[SerializeField]
		private Color _disabledHighlightedColor;

		// Token: 0x04001CE4 RID: 7396
		[SerializeField]
		private string _disabledHighlightedTrigger;

		// Token: 0x04001CE5 RID: 7397
		[SerializeField]
		private bool _autoNavUp = true;

		// Token: 0x04001CE6 RID: 7398
		[SerializeField]
		private bool _autoNavDown = true;

		// Token: 0x04001CE7 RID: 7399
		[SerializeField]
		private bool _autoNavLeft = true;

		// Token: 0x04001CE8 RID: 7400
		[SerializeField]
		private bool _autoNavRight = true;

		// Token: 0x04001CE9 RID: 7401
		private bool isHighlightDisabled;
	}
}
