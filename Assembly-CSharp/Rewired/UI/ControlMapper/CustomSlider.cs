using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000457 RID: 1111
	[AddComponentMenu("")]
	public class CustomSlider : Slider, ICustomSelectable, ICancelHandler, IEventSystemHandler
	{
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001A6A RID: 6762 RVA: 0x00014816 File Offset: 0x00012A16
		// (set) Token: 0x06001A6B RID: 6763 RVA: 0x0001481E File Offset: 0x00012A1E
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

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001A6C RID: 6764 RVA: 0x00014827 File Offset: 0x00012A27
		// (set) Token: 0x06001A6D RID: 6765 RVA: 0x0001482F File Offset: 0x00012A2F
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

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x00014838 File Offset: 0x00012A38
		// (set) Token: 0x06001A6F RID: 6767 RVA: 0x00014840 File Offset: 0x00012A40
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

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x00014849 File Offset: 0x00012A49
		// (set) Token: 0x06001A71 RID: 6769 RVA: 0x00014851 File Offset: 0x00012A51
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

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001A72 RID: 6770 RVA: 0x0001485A File Offset: 0x00012A5A
		// (set) Token: 0x06001A73 RID: 6771 RVA: 0x00014862 File Offset: 0x00012A62
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

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x0001486B File Offset: 0x00012A6B
		// (set) Token: 0x06001A75 RID: 6773 RVA: 0x00014873 File Offset: 0x00012A73
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

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001A76 RID: 6774 RVA: 0x0001487C File Offset: 0x00012A7C
		// (set) Token: 0x06001A77 RID: 6775 RVA: 0x00014884 File Offset: 0x00012A84
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

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001A78 RID: 6776 RVA: 0x000146D1 File Offset: 0x000128D1
		private bool isDisabled
		{
			get
			{
				return !this.IsInteractable();
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06001A79 RID: 6777 RVA: 0x0006C7B4 File Offset: 0x0006A9B4
		// (remove) Token: 0x06001A7A RID: 6778 RVA: 0x0006C7EC File Offset: 0x0006A9EC
		private event UnityAction _CancelEvent;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06001A7B RID: 6779 RVA: 0x0001488D File Offset: 0x00012A8D
		// (remove) Token: 0x06001A7C RID: 6780 RVA: 0x00014896 File Offset: 0x00012A96
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

		// Token: 0x06001A7D RID: 6781 RVA: 0x0006C824 File Offset: 0x0006AA24
		public override Selectable FindSelectableOnLeft()
		{
			if ((base.navigation.mode & 1) != null || this._autoNavLeft)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.left);
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0006C864 File Offset: 0x0006AA64
		public override Selectable FindSelectableOnRight()
		{
			if ((base.navigation.mode & 1) != null || this._autoNavRight)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.right);
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x0006C8A4 File Offset: 0x0006AAA4
		public override Selectable FindSelectableOnUp()
		{
			if ((base.navigation.mode & 2) != null || this._autoNavUp)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.up);
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x0006C8E4 File Offset: 0x0006AAE4
		public override Selectable FindSelectableOnDown()
		{
			if ((base.navigation.mode & 2) != null || this._autoNavDown)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.down);
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x0001489F File Offset: 0x00012A9F
		protected override void OnCanvasGroupChanged()
		{
			base.OnCanvasGroupChanged();
			if (EventSystem.current == null)
			{
				return;
			}
			this.EvaluateHightlightDisabled(EventSystem.current.currentSelectedGameObject == base.gameObject);
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x0006C924 File Offset: 0x0006AB24
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

		// Token: 0x06001A83 RID: 6787 RVA: 0x0006C5CC File Offset: 0x0006A7CC
		private void StartColorTween(Color targetColor, bool instant)
		{
			if (base.targetGraphic == null)
			{
				return;
			}
			base.targetGraphic.CrossFadeColor(targetColor, instant ? 0f : base.colors.fadeDuration, true, true);
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x0001471F File Offset: 0x0001291F
		private void DoSpriteSwap(Sprite newSprite)
		{
			if (base.image == null)
			{
				return;
			}
			base.image.overrideSprite = newSprite;
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x0006C9AC File Offset: 0x0006ABAC
		private void TriggerAnimation(string triggername)
		{
			if (base.animator == null || !base.animator.enabled || !base.animator.isActiveAndEnabled || base.animator.runtimeAnimatorController == null || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			base.animator.ResetTrigger(this._disabledHighlightedTrigger);
			base.animator.SetTrigger(triggername);
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x000148D0 File Offset: 0x00012AD0
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.EvaluateHightlightDisabled(true);
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x000148E0 File Offset: 0x00012AE0
		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			this.EvaluateHightlightDisabled(false);
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x0006CA1C File Offset: 0x0006AC1C
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

		// Token: 0x06001A89 RID: 6793 RVA: 0x000148F0 File Offset: 0x00012AF0
		public void OnCancel(BaseEventData eventData)
		{
			if (this._CancelEvent != null)
			{
				this._CancelEvent.Invoke();
			}
		}

		// Token: 0x04001CF0 RID: 7408
		[SerializeField]
		private Sprite _disabledHighlightedSprite;

		// Token: 0x04001CF1 RID: 7409
		[SerializeField]
		private Color _disabledHighlightedColor;

		// Token: 0x04001CF2 RID: 7410
		[SerializeField]
		private string _disabledHighlightedTrigger;

		// Token: 0x04001CF3 RID: 7411
		[SerializeField]
		private bool _autoNavUp = true;

		// Token: 0x04001CF4 RID: 7412
		[SerializeField]
		private bool _autoNavDown = true;

		// Token: 0x04001CF5 RID: 7413
		[SerializeField]
		private bool _autoNavLeft = true;

		// Token: 0x04001CF6 RID: 7414
		[SerializeField]
		private bool _autoNavRight = true;

		// Token: 0x04001CF7 RID: 7415
		private bool isHighlightDisabled;
	}
}
