using System;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000464 RID: 1124
	[Serializable]
	public abstract class LanguageDataBase : ScriptableObject
	{
		// Token: 0x06001B31 RID: 6961
		public abstract void Initialize();

		// Token: 0x06001B32 RID: 6962
		public abstract string GetCustomEntry(string key);

		// Token: 0x06001B33 RID: 6963
		public abstract bool ContainsCustomEntryKey(string key);

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001B34 RID: 6964
		public abstract string yes { get; }

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001B35 RID: 6965
		public abstract string no { get; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001B36 RID: 6966
		public abstract string add { get; }

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001B37 RID: 6967
		public abstract string replace { get; }

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001B38 RID: 6968
		public abstract string remove { get; }

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001B39 RID: 6969
		public abstract string swap { get; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001B3A RID: 6970
		public abstract string cancel { get; }

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001B3B RID: 6971
		public abstract string none { get; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001B3C RID: 6972
		public abstract string okay { get; }

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001B3D RID: 6973
		public abstract string done { get; }

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001B3E RID: 6974
		public abstract string default_ { get; }

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001B3F RID: 6975
		public abstract string assignControllerWindowTitle { get; }

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001B40 RID: 6976
		public abstract string assignControllerWindowMessage { get; }

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001B41 RID: 6977
		public abstract string controllerAssignmentConflictWindowTitle { get; }

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001B42 RID: 6978
		public abstract string elementAssignmentPrePollingWindowMessage { get; }

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001B43 RID: 6979
		public abstract string elementAssignmentConflictWindowMessage { get; }

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001B44 RID: 6980
		public abstract string elementAssignmentReplacementWindowMessage { get; }

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001B45 RID: 6981
		public abstract string mouseAssignmentConflictWindowTitle { get; }

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001B46 RID: 6982
		public abstract string calibrateControllerWindowTitle { get; }

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001B47 RID: 6983
		public abstract string calibrateAxisStep1WindowTitle { get; }

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001B48 RID: 6984
		public abstract string calibrateAxisStep2WindowTitle { get; }

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001B49 RID: 6985
		public abstract string inputBehaviorSettingsWindowTitle { get; }

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001B4A RID: 6986
		public abstract string restoreDefaultsWindowTitle { get; }

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001B4B RID: 6987
		public abstract string actionColumnLabel { get; }

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001B4C RID: 6988
		public abstract string keyboardColumnLabel { get; }

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001B4D RID: 6989
		public abstract string mouseColumnLabel { get; }

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001B4E RID: 6990
		public abstract string controllerColumnLabel { get; }

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001B4F RID: 6991
		public abstract string removeControllerButtonLabel { get; }

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001B50 RID: 6992
		public abstract string calibrateControllerButtonLabel { get; }

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001B51 RID: 6993
		public abstract string assignControllerButtonLabel { get; }

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001B52 RID: 6994
		public abstract string inputBehaviorSettingsButtonLabel { get; }

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001B53 RID: 6995
		public abstract string doneButtonLabel { get; }

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001B54 RID: 6996
		public abstract string restoreDefaultsButtonLabel { get; }

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001B55 RID: 6997
		public abstract string controllerSettingsGroupLabel { get; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001B56 RID: 6998
		public abstract string playersGroupLabel { get; }

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001B57 RID: 6999
		public abstract string assignedControllersGroupLabel { get; }

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001B58 RID: 7000
		public abstract string settingsGroupLabel { get; }

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001B59 RID: 7001
		public abstract string mapCategoriesGroupLabel { get; }

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001B5A RID: 7002
		public abstract string restoreDefaultsWindowMessage { get; }

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001B5B RID: 7003
		public abstract string calibrateWindow_deadZoneSliderLabel { get; }

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001B5C RID: 7004
		public abstract string calibrateWindow_zeroSliderLabel { get; }

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001B5D RID: 7005
		public abstract string calibrateWindow_sensitivitySliderLabel { get; }

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001B5E RID: 7006
		public abstract string calibrateWindow_invertToggleLabel { get; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001B5F RID: 7007
		public abstract string calibrateWindow_calibrateButtonLabel { get; }

		// Token: 0x06001B60 RID: 7008
		public abstract string GetControllerAssignmentConflictWindowMessage(string joystickName, string otherPlayerName, string currentPlayerName);

		// Token: 0x06001B61 RID: 7009
		public abstract string GetJoystickElementAssignmentPollingWindowMessage(string actionName);

		// Token: 0x06001B62 RID: 7010
		public abstract string GetJoystickElementAssignmentPollingWindowMessage_FullAxisFieldOnly(string actionName);

		// Token: 0x06001B63 RID: 7011
		public abstract string GetKeyboardElementAssignmentPollingWindowMessage(string actionName);

		// Token: 0x06001B64 RID: 7012
		public abstract string GetMouseElementAssignmentPollingWindowMessage(string actionName);

		// Token: 0x06001B65 RID: 7013
		public abstract string GetMouseElementAssignmentPollingWindowMessage_FullAxisFieldOnly(string actionName);

		// Token: 0x06001B66 RID: 7014
		public abstract string GetElementAlreadyInUseBlocked(string elementName);

		// Token: 0x06001B67 RID: 7015
		public abstract string GetElementAlreadyInUseCanReplace(string elementName, bool allowConflicts);

		// Token: 0x06001B68 RID: 7016
		public abstract string GetMouseAssignmentConflictWindowMessage(string otherPlayerName, string thisPlayerName);

		// Token: 0x06001B69 RID: 7017
		public abstract string GetCalibrateAxisStep1WindowMessage(string axisName);

		// Token: 0x06001B6A RID: 7018
		public abstract string GetCalibrateAxisStep2WindowMessage(string axisName);

		// Token: 0x06001B6B RID: 7019
		public abstract string GetPlayerName(int playerId);

		// Token: 0x06001B6C RID: 7020
		public abstract string GetControllerName(Controller controller);

		// Token: 0x06001B6D RID: 7021
		public abstract string GetElementIdentifierName(ActionElementMap actionElementMap);

		// Token: 0x06001B6E RID: 7022
		public abstract string GetElementIdentifierName(Controller controller, int elementIdentifierId, AxisRange axisRange);

		// Token: 0x06001B6F RID: 7023
		public abstract string GetElementIdentifierName(KeyCode keyCode, ModifierKeyFlags modifierKeyFlags);

		// Token: 0x06001B70 RID: 7024
		public abstract string GetActionName(int actionId);

		// Token: 0x06001B71 RID: 7025
		public abstract string GetActionName(int actionId, AxisRange axisRange);

		// Token: 0x06001B72 RID: 7026
		public abstract string GetMapCategoryName(int id);

		// Token: 0x06001B73 RID: 7027
		public abstract string GetActionCategoryName(int id);

		// Token: 0x06001B74 RID: 7028
		public abstract string GetLayoutName(ControllerType controllerType, int id);

		// Token: 0x06001B75 RID: 7029
		public abstract string ModifierKeyFlagsToString(ModifierKeyFlags flags);
	}
}
