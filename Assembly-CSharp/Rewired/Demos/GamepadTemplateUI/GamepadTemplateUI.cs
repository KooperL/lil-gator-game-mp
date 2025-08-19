using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rewired.Demos.GamepadTemplateUI
{
	public class GamepadTemplateUI : MonoBehaviour
	{
		// (get) Token: 0x06001E4C RID: 7756 RVA: 0x0001731D File Offset: 0x0001551D
		private Player player
		{
			get
			{
				return ReInput.players.GetPlayer(this.playerId);
			}
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x00077028 File Offset: 0x00075228
		private void Awake()
		{
			this._uiElementsArray = new GamepadTemplateUI.UIElement[]
			{
				new GamepadTemplateUI.UIElement(0, this.leftStickX),
				new GamepadTemplateUI.UIElement(1, this.leftStickY),
				new GamepadTemplateUI.UIElement(17, this.leftStickButton),
				new GamepadTemplateUI.UIElement(2, this.rightStickX),
				new GamepadTemplateUI.UIElement(3, this.rightStickY),
				new GamepadTemplateUI.UIElement(18, this.rightStickButton),
				new GamepadTemplateUI.UIElement(4, this.actionBottomRow1),
				new GamepadTemplateUI.UIElement(5, this.actionBottomRow2),
				new GamepadTemplateUI.UIElement(6, this.actionBottomRow3),
				new GamepadTemplateUI.UIElement(7, this.actionTopRow1),
				new GamepadTemplateUI.UIElement(8, this.actionTopRow2),
				new GamepadTemplateUI.UIElement(9, this.actionTopRow3),
				new GamepadTemplateUI.UIElement(14, this.center1),
				new GamepadTemplateUI.UIElement(15, this.center2),
				new GamepadTemplateUI.UIElement(16, this.center3),
				new GamepadTemplateUI.UIElement(19, this.dPadUp),
				new GamepadTemplateUI.UIElement(20, this.dPadRight),
				new GamepadTemplateUI.UIElement(21, this.dPadDown),
				new GamepadTemplateUI.UIElement(22, this.dPadLeft),
				new GamepadTemplateUI.UIElement(10, this.leftShoulder),
				new GamepadTemplateUI.UIElement(11, this.leftTrigger),
				new GamepadTemplateUI.UIElement(12, this.rightShoulder),
				new GamepadTemplateUI.UIElement(13, this.rightTrigger)
			};
			for (int i = 0; i < this._uiElementsArray.Length; i++)
			{
				this._uiElements.Add(this._uiElementsArray[i].id, this._uiElementsArray[i].element);
			}
			this._sticks = new GamepadTemplateUI.Stick[]
			{
				new GamepadTemplateUI.Stick(this.leftStick, 0, 1),
				new GamepadTemplateUI.Stick(this.rightStick, 2, 3)
			};
			ReInput.ControllerConnectedEvent += this.OnControllerConnected;
			ReInput.ControllerDisconnectedEvent += this.OnControllerDisconnected;
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x0001732F File Offset: 0x0001552F
		private void Start()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			this.DrawLabels();
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x0001733F File Offset: 0x0001553F
		private void OnDestroy()
		{
			ReInput.ControllerConnectedEvent -= this.OnControllerConnected;
			ReInput.ControllerDisconnectedEvent -= this.OnControllerDisconnected;
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x00017363 File Offset: 0x00015563
		private void Update()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			this.DrawActiveElements();
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x00077240 File Offset: 0x00075440
		private void DrawActiveElements()
		{
			for (int i = 0; i < this._uiElementsArray.Length; i++)
			{
				this._uiElementsArray[i].element.Deactivate();
			}
			for (int j = 0; j < this._sticks.Length; j++)
			{
				this._sticks[j].Reset();
			}
			IList<InputAction> actions = ReInput.mapping.Actions;
			for (int k = 0; k < actions.Count; k++)
			{
				this.ActivateElements(this.player, actions[k].id);
			}
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x000772C8 File Offset: 0x000754C8
		private void ActivateElements(Player player, int actionId)
		{
			float axis = player.GetAxis(actionId);
			if (axis == 0f)
			{
				return;
			}
			IList<InputActionSourceData> currentInputSources = player.GetCurrentInputSources(actionId);
			for (int i = 0; i < currentInputSources.Count; i++)
			{
				InputActionSourceData inputActionSourceData = currentInputSources[i];
				IGamepadTemplate template = inputActionSourceData.controller.GetTemplate<IGamepadTemplate>();
				if (template != null)
				{
					template.GetElementTargets(inputActionSourceData.actionElementMap, this._tempTargetList);
					for (int j = 0; j < this._tempTargetList.Count; j++)
					{
						ControllerTemplateElementTarget controllerTemplateElementTarget = this._tempTargetList[j];
						int id = controllerTemplateElementTarget.element.id;
						ControllerUIElement controllerUIElement = this._uiElements[id];
						if (controllerTemplateElementTarget.elementType == ControllerTemplateElementType.Axis)
						{
							controllerUIElement.Activate(axis);
						}
						else if (controllerTemplateElementTarget.elementType == ControllerTemplateElementType.Button && (player.GetButton(actionId) || player.GetNegativeButton(actionId)))
						{
							controllerUIElement.Activate(1f);
						}
						GamepadTemplateUI.Stick stick = this.GetStick(id);
						if (stick != null)
						{
							stick.SetAxisPosition(id, axis * 20f);
						}
					}
				}
			}
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x000773E4 File Offset: 0x000755E4
		private void DrawLabels()
		{
			for (int i = 0; i < this._uiElementsArray.Length; i++)
			{
				this._uiElementsArray[i].element.ClearLabels();
			}
			IList<InputAction> actions = ReInput.mapping.Actions;
			for (int j = 0; j < actions.Count; j++)
			{
				this.DrawLabels(this.player, actions[j]);
			}
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x00077448 File Offset: 0x00075648
		private void DrawLabels(Player player, InputAction action)
		{
			Controller firstControllerWithTemplate = player.controllers.GetFirstControllerWithTemplate<IGamepadTemplate>();
			if (firstControllerWithTemplate == null)
			{
				return;
			}
			IGamepadTemplate template = firstControllerWithTemplate.GetTemplate<IGamepadTemplate>();
			ControllerMap map = player.controllers.maps.GetMap(firstControllerWithTemplate, "Default", "Default");
			if (map == null)
			{
				return;
			}
			for (int i = 0; i < this._uiElementsArray.Length; i++)
			{
				ControllerUIElement element = this._uiElementsArray[i].element;
				int id = this._uiElementsArray[i].id;
				IControllerTemplateElement element2 = template.GetElement(id);
				this.DrawLabel(element, action, map, template, element2);
			}
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x000774D4 File Offset: 0x000756D4
		private void DrawLabel(ControllerUIElement uiElement, InputAction action, ControllerMap controllerMap, IControllerTemplate template, IControllerTemplateElement element)
		{
			if (element.source == null)
			{
				return;
			}
			if (element.source.type == ControllerTemplateElementSourceType.Axis)
			{
				IControllerTemplateAxisSource controllerTemplateAxisSource = element.source as IControllerTemplateAxisSource;
				if (controllerTemplateAxisSource.splitAxis)
				{
					ActionElementMap actionElementMap = controllerMap.GetFirstElementMapWithElementTarget(controllerTemplateAxisSource.positiveTarget, action.id, true);
					if (actionElementMap != null)
					{
						uiElement.SetLabel(actionElementMap.actionDescriptiveName, AxisRange.Positive);
					}
					actionElementMap = controllerMap.GetFirstElementMapWithElementTarget(controllerTemplateAxisSource.negativeTarget, action.id, true);
					if (actionElementMap != null)
					{
						uiElement.SetLabel(actionElementMap.actionDescriptiveName, AxisRange.Negative);
						return;
					}
				}
				else
				{
					ActionElementMap actionElementMap = controllerMap.GetFirstElementMapWithElementTarget(controllerTemplateAxisSource.fullTarget, action.id, true);
					if (actionElementMap != null)
					{
						uiElement.SetLabel(actionElementMap.actionDescriptiveName, AxisRange.Full);
						return;
					}
					ControllerElementTarget controllerElementTarget = new ControllerElementTarget(controllerTemplateAxisSource.fullTarget)
					{
						axisRange = AxisRange.Positive
					};
					actionElementMap = controllerMap.GetFirstElementMapWithElementTarget(controllerElementTarget, action.id, true);
					if (actionElementMap != null)
					{
						uiElement.SetLabel(actionElementMap.actionDescriptiveName, AxisRange.Positive);
					}
					controllerElementTarget = new ControllerElementTarget(controllerTemplateAxisSource.fullTarget)
					{
						axisRange = AxisRange.Negative
					};
					actionElementMap = controllerMap.GetFirstElementMapWithElementTarget(controllerElementTarget, action.id, true);
					if (actionElementMap != null)
					{
						uiElement.SetLabel(actionElementMap.actionDescriptiveName, AxisRange.Negative);
						return;
					}
				}
			}
			else if (element.source.type == ControllerTemplateElementSourceType.Button)
			{
				IControllerTemplateButtonSource controllerTemplateButtonSource = element.source as IControllerTemplateButtonSource;
				ActionElementMap actionElementMap = controllerMap.GetFirstElementMapWithElementTarget(controllerTemplateButtonSource.target, action.id, true);
				if (actionElementMap != null)
				{
					uiElement.SetLabel(actionElementMap.actionDescriptiveName, AxisRange.Full);
				}
			}
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x0007762C File Offset: 0x0007582C
		private GamepadTemplateUI.Stick GetStick(int elementId)
		{
			for (int i = 0; i < this._sticks.Length; i++)
			{
				if (this._sticks[i].ContainsElement(elementId))
				{
					return this._sticks[i];
				}
			}
			return null;
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x00017373 File Offset: 0x00015573
		private void OnControllerConnected(ControllerStatusChangedEventArgs args)
		{
			this.DrawLabels();
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x00017373 File Offset: 0x00015573
		private void OnControllerDisconnected(ControllerStatusChangedEventArgs args)
		{
			this.DrawLabels();
		}

		private const float stickRadius = 20f;

		public int playerId;

		[SerializeField]
		private RectTransform leftStick;

		[SerializeField]
		private RectTransform rightStick;

		[SerializeField]
		private ControllerUIElement leftStickX;

		[SerializeField]
		private ControllerUIElement leftStickY;

		[SerializeField]
		private ControllerUIElement leftStickButton;

		[SerializeField]
		private ControllerUIElement rightStickX;

		[SerializeField]
		private ControllerUIElement rightStickY;

		[SerializeField]
		private ControllerUIElement rightStickButton;

		[SerializeField]
		private ControllerUIElement actionBottomRow1;

		[SerializeField]
		private ControllerUIElement actionBottomRow2;

		[SerializeField]
		private ControllerUIElement actionBottomRow3;

		[SerializeField]
		private ControllerUIElement actionTopRow1;

		[SerializeField]
		private ControllerUIElement actionTopRow2;

		[SerializeField]
		private ControllerUIElement actionTopRow3;

		[SerializeField]
		private ControllerUIElement leftShoulder;

		[SerializeField]
		private ControllerUIElement leftTrigger;

		[SerializeField]
		private ControllerUIElement rightShoulder;

		[SerializeField]
		private ControllerUIElement rightTrigger;

		[SerializeField]
		private ControllerUIElement center1;

		[SerializeField]
		private ControllerUIElement center2;

		[SerializeField]
		private ControllerUIElement center3;

		[SerializeField]
		private ControllerUIElement dPadUp;

		[SerializeField]
		private ControllerUIElement dPadRight;

		[SerializeField]
		private ControllerUIElement dPadDown;

		[SerializeField]
		private ControllerUIElement dPadLeft;

		private GamepadTemplateUI.UIElement[] _uiElementsArray;

		private Dictionary<int, ControllerUIElement> _uiElements = new Dictionary<int, ControllerUIElement>();

		private IList<ControllerTemplateElementTarget> _tempTargetList = new List<ControllerTemplateElementTarget>(2);

		private GamepadTemplateUI.Stick[] _sticks;

		private class Stick
		{
			// (get) Token: 0x06001E5A RID: 7770 RVA: 0x0001739A File Offset: 0x0001559A
			// (set) Token: 0x06001E5B RID: 7771 RVA: 0x000173C6 File Offset: 0x000155C6
			public Vector2 position
			{
				get
				{
					if (!(this._transform != null))
					{
						return Vector2.zero;
					}
					return this._transform.anchoredPosition - this._origPosition;
				}
				set
				{
					if (this._transform == null)
					{
						return;
					}
					this._transform.anchoredPosition = this._origPosition + value;
				}
			}

			// Token: 0x06001E5C RID: 7772 RVA: 0x00077668 File Offset: 0x00075868
			public Stick(RectTransform transform, int xAxisElementId, int yAxisElementId)
			{
				if (transform == null)
				{
					return;
				}
				this._transform = transform;
				this._origPosition = this._transform.anchoredPosition;
				this._xAxisElementId = xAxisElementId;
				this._yAxisElementId = yAxisElementId;
			}

			// Token: 0x06001E5D RID: 7773 RVA: 0x000173EE File Offset: 0x000155EE
			public void Reset()
			{
				if (this._transform == null)
				{
					return;
				}
				this._transform.anchoredPosition = this._origPosition;
			}

			// Token: 0x06001E5E RID: 7774 RVA: 0x00017410 File Offset: 0x00015610
			public bool ContainsElement(int elementId)
			{
				return !(this._transform == null) && (elementId == this._xAxisElementId || elementId == this._yAxisElementId);
			}

			// Token: 0x06001E5F RID: 7775 RVA: 0x000776BC File Offset: 0x000758BC
			public void SetAxisPosition(int elementId, float value)
			{
				if (this._transform == null)
				{
					return;
				}
				Vector2 position = this.position;
				if (elementId == this._xAxisElementId)
				{
					position.x = value;
				}
				else if (elementId == this._yAxisElementId)
				{
					position.y = value;
				}
				this.position = position;
			}

			private RectTransform _transform;

			private Vector2 _origPosition;

			private int _xAxisElementId = -1;

			private int _yAxisElementId = -1;
		}

		private class UIElement
		{
			// Token: 0x06001E60 RID: 7776 RVA: 0x00017436 File Offset: 0x00015636
			public UIElement(int id, ControllerUIElement element)
			{
				this.id = id;
				this.element = element;
			}

			public int id;

			public ControllerUIElement element;
		}
	}
}
