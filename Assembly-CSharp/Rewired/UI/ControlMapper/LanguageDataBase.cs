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

		// (get) Token: 0x06001604 RID: 5636
		public abstract string yes { get; }

		// (get) Token: 0x06001605 RID: 5637
		public abstract string no { get; }

		// (get) Token: 0x06001606 RID: 5638
		public abstract string add { get; }

		// (get) Token: 0x06001607 RID: 5639
		public abstract string replace { get; }

		// (get) Token: 0x06001608 RID: 5640
		public abstract string remove { get; }

		// (get) Token: 0x06001609 RID: 5641
		public abstract string swap { get; }

		// (get) Token: 0x0600160A RID: 5642
		public abstract string cancel { get; }

		// (get) Token: 0x0600160B RID: 5643
		public abstract string none { get; }

		// (get) Token: 0x0600160C RID: 5644
		public abstract string okay { get; }

		// (get) Token: 0x0600160D RID: 5645
		public abstract string done { get; }

		// (get) Token: 0x0600160E RID: 5646
		public abstract string default_ { get; }

		// (get) Token: 0x0600160F RID: 5647
		public abstract string assignControllerWindowTitle { get; }

		// (get) Token: 0x06001610 RID: 5648
		public abstract string assignControllerWindowMessage { get; }

		// (get) Token: 0x06001611 RID: 5649
		public abstract string controllerAssignmentConflictWindowTitle { get; }

		// (get) Token: 0x06001612 RID: 5650
		public abstract string elementAssignmentPrePollingWindowMessage { get; }

		// (get) Token: 0x06001613 RID: 5651
		public abstract string elementAssignmentConflictWindowMessage { get; }

		// (get) Token: 0x06001614 RID: 5652
		public abstract string elementAssignmentReplacementWindowMessage { get; }

		// (get) Token: 0x06001615 RID: 5653
		public abstract string mouseAssignmentConflictWindowTitle { get; }

		// (get) Token: 0x06001616 RID: 5654
		public abstract string calibrateControllerWindowTitle { get; }

		// (get) Token: 0x06001617 RID: 5655
		public abstract string calibrateAxisStep1WindowTitle { get; }

		// (get) Token: 0x06001618 RID: 5656
		public abstract string calibrateAxisStep2WindowTitle { get; }

		// (get) Token: 0x06001619 RID: 5657
		public abstract string inputBehaviorSettingsWindowTitle { get; }

		// (get) Token: 0x0600161A RID: 5658
		public abstract string restoreDefaultsWindowTitle { get; }

		// (get) Token: 0x0600161B RID: 5659
		public abstract string actionColumnLabel { get; }

		// (get) Token: 0x0600161C RID: 5660
		public abstract string keyboardColumnLabel { get; }

		// (get) Token: 0x0600161D RID: 5661
		public abstract string mouseColumnLabel { get; }

		// (get) Token: 0x0600161E RID: 5662
		public abstract string controllerColumnLabel { get; }

		// (get) Token: 0x0600161F RID: 5663
		public abstract string removeControllerButtonLabel { get; }

		// (get) Token: 0x06001620 RID: 5664
		public abstract string calibrateControllerButtonLabel { get; }

		// (get) Token: 0x06001621 RID: 5665
		public abstract string assignControllerButtonLabel { get; }

		// (get) Token: 0x06001622 RID: 5666
		public abstract string inputBehaviorSettingsButtonLabel { get; }

		// (get) Token: 0x06001623 RID: 5667
		public abstract string doneButtonLabel { get; }

		// (get) Token: 0x06001624 RID: 5668
		public abstract string restoreDefaultsButtonLabel { get; }

		// (get) Token: 0x06001625 RID: 5669
		public abstract string controllerSettingsGroupLabel { get; }

		// (get) Token: 0x06001626 RID: 5670
		public abstract string playersGroupLabel { get; }

		// (get) Token: 0x06001627 RID: 5671
		public abstract string assignedControllersGroupLabel { get; }

		// (get) Token: 0x06001628 RID: 5672
		public abstract string settingsGroupLabel { get; }

		// (get) Token: 0x06001629 RID: 5673
		public abstract string mapCategoriesGroupLabel { get; }

		// (get) Token: 0x0600162A RID: 5674
		public abstract string restoreDefaultsWindowMessage { get; }

		// (get) Token: 0x0600162B RID: 5675
		public abstract string calibrateWindow_deadZoneSliderLabel { get; }

		// (get) Token: 0x0600162C RID: 5676
		public abstract string calibrateWindow_zeroSliderLabel { get; }

		// (get) Token: 0x0600162D RID: 5677
		public abstract string calibrateWindow_sensitivitySliderLabel { get; }

		// (get) Token: 0x0600162E RID: 5678
		public abstract string calibrateWindow_invertToggleLabel { get; }

		// (get) Token: 0x0600162F RID: 5679
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
