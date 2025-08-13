using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rewired.Demos.GamepadTemplateUI
{
	// Token: 0x020004B5 RID: 1205
	public class GamepadTemplateUI : MonoBehaviour
	{
		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001DEC RID: 7660 RVA: 0x00016EDD File Offset: 0x000150DD
		private Player player
		{
			get
			{
				return ReInput.players.GetPlayer(this.playerId);
			}
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x000750A0 File Offset: 0x000732A0
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

		// Token: 0x06001DEE RID: 7662 RVA: 0x00016EEF File Offset: 0x000150EF
		private void Start()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			this.DrawLabels();
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x00016EFF File Offset: 0x000150FF
		private void OnDestroy()
		{
			ReInput.ControllerConnectedEvent -= this.OnControllerConnected;
			ReInput.ControllerDisconnectedEvent -= this.OnControllerDisconnected;
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x00016F23 File Offset: 0x00015123
		private void Update()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			this.DrawActiveElements();
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x000752B8 File Offset: 0x000734B8
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

		// Token: 0x06001DF2 RID: 7666 RVA: 0x00075340 File Offset: 0x00073540
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
						if (controllerTemplateElementTarget.elementType == null)
						{
							controllerUIElement.Activate(axis);
						}
						else if (controllerTemplateElementTarget.elementType == 1 && (player.GetButton(actionId) || player.GetNegativeButton(actionId)))
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

		// Token: 0x06001DF3 RID: 7667 RVA: 0x0007545C File Offset: 0x0007365C
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

		// Token: 0x06001DF4 RID: 7668 RVA: 0x000754C0 File Offset: 0x000736C0
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

		// Token: 0x06001DF5 RID: 7669 RVA: 0x0007554C File Offset: 0x0007374C
		private void DrawLabel(ControllerUIElement uiElement, InputAction action, ControllerMap controllerMap, IControllerTemplate template, IControllerTemplateElement element)
		{
			if (element.source == null)
			{
				return;
			}
			if (element.source.type == null)
			{
				IControllerTemplateAxisSource controllerTemplateAxisSource = element.source as IControllerTemplateAxisSource;
				if (controllerTemplateAxisSource.splitAxis)
				{
					ActionElementMap actionElementMap = controllerMap.GetFirstElementMapWithElementTarget(controllerTemplateAxisSource.positiveTarget, action.id, true);
					if (actionElementMap != null)
					{
						uiElement.SetLabel(actionElementMap.actionDescriptiveName, 1);
					}
					actionElementMap = controllerMap.GetFirstElementMapWithElementTarget(controllerTemplateAxisSource.negativeTarget, action.id, true);
					if (actionElementMap != null)
					{
						uiElement.SetLabel(actionElementMap.actionDescriptiveName, 2);
						return;
					}
				}
				else
				{
					ActionElementMap actionElementMap = controllerMap.GetFirstElementMapWithElementTarget(controllerTemplateAxisSource.fullTarget, action.id, true);
					if (actionElementMap != null)
					{
						uiElement.SetLabel(actionElementMap.actionDescriptiveName, 0);
						return;
					}
					ControllerElementTarget controllerElementTarget;
					controllerElementTarget..ctor(controllerTemplateAxisSource.fullTarget);
					controllerElementTarget.axisRange = 1;
					actionElementMap = controllerMap.GetFirstElementMapWithElementTarget(controllerElementTarget, action.id, true);
					if (actionElementMap != null)
					{
						uiElement.SetLabel(actionElementMap.actionDescriptiveName, 1);
					}
					controllerElementTarget..ctor(controllerTemplateAxisSource.fullTarget);
					controllerElementTarget.axisRange = 2;
					actionElementMap = controllerMap.GetFirstElementMapWithElementTarget(controllerElementTarget, action.id, true);
					if (actionElementMap != null)
					{
						uiElement.SetLabel(actionElementMap.actionDescriptiveName, 2);
						return;
					}
				}
			}
			else if (element.source.type == 1)
			{
				IControllerTemplateButtonSource controllerTemplateButtonSource = element.source as IControllerTemplateButtonSource;
				ActionElementMap actionElementMap = controllerMap.GetFirstElementMapWithElementTarget(controllerTemplateButtonSource.target, action.id, true);
				if (actionElementMap != null)
				{
					uiElement.SetLabel(actionElementMap.actionDescriptiveName, 0);
				}
			}
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x000756A4 File Offset: 0x000738A4
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

		// Token: 0x06001DF7 RID: 7671 RVA: 0x00016F33 File Offset: 0x00015133
		private void OnControllerConnected(ControllerStatusChangedEventArgs args)
		{
			this.DrawLabels();
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x00016F33 File Offset: 0x00015133
		private void OnControllerDisconnected(ControllerStatusChangedEventArgs args)
		{
			this.DrawLabels();
		}

		// Token: 0x04001F38 RID: 7992
		private const float stickRadius = 20f;

		// Token: 0x04001F39 RID: 7993
		public int playerId;

		// Token: 0x04001F3A RID: 7994
		[SerializeField]
		private RectTransform leftStick;

		// Token: 0x04001F3B RID: 7995
		[SerializeField]
		private RectTransform rightStick;

		// Token: 0x04001F3C RID: 7996
		[SerializeField]
		private ControllerUIElement leftStickX;

		// Token: 0x04001F3D RID: 7997
		[SerializeField]
		private ControllerUIElement leftStickY;

		// Token: 0x04001F3E RID: 7998
		[SerializeField]
		private ControllerUIElement leftStickButton;

		// Token: 0x04001F3F RID: 7999
		[SerializeField]
		private ControllerUIElement rightStickX;

		// Token: 0x04001F40 RID: 8000
		[SerializeField]
		private ControllerUIElement rightStickY;

		// Token: 0x04001F41 RID: 8001
		[SerializeField]
		private ControllerUIElement rightStickButton;

		// Token: 0x04001F42 RID: 8002
		[SerializeField]
		private ControllerUIElement actionBottomRow1;

		// Token: 0x04001F43 RID: 8003
		[SerializeField]
		private ControllerUIElement actionBottomRow2;

		// Token: 0x04001F44 RID: 8004
		[SerializeField]
		private ControllerUIElement actionBottomRow3;

		// Token: 0x04001F45 RID: 8005
		[SerializeField]
		private ControllerUIElement actionTopRow1;

		// Token: 0x04001F46 RID: 8006
		[SerializeField]
		private ControllerUIElement actionTopRow2;

		// Token: 0x04001F47 RID: 8007
		[SerializeField]
		private ControllerUIElement actionTopRow3;

		// Token: 0x04001F48 RID: 8008
		[SerializeField]
		private ControllerUIElement leftShoulder;

		// Token: 0x04001F49 RID: 8009
		[SerializeField]
		private ControllerUIElement leftTrigger;

		// Token: 0x04001F4A RID: 8010
		[SerializeField]
		private ControllerUIElement rightShoulder;

		// Token: 0x04001F4B RID: 8011
		[SerializeField]
		private ControllerUIElement rightTrigger;

		// Token: 0x04001F4C RID: 8012
		[SerializeField]
		private ControllerUIElement center1;

		// Token: 0x04001F4D RID: 8013
		[SerializeField]
		private ControllerUIElement center2;

		// Token: 0x04001F4E RID: 8014
		[SerializeField]
		private ControllerUIElement center3;

		// Token: 0x04001F4F RID: 8015
		[SerializeField]
		private ControllerUIElement dPadUp;

		// Token: 0x04001F50 RID: 8016
		[SerializeField]
		private ControllerUIElement dPadRight;

		// Token: 0x04001F51 RID: 8017
		[SerializeField]
		private ControllerUIElement dPadDown;

		// Token: 0x04001F52 RID: 8018
		[SerializeField]
		private ControllerUIElement dPadLeft;

		// Token: 0x04001F53 RID: 8019
		private GamepadTemplateUI.UIElement[] _uiElementsArray;

		// Token: 0x04001F54 RID: 8020
		private Dictionary<int, ControllerUIElement> _uiElements = new Dictionary<int, ControllerUIElement>();

		// Token: 0x04001F55 RID: 8021
		private IList<ControllerTemplateElementTarget> _tempTargetList = new List<ControllerTemplateElementTarget>(2);

		// Token: 0x04001F56 RID: 8022
		private GamepadTemplateUI.Stick[] _sticks;

		// Token: 0x020004B6 RID: 1206
		private class Stick
		{
			// Token: 0x17000627 RID: 1575
			// (get) Token: 0x06001DFA RID: 7674 RVA: 0x00016F5A File Offset: 0x0001515A
			// (set) Token: 0x06001DFB RID: 7675 RVA: 0x00016F86 File Offset: 0x00015186
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

			// Token: 0x06001DFC RID: 7676 RVA: 0x000756E0 File Offset: 0x000738E0
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

			// Token: 0x06001DFD RID: 7677 RVA: 0x00016FAE File Offset: 0x000151AE
			public void Reset()
			{
				if (this._transform == null)
				{
					return;
				}
				this._transform.anchoredPosition = this._origPosition;
			}

			// Token: 0x06001DFE RID: 7678 RVA: 0x00016FD0 File Offset: 0x000151D0
			public bool ContainsElement(int elementId)
			{
				return !(this._transform == null) && (elementId == this._xAxisElementId || elementId == this._yAxisElementId);
			}

			// Token: 0x06001DFF RID: 7679 RVA: 0x00075734 File Offset: 0x00073934
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

			// Token: 0x04001F57 RID: 8023
			private RectTransform _transform;

			// Token: 0x04001F58 RID: 8024
			private Vector2 _origPosition;

			// Token: 0x04001F59 RID: 8025
			private int _xAxisElementId = -1;

			// Token: 0x04001F5A RID: 8026
			private int _yAxisElementId = -1;
		}

		// Token: 0x020004B7 RID: 1207
		private class UIElement
		{
			// Token: 0x06001E00 RID: 7680 RVA: 0x00016FF6 File Offset: 0x000151F6
			public UIElement(int id, ControllerUIElement element)
			{
				this.id = id;
				this.element = element;
			}

			// Token: 0x04001F5B RID: 8027
			public int id;

			// Token: 0x04001F5C RID: 8028
			public ControllerUIElement element;
		}
	}
}
