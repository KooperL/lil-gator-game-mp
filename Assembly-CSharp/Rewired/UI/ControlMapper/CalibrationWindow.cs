using System;
using System.Collections.Generic;
using Rewired.Integration.UnityUI;
using Rewired.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class CalibrationWindow : Window
	{
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x00054734 File Offset: 0x00052934
		private bool axisSelected
		{
			get
			{
				return this.joystick != null && this.selectedAxis >= 0 && this.selectedAxis < this.joystick.calibrationMap.axisCount;
			}
		}

		// (get) Token: 0x060013DB RID: 5083 RVA: 0x00054764 File Offset: 0x00052964
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

		// Token: 0x060013DC RID: 5084 RVA: 0x00054788 File Offset: 0x00052988
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

		// Token: 0x060013DD RID: 5085 RVA: 0x000549C0 File Offset: 0x00052BC0
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
					componentInSelfOrChildren.text = ControlMapper.GetLanguage().GetElementIdentifierName(joystick, joystick.AxisElementIdentifiers[i].id, AxisRange.Full);
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

		// Token: 0x060013DE RID: 5086 RVA: 0x00054BD3 File Offset: 0x00052DD3
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

		// Token: 0x060013DF RID: 5087 RVA: 0x00054C0C File Offset: 0x00052E0C
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
					this.cancelCallback();
				}
				return;
			}
			action(base.id);
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x00054C6C File Offset: 0x00052E6C
		protected override void Update()
		{
			if (!base.initialized)
			{
				return;
			}
			base.Update();
			this.UpdateDisplay();
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x00054C84 File Offset: 0x00052E84
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

		// Token: 0x060013E2 RID: 5090 RVA: 0x00054CB7 File Offset: 0x00052EB7
		public void OnCancel()
		{
			this.Cancel();
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x00054CBF File Offset: 0x00052EBF
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

		// Token: 0x060013E4 RID: 5092 RVA: 0x00054CF0 File Offset: 0x00052EF0
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

		// Token: 0x060013E5 RID: 5093 RVA: 0x00054D23 File Offset: 0x00052F23
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

		// Token: 0x060013E6 RID: 5094 RVA: 0x00054D43 File Offset: 0x00052F43
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

		// Token: 0x060013E7 RID: 5095 RVA: 0x00054D69 File Offset: 0x00052F69
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

		// Token: 0x060013E8 RID: 5096 RVA: 0x00054DA0 File Offset: 0x00052FA0
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

		// Token: 0x060013E9 RID: 5097 RVA: 0x00054DF8 File Offset: 0x00052FF8
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

		// Token: 0x060013EA RID: 5098 RVA: 0x00054E2E File Offset: 0x0005302E
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

		// Token: 0x060013EB RID: 5099 RVA: 0x00054E4F File Offset: 0x0005304F
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

		// Token: 0x060013EC RID: 5100 RVA: 0x00054E7F File Offset: 0x0005307F
		public void OnAxisScrollRectScroll(Vector2 pos)
		{
			bool initialized = base.initialized;
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x00054E88 File Offset: 0x00053088
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

		// Token: 0x060013EE RID: 5102 RVA: 0x00054EAF File Offset: 0x000530AF
		private void UpdateDisplay()
		{
			this.RedrawValueMarkers();
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x00054EB7 File Offset: 0x000530B7
		private void Redraw()
		{
			this.RedrawCalibratedZero();
			this.RedrawValueMarkers();
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x00054EC8 File Offset: 0x000530C8
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

		// Token: 0x060013F1 RID: 5105 RVA: 0x00054F74 File Offset: 0x00053174
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

		// Token: 0x060013F2 RID: 5106 RVA: 0x00055000 File Offset: 0x00053200
		private void RedrawCalibratedZero()
		{
			if (!this.axisSelected)
			{
				return;
			}
			this.calibratedZeroMarker.anchoredPosition = new Vector2(this.axisCalibration.calibratedZero * -this.deadzoneArea.parent.localPosition.x, this.calibratedZeroMarker.anchoredPosition.y);
			this.RedrawDeadzone();
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x00055060 File Offset: 0x00053260
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

		// Token: 0x060013F4 RID: 5108 RVA: 0x00055150 File Offset: 0x00053350
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

		// Token: 0x060013F5 RID: 5109 RVA: 0x000551F2 File Offset: 0x000533F2
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

		// Token: 0x060013F6 RID: 5110 RVA: 0x0005521C File Offset: 0x0005341C
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

		// Token: 0x060013F7 RID: 5111 RVA: 0x000552A4 File Offset: 0x000534A4
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

		// Token: 0x060013F8 RID: 5112 RVA: 0x00055374 File Offset: 0x00053574
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

		// Token: 0x060013F9 RID: 5113 RVA: 0x000553B7 File Offset: 0x000535B7
		private float GetSliderSensitivity(AxisCalibration axisCalibration)
		{
			if (axisCalibration.sensitivityType == AxisSensitivityType.Multiplier)
			{
				return axisCalibration.sensitivity;
			}
			if (axisCalibration.sensitivityType == AxisSensitivityType.Power)
			{
				return CalibrationWindow.ProcessPowerValue(axisCalibration.sensitivity, 0f, this.sensitivitySlider.maxValue);
			}
			return axisCalibration.sensitivity;
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x000553F4 File Offset: 0x000535F4
		public void SetSensitivity(AxisCalibration axisCalibration, float sliderValue)
		{
			if (axisCalibration.sensitivityType == AxisSensitivityType.Multiplier)
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
				if (axisCalibration.sensitivityType == AxisSensitivityType.Power)
				{
					axisCalibration.sensitivity = CalibrationWindow.ProcessPowerValue(sliderValue, 0f, this.sensitivitySlider.maxValue);
					return;
				}
				axisCalibration.sensitivity = sliderValue;
			}
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x00055468 File Offset: 0x00053668
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

		private const float minSensitivityOtherAxes = 0.1f;

		private const float maxDeadzone = 0.8f;

		[SerializeField]
		private RectTransform rightContentContainer;

		[SerializeField]
		private RectTransform valueDisplayGroup;

		[SerializeField]
		private RectTransform calibratedValueMarker;

		[SerializeField]
		private RectTransform rawValueMarker;

		[SerializeField]
		private RectTransform calibratedZeroMarker;

		[SerializeField]
		private RectTransform deadzoneArea;

		[SerializeField]
		private Slider deadzoneSlider;

		[SerializeField]
		private Slider zeroSlider;

		[SerializeField]
		private Slider sensitivitySlider;

		[SerializeField]
		private Toggle invertToggle;

		[SerializeField]
		private RectTransform axisScrollAreaContent;

		[SerializeField]
		private Button doneButton;

		[SerializeField]
		private Button calibrateButton;

		[SerializeField]
		private Text doneButtonLabel;

		[SerializeField]
		private Text cancelButtonLabel;

		[SerializeField]
		private Text defaultButtonLabel;

		[SerializeField]
		private Text deadzoneSliderLabel;

		[SerializeField]
		private Text zeroSliderLabel;

		[SerializeField]
		private Text sensitivitySliderLabel;

		[SerializeField]
		private Text invertToggleLabel;

		[SerializeField]
		private Text calibrateButtonLabel;

		[SerializeField]
		private GameObject axisButtonPrefab;

		private Joystick joystick;

		private string origCalibrationData;

		private int selectedAxis = -1;

		private AxisCalibrationData origSelectedAxisCalibrationData;

		private float displayAreaWidth;

		private List<Button> axisButtons;

		private Dictionary<int, Action<int>> buttonCallbacks;

		private int playerId;

		private RewiredStandaloneInputModule rewiredStandaloneInputModule;

		private int menuHorizActionId = -1;

		private int menuVertActionId = -1;

		private float minSensitivity;

		public enum ButtonIdentifier
		{
			Done,
			Cancel,
			Default,
			Calibrate
		}
	}
}
