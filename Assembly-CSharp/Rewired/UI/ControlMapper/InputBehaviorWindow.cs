using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class InputBehaviorWindow : Window
	{
		// Token: 0x06001B1C RID: 6940 RVA: 0x0006ED54 File Offset: 0x0006CF54
		public override void Initialize(int id, Func<int, bool> isFocusedCallback)
		{
			if (this.spawnTransform == null || this.doneButton == null || this.cancelButton == null || this.defaultButton == null || this.uiControlSetPrefab == null || this.uiSliderControlPrefab == null || this.doneButtonLabel == null || this.cancelButtonLabel == null || this.defaultButtonLabel == null)
			{
				Debug.LogError("Rewired Control Mapper: All inspector values must be assigned!");
				return;
			}
			this.inputBehaviorInfo = new List<InputBehaviorWindow.InputBehaviorInfo>();
			this.buttonCallbacks = new Dictionary<int, Action<int>>();
			this.doneButtonLabel.text = ControlMapper.GetLanguage().done;
			this.cancelButtonLabel.text = ControlMapper.GetLanguage().cancel;
			this.defaultButtonLabel.text = ControlMapper.GetLanguage().default_;
			base.Initialize(id, isFocusedCallback);
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x0006EE48 File Offset: 0x0006D048
		public void SetData(int playerId, ControlMapper.InputBehaviorSettings[] data)
		{
			if (!base.initialized)
			{
				return;
			}
			this.playerId = playerId;
			foreach (ControlMapper.InputBehaviorSettings inputBehaviorSettings in data)
			{
				if (inputBehaviorSettings != null && inputBehaviorSettings.isValid)
				{
					InputBehavior inputBehavior = this.GetInputBehavior(inputBehaviorSettings.inputBehaviorId);
					if (inputBehavior != null)
					{
						UIControlSet uicontrolSet = this.CreateControlSet();
						Dictionary<int, InputBehaviorWindow.PropertyType> dictionary = new Dictionary<int, InputBehaviorWindow.PropertyType>();
						string customEntry = ControlMapper.GetLanguage().GetCustomEntry(inputBehaviorSettings.labelLanguageKey);
						if (!string.IsNullOrEmpty(customEntry))
						{
							uicontrolSet.SetTitle(customEntry);
						}
						else
						{
							uicontrolSet.SetTitle(inputBehavior.name);
						}
						if (inputBehaviorSettings.showJoystickAxisSensitivity)
						{
							UISliderControl uisliderControl = this.CreateSlider(uicontrolSet, inputBehavior.id, null, ControlMapper.GetLanguage().GetCustomEntry(inputBehaviorSettings.joystickAxisSensitivityLabelLanguageKey), inputBehaviorSettings.joystickAxisSensitivityIcon, inputBehaviorSettings.joystickAxisSensitivityMin, inputBehaviorSettings.joystickAxisSensitivityMax, new Action<int, int, float>(this.JoystickAxisSensitivityValueChanged), new Action<int, int>(this.JoystickAxisSensitivityCanceled));
							uisliderControl.slider.value = Mathf.Clamp(inputBehavior.joystickAxisSensitivity, inputBehaviorSettings.joystickAxisSensitivityMin, inputBehaviorSettings.joystickAxisSensitivityMax);
							dictionary.Add(uisliderControl.id, InputBehaviorWindow.PropertyType.JoystickAxisSensitivity);
						}
						if (inputBehaviorSettings.showMouseXYAxisSensitivity)
						{
							UISliderControl uisliderControl2 = this.CreateSlider(uicontrolSet, inputBehavior.id, null, ControlMapper.GetLanguage().GetCustomEntry(inputBehaviorSettings.mouseXYAxisSensitivityLabelLanguageKey), inputBehaviorSettings.mouseXYAxisSensitivityIcon, inputBehaviorSettings.mouseXYAxisSensitivityMin, inputBehaviorSettings.mouseXYAxisSensitivityMax, new Action<int, int, float>(this.MouseXYAxisSensitivityValueChanged), new Action<int, int>(this.MouseXYAxisSensitivityCanceled));
							uisliderControl2.slider.value = Mathf.Clamp(inputBehavior.mouseXYAxisSensitivity, inputBehaviorSettings.mouseXYAxisSensitivityMin, inputBehaviorSettings.mouseXYAxisSensitivityMax);
							dictionary.Add(uisliderControl2.id, InputBehaviorWindow.PropertyType.MouseXYAxisSensitivity);
						}
						this.inputBehaviorInfo.Add(new InputBehaviorWindow.InputBehaviorInfo(inputBehavior, uicontrolSet, dictionary));
					}
				}
			}
			base.defaultUIElement = this.doneButton.gameObject;
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x00014E39 File Offset: 0x00013039
		public void SetButtonCallback(InputBehaviorWindow.ButtonIdentifier buttonIdentifier, Action<int> callback)
		{
			if (!base.initialized)
			{
				return;
			}
			if (callback == null)
			{
				return;
			}
			if (this.buttonCallbacks.ContainsKey((int)buttonIdentifier))
			{
				this.buttonCallbacks[(int)buttonIdentifier] = callback;
				return;
			}
			this.buttonCallbacks.Add((int)buttonIdentifier, callback);
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0006F010 File Offset: 0x0006D210
		public override void Cancel()
		{
			if (!base.initialized)
			{
				return;
			}
			foreach (InputBehaviorWindow.InputBehaviorInfo inputBehaviorInfo in this.inputBehaviorInfo)
			{
				inputBehaviorInfo.RestorePreviousData();
			}
			Action<int> action;
			if (!this.buttonCallbacks.TryGetValue(1, out action))
			{
				if (this.cancelCallback != null)
				{
					this.cancelCallback();
				}
				return;
			}
			action(base.id);
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x0006F09C File Offset: 0x0006D29C
		public void OnDone()
		{
			if (!base.initialized)
			{
				return;
			}
			Action<int> action;
			if (!this.buttonCallbacks.TryGetValue(0, out action))
			{
				return;
			}
			action(base.id);
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x00012753 File Offset: 0x00010953
		public void OnCancel()
		{
			this.Cancel();
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x0006F0D0 File Offset: 0x0006D2D0
		public void OnRestoreDefault()
		{
			if (!base.initialized)
			{
				return;
			}
			foreach (InputBehaviorWindow.InputBehaviorInfo inputBehaviorInfo in this.inputBehaviorInfo)
			{
				inputBehaviorInfo.RestoreDefaultData();
			}
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x00014E71 File Offset: 0x00013071
		private void JoystickAxisSensitivityValueChanged(int inputBehaviorId, int controlId, float value)
		{
			this.GetInputBehavior(inputBehaviorId).joystickAxisSensitivity = value;
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x00014E80 File Offset: 0x00013080
		private void MouseXYAxisSensitivityValueChanged(int inputBehaviorId, int controlId, float value)
		{
			this.GetInputBehavior(inputBehaviorId).mouseXYAxisSensitivity = value;
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x0006F12C File Offset: 0x0006D32C
		private void JoystickAxisSensitivityCanceled(int inputBehaviorId, int controlId)
		{
			InputBehaviorWindow.InputBehaviorInfo inputBehaviorInfo = this.GetInputBehaviorInfo(inputBehaviorId);
			if (inputBehaviorInfo == null)
			{
				return;
			}
			inputBehaviorInfo.RestoreData(InputBehaviorWindow.PropertyType.JoystickAxisSensitivity, controlId);
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x0006F150 File Offset: 0x0006D350
		private void MouseXYAxisSensitivityCanceled(int inputBehaviorId, int controlId)
		{
			InputBehaviorWindow.InputBehaviorInfo inputBehaviorInfo = this.GetInputBehaviorInfo(inputBehaviorId);
			if (inputBehaviorInfo == null)
			{
				return;
			}
			inputBehaviorInfo.RestoreData(InputBehaviorWindow.PropertyType.MouseXYAxisSensitivity, controlId);
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x00014E8F File Offset: 0x0001308F
		public override void TakeInputFocus()
		{
			base.TakeInputFocus();
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x00014E97 File Offset: 0x00013097
		private UIControlSet CreateControlSet()
		{
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.uiControlSetPrefab);
			gameObject.transform.SetParent(this.spawnTransform, false);
			return gameObject.GetComponent<UIControlSet>();
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x0006F174 File Offset: 0x0006D374
		private UISliderControl CreateSlider(UIControlSet set, int inputBehaviorId, string defaultTitle, string overrideTitle, Sprite icon, float minValue, float maxValue, Action<int, int, float> valueChangedCallback, Action<int, int> cancelCallback)
		{
			UISliderControl uisliderControl = set.CreateSlider(this.uiSliderControlPrefab, icon, minValue, maxValue, delegate(int cId, float value)
			{
				valueChangedCallback(inputBehaviorId, cId, value);
			}, delegate(int cId)
			{
				cancelCallback(inputBehaviorId, cId);
			});
			string text = (string.IsNullOrEmpty(overrideTitle) ? defaultTitle : overrideTitle);
			if (!string.IsNullOrEmpty(text))
			{
				uisliderControl.showTitle = true;
				uisliderControl.title.text = text;
			}
			else
			{
				uisliderControl.showTitle = false;
			}
			uisliderControl.showIcon = icon != null;
			return uisliderControl;
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x00014EBB File Offset: 0x000130BB
		private InputBehavior GetInputBehavior(int id)
		{
			return ReInput.mapping.GetInputBehavior(this.playerId, id);
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x0006F20C File Offset: 0x0006D40C
		private InputBehaviorWindow.InputBehaviorInfo GetInputBehaviorInfo(int inputBehaviorId)
		{
			int count = this.inputBehaviorInfo.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.inputBehaviorInfo[i].inputBehavior.id == inputBehaviorId)
				{
					return this.inputBehaviorInfo[i];
				}
			}
			return null;
		}

		private const float minSensitivity = 0.1f;

		[SerializeField]
		private RectTransform spawnTransform;

		[SerializeField]
		private Button doneButton;

		[SerializeField]
		private Button cancelButton;

		[SerializeField]
		private Button defaultButton;

		[SerializeField]
		private Text doneButtonLabel;

		[SerializeField]
		private Text cancelButtonLabel;

		[SerializeField]
		private Text defaultButtonLabel;

		[SerializeField]
		private GameObject uiControlSetPrefab;

		[SerializeField]
		private GameObject uiSliderControlPrefab;

		private List<InputBehaviorWindow.InputBehaviorInfo> inputBehaviorInfo;

		private Dictionary<int, Action<int>> buttonCallbacks;

		private int playerId;

		private class InputBehaviorInfo
		{
			// (get) Token: 0x06001B2D RID: 6957 RVA: 0x00014ED6 File Offset: 0x000130D6
			public InputBehavior inputBehavior
			{
				get
				{
					return this._inputBehavior;
				}
			}

			// (get) Token: 0x06001B2E RID: 6958 RVA: 0x00014EDE File Offset: 0x000130DE
			public UIControlSet controlSet
			{
				get
				{
					return this._controlSet;
				}
			}

			// Token: 0x06001B2F RID: 6959 RVA: 0x00014EE6 File Offset: 0x000130E6
			public InputBehaviorInfo(InputBehavior inputBehavior, UIControlSet controlSet, Dictionary<int, InputBehaviorWindow.PropertyType> idToProperty)
			{
				this._inputBehavior = inputBehavior;
				this._controlSet = controlSet;
				this.idToProperty = idToProperty;
				this.copyOfOriginal = new InputBehavior(inputBehavior);
			}

			// Token: 0x06001B30 RID: 6960 RVA: 0x00014F0F File Offset: 0x0001310F
			public void RestorePreviousData()
			{
				this._inputBehavior.ImportData(this.copyOfOriginal);
			}

			// Token: 0x06001B31 RID: 6961 RVA: 0x00014F23 File Offset: 0x00013123
			public void RestoreDefaultData()
			{
				this._inputBehavior.Reset();
				this.RefreshControls();
			}

			// Token: 0x06001B32 RID: 6962 RVA: 0x0006F258 File Offset: 0x0006D458
			public void RestoreData(InputBehaviorWindow.PropertyType propertyType, int controlId)
			{
				if (propertyType != InputBehaviorWindow.PropertyType.JoystickAxisSensitivity)
				{
					if (propertyType != InputBehaviorWindow.PropertyType.MouseXYAxisSensitivity)
					{
						return;
					}
					float mouseXYAxisSensitivity = this.copyOfOriginal.mouseXYAxisSensitivity;
					this._inputBehavior.mouseXYAxisSensitivity = mouseXYAxisSensitivity;
					UISliderControl control = this._controlSet.GetControl<UISliderControl>(controlId);
					if (control != null)
					{
						control.slider.value = mouseXYAxisSensitivity;
					}
				}
				else
				{
					float joystickAxisSensitivity = this.copyOfOriginal.joystickAxisSensitivity;
					this._inputBehavior.joystickAxisSensitivity = joystickAxisSensitivity;
					UISliderControl control2 = this._controlSet.GetControl<UISliderControl>(controlId);
					if (control2 != null)
					{
						control2.slider.value = joystickAxisSensitivity;
						return;
					}
				}
			}

			// Token: 0x06001B33 RID: 6963 RVA: 0x0006F2E4 File Offset: 0x0006D4E4
			public void RefreshControls()
			{
				if (this._controlSet == null)
				{
					return;
				}
				if (this.idToProperty == null)
				{
					return;
				}
				foreach (KeyValuePair<int, InputBehaviorWindow.PropertyType> keyValuePair in this.idToProperty)
				{
					UISliderControl control = this._controlSet.GetControl<UISliderControl>(keyValuePair.Key);
					if (!(control == null))
					{
						InputBehaviorWindow.PropertyType value = keyValuePair.Value;
						if (value != InputBehaviorWindow.PropertyType.JoystickAxisSensitivity)
						{
							if (value == InputBehaviorWindow.PropertyType.MouseXYAxisSensitivity)
							{
								control.slider.value = this._inputBehavior.mouseXYAxisSensitivity;
							}
						}
						else
						{
							control.slider.value = this._inputBehavior.joystickAxisSensitivity;
						}
					}
				}
			}

			private InputBehavior _inputBehavior;

			private UIControlSet _controlSet;

			private Dictionary<int, InputBehaviorWindow.PropertyType> idToProperty;

			private InputBehavior copyOfOriginal;
		}

		public enum ButtonIdentifier
		{
			Done,
			Cancel,
			Default
		}

		private enum PropertyType
		{
			JoystickAxisSensitivity,
			MouseXYAxisSensitivity
		}
	}
}
