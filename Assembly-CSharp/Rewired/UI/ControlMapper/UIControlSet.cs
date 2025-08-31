using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class UIControlSet : MonoBehaviour
	{
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x0005EDDC File Offset: 0x0005CFDC
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

		// Token: 0x060016A7 RID: 5799 RVA: 0x0005EE01 File Offset: 0x0005D001
		public void SetTitle(string text)
		{
			if (this.title == null)
			{
				return;
			}
			this.title.text = text;
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x0005EE20 File Offset: 0x0005D020
		public T GetControl<T>(int uniqueId) where T : UIControl
		{
			UIControl uicontrol;
			this.controls.TryGetValue(uniqueId, out uicontrol);
			return uicontrol as T;
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0005EE48 File Offset: 0x0005D048
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

		[SerializeField]
		private Text title;

		private Dictionary<int, UIControl> _controls;
	}
}
