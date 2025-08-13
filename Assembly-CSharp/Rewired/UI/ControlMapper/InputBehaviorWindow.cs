using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200045A RID: 1114
	[AddComponentMenu("")]
	public class InputBehaviorWindow : Window
	{
		// Token: 0x06001ABC RID: 6844 RVA: 0x0006CD2C File Offset: 0x0006AF2C
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

		// Token: 0x06001ABD RID: 6845 RVA: 0x0006CE20 File Offset: 0x0006B020
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

		// Token: 0x06001ABE RID: 6846 RVA: 0x00014A3C File Offset: 0x00012C3C
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

		// Token: 0x06001ABF RID: 6847 RVA: 0x0006CFE8 File Offset: 0x0006B1E8
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
					this.cancelCallback.Invoke();
				}
				return;
			}
			action(base.id);
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x0006D074 File Offset: 0x0006B274
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

		// Token: 0x06001AC1 RID: 6849 RVA: 0x00012356 File Offset: 0x00010556
		public void OnCancel()
		{
			this.Cancel();
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x0006D0A8 File Offset: 0x0006B2A8
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

		// Token: 0x06001AC3 RID: 6851 RVA: 0x00014A74 File Offset: 0x00012C74
		private void JoystickAxisSensitivityValueChanged(int inputBehaviorId, int controlId, float value)
		{
			this.GetInputBehavior(inputBehaviorId).joystickAxisSensitivity = value;
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x00014A83 File Offset: 0x00012C83
		private void MouseXYAxisSensitivityValueChanged(int inputBehaviorId, int controlId, float value)
		{
			this.GetInputBehavior(inputBehaviorId).mouseXYAxisSensitivity = value;
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x0006D104 File Offset: 0x0006B304
		private void JoystickAxisSensitivityCanceled(int inputBehaviorId, int controlId)
		{
			InputBehaviorWindow.InputBehaviorInfo inputBehaviorInfo = this.GetInputBehaviorInfo(inputBehaviorId);
			if (inputBehaviorInfo == null)
			{
				return;
			}
			inputBehaviorInfo.RestoreData(InputBehaviorWindow.PropertyType.JoystickAxisSensitivity, controlId);
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x0006D128 File Offset: 0x0006B328
		private void MouseXYAxisSensitivityCanceled(int inputBehaviorId, int controlId)
		{
			InputBehaviorWindow.InputBehaviorInfo inputBehaviorInfo = this.GetInputBehaviorInfo(inputBehaviorId);
			if (inputBehaviorInfo == null)
			{
				return;
			}
			inputBehaviorInfo.RestoreData(InputBehaviorWindow.PropertyType.MouseXYAxisSensitivity, controlId);
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x00014A92 File Offset: 0x00012C92
		public override void TakeInputFocus()
		{
			base.TakeInputFocus();
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x00014A9A File Offset: 0x00012C9A
		private UIControlSet CreateControlSet()
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.uiControlSetPrefab);
			gameObject.transform.SetParent(this.spawnTransform, false);
			return gameObject.GetComponent<UIControlSet>();
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x0006D14C File Offset: 0x0006B34C
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

		// Token: 0x06001ACA RID: 6858 RVA: 0x00014ABE File Offset: 0x00012CBE
		private InputBehavior GetInputBehavior(int id)
		{
			return ReInput.mapping.GetInputBehavior(this.playerId, id);
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x0006D1E4 File Offset: 0x0006B3E4
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

		// Token: 0x04001D02 RID: 7426
		private const float minSensitivity = 0.1f;

		// Token: 0x04001D03 RID: 7427
		[SerializeField]
		private RectTransform spawnTransform;

		// Token: 0x04001D04 RID: 7428
		[SerializeField]
		private Button doneButton;

		// Token: 0x04001D05 RID: 7429
		[SerializeField]
		private Button cancelButton;

		// Token: 0x04001D06 RID: 7430
		[SerializeField]
		private Button defaultButton;

		// Token: 0x04001D07 RID: 7431
		[SerializeField]
		private Text doneButtonLabel;

		// Token: 0x04001D08 RID: 7432
		[SerializeField]
		private Text cancelButtonLabel;

		// Token: 0x04001D09 RID: 7433
		[SerializeField]
		private Text defaultButtonLabel;

		// Token: 0x04001D0A RID: 7434
		[SerializeField]
		private GameObject uiControlSetPrefab;

		// Token: 0x04001D0B RID: 7435
		[SerializeField]
		private GameObject uiSliderControlPrefab;

		// Token: 0x04001D0C RID: 7436
		private List<InputBehaviorWindow.InputBehaviorInfo> inputBehaviorInfo;

		// Token: 0x04001D0D RID: 7437
		private Dictionary<int, Action<int>> buttonCallbacks;

		// Token: 0x04001D0E RID: 7438
		private int playerId;

		// Token: 0x0200045B RID: 1115
		private class InputBehaviorInfo
		{
			// Token: 0x17000527 RID: 1319
			// (get) Token: 0x06001ACD RID: 6861 RVA: 0x00014AD9 File Offset: 0x00012CD9
			public InputBehavior inputBehavior
			{
				get
				{
					return this._inputBehavior;
				}
			}

			// Token: 0x17000528 RID: 1320
			// (get) Token: 0x06001ACE RID: 6862 RVA: 0x00014AE1 File Offset: 0x00012CE1
			public UIControlSet controlSet
			{
				get
				{
					return this._controlSet;
				}
			}

			// Token: 0x06001ACF RID: 6863 RVA: 0x00014AE9 File Offset: 0x00012CE9
			public InputBehaviorInfo(InputBehavior inputBehavior, UIControlSet controlSet, Dictionary<int, InputBehaviorWindow.PropertyType> idToProperty)
			{
				this._inputBehavior = inputBehavior;
				this._controlSet = controlSet;
				this.idToProperty = idToProperty;
				this.copyOfOriginal = new InputBehavior(inputBehavior);
			}

			// Token: 0x06001AD0 RID: 6864 RVA: 0x00014B12 File Offset: 0x00012D12
			public void RestorePreviousData()
			{
				this._inputBehavior.ImportData(this.copyOfOriginal);
			}

			// Token: 0x06001AD1 RID: 6865 RVA: 0x00014B26 File Offset: 0x00012D26
			public void RestoreDefaultData()
			{
				this._inputBehavior.Reset();
				this.RefreshControls();
			}

			// Token: 0x06001AD2 RID: 6866 RVA: 0x0006D230 File Offset: 0x0006B430
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

			// Token: 0x06001AD3 RID: 6867 RVA: 0x0006D2BC File Offset: 0x0006B4BC
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

			// Token: 0x04001D0F RID: 7439
			private InputBehavior _inputBehavior;

			// Token: 0x04001D10 RID: 7440
			private UIControlSet _controlSet;

			// Token: 0x04001D11 RID: 7441
			private Dictionary<int, InputBehaviorWindow.PropertyType> idToProperty;

			// Token: 0x04001D12 RID: 7442
			private InputBehavior copyOfOriginal;
		}

		// Token: 0x0200045C RID: 1116
		public enum ButtonIdentifier
		{
			// Token: 0x04001D14 RID: 7444
			Done,
			// Token: 0x04001D15 RID: 7445
			Cancel,
			// Token: 0x04001D16 RID: 7446
			Default
		}

		// Token: 0x0200045D RID: 1117
		private enum PropertyType
		{
			// Token: 0x04001D18 RID: 7448
			JoystickAxisSensitivity,
			// Token: 0x04001D19 RID: 7449
			MouseXYAxisSensitivity
		}
	}
}
