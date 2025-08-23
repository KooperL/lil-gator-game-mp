using System;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	[Serializable]
	public abstract class LanguageDataBase : ScriptableObject
	{
		public abstract void Initialize();

		public abstract string GetCustomEntry(string key);

		public abstract bool ContainsCustomEntryKey(string key);

		// (get) Token: 0x06001B95 RID: 7061
		public abstract string yes { get; }

		// (get) Token: 0x06001B96 RID: 7062
		public abstract string no { get; }

		// (get) Token: 0x06001B97 RID: 7063
		public abstract string add { get; }

		// (get) Token: 0x06001B98 RID: 7064
		public abstract string replace { get; }

		// (get) Token: 0x06001B99 RID: 7065
		public abstract string remove { get; }

		// (get) Token: 0x06001B9A RID: 7066
		public abstract string swap { get; }

		// (get) Token: 0x06001B9B RID: 7067
		public abstract string cancel { get; }

		// (get) Token: 0x06001B9C RID: 7068
		public abstract string none { get; }

		// (get) Token: 0x06001B9D RID: 7069
		public abstract string okay { get; }

		// (get) Token: 0x06001B9E RID: 7070
		public abstract string done { get; }

		// (get) Token: 0x06001B9F RID: 7071
		public abstract string default_ { get; }

		// (get) Token: 0x06001BA0 RID: 7072
		public abstract string assignControllerWindowTitle { get; }

		// (get) Token: 0x06001BA1 RID: 7073
		public abstract string assignControllerWindowMessage { get; }

		// (get) Token: 0x06001BA2 RID: 7074
		public abstract string controllerAssignmentConflictWindowTitle { get; }

		// (get) Token: 0x06001BA3 RID: 7075
		public abstract string elementAssignmentPrePollingWindowMessage { get; }

		// (get) Token: 0x06001BA4 RID: 7076
		public abstract string elementAssignmentConflictWindowMessage { get; }

		// (get) Token: 0x06001BA5 RID: 7077
		public abstract string elementAssignmentReplacementWindowMessage { get; }

		// (get) Token: 0x06001BA6 RID: 7078
		public abstract string mouseAssignmentConflictWindowTitle { get; }

		// (get) Token: 0x06001BA7 RID: 7079
		public abstract string calibrateControllerWindowTitle { get; }

		// (get) Token: 0x06001BA8 RID: 7080
		public abstract string calibrateAxisStep1WindowTitle { get; }

		// (get) Token: 0x06001BA9 RID: 7081
		public abstract string calibrateAxisStep2WindowTitle { get; }

		// (get) Token: 0x06001BAA RID: 7082
		public abstract string inputBehaviorSettingsWindowTitle { get; }

		// (get) Token: 0x06001BAB RID: 7083
		public abstract string restoreDefaultsWindowTitle { get; }

		// (get) Token: 0x06001BAC RID: 7084
		public abstract string actionColumnLabel { get; }

		// (get) Token: 0x06001BAD RID: 7085
		public abstract string keyboardColumnLabel { get; }

		// (get) Token: 0x06001BAE RID: 7086
		public abstract string mouseColumnLabel { get; }

		// (get) Token: 0x06001BAF RID: 7087
		public abstract string controllerColumnLabel { get; }

		// (get) Token: 0x06001BB0 RID: 7088
		public abstract string removeControllerButtonLabel { get; }

		// (get) Token: 0x06001BB1 RID: 7089
		public abstract string calibrateControllerButtonLabel { get; }

		// (get) Token: 0x06001BB2 RID: 7090
		public abstract string assignControllerButtonLabel { get; }

		// (get) Token: 0x06001BB3 RID: 7091
		public abstract string inputBehaviorSettingsButtonLabel { get; }

		// (get) Token: 0x06001BB4 RID: 7092
		public abstract string doneButtonLabel { get; }

		// (get) Token: 0x06001BB5 RID: 7093
		public abstract string restoreDefaultsButtonLabel { get; }

		// (get) Token: 0x06001BB6 RID: 7094
		public abstract string controllerSettingsGroupLabel { get; }

		// (get) Token: 0x06001BB7 RID: 7095
		public abstract string playersGroupLabel { get; }

		// (get) Token: 0x06001BB8 RID: 7096
		public abstract string assignedControllersGroupLabel { get; }

		// (get) Token: 0x06001BB9 RID: 7097
		public abstract string settingsGroupLabel { get; }

		// (get) Token: 0x06001BBA RID: 7098
		public abstract string mapCategoriesGroupLabel { get; }

		// (get) Token: 0x06001BBB RID: 7099
		public abstract string restoreDefaultsWindowMessage { get; }

		// (get) Token: 0x06001BBC RID: 7100
		public abstract string calibrateWindow_deadZoneSliderLabel { get; }

		// (get) Token: 0x06001BBD RID: 7101
		public abstract string calibrateWindow_zeroSliderLabel { get; }

		// (get) Token: 0x06001BBE RID: 7102
		public abstract string calibrateWindow_sensitivitySliderLabel { get; }

		// (get) Token: 0x06001BBF RID: 7103
		public abstract string calibrateWindow_invertToggleLabel { get; }

		// (get) Token: 0x06001BC0 RID: 7104
		public abstract string calibrateWindow_calibrateButtonLabel { get; }

		public abstract string GetControllerAssignmentConflictWindowMessage(string joystickName, string otherPlayerName, string currentPlayerName);

		public abstract string GetJoystickElementAssignmentPollingWindowMessage(string actionName);

		public abstract string GetJoystickElementAssignmentPollingWindowMessage_FullAxisFieldOnly(string actionName);

		public abstract string GetKeyboardElementAssignmentPollingWindowMessage(string actionName);

		public abstract string GetMouseElementAssignmentPollingWindowMessage(string actionName);

		public abstract string GetMouseElementAssignmentPollingWindowMessage_FullAxisFieldOnly(string actionName);

		public abstract string GetElementAlreadyInUseBlocked(string elementName);

		public abstract string GetElementAlreadyInUseCanReplace(string elementName, bool allowConflicts);

		public abstract string GetMouseAssignmentConflictWindowMessage(string otherPlayerName, string thisPlayerName);

		public abstract string GetCalibrateAxisStep1WindowMessage(string axisName);

		public abstract string GetCalibrateAxisStep2WindowMessage(string axisName);

		public abstract string GetPlayerName(int playerId);

		public abstract string GetControllerName(Controller controller);

		public abstract string GetElementIdentifierName(ActionElementMap actionElementMap);

		public abstract string GetElementIdentifierName(Controller controller, int elementIdentifierId, AxisRange axisRange);

		public abstract string GetElementIdentifierName(KeyCode keyCode, ModifierKeyFlags modifierKeyFlags);

		public abstract string GetActionName(int actionId);

		public abstract string GetActionName(int actionId, AxisRange axisRange);

		public abstract string GetMapCategoryName(int id);

		public abstract string GetActionCategoryName(int id);

		public abstract string GetLayoutName(ControllerType controllerType, int id);

		public abstract string ModifierKeyFlagsToString(ModifierKeyFlags flags);
	}
}
