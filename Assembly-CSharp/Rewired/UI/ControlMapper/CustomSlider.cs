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
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x00014C13 File Offset: 0x00012E13
		// (set) Token: 0x06001ACB RID: 6859 RVA: 0x00014C1B File Offset: 0x00012E1B
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

		// (get) Token: 0x06001ACC RID: 6860 RVA: 0x00014C24 File Offset: 0x00012E24
		// (set) Token: 0x06001ACD RID: 6861 RVA: 0x00014C2C File Offset: 0x00012E2C
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

		// (get) Token: 0x06001ACE RID: 6862 RVA: 0x00014C35 File Offset: 0x00012E35
		// (set) Token: 0x06001ACF RID: 6863 RVA: 0x00014C3D File Offset: 0x00012E3D
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

		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x00014C46 File Offset: 0x00012E46
		// (set) Token: 0x06001AD1 RID: 6865 RVA: 0x00014C4E File Offset: 0x00012E4E
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

		// (get) Token: 0x06001AD2 RID: 6866 RVA: 0x00014C57 File Offset: 0x00012E57
		// (set) Token: 0x06001AD3 RID: 6867 RVA: 0x00014C5F File Offset: 0x00012E5F
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

		// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x00014C68 File Offset: 0x00012E68
		// (set) Token: 0x06001AD5 RID: 6869 RVA: 0x00014C70 File Offset: 0x00012E70
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

		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x00014C79 File Offset: 0x00012E79
		// (set) Token: 0x06001AD7 RID: 6871 RVA: 0x00014C81 File Offset: 0x00012E81
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

		// (get) Token: 0x06001AD8 RID: 6872 RVA: 0x00014ACE File Offset: 0x00012CCE
		private bool isDisabled
		{
			get
			{
				return !this.IsInteractable();
			}
		}

		// (add) Token: 0x06001AD9 RID: 6873 RVA: 0x0006E7DC File Offset: 0x0006C9DC
		// (remove) Token: 0x06001ADA RID: 6874 RVA: 0x0006E814 File Offset: 0x0006CA14
		private event UnityAction _CancelEvent;

		// (add) Token: 0x06001ADB RID: 6875 RVA: 0x00014C8A File Offset: 0x00012E8A
		// (remove) Token: 0x06001ADC RID: 6876 RVA: 0x00014C93 File Offset: 0x00012E93
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

		// Token: 0x06001ADD RID: 6877 RVA: 0x0006E84C File Offset: 0x0006CA4C
		public override Selectable FindSelectableOnLeft()
		{
			if ((base.navigation.mode & 1) != null || this._autoNavLeft)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.left);
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0006E88C File Offset: 0x0006CA8C
		public override Selectable FindSelectableOnRight()
		{
			if ((base.navigation.mode & 1) != null || this._autoNavRight)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.right);
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0006E8CC File Offset: 0x0006CACC
		public override Selectable FindSelectableOnUp()
		{
			if ((base.navigation.mode & 2) != null || this._autoNavUp)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.up);
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0006E90C File Offset: 0x0006CB0C
		public override Selectable FindSelectableOnDown()
		{
			if ((base.navigation.mode & 2) != null || this._autoNavDown)
			{
				return UISelectionUtility.FindNextSelectable(this, base.transform, Vector3.down);
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x00014C9C File Offset: 0x00012E9C
		protected override void OnCanvasGroupChanged()
		{
			base.OnCanvasGroupChanged();
			if (EventSystem.current == null)
			{
				return;
			}
			this.EvaluateHightlightDisabled(EventSystem.current.currentSelectedGameObject == base.gameObject);
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x0006E94C File Offset: 0x0006CB4C
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

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0006E5F4 File Offset: 0x0006C7F4
		private void StartColorTween(Color targetColor, bool instant)
		{
			if (base.targetGraphic == null)
			{
				return;
			}
			base.targetGraphic.CrossFadeColor(targetColor, instant ? 0f : base.colors.fadeDuration, true, true);
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00014B1C File Offset: 0x00012D1C
		private void DoSpriteSwap(Sprite newSprite)
		{
			if (base.image == null)
			{
				return;
			}
			base.image.overrideSprite = newSprite;
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x0006E9D4 File Offset: 0x0006CBD4
		private void TriggerAnimation(string triggername)
		{
			if (base.animator == null || !base.animator.enabled || !base.animator.isActiveAndEnabled || base.animator.runtimeAnimatorController == null || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			base.animator.ResetTrigger(this._disabledHighlightedTrigger);
			base.animator.SetTrigger(triggername);
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x00014CCD File Offset: 0x00012ECD
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.EvaluateHightlightDisabled(true);
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00014CDD File Offset: 0x00012EDD
		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			this.EvaluateHightlightDisabled(false);
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x0006EA44 File Offset: 0x0006CC44
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

		// Token: 0x06001AE9 RID: 6889 RVA: 0x00014CED File Offset: 0x00012EED
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
