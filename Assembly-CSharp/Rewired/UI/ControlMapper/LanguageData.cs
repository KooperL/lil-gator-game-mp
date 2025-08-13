using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000461 RID: 1121
	[Serializable]
	public class LanguageData : LanguageDataBase
	{
		// Token: 0x06001AE7 RID: 6887 RVA: 0x00014C13 File Offset: 0x00012E13
		public override void Initialize()
		{
			if (this._initialized)
			{
				return;
			}
			this.customDict = LanguageData.CustomEntry.ToDictionary(this._customEntries);
			this._initialized = true;
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x0006D37C File Offset: 0x0006B57C
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

		// Token: 0x06001AE9 RID: 6889 RVA: 0x00014C36 File Offset: 0x00012E36
		public override bool ContainsCustomEntryKey(string key)
		{
			return !string.IsNullOrEmpty(key) && this.customDict.ContainsKey(key);
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x00014C4E File Offset: 0x00012E4E
		public override string yes
		{
			get
			{
				return this._yes;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x00014C56 File Offset: 0x00012E56
		public override string no
		{
			get
			{
				return this._no;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x00014C5E File Offset: 0x00012E5E
		public override string add
		{
			get
			{
				return this._add;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001AED RID: 6893 RVA: 0x00014C66 File Offset: 0x00012E66
		public override string replace
		{
			get
			{
				return this._replace;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x00014C6E File Offset: 0x00012E6E
		public override string remove
		{
			get
			{
				return this._remove;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x00014C76 File Offset: 0x00012E76
		public override string swap
		{
			get
			{
				return this._swap;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x00014C7E File Offset: 0x00012E7E
		public override string cancel
		{
			get
			{
				return this._cancel;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x00014C86 File Offset: 0x00012E86
		public override string none
		{
			get
			{
				return this._none;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x00014C8E File Offset: 0x00012E8E
		public override string okay
		{
			get
			{
				return this._okay;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x00014C96 File Offset: 0x00012E96
		public override string done
		{
			get
			{
				return this._done;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x00014C9E File Offset: 0x00012E9E
		public override string default_
		{
			get
			{
				return this._default;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x00014CA6 File Offset: 0x00012EA6
		public override string assignControllerWindowTitle
		{
			get
			{
				return this._assignControllerWindowTitle;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001AF6 RID: 6902 RVA: 0x00014CAE File Offset: 0x00012EAE
		public override string assignControllerWindowMessage
		{
			get
			{
				return this._assignControllerWindowMessage;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x00014CB6 File Offset: 0x00012EB6
		public override string controllerAssignmentConflictWindowTitle
		{
			get
			{
				return this._controllerAssignmentConflictWindowTitle;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001AF8 RID: 6904 RVA: 0x00014CBE File Offset: 0x00012EBE
		public override string elementAssignmentPrePollingWindowMessage
		{
			get
			{
				return this._elementAssignmentPrePollingWindowMessage;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x00014CC6 File Offset: 0x00012EC6
		public override string elementAssignmentConflictWindowMessage
		{
			get
			{
				return this._elementAssignmentConflictWindowMessage;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001AFA RID: 6906 RVA: 0x00014CCE File Offset: 0x00012ECE
		public override string mouseAssignmentConflictWindowTitle
		{
			get
			{
				return this._mouseAssignmentConflictWindowTitle;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x00014CD6 File Offset: 0x00012ED6
		public override string calibrateControllerWindowTitle
		{
			get
			{
				return this._calibrateControllerWindowTitle;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001AFC RID: 6908 RVA: 0x00014CDE File Offset: 0x00012EDE
		public override string calibrateAxisStep1WindowTitle
		{
			get
			{
				return this._calibrateAxisStep1WindowTitle;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x00014CE6 File Offset: 0x00012EE6
		public override string calibrateAxisStep2WindowTitle
		{
			get
			{
				return this._calibrateAxisStep2WindowTitle;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001AFE RID: 6910 RVA: 0x00014CEE File Offset: 0x00012EEE
		public override string inputBehaviorSettingsWindowTitle
		{
			get
			{
				return this._inputBehaviorSettingsWindowTitle;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x00014CF6 File Offset: 0x00012EF6
		public override string restoreDefaultsWindowTitle
		{
			get
			{
				return this._restoreDefaultsWindowTitle;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x00014CFE File Offset: 0x00012EFE
		public override string actionColumnLabel
		{
			get
			{
				return this._actionColumnLabel;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x00014D06 File Offset: 0x00012F06
		public override string keyboardColumnLabel
		{
			get
			{
				return this._keyboardColumnLabel;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x00014D0E File Offset: 0x00012F0E
		public override string mouseColumnLabel
		{
			get
			{
				return this._mouseColumnLabel;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001B03 RID: 6915 RVA: 0x00014D16 File Offset: 0x00012F16
		public override string controllerColumnLabel
		{
			get
			{
				return this._controllerColumnLabel;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001B04 RID: 6916 RVA: 0x00014D1E File Offset: 0x00012F1E
		public override string removeControllerButtonLabel
		{
			get
			{
				return this._removeControllerButtonLabel;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001B05 RID: 6917 RVA: 0x00014D26 File Offset: 0x00012F26
		public override string calibrateControllerButtonLabel
		{
			get
			{
				return this._calibrateControllerButtonLabel;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001B06 RID: 6918 RVA: 0x00014D2E File Offset: 0x00012F2E
		public override string assignControllerButtonLabel
		{
			get
			{
				return this._assignControllerButtonLabel;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001B07 RID: 6919 RVA: 0x00014D36 File Offset: 0x00012F36
		public override string inputBehaviorSettingsButtonLabel
		{
			get
			{
				return this._inputBehaviorSettingsButtonLabel;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001B08 RID: 6920 RVA: 0x00014D3E File Offset: 0x00012F3E
		public override string doneButtonLabel
		{
			get
			{
				return this._doneButtonLabel;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x00014D46 File Offset: 0x00012F46
		public override string restoreDefaultsButtonLabel
		{
			get
			{
				return this._restoreDefaultsButtonLabel;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x00014D4E File Offset: 0x00012F4E
		public override string controllerSettingsGroupLabel
		{
			get
			{
				return this._controllerSettingsGroupLabel;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x00014D56 File Offset: 0x00012F56
		public override string playersGroupLabel
		{
			get
			{
				return this._playersGroupLabel;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x00014D5E File Offset: 0x00012F5E
		public override string assignedControllersGroupLabel
		{
			get
			{
				return this._assignedControllersGroupLabel;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x00014D66 File Offset: 0x00012F66
		public override string settingsGroupLabel
		{
			get
			{
				return this._settingsGroupLabel;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x00014D6E File Offset: 0x00012F6E
		public override string mapCategoriesGroupLabel
		{
			get
			{
				return this._mapCategoriesGroupLabel;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x00014D76 File Offset: 0x00012F76
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

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001B10 RID: 6928 RVA: 0x00014D92 File Offset: 0x00012F92
		public override string calibrateWindow_deadZoneSliderLabel
		{
			get
			{
				return this._calibrateWindow_deadZoneSliderLabel;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x00014D9A File Offset: 0x00012F9A
		public override string calibrateWindow_zeroSliderLabel
		{
			get
			{
				return this._calibrateWindow_zeroSliderLabel;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x00014DA2 File Offset: 0x00012FA2
		public override string calibrateWindow_sensitivitySliderLabel
		{
			get
			{
				return this._calibrateWindow_sensitivitySliderLabel;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001B13 RID: 6931 RVA: 0x00014DAA File Offset: 0x00012FAA
		public override string calibrateWindow_invertToggleLabel
		{
			get
			{
				return this._calibrateWindow_invertToggleLabel;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001B14 RID: 6932 RVA: 0x00014DB2 File Offset: 0x00012FB2
		public override string calibrateWindow_calibrateButtonLabel
		{
			get
			{
				return this._calibrateWindow_calibrateButtonLabel;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x00014DBA File Offset: 0x00012FBA
		public override string elementAssignmentReplacementWindowMessage
		{
			get
			{
				return "Button";
			}
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x00014DC1 File Offset: 0x00012FC1
		public override string GetControllerAssignmentConflictWindowMessage(string joystickName, string otherPlayerName, string currentPlayerName)
		{
			return string.Format(this._controllerAssignmentConflictWindowMessage, joystickName, otherPlayerName, currentPlayerName);
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00014DD1 File Offset: 0x00012FD1
		public override string GetJoystickElementAssignmentPollingWindowMessage(string actionName)
		{
			return string.Format(this._joystickElementAssignmentPollingWindowMessage, actionName);
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x00014DDF File Offset: 0x00012FDF
		public override string GetJoystickElementAssignmentPollingWindowMessage_FullAxisFieldOnly(string actionName)
		{
			return string.Format(this._joystickElementAssignmentPollingWindowMessage_fullAxisFieldOnly, actionName);
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x00014DED File Offset: 0x00012FED
		public override string GetKeyboardElementAssignmentPollingWindowMessage(string actionName)
		{
			return string.Format(this._keyboardElementAssignmentPollingWindowMessage, actionName);
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x00014DFB File Offset: 0x00012FFB
		public override string GetMouseElementAssignmentPollingWindowMessage(string actionName)
		{
			return string.Format(this._mouseElementAssignmentPollingWindowMessage, actionName);
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x00014E09 File Offset: 0x00013009
		public override string GetMouseElementAssignmentPollingWindowMessage_FullAxisFieldOnly(string actionName)
		{
			return string.Format(this._mouseElementAssignmentPollingWindowMessage_fullAxisFieldOnly, actionName);
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x00014E17 File Offset: 0x00013017
		public override string GetElementAlreadyInUseBlocked(string elementName)
		{
			return string.Format(this._elementAlreadyInUseBlocked, elementName);
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x00014E25 File Offset: 0x00013025
		public override string GetElementAlreadyInUseCanReplace(string elementName, bool allowConflicts)
		{
			if (!allowConflicts)
			{
				return string.Format(this._elementAlreadyInUseCanReplace, elementName);
			}
			return string.Format(this._elementAlreadyInUseCanReplace_conflictAllowed, elementName);
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x00014E43 File Offset: 0x00013043
		public override string GetMouseAssignmentConflictWindowMessage(string otherPlayerName, string thisPlayerName)
		{
			return string.Format(this._mouseAssignmentConflictWindowMessage, otherPlayerName, thisPlayerName);
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x00014E52 File Offset: 0x00013052
		public override string GetCalibrateAxisStep1WindowMessage(string axisName)
		{
			return string.Format(this._calibrateAxisStep1WindowMessage, axisName);
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x00014E60 File Offset: 0x00013060
		public override string GetCalibrateAxisStep2WindowMessage(string axisName)
		{
			return string.Format(this._calibrateAxisStep2WindowMessage, axisName);
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x00014E6E File Offset: 0x0001306E
		public override string GetPlayerName(int playerId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				throw new ArgumentException("Invalid player id: " + playerId.ToString());
			}
			return player.descriptiveName;
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x00014E9A File Offset: 0x0001309A
		public override string GetControllerName(Controller controller)
		{
			if (controller == null)
			{
				throw new ArgumentNullException("controller");
			}
			return controller.name;
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0006D3B0 File Offset: 0x0006B5B0
		public override string GetElementIdentifierName(ActionElementMap actionElementMap)
		{
			if (actionElementMap == null)
			{
				throw new ArgumentNullException("actionElementMap");
			}
			if (actionElementMap.controllerMap.controllerType == null)
			{
				return this.GetElementIdentifierName(actionElementMap.keyCode, actionElementMap.modifierKeyFlags);
			}
			return this.GetElementIdentifierName(actionElementMap.controllerMap.controller, actionElementMap.elementIdentifierId, actionElementMap.axisRange);
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x00057A80 File Offset: 0x00055C80
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
			if (type == null)
			{
				return elementIdentifierById.GetDisplayName(elementById.type, axisRange);
			}
			if (type != 1)
			{
				return elementIdentifierById.name;
			}
			return elementIdentifierById.name;
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x00014EB0 File Offset: 0x000130B0
		public override string GetElementIdentifierName(KeyCode keyCode, ModifierKeyFlags modifierKeyFlags)
		{
			if (modifierKeyFlags != null)
			{
				return string.Format("{0}{1}{2}", this.ModifierKeyFlagsToString(modifierKeyFlags), this._modifierKeys.separator, Keyboard.GetKeyName(keyCode));
			}
			return Keyboard.GetKeyName(keyCode);
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x00014EDE File Offset: 0x000130DE
		public override string GetActionName(int actionId)
		{
			InputAction action = ReInput.mapping.GetAction(actionId);
			if (action == null)
			{
				throw new ArgumentException("Invalid action id: " + actionId.ToString());
			}
			return action.descriptiveName;
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x0006D408 File Offset: 0x0006B608
		public override string GetActionName(int actionId, AxisRange axisRange)
		{
			InputAction action = ReInput.mapping.GetAction(actionId);
			if (action == null)
			{
				throw new ArgumentException("Invalid action id: " + actionId.ToString());
			}
			switch (axisRange)
			{
			case 0:
				return action.descriptiveName;
			case 1:
				if (string.IsNullOrEmpty(action.positiveDescriptiveName))
				{
					return action.descriptiveName + " +";
				}
				return action.positiveDescriptiveName;
			case 2:
				if (string.IsNullOrEmpty(action.negativeDescriptiveName))
				{
					return action.descriptiveName + " -";
				}
				return action.negativeDescriptiveName;
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x00014F0A File Offset: 0x0001310A
		public override string GetMapCategoryName(int id)
		{
			InputMapCategory mapCategory = ReInput.mapping.GetMapCategory(id);
			if (mapCategory == null)
			{
				throw new ArgumentException("Invalid map category id: " + id.ToString());
			}
			return mapCategory.descriptiveName;
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x00014F36 File Offset: 0x00013136
		public override string GetActionCategoryName(int id)
		{
			InputCategory actionCategory = ReInput.mapping.GetActionCategory(id);
			if (actionCategory == null)
			{
				throw new ArgumentException("Invalid action category id: " + id.ToString());
			}
			return actionCategory.descriptiveName;
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x00014F62 File Offset: 0x00013162
		public override string GetLayoutName(ControllerType controllerType, int id)
		{
			InputLayout layout = ReInput.mapping.GetLayout(controllerType, id);
			if (layout == null)
			{
				throw new ArgumentException("Invalid " + controllerType.ToString() + " layout id: " + id.ToString());
			}
			return layout.descriptiveName;
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x0006D4A8 File Offset: 0x0006B6A8
		public override string ModifierKeyFlagsToString(ModifierKeyFlags flags)
		{
			int num = 0;
			string text = string.Empty;
			if (Keyboard.ModifierKeyFlagsContain(flags, 1))
			{
				text += this._modifierKeys.control;
				num++;
			}
			if (Keyboard.ModifierKeyFlagsContain(flags, 4))
			{
				if (num > 0 && !string.IsNullOrEmpty(this._modifierKeys.separator))
				{
					text += this._modifierKeys.separator;
				}
				text += this._modifierKeys.command;
				num++;
			}
			if (Keyboard.ModifierKeyFlagsContain(flags, 2))
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
			if (Keyboard.ModifierKeyFlagsContain(flags, 3))
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

		// Token: 0x04001D26 RID: 7462
		[SerializeField]
		private string _yes = "Yes";

		// Token: 0x04001D27 RID: 7463
		[SerializeField]
		private string _no = "No";

		// Token: 0x04001D28 RID: 7464
		[SerializeField]
		private string _add = "Add";

		// Token: 0x04001D29 RID: 7465
		[SerializeField]
		private string _replace = "Replace";

		// Token: 0x04001D2A RID: 7466
		[SerializeField]
		private string _remove = "Remove";

		// Token: 0x04001D2B RID: 7467
		[SerializeField]
		private string _swap = "Swap";

		// Token: 0x04001D2C RID: 7468
		[SerializeField]
		private string _cancel = "Cancel";

		// Token: 0x04001D2D RID: 7469
		[SerializeField]
		private string _none = "None";

		// Token: 0x04001D2E RID: 7470
		[SerializeField]
		private string _okay = "Okay";

		// Token: 0x04001D2F RID: 7471
		[SerializeField]
		private string _done = "Done";

		// Token: 0x04001D30 RID: 7472
		[SerializeField]
		private string _default = "Default";

		// Token: 0x04001D31 RID: 7473
		[SerializeField]
		private string _assignControllerWindowTitle = "Choose Controller";

		// Token: 0x04001D32 RID: 7474
		[SerializeField]
		private string _assignControllerWindowMessage = "Press any button or move an axis on the controller you would like to use.";

		// Token: 0x04001D33 RID: 7475
		[SerializeField]
		private string _controllerAssignmentConflictWindowTitle = "Controller Assignment";

		// Token: 0x04001D34 RID: 7476
		[SerializeField]
		[Tooltip("{0} = Joystick Name\n{1} = Other Player Name\n{2} = This Player Name")]
		private string _controllerAssignmentConflictWindowMessage = "{0} is already assigned to {1}. Do you want to assign this controller to {2} instead?";

		// Token: 0x04001D35 RID: 7477
		[SerializeField]
		private string _elementAssignmentPrePollingWindowMessage = "First center or zero all sticks and axes and press any button or wait for the timer to finish.";

		// Token: 0x04001D36 RID: 7478
		[SerializeField]
		[Tooltip("{0} = Action Name")]
		private string _joystickElementAssignmentPollingWindowMessage = "Now press a button or move an axis to assign it to {0}.";

		// Token: 0x04001D37 RID: 7479
		[SerializeField]
		[Tooltip("This text is only displayed when split-axis fields have been disabled and the user clicks on the full-axis field. Button/key/D-pad input cannot be assigned to a full-axis field.\n{0} = Action Name")]
		private string _joystickElementAssignmentPollingWindowMessage_fullAxisFieldOnly = "Now move an axis to assign it to {0}.";

		// Token: 0x04001D38 RID: 7480
		[SerializeField]
		[Tooltip("{0} = Action Name")]
		private string _keyboardElementAssignmentPollingWindowMessage = "Press a key to assign it to {0}. Modifier keys may also be used. To assign a modifier key alone, hold it down for 1 second.";

		// Token: 0x04001D39 RID: 7481
		[SerializeField]
		[Tooltip("{0} = Action Name")]
		private string _mouseElementAssignmentPollingWindowMessage = "Press a mouse button or move an axis to assign it to {0}.";

		// Token: 0x04001D3A RID: 7482
		[SerializeField]
		[Tooltip("This text is only displayed when split-axis fields have been disabled and the user clicks on the full-axis field. Button/key/D-pad input cannot be assigned to a full-axis field.\n{0} = Action Name")]
		private string _mouseElementAssignmentPollingWindowMessage_fullAxisFieldOnly = "Move an axis to assign it to {0}.";

		// Token: 0x04001D3B RID: 7483
		[SerializeField]
		private string _elementAssignmentConflictWindowMessage = "Assignment Conflict";

		// Token: 0x04001D3C RID: 7484
		[SerializeField]
		[Tooltip("{0} = Element Name")]
		private string _elementAlreadyInUseBlocked = "{0} is already in use cannot be replaced.";

		// Token: 0x04001D3D RID: 7485
		[SerializeField]
		[Tooltip("{0} = Element Name")]
		private string _elementAlreadyInUseCanReplace = "{0} is already in use. Do you want to replace it?";

		// Token: 0x04001D3E RID: 7486
		[SerializeField]
		[Tooltip("{0} = Element Name")]
		private string _elementAlreadyInUseCanReplace_conflictAllowed = "{0} is already in use. Do you want to replace it? You may also choose to add the assignment anyway.";

		// Token: 0x04001D3F RID: 7487
		[SerializeField]
		private string _mouseAssignmentConflictWindowTitle = "Mouse Assignment";

		// Token: 0x04001D40 RID: 7488
		[SerializeField]
		[Tooltip("{0} = Other Player Name\n{1} = This Player Name")]
		private string _mouseAssignmentConflictWindowMessage = "The mouse is already assigned to {0}. Do you want to assign the mouse to {1} instead?";

		// Token: 0x04001D41 RID: 7489
		[SerializeField]
		private string _calibrateControllerWindowTitle = "Calibrate Controller";

		// Token: 0x04001D42 RID: 7490
		[SerializeField]
		private string _calibrateAxisStep1WindowTitle = "Calibrate Zero";

		// Token: 0x04001D43 RID: 7491
		[SerializeField]
		[Tooltip("{0} = Axis Name")]
		private string _calibrateAxisStep1WindowMessage = "Center or zero {0} and press any button or wait for the timer to finish.";

		// Token: 0x04001D44 RID: 7492
		[SerializeField]
		private string _calibrateAxisStep2WindowTitle = "Calibrate Range";

		// Token: 0x04001D45 RID: 7493
		[SerializeField]
		[Tooltip("{0} = Axis Name")]
		private string _calibrateAxisStep2WindowMessage = "Move {0} through its entire range then press any button or wait for the timer to finish.";

		// Token: 0x04001D46 RID: 7494
		[SerializeField]
		private string _inputBehaviorSettingsWindowTitle = "Sensitivity Settings";

		// Token: 0x04001D47 RID: 7495
		[SerializeField]
		private string _restoreDefaultsWindowTitle = "Restore Defaults";

		// Token: 0x04001D48 RID: 7496
		[SerializeField]
		[Tooltip("Message for a single player game.")]
		private string _restoreDefaultsWindowMessage_onePlayer = "This will restore the default input configuration. Are you sure you want to do this?";

		// Token: 0x04001D49 RID: 7497
		[SerializeField]
		[Tooltip("Message for a multi-player game.")]
		private string _restoreDefaultsWindowMessage_multiPlayer = "This will restore the default input configuration for all players. Are you sure you want to do this?";

		// Token: 0x04001D4A RID: 7498
		[SerializeField]
		private string _actionColumnLabel = "Actions";

		// Token: 0x04001D4B RID: 7499
		[SerializeField]
		private string _keyboardColumnLabel = "Keyboard";

		// Token: 0x04001D4C RID: 7500
		[SerializeField]
		private string _mouseColumnLabel = "Mouse";

		// Token: 0x04001D4D RID: 7501
		[SerializeField]
		private string _controllerColumnLabel = "Controller";

		// Token: 0x04001D4E RID: 7502
		[SerializeField]
		private string _removeControllerButtonLabel = "Remove";

		// Token: 0x04001D4F RID: 7503
		[SerializeField]
		private string _calibrateControllerButtonLabel = "Calibrate";

		// Token: 0x04001D50 RID: 7504
		[SerializeField]
		private string _assignControllerButtonLabel = "Assign Controller";

		// Token: 0x04001D51 RID: 7505
		[SerializeField]
		private string _inputBehaviorSettingsButtonLabel = "Sensitivity";

		// Token: 0x04001D52 RID: 7506
		[SerializeField]
		private string _doneButtonLabel = "Done";

		// Token: 0x04001D53 RID: 7507
		[SerializeField]
		private string _restoreDefaultsButtonLabel = "Restore Defaults";

		// Token: 0x04001D54 RID: 7508
		[SerializeField]
		private string _playersGroupLabel = "Players:";

		// Token: 0x04001D55 RID: 7509
		[SerializeField]
		private string _controllerSettingsGroupLabel = "Controller:";

		// Token: 0x04001D56 RID: 7510
		[SerializeField]
		private string _assignedControllersGroupLabel = "Assigned Controllers:";

		// Token: 0x04001D57 RID: 7511
		[SerializeField]
		private string _settingsGroupLabel = "Settings:";

		// Token: 0x04001D58 RID: 7512
		[SerializeField]
		private string _mapCategoriesGroupLabel = "Categories:";

		// Token: 0x04001D59 RID: 7513
		[SerializeField]
		private string _calibrateWindow_deadZoneSliderLabel = "Dead Zone:";

		// Token: 0x04001D5A RID: 7514
		[SerializeField]
		private string _calibrateWindow_zeroSliderLabel = "Zero:";

		// Token: 0x04001D5B RID: 7515
		[SerializeField]
		private string _calibrateWindow_sensitivitySliderLabel = "Sensitivity:";

		// Token: 0x04001D5C RID: 7516
		[SerializeField]
		private string _calibrateWindow_invertToggleLabel = "Invert";

		// Token: 0x04001D5D RID: 7517
		[SerializeField]
		private string _calibrateWindow_calibrateButtonLabel = "Calibrate";

		// Token: 0x04001D5E RID: 7518
		[SerializeField]
		private LanguageData.ModifierKeys _modifierKeys;

		// Token: 0x04001D5F RID: 7519
		[SerializeField]
		private LanguageData.CustomEntry[] _customEntries;

		// Token: 0x04001D60 RID: 7520
		private bool _initialized;

		// Token: 0x04001D61 RID: 7521
		private Dictionary<string, string> customDict;

		// Token: 0x02000462 RID: 1122
		[Serializable]
		protected class CustomEntry
		{
			// Token: 0x06001B2D RID: 6957 RVA: 0x0000227F File Offset: 0x0000047F
			public CustomEntry()
			{
			}

			// Token: 0x06001B2E RID: 6958 RVA: 0x00014FA1 File Offset: 0x000131A1
			public CustomEntry(string key, string value)
			{
				this.key = key;
				this.value = value;
			}

			// Token: 0x06001B2F RID: 6959 RVA: 0x0006D834 File Offset: 0x0006BA34
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

			// Token: 0x04001D62 RID: 7522
			public string key;

			// Token: 0x04001D63 RID: 7523
			public string value;
		}

		// Token: 0x02000463 RID: 1123
		[Serializable]
		protected class ModifierKeys
		{
			// Token: 0x04001D64 RID: 7524
			public string control = "Control";

			// Token: 0x04001D65 RID: 7525
			public string alt = "Alt";

			// Token: 0x04001D66 RID: 7526
			public string shift = "Shift";

			// Token: 0x04001D67 RID: 7527
			public string command = "Command";

			// Token: 0x04001D68 RID: 7528
			public string separator = " + ";
		}
	}
}
