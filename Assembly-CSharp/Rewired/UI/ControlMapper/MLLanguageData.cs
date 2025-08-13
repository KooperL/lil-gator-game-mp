using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000327 RID: 807
	[CreateAssetMenu(menuName = "Rewired/MLLanguageData")]
	[Serializable]
	public class MLLanguageData : LanguageDataBase
	{
		// Token: 0x06001647 RID: 5703 RVA: 0x0005D9BB File Offset: 0x0005BBBB
		public override void Initialize()
		{
			if (this._initialized)
			{
				return;
			}
			this.customDict = MLLanguageData.CustomEntry.ToDictionary(this._customEntries);
			this._initialized = true;
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x0005D9E0 File Offset: 0x0005BBE0
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

		// Token: 0x06001649 RID: 5705 RVA: 0x0005DA12 File Offset: 0x0005BC12
		public override bool ContainsCustomEntryKey(string key)
		{
			return !string.IsNullOrEmpty(key) && this.customDict.ContainsKey(key);
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x0005DA2A File Offset: 0x0005BC2A
		public override string yes
		{
			get
			{
				return this.document.FetchString(this._yes, Language.Auto);
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x0005DA3F File Offset: 0x0005BC3F
		public override string no
		{
			get
			{
				return this.document.FetchString(this._no, Language.Auto);
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x0005DA54 File Offset: 0x0005BC54
		public override string add
		{
			get
			{
				return this.document.FetchString(this._add, Language.Auto);
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x0005DA69 File Offset: 0x0005BC69
		public override string replace
		{
			get
			{
				return this.document.FetchString(this._replace, Language.Auto);
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x0005DA7E File Offset: 0x0005BC7E
		public override string remove
		{
			get
			{
				return this.document.FetchString(this._remove, Language.Auto);
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x0005DA93 File Offset: 0x0005BC93
		public override string swap
		{
			get
			{
				return this.document.FetchString(this._swap, Language.Auto);
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x0005DAA8 File Offset: 0x0005BCA8
		public override string cancel
		{
			get
			{
				return this.document.FetchString(this._cancel, Language.Auto);
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x0005DABD File Offset: 0x0005BCBD
		public override string none
		{
			get
			{
				return this.document.FetchString(this._none, Language.Auto);
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x0005DAD2 File Offset: 0x0005BCD2
		public override string okay
		{
			get
			{
				return this.document.FetchString(this._okay, Language.Auto);
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x0005DAE7 File Offset: 0x0005BCE7
		public override string done
		{
			get
			{
				return this.document.FetchString(this._done, Language.Auto);
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x0005DAFC File Offset: 0x0005BCFC
		public override string default_
		{
			get
			{
				return this.document.FetchString(this._default, Language.Auto);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001655 RID: 5717 RVA: 0x0005DB11 File Offset: 0x0005BD11
		public override string assignControllerWindowTitle
		{
			get
			{
				return this.document.FetchString(this._assignControllerWindowTitle, Language.Auto);
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x0005DB26 File Offset: 0x0005BD26
		public override string assignControllerWindowMessage
		{
			get
			{
				return this.document.FetchString(this._assignControllerWindowMessage, Language.Auto);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x0005DB3B File Offset: 0x0005BD3B
		public override string controllerAssignmentConflictWindowTitle
		{
			get
			{
				return this.document.FetchString(this._controllerAssignmentConflictWindowTitle, Language.Auto);
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x0005DB50 File Offset: 0x0005BD50
		public override string elementAssignmentPrePollingWindowMessage
		{
			get
			{
				return this.document.FetchString(this._elementAssignmentPrePollingWindowMessage, Language.Auto);
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x0005DB65 File Offset: 0x0005BD65
		public override string elementAssignmentConflictWindowMessage
		{
			get
			{
				return this.document.FetchString(this._elementAssignmentConflictWindowMessage, Language.Auto);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x0005DB7A File Offset: 0x0005BD7A
		public override string elementAssignmentReplacementWindowMessage
		{
			get
			{
				return this.document.FetchString(this._elementAssignmentReplacementWindowMessage, Language.Auto);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x0005DB8F File Offset: 0x0005BD8F
		public override string mouseAssignmentConflictWindowTitle
		{
			get
			{
				return this.document.FetchString(this._mouseAssignmentConflictWindowTitle, Language.Auto);
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x0005DBA4 File Offset: 0x0005BDA4
		public override string calibrateControllerWindowTitle
		{
			get
			{
				return this.document.FetchString(this._calibrateControllerWindowTitle, Language.Auto);
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x0600165D RID: 5725 RVA: 0x0005DBB9 File Offset: 0x0005BDB9
		public override string calibrateAxisStep1WindowTitle
		{
			get
			{
				return this.document.FetchString(this._calibrateAxisStep1WindowTitle, Language.Auto);
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x0005DBCE File Offset: 0x0005BDCE
		public override string calibrateAxisStep2WindowTitle
		{
			get
			{
				return this.document.FetchString(this._calibrateAxisStep2WindowTitle, Language.Auto);
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x0005DBE3 File Offset: 0x0005BDE3
		public override string inputBehaviorSettingsWindowTitle
		{
			get
			{
				return this.document.FetchString(this._inputBehaviorSettingsWindowTitle, Language.Auto);
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x0005DBF8 File Offset: 0x0005BDF8
		public override string restoreDefaultsWindowTitle
		{
			get
			{
				return this.document.FetchString(this._restoreDefaultsWindowTitle, Language.Auto);
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x0005DC0D File Offset: 0x0005BE0D
		public override string actionColumnLabel
		{
			get
			{
				return this.document.FetchString(this._actionColumnLabel, Language.Auto);
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x0005DC22 File Offset: 0x0005BE22
		public override string keyboardColumnLabel
		{
			get
			{
				return this.document.FetchString(this._keyboardColumnLabel, Language.Auto);
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x0005DC37 File Offset: 0x0005BE37
		public override string mouseColumnLabel
		{
			get
			{
				return this.document.FetchString(this._mouseColumnLabel, Language.Auto);
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x0005DC4C File Offset: 0x0005BE4C
		public override string controllerColumnLabel
		{
			get
			{
				return this.document.FetchString(this._controllerColumnLabel, Language.Auto);
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x0005DC61 File Offset: 0x0005BE61
		public override string removeControllerButtonLabel
		{
			get
			{
				return this.document.FetchString(this._removeControllerButtonLabel, Language.Auto);
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001666 RID: 5734 RVA: 0x0005DC76 File Offset: 0x0005BE76
		public override string calibrateControllerButtonLabel
		{
			get
			{
				return this.document.FetchString(this._calibrateControllerButtonLabel, Language.Auto);
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x0005DC8B File Offset: 0x0005BE8B
		public override string assignControllerButtonLabel
		{
			get
			{
				return this.document.FetchString(this._assignControllerButtonLabel, Language.Auto);
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x0005DCA0 File Offset: 0x0005BEA0
		public override string inputBehaviorSettingsButtonLabel
		{
			get
			{
				return this.document.FetchString(this._inputBehaviorSettingsButtonLabel, Language.Auto);
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x0005DCB5 File Offset: 0x0005BEB5
		public override string doneButtonLabel
		{
			get
			{
				return this.document.FetchString(this._doneButtonLabel, Language.Auto);
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x0005DCCA File Offset: 0x0005BECA
		public override string restoreDefaultsButtonLabel
		{
			get
			{
				return this.document.FetchString(this._restoreDefaultsButtonLabel, Language.Auto);
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x0005DCDF File Offset: 0x0005BEDF
		public override string controllerSettingsGroupLabel
		{
			get
			{
				return this.document.FetchString(this._controllerSettingsGroupLabel, Language.Auto);
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x0005DCF4 File Offset: 0x0005BEF4
		public override string playersGroupLabel
		{
			get
			{
				return this.document.FetchString(this._playersGroupLabel, Language.Auto);
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x0005DD09 File Offset: 0x0005BF09
		public override string assignedControllersGroupLabel
		{
			get
			{
				return this.document.FetchString(this._assignedControllersGroupLabel, Language.Auto);
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x0005DD1E File Offset: 0x0005BF1E
		public override string settingsGroupLabel
		{
			get
			{
				return this.document.FetchString(this._settingsGroupLabel, Language.Auto);
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x0005DD33 File Offset: 0x0005BF33
		public override string mapCategoriesGroupLabel
		{
			get
			{
				return this.document.FetchString(this._mapCategoriesGroupLabel, Language.Auto);
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x0005DD48 File Offset: 0x0005BF48
		public override string restoreDefaultsWindowMessage
		{
			get
			{
				if (ReInput.players.playerCount > 1)
				{
					return this.document.FetchString(this._restoreDefaultsWindowMessage_multiPlayer, Language.Auto);
				}
				return this.document.FetchString(this._restoreDefaultsWindowMessage_onePlayer, Language.Auto);
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x0005DD7E File Offset: 0x0005BF7E
		public override string calibrateWindow_deadZoneSliderLabel
		{
			get
			{
				return this.document.FetchString(this._calibrateWindow_deadZoneSliderLabel, Language.Auto);
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x0005DD93 File Offset: 0x0005BF93
		public override string calibrateWindow_zeroSliderLabel
		{
			get
			{
				return this.document.FetchString(this._calibrateWindow_zeroSliderLabel, Language.Auto);
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x0005DDA8 File Offset: 0x0005BFA8
		public override string calibrateWindow_sensitivitySliderLabel
		{
			get
			{
				return this.document.FetchString(this._calibrateWindow_sensitivitySliderLabel, Language.Auto);
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x0005DDBD File Offset: 0x0005BFBD
		public override string calibrateWindow_invertToggleLabel
		{
			get
			{
				return this.document.FetchString(this._calibrateWindow_invertToggleLabel, Language.Auto);
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x0005DDD2 File Offset: 0x0005BFD2
		public override string calibrateWindow_calibrateButtonLabel
		{
			get
			{
				return this.document.FetchString(this._calibrateWindow_calibrateButtonLabel, Language.Auto);
			}
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0005DDE7 File Offset: 0x0005BFE7
		public override string GetControllerAssignmentConflictWindowMessage(string joystickName, string otherPlayerName, string currentPlayerName)
		{
			return string.Format(this.document.FetchString(this._controllerAssignmentConflictWindowMessage, Language.Auto), joystickName, otherPlayerName, currentPlayerName);
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x0005DE04 File Offset: 0x0005C004
		public override string GetJoystickElementAssignmentPollingWindowMessage(string actionName)
		{
			return string.Format(this.document.FetchString(this._joystickElementAssignmentPollingWindowMessage, Language.Auto), actionName);
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x0005DE1F File Offset: 0x0005C01F
		public override string GetJoystickElementAssignmentPollingWindowMessage_FullAxisFieldOnly(string actionName)
		{
			return string.Format(this.document.FetchString(this._joystickElementAssignmentPollingWindowMessage_fullAxisFieldOnly, Language.Auto), actionName);
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x0005DE3A File Offset: 0x0005C03A
		public override string GetKeyboardElementAssignmentPollingWindowMessage(string actionName)
		{
			return string.Format(this.document.FetchString(this._keyboardElementAssignmentPollingWindowMessage, Language.Auto), actionName);
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x0005DE55 File Offset: 0x0005C055
		public override string GetMouseElementAssignmentPollingWindowMessage(string actionName)
		{
			return string.Format(this.document.FetchString(this._mouseElementAssignmentPollingWindowMessage, Language.Auto), actionName);
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x0005DE70 File Offset: 0x0005C070
		public override string GetMouseElementAssignmentPollingWindowMessage_FullAxisFieldOnly(string actionName)
		{
			return string.Format(this.document.FetchString(this._mouseElementAssignmentPollingWindowMessage_fullAxisFieldOnly, Language.Auto), actionName);
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x0005DE8B File Offset: 0x0005C08B
		public override string GetElementAlreadyInUseBlocked(string elementName)
		{
			return string.Format(this.document.FetchString(this._elementAlreadyInUseBlocked, Language.Auto), elementName);
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x0005DEA6 File Offset: 0x0005C0A6
		public override string GetElementAlreadyInUseCanReplace(string elementName, bool allowConflicts)
		{
			if (!allowConflicts)
			{
				return string.Format(this.document.FetchString(this._elementAlreadyInUseCanReplace, Language.Auto), elementName);
			}
			return string.Format(this.document.FetchString(this._elementAlreadyInUseCanReplace_conflictAllowed, Language.Auto), elementName);
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x0005DEDE File Offset: 0x0005C0DE
		public override string GetMouseAssignmentConflictWindowMessage(string otherPlayerName, string thisPlayerName)
		{
			return string.Format(this.document.FetchString(this._mouseAssignmentConflictWindowMessage, Language.Auto), otherPlayerName, thisPlayerName);
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0005DEFA File Offset: 0x0005C0FA
		public override string GetCalibrateAxisStep1WindowMessage(string axisName)
		{
			return string.Format(this.document.FetchString(this._calibrateAxisStep1WindowMessage, Language.Auto), axisName);
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0005DF15 File Offset: 0x0005C115
		public override string GetCalibrateAxisStep2WindowMessage(string axisName)
		{
			return string.Format(this.document.FetchString(this._calibrateAxisStep2WindowMessage, Language.Auto), axisName);
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x0005DF30 File Offset: 0x0005C130
		public override string GetPlayerName(int playerId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				throw new ArgumentException("Invalid player id: " + playerId.ToString());
			}
			return player.descriptiveName;
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x0005DF5C File Offset: 0x0005C15C
		public override string GetControllerName(Controller controller)
		{
			if (controller == null)
			{
				throw new ArgumentNullException("controller");
			}
			return controller.name;
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x0005DF74 File Offset: 0x0005C174
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

		// Token: 0x06001684 RID: 5764 RVA: 0x0005DFCC File Offset: 0x0005C1CC
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
			string text;
			if (type != ControllerElementType.Axis)
			{
				if (type != ControllerElementType.Button)
				{
					text = elementIdentifierById.name;
				}
				else
				{
					text = elementIdentifierById.name;
				}
			}
			else
			{
				text = elementIdentifierById.GetDisplayName(elementById.type, axisRange);
			}
			if (this.document.HasString(text))
			{
				text = this.document.FetchString(text, Language.Auto);
			}
			return text;
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x0005E068 File Offset: 0x0005C268
		public override string GetElementIdentifierName(KeyCode keyCode, ModifierKeyFlags modifierKeyFlags)
		{
			string text = Keyboard.GetKeyName(keyCode);
			if (this.document.HasString(text))
			{
				text = this.document.FetchString(text, Language.Auto);
			}
			if (modifierKeyFlags != ModifierKeyFlags.None)
			{
				return string.Format("{0} + {1}", this.ModifierKeyFlagsToString(modifierKeyFlags), text);
			}
			return text;
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x0005E0B0 File Offset: 0x0005C2B0
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
			return this.document.FetchString(text, Language.Auto);
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x0005E15C File Offset: 0x0005C35C
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
				return this.document.FetchString(action.descriptiveName, Language.Auto);
			case AxisRange.Positive:
				if (string.IsNullOrEmpty(action.positiveDescriptiveName))
				{
					return this.document.FetchString(action.descriptiveName, Language.Auto) + " +";
				}
				return this.document.FetchString(action.positiveDescriptiveName, Language.Auto);
			case AxisRange.Negative:
				if (string.IsNullOrEmpty(action.negativeDescriptiveName))
				{
					return this.document.FetchString(action.descriptiveName, Language.Auto) + " -";
				}
				return this.document.FetchString(action.negativeDescriptiveName, Language.Auto);
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x0005E23D File Offset: 0x0005C43D
		public override string GetMapCategoryName(int id)
		{
			InputMapCategory mapCategory = ReInput.mapping.GetMapCategory(id);
			if (mapCategory == null)
			{
				throw new ArgumentException("Invalid map category id: " + id.ToString());
			}
			return mapCategory.descriptiveName;
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x0005E269 File Offset: 0x0005C469
		public override string GetActionCategoryName(int id)
		{
			InputCategory actionCategory = ReInput.mapping.GetActionCategory(id);
			if (actionCategory == null)
			{
				throw new ArgumentException("Invalid action category id: " + id.ToString());
			}
			return actionCategory.descriptiveName;
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x0005E295 File Offset: 0x0005C495
		public override string GetLayoutName(ControllerType controllerType, int id)
		{
			InputLayout layout = ReInput.mapping.GetLayout(controllerType, id);
			if (layout == null)
			{
				throw new ArgumentException("Invalid " + controllerType.ToString() + " layout id: " + id.ToString());
			}
			return layout.descriptiveName;
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x0005E2D4 File Offset: 0x0005C4D4
		public override string ModifierKeyFlagsToString(ModifierKeyFlags flags)
		{
			int num = 0;
			string text = string.Empty;
			if (Keyboard.ModifierKeyFlagsContain(flags, ModifierKey.Control))
			{
				text += this.document.FetchString(this._modifierKeys.control, Language.Auto);
				num++;
			}
			if (Keyboard.ModifierKeyFlagsContain(flags, ModifierKey.Command))
			{
				if (num > 0)
				{
					text += " + ";
				}
				text += this.document.FetchString(this._modifierKeys.command, Language.Auto);
				num++;
			}
			if (Keyboard.ModifierKeyFlagsContain(flags, ModifierKey.Alt))
			{
				if (num > 0)
				{
					text += " + ";
				}
				text += this.document.FetchString(this._modifierKeys.alt, Language.Auto);
				num++;
			}
			if (num >= 3)
			{
				return text;
			}
			if (Keyboard.ModifierKeyFlagsContain(flags, ModifierKey.Shift))
			{
				if (num > 0)
				{
					text += " + ";
				}
				text += this.document.FetchString(this._modifierKeys.shift, Language.Auto);
				num++;
			}
			return text;
		}

		// Token: 0x0400187E RID: 6270
		public MultilingualTextDocument document;

		// Token: 0x0400187F RID: 6271
		[SerializeField]
		[TextLookup("document")]
		private string _yes = "Yes";

		// Token: 0x04001880 RID: 6272
		[SerializeField]
		[TextLookup("document")]
		private string _no = "No";

		// Token: 0x04001881 RID: 6273
		[SerializeField]
		[TextLookup("document")]
		private string _add = "Add";

		// Token: 0x04001882 RID: 6274
		[SerializeField]
		[TextLookup("document")]
		private string _replace = "Replace";

		// Token: 0x04001883 RID: 6275
		[SerializeField]
		[TextLookup("document")]
		private string _remove = "Remove";

		// Token: 0x04001884 RID: 6276
		[SerializeField]
		[TextLookup("document")]
		private string _swap = "Swap";

		// Token: 0x04001885 RID: 6277
		[SerializeField]
		[TextLookup("document")]
		private string _cancel = "Cancel";

		// Token: 0x04001886 RID: 6278
		[SerializeField]
		[TextLookup("document")]
		private string _none = "None";

		// Token: 0x04001887 RID: 6279
		[SerializeField]
		[TextLookup("document")]
		private string _okay = "Okay";

		// Token: 0x04001888 RID: 6280
		[SerializeField]
		[TextLookup("document")]
		private string _done = "Done";

		// Token: 0x04001889 RID: 6281
		[SerializeField]
		[TextLookup("document")]
		private string _default = "Default";

		// Token: 0x0400188A RID: 6282
		[SerializeField]
		[TextLookup("document")]
		private string _assignControllerWindowTitle = "Choose Controller";

		// Token: 0x0400188B RID: 6283
		[SerializeField]
		[TextLookup("document")]
		private string _assignControllerWindowMessage = "Press any button or move an axis on the controller you would like to use.";

		// Token: 0x0400188C RID: 6284
		[SerializeField]
		[TextLookup("document")]
		private string _controllerAssignmentConflictWindowTitle = "Controller Assignment";

		// Token: 0x0400188D RID: 6285
		[SerializeField]
		[TextLookup("document")]
		[Tooltip("{0} = Joystick Name\n{1} = Other Player Name\n{2} = This Player Name")]
		private string _controllerAssignmentConflictWindowMessage = "{0} is already assigned to {1}. Do you want to assign this controller to {2} instead?";

		// Token: 0x0400188E RID: 6286
		[SerializeField]
		[TextLookup("document")]
		private string _elementAssignmentPrePollingWindowMessage = "First center or zero all sticks and axes and press any button or wait for the timer to finish.";

		// Token: 0x0400188F RID: 6287
		[SerializeField]
		[Tooltip("{0} = Action Name")]
		[TextLookup("document")]
		private string _joystickElementAssignmentPollingWindowMessage = "Now press a button or move an axis to assign it to {0}.";

		// Token: 0x04001890 RID: 6288
		[SerializeField]
		[Tooltip("This text is only displayed when split-axis fields have been disabled and the user clicks on the full-axis field. Button/key/D-pad input cannot be assigned to a full-axis field.\n{0} = Action Name")]
		[TextLookup("document")]
		private string _joystickElementAssignmentPollingWindowMessage_fullAxisFieldOnly = "Now move an axis to assign it to {0}.";

		// Token: 0x04001891 RID: 6289
		[SerializeField]
		[Tooltip("{0} = Action Name")]
		[TextLookup("document")]
		private string _keyboardElementAssignmentPollingWindowMessage = "Press a key to assign it to {0}. Modifier keys may also be used. To assign a modifier key alone, hold it down for 1 second.";

		// Token: 0x04001892 RID: 6290
		[SerializeField]
		[Tooltip("{0} = Action Name")]
		[TextLookup("document")]
		private string _mouseElementAssignmentPollingWindowMessage = "Press a mouse button or move an axis to assign it to {0}.";

		// Token: 0x04001893 RID: 6291
		[SerializeField]
		[Tooltip("This text is only displayed when split-axis fields have been disabled and the user clicks on the full-axis field. Button/key/D-pad input cannot be assigned to a full-axis field.\n{0} = Action Name")]
		[TextLookup("document")]
		private string _mouseElementAssignmentPollingWindowMessage_fullAxisFieldOnly = "Move an axis to assign it to {0}.";

		// Token: 0x04001894 RID: 6292
		[SerializeField]
		[TextLookup("document")]
		private string _elementAssignmentConflictWindowMessage = "Assignment Conflict";

		// Token: 0x04001895 RID: 6293
		[SerializeField]
		[TextLookup("document")]
		private string _elementAssignmentReplacementWindowMessage = "Assignment Conflict";

		// Token: 0x04001896 RID: 6294
		[SerializeField]
		[Tooltip("{0} = Element Name")]
		[TextLookup("document")]
		private string _elementAlreadyInUseBlocked = "{0} is already in use cannot be replaced.";

		// Token: 0x04001897 RID: 6295
		[SerializeField]
		[Tooltip("{0} = Element Name")]
		[TextLookup("document")]
		private string _elementAlreadyInUseCanReplace = "{0} is already in use. Do you want to replace it?";

		// Token: 0x04001898 RID: 6296
		[SerializeField]
		[Tooltip("{0} = Element Name")]
		[TextLookup("document")]
		private string _elementAlreadyInUseCanReplace_conflictAllowed = "{0} is already in use. Do you want to replace it? You may also choose to add the assignment anyway.";

		// Token: 0x04001899 RID: 6297
		[SerializeField]
		[TextLookup("document")]
		private string _mouseAssignmentConflictWindowTitle = "Mouse Assignment";

		// Token: 0x0400189A RID: 6298
		[SerializeField]
		[Tooltip("{0} = Other Player Name\n{1} = This Player Name")]
		[TextLookup("document")]
		private string _mouseAssignmentConflictWindowMessage = "The mouse is already assigned to {0}. Do you want to assign the mouse to {1} instead?";

		// Token: 0x0400189B RID: 6299
		[SerializeField]
		[TextLookup("document")]
		private string _calibrateControllerWindowTitle = "Calibrate Controller";

		// Token: 0x0400189C RID: 6300
		[SerializeField]
		[TextLookup("document")]
		private string _calibrateAxisStep1WindowTitle = "Calibrate Zero";

		// Token: 0x0400189D RID: 6301
		[SerializeField]
		[Tooltip("{0} = Axis Name")]
		[TextLookup("document")]
		private string _calibrateAxisStep1WindowMessage = "Center or zero {0} and press any button or wait for the timer to finish.";

		// Token: 0x0400189E RID: 6302
		[SerializeField]
		[TextLookup("document")]
		private string _calibrateAxisStep2WindowTitle = "Calibrate Range";

		// Token: 0x0400189F RID: 6303
		[SerializeField]
		[Tooltip("{0} = Axis Name")]
		[TextLookup("document")]
		private string _calibrateAxisStep2WindowMessage = "Move {0} through its entire range then press any button or wait for the timer to finish.";

		// Token: 0x040018A0 RID: 6304
		[SerializeField]
		[TextLookup("document")]
		private string _inputBehaviorSettingsWindowTitle = "Sensitivity Settings";

		// Token: 0x040018A1 RID: 6305
		[SerializeField]
		[TextLookup("document")]
		private string _restoreDefaultsWindowTitle = "Restore Defaults";

		// Token: 0x040018A2 RID: 6306
		[SerializeField]
		[Tooltip("Message for a single player game.")]
		[TextLookup("document")]
		private string _restoreDefaultsWindowMessage_onePlayer = "This will restore the default input configuration. Are you sure you want to do this?";

		// Token: 0x040018A3 RID: 6307
		[SerializeField]
		[Tooltip("Message for a multi-player game.")]
		[TextLookup("document")]
		private string _restoreDefaultsWindowMessage_multiPlayer = "This will restore the default input configuration for all players. Are you sure you want to do this?";

		// Token: 0x040018A4 RID: 6308
		[SerializeField]
		[TextLookup("document")]
		private string _actionColumnLabel = "Actions";

		// Token: 0x040018A5 RID: 6309
		[SerializeField]
		[TextLookup("document")]
		private string _keyboardColumnLabel = "Keyboard";

		// Token: 0x040018A6 RID: 6310
		[SerializeField]
		[TextLookup("document")]
		private string _mouseColumnLabel = "Mouse";

		// Token: 0x040018A7 RID: 6311
		[SerializeField]
		[TextLookup("document")]
		private string _controllerColumnLabel = "Controller";

		// Token: 0x040018A8 RID: 6312
		[SerializeField]
		[TextLookup("document")]
		private string _removeControllerButtonLabel = "Remove";

		// Token: 0x040018A9 RID: 6313
		[SerializeField]
		[TextLookup("document")]
		private string _calibrateControllerButtonLabel = "Calibrate";

		// Token: 0x040018AA RID: 6314
		[SerializeField]
		[TextLookup("document")]
		private string _assignControllerButtonLabel = "Assign Controller";

		// Token: 0x040018AB RID: 6315
		[SerializeField]
		[TextLookup("document")]
		private string _inputBehaviorSettingsButtonLabel = "Sensitivity";

		// Token: 0x040018AC RID: 6316
		[SerializeField]
		[TextLookup("document")]
		private string _doneButtonLabel = "Done";

		// Token: 0x040018AD RID: 6317
		[SerializeField]
		[TextLookup("document")]
		private string _restoreDefaultsButtonLabel = "Restore Defaults";

		// Token: 0x040018AE RID: 6318
		[SerializeField]
		[TextLookup("document")]
		private string _playersGroupLabel = "Players:";

		// Token: 0x040018AF RID: 6319
		[SerializeField]
		[TextLookup("document")]
		private string _controllerSettingsGroupLabel = "Controller:";

		// Token: 0x040018B0 RID: 6320
		[SerializeField]
		[TextLookup("document")]
		private string _assignedControllersGroupLabel = "Assigned Controllers:";

		// Token: 0x040018B1 RID: 6321
		[SerializeField]
		[TextLookup("document")]
		private string _settingsGroupLabel = "Settings:";

		// Token: 0x040018B2 RID: 6322
		[SerializeField]
		[TextLookup("document")]
		private string _mapCategoriesGroupLabel = "Categories:";

		// Token: 0x040018B3 RID: 6323
		[SerializeField]
		[TextLookup("document")]
		private string _calibrateWindow_deadZoneSliderLabel = "Dead Zone:";

		// Token: 0x040018B4 RID: 6324
		[SerializeField]
		[TextLookup("document")]
		private string _calibrateWindow_zeroSliderLabel = "Zero:";

		// Token: 0x040018B5 RID: 6325
		[SerializeField]
		[TextLookup("document")]
		private string _calibrateWindow_sensitivitySliderLabel = "Sensitivity:";

		// Token: 0x040018B6 RID: 6326
		[SerializeField]
		[TextLookup("document")]
		private string _calibrateWindow_invertToggleLabel = "Invert";

		// Token: 0x040018B7 RID: 6327
		[SerializeField]
		[TextLookup("document")]
		private string _calibrateWindow_calibrateButtonLabel = "Calibrate";

		// Token: 0x040018B8 RID: 6328
		[SerializeField]
		private MLLanguageData.ModifierKeys _modifierKeys;

		// Token: 0x040018B9 RID: 6329
		[SerializeField]
		private MLLanguageData.CustomEntry[] _customEntries;

		// Token: 0x040018BA RID: 6330
		private bool _initialized;

		// Token: 0x040018BB RID: 6331
		private Dictionary<string, string> customDict;

		// Token: 0x0200048E RID: 1166
		[Serializable]
		protected class CustomEntry
		{
			// Token: 0x06001D4C RID: 7500 RVA: 0x00077FE9 File Offset: 0x000761E9
			public CustomEntry()
			{
			}

			// Token: 0x06001D4D RID: 7501 RVA: 0x00077FF1 File Offset: 0x000761F1
			public CustomEntry(string key, string value)
			{
				this.key = key;
				this.value = value;
			}

			// Token: 0x06001D4E RID: 7502 RVA: 0x00078008 File Offset: 0x00076208
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

			// Token: 0x04001EFA RID: 7930
			public string key;

			// Token: 0x04001EFB RID: 7931
			public string value;
		}

		// Token: 0x0200048F RID: 1167
		[Serializable]
		protected class ModifierKeys
		{
			// Token: 0x04001EFC RID: 7932
			[TextLookup("document")]
			public string control = "Control";

			// Token: 0x04001EFD RID: 7933
			[TextLookup("document")]
			public string alt = "Alt";

			// Token: 0x04001EFE RID: 7934
			[TextLookup("document")]
			public string shift = "Shift";

			// Token: 0x04001EFF RID: 7935
			[TextLookup("document")]
			public string command = "Command";

			// Token: 0x04001F00 RID: 7936
			[TextLookup("document")]
			public string separator = " + ";
		}
	}
}
