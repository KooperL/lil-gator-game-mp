using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000325 RID: 805
	[Serializable]
	public class LanguageData : LanguageDataBase
	{
		// Token: 0x060015BB RID: 5563 RVA: 0x0005D0F9 File Offset: 0x0005B2F9
		public override void Initialize()
		{
			if (this._initialized)
			{
				return;
			}
			this.customDict = LanguageData.CustomEntry.ToDictionary(this._customEntries);
			this._initialized = true;
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x0005D11C File Offset: 0x0005B31C
		public override string GetCustomEntry(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				return string.Empty;
			}
			string text;
			if (!this.customDict.TryGetValue(key, out text))
			{
				return string.Empty;
			}
			return text;
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x0005D14E File Offset: 0x0005B34E
		public override bool ContainsCustomEntryKey(string key)
		{
			return !string.IsNullOrEmpty(key) && this.customDict.ContainsKey(key);
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x0005D166 File Offset: 0x0005B366
		public override string yes
		{
			get
			{
				return this._yes;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x0005D16E File Offset: 0x0005B36E
		public override string no
		{
			get
			{
				return this._no;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x0005D176 File Offset: 0x0005B376
		public override string add
		{
			get
			{
				return this._add;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x0005D17E File Offset: 0x0005B37E
		public override string replace
		{
			get
			{
				return this._replace;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x0005D186 File Offset: 0x0005B386
		public override string remove
		{
			get
			{
				return this._remove;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x0005D18E File Offset: 0x0005B38E
		public override string swap
		{
			get
			{
				return this._swap;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x0005D196 File Offset: 0x0005B396
		public override string cancel
		{
			get
			{
				return this._cancel;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x0005D19E File Offset: 0x0005B39E
		public override string none
		{
			get
			{
				return this._none;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x0005D1A6 File Offset: 0x0005B3A6
		public override string okay
		{
			get
			{
				return this._okay;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x0005D1AE File Offset: 0x0005B3AE
		public override string done
		{
			get
			{
				return this._done;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x0005D1B6 File Offset: 0x0005B3B6
		public override string default_
		{
			get
			{
				return this._default;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x0005D1BE File Offset: 0x0005B3BE
		public override string assignControllerWindowTitle
		{
			get
			{
				return this._assignControllerWindowTitle;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x0005D1C6 File Offset: 0x0005B3C6
		public override string assignControllerWindowMessage
		{
			get
			{
				return this._assignControllerWindowMessage;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x0005D1CE File Offset: 0x0005B3CE
		public override string controllerAssignmentConflictWindowTitle
		{
			get
			{
				return this._controllerAssignmentConflictWindowTitle;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x0005D1D6 File Offset: 0x0005B3D6
		public override string elementAssignmentPrePollingWindowMessage
		{
			get
			{
				return this._elementAssignmentPrePollingWindowMessage;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x0005D1DE File Offset: 0x0005B3DE
		public override string elementAssignmentConflictWindowMessage
		{
			get
			{
				return this._elementAssignmentConflictWindowMessage;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060015CE RID: 5582 RVA: 0x0005D1E6 File Offset: 0x0005B3E6
		public override string mouseAssignmentConflictWindowTitle
		{
			get
			{
				return this._mouseAssignmentConflictWindowTitle;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x0005D1EE File Offset: 0x0005B3EE
		public override string calibrateControllerWindowTitle
		{
			get
			{
				return this._calibrateControllerWindowTitle;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x0005D1F6 File Offset: 0x0005B3F6
		public override string calibrateAxisStep1WindowTitle
		{
			get
			{
				return this._calibrateAxisStep1WindowTitle;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x0005D1FE File Offset: 0x0005B3FE
		public override string calibrateAxisStep2WindowTitle
		{
			get
			{
				return this._calibrateAxisStep2WindowTitle;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x0005D206 File Offset: 0x0005B406
		public override string inputBehaviorSettingsWindowTitle
		{
			get
			{
				return this._inputBehaviorSettingsWindowTitle;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x0005D20E File Offset: 0x0005B40E
		public override string restoreDefaultsWindowTitle
		{
			get
			{
				return this._restoreDefaultsWindowTitle;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x0005D216 File Offset: 0x0005B416
		public override string actionColumnLabel
		{
			get
			{
				return this._actionColumnLabel;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x0005D21E File Offset: 0x0005B41E
		public override string keyboardColumnLabel
		{
			get
			{
				return this._keyboardColumnLabel;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060015D6 RID: 5590 RVA: 0x0005D226 File Offset: 0x0005B426
		public override string mouseColumnLabel
		{
			get
			{
				return this._mouseColumnLabel;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x0005D22E File Offset: 0x0005B42E
		public override string controllerColumnLabel
		{
			get
			{
				return this._controllerColumnLabel;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x0005D236 File Offset: 0x0005B436
		public override string removeControllerButtonLabel
		{
			get
			{
				return this._removeControllerButtonLabel;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x060015D9 RID: 5593 RVA: 0x0005D23E File Offset: 0x0005B43E
		public override string calibrateControllerButtonLabel
		{
			get
			{
				return this._calibrateControllerButtonLabel;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x0005D246 File Offset: 0x0005B446
		public override string assignControllerButtonLabel
		{
			get
			{
				return this._assignControllerButtonLabel;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x0005D24E File Offset: 0x0005B44E
		public override string inputBehaviorSettingsButtonLabel
		{
			get
			{
				return this._inputBehaviorSettingsButtonLabel;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x0005D256 File Offset: 0x0005B456
		public override string doneButtonLabel
		{
			get
			{
				return this._doneButtonLabel;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x0005D25E File Offset: 0x0005B45E
		public override string restoreDefaultsButtonLabel
		{
			get
			{
				return this._restoreDefaultsButtonLabel;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x0005D266 File Offset: 0x0005B466
		public override string controllerSettingsGroupLabel
		{
			get
			{
				return this._controllerSettingsGroupLabel;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x0005D26E File Offset: 0x0005B46E
		public override string playersGroupLabel
		{
			get
			{
				return this._playersGroupLabel;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x0005D276 File Offset: 0x0005B476
		public override string assignedControllersGroupLabel
		{
			get
			{
				return this._assignedControllersGroupLabel;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060015E1 RID: 5601 RVA: 0x0005D27E File Offset: 0x0005B47E
		public override string settingsGroupLabel
		{
			get
			{
				return this._settingsGroupLabel;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x0005D286 File Offset: 0x0005B486
		public override string mapCategoriesGroupLabel
		{
			get
			{
				return this._mapCategoriesGroupLabel;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x0005D28E File Offset: 0x0005B48E
		public override string restoreDefaultsWindowMessage
		{
			get
			{
				if (ReInput.players.playerCount > 1)
				{
					return this._restoreDefaultsWindowMessage_multiPlayer;
				}
				return this._restoreDefaultsWindowMessage_onePlayer;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x0005D2AA File Offset: 0x0005B4AA
		public override string calibrateWindow_deadZoneSliderLabel
		{
			get
			{
				return this._calibrateWindow_deadZoneSliderLabel;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x0005D2B2 File Offset: 0x0005B4B2
		public override string calibrateWindow_zeroSliderLabel
		{
			get
			{
				return this._calibrateWindow_zeroSliderLabel;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x0005D2BA File Offset: 0x0005B4BA
		public override string calibrateWindow_sensitivitySliderLabel
		{
			get
			{
				return this._calibrateWindow_sensitivitySliderLabel;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x0005D2C2 File Offset: 0x0005B4C2
		public override string calibrateWindow_invertToggleLabel
		{
			get
			{
				return this._calibrateWindow_invertToggleLabel;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x0005D2CA File Offset: 0x0005B4CA
		public override string calibrateWindow_calibrateButtonLabel
		{
			get
			{
				return this._calibrateWindow_calibrateButtonLabel;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x0005D2D2 File Offset: 0x0005B4D2
		public override string elementAssignmentReplacementWindowMessage
		{
			get
			{
				return "Button";
			}
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x0005D2D9 File Offset: 0x0005B4D9
		public override string GetControllerAssignmentConflictWindowMessage(string joystickName, string otherPlayerName, string currentPlayerName)
		{
			return string.Format(this._controllerAssignmentConflictWindowMessage, joystickName, otherPlayerName, currentPlayerName);
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x0005D2E9 File Offset: 0x0005B4E9
		public override string GetJoystickElementAssignmentPollingWindowMessage(string actionName)
		{
			return string.Format(this._joystickElementAssignmentPollingWindowMessage, actionName);
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x0005D2F7 File Offset: 0x0005B4F7
		public override string GetJoystickElementAssignmentPollingWindowMessage_FullAxisFieldOnly(string actionName)
		{
			return string.Format(this._joystickElementAssignmentPollingWindowMessage_fullAxisFieldOnly, actionName);
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x0005D305 File Offset: 0x0005B505
		public override string GetKeyboardElementAssignmentPollingWindowMessage(string actionName)
		{
			return string.Format(this._keyboardElementAssignmentPollingWindowMessage, actionName);
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x0005D313 File Offset: 0x0005B513
		public override string GetMouseElementAssignmentPollingWindowMessage(string actionName)
		{
			return string.Format(this._mouseElementAssignmentPollingWindowMessage, actionName);
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x0005D321 File Offset: 0x0005B521
		public override string GetMouseElementAssignmentPollingWindowMessage_FullAxisFieldOnly(string actionName)
		{
			return string.Format(this._mouseElementAssignmentPollingWindowMessage_fullAxisFieldOnly, actionName);
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x0005D32F File Offset: 0x0005B52F
		public override string GetElementAlreadyInUseBlocked(string elementName)
		{
			return string.Format(this._elementAlreadyInUseBlocked, elementName);
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x0005D33D File Offset: 0x0005B53D
		public override string GetElementAlreadyInUseCanReplace(string elementName, bool allowConflicts)
		{
			if (!allowConflicts)
			{
				return string.Format(this._elementAlreadyInUseCanReplace, elementName);
			}
			return string.Format(this._elementAlreadyInUseCanReplace_conflictAllowed, elementName);
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x0005D35B File Offset: 0x0005B55B
		public override string GetMouseAssignmentConflictWindowMessage(string otherPlayerName, string thisPlayerName)
		{
			return string.Format(this._mouseAssignmentConflictWindowMessage, otherPlayerName, thisPlayerName);
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x0005D36A File Offset: 0x0005B56A
		public override string GetCalibrateAxisStep1WindowMessage(string axisName)
		{
			return string.Format(this._calibrateAxisStep1WindowMessage, axisName);
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x0005D378 File Offset: 0x0005B578
		public override string GetCalibrateAxisStep2WindowMessage(string axisName)
		{
			return string.Format(this._calibrateAxisStep2WindowMessage, axisName);
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x0005D386 File Offset: 0x0005B586
		public override string GetPlayerName(int playerId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				throw new ArgumentException("Invalid player id: " + playerId.ToString());
			}
			return player.descriptiveName;
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x0005D3B2 File Offset: 0x0005B5B2
		public override string GetControllerName(Controller controller)
		{
			if (controller == null)
			{
				throw new ArgumentNullException("controller");
			}
			return controller.name;
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x0005D3C8 File Offset: 0x0005B5C8
		public override string GetElementIdentifierName(ActionElementMap actionElementMap)
		{
			if (actionElementMap == null)
			{
				throw new ArgumentNullException("actionElementMap");
			}
			if (actionElementMap.controllerMap.controllerType == ControllerType.Keyboard)
			{
				return this.GetElementIdentifierName(actionElementMap.keyCode, actionElementMap.modifierKeyFlags);
			}
			return this.GetElementIdentifierName(actionElementMap.controllerMap.controller, actionElementMap.elementIdentifierId, actionElementMap.axisRange);
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x0005D420 File Offset: 0x0005B620
		public override string GetElementIdentifierName(Controller controller, int elementIdentifierId, AxisRange axisRange)
		{
			if (controller == null)
			{
				throw new ArgumentNullException("controller");
			}
			ControllerElementIdentifier elementIdentifierById = controller.GetElementIdentifierById(elementIdentifierId);
			if (elementIdentifierById == null)
			{
				throw new ArgumentException("Invalid element identifier id: " + elementIdentifierId.ToString());
			}
			Controller.Element elementById = controller.GetElementById(elementIdentifierId);
			if (elementById == null)
			{
				return string.Empty;
			}
			ControllerElementType type = elementById.type;
			if (type == ControllerElementType.Axis)
			{
				return elementIdentifierById.GetDisplayName(elementById.type, axisRange);
			}
			if (type != ControllerElementType.Button)
			{
				return elementIdentifierById.name;
			}
			return elementIdentifierById.name;
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x0005D499 File Offset: 0x0005B699
		public override string GetElementIdentifierName(KeyCode keyCode, ModifierKeyFlags modifierKeyFlags)
		{
			if (modifierKeyFlags != ModifierKeyFlags.None)
			{
				return string.Format("{0}{1}{2}", this.ModifierKeyFlagsToString(modifierKeyFlags), this._modifierKeys.separator, Keyboard.GetKeyName(keyCode));
			}
			return Keyboard.GetKeyName(keyCode);
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x0005D4C7 File Offset: 0x0005B6C7
		public override string GetActionName(int actionId)
		{
			InputAction action = ReInput.mapping.GetAction(actionId);
			if (action == null)
			{
				throw new ArgumentException("Invalid action id: " + actionId.ToString());
			}
			return action.descriptiveName;
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x0005D4F4 File Offset: 0x0005B6F4
		public override string GetActionName(int actionId, AxisRange axisRange)
		{
			InputAction action = ReInput.mapping.GetAction(actionId);
			if (action == null)
			{
				throw new ArgumentException("Invalid action id: " + actionId.ToString());
			}
			switch (axisRange)
			{
			case AxisRange.Full:
				return action.descriptiveName;
			case AxisRange.Positive:
				if (string.IsNullOrEmpty(action.positiveDescriptiveName))
				{
					return action.descriptiveName + " +";
				}
				return action.positiveDescriptiveName;
			case AxisRange.Negative:
				if (string.IsNullOrEmpty(action.negativeDescriptiveName))
				{
					return action.descriptiveName + " -";
				}
				return action.negativeDescriptiveName;
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x0005D591 File Offset: 0x0005B791
		public override string GetMapCategoryName(int id)
		{
			InputMapCategory mapCategory = ReInput.mapping.GetMapCategory(id);
			if (mapCategory == null)
			{
				throw new ArgumentException("Invalid map category id: " + id.ToString());
			}
			return mapCategory.descriptiveName;
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x0005D5BD File Offset: 0x0005B7BD
		public override string GetActionCategoryName(int id)
		{
			InputCategory actionCategory = ReInput.mapping.GetActionCategory(id);
			if (actionCategory == null)
			{
				throw new ArgumentException("Invalid action category id: " + id.ToString());
			}
			return actionCategory.descriptiveName;
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x0005D5E9 File Offset: 0x0005B7E9
		public override string GetLayoutName(ControllerType controllerType, int id)
		{
			InputLayout layout = ReInput.mapping.GetLayout(controllerType, id);
			if (layout == null)
			{
				throw new ArgumentException("Invalid " + controllerType.ToString() + " layout id: " + id.ToString());
			}
			return layout.descriptiveName;
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x0005D628 File Offset: 0x0005B828
		public override string ModifierKeyFlagsToString(ModifierKeyFlags flags)
		{
			int num = 0;
			string text = string.Empty;
			if (Keyboard.ModifierKeyFlagsContain(flags, ModifierKey.Control))
			{
				text += this._modifierKeys.control;
				num++;
			}
			if (Keyboard.ModifierKeyFlagsContain(flags, ModifierKey.Command))
			{
				if (num > 0 && !string.IsNullOrEmpty(this._modifierKeys.separator))
				{
					text += this._modifierKeys.separator;
				}
				text += this._modifierKeys.command;
				num++;
			}
			if (Keyboard.ModifierKeyFlagsContain(flags, ModifierKey.Alt))
			{
				if (num > 0 && !string.IsNullOrEmpty(this._modifierKeys.separator))
				{
					text += this._modifierKeys.separator;
				}
				text += this._modifierKeys.alt;
				num++;
			}
			if (num >= 3)
			{
				return text;
			}
			if (Keyboard.ModifierKeyFlagsContain(flags, ModifierKey.Shift))
			{
				if (num > 0 && !string.IsNullOrEmpty(this._modifierKeys.separator))
				{
					text += this._modifierKeys.separator;
				}
				text += this._modifierKeys.shift;
				num++;
			}
			return text;
		}

		// Token: 0x04001842 RID: 6210
		[SerializeField]
		private string _yes = "Yes";

		// Token: 0x04001843 RID: 6211
		[SerializeField]
		private string _no = "No";

		// Token: 0x04001844 RID: 6212
		[SerializeField]
		private string _add = "Add";

		// Token: 0x04001845 RID: 6213
		[SerializeField]
		private string _replace = "Replace";

		// Token: 0x04001846 RID: 6214
		[SerializeField]
		private string _remove = "Remove";

		// Token: 0x04001847 RID: 6215
		[SerializeField]
		private string _swap = "Swap";

		// Token: 0x04001848 RID: 6216
		[SerializeField]
		private string _cancel = "Cancel";

		// Token: 0x04001849 RID: 6217
		[SerializeField]
		private string _none = "None";

		// Token: 0x0400184A RID: 6218
		[SerializeField]
		private string _okay = "Okay";

		// Token: 0x0400184B RID: 6219
		[SerializeField]
		private string _done = "Done";

		// Token: 0x0400184C RID: 6220
		[SerializeField]
		private string _default = "Default";

		// Token: 0x0400184D RID: 6221
		[SerializeField]
		private string _assignControllerWindowTitle = "Choose Controller";

		// Token: 0x0400184E RID: 6222
		[SerializeField]
		private string _assignControllerWindowMessage = "Press any button or move an axis on the controller you would like to use.";

		// Token: 0x0400184F RID: 6223
		[SerializeField]
		private string _controllerAssignmentConflictWindowTitle = "Controller Assignment";

		// Token: 0x04001850 RID: 6224
		[SerializeField]
		[Tooltip("{0} = Joystick Name\n{1} = Other Player Name\n{2} = This Player Name")]
		private string _controllerAssignmentConflictWindowMessage = "{0} is already assigned to {1}. Do you want to assign this controller to {2} instead?";

		// Token: 0x04001851 RID: 6225
		[SerializeField]
		private string _elementAssignmentPrePollingWindowMessage = "First center or zero all sticks and axes and press any button or wait for the timer to finish.";

		// Token: 0x04001852 RID: 6226
		[SerializeField]
		[Tooltip("{0} = Action Name")]
		private string _joystickElementAssignmentPollingWindowMessage = "Now press a button or move an axis to assign it to {0}.";

		// Token: 0x04001853 RID: 6227
		[SerializeField]
		[Tooltip("This text is only displayed when split-axis fields have been disabled and the user clicks on the full-axis field. Button/key/D-pad input cannot be assigned to a full-axis field.\n{0} = Action Name")]
		private string _joystickElementAssignmentPollingWindowMessage_fullAxisFieldOnly = "Now move an axis to assign it to {0}.";

		// Token: 0x04001854 RID: 6228
		[SerializeField]
		[Tooltip("{0} = Action Name")]
		private string _keyboardElementAssignmentPollingWindowMessage = "Press a key to assign it to {0}. Modifier keys may also be used. To assign a modifier key alone, hold it down for 1 second.";

		// Token: 0x04001855 RID: 6229
		[SerializeField]
		[Tooltip("{0} = Action Name")]
		private string _mouseElementAssignmentPollingWindowMessage = "Press a mouse button or move an axis to assign it to {0}.";

		// Token: 0x04001856 RID: 6230
		[SerializeField]
		[Tooltip("This text is only displayed when split-axis fields have been disabled and the user clicks on the full-axis field. Button/key/D-pad input cannot be assigned to a full-axis field.\n{0} = Action Name")]
		private string _mouseElementAssignmentPollingWindowMessage_fullAxisFieldOnly = "Move an axis to assign it to {0}.";

		// Token: 0x04001857 RID: 6231
		[SerializeField]
		private string _elementAssignmentConflictWindowMessage = "Assignment Conflict";

		// Token: 0x04001858 RID: 6232
		[SerializeField]
		[Tooltip("{0} = Element Name")]
		private string _elementAlreadyInUseBlocked = "{0} is already in use cannot be replaced.";

		// Token: 0x04001859 RID: 6233
		[SerializeField]
		[Tooltip("{0} = Element Name")]
		private string _elementAlreadyInUseCanReplace = "{0} is already in use. Do you want to replace it?";

		// Token: 0x0400185A RID: 6234
		[SerializeField]
		[Tooltip("{0} = Element Name")]
		private string _elementAlreadyInUseCanReplace_conflictAllowed = "{0} is already in use. Do you want to replace it? You may also choose to add the assignment anyway.";

		// Token: 0x0400185B RID: 6235
		[SerializeField]
		private string _mouseAssignmentConflictWindowTitle = "Mouse Assignment";

		// Token: 0x0400185C RID: 6236
		[SerializeField]
		[Tooltip("{0} = Other Player Name\n{1} = This Player Name")]
		private string _mouseAssignmentConflictWindowMessage = "The mouse is already assigned to {0}. Do you want to assign the mouse to {1} instead?";

		// Token: 0x0400185D RID: 6237
		[SerializeField]
		private string _calibrateControllerWindowTitle = "Calibrate Controller";

		// Token: 0x0400185E RID: 6238
		[SerializeField]
		private string _calibrateAxisStep1WindowTitle = "Calibrate Zero";

		// Token: 0x0400185F RID: 6239
		[SerializeField]
		[Tooltip("{0} = Axis Name")]
		private string _calibrateAxisStep1WindowMessage = "Center or zero {0} and press any button or wait for the timer to finish.";

		// Token: 0x04001860 RID: 6240
		[SerializeField]
		private string _calibrateAxisStep2WindowTitle = "Calibrate Range";

		// Token: 0x04001861 RID: 6241
		[SerializeField]
		[Tooltip("{0} = Axis Name")]
		private string _calibrateAxisStep2WindowMessage = "Move {0} through its entire range then press any button or wait for the timer to finish.";

		// Token: 0x04001862 RID: 6242
		[SerializeField]
		private string _inputBehaviorSettingsWindowTitle = "Sensitivity Settings";

		// Token: 0x04001863 RID: 6243
		[SerializeField]
		private string _restoreDefaultsWindowTitle = "Restore Defaults";

		// Token: 0x04001864 RID: 6244
		[SerializeField]
		[Tooltip("Message for a single player game.")]
		private string _restoreDefaultsWindowMessage_onePlayer = "This will restore the default input configuration. Are you sure you want to do this?";

		// Token: 0x04001865 RID: 6245
		[SerializeField]
		[Tooltip("Message for a multi-player game.")]
		private string _restoreDefaultsWindowMessage_multiPlayer = "This will restore the default input configuration for all players. Are you sure you want to do this?";

		// Token: 0x04001866 RID: 6246
		[SerializeField]
		private string _actionColumnLabel = "Actions";

		// Token: 0x04001867 RID: 6247
		[SerializeField]
		private string _keyboardColumnLabel = "Keyboard";

		// Token: 0x04001868 RID: 6248
		[SerializeField]
		private string _mouseColumnLabel = "Mouse";

		// Token: 0x04001869 RID: 6249
		[SerializeField]
		private string _controllerColumnLabel = "Controller";

		// Token: 0x0400186A RID: 6250
		[SerializeField]
		private string _removeControllerButtonLabel = "Remove";

		// Token: 0x0400186B RID: 6251
		[SerializeField]
		private string _calibrateControllerButtonLabel = "Calibrate";

		// Token: 0x0400186C RID: 6252
		[SerializeField]
		private string _assignControllerButtonLabel = "Assign Controller";

		// Token: 0x0400186D RID: 6253
		[SerializeField]
		private string _inputBehaviorSettingsButtonLabel = "Sensitivity";

		// Token: 0x0400186E RID: 6254
		[SerializeField]
		private string _doneButtonLabel = "Done";

		// Token: 0x0400186F RID: 6255
		[SerializeField]
		private string _restoreDefaultsButtonLabel = "Restore Defaults";

		// Token: 0x04001870 RID: 6256
		[SerializeField]
		private string _playersGroupLabel = "Players:";

		// Token: 0x04001871 RID: 6257
		[SerializeField]
		private string _controllerSettingsGroupLabel = "Controller:";

		// Token: 0x04001872 RID: 6258
		[SerializeField]
		private string _assignedControllersGroupLabel = "Assigned Controllers:";

		// Token: 0x04001873 RID: 6259
		[SerializeField]
		private string _settingsGroupLabel = "Settings:";

		// Token: 0x04001874 RID: 6260
		[SerializeField]
		private string _mapCategoriesGroupLabel = "Categories:";

		// Token: 0x04001875 RID: 6261
		[SerializeField]
		private string _calibrateWindow_deadZoneSliderLabel = "Dead Zone:";

		// Token: 0x04001876 RID: 6262
		[SerializeField]
		private string _calibrateWindow_zeroSliderLabel = "Zero:";

		// Token: 0x04001877 RID: 6263
		[SerializeField]
		private string _calibrateWindow_sensitivitySliderLabel = "Sensitivity:";

		// Token: 0x04001878 RID: 6264
		[SerializeField]
		private string _calibrateWindow_invertToggleLabel = "Invert";

		// Token: 0x04001879 RID: 6265
		[SerializeField]
		private string _calibrateWindow_calibrateButtonLabel = "Calibrate";

		// Token: 0x0400187A RID: 6266
		[SerializeField]
		private LanguageData.ModifierKeys _modifierKeys;

		// Token: 0x0400187B RID: 6267
		[SerializeField]
		private LanguageData.CustomEntry[] _customEntries;

		// Token: 0x0400187C RID: 6268
		private bool _initialized;

		// Token: 0x0400187D RID: 6269
		private Dictionary<string, string> customDict;

		// Token: 0x0200048C RID: 1164
		[Serializable]
		protected class CustomEntry
		{
			// Token: 0x06001D48 RID: 7496 RVA: 0x00077EF9 File Offset: 0x000760F9
			public CustomEntry()
			{
			}

			// Token: 0x06001D49 RID: 7497 RVA: 0x00077F01 File Offset: 0x00076101
			public CustomEntry(string key, string value)
			{
				this.key = key;
				this.value = value;
			}

			// Token: 0x06001D4A RID: 7498 RVA: 0x00077F18 File Offset: 0x00076118
			public static Dictionary<string, string> ToDictionary(LanguageData.CustomEntry[] array)
			{
				if (array == null)
				{
					return new Dictionary<string, string>();
				}
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != null && !string.IsNullOrEmpty(array[i].key) && !string.IsNullOrEmpty(array[i].value))
					{
						if (dictionary.ContainsKey(array[i].key))
						{
							Debug.LogError("Key \"" + array[i].key + "\" is already in dictionary!");
						}
						else
						{
							dictionary.Add(array[i].key, array[i].value);
						}
					}
				}
				return dictionary;
			}

			// Token: 0x04001EF3 RID: 7923
			public string key;

			// Token: 0x04001EF4 RID: 7924
			public string value;
		}

		// Token: 0x0200048D RID: 1165
		[Serializable]
		protected class ModifierKeys
		{
			// Token: 0x04001EF5 RID: 7925
			public string control = "Control";

			// Token: 0x04001EF6 RID: 7926
			public string alt = "Alt";

			// Token: 0x04001EF7 RID: 7927
			public string shift = "Shift";

			// Token: 0x04001EF8 RID: 7928
			public string command = "Command";

			// Token: 0x04001EF9 RID: 7929
			public string separator = " + ";
		}
	}
}
