using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class UISliderControl : UIControl
	{
		// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x00015E62 File Offset: 0x00014062
		// (set) Token: 0x06001CA8 RID: 7336 RVA: 0x00015E6A File Offset: 0x0001406A
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

		// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x00015E93 File Offset: 0x00014093
		// (set) Token: 0x06001CAA RID: 7338 RVA: 0x00015E9B File Offset: 0x0001409B
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

		// Token: 0x06001CAB RID: 7339 RVA: 0x00070EC0 File Offset: 0x0006F0C0
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

		public Image iconImage;

		public Slider slider;

		private bool _showIcon;

		private bool _showSlider;
	}
}
