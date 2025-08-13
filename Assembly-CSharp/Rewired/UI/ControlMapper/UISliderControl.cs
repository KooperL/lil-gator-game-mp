using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000333 RID: 819
	[AddComponentMenu("")]
	public class UISliderControl : UIControl
	{
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x0005F355 File Offset: 0x0005D555
		// (set) Token: 0x060016BC RID: 5820 RVA: 0x0005F35D File Offset: 0x0005D55D
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

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x0005F386 File Offset: 0x0005D586
		// (set) Token: 0x060016BE RID: 5822 RVA: 0x0005F38E File Offset: 0x0005D58E
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

		// Token: 0x060016BF RID: 5823 RVA: 0x0005F3B8 File Offset: 0x0005D5B8
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
			entry.eventID = EventTriggerType.Cancel;
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

		// Token: 0x040018E5 RID: 6373
		public Image iconImage;

		// Token: 0x040018E6 RID: 6374
		public Slider slider;

		// Token: 0x040018E7 RID: 6375
		private bool _showIcon;

		// Token: 0x040018E8 RID: 6376
		private bool _showSlider;
	}
}
