using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rewired.Demos.GamepadTemplateUI
{
	public class GamepadTemplateUI : MonoBehaviour
	{
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x00065FE6 File Offset: 0x000641E6
		private Player player
		{
			get
			{
				return ReInput.players.GetPlayer(this.playerId);
			}
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00065FF8 File Offset: 0x000641F8
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

		// Token: 0x060017EA RID: 6122 RVA: 0x0006620D File Offset: 0x0006440D
		private void Start()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			this.DrawLabels();
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x0006621D File Offset: 0x0006441D
		private void OnDestroy()
		{
			ReInput.ControllerConnectedEvent -= this.OnControllerConnected;
			ReInput.ControllerDisconnectedEvent -= this.OnControllerDisconnected;
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00066241 File Offset: 0x00064441
		private void Update()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			this.DrawActiveElements();
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00066254 File Offset: 0x00064454
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

		// Token: 0x060017EE RID: 6126 RVA: 0x000662DC File Offset: 0x000644DC
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

		// Token: 0x060017EF RID: 6127 RVA: 0x000663F8 File Offset: 0x000645F8
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

		// Token: 0x060017F0 RID: 6128 RVA: 0x0006645C File Offset: 0x0006465C
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

		// Token: 0x060017F1 RID: 6129 RVA: 0x000664E8 File Offset: 0x000646E8
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

		// Token: 0x060017F2 RID: 6130 RVA: 0x00066640 File Offset: 0x00064840
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

		// Token: 0x060017F3 RID: 6131 RVA: 0x0006667A File Offset: 0x0006487A
		private void OnControllerConnected(ControllerStatusChangedEventArgs args)
		{
			this.DrawLabels();
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x00066682 File Offset: 0x00064882
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
			// (get) Token: 0x06001E20 RID: 7712 RVA: 0x000792DC File Offset: 0x000774DC
			// (set) Token: 0x06001E21 RID: 7713 RVA: 0x00079308 File Offset: 0x00077508
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

			// Token: 0x06001E22 RID: 7714 RVA: 0x00079330 File Offset: 0x00077530
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

			// Token: 0x06001E23 RID: 7715 RVA: 0x00079381 File Offset: 0x00077581
			public void Reset()
			{
				if (this._transform == null)
				{
					return;
				}
				this._transform.anchoredPosition = this._origPosition;
			}

			// Token: 0x06001E24 RID: 7716 RVA: 0x000793A3 File Offset: 0x000775A3
			public bool ContainsElement(int elementId)
			{
				return !(this._transform == null) && (elementId == this._xAxisElementId || elementId == this._yAxisElementId);
			}

			// Token: 0x06001E25 RID: 7717 RVA: 0x000793CC File Offset: 0x000775CC
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
			// Token: 0x06001E26 RID: 7718 RVA: 0x0007941A File Offset: 0x0007761A
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
