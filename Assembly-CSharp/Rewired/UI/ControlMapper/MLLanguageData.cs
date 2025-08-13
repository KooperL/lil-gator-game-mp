using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000465 RID: 1125
	[CreateAssetMenu(menuName = "Rewired/MLLanguageData")]
	[Serializable]
	public class MLLanguageData : LanguageDataBase
	{
		// Token: 0x06001B77 RID: 7031 RVA: 0x00014FF6 File Offset: 0x000131F6
		public override void Initialize()
		{
			if (this._initialized)
			{
				return;
			}
			this.customDict = MLLanguageData.CustomEntry.ToDictionary(this._customEntries);
			this._initialized = true;
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x0006D8C8 File Offset: 0x0006BAC8
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

		// Token: 0x06001B79 RID: 7033 RVA: 0x00015019 File Offset: 0x00013219
		public override bool ContainsCustomEntryKey(string key)
		{
			return !string.IsNullOrEmpty(key) && this.customDict.ContainsKey(key);
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001B7A RID: 7034 RVA: 0x00015031 File Offset: 0x00013231
		public override string yes
		{
			get
			{
				return this.document.FetchString(this._yes, Language.English);
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001B7B RID: 7035 RVA: 0x00015045 File Offset: 0x00013245
		public override string no
		{
			get
			{
				return this.document.FetchString(this._no, Language.English);
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001B7C RID: 7036 RVA: 0x00015059 File Offset: 0x00013259
		public override string add
		{
			get
			{
				return this.document.FetchString(this._add, Language.English);
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x0001506D File Offset: 0x0001326D
		public override string replace
		{
			get
			{
				return this.document.FetchString(this._replace, Language.English);
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x00015081 File Offset: 0x00013281
		public override string remove
		{
			get
			{
				return this.document.FetchString(this._remove, Language.English);
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001B7F RID: 7039 RVA: 0x00015095 File Offset: 0x00013295
		public override string swap
		{
			get
			{
				return this.document.FetchString(this._swap, Language.English);
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x000150A9 File Offset: 0x000132A9
		public override string cancel
		{
			get
			{
				return this.document.FetchString(this._cancel, Language.English);
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001B81 RID: 7041 RVA: 0x000150BD File Offset: 0x000132BD
		public override string none
		{
			get
			{
				return this.document.FetchString(this._none, Language.English);
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x000150D1 File Offset: 0x000132D1
		public override string okay
		{
			get
			{
				return this.document.FetchString(this._okay, Language.English);
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x000150E5 File Offset: 0x000132E5
		public override string done
		{
			get
			{
				return this.document.FetchString(this._done, Language.English);
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001B84 RID: 7044 RVA: 0x000150F9 File Offset: 0x000132F9
		public override string default_
		{
			get
			{
				return this.document.FetchString(this._default, Language.English);
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x0001510D File Offset: 0x0001330D
		public override string assignControllerWindowTitle
		{
			get
			{
				return this.document.FetchString(this._assignControllerWindowTitle, Language.English);
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x00015121 File Offset: 0x00013321
		public override string assignControllerWindowMessage
		{
			get
			{
				return this.document.FetchString(this._assignControllerWindowMessage, Language.English);
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x00015135 File Offset: 0x00013335
		public override string controllerAssignmentConflictWindowTitle
		{
			get
			{
				return this.document.FetchString(this._controllerAssignmentConflictWindowTitle, Language.English);
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001B88 RID: 7048 RVA: 0x00015149 File Offset: 0x00013349
		public override string elementAssignmentPrePollingWindowMessage
		{
			get
			{
				return this.document.FetchString(this._elementAssignmentPrePollingWindowMessage, Language.English);
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x0001515D File Offset: 0x0001335D
		public override string elementAssignmentConflictWindowMessage
		{
			get
			{
				return this.document.FetchString(this._elementAssignmentConflictWindowMessage, Language.English);
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001B8A RID: 7050 RVA: 0x00015171 File Offset: 0x00013371
		public override string elementAssignmentReplacementWindowMessage
		{
			get
			{
				return this.document.FetchString(this._elementAssignmentReplacementWindowMessage, Language.English);
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x00015185 File Offset: 0x00013385
		public override string mouseAssignmentConflictWindowTitle
		{
			get
			{
				return this.document.FetchString(this._mouseAssignmentConflictWindowTitle, Language.English);
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001B8C RID: 7052 RVA: 0x00015199 File Offset: 0x00013399
		public override string calibrateControllerWindowTitle
		{
			get
			{
				return this.document.FetchString(this._calibrateControllerWindowTitle, Language.English);
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x000151AD File Offset: 0x000133AD
		public override string calibrateAxisStep1WindowTitle
		{
			get
			{
				return this.document.FetchString(this._calibrateAxisStep1WindowTitle, Language.English);
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001B8E RID: 7054 RVA: 0x000151C1 File Offset: 0x000133C1
		public override string calibrateAxisStep2WindowTitle
		{
			get
			{
				return this.document.FetchString(this._calibrateAxisStep2WindowTitle, Language.English);
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001B8F RID: 7055 RVA: 0x000151D5 File Offset: 0x000133D5
		public override string inputBehaviorSettingsWindowTitle
		{
			get
			{
				return this.document.FetchString(this._inputBehaviorSettingsWindowTitle, Language.English);
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x000151E9 File Offset: 0x000133E9
		public override string restoreDefaultsWindowTitle
		{
			get
			{
				return this.document.FetchString(this._restoreDefaultsWindowTitle, Language.English);
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001B91 RID: 7057 RVA: 0x000151FD File Offset: 0x000133FD
		public override string actionColumnLabel
		{
			get
			{
				return this.document.FetchString(this._actionColumnLabel, Language.English);
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001B92 RID: 7058 RVA: 0x00015211 File Offset: 0x00013411
		public override string keyboardColumnLabel
		{
			get
			{
				return this.document.FetchString(this._keyboardColumnLabel, Language.English);
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x00015225 File Offset: 0x00013425
		public override string mouseColumnLabel
		{
			get
			{
				return this.document.FetchString(this._mouseColumnLabel, Language.English);
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x00015239 File Offset: 0x00013439
		public override string controllerColumnLabel
		{
			get
			{
				return this.document.FetchString(this._controllerColumnLabel, Language.English);
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001B95 RID: 7061 RVA: 0x0001524D File Offset: 0x0001344D
		public override string removeControllerButtonLabel
		{
			get
			{
				return this.document.FetchString(this._removeControllerButtonLabel, Language.English);
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001B96 RID: 7062 RVA: 0x00015261 File Offset: 0x00013461
		public override string calibrateControllerButtonLabel
		{
			get
			{
				return this.document.FetchString(this._calibrateControllerButtonLabel, Language.English);
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001B97 RID: 7063 RVA: 0x00015275 File Offset: 0x00013475
		public override string assignControllerButtonLabel
		{
			get
			{
				return this.document.FetchString(this._assignControllerButtonLabel, Language.English);
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001B98 RID: 7064 RVA: 0x00015289 File Offset: 0x00013489
		public override string inputBehaviorSettingsButtonLabel
		{
			get
			{
				return this.document.FetchString(this._inputBehaviorSettingsButtonLabel, Language.English);
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x0001529D File Offset: 0x0001349D
		public override string doneButtonLabel
		{
			get
			{
				return this.document.FetchString(this._doneButtonLabel, Language.English);
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001B9A RID: 7066 RVA: 0x000152B1 File Offset: 0x000134B1
		public override string restoreDefaultsButtonLabel
		{
			get
			{
				return this.document.FetchString(this._restoreDefaultsButtonLabel, Language.English);
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001B9B RID: 7067 RVA: 0x000152C5 File Offset: 0x000134C5
		public override string controllerSettingsGroupLabel
		{
			get
			{
				return this.document.FetchString(this._controllerSettingsGroupLabel, Language.English);
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001B9C RID: 7068 RVA: 0x000152D9 File Offset: 0x000134D9
		public override string playersGroupLabel
		{
			get
			{
				return this.document.FetchString(this._playersGroupLabel, Language.English);
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x000152ED File Offset: 0x000134ED
		public override string assignedControllersGroupLabel
		{
			get
			{
				return this.document.FetchString(this._assignedControllersGroupLabel, Language.English);
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x00015301 File Offset: 0x00013501
		public override string settingsGroupLabel
		{
			get
			{
				return this.document.FetchString(this._settingsGroupLabel, Language.English);
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x00015315 File Offset: 0x00013515
		public override string mapCategoriesGroupLabel
		{
			get
			{
				return this.document.FetchString(this._mapCategoriesGroupLabel, Language.English);
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x00015329 File Offset: 0x00013529
		public override string restoreDefaultsWindowMessage
		{
			get
			{
				if (ReInput.players.playerCount > 1)
				{
					return this.document.FetchString(this._restoreDefaultsWindowMessage_multiPlayer, Language.English);
				}
				return this.document.FetchString(this._restoreDefaultsWindowMessage_onePlayer, Language.English);
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x0001535D File Offset: 0x0001355D
		public override string calibrateWindow_deadZoneSliderLabel
		{
			get
			{
				return this.document.FetchString(this._calibrateWindow_deadZoneSliderLabel, Language.English);
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x00015371 File Offset: 0x00013571
		public override string calibrateWindow_zeroSliderLabel
		{
			get
			{
				return this.document.FetchString(this._calibrateWindow_zeroSliderLabel, Language.English);
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001BA3 RID: 7075 RVA: 0x00015385 File Offset: 0x00013585
		public override string calibrateWindow_sensitivitySliderLabel
		{
			get
			{
				return this.document.FetchString(this._calibrateWindow_sensitivitySliderLabel, Language.English);
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x00015399 File Offset: 0x00013599
		public override string calibrateWindow_invertToggleLabel
		{
			get
			{
				return this.document.FetchString(this._calibrateWindow_invertToggleLabel, Language.English);
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x000153AD File Offset: 0x000135AD
		public override string calibrateWindow_calibrateButtonLabel
		{
			get
			{
				return this.document.FetchString(this._calibrateWindow_calibrateButtonLabel, Language.English);
			}
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x000153C1 File Offset: 0x000135C1
		public override string GetControllerAssignmentConflictWindowMessage(string joystickName, string otherPlayerName, string currentPlayerName)
		{
			return string.Format(this.document.FetchString(this._controllerAssignmentConflictWindowMessage, Language.English), joystickName, otherPlayerName, currentPlayerName);
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x000153DD File Offset: 0x000135DD
		public override string GetJoystickElementAssignmentPollingWindowMessage(string actionName)
		{
			return string.Format(this.document.FetchString(this._joystickElementAssignmentPollingWindowMessage, Language.English), actionName);
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x000153F7 File Offset: 0x000135F7
		public override string GetJoystickElementAssignmentPollingWindowMessage_FullAxisFieldOnly(string actionName)
		{
			return string.Format(this.document.FetchString(this._joystickElementAssignmentPollingWindowMessage_fullAxisFieldOnly, Language.English), actionName);
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x00015411 File Offset: 0x00013611
		public override string GetKeyboardElementAssignmentPollingWindowMessage(string actionName)
		{
			return string.Format(this.document.FetchString(this._keyboardElementAssignmentPollingWindowMessage, Language.English), actionName);
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x0001542B File Offset: 0x0001362B
		public override string GetMouseElementAssignmentPollingWindowMessage(string actionName)
		{
			return string.Format(this.document.FetchString(this._mouseElementAssignmentPollingWindowMessage, Language.English), actionName);
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x00015445 File Offset: 0x00013645
		public override string GetMouseElementAssignmentPollingWindowMessage_FullAxisFieldOnly(string actionName)
		{
			return string.Format(this.document.FetchString(this._mouseElementAssignmentPollingWindowMessage_fullAxisFieldOnly, Language.English), actionName);
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x0001545F File Offset: 0x0001365F
		public override string GetElementAlreadyInUseBlocked(string elementName)
		{
			return string.Format(this.document.FetchString(this._elementAlreadyInUseBlocked, Language.English), elementName);
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x00015479 File Offset: 0x00013679
		public override string GetElementAlreadyInUseCanReplace(string elementName, bool allowConflicts)
		{
			if (!allowConflicts)
			{
				return string.Format(this.document.FetchString(this._elementAlreadyInUseCanReplace, Language.English), elementName);
			}
			return string.Format(this.document.FetchString(this._elementAlreadyInUseCanReplace_conflictAllowed, Language.English), elementName);
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x000154AF File Offset: 0x000136AF
		public override string GetMouseAssignmentConflictWindowMessage(string otherPlayerName, string thisPlayerName)
		{
			return string.Format(this.document.FetchString(this._mouseAssignmentConflictWindowMessage, Language.English), otherPlayerName, thisPlayerName);
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x000154CA File Offset: 0x000136CA
		public override string GetCalibrateAxisStep1WindowMessage(string axisName)
		{
			return string.Format(this.document.FetchString(this._calibrateAxisStep1WindowMessage, Language.English), axisName);
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x000154E4 File Offset: 0x000136E4
		public override string GetCalibrateAxisStep2WindowMessage(string axisName)
		{
			return string.Format(this.document.FetchString(this._calibrateAxisStep2WindowMessage, Language.English), axisName);
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x00014E6E File Offset: 0x0001306E
		public override string GetPlayerName(int playerId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				throw new ArgumentException("Invalid player id: " + playerId.ToString());
			}
			return player.descriptiveName;
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x00014E9A File Offset: 0x0001309A
		public override string GetControllerName(Controller controller)
		{
			if (controller == null)
			{
				throw new ArgumentNullException("controller");
			}
			return controller.name;
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x0006D8FC File Offset: 0x0006BAFC
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
			string elementIdentifierName = this.GetElementIdentifierName(actionElementMap.controllerMap.controller, actionElementMap.elementIdentifierId, actionElementMap.axisRange);
			if (this.document.HasString(elementIdentifierName))
			{
				return this.document.FetchString(elementIdentifierName, Language.English);
			}
			return elementIdentifierName;
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x00057A80 File Offset: 0x00055C80
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

		// Token: 0x06001BB5 RID: 7093 RVA: 0x0006D978 File Offset: 0x0006BB78
		public override string GetElementIdentifierName(KeyCode keyCode, ModifierKeyFlags modifierKeyFlags)
		{
			string text = Keyboard.GetKeyName(keyCode);
			if (this.document.HasString(text))
			{
				text = this.document.FetchString(text, Language.English);
			}
			if (modifierKeyFlags != null)
			{
				return string.Format("{0}{1}{2}", this.ModifierKeyFlagsToString(modifierKeyFlags), this.document.FetchString(this._modifierKeys.separator, Language.English), text);
			}
			return text;
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x0006D9D8 File Offset: 0x0006BBD8
		public override string GetActionName(int actionId)
		{
			InputAction action = ReInput.mapping.GetAction(actionId);
			if (action == null)
			{
				throw new ArgumentException("Invalid action id: " + actionId.ToString());
			}
			string text = action.descriptiveName;
			if ((PlayerInput.interactMapping == PlayerInput.InteractMapping.Primary || PlayerInput.secondaryMapping == PlayerInput.SecondaryMapping.Primary) && text == "Primary")
			{
				if (PlayerInput.interactMapping == PlayerInput.InteractMapping.Primary && PlayerInput.secondaryMapping == PlayerInput.SecondaryMapping.Primary)
				{
					text = "PrimarySecondaryInteract";
				}
				else if (PlayerInput.interactMapping == PlayerInput.InteractMapping.Primary)
				{
					text = "PrimaryInteract";
				}
				else
				{
					text = "PrimarySecondary";
				}
			}
			if (PlayerInput.interactMapping == PlayerInput.InteractMapping.Jump && text == "Jump")
			{
				text = "JumpInteract";
			}
			return this.document.FetchString(text, Language.English);
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x0006DA84 File Offset: 0x0006BC84
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
				return this.document.FetchString(action.descriptiveName, Language.English);
			case 1:
				if (string.IsNullOrEmpty(action.positiveDescriptiveName))
				{
					return this.document.FetchString(action.descriptiveName, Language.English) + " +";
				}
				return this.document.FetchString(action.positiveDescriptiveName, Language.English);
			case 2:
				if (string.IsNullOrEmpty(action.negativeDescriptiveName))
				{
					return this.document.FetchString(action.descriptiveName, Language.English) + " -";
				}
				return this.document.FetchString(action.negativeDescriptiveName, Language.English);
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x00014F0A File Offset: 0x0001310A
		public override string GetMapCategoryName(int id)
		{
			InputMapCategory mapCategory = ReInput.mapping.GetMapCategory(id);
			if (mapCategory == null)
			{
				throw new ArgumentException("Invalid map category id: " + id.ToString());
			}
			return mapCategory.descriptiveName;
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x00014F36 File Offset: 0x00013136
		public override string GetActionCategoryName(int id)
		{
			InputCategory actionCategory = ReInput.mapping.GetActionCategory(id);
			if (actionCategory == null)
			{
				throw new ArgumentException("Invalid action category id: " + id.ToString());
			}
			return actionCategory.descriptiveName;
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x00014F62 File Offset: 0x00013162
		public override string GetLayoutName(ControllerType controllerType, int id)
		{
			InputLayout layout = ReInput.mapping.GetLayout(controllerType, id);
			if (layout == null)
			{
				throw new ArgumentException("Invalid " + controllerType.ToString() + " layout id: " + id.ToString());
			}
			return layout.descriptiveName;
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x0006DB60 File Offset: 0x0006BD60
		public override string ModifierKeyFlagsToString(ModifierKeyFlags flags)
		{
			int num = 0;
			string text = string.Empty;
			if (Keyboard.ModifierKeyFlagsContain(flags, 1))
			{
				text += this.document.FetchString(this._modifierKeys.control, Language.English);
				num++;
			}
			if (Keyboard.ModifierKeyFlagsContain(flags, 4))
			{
				if (num > 0 && !string.IsNullOrEmpty(this.document.FetchString(this._modifierKeys.separator, Language.English)))
				{
					text += this.document.FetchString(this._modifierKeys.separator, Language.English);
				}
				text += this.document.FetchString(this._modifierKeys.command, Language.English);
				num++;
			}
			if (Keyboard.ModifierKeyFlagsContain(flags, 2))
			{
				if (num > 0 && !string.IsNullOrEmpty(this.document.FetchString(this._modifierKeys.separator, Language.English)))
				{
					text += this.document.FetchString(this._modifierKeys.separator, Language.English);
				}
				text += this.document.FetchString(this._modifierKeys.alt, Language.English);
				num++;
			}
			if (num >= 3)
			{
				return text;
			}
			if (Keyboard.ModifierKeyFlagsContain(flags, 3))
			{
				if (num > 0 && !string.IsNullOrEmpty(this.document.FetchString(this._modifierKeys.separator, Language.English)))
				{
					text += this.document.FetchString(this._modifierKeys.separator, Language.English);
				}
				text += this.document.FetchString(this._modifierKeys.shift, Language.English);
				num++;
			}
			return text;
		}

		// Token: 0x04001D69 RID: 7529
		public MultilingualTextDocument document;

		// Token: 0x04001D6A RID: 7530
		[SerializeField]
		[TextLookup("document")]
		private string _yes = "Yes";

		// Token: 0x04001D6B RID: 7531
		[SerializeField]
		[TextLookup("document")]
		private string _no = "No";

		// Token: 0x04001D6C RID: 7532
		[SerializeField]
		[TextLookup("document")]
		private string _add = "Add";

		// Token: 0x04001D6D RID: 7533
		[SerializeField]
		[TextLookup("document")]
		private string _replace = "Replace";

		// Token: 0x04001D6E RID: 7534
		[SerializeField]
		[TextLookup("document")]
		private string _remove = "Remove";

		// Token: 0x04001D6F RID: 7535
		[SerializeField]
		[TextLookup("document")]
		private string _swap = "Swap";

		// Token: 0x04001D70 RID: 7536
		[SerializeField]
		[TextLookup("document")]
		private string _cancel = "Cancel";

		// Token: 0x04001D71 RID: 7537
		[SerializeField]
		[TextLookup("document")]
		private string _none = "None";

		// Token: 0x04001D72 RID: 7538
		[SerializeField]
		[TextLookup("document")]
		private string _okay = "Okay";

		// Token: 0x04001D73 RID: 7539
		[SerializeField]
		[TextLookup("document")]
		private string _done = "Done";

		// Token: 0x04001D74 RID: 7540
		[SerializeField]
		[TextLookup("document")]
		private string _default = "Default";

		// Token: 0x04001D75 RID: 7541
		[SerializeField]
		[TextLookup("document")]
		private string _assignControllerWindowTitle = "Choose Controller";

		// Token: 0x04001D76 RID: 7542
		[SerializeField]
		[TextLookup("document")]
		private string _assignControllerWindowMessage = "Press any button or move an axis on the controller you would like to use.";

		// Token: 0x04001D77 RID: 7543
		[SerializeField]
		[TextLookup("document")]
		private string _controllerAssignmentConflictWindowTitle = "Controller Assignment";

		// Token: 0x04001D78 RID: 7544
		[SerializeField]
		[TextLookup("document")]
		[Tooltip("{0} = Joystick Name\n{1} = Other Player Name\n{2} = This Player Name")]
		private string _controllerAssignmentConflictWindowMessage = "{0} is already assigned to {1}. Do you want to assign this controller to {2} instead?";

		// Token: 0x04001D79 RID: 7545
		[SerializeField]
		[TextLookup("document")]
		private string _elementAssignmentPrePollingWindowMessage = "First center or zero all sticks and axes and press any button or wait for the timer to finish.";

		// Token: 0x04001D7A RID: 7546
		[SerializeField]
		[Tooltip("{0} = Action Name")]
		[TextLookup("document")]
		private string _joystickElementAssignmentPollingWindowMessage = "Now press a button or move an axis to assign it to {0}.";

		// Token: 0x04001D7B RID: 7547
		[SerializeField]
		[Tooltip("This text is only displayed when split-axis fields have been disabled and the user clicks on the full-axis field. Button/key/D-pad input cannot be assigned to a full-axis field.\n{0} = Action Name")]
		[TextLookup("document")]
		private string _joystickElementAssignmentPollingWindowMessage_fullAxisFieldOnly = "Now move an axis to assign it to {0}.";

		// Token: 0x04001D7C RID: 7548
		[SerializeField]
		[Tooltip("{0} = Action Name")]
		[TextLookup("document")]
		private string _keyboardElementAssignmentPollingWindowMessage = "Press a key to assign it to {0}. Modifier keys may also be used. To assign a modifier key alone, hold it down for 1 second.";

		// Token: 0x04001D7D RID: 7549
		[SerializeField]
		[Tooltip("{0} = Action Name")]
		[TextLookup("document")]
		private string _mouseElementAssignmentPollingWindowMessage = "Press a mouse button or move an axis to assign it to {0}.";

		// Token: 0x04001D7E RID: 7550
		[SerializeField]
		[Tooltip("This text is only displayed when split-axis fields have been disabled and the user clicks on the full-axis field. Button/key/D-pad input cannot be assigned to a full-axis field.\n{0} = Action Name")]
		[TextLookup("document")]
		private string _mouseElementAssignmentPollingWindowMessage_fullAxisFieldOnly = "Move an axis to assign it to {0}.";

		// Token: 0x04001D7F RID: 7551
		[SerializeField]
		[TextLookup("document")]
		private string _elementAssignmentConflictWindowMessage = "Assignment Conflict";

		// Token: 0x04001D80 RID: 7552
		[SerializeField]
		[TextLookup("document")]
		private string _elementAssignmentReplacementWindowMessage = "Assignment Conflict";

		// Token: 0x04001D81 RID: 7553
		[SerializeField]
		[Tooltip("{0} = Element Name")]
		[TextLookup("document")]
		private string _elementAlreadyInUseBlocked = "{0} is already in use cannot be replaced.";

		// Token: 0x04001D82 RID: 7554
		[SerializeField]
		[Tooltip("{0} = Element Name")]
		[TextLookup("document")]
		private string _elementAlreadyInUseCanReplace = "{0} is already in use. Do you want to replace it?";

		// Token: 0x04001D83 RID: 7555
		[SerializeField]
		[Tooltip("{0} = Element Name")]
		[TextLookup("document")]
		private string _elementAlreadyInUseCanReplace_conflictAllowed = "{0} is already in use. Do you want to replace it? You may also choose to add the assignment anyway.";

		// Token: 0x04001D84 RID: 7556
		[SerializeField]
		[TextLookup("document")]
		private string _mouseAssignmentConflictWindowTitle = "Mouse Assignment";

		// Token: 0x04001D85 RID: 7557
		[SerializeField]
		[Tooltip("{0} = Other Player Name\n{1} = This Player Name")]
		[TextLookup("document")]
		private string _mouseAssignmentConflictWindowMessage = "The mouse is already assigned to {0}. Do you want to assign the mouse to {1} instead?";

		// Token: 0x04001D86 RID: 7558
		[SerializeField]
		[TextLookup("document")]
		private string _calibrateControllerWindowTitle = "Calibrate Controller";

		// Token: 0x04001D87 RID: 7559
		[SerializeField]
		[TextLookup("document")]
		private string _calibrateAxisStep1WindowTitle = "Calibrate Zero";

		// Token: 0x04001D88 RID: 7560
		[SerializeField]
		[Tooltip("{0} = Axis Name")]
		[TextLookup("document")]
		private string _calibrateAxisStep1WindowMessage = "Center or zero {0} and press any button or wait for the timer to finish.";

		// Token: 0x04001D89 RID: 7561
		[SerializeField]
		[TextLookup("document")]
		private string _calibrateAxisStep2WindowTitle = "Calibrate Range";

		// Token: 0x04001D8A RID: 7562
		[SerializeField]
		[Tooltip("{0} = Axis Name")]
		[TextLookup("document")]
		private string _calibrateAxisStep2WindowMessage = "Move {0} through its entire range then press any button or wait for the timer to finish.";

		// Token: 0x04001D8B RID: 7563
		[SerializeField]
		[TextLookup("document")]
		private string _inputBehaviorSettingsWindowTitle = "Sensitivity Settings";

		// Token: 0x04001D8C RID: 7564
		[SerializeField]
		[TextLookup("document")]
		private string _restoreDefaultsWindowTitle = "Restore Defaults";

		// Token: 0x04001D8D RID: 7565
		[SerializeField]
		[Tooltip("Message for a single player game.")]
		[TextLookup("document")]
		private string _restoreDefaultsWindowMessage_onePlayer = "This will restore the default input configuration. Are you sure you want to do this?";

		// Token: 0x04001D8E RID: 7566
		[SerializeField]
		[Tooltip("Message for a multi-player game.")]
		[TextLookup("document")]
		private string _restoreDefaultsWindowMessage_multiPlayer = "This will restore the default input configuration for all players. Are you sure you want to do this?";

		// Token: 0x04001D8F RID: 7567
		[SerializeField]
		[TextLookup("document")]
		private string _actionColumnLabel = "Actions";

		// Token: 0x04001D90 RID: 7568
		[SerializeField]
		[TextLookup("document")]
		private string _keyboardColumnLabel = "Keyboard";

		// Token: 0x04001D91 RID: 7569
		[SerializeField]
		[TextLookup("document")]
		private string _mouseColumnLabel = "Mouse";

		// Token: 0x04001D92 RID: 7570
		[SerializeField]
		[TextLookup("document")]
		private string _controllerColumnLabel = "Controller";

		// Token: 0x04001D93 RID: 7571
		[SerializeField]
		[TextLookup("document")]
		private string _removeControllerButtonLabel = "Remove";

		// Token: 0x04001D94 RID: 7572
		[SerializeField]
		[TextLookup("document")]
		private string _calibrateControllerButtonLabel = "Calibrate";

		// Token: 0x04001D95 RID: 7573
		[SerializeField]
		[TextLookup("document")]
		private string _assignControllerButtonLabel = "Assign Controller";

		// Token: 0x04001D96 RID: 7574
		[SerializeField]
		[TextLookup("document")]
		private string _inputBehaviorSettingsButtonLabel = "Sensitivity";

		// Token: 0x04001D97 RID: 7575
		[SerializeField]
		[TextLookup("document")]
		private string _doneButtonLabel = "Done";

		// Token: 0x04001D98 RID: 7576
		[SerializeField]
		[TextLookup("document")]
		private string _restoreDefaultsButtonLabel = "Restore Defaults";

		// Token: 0x04001D99 RID: 7577
		[SerializeField]
		[TextLookup("document")]
		private string _playersGroupLabel = "Players:";

		// Token: 0x04001D9A RID: 7578
		[SerializeField]
		[TextLookup("document")]
		private string _controllerSettingsGroupLabel = "Controller:";

		// Token: 0x04001D9B RID: 7579
		[SerializeField]
		[TextLookup("document")]
		private string _assignedControllersGroupLabel = "Assigned Controllers:";

		// Token: 0x04001D9C RID: 7580
		[SerializeField]
		[TextLookup("document")]
		private string _settingsGroupLabel = "Settings:";

		// Token: 0x04001D9D RID: 7581
		[SerializeField]
		[TextLookup("document")]
		private string _mapCategoriesGroupLabel = "Categories:";

		// Token: 0x04001D9E RID: 7582
		[SerializeField]
		[TextLookup("document")]
		private string _calibrateWindow_deadZoneSliderLabel = "Dead Zone:";

		// Token: 0x04001D9F RID: 7583
		[SerializeField]
		[TextLookup("document")]
		private string _calibrateWindow_zeroSliderLabel = "Zero:";

		// Token: 0x04001DA0 RID: 7584
		[SerializeField]
		[TextLookup("document")]
		private string _calibrateWindow_sensitivitySliderLabel = "Sensitivity:";

		// Token: 0x04001DA1 RID: 7585
		[SerializeField]
		[TextLookup("document")]
		private string _calibrateWindow_invertToggleLabel = "Invert";

		// Token: 0x04001DA2 RID: 7586
		[SerializeField]
		[TextLookup("document")]
		private string _calibrateWindow_calibrateButtonLabel = "Calibrate";

		// Token: 0x04001DA3 RID: 7587
		[SerializeField]
		private MLLanguageData.ModifierKeys _modifierKeys;

		// Token: 0x04001DA4 RID: 7588
		[SerializeField]
		private MLLanguageData.CustomEntry[] _customEntries;

		// Token: 0x04001DA5 RID: 7589
		private bool _initialized;

		// Token: 0x04001DA6 RID: 7590
		private Dictionary<string, string> customDict;

		// Token: 0x02000466 RID: 1126
		[Serializable]
		protected class CustomEntry
		{
			// Token: 0x06001BBD RID: 7101 RVA: 0x0000227F File Offset: 0x0000047F
			public CustomEntry()
			{
			}

			// Token: 0x06001BBE RID: 7102 RVA: 0x000154FE File Offset: 0x000136FE
			public CustomEntry(string key, string value)
			{
				this.key = key;
				this.value = value;
			}

			// Token: 0x06001BBF RID: 7103 RVA: 0x0006DF70 File Offset: 0x0006C170
			public static Dictionary<string, string> ToDictionary(MLLanguageData.CustomEntry[] array)
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

			// Token: 0x04001DA7 RID: 7591
			public string key;

			// Token: 0x04001DA8 RID: 7592
			public string value;
		}

		// Token: 0x02000467 RID: 1127
		[Serializable]
		protected class ModifierKeys
		{
			// Token: 0x04001DA9 RID: 7593
			[TextLookup("document")]
			public string control = "Control";

			// Token: 0x04001DAA RID: 7594
			[TextLookup("document")]
			public string alt = "Alt";

			// Token: 0x04001DAB RID: 7595
			[TextLookup("document")]
			public string shift = "Shift";

			// Token: 0x04001DAC RID: 7596
			[TextLookup("document")]
			public string command = "Command";

			// Token: 0x04001DAD RID: 7597
			[TextLookup("document")]
			public string separator = " + ";
		}
	}
}
