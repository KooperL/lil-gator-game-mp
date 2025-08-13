using System;
using System.Collections.Generic;
using Rewired.Integration.UnityUI;
using Rewired.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200042B RID: 1067
	[AddComponentMenu("")]
	public class CalibrationWindow : Window
	{
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x000122B5 File Offset: 0x000104B5
		private bool axisSelected
		{
			get
			{
				return this.joystick != null && this.selectedAxis >= 0 && this.selectedAxis < this.joystick.calibrationMap.axisCount;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x000122E5 File Offset: 0x000104E5
		private AxisCalibration axisCalibration
		{
			get
			{
				if (!this.axisSelected)
				{
					return null;
				}
				return this.joystick.calibrationMap.GetAxis(this.selectedAxis);
			}
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00064324 File Offset: 0x00062524
		public override void Initialize(int id, Func<int, bool> isFocusedCallback)
		{
			if (this.rightContentContainer == null || this.valueDisplayGroup == null || this.calibratedValueMarker == null || this.rawValueMarker == null || this.calibratedZeroMarker == null || this.deadzoneArea == null || this.deadzoneSlider == null || this.sensitivitySlider == null || this.zeroSlider == null || this.invertToggle == null || this.axisScrollAreaContent == null || this.doneButton == null || this.calibrateButton == null || this.axisButtonPrefab == null || this.doneButtonLabel == null || this.cancelButtonLabel == null || this.defaultButtonLabel == null || this.deadzoneSliderLabel == null || this.zeroSliderLabel == null || this.sensitivitySliderLabel == null || this.invertToggleLabel == null || this.calibrateButtonLabel == null)
			{
				Debug.LogError("Rewired Control Mapper: All inspector values must be assigned!");
				return;
			}
			this.axisButtons = new List<Button>();
			this.buttonCallbacks = new Dictionary<int, Action<int>>();
			this.doneButtonLabel.text = ControlMapper.GetLanguage().done;
			this.cancelButtonLabel.text = ControlMapper.GetLanguage().cancel;
			this.defaultButtonLabel.text = ControlMapper.GetLanguage().default_;
			this.deadzoneSliderLabel.text = ControlMapper.GetLanguage().calibrateWindow_deadZoneSliderLabel;
			this.zeroSliderLabel.text = ControlMapper.GetLanguage().calibrateWindow_zeroSliderLabel;
			this.sensitivitySliderLabel.text = ControlMapper.GetLanguage().calibrateWindow_sensitivitySliderLabel;
			this.invertToggleLabel.text = ControlMapper.GetLanguage().calibrateWindow_invertToggleLabel;
			this.calibrateButtonLabel.text = ControlMapper.GetLanguage().calibrateWindow_calibrateButtonLabel;
			base.Initialize(id, isFocusedCallback);
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x0006455C File Offset: 0x0006275C
		public void SetJoystick(int playerId, Joystick joystick)
		{
			if (!base.initialized)
			{
				return;
			}
			this.playerId = playerId;
			this.joystick = joystick;
			if (joystick == null)
			{
				Debug.LogError("Rewired Control Mapper: Joystick cannot be null!");
				return;
			}
			float num = 0f;
			for (int i = 0; i < joystick.axisCount; i++)
			{
				int index = i;
				GameObject gameObject = UITools.InstantiateGUIObject<Button>(this.axisButtonPrefab, this.axisScrollAreaContent, "Axis" + i.ToString());
				Button button = gameObject.GetComponent<Button>();
				button.onClick.AddListener(delegate
				{
					this.OnAxisSelected(index, button);
				});
				Text componentInSelfOrChildren = UnityTools.GetComponentInSelfOrChildren<Text>(gameObject);
				if (componentInSelfOrChildren != null)
				{
					componentInSelfOrChildren.text = ControlMapper.GetLanguage().GetElementIdentifierName(joystick, joystick.AxisElementIdentifiers[i].id, 0);
				}
				if (num == 0f)
				{
					num = UnityTools.GetComponentInSelfOrChildren<LayoutElement>(gameObject).minHeight;
				}
				this.axisButtons.Add(button);
			}
			float spacing = this.axisScrollAreaContent.GetComponent<VerticalLayoutGroup>().spacing;
			this.axisScrollAreaContent.sizeDelta = new Vector2(this.axisScrollAreaContent.sizeDelta.x, Mathf.Max((float)joystick.axisCount * (num + spacing) - spacing, this.axisScrollAreaContent.sizeDelta.y));
			this.origCalibrationData = joystick.calibrationMap.ToXmlString();
			this.displayAreaWidth = this.rightContentContainer.sizeDelta.x;
			this.rewiredStandaloneInputModule = base.gameObject.transform.root.GetComponentInChildren<RewiredStandaloneInputModule>();
			if (this.rewiredStandaloneInputModule != null)
			{
				this.menuHorizActionId = ReInput.mapping.GetActionId(this.rewiredStandaloneInputModule.horizontalAxis);
				this.menuVertActionId = ReInput.mapping.GetActionId(this.rewiredStandaloneInputModule.verticalAxis);
			}
			if (joystick.axisCount > 0)
			{
				this.SelectAxis(0);
			}
			base.defaultUIElement = this.doneButton.gameObject;
			this.RefreshControls();
			this.Redraw();
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x00012307 File Offset: 0x00010507
		public void SetButtonCallback(CalibrationWindow.ButtonIdentifier buttonIdentifier, Action<int> callback)
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

		// Token: 0x060017A8 RID: 6056 RVA: 0x00064770 File Offset: 0x00062970
		public override void Cancel()
		{
			if (!base.initialized)
			{
				return;
			}
			if (this.joystick != null)
			{
				this.joystick.ImportCalibrationMapFromXmlString(this.origCalibrationData);
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

		// Token: 0x060017A9 RID: 6057 RVA: 0x0001233F File Offset: 0x0001053F
		protected override void Update()
		{
			if (!base.initialized)
			{
				return;
			}
			base.Update();
			this.UpdateDisplay();
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x000647D0 File Offset: 0x000629D0
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

		// Token: 0x060017AB RID: 6059 RVA: 0x00012356 File Offset: 0x00010556
		public void OnCancel()
		{
			this.Cancel();
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x0001235E File Offset: 0x0001055E
		public void OnRestoreDefault()
		{
			if (!base.initialized)
			{
				return;
			}
			if (this.joystick == null)
			{
				return;
			}
			this.joystick.calibrationMap.Reset();
			this.RefreshControls();
			this.Redraw();
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x00064804 File Offset: 0x00062A04
		public void OnCalibrate()
		{
			if (!base.initialized)
			{
				return;
			}
			Action<int> action;
			if (!this.buttonCallbacks.TryGetValue(3, out action))
			{
				return;
			}
			action(this.selectedAxis);
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0001238E File Offset: 0x0001058E
		public void OnInvert(bool state)
		{
			if (!base.initialized)
			{
				return;
			}
			if (!this.axisSelected)
			{
				return;
			}
			this.axisCalibration.invert = state;
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x000123AE File Offset: 0x000105AE
		public void OnZeroValueChange(float value)
		{
			if (!base.initialized)
			{
				return;
			}
			if (!this.axisSelected)
			{
				return;
			}
			this.axisCalibration.calibratedZero = value;
			this.RedrawCalibratedZero();
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x000123D4 File Offset: 0x000105D4
		public void OnZeroCancel()
		{
			if (!base.initialized)
			{
				return;
			}
			if (!this.axisSelected)
			{
				return;
			}
			this.axisCalibration.calibratedZero = this.origSelectedAxisCalibrationData.zero;
			this.RedrawCalibratedZero();
			this.RefreshControls();
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x00064838 File Offset: 0x00062A38
		public void OnDeadzoneValueChange(float value)
		{
			if (!base.initialized)
			{
				return;
			}
			if (!this.axisSelected)
			{
				return;
			}
			this.axisCalibration.deadZone = Mathf.Clamp(value, 0f, 0.8f);
			if (value > 0.8f)
			{
				this.deadzoneSlider.value = 0.8f;
			}
			this.RedrawDeadzone();
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0001240A File Offset: 0x0001060A
		public void OnDeadzoneCancel()
		{
			if (!base.initialized)
			{
				return;
			}
			if (!this.axisSelected)
			{
				return;
			}
			this.axisCalibration.deadZone = this.origSelectedAxisCalibrationData.deadZone;
			this.RedrawDeadzone();
			this.RefreshControls();
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x00012440 File Offset: 0x00010640
		public void OnSensitivityValueChange(float value)
		{
			if (!base.initialized)
			{
				return;
			}
			if (!this.axisSelected)
			{
				return;
			}
			this.SetSensitivity(this.axisCalibration, value);
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x00012461 File Offset: 0x00010661
		public void OnSensitivityCancel(float value)
		{
			if (!base.initialized)
			{
				return;
			}
			if (!this.axisSelected)
			{
				return;
			}
			this.axisCalibration.sensitivity = this.origSelectedAxisCalibrationData.sensitivity;
			this.RefreshControls();
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x00012491 File Offset: 0x00010691
		public void OnAxisScrollRectScroll(Vector2 pos)
		{
			bool initialized = base.initialized;
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x0001249A File Offset: 0x0001069A
		private void OnAxisSelected(int axisIndex, Button button)
		{
			if (!base.initialized)
			{
				return;
			}
			if (this.joystick == null)
			{
				return;
			}
			this.SelectAxis(axisIndex);
			this.RefreshControls();
			this.Redraw();
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x000124C1 File Offset: 0x000106C1
		private void UpdateDisplay()
		{
			this.RedrawValueMarkers();
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x000124C9 File Offset: 0x000106C9
		private void Redraw()
		{
			this.RedrawCalibratedZero();
			this.RedrawValueMarkers();
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x00064890 File Offset: 0x00062A90
		private void RefreshControls()
		{
			if (!this.axisSelected)
			{
				this.deadzoneSlider.value = 0f;
				this.zeroSlider.value = 0f;
				this.sensitivitySlider.value = 0f;
				this.invertToggle.isOn = false;
				return;
			}
			this.deadzoneSlider.value = this.axisCalibration.deadZone;
			this.zeroSlider.value = this.axisCalibration.calibratedZero;
			this.sensitivitySlider.value = this.GetSliderSensitivity(this.axisCalibration);
			this.invertToggle.isOn = this.axisCalibration.invert;
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x0006493C File Offset: 0x00062B3C
		private void RedrawDeadzone()
		{
			if (!this.axisSelected)
			{
				return;
			}
			float num = this.displayAreaWidth * this.axisCalibration.deadZone;
			this.deadzoneArea.sizeDelta = new Vector2(num, this.deadzoneArea.sizeDelta.y);
			this.deadzoneArea.anchoredPosition = new Vector2(this.axisCalibration.calibratedZero * -this.deadzoneArea.parent.localPosition.x, this.deadzoneArea.anchoredPosition.y);
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x000649C8 File Offset: 0x00062BC8
		private void RedrawCalibratedZero()
		{
			if (!this.axisSelected)
			{
				return;
			}
			this.calibratedZeroMarker.anchoredPosition = new Vector2(this.axisCalibration.calibratedZero * -this.deadzoneArea.parent.localPosition.x, this.calibratedZeroMarker.anchoredPosition.y);
			this.RedrawDeadzone();
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x00064A28 File Offset: 0x00062C28
		private void RedrawValueMarkers()
		{
			if (!this.axisSelected)
			{
				this.calibratedValueMarker.anchoredPosition = new Vector2(0f, this.calibratedValueMarker.anchoredPosition.y);
				this.rawValueMarker.anchoredPosition = new Vector2(0f, this.rawValueMarker.anchoredPosition.y);
				return;
			}
			float axis = this.joystick.GetAxis(this.selectedAxis);
			float num = Mathf.Clamp(this.joystick.GetAxisRaw(this.selectedAxis), -1f, 1f);
			this.calibratedValueMarker.anchoredPosition = new Vector2(this.displayAreaWidth * 0.5f * axis, this.calibratedValueMarker.anchoredPosition.y);
			this.rawValueMarker.anchoredPosition = new Vector2(this.displayAreaWidth * 0.5f * num, this.rawValueMarker.anchoredPosition.y);
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x00064B18 File Offset: 0x00062D18
		private void SelectAxis(int index)
		{
			if (index < 0 || index >= this.axisButtons.Count)
			{
				return;
			}
			if (this.axisButtons[index] == null)
			{
				return;
			}
			this.axisButtons[index].interactable = false;
			this.axisButtons[index].Select();
			for (int i = 0; i < this.axisButtons.Count; i++)
			{
				if (i != index)
				{
					this.axisButtons[i].interactable = true;
				}
			}
			this.selectedAxis = index;
			this.origSelectedAxisCalibrationData = this.axisCalibration.GetData();
			this.SetMinSensitivity();
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x000124D7 File Offset: 0x000106D7
		public override void TakeInputFocus()
		{
			base.TakeInputFocus();
			if (this.selectedAxis >= 0)
			{
				this.SelectAxis(this.selectedAxis);
			}
			this.RefreshControls();
			this.Redraw();
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x00064BBC File Offset: 0x00062DBC
		private void SetMinSensitivity()
		{
			if (!this.axisSelected)
			{
				return;
			}
			this.minSensitivity = 0.1f;
			if (this.rewiredStandaloneInputModule != null)
			{
				if (this.IsMenuAxis(this.menuHorizActionId, this.selectedAxis))
				{
					this.GetAxisButtonDeadZone(this.playerId, this.menuHorizActionId, ref this.minSensitivity);
					return;
				}
				if (this.IsMenuAxis(this.menuVertActionId, this.selectedAxis))
				{
					this.GetAxisButtonDeadZone(this.playerId, this.menuVertActionId, ref this.minSensitivity);
				}
			}
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x00064C44 File Offset: 0x00062E44
		private bool IsMenuAxis(int actionId, int axisIndex)
		{
			if (this.rewiredStandaloneInputModule == null)
			{
				return false;
			}
			IList<Player> allPlayers = ReInput.players.AllPlayers;
			int count = allPlayers.Count;
			for (int i = 0; i < count; i++)
			{
				IList<JoystickMap> maps = allPlayers[i].controllers.maps.GetMaps<JoystickMap>(this.joystick.id);
				if (maps != null)
				{
					int count2 = maps.Count;
					for (int j = 0; j < count2; j++)
					{
						IList<ActionElementMap> axisMaps = maps[j].AxisMaps;
						if (axisMaps != null)
						{
							int count3 = axisMaps.Count;
							for (int k = 0; k < count3; k++)
							{
								ActionElementMap actionElementMap = axisMaps[k];
								if (actionElementMap.actionId == actionId && actionElementMap.elementIndex == axisIndex)
								{
									return true;
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x00064D14 File Offset: 0x00062F14
		private void GetAxisButtonDeadZone(int playerId, int actionId, ref float value)
		{
			InputAction action = ReInput.mapping.GetAction(actionId);
			if (action == null)
			{
				return;
			}
			int behaviorId = action.behaviorId;
			InputBehavior inputBehavior = ReInput.mapping.GetInputBehavior(playerId, behaviorId);
			if (inputBehavior == null)
			{
				return;
			}
			value = inputBehavior.buttonDeadZone + 0.1f;
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x00012500 File Offset: 0x00010700
		private float GetSliderSensitivity(AxisCalibration axisCalibration)
		{
			if (axisCalibration.sensitivityType == null)
			{
				return axisCalibration.sensitivity;
			}
			if (axisCalibration.sensitivityType == 1)
			{
				return CalibrationWindow.ProcessPowerValue(axisCalibration.sensitivity, 0f, this.sensitivitySlider.maxValue);
			}
			return axisCalibration.sensitivity;
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x00064D58 File Offset: 0x00062F58
		public void SetSensitivity(AxisCalibration axisCalibration, float sliderValue)
		{
			if (axisCalibration.sensitivityType == null)
			{
				axisCalibration.sensitivity = Mathf.Clamp(sliderValue, this.minSensitivity, float.PositiveInfinity);
				if (sliderValue < this.minSensitivity)
				{
					this.sensitivitySlider.value = this.minSensitivity;
					return;
				}
			}
			else
			{
				if (axisCalibration.sensitivityType == 1)
				{
					axisCalibration.sensitivity = CalibrationWindow.ProcessPowerValue(sliderValue, 0f, this.sensitivitySlider.maxValue);
					return;
				}
				axisCalibration.sensitivity = sliderValue;
			}
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00064DCC File Offset: 0x00062FCC
		private static float ProcessPowerValue(float value, float minValue, float maxValue)
		{
			value = Mathf.Clamp(value, minValue, maxValue);
			if (value > 1f)
			{
				value = MathTools.ValueInNewRange(value, 1f, maxValue, 1f, 0f);
			}
			else if (value < 1f)
			{
				value = MathTools.ValueInNewRange(value, 0f, 1f, maxValue, 1f);
			}
			return value;
		}

		// Token: 0x04001B8C RID: 7052
		private const float minSensitivityOtherAxes = 0.1f;

		// Token: 0x04001B8D RID: 7053
		private const float maxDeadzone = 0.8f;

		// Token: 0x04001B8E RID: 7054
		[SerializeField]
		private RectTransform rightContentContainer;

		// Token: 0x04001B8F RID: 7055
		[SerializeField]
		private RectTransform valueDisplayGroup;

		// Token: 0x04001B90 RID: 7056
		[SerializeField]
		private RectTransform calibratedValueMarker;

		// Token: 0x04001B91 RID: 7057
		[SerializeField]
		private RectTransform rawValueMarker;

		// Token: 0x04001B92 RID: 7058
		[SerializeField]
		private RectTransform calibratedZeroMarker;

		// Token: 0x04001B93 RID: 7059
		[SerializeField]
		private RectTransform deadzoneArea;

		// Token: 0x04001B94 RID: 7060
		[SerializeField]
		private Slider deadzoneSlider;

		// Token: 0x04001B95 RID: 7061
		[SerializeField]
		private Slider zeroSlider;

		// Token: 0x04001B96 RID: 7062
		[SerializeField]
		private Slider sensitivitySlider;

		// Token: 0x04001B97 RID: 7063
		[SerializeField]
		private Toggle invertToggle;

		// Token: 0x04001B98 RID: 7064
		[SerializeField]
		private RectTransform axisScrollAreaContent;

		// Token: 0x04001B99 RID: 7065
		[SerializeField]
		private Button doneButton;

		// Token: 0x04001B9A RID: 7066
		[SerializeField]
		private Button calibrateButton;

		// Token: 0x04001B9B RID: 7067
		[SerializeField]
		private Text doneButtonLabel;

		// Token: 0x04001B9C RID: 7068
		[SerializeField]
		private Text cancelButtonLabel;

		// Token: 0x04001B9D RID: 7069
		[SerializeField]
		private Text defaultButtonLabel;

		// Token: 0x04001B9E RID: 7070
		[SerializeField]
		private Text deadzoneSliderLabel;

		// Token: 0x04001B9F RID: 7071
		[SerializeField]
		private Text zeroSliderLabel;

		// Token: 0x04001BA0 RID: 7072
		[SerializeField]
		private Text sensitivitySliderLabel;

		// Token: 0x04001BA1 RID: 7073
		[SerializeField]
		private Text invertToggleLabel;

		// Token: 0x04001BA2 RID: 7074
		[SerializeField]
		private Text calibrateButtonLabel;

		// Token: 0x04001BA3 RID: 7075
		[SerializeField]
		private GameObject axisButtonPrefab;

		// Token: 0x04001BA4 RID: 7076
		private Joystick joystick;

		// Token: 0x04001BA5 RID: 7077
		private string origCalibrationData;

		// Token: 0x04001BA6 RID: 7078
		private int selectedAxis = -1;

		// Token: 0x04001BA7 RID: 7079
		private AxisCalibrationData origSelectedAxisCalibrationData;

		// Token: 0x04001BA8 RID: 7080
		private float displayAreaWidth;

		// Token: 0x04001BA9 RID: 7081
		private List<Button> axisButtons;

		// Token: 0x04001BAA RID: 7082
		private Dictionary<int, Action<int>> buttonCallbacks;

		// Token: 0x04001BAB RID: 7083
		private int playerId;

		// Token: 0x04001BAC RID: 7084
		private RewiredStandaloneInputModule rewiredStandaloneInputModule;

		// Token: 0x04001BAD RID: 7085
		private int menuHorizActionId = -1;

		// Token: 0x04001BAE RID: 7086
		private int menuVertActionId = -1;

		// Token: 0x04001BAF RID: 7087
		private float minSensitivity;

		// Token: 0x0200042C RID: 1068
		public enum ButtonIdentifier
		{
			// Token: 0x04001BB1 RID: 7089
			Done,
			// Token: 0x04001BB2 RID: 7090
			Cancel,
			// Token: 0x04001BB3 RID: 7091
			Default,
			// Token: 0x04001BB4 RID: 7092
			Calibrate
		}
	}
}
