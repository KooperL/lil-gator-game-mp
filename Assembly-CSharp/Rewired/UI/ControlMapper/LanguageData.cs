using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	[Serializable]
	public class LanguageData : LanguageDataBase
	{
		// Token: 0x06001B48 RID: 6984 RVA: 0x0001501A File Offset: 0x0001321A
		public override void Initialize()
		{
			if (this._initialized)
			{
				return;
			}
			this.customDict = LanguageData.CustomEntry.ToDictionary(this._customEntries);
			this._initialized = true;
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x0006F66C File Offset: 0x0006D86C
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

		// Token: 0x06001B4A RID: 6986 RVA: 0x0001503D File Offset: 0x0001323D
		public override bool ContainsCustomEntryKey(string key)
		{
			return !string.IsNullOrEmpty(key) && this.customDict.ContainsKey(key);
		}

		// (get) Token: 0x06001B4B RID: 6987 RVA: 0x00015055 File Offset: 0x00013255
		public override string yes
		{
			get
			{
				return this._yes;
			}
		}

		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x0001505D File Offset: 0x0001325D
		public override string no
		{
			get
			{
				return this._no;
			}
		}

		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x00015065 File Offset: 0x00013265
		public override string add
		{
			get
			{
				return this._add;
			}
		}

		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x0001506D File Offset: 0x0001326D
		public override string replace
		{
			get
			{
				return this._replace;
			}
		}

		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x00015075 File Offset: 0x00013275
		public override string remove
		{
			get
			{
				return this._remove;
			}
		}

		// (get) Token: 0x06001B50 RID: 6992 RVA: 0x0001507D File Offset: 0x0001327D
		public override string swap
		{
			get
			{
				return this._swap;
			}
		}

		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x00015085 File Offset: 0x00013285
		public override string cancel
		{
			get
			{
				return this._cancel;
			}
		}

		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x0001508D File Offset: 0x0001328D
		public override string none
		{
			get
			{
				return this._none;
			}
		}

		// (get) Token: 0x06001B53 RID: 6995 RVA: 0x00015095 File Offset: 0x00013295
		public override string okay
		{
			get
			{
				return this._okay;
			}
		}

		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x0001509D File Offset: 0x0001329D
		public override string done
		{
			get
			{
				return this._done;
			}
		}

		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x000150A5 File Offset: 0x000132A5
		public override string default_
		{
			get
			{
				return this._default;
			}
		}

		// (get) Token: 0x06001B56 RID: 6998 RVA: 0x000150AD File Offset: 0x000132AD
		public override string assignControllerWindowTitle
		{
			get
			{
				return this._assignControllerWindowTitle;
			}
		}

		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x000150B5 File Offset: 0x000132B5
		public override string assignControllerWindowMessage
		{
			get
			{
				return this._assignControllerWindowMessage;
			}
		}

		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x000150BD File Offset: 0x000132BD
		public override string controllerAssignmentConflictWindowTitle
		{
			get
			{
				return this._controllerAssignmentConflictWindowTitle;
			}
		}

		// (get) Token: 0x06001B59 RID: 7001 RVA: 0x000150C5 File Offset: 0x000132C5
		public override string elementAssignmentPrePollingWindowMessage
		{
			get
			{
				return this._elementAssignmentPrePollingWindowMessage;
			}
		}

		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x000150CD File Offset: 0x000132CD
		public override string elementAssignmentConflictWindowMessage
		{
			get
			{
				return this._elementAssignmentConflictWindowMessage;
			}
		}

		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x000150D5 File Offset: 0x000132D5
		public override string mouseAssignmentConflictWindowTitle
		{
			get
			{
				return this._mouseAssignmentConflictWindowTitle;
			}
		}

		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x000150DD File Offset: 0x000132DD
		public override string calibrateControllerWindowTitle
		{
			get
			{
				return this._calibrateControllerWindowTitle;
			}
		}

		// (get) Token: 0x06001B5D RID: 7005 RVA: 0x000150E5 File Offset: 0x000132E5
		public override string calibrateAxisStep1WindowTitle
		{
			get
			{
				return this._calibrateAxisStep1WindowTitle;
			}
		}

		// (get) Token: 0x06001B5E RID: 7006 RVA: 0x000150ED File Offset: 0x000132ED
		public override string calibrateAxisStep2WindowTitle
		{
			get
			{
				return this._calibrateAxisStep2WindowTitle;
			}
		}

		// (get) Token: 0x06001B5F RID: 7007 RVA: 0x000150F5 File Offset: 0x000132F5
		public override string inputBehaviorSettingsWindowTitle
		{
			get
			{
				return this._inputBehaviorSettingsWindowTitle;
			}
		}

		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x000150FD File Offset: 0x000132FD
		public override string restoreDefaultsWindowTitle
		{
			get
			{
				return this._restoreDefaultsWindowTitle;
			}
		}

		// (get) Token: 0x06001B61 RID: 7009 RVA: 0x00015105 File Offset: 0x00013305
		public override string actionColumnLabel
		{
			get
			{
				return this._actionColumnLabel;
			}
		}

		// (get) Token: 0x06001B62 RID: 7010 RVA: 0x0001510D File Offset: 0x0001330D
		public override string keyboardColumnLabel
		{
			get
			{
				return this._keyboardColumnLabel;
			}
		}

		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x00015115 File Offset: 0x00013315
		public override string mouseColumnLabel
		{
			get
			{
				return this._mouseColumnLabel;
			}
		}

		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x0001511D File Offset: 0x0001331D
		public override string controllerColumnLabel
		{
			get
			{
				return this._controllerColumnLabel;
			}
		}

		// (get) Token: 0x06001B65 RID: 7013 RVA: 0x00015125 File Offset: 0x00013325
		public override string removeControllerButtonLabel
		{
			get
			{
				return this._removeControllerButtonLabel;
			}
		}

		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x0001512D File Offset: 0x0001332D
		public override string calibrateControllerButtonLabel
		{
			get
			{
				return this._calibrateControllerButtonLabel;
			}
		}

		// (get) Token: 0x06001B67 RID: 7015 RVA: 0x00015135 File Offset: 0x00013335
		public override string assignControllerButtonLabel
		{
			get
			{
				return this._assignControllerButtonLabel;
			}
		}

		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x0001513D File Offset: 0x0001333D
		public override string inputBehaviorSettingsButtonLabel
		{
			get
			{
				return this._inputBehaviorSettingsButtonLabel;
			}
		}

		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x00015145 File Offset: 0x00013345
		public override string doneButtonLabel
		{
			get
			{
				return this._doneButtonLabel;
			}
		}

		// (get) Token: 0x06001B6A RID: 7018 RVA: 0x0001514D File Offset: 0x0001334D
		public override string restoreDefaultsButtonLabel
		{
			get
			{
				return this._restoreDefaultsButtonLabel;
			}
		}

		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x00015155 File Offset: 0x00013355
		public override string controllerSettingsGroupLabel
		{
			get
			{
				return this._controllerSettingsGroupLabel;
			}
		}

		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x0001515D File Offset: 0x0001335D
		public override string playersGroupLabel
		{
			get
			{
				return this._playersGroupLabel;
			}
		}

		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x00015165 File Offset: 0x00013365
		public override string assignedControllersGroupLabel
		{
			get
			{
				return this._assignedControllersGroupLabel;
			}
		}

		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x0001516D File Offset: 0x0001336D
		public override string settingsGroupLabel
		{
			get
			{
				return this._settingsGroupLabel;
			}
		}

		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x00015175 File Offset: 0x00013375
		public override string mapCategoriesGroupLabel
		{
			get
			{
				return this._mapCategoriesGroupLabel;
			}
		}

		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x0001517D File Offset: 0x0001337D
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

		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x00015199 File Offset: 0x00013399
		public override string calibrateWindow_deadZoneSliderLabel
		{
			get
			{
				return this._calibrateWindow_deadZoneSliderLabel;
			}
		}

		// (get) Token: 0x06001B72 RID: 7026 RVA: 0x000151A1 File Offset: 0x000133A1
		public override string calibrateWindow_zeroSliderLabel
		{
			get
			{
				return this._calibrateWindow_zeroSliderLabel;
			}
		}

		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x000151A9 File Offset: 0x000133A9
		public override string calibrateWindow_sensitivitySliderLabel
		{
			get
			{
				return this._calibrateWindow_sensitivitySliderLabel;
			}
		}

		// (get) Token: 0x06001B74 RID: 7028 RVA: 0x000151B1 File Offset: 0x000133B1
		public override string calibrateWindow_invertToggleLabel
		{
			get
			{
				return this._calibrateWindow_invertToggleLabel;
			}
		}

		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x000151B9 File Offset: 0x000133B9
		public override string calibrateWindow_calibrateButtonLabel
		{
			get
			{
				return this._calibrateWindow_calibrateButtonLabel;
			}
		}

		// (get) Token: 0x06001B76 RID: 7030 RVA: 0x000151C1 File Offset: 0x000133C1
		public override string elementAssignmentReplacementWindowMessage
		{
			get
			{
				return "Button";
			}
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x000151C8 File Offset: 0x000133C8
		public override string GetControllerAssignmentConflictWindowMessage(string joystickName, string otherPlayerName, string currentPlayerName)
		{
			return string.Format(this._controllerAssignmentConflictWindowMessage, joystickName, otherPlayerName, currentPlayerName);
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x000151D8 File Offset: 0x000133D8
		public override string GetJoystickElementAssignmentPollingWindowMessage(string actionName)
		{
			return string.Format(this._joystickElementAssignmentPollingWindowMessage, actionName);
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x000151E6 File Offset: 0x000133E6
		public override string GetJoystickElementAssignmentPollingWindowMessage_FullAxisFieldOnly(string actionName)
		{
			return string.Format(this._joystickElementAssignmentPollingWindowMessage_fullAxisFieldOnly, actionName);
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x000151F4 File Offset: 0x000133F4
		public override string GetKeyboardElementAssignmentPollingWindowMessage(string actionName)
		{
			return string.Format(this._keyboardElementAssignmentPollingWindowMessage, actionName);
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x00015202 File Offset: 0x00013402
		public override string GetMouseElementAssignmentPollingWindowMessage(string actionName)
		{
			return string.Format(this._mouseElementAssignmentPollingWindowMessage, actionName);
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x00015210 File Offset: 0x00013410
		public override string GetMouseElementAssignmentPollingWindowMessage_FullAxisFieldOnly(string actionName)
		{
			return string.Format(this._mouseElementAssignmentPollingWindowMessage_fullAxisFieldOnly, actionName);
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x0001521E File Offset: 0x0001341E
		public override string GetElementAlreadyInUseBlocked(string elementName)
		{
			return string.Format(this._elementAlreadyInUseBlocked, elementName);
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x0001522C File Offset: 0x0001342C
		public override string GetElementAlreadyInUseCanReplace(string elementName, bool allowConflicts)
		{
			if (!allowConflicts)
			{
				return string.Format(this._elementAlreadyInUseCanReplace, elementName);
			}
			return string.Format(this._elementAlreadyInUseCanReplace_conflictAllowed, elementName);
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x0001524A File Offset: 0x0001344A
		public override string GetMouseAssignmentConflictWindowMessage(string otherPlayerName, string thisPlayerName)
		{
			return string.Format(this._mouseAssignmentConflictWindowMessage, otherPlayerName, thisPlayerName);
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x00015259 File Offset: 0x00013459
		public override string GetCalibrateAxisStep1WindowMessage(string axisName)
		{
			return string.Format(this._calibrateAxisStep1WindowMessage, axisName);
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x00015267 File Offset: 0x00013467
		public override string GetCalibrateAxisStep2WindowMessage(string axisName)
		{
			return string.Format(this._calibrateAxisStep2WindowMessage, axisName);
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x00015275 File Offset: 0x00013475
		public override string GetPlayerName(int playerId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				throw new ArgumentException("Invalid player id: " + playerId.ToString());
			}
			return player.descriptiveName;
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x000152A1 File Offset: 0x000134A1
		public override string GetControllerName(Controller controller)
		{
			if (controller == null)
			{
				throw new ArgumentNullException("controller");
			}
			return controller.name;
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x0006F6A0 File Offset: 0x0006D8A0
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

		// Token: 0x06001B85 RID: 7045 RVA: 0x00059D08 File Offset: 0x00057F08
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

		// Token: 0x06001B86 RID: 7046 RVA: 0x000152B7 File Offset: 0x000134B7
		public override string GetElementIdentifierName(KeyCode keyCode, ModifierKeyFlags modifierKeyFlags)
		{
			if (modifierKeyFlags != ModifierKeyFlags.None)
			{
				return string.Format("{0}{1}{2}", this.ModifierKeyFlagsToString(modifierKeyFlags), this._modifierKeys.separator, Keyboard.GetKeyName(keyCode));
			}
			return Keyboard.GetKeyName(keyCode);
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x000152E5 File Offset: 0x000134E5
		public override string GetActionName(int actionId)
		{
			InputAction action = ReInput.mapping.GetAction(actionId);
			if (action == null)
			{
				throw new ArgumentException("Invalid action id: " + actionId.ToString());
			}
			return action.descriptiveName;
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x0006F6F8 File Offset: 0x0006D8F8
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

		// Token: 0x06001B89 RID: 7049 RVA: 0x00015311 File Offset: 0x00013511
		public override string GetMapCategoryName(int id)
		{
			InputMapCategory mapCategory = ReInput.mapping.GetMapCategory(id);
			if (mapCategory == null)
			{
				throw new ArgumentException("Invalid map category id: " + id.ToString());
			}
			return mapCategory.descriptiveName;
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x0001533D File Offset: 0x0001353D
		public override string GetActionCategoryName(int id)
		{
			InputCategory actionCategory = ReInput.mapping.GetActionCategory(id);
			if (actionCategory == null)
			{
				throw new ArgumentException("Invalid action category id: " + id.ToString());
			}
			return actionCategory.descriptiveName;
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x00015369 File Offset: 0x00013569
		public override string GetLayoutName(ControllerType controllerType, int id)
		{
			InputLayout layout = ReInput.mapping.GetLayout(controllerType, id);
			if (layout == null)
			{
				throw new ArgumentException("Invalid " + controllerType.ToString() + " layout id: " + id.ToString());
			}
			return layout.descriptiveName;
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x0006F798 File Offset: 0x0006D998
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

		[SerializeField]
		private string _yes = "Yes";

		[SerializeField]
		private string _no = "No";

		[SerializeField]
		private string _add = "Add";

		[SerializeField]
		private string _replace = "Replace";

		[SerializeField]
		private string _remove = "Remove";

		[SerializeField]
		private string _swap = "Swap";

		[SerializeField]
		private string _cancel = "Cancel";

		[SerializeField]
		private string _none = "None";

		[SerializeField]
		private string _okay = "Okay";

		[SerializeField]
		private string _done = "Done";

		[SerializeField]
		private string _default = "Default";

		[SerializeField]
		private string _assignControllerWindowTitle = "Choose Controller";

		[SerializeField]
		private string _assignControllerWindowMessage = "Press any button or move an axis on the controller you would like to use.";

		[SerializeField]
		private string _controllerAssignmentConflictWindowTitle = "Controller Assignment";

		[SerializeField]
		[Tooltip("{0} = Joystick Name\n{1} = Other Player Name\n{2} = This Player Name")]
		private string _controllerAssignmentConflictWindowMessage = "{0} is already assigned to {1}. Do you want to assign this controller to {2} instead?";

		[SerializeField]
		private string _elementAssignmentPrePollingWindowMessage = "First center or zero all sticks and axes and press any button or wait for the timer to finish.";

		[SerializeField]
		[Tooltip("{0} = Action Name")]
		private string _joystickElementAssignmentPollingWindowMessage = "Now press a button or move an axis to assign it to {0}.";

		[SerializeField]
		[Tooltip("This text is only displayed when split-axis fields have been disabled and the user clicks on the full-axis field. Button/key/D-pad input cannot be assigned to a full-axis field.\n{0} = Action Name")]
		private string _joystickElementAssignmentPollingWindowMessage_fullAxisFieldOnly = "Now move an axis to assign it to {0}.";

		[SerializeField]
		[Tooltip("{0} = Action Name")]
		private string _keyboardElementAssignmentPollingWindowMessage = "Press a key to assign it to {0}. Modifier keys may also be used. To assign a modifier key alone, hold it down for 1 second.";

		[SerializeField]
		[Tooltip("{0} = Action Name")]
		private string _mouseElementAssignmentPollingWindowMessage = "Press a mouse button or move an axis to assign it to {0}.";

		[SerializeField]
		[Tooltip("This text is only displayed when split-axis fields have been disabled and the user clicks on the full-axis field. Button/key/D-pad input cannot be assigned to a full-axis field.\n{0} = Action Name")]
		private string _mouseElementAssignmentPollingWindowMessage_fullAxisFieldOnly = "Move an axis to assign it to {0}.";

		[SerializeField]
		private string _elementAssignmentConflictWindowMessage = "Assignment Conflict";

		[SerializeField]
		[Tooltip("{0} = Element Name")]
		private string _elementAlreadyInUseBlocked = "{0} is already in use cannot be replaced.";

		[SerializeField]
		[Tooltip("{0} = Element Name")]
		private string _elementAlreadyInUseCanReplace = "{0} is already in use. Do you want to replace it?";

		[SerializeField]
		[Tooltip("{0} = Element Name")]
		private string _elementAlreadyInUseCanReplace_conflictAllowed = "{0} is already in use. Do you want to replace it? You may also choose to add the assignment anyway.";

		[SerializeField]
		private string _mouseAssignmentConflictWindowTitle = "Mouse Assignment";

		[SerializeField]
		[Tooltip("{0} = Other Player Name\n{1} = This Player Name")]
		private string _mouseAssignmentConflictWindowMessage = "The mouse is already assigned to {0}. Do you want to assign the mouse to {1} instead?";

		[SerializeField]
		private string _calibrateControllerWindowTitle = "Calibrate Controller";

		[SerializeField]
		private string _calibrateAxisStep1WindowTitle = "Calibrate Zero";

		[SerializeField]
		[Tooltip("{0} = Axis Name")]
		private string _calibrateAxisStep1WindowMessage = "Center or zero {0} and press any button or wait for the timer to finish.";

		[SerializeField]
		private string _calibrateAxisStep2WindowTitle = "Calibrate Range";

		[SerializeField]
		[Tooltip("{0} = Axis Name")]
		private string _calibrateAxisStep2WindowMessage = "Move {0} through its entire range then press any button or wait for the timer to finish.";

		[SerializeField]
		private string _inputBehaviorSettingsWindowTitle = "Sensitivity Settings";

		[SerializeField]
		private string _restoreDefaultsWindowTitle = "Restore Defaults";

		[SerializeField]
		[Tooltip("Message for a single player game.")]
		private string _restoreDefaultsWindowMessage_onePlayer = "This will restore the default input configuration. Are you sure you want to do this?";

		[SerializeField]
		[Tooltip("Message for a multi-player game.")]
		private string _restoreDefaultsWindowMessage_multiPlayer = "This will restore the default input configuration for all players. Are you sure you want to do this?";

		[SerializeField]
		private string _actionColumnLabel = "Actions";

		[SerializeField]
		private string _keyboardColumnLabel = "Keyboard";

		[SerializeField]
		private string _mouseColumnLabel = "Mouse";

		[SerializeField]
		private string _controllerColumnLabel = "Controller";

		[SerializeField]
		private string _removeControllerButtonLabel = "Remove";

		[SerializeField]
		private string _calibrateControllerButtonLabel = "Calibrate";

		[SerializeField]
		private string _assignControllerButtonLabel = "Assign Controller";

		[SerializeField]
		private string _inputBehaviorSettingsButtonLabel = "Sensitivity";

		[SerializeField]
		private string _doneButtonLabel = "Done";

		[SerializeField]
		private string _restoreDefaultsButtonLabel = "Restore Defaults";

		[SerializeField]
		private string _playersGroupLabel = "Players:";

		[SerializeField]
		private string _controllerSettingsGroupLabel = "Controller:";

		[SerializeField]
		private string _assignedControllersGroupLabel = "Assigned Controllers:";

		[SerializeField]
		private string _settingsGroupLabel = "Settings:";

		[SerializeField]
		private string _mapCategoriesGroupLabel = "Categories:";

		[SerializeField]
		private string _calibrateWindow_deadZoneSliderLabel = "Dead Zone:";

		[SerializeField]
		private string _calibrateWindow_zeroSliderLabel = "Zero:";

		[SerializeField]
		private string _calibrateWindow_sensitivitySliderLabel = "Sensitivity:";

		[SerializeField]
		private string _calibrateWindow_invertToggleLabel = "Invert";

		[SerializeField]
		private string _calibrateWindow_calibrateButtonLabel = "Calibrate";

		[SerializeField]
		private LanguageData.ModifierKeys _modifierKeys;

		[SerializeField]
		private LanguageData.CustomEntry[] _customEntries;

		private bool _initialized;

		private Dictionary<string, string> customDict;

		[Serializable]
		protected class CustomEntry
		{
			// Token: 0x06001B8E RID: 7054 RVA: 0x000022AD File Offset: 0x000004AD
			public CustomEntry()
			{
			}

			// Token: 0x06001B8F RID: 7055 RVA: 0x000153A8 File Offset: 0x000135A8
			public CustomEntry(string key, string value)
			{
				this.key = key;
				this.value = value;
			}

			// Token: 0x06001B90 RID: 7056 RVA: 0x0006FB24 File Offset: 0x0006DD24
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

			public string key;

			public string value;
		}

		[Serializable]
		protected class ModifierKeys
		{
			public string control = "Control";

			public string alt = "Alt";

			public string shift = "Shift";

			public string command = "Command";

			public string separator = " + ";
		}
	}
}
