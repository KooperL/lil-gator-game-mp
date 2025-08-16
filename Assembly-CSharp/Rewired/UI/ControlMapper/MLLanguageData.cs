using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	[CreateAssetMenu(menuName = "Rewired/MLLanguageData")]
	[Serializable]
	public class MLLanguageData : LanguageDataBase
	{
		// Token: 0x06001BD7 RID: 7127 RVA: 0x000153DE File Offset: 0x000135DE
		public override void Initialize()
		{
			if (this._initialized)
			{
				return;
			}
			this.customDict = MLLanguageData.CustomEntry.ToDictionary(this._customEntries);
			this._initialized = true;
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x0006F75C File Offset: 0x0006D95C
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

		// Token: 0x06001BD9 RID: 7129 RVA: 0x00015401 File Offset: 0x00013601
		public override bool ContainsCustomEntryKey(string key)
		{
			return !string.IsNullOrEmpty(key) && this.customDict.ContainsKey(key);
		}

		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x00015419 File Offset: 0x00013619
		public override string yes
		{
			get
			{
				return this.document.FetchString(this._yes, Language.Auto);
			}
		}

		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x0001542E File Offset: 0x0001362E
		public override string no
		{
			get
			{
				return this.document.FetchString(this._no, Language.Auto);
			}
		}

		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x00015443 File Offset: 0x00013643
		public override string add
		{
			get
			{
				return this.document.FetchString(this._add, Language.Auto);
			}
		}

		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x00015458 File Offset: 0x00013658
		public override string replace
		{
			get
			{
				return this.document.FetchString(this._replace, Language.Auto);
			}
		}

		// (get) Token: 0x06001BDE RID: 7134 RVA: 0x0001546D File Offset: 0x0001366D
		public override string remove
		{
			get
			{
				return this.document.FetchString(this._remove, Language.Auto);
			}
		}

		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x00015482 File Offset: 0x00013682
		public override string swap
		{
			get
			{
				return this.document.FetchString(this._swap, Language.Auto);
			}
		}

		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x00015497 File Offset: 0x00013697
		public override string cancel
		{
			get
			{
				return this.document.FetchString(this._cancel, Language.Auto);
			}
		}

		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x000154AC File Offset: 0x000136AC
		public override string none
		{
			get
			{
				return this.document.FetchString(this._none, Language.Auto);
			}
		}

		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x000154C1 File Offset: 0x000136C1
		public override string okay
		{
			get
			{
				return this.document.FetchString(this._okay, Language.Auto);
			}
		}

		// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x000154D6 File Offset: 0x000136D6
		public override string done
		{
			get
			{
				return this.document.FetchString(this._done, Language.Auto);
			}
		}

		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x000154EB File Offset: 0x000136EB
		public override string default_
		{
			get
			{
				return this.document.FetchString(this._default, Language.Auto);
			}
		}

		// (get) Token: 0x06001BE5 RID: 7141 RVA: 0x00015500 File Offset: 0x00013700
		public override string assignControllerWindowTitle
		{
			get
			{
				return this.document.FetchString(this._assignControllerWindowTitle, Language.Auto);
			}
		}

		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x00015515 File Offset: 0x00013715
		public override string assignControllerWindowMessage
		{
			get
			{
				return this.document.FetchString(this._assignControllerWindowMessage, Language.Auto);
			}
		}

		// (get) Token: 0x06001BE7 RID: 7143 RVA: 0x0001552A File Offset: 0x0001372A
		public override string controllerAssignmentConflictWindowTitle
		{
			get
			{
				return this.document.FetchString(this._controllerAssignmentConflictWindowTitle, Language.Auto);
			}
		}

		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x0001553F File Offset: 0x0001373F
		public override string elementAssignmentPrePollingWindowMessage
		{
			get
			{
				return this.document.FetchString(this._elementAssignmentPrePollingWindowMessage, Language.Auto);
			}
		}

		// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x00015554 File Offset: 0x00013754
		public override string elementAssignmentConflictWindowMessage
		{
			get
			{
				return this.document.FetchString(this._elementAssignmentConflictWindowMessage, Language.Auto);
			}
		}

		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x00015569 File Offset: 0x00013769
		public override string elementAssignmentReplacementWindowMessage
		{
			get
			{
				return this.document.FetchString(this._elementAssignmentReplacementWindowMessage, Language.Auto);
			}
		}

		// (get) Token: 0x06001BEB RID: 7147 RVA: 0x0001557E File Offset: 0x0001377E
		public override string mouseAssignmentConflictWindowTitle
		{
			get
			{
				return this.document.FetchString(this._mouseAssignmentConflictWindowTitle, Language.Auto);
			}
		}

		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x00015593 File Offset: 0x00013793
		public override string calibrateControllerWindowTitle
		{
			get
			{
				return this.document.FetchString(this._calibrateControllerWindowTitle, Language.Auto);
			}
		}

		// (get) Token: 0x06001BED RID: 7149 RVA: 0x000155A8 File Offset: 0x000137A8
		public override string calibrateAxisStep1WindowTitle
		{
			get
			{
				return this.document.FetchString(this._calibrateAxisStep1WindowTitle, Language.Auto);
			}
		}

		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x000155BD File Offset: 0x000137BD
		public override string calibrateAxisStep2WindowTitle
		{
			get
			{
				return this.document.FetchString(this._calibrateAxisStep2WindowTitle, Language.Auto);
			}
		}

		// (get) Token: 0x06001BEF RID: 7151 RVA: 0x000155D2 File Offset: 0x000137D2
		public override string inputBehaviorSettingsWindowTitle
		{
			get
			{
				return this.document.FetchString(this._inputBehaviorSettingsWindowTitle, Language.Auto);
			}
		}

		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x000155E7 File Offset: 0x000137E7
		public override string restoreDefaultsWindowTitle
		{
			get
			{
				return this.document.FetchString(this._restoreDefaultsWindowTitle, Language.Auto);
			}
		}

		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x000155FC File Offset: 0x000137FC
		public override string actionColumnLabel
		{
			get
			{
				return this.document.FetchString(this._actionColumnLabel, Language.Auto);
			}
		}

		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x00015611 File Offset: 0x00013811
		public override string keyboardColumnLabel
		{
			get
			{
				return this.document.FetchString(this._keyboardColumnLabel, Language.Auto);
			}
		}

		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x00015626 File Offset: 0x00013826
		public override string mouseColumnLabel
		{
			get
			{
				return this.document.FetchString(this._mouseColumnLabel, Language.Auto);
			}
		}

		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x0001563B File Offset: 0x0001383B
		public override string controllerColumnLabel
		{
			get
			{
				return this.document.FetchString(this._controllerColumnLabel, Language.Auto);
			}
		}

		// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x00015650 File Offset: 0x00013850
		public override string removeControllerButtonLabel
		{
			get
			{
				return this.document.FetchString(this._removeControllerButtonLabel, Language.Auto);
			}
		}

		// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x00015665 File Offset: 0x00013865
		public override string calibrateControllerButtonLabel
		{
			get
			{
				return this.document.FetchString(this._calibrateControllerButtonLabel, Language.Auto);
			}
		}

		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x0001567A File Offset: 0x0001387A
		public override string assignControllerButtonLabel
		{
			get
			{
				return this.document.FetchString(this._assignControllerButtonLabel, Language.Auto);
			}
		}

		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x0001568F File Offset: 0x0001388F
		public override string inputBehaviorSettingsButtonLabel
		{
			get
			{
				return this.document.FetchString(this._inputBehaviorSettingsButtonLabel, Language.Auto);
			}
		}

		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x000156A4 File Offset: 0x000138A4
		public override string doneButtonLabel
		{
			get
			{
				return this.document.FetchString(this._doneButtonLabel, Language.Auto);
			}
		}

		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x000156B9 File Offset: 0x000138B9
		public override string restoreDefaultsButtonLabel
		{
			get
			{
				return this.document.FetchString(this._restoreDefaultsButtonLabel, Language.Auto);
			}
		}

		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x000156CE File Offset: 0x000138CE
		public override string controllerSettingsGroupLabel
		{
			get
			{
				return this.document.FetchString(this._controllerSettingsGroupLabel, Language.Auto);
			}
		}

		// (get) Token: 0x06001BFC RID: 7164 RVA: 0x000156E3 File Offset: 0x000138E3
		public override string playersGroupLabel
		{
			get
			{
				return this.document.FetchString(this._playersGroupLabel, Language.Auto);
			}
		}

		// (get) Token: 0x06001BFD RID: 7165 RVA: 0x000156F8 File Offset: 0x000138F8
		public override string assignedControllersGroupLabel
		{
			get
			{
				return this.document.FetchString(this._assignedControllersGroupLabel, Language.Auto);
			}
		}

		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x0001570D File Offset: 0x0001390D
		public override string settingsGroupLabel
		{
			get
			{
				return this.document.FetchString(this._settingsGroupLabel, Language.Auto);
			}
		}

		// (get) Token: 0x06001BFF RID: 7167 RVA: 0x00015722 File Offset: 0x00013922
		public override string mapCategoriesGroupLabel
		{
			get
			{
				return this.document.FetchString(this._mapCategoriesGroupLabel, Language.Auto);
			}
		}

		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x00015737 File Offset: 0x00013937
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

		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x0001576D File Offset: 0x0001396D
		public override string calibrateWindow_deadZoneSliderLabel
		{
			get
			{
				return this.document.FetchString(this._calibrateWindow_deadZoneSliderLabel, Language.Auto);
			}
		}

		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x00015782 File Offset: 0x00013982
		public override string calibrateWindow_zeroSliderLabel
		{
			get
			{
				return this.document.FetchString(this._calibrateWindow_zeroSliderLabel, Language.Auto);
			}
		}

		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x00015797 File Offset: 0x00013997
		public override string calibrateWindow_sensitivitySliderLabel
		{
			get
			{
				return this.document.FetchString(this._calibrateWindow_sensitivitySliderLabel, Language.Auto);
			}
		}

		// (get) Token: 0x06001C04 RID: 7172 RVA: 0x000157AC File Offset: 0x000139AC
		public override string calibrateWindow_invertToggleLabel
		{
			get
			{
				return this.document.FetchString(this._calibrateWindow_invertToggleLabel, Language.Auto);
			}
		}

		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x000157C1 File Offset: 0x000139C1
		public override string calibrateWindow_calibrateButtonLabel
		{
			get
			{
				return this.document.FetchString(this._calibrateWindow_calibrateButtonLabel, Language.Auto);
			}
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x000157D6 File Offset: 0x000139D6
		public override string GetControllerAssignmentConflictWindowMessage(string joystickName, string otherPlayerName, string currentPlayerName)
		{
			return string.Format(this.document.FetchString(this._controllerAssignmentConflictWindowMessage, Language.Auto), joystickName, otherPlayerName, currentPlayerName);
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x000157F3 File Offset: 0x000139F3
		public override string GetJoystickElementAssignmentPollingWindowMessage(string actionName)
		{
			return string.Format(this.document.FetchString(this._joystickElementAssignmentPollingWindowMessage, Language.Auto), actionName);
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x0001580E File Offset: 0x00013A0E
		public override string GetJoystickElementAssignmentPollingWindowMessage_FullAxisFieldOnly(string actionName)
		{
			return string.Format(this.document.FetchString(this._joystickElementAssignmentPollingWindowMessage_fullAxisFieldOnly, Language.Auto), actionName);
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x00015829 File Offset: 0x00013A29
		public override string GetKeyboardElementAssignmentPollingWindowMessage(string actionName)
		{
			return string.Format(this.document.FetchString(this._keyboardElementAssignmentPollingWindowMessage, Language.Auto), actionName);
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x00015844 File Offset: 0x00013A44
		public override string GetMouseElementAssignmentPollingWindowMessage(string actionName)
		{
			return string.Format(this.document.FetchString(this._mouseElementAssignmentPollingWindowMessage, Language.Auto), actionName);
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x0001585F File Offset: 0x00013A5F
		public override string GetMouseElementAssignmentPollingWindowMessage_FullAxisFieldOnly(string actionName)
		{
			return string.Format(this.document.FetchString(this._mouseElementAssignmentPollingWindowMessage_fullAxisFieldOnly, Language.Auto), actionName);
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x0001587A File Offset: 0x00013A7A
		public override string GetElementAlreadyInUseBlocked(string elementName)
		{
			return string.Format(this.document.FetchString(this._elementAlreadyInUseBlocked, Language.Auto), elementName);
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x00015895 File Offset: 0x00013A95
		public override string GetElementAlreadyInUseCanReplace(string elementName, bool allowConflicts)
		{
			if (!allowConflicts)
			{
				return string.Format(this.document.FetchString(this._elementAlreadyInUseCanReplace, Language.Auto), elementName);
			}
			return string.Format(this.document.FetchString(this._elementAlreadyInUseCanReplace_conflictAllowed, Language.Auto), elementName);
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x000158CD File Offset: 0x00013ACD
		public override string GetMouseAssignmentConflictWindowMessage(string otherPlayerName, string thisPlayerName)
		{
			return string.Format(this.document.FetchString(this._mouseAssignmentConflictWindowMessage, Language.Auto), otherPlayerName, thisPlayerName);
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x000158E9 File Offset: 0x00013AE9
		public override string GetCalibrateAxisStep1WindowMessage(string axisName)
		{
			return string.Format(this.document.FetchString(this._calibrateAxisStep1WindowMessage, Language.Auto), axisName);
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x00015904 File Offset: 0x00013B04
		public override string GetCalibrateAxisStep2WindowMessage(string axisName)
		{
			return string.Format(this.document.FetchString(this._calibrateAxisStep2WindowMessage, Language.Auto), axisName);
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x00015256 File Offset: 0x00013456
		public override string GetPlayerName(int playerId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				throw new ArgumentException("Invalid player id: " + playerId.ToString());
			}
			return player.descriptiveName;
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x00015282 File Offset: 0x00013482
		public override string GetControllerName(Controller controller)
		{
			if (controller == null)
			{
				throw new ArgumentNullException("controller");
			}
			return controller.name;
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x0006F244 File Offset: 0x0006D444
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

		// Token: 0x06001C14 RID: 7188 RVA: 0x0006F790 File Offset: 0x0006D990
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

		// Token: 0x06001C15 RID: 7189 RVA: 0x0006F82C File Offset: 0x0006DA2C
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

		// Token: 0x06001C16 RID: 7190 RVA: 0x0006F874 File Offset: 0x0006DA74
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

		// Token: 0x06001C17 RID: 7191 RVA: 0x0006F920 File Offset: 0x0006DB20
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

		// Token: 0x06001C18 RID: 7192 RVA: 0x000152F2 File Offset: 0x000134F2
		public override string GetMapCategoryName(int id)
		{
			InputMapCategory mapCategory = ReInput.mapping.GetMapCategory(id);
			if (mapCategory == null)
			{
				throw new ArgumentException("Invalid map category id: " + id.ToString());
			}
			return mapCategory.descriptiveName;
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x0001531E File Offset: 0x0001351E
		public override string GetActionCategoryName(int id)
		{
			InputCategory actionCategory = ReInput.mapping.GetActionCategory(id);
			if (actionCategory == null)
			{
				throw new ArgumentException("Invalid action category id: " + id.ToString());
			}
			return actionCategory.descriptiveName;
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x0001534A File Offset: 0x0001354A
		public override string GetLayoutName(ControllerType controllerType, int id)
		{
			InputLayout layout = ReInput.mapping.GetLayout(controllerType, id);
			if (layout == null)
			{
				throw new ArgumentException("Invalid " + controllerType.ToString() + " layout id: " + id.ToString());
			}
			return layout.descriptiveName;
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x0006FA04 File Offset: 0x0006DC04
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

		public MultilingualTextDocument document;

		[SerializeField]
		[TextLookup("document")]
		private string _yes = "Yes";

		[SerializeField]
		[TextLookup("document")]
		private string _no = "No";

		[SerializeField]
		[TextLookup("document")]
		private string _add = "Add";

		[SerializeField]
		[TextLookup("document")]
		private string _replace = "Replace";

		[SerializeField]
		[TextLookup("document")]
		private string _remove = "Remove";

		[SerializeField]
		[TextLookup("document")]
		private string _swap = "Swap";

		[SerializeField]
		[TextLookup("document")]
		private string _cancel = "Cancel";

		[SerializeField]
		[TextLookup("document")]
		private string _none = "None";

		[SerializeField]
		[TextLookup("document")]
		private string _okay = "Okay";

		[SerializeField]
		[TextLookup("document")]
		private string _done = "Done";

		[SerializeField]
		[TextLookup("document")]
		private string _default = "Default";

		[SerializeField]
		[TextLookup("document")]
		private string _assignControllerWindowTitle = "Choose Controller";

		[SerializeField]
		[TextLookup("document")]
		private string _assignControllerWindowMessage = "Press any button or move an axis on the controller you would like to use.";

		[SerializeField]
		[TextLookup("document")]
		private string _controllerAssignmentConflictWindowTitle = "Controller Assignment";

		[SerializeField]
		[TextLookup("document")]
		[Tooltip("{0} = Joystick Name\n{1} = Other Player Name\n{2} = This Player Name")]
		private string _controllerAssignmentConflictWindowMessage = "{0} is already assigned to {1}. Do you want to assign this controller to {2} instead?";

		[SerializeField]
		[TextLookup("document")]
		private string _elementAssignmentPrePollingWindowMessage = "First center or zero all sticks and axes and press any button or wait for the timer to finish.";

		[SerializeField]
		[Tooltip("{0} = Action Name")]
		[TextLookup("document")]
		private string _joystickElementAssignmentPollingWindowMessage = "Now press a button or move an axis to assign it to {0}.";

		[SerializeField]
		[Tooltip("This text is only displayed when split-axis fields have been disabled and the user clicks on the full-axis field. Button/key/D-pad input cannot be assigned to a full-axis field.\n{0} = Action Name")]
		[TextLookup("document")]
		private string _joystickElementAssignmentPollingWindowMessage_fullAxisFieldOnly = "Now move an axis to assign it to {0}.";

		[SerializeField]
		[Tooltip("{0} = Action Name")]
		[TextLookup("document")]
		private string _keyboardElementAssignmentPollingWindowMessage = "Press a key to assign it to {0}. Modifier keys may also be used. To assign a modifier key alone, hold it down for 1 second.";

		[SerializeField]
		[Tooltip("{0} = Action Name")]
		[TextLookup("document")]
		private string _mouseElementAssignmentPollingWindowMessage = "Press a mouse button or move an axis to assign it to {0}.";

		[SerializeField]
		[Tooltip("This text is only displayed when split-axis fields have been disabled and the user clicks on the full-axis field. Button/key/D-pad input cannot be assigned to a full-axis field.\n{0} = Action Name")]
		[TextLookup("document")]
		private string _mouseElementAssignmentPollingWindowMessage_fullAxisFieldOnly = "Move an axis to assign it to {0}.";

		[SerializeField]
		[TextLookup("document")]
		private string _elementAssignmentConflictWindowMessage = "Assignment Conflict";

		[SerializeField]
		[TextLookup("document")]
		private string _elementAssignmentReplacementWindowMessage = "Assignment Conflict";

		[SerializeField]
		[Tooltip("{0} = Element Name")]
		[TextLookup("document")]
		private string _elementAlreadyInUseBlocked = "{0} is already in use cannot be replaced.";

		[SerializeField]
		[Tooltip("{0} = Element Name")]
		[TextLookup("document")]
		private string _elementAlreadyInUseCanReplace = "{0} is already in use. Do you want to replace it?";

		[SerializeField]
		[Tooltip("{0} = Element Name")]
		[TextLookup("document")]
		private string _elementAlreadyInUseCanReplace_conflictAllowed = "{0} is already in use. Do you want to replace it? You may also choose to add the assignment anyway.";

		[SerializeField]
		[TextLookup("document")]
		private string _mouseAssignmentConflictWindowTitle = "Mouse Assignment";

		[SerializeField]
		[Tooltip("{0} = Other Player Name\n{1} = This Player Name")]
		[TextLookup("document")]
		private string _mouseAssignmentConflictWindowMessage = "The mouse is already assigned to {0}. Do you want to assign the mouse to {1} instead?";

		[SerializeField]
		[TextLookup("document")]
		private string _calibrateControllerWindowTitle = "Calibrate Controller";

		[SerializeField]
		[TextLookup("document")]
		private string _calibrateAxisStep1WindowTitle = "Calibrate Zero";

		[SerializeField]
		[Tooltip("{0} = Axis Name")]
		[TextLookup("document")]
		private string _calibrateAxisStep1WindowMessage = "Center or zero {0} and press any button or wait for the timer to finish.";

		[SerializeField]
		[TextLookup("document")]
		private string _calibrateAxisStep2WindowTitle = "Calibrate Range";

		[SerializeField]
		[Tooltip("{0} = Axis Name")]
		[TextLookup("document")]
		private string _calibrateAxisStep2WindowMessage = "Move {0} through its entire range then press any button or wait for the timer to finish.";

		[SerializeField]
		[TextLookup("document")]
		private string _inputBehaviorSettingsWindowTitle = "Sensitivity Settings";

		[SerializeField]
		[TextLookup("document")]
		private string _restoreDefaultsWindowTitle = "Restore Defaults";

		[SerializeField]
		[Tooltip("Message for a single player game.")]
		[TextLookup("document")]
		private string _restoreDefaultsWindowMessage_onePlayer = "This will restore the default input configuration. Are you sure you want to do this?";

		[SerializeField]
		[Tooltip("Message for a multi-player game.")]
		[TextLookup("document")]
		private string _restoreDefaultsWindowMessage_multiPlayer = "This will restore the default input configuration for all players. Are you sure you want to do this?";

		[SerializeField]
		[TextLookup("document")]
		private string _actionColumnLabel = "Actions";

		[SerializeField]
		[TextLookup("document")]
		private string _keyboardColumnLabel = "Keyboard";

		[SerializeField]
		[TextLookup("document")]
		private string _mouseColumnLabel = "Mouse";

		[SerializeField]
		[TextLookup("document")]
		private string _controllerColumnLabel = "Controller";

		[SerializeField]
		[TextLookup("document")]
		private string _removeControllerButtonLabel = "Remove";

		[SerializeField]
		[TextLookup("document")]
		private string _calibrateControllerButtonLabel = "Calibrate";

		[SerializeField]
		[TextLookup("document")]
		private string _assignControllerButtonLabel = "Assign Controller";

		[SerializeField]
		[TextLookup("document")]
		private string _inputBehaviorSettingsButtonLabel = "Sensitivity";

		[SerializeField]
		[TextLookup("document")]
		private string _doneButtonLabel = "Done";

		[SerializeField]
		[TextLookup("document")]
		private string _restoreDefaultsButtonLabel = "Restore Defaults";

		[SerializeField]
		[TextLookup("document")]
		private string _playersGroupLabel = "Players:";

		[SerializeField]
		[TextLookup("document")]
		private string _controllerSettingsGroupLabel = "Controller:";

		[SerializeField]
		[TextLookup("document")]
		private string _assignedControllersGroupLabel = "Assigned Controllers:";

		[SerializeField]
		[TextLookup("document")]
		private string _settingsGroupLabel = "Settings:";

		[SerializeField]
		[TextLookup("document")]
		private string _mapCategoriesGroupLabel = "Categories:";

		[SerializeField]
		[TextLookup("document")]
		private string _calibrateWindow_deadZoneSliderLabel = "Dead Zone:";

		[SerializeField]
		[TextLookup("document")]
		private string _calibrateWindow_zeroSliderLabel = "Zero:";

		[SerializeField]
		[TextLookup("document")]
		private string _calibrateWindow_sensitivitySliderLabel = "Sensitivity:";

		[SerializeField]
		[TextLookup("document")]
		private string _calibrateWindow_invertToggleLabel = "Invert";

		[SerializeField]
		[TextLookup("document")]
		private string _calibrateWindow_calibrateButtonLabel = "Calibrate";

		[SerializeField]
		private MLLanguageData.ModifierKeys _modifierKeys;

		[SerializeField]
		private MLLanguageData.CustomEntry[] _customEntries;

		private bool _initialized;

		private Dictionary<string, string> customDict;

		[Serializable]
		protected class CustomEntry
		{
			// Token: 0x06001C1D RID: 7197 RVA: 0x000022AD File Offset: 0x000004AD
			public CustomEntry()
			{
			}

			// Token: 0x06001C1E RID: 7198 RVA: 0x0001591F File Offset: 0x00013B1F
			public CustomEntry(string key, string value)
			{
				this.key = key;
				this.value = value;
			}

			// Token: 0x06001C1F RID: 7199 RVA: 0x0006FD88 File Offset: 0x0006DF88
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

			public string key;

			public string value;
		}

		[Serializable]
		protected class ModifierKeys
		{
			[TextLookup("document")]
			public string control = "Control";

			[TextLookup("document")]
			public string alt = "Alt";

			[TextLookup("document")]
			public string shift = "Shift";

			[TextLookup("document")]
			public string command = "Command";

			[TextLookup("document")]
			public string separator = " + ";
		}
	}
}
