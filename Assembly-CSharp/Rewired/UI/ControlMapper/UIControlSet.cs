using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class UIControlSet : MonoBehaviour
	{
		// (get) Token: 0x06001C8D RID: 7309 RVA: 0x00070A2C File Offset: 0x0006EC2C
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

		// Token: 0x06001C8E RID: 7310 RVA: 0x00015D50 File Offset: 0x00013F50
		public void SetTitle(string text)
		{
			if (this.title == null)
			{
				return;
			}
			this.title.text = text;
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x00070A54 File Offset: 0x0006EC54
		public T GetControl<T>(int uniqueId) where T : UIControl
		{
			UIControl uicontrol;
			this.controls.TryGetValue(uniqueId, out uicontrol);
			return uicontrol as T;
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x00070A7C File Offset: 0x0006EC7C
		public UISliderControl CreateSlider(GameObject prefab, Sprite icon, float minValue, float maxValue, Action<int, float> valueChangedCallback, Action<int> cancelCallback)
		{
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(prefab);
			UISliderControl control = gameObject.GetComponent<UISliderControl>();
			if (control == null)
			{
				global::UnityEngine.Object.Destroy(gameObject);
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
