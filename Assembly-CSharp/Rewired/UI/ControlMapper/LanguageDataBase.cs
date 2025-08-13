using System;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000326 RID: 806
	[Serializable]
	public abstract class LanguageDataBase : ScriptableObject
	{
		// Token: 0x06001601 RID: 5633
		public abstract void Initialize();

		// Token: 0x06001602 RID: 5634
		public abstract string GetCustomEntry(string key);

		// Token: 0x06001603 RID: 5635
		public abstract bool ContainsCustomEntryKey(string key);

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001604 RID: 5636
		public abstract string yes { get; }

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001605 RID: 5637
		public abstract string no { get; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001606 RID: 5638
		public abstract string add { get; }

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001607 RID: 5639
		public abstract string replace { get; }

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001608 RID: 5640
		public abstract string remove { get; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001609 RID: 5641
		public abstract string swap { get; }

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x0600160A RID: 5642
		public abstract string cancel { get; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x0600160B RID: 5643
		public abstract string none { get; }

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600160C RID: 5644
		public abstract string okay { get; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600160D RID: 5645
		public abstract string done { get; }

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x0600160E RID: 5646
		public abstract string default_ { get; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x0600160F RID: 5647
		public abstract string assignControllerWindowTitle { get; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001610 RID: 5648
		public abstract string assignControllerWindowMessage { get; }

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001611 RID: 5649
		public abstract string controllerAssignmentConflictWindowTitle { get; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001612 RID: 5650
		public abstract string elementAssignmentPrePollingWindowMessage { get; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001613 RID: 5651
		public abstract string elementAssignmentConflictWindowMessage { get; }

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001614 RID: 5652
		public abstract string elementAssignmentReplacementWindowMessage { get; }

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001615 RID: 5653
		public abstract string mouseAssignmentConflictWindowTitle { get; }

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001616 RID: 5654
		public abstract string calibrateControllerWindowTitle { get; }

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001617 RID: 5655
		public abstract string calibrateAxisStep1WindowTitle { get; }

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001618 RID: 5656
		public abstract string calibrateAxisStep2WindowTitle { get; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001619 RID: 5657
		public abstract string inputBehaviorSettingsWindowTitle { get; }

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x0600161A RID: 5658
		public abstract string restoreDefaultsWindowTitle { get; }

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x0600161B RID: 5659
		public abstract string actionColumnLabel { get; }

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600161C RID: 5660
		public abstract string keyboardColumnLabel { get; }

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600161D RID: 5661
		public abstract string mouseColumnLabel { get; }

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x0600161E RID: 5662
		public abstract string controllerColumnLabel { get; }

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600161F RID: 5663
		public abstract string removeControllerButtonLabel { get; }

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001620 RID: 5664
		public abstract string calibrateControllerButtonLabel { get; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001621 RID: 5665
		public abstract string assignControllerButtonLabel { get; }

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001622 RID: 5666
		public abstract string inputBehaviorSettingsButtonLabel { get; }

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001623 RID: 5667
		public abstract string doneButtonLabel { get; }

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001624 RID: 5668
		public abstract string restoreDefaultsButtonLabel { get; }

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001625 RID: 5669
		public abstract string controllerSettingsGroupLabel { get; }

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001626 RID: 5670
		public abstract string playersGroupLabel { get; }

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001627 RID: 5671
		public abstract string assignedControllersGroupLabel { get; }

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001628 RID: 5672
		public abstract string settingsGroupLabel { get; }

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001629 RID: 5673
		public abstract string mapCategoriesGroupLabel { get; }

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600162A RID: 5674
		public abstract string restoreDefaultsWindowMessage { get; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x0600162B RID: 5675
		public abstract string calibrateWindow_deadZoneSliderLabel { get; }

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x0600162C RID: 5676
		public abstract string calibrateWindow_zeroSliderLabel { get; }

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x0600162D RID: 5677
		public abstract string calibrateWindow_sensitivitySliderLabel { get; }

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x0600162E RID: 5678
		public abstract string calibrateWindow_invertToggleLabel { get; }

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x0600162F RID: 5679
		public abstract string calibrateWindow_calibrateButtonLabel { get; }

		// Token: 0x06001630 RID: 5680
		public abstract string GetControllerAssignmentConflictWindowMessage(string joystickName, string otherPlayerName, string currentPlayerName);

		// Token: 0x06001631 RID: 5681
		public abstract string GetJoystickElementAssignmentPollingWindowMessage(string actionName);

		// Token: 0x06001632 RID: 5682
		public abstract string GetJoystickElementAssignmentPollingWindowMessage_FullAxisFieldOnly(string actionName);

		// Token: 0x06001633 RID: 5683
		public abstract string GetKeyboardElementAssignmentPollingWindowMessage(string actionName);

		// Token: 0x06001634 RID: 5684
		public abstract string GetMouseElementAssignmentPollingWindowMessage(string actionName);

		// Token: 0x06001635 RID: 5685
		public abstract string GetMouseElementAssignmentPollingWindowMessage_FullAxisFieldOnly(string actionName);

		// Token: 0x06001636 RID: 5686
		public abstract string GetElementAlreadyInUseBlocked(string elementName);

		// Token: 0x06001637 RID: 5687
		public abstract string GetElementAlreadyInUseCanReplace(string elementName, bool allowConflicts);

		// Token: 0x06001638 RID: 5688
		public abstract string GetMouseAssignmentConflictWindowMessage(string otherPlayerName, string thisPlayerName);

		// Token: 0x06001639 RID: 5689
		public abstract string GetCalibrateAxisStep1WindowMessage(string axisName);

		// Token: 0x0600163A RID: 5690
		public abstract string GetCalibrateAxisStep2WindowMessage(string axisName);

		// Token: 0x0600163B RID: 5691
		public abstract string GetPlayerName(int playerId);

		// Token: 0x0600163C RID: 5692
		public abstract string GetControllerName(Controller controller);

		// Token: 0x0600163D RID: 5693
		public abstract string GetElementIdentifierName(ActionElementMap actionElementMap);

		// Token: 0x0600163E RID: 5694
		public abstract string GetElementIdentifierName(Controller controller, int elementIdentifierId, AxisRange axisRange);

		// Token: 0x0600163F RID: 5695
		public abstract string GetElementIdentifierName(KeyCode keyCode, ModifierKeyFlags modifierKeyFlags);

		// Token: 0x06001640 RID: 5696
		public abstract string GetActionName(int actionId);

		// Token: 0x06001641 RID: 5697
		public abstract string GetActionName(int actionId, AxisRange axisRange);

		// Token: 0x06001642 RID: 5698
		public abstract string GetMapCategoryName(int id);

		// Token: 0x06001643 RID: 5699
		public abstract string GetActionCategoryName(int id);

		// Token: 0x06001644 RID: 5700
		public abstract string GetLayoutName(ControllerType controllerType, int id);

		// Token: 0x06001645 RID: 5701
		public abstract string ModifierKeyFlagsToString(ModifierKeyFlags flags);
	}
}
