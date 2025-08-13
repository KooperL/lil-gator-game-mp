using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000479 RID: 1145
	[AddComponentMenu("")]
	public class UIControlSet : MonoBehaviour
	{
		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001C2D RID: 7213 RVA: 0x0006EA80 File Offset: 0x0006CC80
		private Dictionary<int, UIControl> controls
		{
			get
			{
				Dictionary<int, UIControl> dictionary;
				if ((dictionary = this._controls) == null)
				{
					dictionary = (this._controls = new Dictionary<int, UIControl>());
				}
				return dictionary;
			}
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x0001591A File Offset: 0x00013B1A
		public void SetTitle(string text)
		{
			if (this.title == null)
			{
				return;
			}
			this.title.text = text;
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x0006EAA8 File Offset: 0x0006CCA8
		public T GetControl<T>(int uniqueId) where T : UIControl
		{
			UIControl uicontrol;
			this.controls.TryGetValue(uniqueId, out uicontrol);
			return uicontrol as T;
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x0006EAD0 File Offset: 0x0006CCD0
		public UISliderControl CreateSlider(GameObject prefab, Sprite icon, float minValue, float maxValue, Action<int, float> valueChangedCallback, Action<int> cancelCallback)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(prefab);
			UISliderControl control = gameObject.GetComponent<UISliderControl>();
			if (control == null)
			{
				Object.Destroy(gameObject);
				Debug.LogError("Prefab missing UISliderControl component!");
				return null;
			}
			gameObject.transform.SetParent(base.transform, false);
			if (control.iconImage != null)
			{
				control.iconImage.sprite = icon;
			}
			if (control.slider != null)
			{
				control.slider.minValue = minValue;
				control.slider.maxValue = maxValue;
				if (valueChangedCallback != null)
				{
					control.slider.onValueChanged.AddListener(delegate(float value)
					{
						valueChangedCallback(control.id, value);
					});
				}
				if (cancelCallback != null)
				{
					control.SetCancelCallback(delegate
					{
						cancelCallback(control.id);
					});
				}
			}
			this.controls.Add(control.id, control);
			return control;
		}

		// Token: 0x04001DFF RID: 7679
		[SerializeField]
		private Text title;

		// Token: 0x04001E00 RID: 7680
		private Dictionary<int, UIControl> _controls;
	}
}
