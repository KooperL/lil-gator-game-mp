using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000480 RID: 1152
	[AddComponentMenu("")]
	public class UISliderControl : UIControl
	{
		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001C47 RID: 7239 RVA: 0x00015A2C File Offset: 0x00013C2C
		// (set) Token: 0x06001C48 RID: 7240 RVA: 0x00015A34 File Offset: 0x00013C34
		public bool showIcon
		{
			get
			{
				return this._showIcon;
			}
			set
			{
				if (this.iconImage == null)
				{
					return;
				}
				this.iconImage.gameObject.SetActive(value);
				this._showIcon = value;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001C49 RID: 7241 RVA: 0x00015A5D File Offset: 0x00013C5D
		// (set) Token: 0x06001C4A RID: 7242 RVA: 0x00015A65 File Offset: 0x00013C65
		public bool showSlider
		{
			get
			{
				return this._showSlider;
			}
			set
			{
				if (this.slider == null)
				{
					return;
				}
				this.slider.gameObject.SetActive(value);
				this._showSlider = value;
			}
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x0006EF14 File Offset: 0x0006D114
		public override void SetCancelCallback(Action cancelCallback)
		{
			base.SetCancelCallback(cancelCallback);
			if (cancelCallback == null || this.slider == null)
			{
				return;
			}
			if (this.slider is ICustomSelectable)
			{
				(this.slider as ICustomSelectable).CancelEvent += delegate
				{
					cancelCallback();
				};
				return;
			}
			EventTrigger eventTrigger = this.slider.GetComponent<EventTrigger>();
			if (eventTrigger == null)
			{
				eventTrigger = this.slider.gameObject.AddComponent<EventTrigger>();
			}
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.callback = new EventTrigger.TriggerEvent();
			entry.eventID = 16;
			entry.callback.AddListener(delegate(BaseEventData data)
			{
				cancelCallback();
			});
			if (eventTrigger.triggers == null)
			{
				eventTrigger.triggers = new List<EventTrigger.Entry>();
			}
			eventTrigger.triggers.Add(entry);
		}

		// Token: 0x04001E0F RID: 7695
		public Image iconImage;

		// Token: 0x04001E10 RID: 7696
		public Slider slider;

		// Token: 0x04001E11 RID: 7697
		private bool _showIcon;

		// Token: 0x04001E12 RID: 7698
		private bool _showSlider;
	}
}
