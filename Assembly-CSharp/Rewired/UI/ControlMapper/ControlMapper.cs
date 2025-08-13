using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Rewired.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000431 RID: 1073
	[AddComponentMenu("")]
	public class ControlMapper : MonoBehaviour
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060017CF RID: 6095 RVA: 0x000125D9 File Offset: 0x000107D9
		// (remove) Token: 0x060017D0 RID: 6096 RVA: 0x000125F2 File Offset: 0x000107F2
		public event Action ScreenClosedEvent
		{
			add
			{
				this._ScreenClosedEvent = (Action)Delegate.Combine(this._ScreenClosedEvent, value);
			}
			remove
			{
				this._ScreenClosedEvent = (Action)Delegate.Remove(this._ScreenClosedEvent, value);
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060017D1 RID: 6097 RVA: 0x0001260B File Offset: 0x0001080B
		// (remove) Token: 0x060017D2 RID: 6098 RVA: 0x00012624 File Offset: 0x00010824
		public event Action ScreenOpenedEvent
		{
			add
			{
				this._ScreenOpenedEvent = (Action)Delegate.Combine(this._ScreenOpenedEvent, value);
			}
			remove
			{
				this._ScreenOpenedEvent = (Action)Delegate.Remove(this._ScreenOpenedEvent, value);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060017D3 RID: 6099 RVA: 0x0001263D File Offset: 0x0001083D
		// (remove) Token: 0x060017D4 RID: 6100 RVA: 0x00012656 File Offset: 0x00010856
		public event Action PopupWindowClosedEvent
		{
			add
			{
				this._PopupWindowClosedEvent = (Action)Delegate.Combine(this._PopupWindowClosedEvent, value);
			}
			remove
			{
				this._PopupWindowClosedEvent = (Action)Delegate.Remove(this._PopupWindowClosedEvent, value);
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060017D5 RID: 6101 RVA: 0x0001266F File Offset: 0x0001086F
		// (remove) Token: 0x060017D6 RID: 6102 RVA: 0x00012688 File Offset: 0x00010888
		public event Action PopupWindowOpenedEvent
		{
			add
			{
				this._PopupWindowOpenedEvent = (Action)Delegate.Combine(this._PopupWindowOpenedEvent, value);
			}
			remove
			{
				this._PopupWindowOpenedEvent = (Action)Delegate.Remove(this._PopupWindowOpenedEvent, value);
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060017D7 RID: 6103 RVA: 0x000126A1 File Offset: 0x000108A1
		// (remove) Token: 0x060017D8 RID: 6104 RVA: 0x000126BA File Offset: 0x000108BA
		public event Action InputPollingStartedEvent
		{
			add
			{
				this._InputPollingStartedEvent = (Action)Delegate.Combine(this._InputPollingStartedEvent, value);
			}
			remove
			{
				this._InputPollingStartedEvent = (Action)Delegate.Remove(this._InputPollingStartedEvent, value);
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060017D9 RID: 6105 RVA: 0x000126D3 File Offset: 0x000108D3
		// (remove) Token: 0x060017DA RID: 6106 RVA: 0x000126EC File Offset: 0x000108EC
		public event Action InputPollingEndedEvent
		{
			add
			{
				this._InputPollingEndedEvent = (Action)Delegate.Combine(this._InputPollingEndedEvent, value);
			}
			remove
			{
				this._InputPollingEndedEvent = (Action)Delegate.Remove(this._InputPollingEndedEvent, value);
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060017DB RID: 6107 RVA: 0x00012705 File Offset: 0x00010905
		// (remove) Token: 0x060017DC RID: 6108 RVA: 0x00012713 File Offset: 0x00010913
		public event UnityAction onScreenClosed
		{
			add
			{
				this._onScreenClosed.AddListener(value);
			}
			remove
			{
				this._onScreenClosed.RemoveListener(value);
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060017DD RID: 6109 RVA: 0x00012721 File Offset: 0x00010921
		// (remove) Token: 0x060017DE RID: 6110 RVA: 0x0001272F File Offset: 0x0001092F
		public event UnityAction onScreenOpened
		{
			add
			{
				this._onScreenOpened.AddListener(value);
			}
			remove
			{
				this._onScreenOpened.RemoveListener(value);
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060017DF RID: 6111 RVA: 0x0001273D File Offset: 0x0001093D
		// (remove) Token: 0x060017E0 RID: 6112 RVA: 0x0001274B File Offset: 0x0001094B
		public event UnityAction onPopupWindowClosed
		{
			add
			{
				this._onPopupWindowClosed.AddListener(value);
			}
			remove
			{
				this._onPopupWindowClosed.RemoveListener(value);
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060017E1 RID: 6113 RVA: 0x00012759 File Offset: 0x00010959
		// (remove) Token: 0x060017E2 RID: 6114 RVA: 0x00012767 File Offset: 0x00010967
		public event UnityAction onPopupWindowOpened
		{
			add
			{
				this._onPopupWindowOpened.AddListener(value);
			}
			remove
			{
				this._onPopupWindowOpened.RemoveListener(value);
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060017E3 RID: 6115 RVA: 0x00012775 File Offset: 0x00010975
		// (remove) Token: 0x060017E4 RID: 6116 RVA: 0x00012783 File Offset: 0x00010983
		public event UnityAction onInputPollingStarted
		{
			add
			{
				this._onInputPollingStarted.AddListener(value);
			}
			remove
			{
				this._onInputPollingStarted.RemoveListener(value);
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060017E5 RID: 6117 RVA: 0x00012791 File Offset: 0x00010991
		// (remove) Token: 0x060017E6 RID: 6118 RVA: 0x0001279F File Offset: 0x0001099F
		public event UnityAction onInputPollingEnded
		{
			add
			{
				this._onInputPollingEnded.AddListener(value);
			}
			remove
			{
				this._onInputPollingEnded.RemoveListener(value);
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x000127AD File Offset: 0x000109AD
		// (set) Token: 0x060017E8 RID: 6120 RVA: 0x000127B5 File Offset: 0x000109B5
		public InputManager rewiredInputManager
		{
			get
			{
				return this._rewiredInputManager;
			}
			set
			{
				this._rewiredInputManager = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x000127C5 File Offset: 0x000109C5
		// (set) Token: 0x060017EA RID: 6122 RVA: 0x000127CD File Offset: 0x000109CD
		public bool dontDestroyOnLoad
		{
			get
			{
				return this._dontDestroyOnLoad;
			}
			set
			{
				if (value != this._dontDestroyOnLoad && value)
				{
					Object.DontDestroyOnLoad(base.transform.gameObject);
				}
				this._dontDestroyOnLoad = value;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x000127F2 File Offset: 0x000109F2
		// (set) Token: 0x060017EC RID: 6124 RVA: 0x000127FA File Offset: 0x000109FA
		public int keyboardMapDefaultLayout
		{
			get
			{
				return this._keyboardMapDefaultLayout;
			}
			set
			{
				this._keyboardMapDefaultLayout = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x0001280A File Offset: 0x00010A0A
		// (set) Token: 0x060017EE RID: 6126 RVA: 0x00012812 File Offset: 0x00010A12
		public int mouseMapDefaultLayout
		{
			get
			{
				return this._mouseMapDefaultLayout;
			}
			set
			{
				this._mouseMapDefaultLayout = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x00012822 File Offset: 0x00010A22
		// (set) Token: 0x060017F0 RID: 6128 RVA: 0x0001282A File Offset: 0x00010A2A
		public int joystickMapDefaultLayout
		{
			get
			{
				return this._joystickMapDefaultLayout;
			}
			set
			{
				this._joystickMapDefaultLayout = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x0001283A File Offset: 0x00010A3A
		// (set) Token: 0x060017F2 RID: 6130 RVA: 0x00012853 File Offset: 0x00010A53
		public bool showPlayers
		{
			get
			{
				return this._showPlayers && ReInput.players.playerCount > 1;
			}
			set
			{
				this._showPlayers = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x00012863 File Offset: 0x00010A63
		// (set) Token: 0x060017F4 RID: 6132 RVA: 0x0001286B File Offset: 0x00010A6B
		public bool showControllers
		{
			get
			{
				return this._showControllers;
			}
			set
			{
				this._showControllers = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x0001287B File Offset: 0x00010A7B
		// (set) Token: 0x060017F6 RID: 6134 RVA: 0x00012883 File Offset: 0x00010A83
		public bool showKeyboard
		{
			get
			{
				return this._showKeyboard;
			}
			set
			{
				this._showKeyboard = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x00012893 File Offset: 0x00010A93
		// (set) Token: 0x060017F8 RID: 6136 RVA: 0x0001289B File Offset: 0x00010A9B
		public bool showMouse
		{
			get
			{
				return this._showMouse;
			}
			set
			{
				this._showMouse = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x000128AB File Offset: 0x00010AAB
		// (set) Token: 0x060017FA RID: 6138 RVA: 0x000128B3 File Offset: 0x00010AB3
		public int maxControllersPerPlayer
		{
			get
			{
				return this._maxControllersPerPlayer;
			}
			set
			{
				this._maxControllersPerPlayer = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x000128C3 File Offset: 0x00010AC3
		// (set) Token: 0x060017FC RID: 6140 RVA: 0x000128CB File Offset: 0x00010ACB
		public bool showActionCategoryLabels
		{
			get
			{
				return this._showActionCategoryLabels;
			}
			set
			{
				this._showActionCategoryLabels = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x000128DB File Offset: 0x00010ADB
		// (set) Token: 0x060017FE RID: 6142 RVA: 0x000128E3 File Offset: 0x00010AE3
		public int keyboardInputFieldCount
		{
			get
			{
				return this._keyboardInputFieldCount;
			}
			set
			{
				this._keyboardInputFieldCount = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x000128F3 File Offset: 0x00010AF3
		// (set) Token: 0x06001800 RID: 6144 RVA: 0x000128FB File Offset: 0x00010AFB
		public int mouseInputFieldCount
		{
			get
			{
				return this._mouseInputFieldCount;
			}
			set
			{
				this._mouseInputFieldCount = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x0001290B File Offset: 0x00010B0B
		// (set) Token: 0x06001802 RID: 6146 RVA: 0x00012913 File Offset: 0x00010B13
		public int controllerInputFieldCount
		{
			get
			{
				return this._controllerInputFieldCount;
			}
			set
			{
				this._controllerInputFieldCount = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x00012923 File Offset: 0x00010B23
		// (set) Token: 0x06001804 RID: 6148 RVA: 0x0001292B File Offset: 0x00010B2B
		public bool showFullAxisInputFields
		{
			get
			{
				return this._showFullAxisInputFields;
			}
			set
			{
				this._showFullAxisInputFields = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x0001293B File Offset: 0x00010B3B
		// (set) Token: 0x06001806 RID: 6150 RVA: 0x00012943 File Offset: 0x00010B43
		public bool showSplitAxisInputFields
		{
			get
			{
				return this._showSplitAxisInputFields;
			}
			set
			{
				this._showSplitAxisInputFields = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x00012953 File Offset: 0x00010B53
		// (set) Token: 0x06001808 RID: 6152 RVA: 0x0001295B File Offset: 0x00010B5B
		public bool allowElementAssignmentConflicts
		{
			get
			{
				return this._allowElementAssignmentConflicts;
			}
			set
			{
				this._allowElementAssignmentConflicts = value;
				this.InspectorPropertyChanged(false);
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x0001296B File Offset: 0x00010B6B
		// (set) Token: 0x0600180A RID: 6154 RVA: 0x00012973 File Offset: 0x00010B73
		public bool allowElementAssignmentSwap
		{
			get
			{
				return this._allowElementAssignmentSwap;
			}
			set
			{
				this._allowElementAssignmentSwap = value;
				this.InspectorPropertyChanged(false);
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x00012983 File Offset: 0x00010B83
		// (set) Token: 0x0600180C RID: 6156 RVA: 0x0001298B File Offset: 0x00010B8B
		public int actionLabelWidth
		{
			get
			{
				return this._actionLabelWidth;
			}
			set
			{
				this._actionLabelWidth = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x0001299B File Offset: 0x00010B9B
		// (set) Token: 0x0600180E RID: 6158 RVA: 0x000129A3 File Offset: 0x00010BA3
		public int keyboardColMaxWidth
		{
			get
			{
				return this._keyboardColMaxWidth;
			}
			set
			{
				this._keyboardColMaxWidth = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x000129B3 File Offset: 0x00010BB3
		// (set) Token: 0x06001810 RID: 6160 RVA: 0x000129BB File Offset: 0x00010BBB
		public int mouseColMaxWidth
		{
			get
			{
				return this._mouseColMaxWidth;
			}
			set
			{
				this._mouseColMaxWidth = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x000129CB File Offset: 0x00010BCB
		// (set) Token: 0x06001812 RID: 6162 RVA: 0x000129D3 File Offset: 0x00010BD3
		public int controllerColMaxWidth
		{
			get
			{
				return this._controllerColMaxWidth;
			}
			set
			{
				this._controllerColMaxWidth = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x000129E3 File Offset: 0x00010BE3
		// (set) Token: 0x06001814 RID: 6164 RVA: 0x000129EB File Offset: 0x00010BEB
		public int inputRowHeight
		{
			get
			{
				return this._inputRowHeight;
			}
			set
			{
				this._inputRowHeight = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x000129FB File Offset: 0x00010BFB
		// (set) Token: 0x06001816 RID: 6166 RVA: 0x00012A03 File Offset: 0x00010C03
		public int inputColumnSpacing
		{
			get
			{
				return this._inputColumnSpacing;
			}
			set
			{
				this._inputColumnSpacing = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x00012A13 File Offset: 0x00010C13
		// (set) Token: 0x06001818 RID: 6168 RVA: 0x00012A1B File Offset: 0x00010C1B
		public int inputRowCategorySpacing
		{
			get
			{
				return this._inputRowCategorySpacing;
			}
			set
			{
				this._inputRowCategorySpacing = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x00012A2B File Offset: 0x00010C2B
		// (set) Token: 0x0600181A RID: 6170 RVA: 0x00012A33 File Offset: 0x00010C33
		public int invertToggleWidth
		{
			get
			{
				return this._invertToggleWidth;
			}
			set
			{
				this._invertToggleWidth = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x00012A43 File Offset: 0x00010C43
		// (set) Token: 0x0600181C RID: 6172 RVA: 0x00012A4B File Offset: 0x00010C4B
		public int defaultWindowWidth
		{
			get
			{
				return this._defaultWindowWidth;
			}
			set
			{
				this._defaultWindowWidth = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x00012A5B File Offset: 0x00010C5B
		// (set) Token: 0x0600181E RID: 6174 RVA: 0x00012A63 File Offset: 0x00010C63
		public int defaultWindowHeight
		{
			get
			{
				return this._defaultWindowHeight;
			}
			set
			{
				this._defaultWindowHeight = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x00012A73 File Offset: 0x00010C73
		// (set) Token: 0x06001820 RID: 6176 RVA: 0x00012A7B File Offset: 0x00010C7B
		public float controllerAssignmentTimeout
		{
			get
			{
				return this._controllerAssignmentTimeout;
			}
			set
			{
				this._controllerAssignmentTimeout = value;
				this.InspectorPropertyChanged(false);
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001821 RID: 6177 RVA: 0x00012A8B File Offset: 0x00010C8B
		// (set) Token: 0x06001822 RID: 6178 RVA: 0x00012A93 File Offset: 0x00010C93
		public float preInputAssignmentTimeout
		{
			get
			{
				return this._preInputAssignmentTimeout;
			}
			set
			{
				this._preInputAssignmentTimeout = value;
				this.InspectorPropertyChanged(false);
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001823 RID: 6179 RVA: 0x00012AA3 File Offset: 0x00010CA3
		// (set) Token: 0x06001824 RID: 6180 RVA: 0x00012AAB File Offset: 0x00010CAB
		public float inputAssignmentTimeout
		{
			get
			{
				return this._inputAssignmentTimeout;
			}
			set
			{
				this._inputAssignmentTimeout = value;
				this.InspectorPropertyChanged(false);
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x00012ABB File Offset: 0x00010CBB
		// (set) Token: 0x06001826 RID: 6182 RVA: 0x00012AC3 File Offset: 0x00010CC3
		public float axisCalibrationTimeout
		{
			get
			{
				return this._axisCalibrationTimeout;
			}
			set
			{
				this._axisCalibrationTimeout = value;
				this.InspectorPropertyChanged(false);
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001827 RID: 6183 RVA: 0x00012AD3 File Offset: 0x00010CD3
		// (set) Token: 0x06001828 RID: 6184 RVA: 0x00012ADB File Offset: 0x00010CDB
		public bool ignoreMouseXAxisAssignment
		{
			get
			{
				return this._ignoreMouseXAxisAssignment;
			}
			set
			{
				this._ignoreMouseXAxisAssignment = value;
				this.InspectorPropertyChanged(false);
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001829 RID: 6185 RVA: 0x00012AEB File Offset: 0x00010CEB
		// (set) Token: 0x0600182A RID: 6186 RVA: 0x00012AF3 File Offset: 0x00010CF3
		public bool ignoreMouseYAxisAssignment
		{
			get
			{
				return this._ignoreMouseYAxisAssignment;
			}
			set
			{
				this._ignoreMouseYAxisAssignment = value;
				this.InspectorPropertyChanged(false);
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x00012B03 File Offset: 0x00010D03
		// (set) Token: 0x0600182C RID: 6188 RVA: 0x00012B0B File Offset: 0x00010D0B
		public bool universalCancelClosesScreen
		{
			get
			{
				return this._universalCancelClosesScreen;
			}
			set
			{
				this._universalCancelClosesScreen = value;
				this.InspectorPropertyChanged(false);
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x00012B1B File Offset: 0x00010D1B
		// (set) Token: 0x0600182E RID: 6190 RVA: 0x00012B23 File Offset: 0x00010D23
		public bool showInputBehaviorSettings
		{
			get
			{
				return this._showInputBehaviorSettings;
			}
			set
			{
				this._showInputBehaviorSettings = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x00012B33 File Offset: 0x00010D33
		// (set) Token: 0x06001830 RID: 6192 RVA: 0x00012B3B File Offset: 0x00010D3B
		public bool useThemeSettings
		{
			get
			{
				return this._useThemeSettings;
			}
			set
			{
				this._useThemeSettings = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x00012B4B File Offset: 0x00010D4B
		// (set) Token: 0x06001832 RID: 6194 RVA: 0x00012B53 File Offset: 0x00010D53
		public LanguageDataBase language
		{
			get
			{
				return this._language;
			}
			set
			{
				this._language = value;
				if (this._language != null)
				{
					this._language.Initialize();
				}
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x00012B7C File Offset: 0x00010D7C
		// (set) Token: 0x06001834 RID: 6196 RVA: 0x00012B84 File Offset: 0x00010D84
		public bool showPlayersGroupLabel
		{
			get
			{
				return this._showPlayersGroupLabel;
			}
			set
			{
				this._showPlayersGroupLabel = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x00012B94 File Offset: 0x00010D94
		// (set) Token: 0x06001836 RID: 6198 RVA: 0x00012B9C File Offset: 0x00010D9C
		public bool showControllerGroupLabel
		{
			get
			{
				return this._showControllerGroupLabel;
			}
			set
			{
				this._showControllerGroupLabel = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x00012BAC File Offset: 0x00010DAC
		// (set) Token: 0x06001838 RID: 6200 RVA: 0x00012BB4 File Offset: 0x00010DB4
		public bool showAssignedControllersGroupLabel
		{
			get
			{
				return this._showAssignedControllersGroupLabel;
			}
			set
			{
				this._showAssignedControllersGroupLabel = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x00012BC4 File Offset: 0x00010DC4
		// (set) Token: 0x0600183A RID: 6202 RVA: 0x00012BCC File Offset: 0x00010DCC
		public bool showSettingsGroupLabel
		{
			get
			{
				return this._showSettingsGroupLabel;
			}
			set
			{
				this._showSettingsGroupLabel = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x00012BDC File Offset: 0x00010DDC
		// (set) Token: 0x0600183C RID: 6204 RVA: 0x00012BE4 File Offset: 0x00010DE4
		public bool showMapCategoriesGroupLabel
		{
			get
			{
				return this._showMapCategoriesGroupLabel;
			}
			set
			{
				this._showMapCategoriesGroupLabel = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x00012BF4 File Offset: 0x00010DF4
		// (set) Token: 0x0600183E RID: 6206 RVA: 0x00012BFC File Offset: 0x00010DFC
		public bool showControllerNameLabel
		{
			get
			{
				return this._showControllerNameLabel;
			}
			set
			{
				this._showControllerNameLabel = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x00012C0C File Offset: 0x00010E0C
		// (set) Token: 0x06001840 RID: 6208 RVA: 0x00012C14 File Offset: 0x00010E14
		public bool showAssignedControllers
		{
			get
			{
				return this._showAssignedControllers;
			}
			set
			{
				this._showAssignedControllers = value;
				this.InspectorPropertyChanged(true);
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x00012C24 File Offset: 0x00010E24
		// (set) Token: 0x06001842 RID: 6210 RVA: 0x00012C2C File Offset: 0x00010E2C
		public Action restoreDefaultsDelegate
		{
			get
			{
				return this._restoreDefaultsDelegate;
			}
			set
			{
				this._restoreDefaultsDelegate = value;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x00012C35 File Offset: 0x00010E35
		public bool isOpen
		{
			get
			{
				if (!this.initialized)
				{
					return this.references.canvas != null && this.references.canvas.gameObject.activeInHierarchy;
				}
				return this.canvas.activeInHierarchy;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x00012C75 File Offset: 0x00010E75
		private bool isFocused
		{
			get
			{
				return this.initialized && !this.windowManager.isWindowOpen;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x00012C8F File Offset: 0x00010E8F
		private bool inputAllowed
		{
			get
			{
				return this.blockInputOnFocusEndTime <= Time.unscaledTime;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x00064ED8 File Offset: 0x000630D8
		private int inputGridColumnCount
		{
			get
			{
				int num = 1;
				if (this._showKeyboard)
				{
					num++;
				}
				if (this._showMouse)
				{
					num++;
				}
				if (this._showControllers)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001847 RID: 6215 RVA: 0x00064F0C File Offset: 0x0006310C
		private int inputGridWidth
		{
			get
			{
				return this._actionLabelWidth + (this._showKeyboard ? this._keyboardColMaxWidth : 0) + (this._showMouse ? this._mouseColMaxWidth : 0) + (this._showControllers ? this._controllerColMaxWidth : 0) + (this.inputGridColumnCount - 1) * this._inputColumnSpacing;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x00012CA1 File Offset: 0x00010EA1
		private Player currentPlayer
		{
			get
			{
				return ReInput.players.GetPlayer(this.currentPlayerId);
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x00012CB3 File Offset: 0x00010EB3
		private InputCategory currentMapCategory
		{
			get
			{
				return ReInput.mapping.GetMapCategory(this.currentMapCategoryId);
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x00064F68 File Offset: 0x00063168
		private ControlMapper.MappingSet currentMappingSet
		{
			get
			{
				if (this.currentMapCategoryId < 0)
				{
					return null;
				}
				for (int i = 0; i < this._mappingSets.Length; i++)
				{
					if (this._mappingSets[i].mapCategoryId == this.currentMapCategoryId)
					{
						return this._mappingSets[i];
					}
				}
				return null;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x00012CC5 File Offset: 0x00010EC5
		private Joystick currentJoystick
		{
			get
			{
				return ReInput.controllers.GetJoystick(this.currentJoystickId);
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x00012CD7 File Offset: 0x00010ED7
		private bool isJoystickSelected
		{
			get
			{
				return this.currentJoystickId >= 0;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x00012CE5 File Offset: 0x00010EE5
		private GameObject currentUISelection
		{
			get
			{
				if (!(EventSystem.current != null))
				{
					return null;
				}
				return EventSystem.current.currentSelectedGameObject;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x00012D00 File Offset: 0x00010F00
		private bool showSettings
		{
			get
			{
				return this._showInputBehaviorSettings && this._inputBehaviorSettings.Length != 0;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x00012D16 File Offset: 0x00010F16
		private bool showMapCategories
		{
			get
			{
				return this._mappingSets != null && this._mappingSets.Length > 1;
			}
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x00012D30 File Offset: 0x00010F30
		private void Awake()
		{
			if (this._dontDestroyOnLoad)
			{
				Object.DontDestroyOnLoad(base.transform.gameObject);
			}
			this.PreInitialize();
			if (this.isOpen)
			{
				this.Initialize();
				this.Open(true);
			}
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x00012D65 File Offset: 0x00010F65
		private void Start()
		{
			if (this._openOnStart)
			{
				this.Open(false);
			}
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x00012D76 File Offset: 0x00010F76
		private void Update()
		{
			if (!this.isOpen)
			{
				return;
			}
			if (!this.initialized)
			{
				return;
			}
			this.CheckUISelection();
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x00064FB4 File Offset: 0x000631B4
		private void OnDestroy()
		{
			ReInput.ControllerConnectedEvent -= this.OnJoystickConnected;
			ReInput.ControllerDisconnectedEvent -= this.OnJoystickDisconnected;
			ReInput.ControllerPreDisconnectEvent -= this.OnJoystickPreDisconnect;
			if (ReInput.players != null && ReInput.players.GetPlayer(0) != null)
			{
				ReInput.players.GetPlayer(0).controllers.RemoveLastActiveControllerChangedDelegate(new PlayerActiveControllerChangedDelegate(this.OnLastActiveControllerChanged));
			}
			this.UnsubscribeMenuControlInputEvents();
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x00012D90 File Offset: 0x00010F90
		private void PreInitialize()
		{
			if (!ReInput.isReady)
			{
				Debug.LogError("Rewired Control Mapper: Rewired has not been initialized! Are you missing a Rewired Input Manager in your scene?");
				return;
			}
			this.SubscribeMenuControlInputEvents();
			this.isPreInitialized = true;
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x00065030 File Offset: 0x00063230
		private void Initialize()
		{
			if (!this.isPreInitialized)
			{
				this.PreInitialize();
			}
			if (this.initialized)
			{
				return;
			}
			if (!ReInput.isReady)
			{
				return;
			}
			if (this._rewiredInputManager == null)
			{
				this._rewiredInputManager = Object.FindObjectOfType<InputManager>();
				if (this._rewiredInputManager == null)
				{
					Debug.LogError("Rewired Control Mapper: A Rewired Input Manager was not assigned in the inspector or found in the current scene! Control Mapper will not function.");
					return;
				}
			}
			if (ControlMapper.Instance != null)
			{
				Debug.LogError("Rewired Control Mapper: Only one ControlMapper can exist at one time!");
				return;
			}
			ControlMapper.Instance = this;
			if (this.prefabs == null || !this.prefabs.Check())
			{
				Debug.LogError("Rewired Control Mapper: All prefabs must be assigned in the inspector!");
				return;
			}
			if (this.references == null || !this.references.Check())
			{
				Debug.LogError("Rewired Control Mapper: All references must be assigned in the inspector!");
				return;
			}
			this.references.inputGridLayoutElement = this.references.inputGridContainer.GetComponent<LayoutElement>();
			if (this.references.inputGridLayoutElement == null)
			{
				Debug.LogError("Rewired Control Mapper: InputGridContainer is missing LayoutElement component!");
				return;
			}
			if (this._showKeyboard && this._keyboardInputFieldCount < 1)
			{
				Debug.LogWarning("Rewired Control Mapper: Keyboard Input Fields must be at least 1!");
				this._keyboardInputFieldCount = 1;
			}
			if (this._showMouse && this._mouseInputFieldCount < 1)
			{
				Debug.LogWarning("Rewired Control Mapper: Mouse Input Fields must be at least 1!");
				this._mouseInputFieldCount = 1;
			}
			if (this._showControllers && this._controllerInputFieldCount < 1)
			{
				Debug.LogWarning("Rewired Control Mapper: Controller Input Fields must be at least 1!");
				this._controllerInputFieldCount = 1;
			}
			if (this._maxControllersPerPlayer < 0)
			{
				Debug.LogWarning("Rewired Control Mapper: Max Controllers Per Player must be at least 0 (no limit)!");
				this._maxControllersPerPlayer = 0;
			}
			if (this._useThemeSettings && this._themeSettings == null)
			{
				Debug.LogWarning("Rewired Control Mapper: To use theming, Theme Settings must be set in the inspector! Theming has been disabled.");
				this._useThemeSettings = false;
			}
			if (this._language == null)
			{
				Debug.LogError("Rawired UI: Language must be set in the inspector!");
				return;
			}
			this._language.Initialize();
			this.inputFieldActivatedDelegate = new Action<InputFieldInfo>(this.OnInputFieldActivated);
			this.inputFieldInvertToggleStateChangedDelegate = new Action<ToggleInfo, bool>(this.OnInputFieldInvertToggleStateChanged);
			ReInput.ControllerConnectedEvent += this.OnJoystickConnected;
			ReInput.ControllerDisconnectedEvent += this.OnJoystickDisconnected;
			ReInput.ControllerPreDisconnectEvent += this.OnJoystickPreDisconnect;
			ReInput.players.GetPlayer(0).controllers.AddLastActiveControllerChangedDelegate(new PlayerActiveControllerChangedDelegate(this.OnLastActiveControllerChanged));
			this.playerCount = ReInput.players.playerCount;
			this.canvas = this.references.canvas.gameObject;
			this.windowManager = new ControlMapper.WindowManager(this.prefabs.window, this.prefabs.fader, this.references.canvas.transform);
			this.playerButtons = new List<ControlMapper.GUIButton>();
			this.mapCategoryButtons = new List<ControlMapper.GUIButton>();
			this.assignedControllerButtons = new List<ControlMapper.GUIButton>();
			this.miscInstantiatedObjects = new List<GameObject>();
			this.currentMapCategoryId = this._mappingSets[0].mapCategoryId;
			this.Draw();
			this.CreateInputGrid();
			this.CreateLayout();
			this.SubscribeFixedUISelectionEvents();
			this.initialized = true;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x00065320 File Offset: 0x00063520
		private void OnLastActiveControllerChanged(Player player, Controller controller)
		{
			if (!this.initialized)
			{
				return;
			}
			if (!this._showControllers)
			{
				return;
			}
			if (controller is Joystick && controller.id != this.currentJoystickId)
			{
				this.ClearVarsOnJoystickChange();
				this.currentJoystickId = controller.id;
				this.ForceRefresh();
			}
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x00012DB1 File Offset: 0x00010FB1
		private void OnJoystickConnected(ControllerStatusChangedEventArgs args)
		{
			if (!this.initialized)
			{
				return;
			}
			if (!this._showControllers)
			{
				return;
			}
			this.ClearVarsOnJoystickChange();
			this.ForceRefresh();
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x00012DB1 File Offset: 0x00010FB1
		private void OnJoystickDisconnected(ControllerStatusChangedEventArgs args)
		{
			if (!this.initialized)
			{
				return;
			}
			if (!this._showControllers)
			{
				return;
			}
			this.ClearVarsOnJoystickChange();
			this.ForceRefresh();
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x00012DD1 File Offset: 0x00010FD1
		private void OnJoystickPreDisconnect(ControllerStatusChangedEventArgs args)
		{
			if (!this.initialized)
			{
				return;
			}
			bool showControllers = this._showControllers;
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x00065370 File Offset: 0x00063570
		public void OnButtonActivated(ButtonInfo buttonInfo)
		{
			if (!this.initialized)
			{
				return;
			}
			if (!this.inputAllowed)
			{
				return;
			}
			string identifier = buttonInfo.identifier;
			if (identifier != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(identifier);
				if (num <= 1656078790U)
				{
					if (num <= 1293854844U)
					{
						if (num != 36291085U)
						{
							if (num != 1293854844U)
							{
								return;
							}
							if (!(identifier == "AssignController"))
							{
								return;
							}
							this.ShowAssignControllerWindow();
							return;
						}
						else
						{
							if (!(identifier == "MapCategorySelection"))
							{
								return;
							}
							this.OnMapCategorySelected(buttonInfo.intData, true);
							return;
						}
					}
					else if (num != 1619204974U)
					{
						if (num != 1656078790U)
						{
							return;
						}
						if (!(identifier == "EditInputBehaviors"))
						{
							return;
						}
						this.ShowEditInputBehaviorsWindow();
						return;
					}
					else
					{
						if (!(identifier == "PlayerSelection"))
						{
							return;
						}
						this.OnPlayerSelected(buttonInfo.intData, true);
						return;
					}
				}
				else if (num <= 2379421585U)
				{
					if (num != 2139278426U)
					{
						if (num != 2379421585U)
						{
							return;
						}
						if (!(identifier == "Done"))
						{
							return;
						}
						this.Close(true);
						return;
					}
					else
					{
						if (!(identifier == "CalibrateController"))
						{
							return;
						}
						this.ShowCalibrateControllerWindow();
						return;
					}
				}
				else if (num != 2857234147U)
				{
					if (num != 3019194153U)
					{
						if (num != 3496297297U)
						{
							return;
						}
						if (!(identifier == "AssignedControllerSelection"))
						{
							return;
						}
						this.OnControllerSelected(buttonInfo.intData);
						return;
					}
					else
					{
						if (!(identifier == "RemoveController"))
						{
							return;
						}
						this.OnRemoveCurrentController();
						return;
					}
				}
				else
				{
					if (!(identifier == "RestoreDefaults"))
					{
						return;
					}
					this.OnRestoreDefaults();
				}
			}
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x000654E0 File Offset: 0x000636E0
		public void OnInputFieldActivated(InputFieldInfo fieldInfo)
		{
			if (!this.initialized)
			{
				return;
			}
			if (!this.inputAllowed)
			{
				return;
			}
			if (this.currentPlayer == null)
			{
				return;
			}
			InputAction action = ReInput.mapping.GetAction(fieldInfo.actionId);
			if (action == null)
			{
				return;
			}
			AxisRange axisRange = ((action.type == null) ? fieldInfo.axisRange : 0);
			string actionName = this._language.GetActionName(action.id, axisRange);
			ControllerMap controllerMap = this.GetControllerMap(fieldInfo.controllerType);
			if (controllerMap == null)
			{
				return;
			}
			ActionElementMap actionElementMap = ((fieldInfo.actionElementMapId >= 0) ? controllerMap.GetElementMap(fieldInfo.actionElementMapId) : null);
			if (actionElementMap != null)
			{
				this.ShowBeginElementAssignmentReplacementWindow(fieldInfo, action, controllerMap, actionElementMap, actionName);
				return;
			}
			this.ShowCreateNewElementAssignmentWindow(fieldInfo, action, controllerMap, actionName);
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x00012DE3 File Offset: 0x00010FE3
		public void OnInputFieldInvertToggleStateChanged(ToggleInfo toggleInfo, bool newState)
		{
			if (!this.initialized)
			{
				return;
			}
			if (!this.inputAllowed)
			{
				return;
			}
			this.SetActionAxisInverted(newState, toggleInfo.controllerType, toggleInfo.actionElementMapId);
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x00012E0A File Offset: 0x0001100A
		private void OnPlayerSelected(int playerId, bool redraw)
		{
			if (!this.initialized)
			{
				return;
			}
			this.currentPlayerId = playerId;
			this.ClearVarsOnPlayerChange();
			if (redraw)
			{
				this.Redraw(true, true);
			}
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x00012E2D File Offset: 0x0001102D
		private void OnControllerSelected(int joystickId)
		{
			if (!this.initialized)
			{
				return;
			}
			this.currentJoystickId = joystickId;
			this.Redraw(true, true);
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x00012E47 File Offset: 0x00011047
		private void OnRemoveCurrentController()
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			if (this.currentJoystickId < 0)
			{
				return;
			}
			this.RemoveController(this.currentPlayer, this.currentJoystickId);
			this.ClearVarsOnJoystickChange();
			this.Redraw(false, false);
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x00012E7C File Offset: 0x0001107C
		private void OnMapCategorySelected(int id, bool redraw)
		{
			if (!this.initialized)
			{
				return;
			}
			this.currentMapCategoryId = id;
			if (redraw)
			{
				this.Redraw(true, true);
			}
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x00012E99 File Offset: 0x00011099
		private void OnRestoreDefaults()
		{
			if (!this.initialized)
			{
				return;
			}
			this.ShowRestoreDefaultsWindow();
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x00012EAA File Offset: 0x000110AA
		private void OnScreenToggleActionPressed(InputActionEventData data)
		{
			if (!this.isOpen)
			{
				this.Open();
				return;
			}
			if (!this.initialized)
			{
				return;
			}
			if (!this.isFocused)
			{
				return;
			}
			this.Close(true);
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x00012ED4 File Offset: 0x000110D4
		private void OnScreenOpenActionPressed(InputActionEventData data)
		{
			this.Open();
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x00012EDC File Offset: 0x000110DC
		private void OnScreenCloseActionPressed(InputActionEventData data)
		{
			if (!this.initialized)
			{
				return;
			}
			if (!this.isOpen)
			{
				return;
			}
			if (!this.isFocused)
			{
				return;
			}
			this.Close(true);
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x00012F00 File Offset: 0x00011100
		private void OnUniversalCancelActionPressed(InputActionEventData data)
		{
			if (!this.initialized)
			{
				return;
			}
			if (!this.isOpen)
			{
				return;
			}
			if (this._universalCancelClosesScreen)
			{
				if (this.isFocused)
				{
					this.Close(true);
					return;
				}
			}
			else if (this.isFocused)
			{
				return;
			}
			this.CloseAllWindows();
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x00012F3B File Offset: 0x0001113B
		private void OnWindowCancel(int windowId)
		{
			if (!this.initialized)
			{
				return;
			}
			if (windowId < 0)
			{
				return;
			}
			this.CloseWindow(windowId);
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x00012F52 File Offset: 0x00011152
		private void OnRemoveElementAssignment(int windowId, ControllerMap map, ActionElementMap aem)
		{
			if (map == null || aem == null)
			{
				return;
			}
			map.DeleteElementMap(aem.id);
			this.CloseWindow(windowId);
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x00065588 File Offset: 0x00063788
		private void OnBeginElementAssignment(InputFieldInfo fieldInfo, ControllerMap map, ActionElementMap aem, string actionName)
		{
			if (fieldInfo == null || map == null)
			{
				return;
			}
			this.pendingInputMapping = new ControlMapper.InputMapping(actionName, fieldInfo, map, aem, fieldInfo.controllerType, fieldInfo.controllerId);
			switch (fieldInfo.controllerType)
			{
			case 0:
				this.ShowElementAssignmentPollingWindow();
				return;
			case 1:
				this.ShowElementAssignmentPollingWindow();
				return;
			case 2:
				if (this.preInputAssignmentTimeout == 0f)
				{
					this.ShowElementAssignmentPollingWindow();
					return;
				}
				this.ShowElementAssignmentPrePollingWindow();
				return;
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x00012F6F File Offset: 0x0001116F
		private void OnControllerAssignmentConfirmed(int windowId, Player player, int controllerId)
		{
			if (windowId < 0 || player == null || controllerId < 0)
			{
				return;
			}
			this.AssignController(player, controllerId);
			this.CloseWindow(windowId);
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x00065608 File Offset: 0x00063808
		private void OnMouseAssignmentConfirmed(int windowId, Player player)
		{
			if (windowId < 0 || player == null)
			{
				return;
			}
			IList<Player> players = ReInput.players.Players;
			for (int i = 0; i < players.Count; i++)
			{
				if (players[i] != player)
				{
					players[i].controllers.hasMouse = false;
				}
			}
			player.controllers.hasMouse = true;
			this.CloseWindow(windowId);
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x00065668 File Offset: 0x00063868
		private void OnElementAssignmentConflictReplaceConfirmed(int windowId, ControlMapper.InputMapping mapping, ElementAssignment assignment, bool skipOtherPlayers, bool allowSwap)
		{
			if (this.currentPlayer == null || mapping == null)
			{
				return;
			}
			ElementAssignmentConflictCheck elementAssignmentConflictCheck;
			if (!this.CreateConflictCheck(mapping, assignment, out elementAssignmentConflictCheck))
			{
				Debug.LogError("Rewired Control Mapper: Error creating conflict check!");
				this.CloseWindow(windowId);
				return;
			}
			ElementAssignmentConflictInfo elementAssignmentConflictInfo = default(ElementAssignmentConflictInfo);
			ActionElementMap actionElementMap = null;
			ActionElementMap actionElementMap2 = null;
			bool flag = false;
			if (allowSwap && mapping.aem != null && this.GetFirstElementAssignmentConflict(elementAssignmentConflictCheck, out elementAssignmentConflictInfo, skipOtherPlayers))
			{
				flag = true;
				actionElementMap2 = new ActionElementMap(mapping.aem);
				actionElementMap = new ActionElementMap(elementAssignmentConflictInfo.elementMap);
			}
			IList<Player> allPlayers = ReInput.players.AllPlayers;
			for (int i = 0; i < allPlayers.Count; i++)
			{
				Player player = allPlayers[i];
				if (!skipOtherPlayers || player == this.currentPlayer || player == ReInput.players.SystemPlayer)
				{
					player.controllers.conflictChecking.RemoveElementAssignmentConflicts(elementAssignmentConflictCheck);
				}
			}
			mapping.map.ReplaceOrCreateElementMap(assignment);
			if (allowSwap && flag)
			{
				int actionId = actionElementMap.actionId;
				Pole axisContribution = actionElementMap.axisContribution;
				bool flag2 = actionElementMap.invert;
				AxisRange axisRange = actionElementMap2.axisRange;
				ControllerElementType elementType = actionElementMap2.elementType;
				int elementIdentifierId = actionElementMap2.elementIdentifierId;
				KeyCode keyCode = actionElementMap2.keyCode;
				ModifierKeyFlags modifierKeyFlags = actionElementMap2.modifierKeyFlags;
				if (elementType == actionElementMap.elementType && elementType == null)
				{
					if (axisRange != actionElementMap.axisRange)
					{
						if (axisRange == null)
						{
							axisRange = 1;
						}
						else if (actionElementMap.axisRange == null)
						{
						}
					}
				}
				else if (elementType == null && (actionElementMap.elementType == 1 || (actionElementMap.elementType == null && actionElementMap.axisRange != null)) && axisRange == null)
				{
					axisRange = 1;
				}
				if (elementType != null || axisRange != null)
				{
					flag2 = false;
				}
				int num = 0;
				foreach (ActionElementMap actionElementMap3 in elementAssignmentConflictInfo.controllerMap.ElementMapsWithAction(actionId))
				{
					if (this.SwapIsSameInputRange(elementType, axisRange, axisContribution, actionElementMap3.elementType, actionElementMap3.axisRange, actionElementMap3.axisContribution))
					{
						num++;
					}
				}
				if (num < this.GetControllerInputFieldCount(mapping.controllerType))
				{
					elementAssignmentConflictInfo.controllerMap.ReplaceOrCreateElementMap(ElementAssignment.CompleteAssignment(mapping.controllerType, elementType, elementIdentifierId, axisRange, keyCode, modifierKeyFlags, actionId, axisContribution, flag2));
				}
			}
			this.CloseWindow(windowId);
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x00012F8C File Offset: 0x0001118C
		private void OnElementAssignmentAddConfirmed(int windowId, ControlMapper.InputMapping mapping, ElementAssignment assignment)
		{
			if (this.currentPlayer == null || mapping == null)
			{
				return;
			}
			mapping.map.ReplaceOrCreateElementMap(assignment);
			this.CloseWindow(windowId);
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x000658A4 File Offset: 0x00063AA4
		private void OnRestoreDefaultsConfirmed(int windowId)
		{
			if (this._restoreDefaultsDelegate == null)
			{
				IList<Player> players = ReInput.players.Players;
				for (int i = 0; i < players.Count; i++)
				{
					Player player = players[i];
					if (this._showControllers)
					{
						player.controllers.maps.LoadDefaultMaps(2);
					}
					if (this._showKeyboard)
					{
						player.controllers.maps.LoadDefaultMaps(0);
					}
					if (this._showMouse)
					{
						player.controllers.maps.LoadDefaultMaps(1);
					}
				}
			}
			this.CloseWindow(windowId);
			if (this._restoreDefaultsDelegate != null)
			{
				this._restoreDefaultsDelegate();
			}
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x00065944 File Offset: 0x00063B44
		private void OnAssignControllerWindowUpdate(int windowId)
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			Window window = this.windowManager.GetWindow(windowId);
			if (windowId < 0)
			{
				return;
			}
			this.InputPollingStarted();
			if (window.timer.finished)
			{
				this.InputPollingStopped();
				this.CloseWindow(windowId);
				return;
			}
			ControllerPollingInfo controllerPollingInfo = ReInput.controllers.polling.PollAllControllersOfTypeForFirstElementDown(2);
			if (!controllerPollingInfo.success)
			{
				window.SetContentText(Mathf.CeilToInt(window.timer.remaining).ToString(), 1);
				return;
			}
			this.InputPollingStopped();
			if (ReInput.controllers.IsControllerAssigned(2, controllerPollingInfo.controllerId) && !this.currentPlayer.controllers.ContainsController(2, controllerPollingInfo.controllerId))
			{
				this.ShowControllerAssignmentConflictWindow(controllerPollingInfo.controllerId);
				return;
			}
			this.OnControllerAssignmentConfirmed(windowId, this.currentPlayer, controllerPollingInfo.controllerId);
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x00065A20 File Offset: 0x00063C20
		private void OnElementAssignmentPrePollingWindowUpdate(int windowId)
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			Window window = this.windowManager.GetWindow(windowId);
			if (windowId < 0)
			{
				return;
			}
			if (this.pendingInputMapping == null)
			{
				return;
			}
			this.InputPollingStarted();
			if (!window.timer.finished)
			{
				window.SetContentText(Mathf.CeilToInt(window.timer.remaining).ToString(), 1);
				ControllerType controllerType = this.pendingInputMapping.controllerType;
				ControllerPollingInfo controllerPollingInfo;
				if (controllerType > 1)
				{
					if (controllerType != 2)
					{
						throw new NotImplementedException();
					}
					if (this.currentPlayer.controllers.joystickCount == 0)
					{
						return;
					}
					controllerPollingInfo = ReInput.controllers.polling.PollControllerForFirstButtonDown(this.pendingInputMapping.controllerType, this.currentJoystick.id);
				}
				else
				{
					controllerPollingInfo = ReInput.controllers.polling.PollControllerForFirstButtonDown(this.pendingInputMapping.controllerType, 0);
				}
				if (!controllerPollingInfo.success)
				{
					return;
				}
			}
			this.ShowElementAssignmentPollingWindow();
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x00065B0C File Offset: 0x00063D0C
		private void OnJoystickElementAssignmentPollingWindowUpdate(int windowId)
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			Window window = this.windowManager.GetWindow(windowId);
			if (windowId < 0)
			{
				return;
			}
			if (this.pendingInputMapping == null)
			{
				return;
			}
			this.InputPollingStarted();
			if (window.timer.finished)
			{
				this.InputPollingStopped();
				this.CloseWindow(windowId);
				return;
			}
			window.SetContentText(Mathf.CeilToInt(window.timer.remaining).ToString(), 1);
			if (this.currentPlayer.controllers.joystickCount == 0)
			{
				return;
			}
			ControllerPollingInfo controllerPollingInfo = ReInput.controllers.polling.PollControllerForFirstElementDown(2, this.currentJoystick.id);
			if (!controllerPollingInfo.success)
			{
				return;
			}
			if (!this.IsAllowedAssignment(this.pendingInputMapping, controllerPollingInfo))
			{
				return;
			}
			ElementAssignment elementAssignment = this.pendingInputMapping.ToElementAssignment(controllerPollingInfo);
			if (!this.HasElementAssignmentConflicts(this.currentPlayer, this.pendingInputMapping, elementAssignment, false))
			{
				this.pendingInputMapping.map.ReplaceOrCreateElementMap(elementAssignment);
				this.InputPollingStopped();
				this.CloseWindow(windowId);
				return;
			}
			this.InputPollingStopped();
			this.ShowElementAssignmentConflictWindow(elementAssignment, false);
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x00065C18 File Offset: 0x00063E18
		private void OnKeyboardElementAssignmentPollingWindowUpdate(int windowId)
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			Window window = this.windowManager.GetWindow(windowId);
			if (windowId < 0)
			{
				return;
			}
			if (this.pendingInputMapping == null)
			{
				return;
			}
			this.InputPollingStarted();
			if (window.timer.finished)
			{
				this.InputPollingStopped();
				this.CloseWindow(windowId);
				return;
			}
			ControllerPollingInfo controllerPollingInfo;
			bool flag;
			ModifierKeyFlags modifierKeyFlags;
			string text;
			this.PollKeyboardForAssignment(out controllerPollingInfo, out flag, out modifierKeyFlags, out text);
			if (flag)
			{
				window.timer.Start(this._inputAssignmentTimeout);
			}
			window.SetContentText(flag ? string.Empty : Mathf.CeilToInt(window.timer.remaining).ToString(), 2);
			window.SetContentText(text, 1);
			if (!controllerPollingInfo.success)
			{
				return;
			}
			if (!this.IsAllowedAssignment(this.pendingInputMapping, controllerPollingInfo))
			{
				return;
			}
			ElementAssignment elementAssignment = this.pendingInputMapping.ToElementAssignment(controllerPollingInfo, modifierKeyFlags);
			if (!this.HasElementAssignmentConflicts(this.currentPlayer, this.pendingInputMapping, elementAssignment, false))
			{
				this.pendingInputMapping.map.ReplaceOrCreateElementMap(elementAssignment);
				this.InputPollingStopped();
				this.CloseWindow(windowId);
				return;
			}
			this.InputPollingStopped();
			this.ShowElementAssignmentConflictWindow(elementAssignment, false);
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x00065D30 File Offset: 0x00063F30
		private void OnMouseElementAssignmentPollingWindowUpdate(int windowId)
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			Window window = this.windowManager.GetWindow(windowId);
			if (windowId < 0)
			{
				return;
			}
			if (this.pendingInputMapping == null)
			{
				return;
			}
			this.InputPollingStarted();
			if (window.timer.finished)
			{
				this.InputPollingStopped();
				this.CloseWindow(windowId);
				return;
			}
			window.SetContentText(Mathf.CeilToInt(window.timer.remaining).ToString(), 1);
			ControllerPollingInfo controllerPollingInfo;
			if (this._ignoreMouseXAxisAssignment || this._ignoreMouseYAxisAssignment)
			{
				controllerPollingInfo = default(ControllerPollingInfo);
				using (IEnumerator<ControllerPollingInfo> enumerator = ReInput.controllers.polling.PollControllerForAllElementsDown(1, 0).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ControllerPollingInfo controllerPollingInfo2 = enumerator.Current;
						if (controllerPollingInfo2.elementType != null || ((!this._ignoreMouseXAxisAssignment || controllerPollingInfo2.elementIndex != 0) && (!this._ignoreMouseYAxisAssignment || controllerPollingInfo2.elementIndex != 1)))
						{
							controllerPollingInfo = controllerPollingInfo2;
							break;
						}
					}
					goto IL_00F9;
				}
			}
			controllerPollingInfo = ReInput.controllers.polling.PollControllerForFirstElementDown(1, 0);
			IL_00F9:
			if (!controllerPollingInfo.success)
			{
				return;
			}
			if (!this.IsAllowedAssignment(this.pendingInputMapping, controllerPollingInfo))
			{
				return;
			}
			ElementAssignment elementAssignment = this.pendingInputMapping.ToElementAssignment(controllerPollingInfo);
			if (!this.HasElementAssignmentConflicts(this.currentPlayer, this.pendingInputMapping, elementAssignment, true))
			{
				this.pendingInputMapping.map.ReplaceOrCreateElementMap(elementAssignment);
				this.InputPollingStopped();
				this.CloseWindow(windowId);
				return;
			}
			this.InputPollingStopped();
			this.ShowElementAssignmentConflictWindow(elementAssignment, true);
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x00065EB4 File Offset: 0x000640B4
		private void OnCalibrateAxisStep1WindowUpdate(int windowId)
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			Window window = this.windowManager.GetWindow(windowId);
			if (windowId < 0)
			{
				return;
			}
			if (this.pendingAxisCalibration == null || !this.pendingAxisCalibration.isValid)
			{
				return;
			}
			this.InputPollingStarted();
			if (!window.timer.finished)
			{
				window.SetContentText(Mathf.CeilToInt(window.timer.remaining).ToString(), 1);
				if (this.currentPlayer.controllers.joystickCount == 0)
				{
					return;
				}
				if (!this.pendingAxisCalibration.joystick.PollForFirstButtonDown().success)
				{
					return;
				}
			}
			this.pendingAxisCalibration.RecordZero();
			this.CloseWindow(windowId);
			this.ShowCalibrateAxisStep2Window();
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x00065F6C File Offset: 0x0006416C
		private void OnCalibrateAxisStep2WindowUpdate(int windowId)
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			Window window = this.windowManager.GetWindow(windowId);
			if (windowId < 0)
			{
				return;
			}
			if (this.pendingAxisCalibration == null || !this.pendingAxisCalibration.isValid)
			{
				return;
			}
			if (!window.timer.finished)
			{
				window.SetContentText(Mathf.CeilToInt(window.timer.remaining).ToString(), 1);
				this.pendingAxisCalibration.RecordMinMax();
				if (this.currentPlayer.controllers.joystickCount == 0)
				{
					return;
				}
				if (!this.pendingAxisCalibration.joystick.PollForFirstButtonDown().success)
				{
					return;
				}
			}
			this.EndAxisCalibration();
			this.InputPollingStopped();
			this.CloseWindow(windowId);
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x00066024 File Offset: 0x00064224
		private void ShowAssignControllerWindow()
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			if (ReInput.controllers.joystickCount == 0)
			{
				return;
			}
			Window window = this.OpenWindow(true);
			if (window == null)
			{
				return;
			}
			window.SetUpdateCallback(new Action<int>(this.OnAssignControllerWindowUpdate));
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this._language.assignControllerWindowTitle);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), this._language.assignControllerWindowMessage);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.BottomCenter, UIAnchor.BottomHStretch, Vector2.zero, "");
			window.timer.Start(this._controllerAssignmentTimeout);
			this.windowManager.Focus(window);
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x00066104 File Offset: 0x00064304
		private void ShowControllerAssignmentConflictWindow(int controllerId)
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			if (ReInput.controllers.joystickCount == 0)
			{
				return;
			}
			Window window = this.OpenWindow(true);
			if (window == null)
			{
				return;
			}
			string text = string.Empty;
			IList<Player> players = ReInput.players.Players;
			for (int i = 0; i < players.Count; i++)
			{
				if (players[i] != this.currentPlayer && players[i].controllers.ContainsController(2, controllerId))
				{
					text = this._language.GetPlayerName(players[i].id);
					break;
				}
			}
			Joystick joystick = ReInput.controllers.GetJoystick(controllerId);
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this._language.controllerAssignmentConflictWindowTitle);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), this._language.GetControllerAssignmentConflictWindowMessage(this._language.GetControllerName(joystick), text, this._language.GetPlayerName(this.currentPlayer.id)));
			UnityAction unityAction = delegate
			{
				this.OnWindowCancel(window.id);
			};
			window.cancelCallback = unityAction;
			window.CreateButton(this.prefabs.fitButton, UIPivot.BottomLeft, UIAnchor.BottomLeft, Vector2.zero, this._language.yes, delegate
			{
				this.OnControllerAssignmentConfirmed(window.id, this.currentPlayer, controllerId);
			}, unityAction, true);
			window.CreateButton(this.prefabs.fitButton, UIPivot.BottomRight, UIAnchor.BottomRight, Vector2.zero, this._language.no, unityAction, unityAction, false);
			this.windowManager.Focus(window);
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x000662F8 File Offset: 0x000644F8
		private void ShowBeginElementAssignmentReplacementWindow(InputFieldInfo fieldInfo, InputAction action, ControllerMap map, ActionElementMap aem, string actionName)
		{
			if (this.inputGrid.GetGUIInputField(this.currentMapCategoryId, action.id, fieldInfo.axisRange, fieldInfo.controllerType, fieldInfo.intData) == null)
			{
				return;
			}
			Window window = this.OpenWindow(true);
			if (window == null)
			{
				return;
			}
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, actionName);
			GameObject display = this._buttonDisplaySettings.GetDisplay(aem, false);
			display.transform.SetParent(window.transform);
			display.transform.localPosition = 1.5f * display.GetComponent<ButtonDisplaySettings>().positionOffset;
			display.transform.localScale = 1.5f * Vector3.one;
			UnityAction unityAction = delegate
			{
				this.OnWindowCancel(window.id);
			};
			window.cancelCallback = unityAction;
			window.CreateButton(this.prefabs.fitButton, UIPivot.BottomLeft, UIAnchor.BottomLeft, Vector2.zero, this._language.replace, delegate
			{
				this.OnBeginElementAssignment(fieldInfo, map, aem, actionName);
			}, unityAction, true);
			window.CreateButton(this.prefabs.fitButton, UIPivot.BottomCenter, UIAnchor.BottomCenter, Vector2.zero, this._language.remove, delegate
			{
				this.OnRemoveElementAssignment(window.id, map, aem);
			}, unityAction, false);
			window.CreateButton(this.prefabs.fitButton, UIPivot.BottomRight, UIAnchor.BottomRight, Vector2.zero, this._language.cancel, unityAction, unityAction, false);
			this.windowManager.Focus(window);
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x00012FAE File Offset: 0x000111AE
		private void ShowCreateNewElementAssignmentWindow(InputFieldInfo fieldInfo, InputAction action, ControllerMap map, string actionName)
		{
			if (this.inputGrid.GetGUIInputField(this.currentMapCategoryId, action.id, fieldInfo.axisRange, fieldInfo.controllerType, fieldInfo.intData) == null)
			{
				return;
			}
			this.OnBeginElementAssignment(fieldInfo, map, null, actionName);
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x000664EC File Offset: 0x000646EC
		private void ShowElementAssignmentPrePollingWindow()
		{
			if (this.pendingInputMapping == null)
			{
				return;
			}
			Window window = this.OpenWindow(true);
			if (window == null)
			{
				return;
			}
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this.pendingInputMapping.actionName);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), this._language.elementAssignmentPrePollingWindowMessage);
			if (this.prefabs.centerStickGraphic != null)
			{
				window.AddContentImage(this.prefabs.centerStickGraphic, UIPivot.BottomCenter, UIAnchor.BottomCenter, new Vector2(0f, 40f));
			}
			window.AddContentText(this.prefabs.windowContentText, UIPivot.BottomCenter, UIAnchor.BottomHStretch, Vector2.zero, "");
			window.SetUpdateCallback(new Action<int>(this.OnElementAssignmentPrePollingWindowUpdate));
			window.timer.Start(this._preInputAssignmentTimeout);
			this.windowManager.Focus(window);
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x000665FC File Offset: 0x000647FC
		private void ShowElementAssignmentPollingWindow()
		{
			if (this.pendingInputMapping == null)
			{
				return;
			}
			switch (this.pendingInputMapping.controllerType)
			{
			case 0:
				this.ShowKeyboardElementAssignmentPollingWindow();
				return;
			case 1:
				if (this.currentPlayer.controllers.hasMouse)
				{
					this.ShowMouseElementAssignmentPollingWindow();
					return;
				}
				this.ShowMouseAssignmentConflictWindow();
				return;
			case 2:
				this.ShowJoystickElementAssignmentPollingWindow();
				return;
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x00066668 File Offset: 0x00064868
		private void ShowJoystickElementAssignmentPollingWindow()
		{
			if (this.pendingInputMapping == null)
			{
				return;
			}
			Window window = this.OpenWindow(true);
			if (window == null)
			{
				return;
			}
			string text = ((this.pendingInputMapping.axisRange == null && this._showFullAxisInputFields && !this._showSplitAxisInputFields) ? this._language.GetJoystickElementAssignmentPollingWindowMessage_FullAxisFieldOnly(this.pendingInputMapping.actionName) : this._language.GetJoystickElementAssignmentPollingWindowMessage(this.pendingInputMapping.actionName));
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this.pendingInputMapping.actionName);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), text);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.BottomCenter, UIAnchor.BottomHStretch, Vector2.zero, "");
			window.SetUpdateCallback(new Action<int>(this.OnJoystickElementAssignmentPollingWindowUpdate));
			window.timer.Start(this._inputAssignmentTimeout);
			this.windowManager.Focus(window);
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x0006677C File Offset: 0x0006497C
		private void ShowKeyboardElementAssignmentPollingWindow()
		{
			if (this.pendingInputMapping == null)
			{
				return;
			}
			Window window = this.OpenWindow(true);
			if (window == null)
			{
				return;
			}
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this.pendingInputMapping.actionName);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), this._language.GetKeyboardElementAssignmentPollingWindowMessage(this.pendingInputMapping.actionName));
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -(window.GetContentTextHeight(0) + 50f)), "");
			window.AddContentText(this.prefabs.windowContentText, UIPivot.BottomCenter, UIAnchor.BottomHStretch, Vector2.zero, "");
			window.SetUpdateCallback(new Action<int>(this.OnKeyboardElementAssignmentPollingWindowUpdate));
			window.timer.Start(this._inputAssignmentTimeout);
			this.windowManager.Focus(window);
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x00066894 File Offset: 0x00064A94
		private void ShowMouseElementAssignmentPollingWindow()
		{
			if (this.pendingInputMapping == null)
			{
				return;
			}
			Window window = this.OpenWindow(true);
			if (window == null)
			{
				return;
			}
			string text = ((this.pendingInputMapping.axisRange == null && this._showFullAxisInputFields && !this._showSplitAxisInputFields) ? this._language.GetMouseElementAssignmentPollingWindowMessage_FullAxisFieldOnly(this.pendingInputMapping.actionName) : this._language.GetMouseElementAssignmentPollingWindowMessage(this.pendingInputMapping.actionName));
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this.pendingInputMapping.actionName);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), text);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.BottomCenter, UIAnchor.BottomHStretch, Vector2.zero, "");
			window.SetUpdateCallback(new Action<int>(this.OnMouseElementAssignmentPollingWindowUpdate));
			window.timer.Start(this._inputAssignmentTimeout);
			this.windowManager.Focus(window);
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x000669A8 File Offset: 0x00064BA8
		private void ShowElementAssignmentConflictWindow(ElementAssignment assignment, bool skipOtherPlayers)
		{
			if (this.pendingInputMapping == null)
			{
				return;
			}
			bool flag = this.IsBlockingAssignmentConflict(this.pendingInputMapping, assignment, skipOtherPlayers);
			string text = (flag ? this._language.GetElementAlreadyInUseBlocked(this.pendingInputMapping.elementName) : this._language.GetElementAlreadyInUseCanReplace(this.pendingInputMapping.elementName, this._allowElementAssignmentConflicts));
			Window window = this.OpenWindow(true);
			if (window == null)
			{
				return;
			}
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this._language.elementAssignmentConflictWindowMessage);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), text);
			UnityAction unityAction = delegate
			{
				this.OnWindowCancel(window.id);
			};
			window.cancelCallback = unityAction;
			if (flag)
			{
				window.CreateButton(this.prefabs.fitButton, UIPivot.BottomCenter, UIAnchor.BottomCenter, Vector2.zero, this._language.okay, unityAction, unityAction, true);
			}
			else
			{
				window.CreateButton(this.prefabs.fitButton, UIPivot.BottomLeft, UIAnchor.BottomLeft, Vector2.zero, this._language.replace, delegate
				{
					this.OnElementAssignmentConflictReplaceConfirmed(window.id, this.pendingInputMapping, assignment, skipOtherPlayers, false);
				}, unityAction, true);
				if (this._allowElementAssignmentConflicts)
				{
					window.CreateButton(this.prefabs.fitButton, UIPivot.BottomCenter, UIAnchor.BottomCenter, Vector2.zero, this._language.add, delegate
					{
						this.OnElementAssignmentAddConfirmed(window.id, this.pendingInputMapping, assignment);
					}, unityAction, false);
				}
				else if (this.ShowSwapButton(window.id, this.pendingInputMapping, assignment, skipOtherPlayers))
				{
					window.CreateButton(this.prefabs.fitButton, UIPivot.BottomCenter, UIAnchor.BottomCenter, Vector2.zero, this._language.swap, delegate
					{
						this.OnElementAssignmentConflictReplaceConfirmed(window.id, this.pendingInputMapping, assignment, skipOtherPlayers, true);
					}, unityAction, false);
				}
				window.CreateButton(this.prefabs.fitButton, UIPivot.BottomRight, UIAnchor.BottomRight, Vector2.zero, this._language.cancel, unityAction, unityAction, false);
			}
			this.windowManager.Focus(window);
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x00066C20 File Offset: 0x00064E20
		private void ShowMouseAssignmentConflictWindow()
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			Window window = this.OpenWindow(true);
			if (window == null)
			{
				return;
			}
			string text = string.Empty;
			IList<Player> players = ReInput.players.Players;
			for (int i = 0; i < players.Count; i++)
			{
				if (players[i] != this.currentPlayer && players[i].controllers.hasMouse)
				{
					text = this._language.GetPlayerName(players[i].id);
					break;
				}
			}
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this._language.mouseAssignmentConflictWindowTitle);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), this._language.GetMouseAssignmentConflictWindowMessage(text, this._language.GetPlayerName(this.currentPlayer.id)));
			UnityAction unityAction = delegate
			{
				this.OnWindowCancel(window.id);
			};
			window.cancelCallback = unityAction;
			window.CreateButton(this.prefabs.fitButton, UIPivot.BottomLeft, UIAnchor.BottomLeft, Vector2.zero, this._language.yes, delegate
			{
				this.OnMouseAssignmentConfirmed(window.id, this.currentPlayer);
			}, unityAction, true);
			window.CreateButton(this.prefabs.fitButton, UIPivot.BottomRight, UIAnchor.BottomRight, Vector2.zero, this._language.no, unityAction, unityAction, false);
			this.windowManager.Focus(window);
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x00066DD8 File Offset: 0x00064FD8
		private void ShowCalibrateControllerWindow()
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			if (this.currentPlayer.controllers.joystickCount == 0)
			{
				return;
			}
			CalibrationWindow calibrationWindow = this.OpenWindow(this.prefabs.calibrationWindow, "CalibrationWindow", true) as CalibrationWindow;
			if (calibrationWindow == null)
			{
				return;
			}
			Joystick currentJoystick = this.currentJoystick;
			calibrationWindow.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this._language.calibrateControllerWindowTitle);
			calibrationWindow.SetJoystick(this.currentPlayer.id, currentJoystick);
			calibrationWindow.SetButtonCallback(CalibrationWindow.ButtonIdentifier.Done, new Action<int>(this.CloseWindow));
			calibrationWindow.SetButtonCallback(CalibrationWindow.ButtonIdentifier.Calibrate, new Action<int>(this.StartAxisCalibration));
			calibrationWindow.SetButtonCallback(CalibrationWindow.ButtonIdentifier.Cancel, new Action<int>(this.CloseWindow));
			this.windowManager.Focus(calibrationWindow);
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x00066EA8 File Offset: 0x000650A8
		private void ShowCalibrateAxisStep1Window()
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			Window window = this.OpenWindow(false);
			if (window == null)
			{
				return;
			}
			if (this.pendingAxisCalibration == null)
			{
				return;
			}
			Joystick joystick = this.pendingAxisCalibration.joystick;
			if (joystick.axisCount == 0)
			{
				return;
			}
			int axisIndex = this.pendingAxisCalibration.axisIndex;
			if (axisIndex < 0 || axisIndex >= joystick.axisCount)
			{
				return;
			}
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this._language.calibrateAxisStep1WindowTitle);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), this._language.GetCalibrateAxisStep1WindowMessage(this._language.GetElementIdentifierName(joystick, joystick.AxisElementIdentifiers[axisIndex].id, 0)));
			if (this.prefabs.centerStickGraphic != null)
			{
				window.AddContentImage(this.prefabs.centerStickGraphic, UIPivot.BottomCenter, UIAnchor.BottomCenter, new Vector2(0f, 40f));
			}
			window.AddContentText(this.prefabs.windowContentText, UIPivot.BottomCenter, UIAnchor.BottomHStretch, Vector2.zero, "");
			window.SetUpdateCallback(new Action<int>(this.OnCalibrateAxisStep1WindowUpdate));
			window.timer.Start(this._axisCalibrationTimeout);
			this.windowManager.Focus(window);
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x00067010 File Offset: 0x00065210
		private void ShowCalibrateAxisStep2Window()
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			Window window = this.OpenWindow(false);
			if (window == null)
			{
				return;
			}
			if (this.pendingAxisCalibration == null)
			{
				return;
			}
			Joystick joystick = this.pendingAxisCalibration.joystick;
			if (joystick.axisCount == 0)
			{
				return;
			}
			int axisIndex = this.pendingAxisCalibration.axisIndex;
			if (axisIndex < 0 || axisIndex >= joystick.axisCount)
			{
				return;
			}
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this._language.calibrateAxisStep2WindowTitle);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), this._language.GetCalibrateAxisStep2WindowMessage(this._language.GetElementIdentifierName(joystick, joystick.AxisElementIdentifiers[axisIndex].id, 0)));
			if (this.prefabs.moveStickGraphic != null)
			{
				window.AddContentImage(this.prefabs.moveStickGraphic, UIPivot.BottomCenter, UIAnchor.BottomCenter, new Vector2(0f, 40f));
			}
			window.AddContentText(this.prefabs.windowContentText, UIPivot.BottomCenter, UIAnchor.BottomHStretch, Vector2.zero, "");
			window.SetUpdateCallback(new Action<int>(this.OnCalibrateAxisStep2WindowUpdate));
			window.timer.Start(this._axisCalibrationTimeout);
			this.windowManager.Focus(window);
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x00067178 File Offset: 0x00065378
		private void ShowEditInputBehaviorsWindow()
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			if (this._inputBehaviorSettings == null)
			{
				return;
			}
			InputBehaviorWindow inputBehaviorWindow = this.OpenWindow(this.prefabs.inputBehaviorsWindow, "EditInputBehaviorsWindow", true) as InputBehaviorWindow;
			if (inputBehaviorWindow == null)
			{
				return;
			}
			inputBehaviorWindow.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, this._language.inputBehaviorSettingsWindowTitle);
			inputBehaviorWindow.SetData(this.currentPlayer.id, this._inputBehaviorSettings);
			inputBehaviorWindow.SetButtonCallback(InputBehaviorWindow.ButtonIdentifier.Done, new Action<int>(this.CloseWindow));
			inputBehaviorWindow.SetButtonCallback(InputBehaviorWindow.ButtonIdentifier.Cancel, new Action<int>(this.CloseWindow));
			this.windowManager.Focus(inputBehaviorWindow);
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x00067228 File Offset: 0x00065428
		private void ShowRestoreDefaultsWindow()
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			this.OpenModal(this._language.restoreDefaultsWindowTitle, this._language.restoreDefaultsWindowMessage, this._language.yes, new Action<int>(this.OnRestoreDefaultsConfirmed), this._language.no, new Action<int>(this.OnWindowCancel), true);
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x0006728C File Offset: 0x0006548C
		private void CreateInputGrid()
		{
			this.InitializeInputGrid();
			this.CreateHeaderLabels();
			this.CreateActionLabelColumn();
			this.CreateKeyboardInputFieldColumn();
			this.CreateMouseInputFieldColumn();
			this.CreateControllerInputFieldColumn();
			this.CreateInputActionLabels();
			this.CreateInputFields();
			this.inputGrid.HideAll();
			this.ResetInputGridScrollBar();
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x000672DC File Offset: 0x000654DC
		private void InitializeInputGrid()
		{
			if (this.inputGrid == null)
			{
				this.inputGrid = new ControlMapper.InputGrid();
			}
			else
			{
				this.inputGrid.ClearAll();
			}
			for (int i = 0; i < this._mappingSets.Length; i++)
			{
				ControlMapper.MappingSet mappingSet = this._mappingSets[i];
				if (mappingSet != null && mappingSet.isValid)
				{
					InputMapCategory mapCategory = ReInput.mapping.GetMapCategory(mappingSet.mapCategoryId);
					if (mapCategory != null && mapCategory.userAssignable)
					{
						this.inputGrid.AddMapCategory(mappingSet.mapCategoryId);
						if (mappingSet.actionListMode == ControlMapper.MappingSet.ActionListMode.ActionCategory)
						{
							IList<int> actionCategoryIds = mappingSet.actionCategoryIds;
							for (int j = 0; j < actionCategoryIds.Count; j++)
							{
								int num = actionCategoryIds[j];
								InputCategory actionCategory = ReInput.mapping.GetActionCategory(num);
								if (actionCategory != null && actionCategory.userAssignable)
								{
									this.inputGrid.AddActionCategory(mappingSet.mapCategoryId, num);
									foreach (InputAction inputAction in ReInput.mapping.UserAssignableActionsInCategory(num))
									{
										if (inputAction.type == null)
										{
											if (this._showFullAxisInputFields)
											{
												this.inputGrid.AddAction(mappingSet.mapCategoryId, inputAction, 0);
											}
											if (this._showSplitAxisInputFields)
											{
												this.inputGrid.AddAction(mappingSet.mapCategoryId, inputAction, 1);
												this.inputGrid.AddAction(mappingSet.mapCategoryId, inputAction, 2);
											}
										}
										else if (inputAction.type == 1)
										{
											this.inputGrid.AddAction(mappingSet.mapCategoryId, inputAction, 1);
										}
									}
								}
							}
						}
						else
						{
							IList<int> actionIds = mappingSet.actionIds;
							for (int k = 0; k < actionIds.Count; k++)
							{
								InputAction action = ReInput.mapping.GetAction(actionIds[k]);
								if (action != null)
								{
									if (action.type == null)
									{
										if (this._showFullAxisInputFields)
										{
											this.inputGrid.AddAction(mappingSet.mapCategoryId, action, 0);
										}
										if (this._showSplitAxisInputFields)
										{
											this.inputGrid.AddAction(mappingSet.mapCategoryId, action, 1);
											this.inputGrid.AddAction(mappingSet.mapCategoryId, action, 2);
										}
									}
									else if (action.type == 1)
									{
										this.inputGrid.AddAction(mappingSet.mapCategoryId, action, 1);
									}
								}
							}
						}
					}
				}
			}
			this.references.inputGridInnerGroup.GetComponent<HorizontalLayoutGroup>().spacing = (float)this._inputColumnSpacing;
			this.references.inputGridLayoutElement.flexibleWidth = 0f;
			this.references.inputGridLayoutElement.preferredWidth = (float)this.inputGridWidth;
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x00067598 File Offset: 0x00065798
		private void RefreshInputGridStructure()
		{
			if (this.currentMappingSet == null)
			{
				return;
			}
			this.inputGrid.HideAll();
			this.inputGrid.Show(this.currentMappingSet.mapCategoryId);
			this.references.inputGridInnerGroup.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(1, this.inputGrid.GetColumnHeight(this.currentMappingSet.mapCategoryId));
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x000675FC File Offset: 0x000657FC
		private void CreateHeaderLabels()
		{
			this.references.inputGridHeader1 = this.CreateNewColumnGroup("ActionsHeader", this.references.inputGridHeadersGroup, this._actionLabelWidth).transform;
			this.CreateLabel(this.prefabs.inputGridHeaderLabel, this._language.actionColumnLabel, this.references.inputGridHeader1, Vector2.zero);
			if (this._showKeyboard)
			{
				this.references.inputGridHeader2 = this.CreateNewColumnGroup("KeybordHeader", this.references.inputGridHeadersGroup, this._keyboardColMaxWidth).transform;
				this.CreateLabel(this.prefabs.inputGridHeaderLabel, this._language.keyboardColumnLabel, this.references.inputGridHeader2, Vector2.zero).SetTextAlignment(4);
			}
			if (this._showMouse)
			{
				this.references.inputGridHeader3 = this.CreateNewColumnGroup("MouseHeader", this.references.inputGridHeadersGroup, this._mouseColMaxWidth).transform;
				this.CreateLabel(this.prefabs.inputGridHeaderLabel, this._language.mouseColumnLabel, this.references.inputGridHeader3, Vector2.zero).SetTextAlignment(4);
			}
			if (this._showControllers)
			{
				this.references.inputGridHeader4 = this.CreateNewColumnGroup("ControllerHeader", this.references.inputGridHeadersGroup, this._controllerColMaxWidth).transform;
				this.CreateLabel(this.prefabs.inputGridHeaderLabel, this._language.controllerColumnLabel, this.references.inputGridHeader4, Vector2.zero).SetTextAlignment(4);
			}
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x00067794 File Offset: 0x00065994
		private void CreateActionLabelColumn()
		{
			Transform transform = this.CreateNewColumnGroup("ActionLabelColumn", this.references.inputGridInnerGroup, this._actionLabelWidth).transform;
			this.references.inputGridActionColumn = transform;
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x00012FE7 File Offset: 0x000111E7
		private void CreateKeyboardInputFieldColumn()
		{
			if (!this._showKeyboard)
			{
				return;
			}
			this.CreateInputFieldColumn("KeyboardColumn", 0, this._keyboardColMaxWidth, this._keyboardInputFieldCount, true);
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0001300B File Offset: 0x0001120B
		private void CreateMouseInputFieldColumn()
		{
			if (!this._showMouse)
			{
				return;
			}
			this.CreateInputFieldColumn("MouseColumn", 1, this._mouseColMaxWidth, this._mouseInputFieldCount, false);
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x0001302F File Offset: 0x0001122F
		private void CreateControllerInputFieldColumn()
		{
			if (!this._showControllers)
			{
				return;
			}
			this.CreateInputFieldColumn("ControllerColumn", 2, this._controllerColMaxWidth, this._controllerInputFieldCount, false);
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x000677D0 File Offset: 0x000659D0
		private void CreateInputFieldColumn(string name, ControllerType controllerType, int maxWidth, int cols, bool disableFullAxis)
		{
			Transform transform = this.CreateNewColumnGroup(name, this.references.inputGridInnerGroup, maxWidth).transform;
			switch (controllerType)
			{
			case 0:
				this.references.inputGridKeyboardColumn = transform;
				return;
			case 1:
				this.references.inputGridMouseColumn = transform;
				return;
			case 2:
				this.references.inputGridControllerColumn = transform;
				return;
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x000050C3 File Offset: 0x000032C3
		private bool ShouldDisplayElement(int elementIndex)
		{
			return true;
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x00067838 File Offset: 0x00065A38
		private void CreateInputActionLabels()
		{
			Transform inputGridActionColumn = this.references.inputGridActionColumn;
			for (int i = 0; i < this._mappingSets.Length; i++)
			{
				ControlMapper.MappingSet mappingSet = this._mappingSets[i];
				if (mappingSet != null && mappingSet.isValid)
				{
					int num = 0;
					if (mappingSet.actionListMode == ControlMapper.MappingSet.ActionListMode.ActionCategory)
					{
						int num2 = 0;
						IList<int> actionCategoryIds = mappingSet.actionCategoryIds;
						for (int j = 0; j < actionCategoryIds.Count; j++)
						{
							InputCategory actionCategory = ReInput.mapping.GetActionCategory(actionCategoryIds[j]);
							if (actionCategory != null && actionCategory.userAssignable && this.CountIEnumerable<InputAction>(ReInput.mapping.UserAssignableActionsInCategory(actionCategory.id)) != 0)
							{
								if (this._showActionCategoryLabels)
								{
									if (num2 > 0)
									{
										num -= this._inputRowCategorySpacing;
									}
									ControlMapper.GUILabel guilabel = this.CreateLabel(this._language.GetActionCategoryName(actionCategory.id), inputGridActionColumn, new Vector2(0f, (float)num));
									guilabel.SetFontStyle(1);
									guilabel.rectTransform.SetSizeWithCurrentAnchors(1, (float)this._inputRowHeight);
									this.inputGrid.AddActionCategoryLabel(mappingSet.mapCategoryId, actionCategory.id, guilabel);
									num -= this._inputRowHeight;
								}
								foreach (InputAction inputAction in ReInput.mapping.UserAssignableActionsInCategory(actionCategory.id, true))
								{
									if (inputAction.type == null)
									{
										if (this._showFullAxisInputFields)
										{
											ControlMapper.GUILabel guilabel2 = this.CreateLabel(this._language.GetActionName(inputAction.id, 0), inputGridActionColumn, new Vector2(0f, (float)num));
											guilabel2.rectTransform.SetSizeWithCurrentAnchors(1, (float)this._inputRowHeight);
											this.inputGrid.AddActionLabel(mappingSet.mapCategoryId, inputAction.id, 0, guilabel2);
											num -= this._inputRowHeight;
										}
										if (this._showSplitAxisInputFields)
										{
											string actionName = this._language.GetActionName(inputAction.id, 1);
											ControlMapper.GUILabel guilabel2 = this.CreateLabel(actionName, inputGridActionColumn, new Vector2(0f, (float)num));
											guilabel2.rectTransform.SetSizeWithCurrentAnchors(1, (float)this._inputRowHeight);
											this.inputGrid.AddActionLabel(mappingSet.mapCategoryId, inputAction.id, 1, guilabel2);
											num -= this._inputRowHeight;
											string actionName2 = this._language.GetActionName(inputAction.id, 2);
											guilabel2 = this.CreateLabel(actionName2, inputGridActionColumn, new Vector2(0f, (float)num));
											guilabel2.rectTransform.SetSizeWithCurrentAnchors(1, (float)this._inputRowHeight);
											this.inputGrid.AddActionLabel(mappingSet.mapCategoryId, inputAction.id, 2, guilabel2);
											num -= this._inputRowHeight;
										}
									}
									else if (inputAction.type == 1)
									{
										ControlMapper.GUILabel guilabel2 = this.CreateLabel(this._language.GetActionName(inputAction.id), inputGridActionColumn, new Vector2(0f, (float)num));
										guilabel2.rectTransform.SetSizeWithCurrentAnchors(1, (float)this._inputRowHeight);
										this.inputGrid.AddActionLabel(mappingSet.mapCategoryId, inputAction.id, 1, guilabel2);
										num -= this._inputRowHeight;
									}
								}
								num2++;
							}
						}
					}
					else
					{
						IList<int> actionIds = mappingSet.actionIds;
						for (int k = 0; k < actionIds.Count; k++)
						{
							if (this.ShouldDisplayElement(k))
							{
								InputAction action = ReInput.mapping.GetAction(actionIds[k]);
								if (action != null && action.userAssignable)
								{
									InputCategory actionCategory2 = ReInput.mapping.GetActionCategory(action.categoryId);
									if (actionCategory2 != null && actionCategory2.userAssignable)
									{
										if (action.type == null)
										{
											if (this._showFullAxisInputFields)
											{
												ControlMapper.GUILabel guilabel3 = this.CreateLabel(this._language.GetActionName(action.id, 0), inputGridActionColumn, new Vector2(0f, (float)num));
												guilabel3.rectTransform.SetSizeWithCurrentAnchors(1, (float)this._inputRowHeight);
												this.inputGrid.AddActionLabel(mappingSet.mapCategoryId, action.id, 0, guilabel3);
												num -= this._inputRowHeight;
											}
											if (this._showSplitAxisInputFields)
											{
												ControlMapper.GUILabel guilabel3 = this.CreateLabel(this._language.GetActionName(action.id, 1), inputGridActionColumn, new Vector2(0f, (float)num));
												guilabel3.rectTransform.SetSizeWithCurrentAnchors(1, (float)this._inputRowHeight);
												this.inputGrid.AddActionLabel(mappingSet.mapCategoryId, action.id, 1, guilabel3);
												num -= this._inputRowHeight;
												guilabel3 = this.CreateLabel(this._language.GetActionName(action.id, 2), inputGridActionColumn, new Vector2(0f, (float)num));
												guilabel3.rectTransform.SetSizeWithCurrentAnchors(1, (float)this._inputRowHeight);
												this.inputGrid.AddActionLabel(mappingSet.mapCategoryId, action.id, 2, guilabel3);
												num -= this._inputRowHeight;
											}
										}
										else if (action.type == 1)
										{
											ControlMapper.GUILabel guilabel3 = this.CreateLabel(this._language.GetActionName(action.id), inputGridActionColumn, new Vector2(0f, (float)num));
											guilabel3.rectTransform.SetSizeWithCurrentAnchors(1, (float)this._inputRowHeight);
											this.inputGrid.AddActionLabel(mappingSet.mapCategoryId, action.id, 1, guilabel3);
											num -= this._inputRowHeight;
										}
									}
								}
							}
						}
					}
					this.inputGrid.SetColumnHeight(mappingSet.mapCategoryId, (float)(-(float)num));
				}
			}
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x00067DD0 File Offset: 0x00065FD0
		private void CreateInputFields()
		{
			if (this._showControllers)
			{
				this.CreateInputFields(this.references.inputGridControllerColumn, 2, this._controllerColMaxWidth, this._controllerInputFieldCount, false);
			}
			if (this._showKeyboard)
			{
				this.CreateInputFields(this.references.inputGridKeyboardColumn, 0, this._keyboardColMaxWidth, this._keyboardInputFieldCount, true);
			}
			if (this._showMouse)
			{
				this.CreateInputFields(this.references.inputGridMouseColumn, 1, this._mouseColMaxWidth, this._mouseInputFieldCount, false);
			}
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x00067E54 File Offset: 0x00066054
		private void CreateInputFields(Transform columnXform, ControllerType controllerType, int maxWidth, int cols, bool disableFullAxis)
		{
			for (int i = 0; i < this._mappingSets.Length; i++)
			{
				ControlMapper.MappingSet mappingSet = this._mappingSets[i];
				if (mappingSet != null && mappingSet.isValid)
				{
					int num = maxWidth / cols;
					int num2 = 0;
					int num3 = 0;
					if (mappingSet.actionListMode == ControlMapper.MappingSet.ActionListMode.ActionCategory)
					{
						IList<int> actionCategoryIds = mappingSet.actionCategoryIds;
						for (int j = 0; j < actionCategoryIds.Count; j++)
						{
							if (this.ShouldDisplayElement(j))
							{
								InputCategory actionCategory = ReInput.mapping.GetActionCategory(actionCategoryIds[j]);
								if (actionCategory != null && actionCategory.userAssignable && this.CountIEnumerable<InputAction>(ReInput.mapping.UserAssignableActionsInCategory(actionCategory.id)) != 0)
								{
									if (this._showActionCategoryLabels)
									{
										num2 -= ((num3 > 0) ? (this._inputRowHeight + this._inputRowCategorySpacing) : this._inputRowHeight);
									}
									foreach (InputAction inputAction in ReInput.mapping.UserAssignableActionsInCategory(actionCategory.id, true))
									{
										if (inputAction.type == null)
										{
											if (this._showFullAxisInputFields)
											{
												this.CreateInputFieldSet(columnXform, mappingSet.mapCategoryId, inputAction, 0, controllerType, cols, num, ref num2, disableFullAxis);
											}
											if (this._showSplitAxisInputFields)
											{
												this.CreateInputFieldSet(columnXform, mappingSet.mapCategoryId, inputAction, 1, controllerType, cols, num, ref num2, false);
												this.CreateInputFieldSet(columnXform, mappingSet.mapCategoryId, inputAction, 2, controllerType, cols, num, ref num2, false);
											}
										}
										else if (inputAction.type == 1)
										{
											this.CreateInputFieldSet(columnXform, mappingSet.mapCategoryId, inputAction, 1, controllerType, cols, num, ref num2, false);
										}
										num3++;
									}
								}
							}
						}
					}
					else
					{
						IList<int> actionIds = mappingSet.actionIds;
						for (int k = 0; k < actionIds.Count; k++)
						{
							InputAction action = ReInput.mapping.GetAction(actionIds[k]);
							if (action != null && action.userAssignable)
							{
								InputCategory actionCategory2 = ReInput.mapping.GetActionCategory(action.categoryId);
								if (actionCategory2 != null && actionCategory2.userAssignable)
								{
									if (action.type == null)
									{
										if (this._showFullAxisInputFields)
										{
											this.CreateInputFieldSet(columnXform, mappingSet.mapCategoryId, action, 0, controllerType, cols, num, ref num2, disableFullAxis);
										}
										if (this._showSplitAxisInputFields)
										{
											this.CreateInputFieldSet(columnXform, mappingSet.mapCategoryId, action, 1, controllerType, cols, num, ref num2, false);
											this.CreateInputFieldSet(columnXform, mappingSet.mapCategoryId, action, 2, controllerType, cols, num, ref num2, false);
										}
									}
									else if (action.type == 1)
									{
										this.CreateInputFieldSet(columnXform, mappingSet.mapCategoryId, action, 1, controllerType, cols, num, ref num2, false);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x00068118 File Offset: 0x00066318
		private void CreateInputFieldSet(Transform parent, int mapCategoryId, InputAction action, AxisRange axisRange, ControllerType controllerType, int cols, int fieldWidth, ref int yPos, bool disableFullAxis)
		{
			GameObject gameObject = this.CreateNewGUIObject("FieldLayoutGroup", parent, new Vector2(0f, (float)yPos));
			HorizontalLayoutGroup horizontalLayoutGroup = gameObject.AddComponent<HorizontalLayoutGroup>();
			horizontalLayoutGroup.padding = this._inputRowPadding;
			horizontalLayoutGroup.spacing = (float)this._inputRowFieldSpacing;
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.anchorMin = new Vector2(0f, 1f);
			component.anchorMax = new Vector2(1f, 1f);
			component.pivot = new Vector2(0f, 1f);
			component.sizeDelta = Vector2.zero;
			component.SetSizeWithCurrentAnchors(1, (float)this._inputRowHeight);
			this.inputGrid.AddInputFieldSet(mapCategoryId, action, axisRange, controllerType, gameObject);
			for (int i = 0; i < cols; i++)
			{
				int num = ((axisRange == null) ? this._invertToggleWidth : 0);
				ControlMapper.GUIInputField guiinputField = this.CreateInputField(horizontalLayoutGroup.transform, Vector2.zero, "", action.id, axisRange, controllerType, i);
				guiinputField.SetFirstChildObjectWidth(ControlMapper.LayoutElementSizeType.PreferredSize, fieldWidth - num);
				this.inputGrid.AddInputField(mapCategoryId, action, axisRange, controllerType, i, guiinputField);
				if (axisRange == null)
				{
					if (!disableFullAxis)
					{
						ControlMapper.GUIToggle guitoggle = this.CreateToggle(this.prefabs.inputGridFieldInvertToggle, horizontalLayoutGroup.transform, Vector2.zero, "", action.id, axisRange, controllerType, i);
						guitoggle.SetFirstChildObjectWidth(ControlMapper.LayoutElementSizeType.MinSize, num);
						guiinputField.AddToggle(guitoggle);
					}
					else
					{
						guiinputField.SetInteractible(false, false, true);
					}
				}
			}
			yPos -= this._inputRowHeight;
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x00068294 File Offset: 0x00066494
		private void PopulateInputFields()
		{
			this.inputGrid.InitializeFields(this.currentMapCategoryId);
			if (this.currentPlayer == null)
			{
				return;
			}
			this.inputGrid.SetFieldsActive(this.currentMapCategoryId, true);
			foreach (ControlMapper.InputActionSet inputActionSet in this.inputGrid.GetActionSets(this.currentMapCategoryId))
			{
				if (this._showKeyboard)
				{
					ControllerType controllerType = 0;
					int num = 0;
					int num2 = this._keyboardMapDefaultLayout;
					int num3 = this._keyboardInputFieldCount;
					ControllerMap controllerMapOrCreateNew = this.GetControllerMapOrCreateNew(controllerType, num, num2);
					this.PopulateInputFieldGroup(inputActionSet, controllerMapOrCreateNew, controllerType, num, num3);
				}
				if (this._showMouse)
				{
					ControllerType controllerType = 1;
					int num = 0;
					int num2 = this._mouseMapDefaultLayout;
					int num3 = this._mouseInputFieldCount;
					ControllerMap controllerMapOrCreateNew2 = this.GetControllerMapOrCreateNew(controllerType, num, num2);
					if (this.currentPlayer.controllers.hasMouse)
					{
						this.PopulateInputFieldGroup(inputActionSet, controllerMapOrCreateNew2, controllerType, num, num3);
					}
				}
				if (this.isJoystickSelected && this.currentPlayer.controllers.joystickCount > 0)
				{
					ControllerType controllerType = 2;
					int num = this.currentJoystick.id;
					int num2 = this._joystickMapDefaultLayout;
					int num3 = this._controllerInputFieldCount;
					ControllerMap controllerMapOrCreateNew3 = this.GetControllerMapOrCreateNew(controllerType, num, num2);
					this.PopulateInputFieldGroup(inputActionSet, controllerMapOrCreateNew3, controllerType, num, num3);
				}
				else
				{
					this.DisableInputFieldGroup(inputActionSet, 2, this._controllerInputFieldCount);
				}
			}
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x000683F8 File Offset: 0x000665F8
		private void PopulateInputFieldGroup(ControlMapper.InputActionSet actionSet, ControllerMap controllerMap, ControllerType controllerType, int controllerId, int maxFields)
		{
			if (controllerMap == null)
			{
				return;
			}
			int num = 0;
			this.inputGrid.SetFixedFieldData(this.currentMapCategoryId, actionSet.actionId, actionSet.axisRange, controllerType, controllerId);
			foreach (ActionElementMap actionElementMap in controllerMap.ElementMapsWithAction(actionSet.actionId))
			{
				if (actionElementMap.elementType == 1)
				{
					if (actionSet.axisRange == null)
					{
						continue;
					}
					if (actionSet.axisRange == 1)
					{
						if (actionElementMap.axisContribution == 1)
						{
							continue;
						}
					}
					else if (actionSet.axisRange == 2 && actionElementMap.axisContribution == null)
					{
						continue;
					}
					this.inputGrid.PopulateField(this.currentMapCategoryId, actionSet.actionId, actionSet.axisRange, controllerType, controllerId, num, actionElementMap.id, this._buttonDisplaySettings.GetDisplay(actionElementMap, false), false);
				}
				else if (actionElementMap.elementType == null)
				{
					if (actionSet.axisRange == null)
					{
						if (actionElementMap.axisRange != null)
						{
							continue;
						}
						this.inputGrid.PopulateField(this.currentMapCategoryId, actionSet.actionId, actionSet.axisRange, controllerType, controllerId, num, actionElementMap.id, this._buttonDisplaySettings.GetDisplay(actionElementMap, false), actionElementMap.invert);
					}
					else if (actionSet.axisRange == 1)
					{
						if ((actionElementMap.axisRange == null && ReInput.mapping.GetAction(actionSet.actionId).type != 1) || actionElementMap.axisContribution == 1)
						{
							continue;
						}
						this.inputGrid.PopulateField(this.currentMapCategoryId, actionSet.actionId, actionSet.axisRange, controllerType, controllerId, num, actionElementMap.id, this._buttonDisplaySettings.GetDisplay(actionElementMap, false), false);
					}
					else if (actionSet.axisRange == 2)
					{
						if (actionElementMap.axisRange == null || actionElementMap.axisContribution == null)
						{
							continue;
						}
						this.inputGrid.PopulateField(this.currentMapCategoryId, actionSet.actionId, actionSet.axisRange, controllerType, controllerId, num, actionElementMap.id, this._buttonDisplaySettings.GetDisplay(actionElementMap, false), false);
					}
				}
				num++;
				if (num > maxFields)
				{
					break;
				}
			}
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x0006861C File Offset: 0x0006681C
		private void DisableInputFieldGroup(ControlMapper.InputActionSet actionSet, ControllerType controllerType, int fieldCount)
		{
			for (int i = 0; i < fieldCount; i++)
			{
				ControlMapper.GUIInputField guiinputField = this.inputGrid.GetGUIInputField(this.currentMapCategoryId, actionSet.actionId, actionSet.axisRange, controllerType, i);
				if (guiinputField != null)
				{
					guiinputField.SetInteractible(false, false);
				}
			}
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x00068660 File Offset: 0x00066860
		private void ResetInputGridScrollBar()
		{
			this.references.inputGridInnerGroup.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			this.references.inputGridVScrollbar.value = 1f;
			this.references.inputGridScrollRect.verticalScrollbarVisibility = 1;
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x000686B0 File Offset: 0x000668B0
		private void CreateLayout()
		{
			this.references.playersGroup.gameObject.SetActive(this.showPlayers);
			this.references.controllerGroup.gameObject.SetActive(this._showControllers);
			this.references.assignedControllersGroup.gameObject.SetActive(this._showControllers && this.ShowAssignedControllers());
			this.references.settingsAndMapCategoriesGroup.gameObject.SetActive(this.showSettings || this.showMapCategories);
			this.references.settingsGroup.gameObject.SetActive(this.showSettings);
			this.references.mapCategoriesGroup.gameObject.SetActive(this.showMapCategories);
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x00013053 File Offset: 0x00011253
		private void Draw()
		{
			this.DrawPlayersGroup();
			this.DrawControllersGroup();
			this.DrawSettingsGroup();
			this.DrawMapCategoriesGroup();
			this.DrawWindowButtonsGroup();
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x00068778 File Offset: 0x00066978
		private void DrawPlayersGroup()
		{
			if (!this.showPlayers)
			{
				return;
			}
			this.references.playersGroup.labelText = this._language.playersGroupLabel;
			this.references.playersGroup.SetLabelActive(this._showPlayersGroupLabel);
			for (int i = 0; i < this.playerCount; i++)
			{
				Player player = ReInput.players.GetPlayer(i);
				if (player != null)
				{
					ControlMapper.GUIButton guibutton = new ControlMapper.GUIButton(UITools.InstantiateGUIObject<ButtonInfo>(this.prefabs.button, this.references.playersGroup.content, "Player" + i.ToString() + "Button"));
					guibutton.SetLabel(this._language.GetPlayerName(player.id));
					guibutton.SetButtonInfoData("PlayerSelection", player.id);
					guibutton.SetOnClickCallback(new Action<ButtonInfo>(this.OnButtonActivated));
					guibutton.buttonInfo.OnSelectedEvent += this.OnUIElementSelected;
					this.playerButtons.Add(guibutton);
				}
			}
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00068884 File Offset: 0x00066A84
		private void DrawControllersGroup()
		{
			if (!this._showControllers)
			{
				return;
			}
			this.references.controllerSettingsGroup.labelText = this._language.controllerSettingsGroupLabel;
			this.references.controllerSettingsGroup.SetLabelActive(this._showControllerGroupLabel);
			this.references.controllerNameLabel.gameObject.SetActive(this._showControllerNameLabel);
			this.references.controllerGroupLabelGroup.gameObject.SetActive(this._showControllerGroupLabel || this._showControllerNameLabel);
			if (this.ShowAssignedControllers())
			{
				this.references.assignedControllersGroup.labelText = this._language.assignedControllersGroupLabel;
				this.references.assignedControllersGroup.SetLabelActive(this._showAssignedControllersGroupLabel);
			}
			this.references.removeControllerButton.GetComponent<ButtonInfo>().text.text = this._language.removeControllerButtonLabel;
			this.references.calibrateControllerButton.GetComponent<ButtonInfo>().text.text = this._language.calibrateControllerButtonLabel;
			this.references.assignControllerButton.GetComponent<ButtonInfo>().text.text = this._language.assignControllerButtonLabel;
			ControlMapper.GUIButton guibutton = this.CreateButton(this._language.none, this.references.assignedControllersGroup.content, Vector2.zero);
			guibutton.SetInteractible(false, false, true);
			this.assignedControllerButtonsPlaceholder = guibutton;
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x000689EC File Offset: 0x00066BEC
		private void DrawSettingsGroup()
		{
			if (!this.showSettings)
			{
				return;
			}
			this.references.settingsGroup.labelText = this._language.settingsGroupLabel;
			this.references.settingsGroup.SetLabelActive(this._showSettingsGroupLabel);
			ControlMapper.GUIButton guibutton = this.CreateButton(this._language.inputBehaviorSettingsButtonLabel, this.references.settingsGroup.content, Vector2.zero);
			this.miscInstantiatedObjects.Add(guibutton.gameObject);
			guibutton.buttonInfo.OnSelectedEvent += this.OnUIElementSelected;
			guibutton.SetButtonInfoData("EditInputBehaviors", 0);
			guibutton.SetOnClickCallback(new Action<ButtonInfo>(this.OnButtonActivated));
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x00068AA0 File Offset: 0x00066CA0
		private void DrawMapCategoriesGroup()
		{
			if (!this.showMapCategories)
			{
				return;
			}
			if (this._mappingSets == null)
			{
				return;
			}
			this.references.mapCategoriesGroup.labelText = this._language.mapCategoriesGroupLabel;
			this.references.mapCategoriesGroup.SetLabelActive(this._showMapCategoriesGroupLabel);
			for (int i = 0; i < this._mappingSets.Length; i++)
			{
				ControlMapper.MappingSet mappingSet = this._mappingSets[i];
				if (mappingSet != null)
				{
					InputMapCategory mapCategory = ReInput.mapping.GetMapCategory(mappingSet.mapCategoryId);
					if (mapCategory != null)
					{
						ControlMapper.GUIButton guibutton = new ControlMapper.GUIButton(UITools.InstantiateGUIObject<ButtonInfo>(this.prefabs.button, this.references.mapCategoriesGroup.content, mapCategory.name + "Button"));
						guibutton.SetLabel(this._language.GetMapCategoryName(mapCategory.id));
						guibutton.SetButtonInfoData("MapCategorySelection", mapCategory.id);
						guibutton.SetOnClickCallback(new Action<ButtonInfo>(this.OnButtonActivated));
						guibutton.buttonInfo.OnSelectedEvent += this.OnUIElementSelected;
						this.mapCategoryButtons.Add(guibutton);
					}
				}
			}
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x00068BC4 File Offset: 0x00066DC4
		private void DrawWindowButtonsGroup()
		{
			this.references.doneButton.GetComponent<ButtonInfo>().text.text = this._language.doneButtonLabel;
			this.references.restoreDefaultsButton.GetComponent<ButtonInfo>().text.text = this._language.restoreDefaultsButtonLabel;
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x00013073 File Offset: 0x00011273
		private void Redraw(bool listsChanged, bool playTransitions)
		{
			this.RedrawPlayerGroup(playTransitions);
			this.RedrawControllerGroup();
			this.RedrawMapCategoriesGroup(playTransitions);
			this.RedrawInputGrid(listsChanged);
			if (this.currentUISelection == null || !this.currentUISelection.activeInHierarchy)
			{
				this.RestoreLastUISelection();
			}
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x00068C1C File Offset: 0x00066E1C
		private void RedrawPlayerGroup(bool playTransitions)
		{
			if (!this.showPlayers)
			{
				return;
			}
			for (int i = 0; i < this.playerButtons.Count; i++)
			{
				bool flag = this.currentPlayerId != this.playerButtons[i].buttonInfo.intData;
				this.playerButtons[i].SetInteractible(flag, playTransitions);
			}
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x00068C80 File Offset: 0x00066E80
		private void RedrawControllerGroup()
		{
			int num = -1;
			this.references.controllerNameLabel.text = this._language.none;
			UITools.SetInteractable(this.references.removeControllerButton, false, false);
			UITools.SetInteractable(this.references.assignControllerButton, false, false);
			UITools.SetInteractable(this.references.calibrateControllerButton, false, false);
			if (this.ShowAssignedControllers())
			{
				foreach (ControlMapper.GUIButton guibutton in this.assignedControllerButtons)
				{
					if (!(guibutton.gameObject == null))
					{
						if (this.currentUISelection == guibutton.gameObject)
						{
							num = guibutton.buttonInfo.intData;
						}
						Object.Destroy(guibutton.gameObject);
					}
				}
				this.assignedControllerButtons.Clear();
				this.assignedControllerButtonsPlaceholder.SetActive(true);
			}
			Player player = ReInput.players.GetPlayer(this.currentPlayerId);
			if (player == null)
			{
				return;
			}
			if (this.ShowAssignedControllers())
			{
				if (player.controllers.joystickCount > 0)
				{
					this.assignedControllerButtonsPlaceholder.SetActive(false);
				}
				foreach (Joystick joystick in player.controllers.Joysticks)
				{
					ControlMapper.GUIButton guibutton2 = this.CreateButton(this._language.GetControllerName(joystick), this.references.assignedControllersGroup.content, Vector2.zero);
					guibutton2.SetButtonInfoData("AssignedControllerSelection", joystick.id);
					guibutton2.SetOnClickCallback(new Action<ButtonInfo>(this.OnButtonActivated));
					guibutton2.buttonInfo.OnSelectedEvent += this.OnUIElementSelected;
					this.assignedControllerButtons.Add(guibutton2);
					if (joystick.id == this.currentJoystickId)
					{
						guibutton2.SetInteractible(false, true);
					}
				}
				if (player.controllers.joystickCount > 0 && !this.isJoystickSelected)
				{
					Controller lastActiveController = player.controllers.GetLastActiveController();
					if (lastActiveController is Joystick)
					{
						this.currentJoystickId = lastActiveController.id;
					}
					else
					{
						this.currentJoystickId = player.controllers.Joysticks[0].id;
					}
					this.assignedControllerButtons[0].SetInteractible(false, false);
				}
				if (num < 0)
				{
					goto IL_02FA;
				}
				using (List<ControlMapper.GUIButton>.Enumerator enumerator = this.assignedControllerButtons.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ControlMapper.GUIButton guibutton3 = enumerator.Current;
						if (guibutton3.buttonInfo.intData == num)
						{
							this.SetUISelection(guibutton3.gameObject);
							break;
						}
					}
					goto IL_02FA;
				}
			}
			if (player.controllers.joystickCount > 0 && !this.isJoystickSelected)
			{
				Controller lastActiveController2 = player.controllers.GetLastActiveController();
				if (lastActiveController2 is Joystick)
				{
					this.currentJoystickId = lastActiveController2.id;
				}
				else
				{
					this.currentJoystickId = player.controllers.Joysticks[0].id;
				}
			}
			IL_02FA:
			if (this.isJoystickSelected && player.controllers.joystickCount > 0)
			{
				this.references.removeControllerButton.interactable = true;
				this.references.controllerNameLabel.text = this._language.GetControllerName(this.currentJoystick);
				if (this.currentJoystick.axisCount > 0)
				{
					this.references.calibrateControllerButton.interactable = true;
				}
			}
			int joystickCount = player.controllers.joystickCount;
			int joystickCount2 = ReInput.controllers.joystickCount;
			int maxControllersPerPlayer = this.GetMaxControllersPerPlayer();
			bool flag = maxControllersPerPlayer == 0;
			if (joystickCount2 > 0 && joystickCount < joystickCount2 && (maxControllersPerPlayer == 1 || flag || joystickCount < maxControllersPerPlayer))
			{
				UITools.SetInteractable(this.references.assignControllerButton, true, false);
			}
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x00069068 File Offset: 0x00067268
		private void RedrawMapCategoriesGroup(bool playTransitions)
		{
			if (!this.showMapCategories)
			{
				return;
			}
			for (int i = 0; i < this.mapCategoryButtons.Count; i++)
			{
				bool flag = this.currentMapCategoryId != this.mapCategoryButtons[i].buttonInfo.intData;
				this.mapCategoryButtons[i].SetInteractible(flag, playTransitions);
			}
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x000130B1 File Offset: 0x000112B1
		private void RedrawInputGrid(bool listsChanged)
		{
			if (listsChanged)
			{
				this.RefreshInputGridStructure();
			}
			this.PopulateInputFields();
			if (listsChanged)
			{
				this.ResetInputGridScrollBar();
			}
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x000130CB File Offset: 0x000112CB
		private void ForceRefresh()
		{
			if (this.windowManager.isWindowOpen)
			{
				this.CloseAllWindows();
				return;
			}
			this.Redraw(false, false);
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x000690CC File Offset: 0x000672CC
		private void CreateInputCategoryRow(ref int rowCount, InputCategory category)
		{
			this.CreateLabel(this._language.GetMapCategoryName(category.id), this.references.inputGridActionColumn, new Vector2(0f, (float)(rowCount * this._inputRowHeight) * -1f));
			rowCount++;
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x000130E9 File Offset: 0x000112E9
		private ControlMapper.GUILabel CreateLabel(string labelText, Transform parent, Vector2 offset)
		{
			return this.CreateLabel(this.prefabs.inputGridLabel, labelText, parent, offset);
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x0006911C File Offset: 0x0006731C
		private ControlMapper.GUILabel CreateLabel(GameObject prefab, string labelText, Transform parent, Vector2 offset)
		{
			GameObject gameObject = this.InstantiateGUIObject(prefab, parent, offset);
			Text componentInSelfOrChildren = UnityTools.GetComponentInSelfOrChildren<Text>(gameObject);
			if (componentInSelfOrChildren == null)
			{
				Debug.LogError("Rewired Control Mapper: Label prefab is missing Text component!");
				return null;
			}
			componentInSelfOrChildren.text = labelText;
			return new ControlMapper.GUILabel(gameObject);
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x000130FF File Offset: 0x000112FF
		private ControlMapper.GUIButton CreateButton(string labelText, Transform parent, Vector2 offset)
		{
			ControlMapper.GUIButton guibutton = new ControlMapper.GUIButton(this.InstantiateGUIObject(this.prefabs.button, parent, offset));
			guibutton.SetLabel(labelText);
			return guibutton;
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x00013120 File Offset: 0x00011320
		private ControlMapper.GUIButton CreateFitButton(string labelText, Transform parent, Vector2 offset)
		{
			ControlMapper.GUIButton guibutton = new ControlMapper.GUIButton(this.InstantiateGUIObject(this.prefabs.fitButton, parent, offset));
			guibutton.SetLabel(labelText);
			return guibutton;
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00013141 File Offset: 0x00011341
		private ControlMapper.GUIInputField CreateInputField(Transform parent, Vector2 offset, string label, int actionId, AxisRange axisRange, ControllerType controllerType, int fieldIndex)
		{
			ControlMapper.GUIInputField guiinputField = this.CreateInputField(parent, offset);
			guiinputField.SetFieldInfoData(actionId, axisRange, controllerType, fieldIndex);
			guiinputField.SetOnClickCallback(this.inputFieldActivatedDelegate);
			guiinputField.fieldInfo.OnSelectedEvent += this.OnUIElementSelected;
			return guiinputField;
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0001317C File Offset: 0x0001137C
		private ControlMapper.GUIInputField CreateInputField(Transform parent, Vector2 offset)
		{
			return new ControlMapper.GUIInputField(this.InstantiateGUIObject(this.prefabs.inputGridFieldButton, parent, offset));
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x00013196 File Offset: 0x00011396
		private ControlMapper.GUIToggle CreateToggle(GameObject prefab, Transform parent, Vector2 offset, string label, int actionId, AxisRange axisRange, ControllerType controllerType, int fieldIndex)
		{
			ControlMapper.GUIToggle guitoggle = this.CreateToggle(prefab, parent, offset);
			guitoggle.SetToggleInfoData(actionId, axisRange, controllerType, fieldIndex);
			guitoggle.SetOnSubmitCallback(this.inputFieldInvertToggleStateChangedDelegate);
			guitoggle.toggleInfo.OnSelectedEvent += this.OnUIElementSelected;
			return guitoggle;
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x000131D2 File Offset: 0x000113D2
		private ControlMapper.GUIToggle CreateToggle(GameObject prefab, Transform parent, Vector2 offset)
		{
			return new ControlMapper.GUIToggle(this.InstantiateGUIObject(prefab, parent, offset));
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x00069160 File Offset: 0x00067360
		private GameObject InstantiateGUIObject(GameObject prefab, Transform parent, Vector2 offset)
		{
			if (prefab == null)
			{
				Debug.LogError("Rewired Control Mapper: Prefab is null!");
				return null;
			}
			GameObject gameObject = Object.Instantiate<GameObject>(prefab);
			return this.InitializeNewGUIGameObject(gameObject, parent, offset);
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x00069194 File Offset: 0x00067394
		private GameObject CreateNewGUIObject(string name, Transform parent, Vector2 offset)
		{
			GameObject gameObject = new GameObject();
			gameObject.name = name;
			gameObject.AddComponent<RectTransform>();
			return this.InitializeNewGUIGameObject(gameObject, parent, offset);
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x000691C0 File Offset: 0x000673C0
		private GameObject InitializeNewGUIGameObject(GameObject gameObject, Transform parent, Vector2 offset)
		{
			if (gameObject == null)
			{
				Debug.LogError("Rewired Control Mapper: GameObject is null!");
				return null;
			}
			RectTransform component = gameObject.GetComponent<RectTransform>();
			if (component == null)
			{
				Debug.LogError("Rewired Control Mapper: GameObject does not have a RectTransform component!");
				return gameObject;
			}
			if (parent != null)
			{
				component.SetParent(parent, false);
			}
			component.anchoredPosition = offset;
			return gameObject;
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x00069218 File Offset: 0x00067418
		private GameObject CreateNewColumnGroup(string name, Transform parent, int maxWidth)
		{
			GameObject gameObject = this.CreateNewGUIObject(name, parent, Vector2.zero);
			this.inputGrid.AddGroup(gameObject);
			LayoutElement layoutElement = gameObject.AddComponent<LayoutElement>();
			if (maxWidth >= 0)
			{
				layoutElement.preferredWidth = (float)maxWidth;
			}
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.anchorMin = new Vector2(0f, 0f);
			component.anchorMax = new Vector2(1f, 0f);
			return gameObject;
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x000131E2 File Offset: 0x000113E2
		private Window OpenWindow(bool closeOthers)
		{
			return this.OpenWindow(string.Empty, closeOthers);
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x00069284 File Offset: 0x00067484
		private Window OpenWindow(string name, bool closeOthers)
		{
			if (closeOthers)
			{
				this.windowManager.CancelAll();
			}
			Window window = this.windowManager.OpenWindow(name, this._defaultWindowWidth, this._defaultWindowHeight);
			if (window == null)
			{
				return null;
			}
			this.ChildWindowOpened();
			return window;
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x000131F0 File Offset: 0x000113F0
		private Window OpenWindow(GameObject windowPrefab, bool closeOthers)
		{
			return this.OpenWindow(windowPrefab, string.Empty, closeOthers);
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x000692CC File Offset: 0x000674CC
		private Window OpenWindow(GameObject windowPrefab, string name, bool closeOthers)
		{
			if (closeOthers)
			{
				this.windowManager.CancelAll();
			}
			Window window = this.windowManager.OpenWindow(windowPrefab, name);
			if (window == null)
			{
				return null;
			}
			this.ChildWindowOpened();
			return window;
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x00069308 File Offset: 0x00067508
		private void OpenModal(string title, string message, string confirmText, Action<int> confirmAction, string cancelText, Action<int> cancelAction, bool closeOthers)
		{
			Window window = this.OpenWindow(closeOthers);
			if (window == null)
			{
				return;
			}
			window.CreateTitleText(this.prefabs.windowTitleText, Vector2.zero, title);
			window.AddContentText(this.prefabs.windowContentText, UIPivot.TopCenter, UIAnchor.TopHStretch, new Vector2(0f, -100f), message);
			UnityAction unityAction = delegate
			{
				this.OnWindowCancel(window.id);
			};
			window.cancelCallback = unityAction;
			window.CreateButton(this.prefabs.fitButton, UIPivot.BottomLeft, UIAnchor.BottomLeft, Vector2.zero, confirmText, delegate
			{
				this.OnRestoreDefaultsConfirmed(window.id);
			}, unityAction, false);
			window.CreateButton(this.prefabs.fitButton, UIPivot.BottomRight, UIAnchor.BottomRight, Vector2.zero, cancelText, unityAction, unityAction, true);
			this.windowManager.Focus(window);
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x000131FF File Offset: 0x000113FF
		private void CloseWindow(int windowId)
		{
			if (!this.windowManager.isWindowOpen)
			{
				return;
			}
			this.windowManager.CloseWindow(windowId);
			this.ChildWindowClosed();
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00013221 File Offset: 0x00011421
		private void CloseTopWindow()
		{
			if (!this.windowManager.isWindowOpen)
			{
				return;
			}
			this.windowManager.CloseTop();
			this.ChildWindowClosed();
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x00013242 File Offset: 0x00011442
		private void CloseAllWindows()
		{
			if (!this.windowManager.isWindowOpen)
			{
				return;
			}
			this.windowManager.CancelAll();
			this.ChildWindowClosed();
			this.InputPollingStopped();
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x00013269 File Offset: 0x00011469
		private void ChildWindowOpened()
		{
			if (!this.windowManager.isWindowOpen)
			{
				return;
			}
			this.SetIsFocused(false);
			if (this._PopupWindowOpenedEvent != null)
			{
				this._PopupWindowOpenedEvent();
			}
			if (this._onPopupWindowOpened != null)
			{
				this._onPopupWindowOpened.Invoke();
			}
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x000132A6 File Offset: 0x000114A6
		private void ChildWindowClosed()
		{
			if (this.windowManager.isWindowOpen)
			{
				return;
			}
			this.SetIsFocused(true);
			if (this._PopupWindowClosedEvent != null)
			{
				this._PopupWindowClosedEvent();
			}
			if (this._onPopupWindowClosed != null)
			{
				this._onPopupWindowClosed.Invoke();
			}
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x00069414 File Offset: 0x00067614
		private bool HasElementAssignmentConflicts(Player player, ControlMapper.InputMapping mapping, ElementAssignment assignment, bool skipOtherPlayers)
		{
			if (player == null || mapping == null)
			{
				return false;
			}
			ElementAssignmentConflictCheck elementAssignmentConflictCheck;
			if (!this.CreateConflictCheck(mapping, assignment, out elementAssignmentConflictCheck))
			{
				return false;
			}
			if (skipOtherPlayers)
			{
				return ReInput.players.SystemPlayer.controllers.conflictChecking.DoesElementAssignmentConflict(elementAssignmentConflictCheck) || player.controllers.conflictChecking.DoesElementAssignmentConflict(elementAssignmentConflictCheck);
			}
			return ReInput.controllers.conflictChecking.DoesElementAssignmentConflict(elementAssignmentConflictCheck);
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x00069480 File Offset: 0x00067680
		private bool IsBlockingAssignmentConflict(ControlMapper.InputMapping mapping, ElementAssignment assignment, bool skipOtherPlayers)
		{
			ElementAssignmentConflictCheck elementAssignmentConflictCheck;
			if (!this.CreateConflictCheck(mapping, assignment, out elementAssignmentConflictCheck))
			{
				return false;
			}
			if (skipOtherPlayers)
			{
				foreach (ElementAssignmentConflictInfo elementAssignmentConflictInfo in ReInput.players.SystemPlayer.controllers.conflictChecking.ElementAssignmentConflicts(elementAssignmentConflictCheck))
				{
					if (!elementAssignmentConflictInfo.isUserAssignable)
					{
						return true;
					}
				}
				using (IEnumerator<ElementAssignmentConflictInfo> enumerator = this.currentPlayer.controllers.conflictChecking.ElementAssignmentConflicts(elementAssignmentConflictCheck).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ElementAssignmentConflictInfo elementAssignmentConflictInfo2 = enumerator.Current;
						if (!elementAssignmentConflictInfo2.isUserAssignable)
						{
							return true;
						}
					}
					return false;
				}
			}
			foreach (ElementAssignmentConflictInfo elementAssignmentConflictInfo3 in ReInput.controllers.conflictChecking.ElementAssignmentConflicts(elementAssignmentConflictCheck))
			{
				if (!elementAssignmentConflictInfo3.isUserAssignable)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x000132E3 File Offset: 0x000114E3
		private IEnumerable<ElementAssignmentConflictInfo> ElementAssignmentConflicts(Player player, ControlMapper.InputMapping mapping, ElementAssignment assignment, bool skipOtherPlayers)
		{
			if (player == null || mapping == null)
			{
				yield break;
			}
			ElementAssignmentConflictCheck conflictCheck;
			if (!this.CreateConflictCheck(mapping, assignment, out conflictCheck))
			{
				yield break;
			}
			if (skipOtherPlayers)
			{
				foreach (ElementAssignmentConflictInfo elementAssignmentConflictInfo in ReInput.players.SystemPlayer.controllers.conflictChecking.ElementAssignmentConflicts(conflictCheck))
				{
					if (!elementAssignmentConflictInfo.isUserAssignable)
					{
						yield return elementAssignmentConflictInfo;
					}
				}
				IEnumerator<ElementAssignmentConflictInfo> enumerator = null;
				foreach (ElementAssignmentConflictInfo elementAssignmentConflictInfo2 in player.controllers.conflictChecking.ElementAssignmentConflicts(conflictCheck))
				{
					if (!elementAssignmentConflictInfo2.isUserAssignable)
					{
						yield return elementAssignmentConflictInfo2;
					}
				}
				enumerator = null;
			}
			else
			{
				foreach (ElementAssignmentConflictInfo elementAssignmentConflictInfo3 in ReInput.controllers.conflictChecking.ElementAssignmentConflicts(conflictCheck))
				{
					if (!elementAssignmentConflictInfo3.isUserAssignable)
					{
						yield return elementAssignmentConflictInfo3;
					}
				}
				IEnumerator<ElementAssignmentConflictInfo> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x000695A4 File Offset: 0x000677A4
		private bool CreateConflictCheck(ControlMapper.InputMapping mapping, ElementAssignment assignment, out ElementAssignmentConflictCheck conflictCheck)
		{
			if (mapping == null || this.currentPlayer == null)
			{
				conflictCheck = default(ElementAssignmentConflictCheck);
				return false;
			}
			conflictCheck = assignment.ToElementAssignmentConflictCheck();
			conflictCheck.playerId = this.currentPlayer.id;
			conflictCheck.controllerType = mapping.controllerType;
			conflictCheck.controllerId = mapping.controllerId;
			conflictCheck.controllerMapId = mapping.map.id;
			conflictCheck.controllerMapCategoryId = mapping.map.categoryId;
			if (mapping.aem != null)
			{
				conflictCheck.elementMapId = mapping.aem.id;
			}
			return true;
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x00069638 File Offset: 0x00067838
		private void PollKeyboardForAssignment(out ControllerPollingInfo pollingInfo, out bool modifierKeyPressed, out ModifierKeyFlags modifierFlags, out string label)
		{
			pollingInfo = default(ControllerPollingInfo);
			label = string.Empty;
			modifierKeyPressed = false;
			modifierFlags = 0;
			int num = 0;
			ControllerPollingInfo controllerPollingInfo = default(ControllerPollingInfo);
			ControllerPollingInfo controllerPollingInfo2 = default(ControllerPollingInfo);
			ModifierKeyFlags modifierKeyFlags = 0;
			foreach (ControllerPollingInfo controllerPollingInfo3 in ReInput.controllers.Keyboard.PollForAllKeys())
			{
				KeyCode keyboardKey = controllerPollingInfo3.keyboardKey;
				if (keyboardKey != 313)
				{
					if (Keyboard.IsModifierKey(controllerPollingInfo3.keyboardKey))
					{
						if (num == 0)
						{
							controllerPollingInfo2 = controllerPollingInfo3;
						}
						modifierKeyFlags |= Keyboard.KeyCodeToModifierKeyFlags(keyboardKey);
						num++;
					}
					else if (controllerPollingInfo.keyboardKey == null)
					{
						controllerPollingInfo = controllerPollingInfo3;
					}
				}
			}
			if (controllerPollingInfo.keyboardKey == null)
			{
				if (num > 0)
				{
					modifierKeyPressed = true;
					if (num == 1)
					{
						if (ReInput.controllers.Keyboard.GetKeyTimePressed(controllerPollingInfo2.keyboardKey) > 1.0)
						{
							pollingInfo = controllerPollingInfo2;
							return;
						}
						label = Keyboard.GetKeyName(controllerPollingInfo2.keyboardKey);
						return;
					}
					else
					{
						label = this._language.ModifierKeyFlagsToString(modifierKeyFlags);
					}
				}
				return;
			}
			if (!ReInput.controllers.Keyboard.GetKeyDown(controllerPollingInfo.keyboardKey))
			{
				return;
			}
			if (num == 0)
			{
				pollingInfo = controllerPollingInfo;
				return;
			}
			pollingInfo = controllerPollingInfo;
			modifierFlags = modifierKeyFlags;
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00069784 File Offset: 0x00067984
		private bool GetFirstElementAssignmentConflict(ElementAssignmentConflictCheck conflictCheck, out ElementAssignmentConflictInfo conflict, bool skipOtherPlayers)
		{
			if (this.GetFirstElementAssignmentConflict(this.currentPlayer, conflictCheck, out conflict))
			{
				return true;
			}
			if (this.GetFirstElementAssignmentConflict(ReInput.players.SystemPlayer, conflictCheck, out conflict))
			{
				return true;
			}
			if (!skipOtherPlayers)
			{
				IList<Player> players = ReInput.players.Players;
				for (int i = 0; i < players.Count; i++)
				{
					Player player = players[i];
					if (player != this.currentPlayer && this.GetFirstElementAssignmentConflict(player, conflictCheck, out conflict))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x000697F8 File Offset: 0x000679F8
		private bool GetFirstElementAssignmentConflict(Player player, ElementAssignmentConflictCheck conflictCheck, out ElementAssignmentConflictInfo conflict)
		{
			using (IEnumerator<ElementAssignmentConflictInfo> enumerator = player.controllers.conflictChecking.ElementAssignmentConflicts(conflictCheck).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ElementAssignmentConflictInfo elementAssignmentConflictInfo = enumerator.Current;
					conflict = elementAssignmentConflictInfo;
					return true;
				}
			}
			conflict = default(ElementAssignmentConflictInfo);
			return false;
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x00069860 File Offset: 0x00067A60
		private void StartAxisCalibration(int axisIndex)
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			if (this.currentPlayer.controllers.joystickCount == 0)
			{
				return;
			}
			Joystick currentJoystick = this.currentJoystick;
			if (axisIndex < 0 || axisIndex >= currentJoystick.axisCount)
			{
				return;
			}
			this.pendingAxisCalibration = new ControlMapper.AxisCalibrator(currentJoystick, axisIndex);
			this.ShowCalibrateAxisStep1Window();
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00013310 File Offset: 0x00011510
		private void EndAxisCalibration()
		{
			if (this.pendingAxisCalibration == null)
			{
				return;
			}
			this.pendingAxisCalibration.Commit();
			this.pendingAxisCalibration = null;
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0001332D File Offset: 0x0001152D
		private void SetUISelection(GameObject selection)
		{
			if (EventSystem.current == null)
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(selection);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x00013348 File Offset: 0x00011548
		private void RestoreLastUISelection()
		{
			if (this.lastUISelection == null || !this.lastUISelection.activeInHierarchy)
			{
				this.SetDefaultUISelection();
				return;
			}
			this.SetUISelection(this.lastUISelection);
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x00013378 File Offset: 0x00011578
		private void SetDefaultUISelection()
		{
			if (!this.isOpen)
			{
				return;
			}
			if (this.references.defaultSelection == null)
			{
				this.SetUISelection(null);
				return;
			}
			this.SetUISelection(this.references.defaultSelection.gameObject);
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x000698B4 File Offset: 0x00067AB4
		private void SelectDefaultMapCategory(bool redraw)
		{
			this.currentMapCategoryId = this.GetDefaultMapCategoryId();
			this.OnMapCategorySelected(this.currentMapCategoryId, redraw);
			if (!this.showMapCategories)
			{
				return;
			}
			for (int i = 0; i < this._mappingSets.Length; i++)
			{
				if (ReInput.mapping.GetMapCategory(this._mappingSets[i].mapCategoryId) != null)
				{
					this.currentMapCategoryId = this._mappingSets[i].mapCategoryId;
					break;
				}
			}
			if (this.currentMapCategoryId < 0)
			{
				return;
			}
			for (int j = 0; j < this._mappingSets.Length; j++)
			{
				bool flag = this._mappingSets[j].mapCategoryId != this.currentMapCategoryId;
				this.mapCategoryButtons[j].SetInteractible(flag, false);
			}
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x000133B4 File Offset: 0x000115B4
		private void CheckUISelection()
		{
			if (!this.isFocused)
			{
				return;
			}
			if (this.currentUISelection == null)
			{
				this.RestoreLastUISelection();
			}
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x000133D3 File Offset: 0x000115D3
		private void OnUIElementSelected(GameObject selectedObject)
		{
			this.lastUISelection = selectedObject;
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x000133DC File Offset: 0x000115DC
		private void SetIsFocused(bool state)
		{
			this.references.mainCanvasGroup.interactable = state;
			if (state)
			{
				this.Redraw(false, false);
				this.RestoreLastUISelection();
				this.blockInputOnFocusEndTime = Time.unscaledTime + 0.1f;
			}
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x00013411 File Offset: 0x00011611
		public void Toggle()
		{
			if (this.isOpen)
			{
				this.Close(true);
				return;
			}
			this.Open();
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00013429 File Offset: 0x00011629
		public void Open()
		{
			this.Open(false);
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x00069970 File Offset: 0x00067B70
		private void Open(bool force)
		{
			ControlMapper.isActive = true;
			if (!this.initialized)
			{
				this.Initialize();
			}
			if (!this.initialized)
			{
				return;
			}
			if (!force && this.isOpen)
			{
				return;
			}
			this.Clear();
			this.canvas.SetActive(true);
			this.OnPlayerSelected(0, false);
			this.SelectDefaultMapCategory(false);
			this.SetDefaultUISelection();
			this.Redraw(true, false);
			if (this._ScreenOpenedEvent != null)
			{
				this._ScreenOpenedEvent();
			}
			if (this._onScreenOpened != null)
			{
				this._onScreenOpened.Invoke();
			}
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x000699FC File Offset: 0x00067BFC
		public void Close(bool save)
		{
			ControlMapper.isActive = false;
			if (!this.initialized)
			{
				return;
			}
			if (!this.isOpen)
			{
				return;
			}
			if (save && ReInput.userDataStore != null)
			{
				ReInput.userDataStore.Save();
			}
			this.Clear();
			this.canvas.SetActive(false);
			this.SetUISelection(null);
			if (this._ScreenClosedEvent != null)
			{
				this._ScreenClosedEvent();
			}
			if (this._onScreenClosed != null)
			{
				this._onScreenClosed.Invoke();
			}
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x00013432 File Offset: 0x00011632
		private void Clear()
		{
			this.windowManager.CancelAll();
			this.lastUISelection = null;
			this.pendingInputMapping = null;
			this.pendingAxisCalibration = null;
			this.InputPollingStopped();
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0001345A File Offset: 0x0001165A
		private void ClearCompletely()
		{
			this.Clear();
			this.ClearSpawnedObjects();
			this.ClearAllVars();
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x00069A74 File Offset: 0x00067C74
		private void ClearSpawnedObjects()
		{
			this.windowManager.ClearCompletely();
			this.inputGrid.ClearAll();
			foreach (ControlMapper.GUIButton guibutton in this.playerButtons)
			{
				Object.Destroy(guibutton.gameObject);
			}
			this.playerButtons.Clear();
			foreach (ControlMapper.GUIButton guibutton2 in this.mapCategoryButtons)
			{
				Object.Destroy(guibutton2.gameObject);
			}
			this.mapCategoryButtons.Clear();
			foreach (ControlMapper.GUIButton guibutton3 in this.assignedControllerButtons)
			{
				Object.Destroy(guibutton3.gameObject);
			}
			this.assignedControllerButtons.Clear();
			if (this.assignedControllerButtonsPlaceholder != null)
			{
				Object.Destroy(this.assignedControllerButtonsPlaceholder.gameObject);
				this.assignedControllerButtonsPlaceholder = null;
			}
			foreach (GameObject gameObject in this.miscInstantiatedObjects)
			{
				Object.Destroy(gameObject);
			}
			this.miscInstantiatedObjects.Clear();
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x0001346E File Offset: 0x0001166E
		private void ClearVarsOnPlayerChange()
		{
			this.currentJoystickId = -1;
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x0001346E File Offset: 0x0001166E
		private void ClearVarsOnJoystickChange()
		{
			this.currentJoystickId = -1;
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x00069BF4 File Offset: 0x00067DF4
		private void ClearAllVars()
		{
			this.initialized = false;
			ControlMapper.Instance = null;
			this.playerCount = 0;
			this.inputGrid = null;
			this.windowManager = null;
			this.currentPlayerId = -1;
			this.currentMapCategoryId = -1;
			this.playerButtons = null;
			this.mapCategoryButtons = null;
			this.miscInstantiatedObjects = null;
			this.canvas = null;
			this.lastUISelection = null;
			this.currentJoystickId = -1;
			this.pendingInputMapping = null;
			this.pendingAxisCalibration = null;
			this.inputFieldActivatedDelegate = null;
			this.inputFieldInvertToggleStateChangedDelegate = null;
			this.isPollingForInput = false;
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x00013477 File Offset: 0x00011677
		public void Reset()
		{
			if (!this.initialized)
			{
				return;
			}
			this.ClearCompletely();
			this.Initialize();
			if (this.isOpen)
			{
				this.Open(true);
			}
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x00069C80 File Offset: 0x00067E80
		private void SetActionAxisInverted(bool state, ControllerType controllerType, int actionElementMapId)
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			ControllerMapWithAxes controllerMapWithAxes = this.GetControllerMap(controllerType) as ControllerMapWithAxes;
			if (controllerMapWithAxes == null)
			{
				return;
			}
			ActionElementMap elementMap = controllerMapWithAxes.GetElementMap(actionElementMapId);
			if (elementMap == null)
			{
				return;
			}
			elementMap.invert = state;
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x00069CBC File Offset: 0x00067EBC
		private ControllerMap GetControllerMap(ControllerType type)
		{
			if (this.currentPlayer == null)
			{
				return null;
			}
			int num = 0;
			switch (type)
			{
			case 0:
			case 1:
				break;
			case 2:
				if (this.currentPlayer.controllers.joystickCount <= 0)
				{
					return null;
				}
				num = this.currentJoystick.id;
				break;
			default:
				throw new NotImplementedException();
			}
			return this.currentPlayer.controllers.maps.GetFirstMapInCategory(type, num, this.currentMapCategoryId);
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x00069D30 File Offset: 0x00067F30
		private ControllerMap GetControllerMapOrCreateNew(ControllerType controllerType, int controllerId, int layoutId)
		{
			ControllerMap controllerMap = this.GetControllerMap(controllerType);
			if (controllerMap == null)
			{
				this.currentPlayer.controllers.maps.AddEmptyMap(controllerType, controllerId, this.currentMapCategoryId, layoutId);
				controllerMap = this.currentPlayer.controllers.maps.GetMap(controllerType, controllerId, this.currentMapCategoryId, layoutId);
			}
			return controllerMap;
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x00069D88 File Offset: 0x00067F88
		private int CountIEnumerable<T>(IEnumerable<T> enumerable)
		{
			if (enumerable == null)
			{
				return 0;
			}
			IEnumerator<T> enumerator = enumerable.GetEnumerator();
			if (enumerator == null)
			{
				return 0;
			}
			int num = 0;
			while (enumerator.MoveNext())
			{
				num++;
			}
			return num;
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x00069DB8 File Offset: 0x00067FB8
		private int GetDefaultMapCategoryId()
		{
			if (this._mappingSets.Length == 0)
			{
				return 0;
			}
			for (int i = 0; i < this._mappingSets.Length; i++)
			{
				if (ReInput.mapping.GetMapCategory(this._mappingSets[i].mapCategoryId) != null)
				{
					return this._mappingSets[i].mapCategoryId;
				}
			}
			return 0;
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x00069E0C File Offset: 0x0006800C
		private void SubscribeFixedUISelectionEvents()
		{
			if (this.references.fixedSelectableUIElements == null)
			{
				return;
			}
			GameObject[] fixedSelectableUIElements = this.references.fixedSelectableUIElements;
			for (int i = 0; i < fixedSelectableUIElements.Length; i++)
			{
				UIElementInfo component = UnityTools.GetComponent<UIElementInfo>(fixedSelectableUIElements[i]);
				if (!(component == null))
				{
					component.OnSelectedEvent += this.OnUIElementSelected;
				}
			}
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x00069E68 File Offset: 0x00068068
		private void SubscribeMenuControlInputEvents()
		{
			this.SubscribeRewiredInputEventAllPlayers(this._screenToggleAction, new Action<InputActionEventData>(this.OnScreenToggleActionPressed));
			this.SubscribeRewiredInputEventAllPlayers(this._screenOpenAction, new Action<InputActionEventData>(this.OnScreenOpenActionPressed));
			this.SubscribeRewiredInputEventAllPlayers(this._screenCloseAction, new Action<InputActionEventData>(this.OnScreenCloseActionPressed));
			this.SubscribeRewiredInputEventAllPlayers(this._universalCancelAction, new Action<InputActionEventData>(this.OnUniversalCancelActionPressed));
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x00069ED8 File Offset: 0x000680D8
		private void UnsubscribeMenuControlInputEvents()
		{
			this.UnsubscribeRewiredInputEventAllPlayers(this._screenToggleAction, new Action<InputActionEventData>(this.OnScreenToggleActionPressed));
			this.UnsubscribeRewiredInputEventAllPlayers(this._screenOpenAction, new Action<InputActionEventData>(this.OnScreenOpenActionPressed));
			this.UnsubscribeRewiredInputEventAllPlayers(this._screenCloseAction, new Action<InputActionEventData>(this.OnScreenCloseActionPressed));
			this.UnsubscribeRewiredInputEventAllPlayers(this._universalCancelAction, new Action<InputActionEventData>(this.OnUniversalCancelActionPressed));
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x00069F48 File Offset: 0x00068148
		private void SubscribeRewiredInputEventAllPlayers(int actionId, Action<InputActionEventData> callback)
		{
			if (actionId < 0 || callback == null)
			{
				return;
			}
			if (ReInput.mapping.GetAction(actionId) == null)
			{
				Debug.LogWarning("Rewired Control Mapper: " + actionId.ToString() + " is not a valid Action id!");
				return;
			}
			foreach (Player player in ReInput.players.AllPlayers)
			{
				player.AddInputEventDelegate(callback, 0, 3, actionId);
			}
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x00069FCC File Offset: 0x000681CC
		private void UnsubscribeRewiredInputEventAllPlayers(int actionId, Action<InputActionEventData> callback)
		{
			if (actionId < 0 || callback == null)
			{
				return;
			}
			if (!ReInput.isReady)
			{
				return;
			}
			if (ReInput.mapping.GetAction(actionId) == null)
			{
				Debug.LogWarning("Rewired Control Mapper: " + actionId.ToString() + " is not a valid Action id!");
				return;
			}
			foreach (Player player in ReInput.players.AllPlayers)
			{
				player.RemoveInputEventDelegate(callback, 0, 3, actionId);
			}
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x0001349D File Offset: 0x0001169D
		private int GetMaxControllersPerPlayer()
		{
			if (this._rewiredInputManager.userData.ConfigVars.autoAssignJoysticks)
			{
				return this._rewiredInputManager.userData.ConfigVars.maxJoysticksPerPlayer;
			}
			return this._maxControllersPerPlayer;
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x000134D2 File Offset: 0x000116D2
		private bool ShowAssignedControllers()
		{
			return this._showControllers && (this._showAssignedControllers || this.GetMaxControllersPerPlayer() != 1);
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x000134F4 File Offset: 0x000116F4
		private void InspectorPropertyChanged(bool reset = false)
		{
			if (reset)
			{
				this.Reset();
			}
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0006A058 File Offset: 0x00068258
		private void AssignController(Player player, int controllerId)
		{
			if (player == null)
			{
				return;
			}
			if (player.controllers.ContainsController(2, controllerId))
			{
				return;
			}
			if (this.GetMaxControllersPerPlayer() == 1)
			{
				this.RemoveAllControllers(player);
				this.ClearVarsOnJoystickChange();
			}
			foreach (Player player2 in ReInput.players.Players)
			{
				if (player2 != player)
				{
					this.RemoveController(player2, controllerId);
				}
			}
			player.controllers.AddController(2, controllerId, false);
			if (ReInput.userDataStore != null)
			{
				ReInput.userDataStore.LoadControllerData(player.id, 2, controllerId);
			}
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x0006A100 File Offset: 0x00068300
		private void RemoveAllControllers(Player player)
		{
			if (player == null)
			{
				return;
			}
			IList<Joystick> joysticks = player.controllers.Joysticks;
			for (int i = joysticks.Count - 1; i >= 0; i--)
			{
				this.RemoveController(player, joysticks[i].id);
			}
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x000134FF File Offset: 0x000116FF
		private void RemoveController(Player player, int controllerId)
		{
			if (player == null)
			{
				return;
			}
			if (!player.controllers.ContainsController(2, controllerId))
			{
				return;
			}
			if (ReInput.userDataStore != null)
			{
				ReInput.userDataStore.SaveControllerData(player.id, 2, controllerId);
			}
			player.controllers.RemoveController(2, controllerId);
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x0001353B File Offset: 0x0001173B
		private bool IsAllowedAssignment(ControlMapper.InputMapping pendingInputMapping, ControllerPollingInfo pollingInfo)
		{
			return pendingInputMapping != null && (pendingInputMapping.axisRange != null || this._showSplitAxisInputFields || pollingInfo.elementType != 1);
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x0001355F File Offset: 0x0001175F
		private void InputPollingStarted()
		{
			bool flag = this.isPollingForInput;
			this.isPollingForInput = true;
			if (!flag)
			{
				if (this._InputPollingStartedEvent != null)
				{
					this._InputPollingStartedEvent();
				}
				if (this._onInputPollingStarted != null)
				{
					this._onInputPollingStarted.Invoke();
				}
			}
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x00013596 File Offset: 0x00011796
		private void InputPollingStopped()
		{
			bool flag = this.isPollingForInput;
			this.isPollingForInput = false;
			if (flag)
			{
				if (this._InputPollingEndedEvent != null)
				{
					this._InputPollingEndedEvent();
				}
				if (this._onInputPollingEnded != null)
				{
					this._onInputPollingEnded.Invoke();
				}
			}
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x000135CD File Offset: 0x000117CD
		private int GetControllerInputFieldCount(ControllerType controllerType)
		{
			switch (controllerType)
			{
			case 0:
				return this._keyboardInputFieldCount;
			case 1:
				return this._mouseInputFieldCount;
			case 2:
				return this._controllerInputFieldCount;
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x0006A144 File Offset: 0x00068344
		private bool ShowSwapButton(int windowId, ControlMapper.InputMapping mapping, ElementAssignment assignment, bool skipOtherPlayers)
		{
			if (this.currentPlayer == null)
			{
				return false;
			}
			if (!this._allowElementAssignmentSwap)
			{
				return false;
			}
			if (mapping == null || mapping.aem == null)
			{
				return false;
			}
			ElementAssignmentConflictCheck elementAssignmentConflictCheck;
			if (!this.CreateConflictCheck(mapping, assignment, out elementAssignmentConflictCheck))
			{
				Debug.LogError("Rewired Control Mapper: Error creating conflict check!");
				return false;
			}
			List<ElementAssignmentConflictInfo> list = new List<ElementAssignmentConflictInfo>();
			list.AddRange(this.currentPlayer.controllers.conflictChecking.ElementAssignmentConflicts(elementAssignmentConflictCheck));
			list.AddRange(ReInput.players.SystemPlayer.controllers.conflictChecking.ElementAssignmentConflicts(elementAssignmentConflictCheck));
			if (list.Count == 0)
			{
				return false;
			}
			ActionElementMap aem2 = mapping.aem;
			ElementAssignmentConflictInfo elementAssignmentConflictInfo = list[0];
			int actionId = elementAssignmentConflictInfo.elementMap.actionId;
			Pole axisContribution = elementAssignmentConflictInfo.elementMap.axisContribution;
			AxisRange axisRange = aem2.axisRange;
			ControllerElementType elementType = aem2.elementType;
			if (elementType == elementAssignmentConflictInfo.elementMap.elementType && elementType == null)
			{
				if (axisRange != elementAssignmentConflictInfo.elementMap.axisRange)
				{
					if (axisRange == null)
					{
						axisRange = 1;
					}
					else if (elementAssignmentConflictInfo.elementMap.axisRange == null)
					{
					}
				}
			}
			else if (elementType == null && (elementAssignmentConflictInfo.elementMap.elementType == 1 || (elementAssignmentConflictInfo.elementMap.elementType == null && elementAssignmentConflictInfo.elementMap.axisRange != null)) && axisRange == null)
			{
				axisRange = 1;
			}
			int num = 0;
			if (assignment.actionId == elementAssignmentConflictInfo.actionId && mapping.map == elementAssignmentConflictInfo.controllerMap)
			{
				Controller controller = ReInput.controllers.GetController(mapping.controllerType, mapping.controllerId);
				if (this.SwapIsSameInputRange(elementType, axisRange, axisContribution, controller.GetElementById(assignment.elementIdentifierId).type, assignment.axisRange, assignment.axisContribution))
				{
					num++;
				}
			}
			using (IEnumerator<ActionElementMap> enumerator = elementAssignmentConflictInfo.controllerMap.ElementMapsWithAction(actionId).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ActionElementMap aem = enumerator.Current;
					if (aem.id != aem2.id && list.FindIndex((ElementAssignmentConflictInfo x) => x.elementMapId == aem.id) < 0 && this.SwapIsSameInputRange(elementType, axisRange, axisContribution, aem.elementType, aem.axisRange, aem.axisContribution))
					{
						num++;
					}
				}
			}
			return num < this.GetControllerInputFieldCount(mapping.controllerType);
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x000135FD File Offset: 0x000117FD
		private bool SwapIsSameInputRange(ControllerElementType origElementType, AxisRange origAxisRange, Pole origAxisContribution, ControllerElementType conflictElementType, AxisRange conflictAxisRange, Pole conflictAxisContribution)
		{
			return ((origElementType == 1 || (origElementType == null && origAxisRange != null)) && (conflictElementType == 1 || (conflictElementType == null && conflictAxisRange != null)) && conflictAxisContribution == origAxisContribution) || (origElementType == null && origAxisRange == null && conflictElementType == null && conflictAxisRange == null);
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x0001362E File Offset: 0x0001182E
		public static void ApplyTheme(ThemedElement.ElementInfo[] elementInfo)
		{
			if (ControlMapper.Instance == null)
			{
				return;
			}
			if (ControlMapper.Instance._themeSettings == null)
			{
				return;
			}
			if (!ControlMapper.Instance._useThemeSettings)
			{
				return;
			}
			ControlMapper.Instance._themeSettings.Apply(elementInfo);
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x0001366E File Offset: 0x0001186E
		public static LanguageDataBase GetLanguage()
		{
			if (ControlMapper.Instance == null)
			{
				return null;
			}
			return ControlMapper.Instance._language;
		}

		// Token: 0x04001BC0 RID: 7104
		public static bool isActive;

		// Token: 0x04001BC1 RID: 7105
		public const int versionMajor = 1;

		// Token: 0x04001BC2 RID: 7106
		public const int versionMinor = 1;

		// Token: 0x04001BC3 RID: 7107
		public const bool usesTMPro = false;

		// Token: 0x04001BC4 RID: 7108
		private const float blockInputOnFocusTimeout = 0.1f;

		// Token: 0x04001BC5 RID: 7109
		private const string buttonIdentifier_playerSelection = "PlayerSelection";

		// Token: 0x04001BC6 RID: 7110
		private const string buttonIdentifier_removeController = "RemoveController";

		// Token: 0x04001BC7 RID: 7111
		private const string buttonIdentifier_assignController = "AssignController";

		// Token: 0x04001BC8 RID: 7112
		private const string buttonIdentifier_calibrateController = "CalibrateController";

		// Token: 0x04001BC9 RID: 7113
		private const string buttonIdentifier_editInputBehaviors = "EditInputBehaviors";

		// Token: 0x04001BCA RID: 7114
		private const string buttonIdentifier_mapCategorySelection = "MapCategorySelection";

		// Token: 0x04001BCB RID: 7115
		private const string buttonIdentifier_assignedControllerSelection = "AssignedControllerSelection";

		// Token: 0x04001BCC RID: 7116
		private const string buttonIdentifier_done = "Done";

		// Token: 0x04001BCD RID: 7117
		private const string buttonIdentifier_restoreDefaults = "RestoreDefaults";

		// Token: 0x04001BCE RID: 7118
		[SerializeField]
		[Tooltip("Must be assigned a Rewired Input Manager scene object or prefab.")]
		private InputManager _rewiredInputManager;

		// Token: 0x04001BCF RID: 7119
		[SerializeField]
		[Tooltip("Set to True to prevent the Game Object from being destroyed when a new scene is loaded.\n\nNOTE: Changing this value from True to False at runtime will have no effect because Object.DontDestroyOnLoad cannot be undone once set.")]
		private bool _dontDestroyOnLoad;

		// Token: 0x04001BD0 RID: 7120
		[SerializeField]
		[Tooltip("Open the control mapping screen immediately on start. Mainly used for testing.")]
		private bool _openOnStart;

		// Token: 0x04001BD1 RID: 7121
		[SerializeField]
		[Tooltip("The Layout of the Keyboard Maps to be displayed.")]
		private int _keyboardMapDefaultLayout;

		// Token: 0x04001BD2 RID: 7122
		[SerializeField]
		[Tooltip("The Layout of the Mouse Maps to be displayed.")]
		private int _mouseMapDefaultLayout;

		// Token: 0x04001BD3 RID: 7123
		[SerializeField]
		[Tooltip("The Layout of the Mouse Maps to be displayed.")]
		private int _joystickMapDefaultLayout;

		// Token: 0x04001BD4 RID: 7124
		[SerializeField]
		public ControlMapper.MappingSet[] _mappingSets = new ControlMapper.MappingSet[] { ControlMapper.MappingSet.Default };

		// Token: 0x04001BD5 RID: 7125
		[SerializeField]
		[Tooltip("Display a selectable list of Players. If your game only supports 1 player, you can disable this.")]
		private bool _showPlayers = true;

		// Token: 0x04001BD6 RID: 7126
		[SerializeField]
		[Tooltip("Display the Controller column for input mapping.")]
		private bool _showControllers = true;

		// Token: 0x04001BD7 RID: 7127
		[SerializeField]
		[Tooltip("Display the Keyboard column for input mapping.")]
		private bool _showKeyboard = true;

		// Token: 0x04001BD8 RID: 7128
		[SerializeField]
		[Tooltip("Display the Mouse column for input mapping.")]
		private bool _showMouse = true;

		// Token: 0x04001BD9 RID: 7129
		[SerializeField]
		[Tooltip("The maximum number of controllers allowed to be assigned to a Player. If set to any value other than 1, a selectable list of currently-assigned controller will be displayed to the user. [0 = infinite]")]
		private int _maxControllersPerPlayer = 1;

		// Token: 0x04001BDA RID: 7130
		[SerializeField]
		[Tooltip("Display section labels for each Action Category in the input field grid. Only applies if Action Categories are used to display the Action list.")]
		private bool _showActionCategoryLabels;

		// Token: 0x04001BDB RID: 7131
		[SerializeField]
		[Tooltip("The number of input fields to display for the keyboard. If you want to support alternate mappings on the same device, set this to 2 or more.")]
		private int _keyboardInputFieldCount = 2;

		// Token: 0x04001BDC RID: 7132
		[SerializeField]
		[Tooltip("The number of input fields to display for the mouse. If you want to support alternate mappings on the same device, set this to 2 or more.")]
		private int _mouseInputFieldCount = 1;

		// Token: 0x04001BDD RID: 7133
		[SerializeField]
		[Tooltip("The number of input fields to display for joysticks. If you want to support alternate mappings on the same device, set this to 2 or more.")]
		private int _controllerInputFieldCount = 1;

		// Token: 0x04001BDE RID: 7134
		[SerializeField]
		[Tooltip("Display a full-axis input assignment field for every axis-type Action in the input field grid. Also displays an invert toggle for the user  to invert the full-axis assignment direction.\n\n*IMPORTANT*: This field is required if you have made any full-axis assignments in the Rewired Input Manager or in saved XML user data. Disabling this field when you have full-axis assignments will result in the inability for the user to view, remove, or modify these full-axis assignments. In addition, these assignments may cause conflicts when trying to remap the same axes to Actions.")]
		private bool _showFullAxisInputFields = true;

		// Token: 0x04001BDF RID: 7135
		[SerializeField]
		[Tooltip("Display a positive and negative input assignment field for every axis-type Action in the input field grid.\n\n*IMPORTANT*: These fields are required to assign buttons, keyboard keys, and hat or D-Pad directions to axis-type Actions. If you have made any split-axis assignments or button/key/D-pad assignments to axis-type Actions in the Rewired Input Manager or in saved XML user data, disabling these fields will result in the inability for the user to view, remove, or modify these assignments. In addition, these assignments may cause conflicts when trying to remap the same elements to Actions.")]
		private bool _showSplitAxisInputFields = true;

		// Token: 0x04001BE0 RID: 7136
		[SerializeField]
		[Tooltip("If enabled, when an element assignment conflict is found, an option will be displayed that allows the user to make the conflicting assignment anyway.")]
		private bool _allowElementAssignmentConflicts;

		// Token: 0x04001BE1 RID: 7137
		[SerializeField]
		[Tooltip("If enabled, when an element assignment conflict is found, an option will be displayed that allows the user to swap conflicting assignments. This only applies to the first conflicting assignment found. This option will not be displayed if allowElementAssignmentConflicts is true.")]
		private bool _allowElementAssignmentSwap;

		// Token: 0x04001BE2 RID: 7138
		[SerializeField]
		[Tooltip("The width in relative pixels of the Action label column.")]
		private int _actionLabelWidth = 200;

		// Token: 0x04001BE3 RID: 7139
		[SerializeField]
		[Tooltip("The width in relative pixels of the Keyboard column.")]
		private int _keyboardColMaxWidth = 360;

		// Token: 0x04001BE4 RID: 7140
		[SerializeField]
		[Tooltip("The width in relative pixels of the Mouse column.")]
		private int _mouseColMaxWidth = 200;

		// Token: 0x04001BE5 RID: 7141
		[SerializeField]
		[Tooltip("The width in relative pixels of the Controller column.")]
		private int _controllerColMaxWidth = 200;

		// Token: 0x04001BE6 RID: 7142
		[SerializeField]
		[Tooltip("The height in relative pixels of the input grid button rows.")]
		private int _inputRowHeight = 40;

		// Token: 0x04001BE7 RID: 7143
		[SerializeField]
		[Tooltip("The padding of the input grid button rows.")]
		private RectOffset _inputRowPadding = new RectOffset();

		// Token: 0x04001BE8 RID: 7144
		[SerializeField]
		[Tooltip("The width in relative pixels of spacing between input fields in a single column.")]
		private int _inputRowFieldSpacing;

		// Token: 0x04001BE9 RID: 7145
		[SerializeField]
		[Tooltip("The width in relative pixels of spacing between columns.")]
		private int _inputColumnSpacing = 40;

		// Token: 0x04001BEA RID: 7146
		[SerializeField]
		[Tooltip("The height in relative pixels of the space between Action Category sections. Only applicable if Show Action Category Labels is checked.")]
		private int _inputRowCategorySpacing = 20;

		// Token: 0x04001BEB RID: 7147
		[SerializeField]
		[Tooltip("The width in relative pixels of the invert toggle buttons.")]
		private int _invertToggleWidth = 40;

		// Token: 0x04001BEC RID: 7148
		[SerializeField]
		[Tooltip("The width in relative pixels of generated popup windows.")]
		private int _defaultWindowWidth = 500;

		// Token: 0x04001BED RID: 7149
		[SerializeField]
		[Tooltip("The height in relative pixels of generated popup windows.")]
		private int _defaultWindowHeight = 400;

		// Token: 0x04001BEE RID: 7150
		[SerializeField]
		[Tooltip("The time in seconds the user has to press an element on a controller when assigning a controller to a Player. If this time elapses with no user input a controller, the assignment will be canceled.")]
		private float _controllerAssignmentTimeout = 5f;

		// Token: 0x04001BEF RID: 7151
		[SerializeField]
		[Tooltip("The time in seconds the user has to press an element on a controller while waiting for axes to be centered before assigning input.")]
		private float _preInputAssignmentTimeout = 5f;

		// Token: 0x04001BF0 RID: 7152
		[SerializeField]
		[Tooltip("The time in seconds the user has to press an element on a controller when assigning input. If this time elapses with no user input on the target controller, the assignment will be canceled.")]
		private float _inputAssignmentTimeout = 5f;

		// Token: 0x04001BF1 RID: 7153
		[SerializeField]
		[Tooltip("The time in seconds the user has to press an element on a controller during calibration.")]
		private float _axisCalibrationTimeout = 5f;

		// Token: 0x04001BF2 RID: 7154
		[SerializeField]
		[Tooltip("If checked, mouse X-axis movement will always be ignored during input assignment. Check this if you don't want the horizontal mouse axis to be user-assignable to any Actions.")]
		private bool _ignoreMouseXAxisAssignment = true;

		// Token: 0x04001BF3 RID: 7155
		[SerializeField]
		[Tooltip("If checked, mouse Y-axis movement will always be ignored during input assignment. Check this if you don't want the vertical mouse axis to be user-assignable to any Actions.")]
		private bool _ignoreMouseYAxisAssignment = true;

		// Token: 0x04001BF4 RID: 7156
		[SerializeField]
		[Tooltip("An Action that when activated will alternately close or open the main screen as long as no popup windows are open.")]
		private int _screenToggleAction = -1;

		// Token: 0x04001BF5 RID: 7157
		[SerializeField]
		[Tooltip("An Action that when activated will open the main screen if it is closed.")]
		private int _screenOpenAction = -1;

		// Token: 0x04001BF6 RID: 7158
		[SerializeField]
		[Tooltip("An Action that when activated will close the main screen as long as no popup windows are open.")]
		private int _screenCloseAction = -1;

		// Token: 0x04001BF7 RID: 7159
		[SerializeField]
		[Tooltip("An Action that when activated will cancel and close any open popup window. Use with care because the element assigned to this Action can never be mapped by the user (because it would just cancel his assignment).")]
		private int _universalCancelAction = -1;

		// Token: 0x04001BF8 RID: 7160
		[SerializeField]
		[Tooltip("If enabled, Universal Cancel will also close the main screen if pressed when no windows are open.")]
		private bool _universalCancelClosesScreen = true;

		// Token: 0x04001BF9 RID: 7161
		[SerializeField]
		[Tooltip("If checked, controls will be displayed which will allow the user to customize certain Input Behavior settings.")]
		private bool _showInputBehaviorSettings;

		// Token: 0x04001BFA RID: 7162
		[SerializeField]
		[Tooltip("Customizable settings for user-modifiable Input Behaviors. This can be used for settings like Mouse Look Sensitivity.")]
		private ControlMapper.InputBehaviorSettings[] _inputBehaviorSettings;

		// Token: 0x04001BFB RID: 7163
		[SerializeField]
		[Tooltip("If enabled, UI elements will be themed based on the settings in Theme Settings.")]
		private bool _useThemeSettings = true;

		// Token: 0x04001BFC RID: 7164
		[SerializeField]
		[Tooltip("Must be assigned a ThemeSettings object. Used to theme UI elements.")]
		private ThemeSettings _themeSettings;

		// Token: 0x04001BFD RID: 7165
		[SerializeField]
		[Tooltip("Must be assigned a LanguageData object. Used to retrieve language entries for UI elements.")]
		private LanguageDataBase _language;

		// Token: 0x04001BFE RID: 7166
		[SerializeField]
		private UIButtonDisplaySettings _buttonDisplaySettings;

		// Token: 0x04001BFF RID: 7167
		[SerializeField]
		[Tooltip("A list of prefabs. You should not have to modify this.")]
		private ControlMapper.Prefabs prefabs;

		// Token: 0x04001C00 RID: 7168
		[SerializeField]
		[Tooltip("A list of references to elements in the hierarchy. You should not have to modify this.")]
		private ControlMapper.References references;

		// Token: 0x04001C01 RID: 7169
		[SerializeField]
		[Tooltip("Show the label for the Players button group?")]
		private bool _showPlayersGroupLabel = true;

		// Token: 0x04001C02 RID: 7170
		[SerializeField]
		[Tooltip("Show the label for the Controller button group?")]
		private bool _showControllerGroupLabel = true;

		// Token: 0x04001C03 RID: 7171
		[SerializeField]
		[Tooltip("Show the label for the Assigned Controllers button group?")]
		private bool _showAssignedControllersGroupLabel = true;

		// Token: 0x04001C04 RID: 7172
		[SerializeField]
		[Tooltip("Show the label for the Settings button group?")]
		private bool _showSettingsGroupLabel = true;

		// Token: 0x04001C05 RID: 7173
		[SerializeField]
		[Tooltip("Show the label for the Map Categories button group?")]
		private bool _showMapCategoriesGroupLabel = true;

		// Token: 0x04001C06 RID: 7174
		[SerializeField]
		[Tooltip("Show the label for the current controller name?")]
		private bool _showControllerNameLabel = true;

		// Token: 0x04001C07 RID: 7175
		[SerializeField]
		[Tooltip("Show the Assigned Controllers group? If joystick auto-assignment is enabled in the Rewired Input Manager and the max joysticks per player is set to any value other than 1, the Assigned Controllers group will always be displayed.")]
		private bool _showAssignedControllers = true;

		// Token: 0x04001C08 RID: 7176
		private Action _ScreenClosedEvent;

		// Token: 0x04001C09 RID: 7177
		private Action _ScreenOpenedEvent;

		// Token: 0x04001C0A RID: 7178
		private Action _PopupWindowOpenedEvent;

		// Token: 0x04001C0B RID: 7179
		private Action _PopupWindowClosedEvent;

		// Token: 0x04001C0C RID: 7180
		private Action _InputPollingStartedEvent;

		// Token: 0x04001C0D RID: 7181
		private Action _InputPollingEndedEvent;

		// Token: 0x04001C0E RID: 7182
		[SerializeField]
		[Tooltip("Event sent when the UI is closed.")]
		private UnityEvent _onScreenClosed;

		// Token: 0x04001C0F RID: 7183
		[SerializeField]
		[Tooltip("Event sent when the UI is opened.")]
		private UnityEvent _onScreenOpened;

		// Token: 0x04001C10 RID: 7184
		[SerializeField]
		[Tooltip("Event sent when a popup window is closed.")]
		private UnityEvent _onPopupWindowClosed;

		// Token: 0x04001C11 RID: 7185
		[SerializeField]
		[Tooltip("Event sent when a popup window is opened.")]
		private UnityEvent _onPopupWindowOpened;

		// Token: 0x04001C12 RID: 7186
		[SerializeField]
		[Tooltip("Event sent when polling for input has started.")]
		private UnityEvent _onInputPollingStarted;

		// Token: 0x04001C13 RID: 7187
		[SerializeField]
		[Tooltip("Event sent when polling for input has ended.")]
		private UnityEvent _onInputPollingEnded;

		// Token: 0x04001C14 RID: 7188
		private static ControlMapper Instance;

		// Token: 0x04001C15 RID: 7189
		private bool initialized;

		// Token: 0x04001C16 RID: 7190
		private int playerCount;

		// Token: 0x04001C17 RID: 7191
		private ControlMapper.InputGrid inputGrid;

		// Token: 0x04001C18 RID: 7192
		private ControlMapper.WindowManager windowManager;

		// Token: 0x04001C19 RID: 7193
		private int currentPlayerId;

		// Token: 0x04001C1A RID: 7194
		private int currentMapCategoryId;

		// Token: 0x04001C1B RID: 7195
		private List<ControlMapper.GUIButton> playerButtons;

		// Token: 0x04001C1C RID: 7196
		private List<ControlMapper.GUIButton> mapCategoryButtons;

		// Token: 0x04001C1D RID: 7197
		private List<ControlMapper.GUIButton> assignedControllerButtons;

		// Token: 0x04001C1E RID: 7198
		private ControlMapper.GUIButton assignedControllerButtonsPlaceholder;

		// Token: 0x04001C1F RID: 7199
		private List<GameObject> miscInstantiatedObjects;

		// Token: 0x04001C20 RID: 7200
		private GameObject canvas;

		// Token: 0x04001C21 RID: 7201
		private GameObject lastUISelection;

		// Token: 0x04001C22 RID: 7202
		private int currentJoystickId = -1;

		// Token: 0x04001C23 RID: 7203
		private float blockInputOnFocusEndTime;

		// Token: 0x04001C24 RID: 7204
		private bool isPollingForInput;

		// Token: 0x04001C25 RID: 7205
		private ControlMapper.InputMapping pendingInputMapping;

		// Token: 0x04001C26 RID: 7206
		private ControlMapper.AxisCalibrator pendingAxisCalibration;

		// Token: 0x04001C27 RID: 7207
		private Action<InputFieldInfo> inputFieldActivatedDelegate;

		// Token: 0x04001C28 RID: 7208
		private Action<ToggleInfo, bool> inputFieldInvertToggleStateChangedDelegate;

		// Token: 0x04001C29 RID: 7209
		private Action _restoreDefaultsDelegate;

		// Token: 0x04001C2A RID: 7210
		private bool isPreInitialized;

		// Token: 0x02000432 RID: 1074
		private abstract class GUIElement
		{
			// Token: 0x1700049A RID: 1178
			// (get) Token: 0x060018EF RID: 6383 RVA: 0x00013689 File Offset: 0x00011889
			// (set) Token: 0x060018F0 RID: 6384 RVA: 0x00013691 File Offset: 0x00011891
			public RectTransform rectTransform { get; private set; }

			// Token: 0x060018F1 RID: 6385 RVA: 0x0006A52C File Offset: 0x0006872C
			public GUIElement(GameObject gameObject)
			{
				if (gameObject == null)
				{
					Debug.LogError("Rewired Control Mapper: gameObject is null!");
					return;
				}
				this.selectable = gameObject.GetComponent<Selectable>();
				if (this.selectable == null)
				{
					Debug.LogError("Rewired Control Mapper: Selectable is null!");
					return;
				}
				this.gameObject = gameObject;
				this.rectTransform = gameObject.GetComponent<RectTransform>();
				this.text = UnityTools.GetComponentInSelfOrChildren<Text>(gameObject);
				this.uiElementInfo = gameObject.GetComponent<UIElementInfo>();
				this.children = new List<ControlMapper.GUIElement>();
			}

			// Token: 0x060018F2 RID: 6386 RVA: 0x0006A5B0 File Offset: 0x000687B0
			public GUIElement(Selectable selectable, Text label)
			{
				if (selectable == null)
				{
					Debug.LogError("Rewired Control Mapper: Selectable is null!");
					return;
				}
				this.selectable = selectable;
				this.gameObject = selectable.gameObject;
				this.rectTransform = this.gameObject.GetComponent<RectTransform>();
				this.text = label;
				this.uiElementInfo = this.gameObject.GetComponent<UIElementInfo>();
				this.children = new List<ControlMapper.GUIElement>();
			}

			// Token: 0x060018F3 RID: 6387 RVA: 0x0001369A File Offset: 0x0001189A
			public virtual void SetInteractible(bool state, bool playTransition)
			{
				this.SetInteractible(state, playTransition, false);
			}

			// Token: 0x060018F4 RID: 6388 RVA: 0x0006A620 File Offset: 0x00068820
			public virtual void SetInteractible(bool state, bool playTransition, bool permanent)
			{
				for (int i = 0; i < this.children.Count; i++)
				{
					if (this.children[i] != null)
					{
						this.children[i].SetInteractible(state, playTransition, permanent);
					}
				}
				if (this.permanentStateSet)
				{
					return;
				}
				if (this.selectable == null)
				{
					return;
				}
				if (permanent)
				{
					this.permanentStateSet = true;
				}
				if (this.selectable.interactable == state)
				{
					return;
				}
				UITools.SetInteractable(this.selectable, state, playTransition);
			}

			// Token: 0x060018F5 RID: 6389 RVA: 0x0006A6A4 File Offset: 0x000688A4
			public virtual void SetTextWidth(int value)
			{
				if (this.text == null)
				{
					return;
				}
				LayoutElement layoutElement = this.text.GetComponent<LayoutElement>();
				if (layoutElement == null)
				{
					layoutElement = this.text.gameObject.AddComponent<LayoutElement>();
				}
				layoutElement.preferredWidth = (float)value;
			}

			// Token: 0x060018F6 RID: 6390 RVA: 0x0006A6F0 File Offset: 0x000688F0
			public virtual void SetFirstChildObjectWidth(ControlMapper.LayoutElementSizeType type, int value)
			{
				if (this.rectTransform.childCount == 0)
				{
					return;
				}
				Transform child = this.rectTransform.GetChild(0);
				LayoutElement layoutElement = child.GetComponent<LayoutElement>();
				if (layoutElement == null)
				{
					layoutElement = child.gameObject.AddComponent<LayoutElement>();
				}
				if (type == ControlMapper.LayoutElementSizeType.MinSize)
				{
					layoutElement.minWidth = (float)value;
					return;
				}
				if (type == ControlMapper.LayoutElementSizeType.PreferredSize)
				{
					layoutElement.preferredWidth = (float)value;
					return;
				}
				throw new NotImplementedException();
			}

			// Token: 0x060018F7 RID: 6391 RVA: 0x000136A5 File Offset: 0x000118A5
			public virtual void SetLabel(string label)
			{
				if (this.text == null)
				{
					return;
				}
				this.text.text = label;
			}

			// Token: 0x060018F8 RID: 6392 RVA: 0x000136C2 File Offset: 0x000118C2
			public virtual string GetLabel()
			{
				if (this.text == null)
				{
					return string.Empty;
				}
				return this.text.text;
			}

			// Token: 0x060018F9 RID: 6393 RVA: 0x000136E3 File Offset: 0x000118E3
			public virtual void AddChild(ControlMapper.GUIElement child)
			{
				this.children.Add(child);
			}

			// Token: 0x060018FA RID: 6394 RVA: 0x000136F1 File Offset: 0x000118F1
			public void SetElementInfoData(string identifier, int intData)
			{
				if (this.uiElementInfo == null)
				{
					return;
				}
				this.uiElementInfo.identifier = identifier;
				this.uiElementInfo.intData = intData;
			}

			// Token: 0x060018FB RID: 6395 RVA: 0x0001371A File Offset: 0x0001191A
			public virtual void SetActive(bool state)
			{
				if (this.gameObject == null)
				{
					return;
				}
				this.gameObject.SetActive(state);
			}

			// Token: 0x060018FC RID: 6396 RVA: 0x0006A754 File Offset: 0x00068954
			protected virtual bool Init()
			{
				bool flag = true;
				for (int i = 0; i < this.children.Count; i++)
				{
					if (this.children[i] != null && !this.children[i].Init())
					{
						flag = false;
					}
				}
				if (this.selectable == null)
				{
					Debug.LogError("Rewired Control Mapper: UI Element is missing Selectable component!");
					flag = false;
				}
				if (this.rectTransform == null)
				{
					Debug.LogError("Rewired Control Mapper: UI Element is missing RectTransform component!");
					flag = false;
				}
				if (this.uiElementInfo == null)
				{
					Debug.LogError("Rewired Control Mapper: UI Element is missing UIElementInfo component!");
					flag = false;
				}
				return flag;
			}

			// Token: 0x04001C2B RID: 7211
			public readonly GameObject gameObject;

			// Token: 0x04001C2C RID: 7212
			protected readonly Text text;

			// Token: 0x04001C2D RID: 7213
			public readonly Selectable selectable;

			// Token: 0x04001C2E RID: 7214
			protected readonly UIElementInfo uiElementInfo;

			// Token: 0x04001C2F RID: 7215
			protected bool permanentStateSet;

			// Token: 0x04001C30 RID: 7216
			protected readonly List<ControlMapper.GUIElement> children;
		}

		// Token: 0x02000433 RID: 1075
		private class GUIButton : ControlMapper.GUIElement
		{
			// Token: 0x1700049B RID: 1179
			// (get) Token: 0x060018FD RID: 6397 RVA: 0x00013737 File Offset: 0x00011937
			protected Button button
			{
				get
				{
					return this.selectable as Button;
				}
			}

			// Token: 0x1700049C RID: 1180
			// (get) Token: 0x060018FE RID: 6398 RVA: 0x00013744 File Offset: 0x00011944
			public ButtonInfo buttonInfo
			{
				get
				{
					return this.uiElementInfo as ButtonInfo;
				}
			}

			// Token: 0x060018FF RID: 6399 RVA: 0x00013751 File Offset: 0x00011951
			public GUIButton(GameObject gameObject)
				: base(gameObject)
			{
				this.Init();
			}

			// Token: 0x06001900 RID: 6400 RVA: 0x00013761 File Offset: 0x00011961
			public GUIButton(Button button, Text label)
				: base(button, label)
			{
				this.Init();
			}

			// Token: 0x06001901 RID: 6401 RVA: 0x00013772 File Offset: 0x00011972
			public void SetButtonInfoData(string identifier, int intData)
			{
				base.SetElementInfoData(identifier, intData);
			}

			// Token: 0x06001902 RID: 6402 RVA: 0x0006A7EC File Offset: 0x000689EC
			public void SetOnClickCallback(Action<ButtonInfo> callback)
			{
				if (this.button == null)
				{
					return;
				}
				this.button.onClick.AddListener(delegate
				{
					callback(this.buttonInfo);
				});
			}
		}

		// Token: 0x02000435 RID: 1077
		private class GUIInputField : ControlMapper.GUIElement
		{
			// Token: 0x1700049D RID: 1181
			// (get) Token: 0x06001905 RID: 6405 RVA: 0x00013737 File Offset: 0x00011937
			protected Button button
			{
				get
				{
					return this.selectable as Button;
				}
			}

			// Token: 0x1700049E RID: 1182
			// (get) Token: 0x06001906 RID: 6406 RVA: 0x00013794 File Offset: 0x00011994
			public InputFieldInfo fieldInfo
			{
				get
				{
					return this.uiElementInfo as InputFieldInfo;
				}
			}

			// Token: 0x1700049F RID: 1183
			// (get) Token: 0x06001907 RID: 6407 RVA: 0x000137A1 File Offset: 0x000119A1
			public bool hasToggle
			{
				get
				{
					return this.toggle != null;
				}
			}

			// Token: 0x170004A0 RID: 1184
			// (get) Token: 0x06001908 RID: 6408 RVA: 0x000137AC File Offset: 0x000119AC
			// (set) Token: 0x06001909 RID: 6409 RVA: 0x000137B4 File Offset: 0x000119B4
			public ControlMapper.GUIToggle toggle { get; private set; }

			// Token: 0x170004A1 RID: 1185
			// (get) Token: 0x0600190A RID: 6410 RVA: 0x000137BD File Offset: 0x000119BD
			// (set) Token: 0x0600190B RID: 6411 RVA: 0x000137DA File Offset: 0x000119DA
			public int actionElementMapId
			{
				get
				{
					if (this.fieldInfo == null)
					{
						return -1;
					}
					return this.fieldInfo.actionElementMapId;
				}
				set
				{
					if (this.fieldInfo == null)
					{
						return;
					}
					this.fieldInfo.actionElementMapId = value;
				}
			}

			// Token: 0x170004A2 RID: 1186
			// (get) Token: 0x0600190C RID: 6412 RVA: 0x000137F7 File Offset: 0x000119F7
			// (set) Token: 0x0600190D RID: 6413 RVA: 0x00013814 File Offset: 0x00011A14
			public int controllerId
			{
				get
				{
					if (this.fieldInfo == null)
					{
						return -1;
					}
					return this.fieldInfo.controllerId;
				}
				set
				{
					if (this.fieldInfo == null)
					{
						return;
					}
					this.fieldInfo.controllerId = value;
				}
			}

			// Token: 0x0600190E RID: 6414 RVA: 0x00013751 File Offset: 0x00011951
			public GUIInputField(GameObject gameObject)
				: base(gameObject)
			{
				this.Init();
			}

			// Token: 0x0600190F RID: 6415 RVA: 0x00013761 File Offset: 0x00011961
			public GUIInputField(Button button, Text label)
				: base(button, label)
			{
				this.Init();
			}

			// Token: 0x06001910 RID: 6416 RVA: 0x0006A838 File Offset: 0x00068A38
			public void SetFieldInfoData(int actionId, AxisRange axisRange, ControllerType controllerType, int intData)
			{
				base.SetElementInfoData(string.Empty, intData);
				if (this.fieldInfo == null)
				{
					return;
				}
				this.fieldInfo.actionId = actionId;
				this.fieldInfo.axisRange = axisRange;
				this.fieldInfo.controllerType = controllerType;
			}

			// Token: 0x06001911 RID: 6417 RVA: 0x0006A888 File Offset: 0x00068A88
			public void SetOnClickCallback(Action<InputFieldInfo> callback)
			{
				if (this.button == null)
				{
					return;
				}
				this.button.onClick.AddListener(delegate
				{
					callback(this.fieldInfo);
				});
			}

			// Token: 0x06001912 RID: 6418 RVA: 0x00013831 File Offset: 0x00011A31
			public virtual void SetInteractable(bool state, bool playTransition, bool permanent)
			{
				if (this.permanentStateSet)
				{
					return;
				}
				if (this.hasToggle && !state)
				{
					this.toggle.SetInteractible(state, playTransition, permanent);
				}
				base.SetInteractible(state, playTransition, permanent);
			}

			// Token: 0x06001913 RID: 6419 RVA: 0x0001385E File Offset: 0x00011A5E
			public void AddToggle(ControlMapper.GUIToggle toggle)
			{
				if (toggle == null)
				{
					return;
				}
				this.toggle = toggle;
			}

			// Token: 0x06001914 RID: 6420 RVA: 0x0006A8D4 File Offset: 0x00068AD4
			public void SetDisplay(GameObject buttonDisplay)
			{
				if (this.buttonDisplay != null)
				{
					Object.Destroy(this.buttonDisplay);
				}
				if (buttonDisplay == null)
				{
					return;
				}
				this.buttonDisplay = buttonDisplay;
				buttonDisplay.GetOrAddComponent<LayoutElement>().ignoreLayout = true;
				buttonDisplay.transform.SetParent(this.gameObject.transform, false);
				ButtonDisplaySettings component = buttonDisplay.GetComponent<ButtonDisplaySettings>();
				buttonDisplay.transform.localPosition = 0.5f * component.positionOffset;
				buttonDisplay.transform.localRotation = Quaternion.identity;
				buttonDisplay.transform.localScale = 0.5f * Vector3.one;
				buttonDisplay.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			}

			// Token: 0x06001915 RID: 6421 RVA: 0x0006A990 File Offset: 0x00068B90
			protected override bool Init()
			{
				if (this.text != null)
				{
					this.text.enabled = false;
				}
				this.buttonDisplay = new GameObject();
				this.buttonDisplay.transform.SetParent(this.gameObject.transform);
				return base.Init();
			}

			// Token: 0x04001C35 RID: 7221
			public GameObject buttonDisplay;
		}

		// Token: 0x02000437 RID: 1079
		private class GUIToggle : ControlMapper.GUIElement
		{
			// Token: 0x170004A3 RID: 1187
			// (get) Token: 0x06001918 RID: 6424 RVA: 0x00013883 File Offset: 0x00011A83
			protected Toggle toggle
			{
				get
				{
					return this.selectable as Toggle;
				}
			}

			// Token: 0x170004A4 RID: 1188
			// (get) Token: 0x06001919 RID: 6425 RVA: 0x00013890 File Offset: 0x00011A90
			public ToggleInfo toggleInfo
			{
				get
				{
					return this.uiElementInfo as ToggleInfo;
				}
			}

			// Token: 0x170004A5 RID: 1189
			// (get) Token: 0x0600191A RID: 6426 RVA: 0x0001389D File Offset: 0x00011A9D
			// (set) Token: 0x0600191B RID: 6427 RVA: 0x000138BA File Offset: 0x00011ABA
			public int actionElementMapId
			{
				get
				{
					if (this.toggleInfo == null)
					{
						return -1;
					}
					return this.toggleInfo.actionElementMapId;
				}
				set
				{
					if (this.toggleInfo == null)
					{
						return;
					}
					this.toggleInfo.actionElementMapId = value;
				}
			}

			// Token: 0x0600191C RID: 6428 RVA: 0x00013751 File Offset: 0x00011951
			public GUIToggle(GameObject gameObject)
				: base(gameObject)
			{
				this.Init();
			}

			// Token: 0x0600191D RID: 6429 RVA: 0x00013761 File Offset: 0x00011961
			public GUIToggle(Toggle toggle, Text label)
				: base(toggle, label)
			{
				this.Init();
			}

			// Token: 0x0600191E RID: 6430 RVA: 0x0006A9E4 File Offset: 0x00068BE4
			public void SetToggleInfoData(int actionId, AxisRange axisRange, ControllerType controllerType, int intData)
			{
				base.SetElementInfoData(string.Empty, intData);
				if (this.toggleInfo == null)
				{
					return;
				}
				this.toggleInfo.actionId = actionId;
				this.toggleInfo.axisRange = axisRange;
				this.toggleInfo.controllerType = controllerType;
			}

			// Token: 0x0600191F RID: 6431 RVA: 0x0006AA34 File Offset: 0x00068C34
			public void SetOnSubmitCallback(Action<ToggleInfo, bool> callback)
			{
				if (this.toggle == null)
				{
					return;
				}
				EventTrigger eventTrigger = this.toggle.GetComponent<EventTrigger>();
				if (eventTrigger == null)
				{
					eventTrigger = this.toggle.gameObject.AddComponent<EventTrigger>();
				}
				EventTrigger.TriggerEvent triggerEvent = new EventTrigger.TriggerEvent();
				triggerEvent.AddListener(delegate(BaseEventData data)
				{
					PointerEventData pointerEventData = data as PointerEventData;
					if (pointerEventData != null && pointerEventData.button != null)
					{
						return;
					}
					callback(this.toggleInfo, this.toggle.isOn);
				});
				EventTrigger.Entry entry = new EventTrigger.Entry
				{
					callback = triggerEvent,
					eventID = 15
				};
				EventTrigger.Entry entry2 = new EventTrigger.Entry
				{
					callback = triggerEvent,
					eventID = 4
				};
				if (eventTrigger.triggers != null)
				{
					eventTrigger.triggers.Clear();
				}
				else
				{
					eventTrigger.triggers = new List<EventTrigger.Entry>();
				}
				eventTrigger.triggers.Add(entry);
				eventTrigger.triggers.Add(entry2);
			}

			// Token: 0x06001920 RID: 6432 RVA: 0x000138D7 File Offset: 0x00011AD7
			public void SetToggleState(bool state)
			{
				if (this.toggle == null)
				{
					return;
				}
				this.toggle.isOn = state;
			}
		}

		// Token: 0x02000439 RID: 1081
		private class GUILabel
		{
			// Token: 0x170004A6 RID: 1190
			// (get) Token: 0x06001923 RID: 6435 RVA: 0x000138F4 File Offset: 0x00011AF4
			// (set) Token: 0x06001924 RID: 6436 RVA: 0x000138FC File Offset: 0x00011AFC
			public GameObject gameObject { get; private set; }

			// Token: 0x170004A7 RID: 1191
			// (get) Token: 0x06001925 RID: 6437 RVA: 0x00013905 File Offset: 0x00011B05
			// (set) Token: 0x06001926 RID: 6438 RVA: 0x0001390D File Offset: 0x00011B0D
			private Text text { get; set; }

			// Token: 0x170004A8 RID: 1192
			// (get) Token: 0x06001927 RID: 6439 RVA: 0x00013916 File Offset: 0x00011B16
			// (set) Token: 0x06001928 RID: 6440 RVA: 0x0001391E File Offset: 0x00011B1E
			public RectTransform rectTransform { get; private set; }

			// Token: 0x06001929 RID: 6441 RVA: 0x00013927 File Offset: 0x00011B27
			public GUILabel(GameObject gameObject)
			{
				if (gameObject == null)
				{
					Debug.LogError("Rewired Control Mapper: gameObject is null!");
					return;
				}
				this.text = UnityTools.GetComponentInSelfOrChildren<Text>(gameObject);
				this.Check();
			}

			// Token: 0x0600192A RID: 6442 RVA: 0x00013956 File Offset: 0x00011B56
			public GUILabel(Text label)
			{
				this.text = label;
				this.Check();
			}

			// Token: 0x0600192B RID: 6443 RVA: 0x0001396C File Offset: 0x00011B6C
			public void SetSize(int width, int height)
			{
				if (this.text == null)
				{
					return;
				}
				this.rectTransform.SetSizeWithCurrentAnchors(0, (float)width);
				this.rectTransform.SetSizeWithCurrentAnchors(1, (float)height);
			}

			// Token: 0x0600192C RID: 6444 RVA: 0x00013999 File Offset: 0x00011B99
			public void SetWidth(int width)
			{
				if (this.text == null)
				{
					return;
				}
				this.rectTransform.SetSizeWithCurrentAnchors(0, (float)width);
			}

			// Token: 0x0600192D RID: 6445 RVA: 0x000139B8 File Offset: 0x00011BB8
			public void SetHeight(int height)
			{
				if (this.text == null)
				{
					return;
				}
				this.rectTransform.SetSizeWithCurrentAnchors(1, (float)height);
			}

			// Token: 0x0600192E RID: 6446 RVA: 0x000139D7 File Offset: 0x00011BD7
			public void SetLabel(string label)
			{
				if (this.text == null)
				{
					return;
				}
				this.text.text = label;
			}

			// Token: 0x0600192F RID: 6447 RVA: 0x000139F4 File Offset: 0x00011BF4
			public void SetFontStyle(FontStyle style)
			{
				if (this.text == null)
				{
					return;
				}
				this.text.fontStyle = style;
			}

			// Token: 0x06001930 RID: 6448 RVA: 0x00013A11 File Offset: 0x00011C11
			public void SetTextAlignment(TextAnchor alignment)
			{
				if (this.text == null)
				{
					return;
				}
				this.text.alignment = alignment;
			}

			// Token: 0x06001931 RID: 6449 RVA: 0x00013A2E File Offset: 0x00011C2E
			public void SetActive(bool state)
			{
				if (this.gameObject == null)
				{
					return;
				}
				this.gameObject.SetActive(state);
			}

			// Token: 0x06001932 RID: 6450 RVA: 0x0006AB50 File Offset: 0x00068D50
			private bool Check()
			{
				bool flag = true;
				if (this.text == null)
				{
					Debug.LogError("Rewired Control Mapper: Button is missing Text child component!");
					flag = false;
				}
				this.gameObject = this.text.gameObject;
				this.rectTransform = this.text.GetComponent<RectTransform>();
				return flag;
			}
		}

		// Token: 0x0200043A RID: 1082
		[Serializable]
		public class MappingSet
		{
			// Token: 0x170004A9 RID: 1193
			// (get) Token: 0x06001933 RID: 6451 RVA: 0x00013A4B File Offset: 0x00011C4B
			public int mapCategoryId
			{
				get
				{
					return this._mapCategoryId;
				}
			}

			// Token: 0x170004AA RID: 1194
			// (get) Token: 0x06001934 RID: 6452 RVA: 0x00013A53 File Offset: 0x00011C53
			public ControlMapper.MappingSet.ActionListMode actionListMode
			{
				get
				{
					return this._actionListMode;
				}
			}

			// Token: 0x170004AB RID: 1195
			// (get) Token: 0x06001935 RID: 6453 RVA: 0x00013A5B File Offset: 0x00011C5B
			public IList<int> actionCategoryIds
			{
				get
				{
					if (this._actionCategoryIds == null)
					{
						return null;
					}
					if (this._actionCategoryIdsReadOnly == null)
					{
						this._actionCategoryIdsReadOnly = new ReadOnlyCollection<int>(this._actionCategoryIds);
					}
					return this._actionCategoryIdsReadOnly;
				}
			}

			// Token: 0x170004AC RID: 1196
			// (get) Token: 0x06001936 RID: 6454 RVA: 0x00013A86 File Offset: 0x00011C86
			public IList<int> actionIds
			{
				get
				{
					if (this._actionIds == null)
					{
						return null;
					}
					if (this._actionIdsReadOnly == null)
					{
						this._actionIdsReadOnly = new ReadOnlyCollection<int>(this._actionIds);
					}
					return this._actionIds;
				}
			}

			// Token: 0x170004AD RID: 1197
			// (get) Token: 0x06001937 RID: 6455 RVA: 0x00013AB1 File Offset: 0x00011CB1
			public bool isValid
			{
				get
				{
					return this._mapCategoryId >= 0 && ReInput.mapping.GetMapCategory(this._mapCategoryId) != null;
				}
			}

			// Token: 0x06001938 RID: 6456 RVA: 0x00013AD1 File Offset: 0x00011CD1
			public MappingSet()
			{
				this._mapCategoryId = -1;
				this._actionCategoryIds = new int[0];
				this._actionIds = new int[0];
				this._actionListMode = ControlMapper.MappingSet.ActionListMode.ActionCategory;
			}

			// Token: 0x06001939 RID: 6457 RVA: 0x00013AFF File Offset: 0x00011CFF
			private MappingSet(int mapCategoryId, ControlMapper.MappingSet.ActionListMode actionListMode, int[] actionCategoryIds, int[] actionIds)
			{
				this._mapCategoryId = mapCategoryId;
				this._actionListMode = actionListMode;
				this._actionCategoryIds = actionCategoryIds;
				this._actionIds = actionIds;
			}

			// Token: 0x170004AE RID: 1198
			// (get) Token: 0x0600193A RID: 6458 RVA: 0x00013B24 File Offset: 0x00011D24
			public static ControlMapper.MappingSet Default
			{
				get
				{
					return new ControlMapper.MappingSet(0, ControlMapper.MappingSet.ActionListMode.ActionCategory, new int[1], new int[0]);
				}
			}

			// Token: 0x04001C3D RID: 7229
			[SerializeField]
			[Tooltip("The Map Category that will be displayed to the user for remapping.")]
			private int _mapCategoryId;

			// Token: 0x04001C3E RID: 7230
			[SerializeField]
			[Tooltip("Choose whether you want to list Actions to display for this Map Category by individual Action or by all the Actions in an Action Category.")]
			private ControlMapper.MappingSet.ActionListMode _actionListMode;

			// Token: 0x04001C3F RID: 7231
			[SerializeField]
			private int[] _actionCategoryIds;

			// Token: 0x04001C40 RID: 7232
			[SerializeField]
			private int[] _actionIds;

			// Token: 0x04001C41 RID: 7233
			private IList<int> _actionCategoryIdsReadOnly;

			// Token: 0x04001C42 RID: 7234
			private IList<int> _actionIdsReadOnly;

			// Token: 0x0200043B RID: 1083
			public enum ActionListMode
			{
				// Token: 0x04001C44 RID: 7236
				ActionCategory,
				// Token: 0x04001C45 RID: 7237
				Action
			}
		}

		// Token: 0x0200043C RID: 1084
		[Serializable]
		public class InputBehaviorSettings
		{
			// Token: 0x170004AF RID: 1199
			// (get) Token: 0x0600193B RID: 6459 RVA: 0x00013B39 File Offset: 0x00011D39
			public int inputBehaviorId
			{
				get
				{
					return this._inputBehaviorId;
				}
			}

			// Token: 0x170004B0 RID: 1200
			// (get) Token: 0x0600193C RID: 6460 RVA: 0x00013B41 File Offset: 0x00011D41
			public bool showJoystickAxisSensitivity
			{
				get
				{
					return this._showJoystickAxisSensitivity;
				}
			}

			// Token: 0x170004B1 RID: 1201
			// (get) Token: 0x0600193D RID: 6461 RVA: 0x00013B49 File Offset: 0x00011D49
			public bool showMouseXYAxisSensitivity
			{
				get
				{
					return this._showMouseXYAxisSensitivity;
				}
			}

			// Token: 0x170004B2 RID: 1202
			// (get) Token: 0x0600193E RID: 6462 RVA: 0x00013B51 File Offset: 0x00011D51
			public string labelLanguageKey
			{
				get
				{
					return this._labelLanguageKey;
				}
			}

			// Token: 0x170004B3 RID: 1203
			// (get) Token: 0x0600193F RID: 6463 RVA: 0x00013B59 File Offset: 0x00011D59
			public string joystickAxisSensitivityLabelLanguageKey
			{
				get
				{
					return this._joystickAxisSensitivityLabelLanguageKey;
				}
			}

			// Token: 0x170004B4 RID: 1204
			// (get) Token: 0x06001940 RID: 6464 RVA: 0x00013B61 File Offset: 0x00011D61
			public string mouseXYAxisSensitivityLabelLanguageKey
			{
				get
				{
					return this._mouseXYAxisSensitivityLabelLanguageKey;
				}
			}

			// Token: 0x170004B5 RID: 1205
			// (get) Token: 0x06001941 RID: 6465 RVA: 0x00013B69 File Offset: 0x00011D69
			public Sprite joystickAxisSensitivityIcon
			{
				get
				{
					return this._joystickAxisSensitivityIcon;
				}
			}

			// Token: 0x170004B6 RID: 1206
			// (get) Token: 0x06001942 RID: 6466 RVA: 0x00013B71 File Offset: 0x00011D71
			public Sprite mouseXYAxisSensitivityIcon
			{
				get
				{
					return this._mouseXYAxisSensitivityIcon;
				}
			}

			// Token: 0x170004B7 RID: 1207
			// (get) Token: 0x06001943 RID: 6467 RVA: 0x00013B79 File Offset: 0x00011D79
			public float joystickAxisSensitivityMin
			{
				get
				{
					return this._joystickAxisSensitivityMin;
				}
			}

			// Token: 0x170004B8 RID: 1208
			// (get) Token: 0x06001944 RID: 6468 RVA: 0x00013B81 File Offset: 0x00011D81
			public float joystickAxisSensitivityMax
			{
				get
				{
					return this._joystickAxisSensitivityMax;
				}
			}

			// Token: 0x170004B9 RID: 1209
			// (get) Token: 0x06001945 RID: 6469 RVA: 0x00013B89 File Offset: 0x00011D89
			public float mouseXYAxisSensitivityMin
			{
				get
				{
					return this._mouseXYAxisSensitivityMin;
				}
			}

			// Token: 0x170004BA RID: 1210
			// (get) Token: 0x06001946 RID: 6470 RVA: 0x00013B91 File Offset: 0x00011D91
			public float mouseXYAxisSensitivityMax
			{
				get
				{
					return this._mouseXYAxisSensitivityMax;
				}
			}

			// Token: 0x170004BB RID: 1211
			// (get) Token: 0x06001947 RID: 6471 RVA: 0x00013B99 File Offset: 0x00011D99
			public bool isValid
			{
				get
				{
					return this._inputBehaviorId >= 0 && (this._showJoystickAxisSensitivity || this._showMouseXYAxisSensitivity);
				}
			}

			// Token: 0x04001C46 RID: 7238
			[SerializeField]
			[Tooltip("The Input Behavior that will be displayed to the user for modification.")]
			private int _inputBehaviorId = -1;

			// Token: 0x04001C47 RID: 7239
			[SerializeField]
			[Tooltip("If checked, a slider will be displayed so the user can change this value.")]
			private bool _showJoystickAxisSensitivity = true;

			// Token: 0x04001C48 RID: 7240
			[SerializeField]
			[Tooltip("If checked, a slider will be displayed so the user can change this value.")]
			private bool _showMouseXYAxisSensitivity = true;

			// Token: 0x04001C49 RID: 7241
			[SerializeField]
			[Tooltip("If set to a non-blank value, this key will be used to look up the name in Language to be displayed as the title for the Input Behavior control set. Otherwise, the name field of the InputBehavior will be used.")]
			private string _labelLanguageKey = string.Empty;

			// Token: 0x04001C4A RID: 7242
			[SerializeField]
			[Tooltip("If set to a non-blank value, this name will be displayed above the individual slider control. Otherwise, no name will be displayed.")]
			private string _joystickAxisSensitivityLabelLanguageKey = string.Empty;

			// Token: 0x04001C4B RID: 7243
			[SerializeField]
			[Tooltip("If set to a non-blank value, this key will be used to look up the name in Language to be displayed above the individual slider control. Otherwise, no name will be displayed.")]
			private string _mouseXYAxisSensitivityLabelLanguageKey = string.Empty;

			// Token: 0x04001C4C RID: 7244
			[SerializeField]
			[Tooltip("The icon to display next to the slider. Set to none for no icon.")]
			private Sprite _joystickAxisSensitivityIcon;

			// Token: 0x04001C4D RID: 7245
			[SerializeField]
			[Tooltip("The icon to display next to the slider. Set to none for no icon.")]
			private Sprite _mouseXYAxisSensitivityIcon;

			// Token: 0x04001C4E RID: 7246
			[SerializeField]
			[Tooltip("Minimum value the user is allowed to set for this property.")]
			private float _joystickAxisSensitivityMin;

			// Token: 0x04001C4F RID: 7247
			[SerializeField]
			[Tooltip("Maximum value the user is allowed to set for this property.")]
			private float _joystickAxisSensitivityMax = 2f;

			// Token: 0x04001C50 RID: 7248
			[SerializeField]
			[Tooltip("Minimum value the user is allowed to set for this property.")]
			private float _mouseXYAxisSensitivityMin;

			// Token: 0x04001C51 RID: 7249
			[SerializeField]
			[Tooltip("Maximum value the user is allowed to set for this property.")]
			private float _mouseXYAxisSensitivityMax = 2f;
		}

		// Token: 0x0200043D RID: 1085
		[Serializable]
		private class Prefabs
		{
			// Token: 0x170004BC RID: 1212
			// (get) Token: 0x06001949 RID: 6473 RVA: 0x00013BB6 File Offset: 0x00011DB6
			public GameObject button
			{
				get
				{
					return this._button;
				}
			}

			// Token: 0x170004BD RID: 1213
			// (get) Token: 0x0600194A RID: 6474 RVA: 0x00013BBE File Offset: 0x00011DBE
			public GameObject fitButton
			{
				get
				{
					return this._fitButton;
				}
			}

			// Token: 0x170004BE RID: 1214
			// (get) Token: 0x0600194B RID: 6475 RVA: 0x00013BC6 File Offset: 0x00011DC6
			public GameObject inputGridLabel
			{
				get
				{
					return this._inputGridLabel;
				}
			}

			// Token: 0x170004BF RID: 1215
			// (get) Token: 0x0600194C RID: 6476 RVA: 0x00013BCE File Offset: 0x00011DCE
			public GameObject inputGridHeaderLabel
			{
				get
				{
					return this._inputGridHeaderLabel;
				}
			}

			// Token: 0x170004C0 RID: 1216
			// (get) Token: 0x0600194D RID: 6477 RVA: 0x00013BD6 File Offset: 0x00011DD6
			public GameObject inputGridFieldButton
			{
				get
				{
					return this._inputGridFieldButton;
				}
			}

			// Token: 0x170004C1 RID: 1217
			// (get) Token: 0x0600194E RID: 6478 RVA: 0x00013BDE File Offset: 0x00011DDE
			public GameObject inputGridFieldInvertToggle
			{
				get
				{
					return this._inputGridFieldInvertToggle;
				}
			}

			// Token: 0x170004C2 RID: 1218
			// (get) Token: 0x0600194F RID: 6479 RVA: 0x00013BE6 File Offset: 0x00011DE6
			public GameObject window
			{
				get
				{
					return this._window;
				}
			}

			// Token: 0x170004C3 RID: 1219
			// (get) Token: 0x06001950 RID: 6480 RVA: 0x00013BEE File Offset: 0x00011DEE
			public GameObject windowTitleText
			{
				get
				{
					return this._windowTitleText;
				}
			}

			// Token: 0x170004C4 RID: 1220
			// (get) Token: 0x06001951 RID: 6481 RVA: 0x00013BF6 File Offset: 0x00011DF6
			public GameObject windowContentText
			{
				get
				{
					return this._windowContentText;
				}
			}

			// Token: 0x170004C5 RID: 1221
			// (get) Token: 0x06001952 RID: 6482 RVA: 0x00013BFE File Offset: 0x00011DFE
			public GameObject fader
			{
				get
				{
					return this._fader;
				}
			}

			// Token: 0x170004C6 RID: 1222
			// (get) Token: 0x06001953 RID: 6483 RVA: 0x00013C06 File Offset: 0x00011E06
			public GameObject calibrationWindow
			{
				get
				{
					return this._calibrationWindow;
				}
			}

			// Token: 0x170004C7 RID: 1223
			// (get) Token: 0x06001954 RID: 6484 RVA: 0x00013C0E File Offset: 0x00011E0E
			public GameObject inputBehaviorsWindow
			{
				get
				{
					return this._inputBehaviorsWindow;
				}
			}

			// Token: 0x170004C8 RID: 1224
			// (get) Token: 0x06001955 RID: 6485 RVA: 0x00013C16 File Offset: 0x00011E16
			public GameObject centerStickGraphic
			{
				get
				{
					return this._centerStickGraphic;
				}
			}

			// Token: 0x170004C9 RID: 1225
			// (get) Token: 0x06001956 RID: 6486 RVA: 0x00013C1E File Offset: 0x00011E1E
			public GameObject moveStickGraphic
			{
				get
				{
					return this._moveStickGraphic;
				}
			}

			// Token: 0x06001957 RID: 6487 RVA: 0x0006ABFC File Offset: 0x00068DFC
			public bool Check()
			{
				return !(this._button == null) && !(this._fitButton == null) && !(this._inputGridLabel == null) && !(this._inputGridHeaderLabel == null) && !(this._inputGridFieldButton == null) && !(this._inputGridFieldInvertToggle == null) && !(this._window == null) && !(this._windowTitleText == null) && !(this._windowContentText == null) && !(this._fader == null) && !(this._calibrationWindow == null) && !(this._inputBehaviorsWindow == null);
			}

			// Token: 0x04001C52 RID: 7250
			[SerializeField]
			private GameObject _button;

			// Token: 0x04001C53 RID: 7251
			[SerializeField]
			private GameObject _fitButton;

			// Token: 0x04001C54 RID: 7252
			[SerializeField]
			private GameObject _inputGridLabel;

			// Token: 0x04001C55 RID: 7253
			[SerializeField]
			private GameObject _inputGridHeaderLabel;

			// Token: 0x04001C56 RID: 7254
			[SerializeField]
			private GameObject _inputGridFieldButton;

			// Token: 0x04001C57 RID: 7255
			[SerializeField]
			private GameObject _inputGridFieldInvertToggle;

			// Token: 0x04001C58 RID: 7256
			[SerializeField]
			private GameObject _window;

			// Token: 0x04001C59 RID: 7257
			[SerializeField]
			private GameObject _windowTitleText;

			// Token: 0x04001C5A RID: 7258
			[SerializeField]
			private GameObject _windowContentText;

			// Token: 0x04001C5B RID: 7259
			[SerializeField]
			private GameObject _fader;

			// Token: 0x04001C5C RID: 7260
			[SerializeField]
			private GameObject _calibrationWindow;

			// Token: 0x04001C5D RID: 7261
			[SerializeField]
			private GameObject _inputBehaviorsWindow;

			// Token: 0x04001C5E RID: 7262
			[SerializeField]
			private GameObject _centerStickGraphic;

			// Token: 0x04001C5F RID: 7263
			[SerializeField]
			private GameObject _moveStickGraphic;
		}

		// Token: 0x0200043E RID: 1086
		[Serializable]
		private class References
		{
			// Token: 0x170004CA RID: 1226
			// (get) Token: 0x06001959 RID: 6489 RVA: 0x00013C26 File Offset: 0x00011E26
			public Canvas canvas
			{
				get
				{
					return this._canvas;
				}
			}

			// Token: 0x170004CB RID: 1227
			// (get) Token: 0x0600195A RID: 6490 RVA: 0x00013C2E File Offset: 0x00011E2E
			public CanvasGroup mainCanvasGroup
			{
				get
				{
					return this._mainCanvasGroup;
				}
			}

			// Token: 0x170004CC RID: 1228
			// (get) Token: 0x0600195B RID: 6491 RVA: 0x00013C36 File Offset: 0x00011E36
			public Transform mainContent
			{
				get
				{
					return this._mainContent;
				}
			}

			// Token: 0x170004CD RID: 1229
			// (get) Token: 0x0600195C RID: 6492 RVA: 0x00013C3E File Offset: 0x00011E3E
			public Transform mainContentInner
			{
				get
				{
					return this._mainContentInner;
				}
			}

			// Token: 0x170004CE RID: 1230
			// (get) Token: 0x0600195D RID: 6493 RVA: 0x00013C46 File Offset: 0x00011E46
			public UIGroup playersGroup
			{
				get
				{
					return this._playersGroup;
				}
			}

			// Token: 0x170004CF RID: 1231
			// (get) Token: 0x0600195E RID: 6494 RVA: 0x00013C4E File Offset: 0x00011E4E
			public Transform controllerGroup
			{
				get
				{
					return this._controllerGroup;
				}
			}

			// Token: 0x170004D0 RID: 1232
			// (get) Token: 0x0600195F RID: 6495 RVA: 0x00013C56 File Offset: 0x00011E56
			public Transform controllerGroupLabelGroup
			{
				get
				{
					return this._controllerGroupLabelGroup;
				}
			}

			// Token: 0x170004D1 RID: 1233
			// (get) Token: 0x06001960 RID: 6496 RVA: 0x00013C5E File Offset: 0x00011E5E
			public UIGroup controllerSettingsGroup
			{
				get
				{
					return this._controllerSettingsGroup;
				}
			}

			// Token: 0x170004D2 RID: 1234
			// (get) Token: 0x06001961 RID: 6497 RVA: 0x00013C66 File Offset: 0x00011E66
			public UIGroup assignedControllersGroup
			{
				get
				{
					return this._assignedControllersGroup;
				}
			}

			// Token: 0x170004D3 RID: 1235
			// (get) Token: 0x06001962 RID: 6498 RVA: 0x00013C6E File Offset: 0x00011E6E
			public Transform settingsAndMapCategoriesGroup
			{
				get
				{
					return this._settingsAndMapCategoriesGroup;
				}
			}

			// Token: 0x170004D4 RID: 1236
			// (get) Token: 0x06001963 RID: 6499 RVA: 0x00013C76 File Offset: 0x00011E76
			public UIGroup settingsGroup
			{
				get
				{
					return this._settingsGroup;
				}
			}

			// Token: 0x170004D5 RID: 1237
			// (get) Token: 0x06001964 RID: 6500 RVA: 0x00013C7E File Offset: 0x00011E7E
			public UIGroup mapCategoriesGroup
			{
				get
				{
					return this._mapCategoriesGroup;
				}
			}

			// Token: 0x170004D6 RID: 1238
			// (get) Token: 0x06001965 RID: 6501 RVA: 0x00013C86 File Offset: 0x00011E86
			public Transform inputGridGroup
			{
				get
				{
					return this._inputGridGroup;
				}
			}

			// Token: 0x170004D7 RID: 1239
			// (get) Token: 0x06001966 RID: 6502 RVA: 0x00013C8E File Offset: 0x00011E8E
			public Transform inputGridContainer
			{
				get
				{
					return this._inputGridContainer;
				}
			}

			// Token: 0x170004D8 RID: 1240
			// (get) Token: 0x06001967 RID: 6503 RVA: 0x00013C96 File Offset: 0x00011E96
			public Transform inputGridHeadersGroup
			{
				get
				{
					return this._inputGridHeadersGroup;
				}
			}

			// Token: 0x170004D9 RID: 1241
			// (get) Token: 0x06001968 RID: 6504 RVA: 0x00013C9E File Offset: 0x00011E9E
			public Scrollbar inputGridVScrollbar
			{
				get
				{
					return this._inputGridVScrollbar;
				}
			}

			// Token: 0x170004DA RID: 1242
			// (get) Token: 0x06001969 RID: 6505 RVA: 0x00013CA6 File Offset: 0x00011EA6
			public ScrollRect inputGridScrollRect
			{
				get
				{
					return this._inputGridScrollRect;
				}
			}

			// Token: 0x170004DB RID: 1243
			// (get) Token: 0x0600196A RID: 6506 RVA: 0x00013CAE File Offset: 0x00011EAE
			public Transform inputGridInnerGroup
			{
				get
				{
					return this._inputGridInnerGroup;
				}
			}

			// Token: 0x170004DC RID: 1244
			// (get) Token: 0x0600196B RID: 6507 RVA: 0x00013CB6 File Offset: 0x00011EB6
			public Text controllerNameLabel
			{
				get
				{
					return this._controllerNameLabel;
				}
			}

			// Token: 0x170004DD RID: 1245
			// (get) Token: 0x0600196C RID: 6508 RVA: 0x00013CBE File Offset: 0x00011EBE
			public Button removeControllerButton
			{
				get
				{
					return this._removeControllerButton;
				}
			}

			// Token: 0x170004DE RID: 1246
			// (get) Token: 0x0600196D RID: 6509 RVA: 0x00013CC6 File Offset: 0x00011EC6
			public Button assignControllerButton
			{
				get
				{
					return this._assignControllerButton;
				}
			}

			// Token: 0x170004DF RID: 1247
			// (get) Token: 0x0600196E RID: 6510 RVA: 0x00013CCE File Offset: 0x00011ECE
			public Button calibrateControllerButton
			{
				get
				{
					return this._calibrateControllerButton;
				}
			}

			// Token: 0x170004E0 RID: 1248
			// (get) Token: 0x0600196F RID: 6511 RVA: 0x00013CD6 File Offset: 0x00011ED6
			public Button doneButton
			{
				get
				{
					return this._doneButton;
				}
			}

			// Token: 0x170004E1 RID: 1249
			// (get) Token: 0x06001970 RID: 6512 RVA: 0x00013CDE File Offset: 0x00011EDE
			public Button restoreDefaultsButton
			{
				get
				{
					return this._restoreDefaultsButton;
				}
			}

			// Token: 0x170004E2 RID: 1250
			// (get) Token: 0x06001971 RID: 6513 RVA: 0x00013CE6 File Offset: 0x00011EE6
			public Selectable defaultSelection
			{
				get
				{
					return this._defaultSelection;
				}
			}

			// Token: 0x170004E3 RID: 1251
			// (get) Token: 0x06001972 RID: 6514 RVA: 0x00013CEE File Offset: 0x00011EEE
			public GameObject[] fixedSelectableUIElements
			{
				get
				{
					return this._fixedSelectableUIElements;
				}
			}

			// Token: 0x170004E4 RID: 1252
			// (get) Token: 0x06001973 RID: 6515 RVA: 0x00013CF6 File Offset: 0x00011EF6
			public Image mainBackgroundImage
			{
				get
				{
					return this._mainBackgroundImage;
				}
			}

			// Token: 0x170004E5 RID: 1253
			// (get) Token: 0x06001974 RID: 6516 RVA: 0x00013CFE File Offset: 0x00011EFE
			// (set) Token: 0x06001975 RID: 6517 RVA: 0x00013D06 File Offset: 0x00011F06
			public LayoutElement inputGridLayoutElement { get; set; }

			// Token: 0x170004E6 RID: 1254
			// (get) Token: 0x06001976 RID: 6518 RVA: 0x00013D0F File Offset: 0x00011F0F
			// (set) Token: 0x06001977 RID: 6519 RVA: 0x00013D17 File Offset: 0x00011F17
			public Transform inputGridActionColumn { get; set; }

			// Token: 0x170004E7 RID: 1255
			// (get) Token: 0x06001978 RID: 6520 RVA: 0x00013D20 File Offset: 0x00011F20
			// (set) Token: 0x06001979 RID: 6521 RVA: 0x00013D28 File Offset: 0x00011F28
			public Transform inputGridKeyboardColumn { get; set; }

			// Token: 0x170004E8 RID: 1256
			// (get) Token: 0x0600197A RID: 6522 RVA: 0x00013D31 File Offset: 0x00011F31
			// (set) Token: 0x0600197B RID: 6523 RVA: 0x00013D39 File Offset: 0x00011F39
			public Transform inputGridMouseColumn { get; set; }

			// Token: 0x170004E9 RID: 1257
			// (get) Token: 0x0600197C RID: 6524 RVA: 0x00013D42 File Offset: 0x00011F42
			// (set) Token: 0x0600197D RID: 6525 RVA: 0x00013D4A File Offset: 0x00011F4A
			public Transform inputGridControllerColumn { get; set; }

			// Token: 0x170004EA RID: 1258
			// (get) Token: 0x0600197E RID: 6526 RVA: 0x00013D53 File Offset: 0x00011F53
			// (set) Token: 0x0600197F RID: 6527 RVA: 0x00013D5B File Offset: 0x00011F5B
			public Transform inputGridHeader1 { get; set; }

			// Token: 0x170004EB RID: 1259
			// (get) Token: 0x06001980 RID: 6528 RVA: 0x00013D64 File Offset: 0x00011F64
			// (set) Token: 0x06001981 RID: 6529 RVA: 0x00013D6C File Offset: 0x00011F6C
			public Transform inputGridHeader2 { get; set; }

			// Token: 0x170004EC RID: 1260
			// (get) Token: 0x06001982 RID: 6530 RVA: 0x00013D75 File Offset: 0x00011F75
			// (set) Token: 0x06001983 RID: 6531 RVA: 0x00013D7D File Offset: 0x00011F7D
			public Transform inputGridHeader3 { get; set; }

			// Token: 0x170004ED RID: 1261
			// (get) Token: 0x06001984 RID: 6532 RVA: 0x00013D86 File Offset: 0x00011F86
			// (set) Token: 0x06001985 RID: 6533 RVA: 0x00013D8E File Offset: 0x00011F8E
			public Transform inputGridHeader4 { get; set; }

			// Token: 0x06001986 RID: 6534 RVA: 0x0006ACBC File Offset: 0x00068EBC
			public bool Check()
			{
				return !(this._canvas == null) && !(this._mainCanvasGroup == null) && !(this._mainContent == null) && !(this._mainContentInner == null) && !(this._playersGroup == null) && !(this._controllerGroup == null) && !(this._controllerGroupLabelGroup == null) && !(this._controllerSettingsGroup == null) && !(this._assignedControllersGroup == null) && !(this._settingsAndMapCategoriesGroup == null) && !(this._settingsGroup == null) && !(this._mapCategoriesGroup == null) && !(this._inputGridGroup == null) && !(this._inputGridContainer == null) && !(this._inputGridHeadersGroup == null) && !(this._inputGridVScrollbar == null) && !(this._inputGridScrollRect == null) && !(this._inputGridInnerGroup == null) && !(this._controllerNameLabel == null) && !(this._removeControllerButton == null) && !(this._assignControllerButton == null) && !(this._calibrateControllerButton == null) && !(this._doneButton == null) && !(this._restoreDefaultsButton == null) && !(this._defaultSelection == null);
			}

			// Token: 0x04001C60 RID: 7264
			[SerializeField]
			private Canvas _canvas;

			// Token: 0x04001C61 RID: 7265
			[SerializeField]
			private CanvasGroup _mainCanvasGroup;

			// Token: 0x04001C62 RID: 7266
			[SerializeField]
			private Transform _mainContent;

			// Token: 0x04001C63 RID: 7267
			[SerializeField]
			private Transform _mainContentInner;

			// Token: 0x04001C64 RID: 7268
			[SerializeField]
			private UIGroup _playersGroup;

			// Token: 0x04001C65 RID: 7269
			[SerializeField]
			private Transform _controllerGroup;

			// Token: 0x04001C66 RID: 7270
			[SerializeField]
			private Transform _controllerGroupLabelGroup;

			// Token: 0x04001C67 RID: 7271
			[SerializeField]
			private UIGroup _controllerSettingsGroup;

			// Token: 0x04001C68 RID: 7272
			[SerializeField]
			private UIGroup _assignedControllersGroup;

			// Token: 0x04001C69 RID: 7273
			[SerializeField]
			private Transform _settingsAndMapCategoriesGroup;

			// Token: 0x04001C6A RID: 7274
			[SerializeField]
			private UIGroup _settingsGroup;

			// Token: 0x04001C6B RID: 7275
			[SerializeField]
			private UIGroup _mapCategoriesGroup;

			// Token: 0x04001C6C RID: 7276
			[SerializeField]
			private Transform _inputGridGroup;

			// Token: 0x04001C6D RID: 7277
			[SerializeField]
			private Transform _inputGridContainer;

			// Token: 0x04001C6E RID: 7278
			[SerializeField]
			private Transform _inputGridHeadersGroup;

			// Token: 0x04001C6F RID: 7279
			[SerializeField]
			private Scrollbar _inputGridVScrollbar;

			// Token: 0x04001C70 RID: 7280
			[SerializeField]
			private ScrollRect _inputGridScrollRect;

			// Token: 0x04001C71 RID: 7281
			[SerializeField]
			private Transform _inputGridInnerGroup;

			// Token: 0x04001C72 RID: 7282
			[SerializeField]
			private Text _controllerNameLabel;

			// Token: 0x04001C73 RID: 7283
			[SerializeField]
			private Button _removeControllerButton;

			// Token: 0x04001C74 RID: 7284
			[SerializeField]
			private Button _assignControllerButton;

			// Token: 0x04001C75 RID: 7285
			[SerializeField]
			private Button _calibrateControllerButton;

			// Token: 0x04001C76 RID: 7286
			[SerializeField]
			private Button _doneButton;

			// Token: 0x04001C77 RID: 7287
			[SerializeField]
			private Button _restoreDefaultsButton;

			// Token: 0x04001C78 RID: 7288
			[SerializeField]
			private Selectable _defaultSelection;

			// Token: 0x04001C79 RID: 7289
			[SerializeField]
			private GameObject[] _fixedSelectableUIElements;

			// Token: 0x04001C7A RID: 7290
			[SerializeField]
			private Image _mainBackgroundImage;
		}

		// Token: 0x0200043F RID: 1087
		private class InputActionSet
		{
			// Token: 0x170004EE RID: 1262
			// (get) Token: 0x06001988 RID: 6536 RVA: 0x00013D97 File Offset: 0x00011F97
			public int actionId
			{
				get
				{
					return this._actionId;
				}
			}

			// Token: 0x170004EF RID: 1263
			// (get) Token: 0x06001989 RID: 6537 RVA: 0x00013D9F File Offset: 0x00011F9F
			public AxisRange axisRange
			{
				get
				{
					return this._axisRange;
				}
			}

			// Token: 0x0600198A RID: 6538 RVA: 0x00013DA7 File Offset: 0x00011FA7
			public InputActionSet(int actionId, AxisRange axisRange)
			{
				this._actionId = actionId;
				this._axisRange = axisRange;
			}

			// Token: 0x04001C84 RID: 7300
			private int _actionId;

			// Token: 0x04001C85 RID: 7301
			private AxisRange _axisRange;
		}

		// Token: 0x02000440 RID: 1088
		private class InputMapping
		{
			// Token: 0x170004F0 RID: 1264
			// (get) Token: 0x0600198B RID: 6539 RVA: 0x00013DBD File Offset: 0x00011FBD
			// (set) Token: 0x0600198C RID: 6540 RVA: 0x00013DC5 File Offset: 0x00011FC5
			public string actionName { get; private set; }

			// Token: 0x170004F1 RID: 1265
			// (get) Token: 0x0600198D RID: 6541 RVA: 0x00013DCE File Offset: 0x00011FCE
			// (set) Token: 0x0600198E RID: 6542 RVA: 0x00013DD6 File Offset: 0x00011FD6
			public InputFieldInfo fieldInfo { get; private set; }

			// Token: 0x170004F2 RID: 1266
			// (get) Token: 0x0600198F RID: 6543 RVA: 0x00013DDF File Offset: 0x00011FDF
			// (set) Token: 0x06001990 RID: 6544 RVA: 0x00013DE7 File Offset: 0x00011FE7
			public ControllerMap map { get; private set; }

			// Token: 0x170004F3 RID: 1267
			// (get) Token: 0x06001991 RID: 6545 RVA: 0x00013DF0 File Offset: 0x00011FF0
			// (set) Token: 0x06001992 RID: 6546 RVA: 0x00013DF8 File Offset: 0x00011FF8
			public ActionElementMap aem { get; private set; }

			// Token: 0x170004F4 RID: 1268
			// (get) Token: 0x06001993 RID: 6547 RVA: 0x00013E01 File Offset: 0x00012001
			// (set) Token: 0x06001994 RID: 6548 RVA: 0x00013E09 File Offset: 0x00012009
			public ControllerType controllerType { get; private set; }

			// Token: 0x170004F5 RID: 1269
			// (get) Token: 0x06001995 RID: 6549 RVA: 0x00013E12 File Offset: 0x00012012
			// (set) Token: 0x06001996 RID: 6550 RVA: 0x00013E1A File Offset: 0x0001201A
			public int controllerId { get; private set; }

			// Token: 0x170004F6 RID: 1270
			// (get) Token: 0x06001997 RID: 6551 RVA: 0x00013E23 File Offset: 0x00012023
			// (set) Token: 0x06001998 RID: 6552 RVA: 0x00013E2B File Offset: 0x0001202B
			public ControllerPollingInfo pollingInfo { get; set; }

			// Token: 0x170004F7 RID: 1271
			// (get) Token: 0x06001999 RID: 6553 RVA: 0x00013E34 File Offset: 0x00012034
			// (set) Token: 0x0600199A RID: 6554 RVA: 0x00013E3C File Offset: 0x0001203C
			public ModifierKeyFlags modifierKeyFlags { get; set; }

			// Token: 0x170004F8 RID: 1272
			// (get) Token: 0x0600199B RID: 6555 RVA: 0x0006AE58 File Offset: 0x00069058
			public AxisRange axisRange
			{
				get
				{
					AxisRange axisRange = 1;
					if (this.pollingInfo.elementType == null)
					{
						if (this.fieldInfo.axisRange == null)
						{
							axisRange = 0;
						}
						else
						{
							axisRange = ((this.pollingInfo.axisPole == null) ? 1 : 2);
						}
					}
					return axisRange;
				}
			}

			// Token: 0x170004F9 RID: 1273
			// (get) Token: 0x0600199C RID: 6556 RVA: 0x0006AEA0 File Offset: 0x000690A0
			public string elementName
			{
				get
				{
					if (this.controllerType == null)
					{
						return ControlMapper.GetLanguage().GetElementIdentifierName(this.pollingInfo.keyboardKey, this.modifierKeyFlags);
					}
					return ControlMapper.GetLanguage().GetElementIdentifierName(this.pollingInfo.controller, this.pollingInfo.elementIdentifierId, (this.pollingInfo.axisPole == null) ? 1 : 2);
				}
			}

			// Token: 0x0600199D RID: 6557 RVA: 0x00013E45 File Offset: 0x00012045
			public InputMapping(string actionName, InputFieldInfo fieldInfo, ControllerMap map, ActionElementMap aem, ControllerType controllerType, int controllerId)
			{
				this.actionName = actionName;
				this.fieldInfo = fieldInfo;
				this.map = map;
				this.aem = aem;
				this.controllerType = controllerType;
				this.controllerId = controllerId;
			}

			// Token: 0x0600199E RID: 6558 RVA: 0x00013E7A File Offset: 0x0001207A
			public ElementAssignment ToElementAssignment(ControllerPollingInfo pollingInfo)
			{
				this.pollingInfo = pollingInfo;
				return this.ToElementAssignment();
			}

			// Token: 0x0600199F RID: 6559 RVA: 0x00013E89 File Offset: 0x00012089
			public ElementAssignment ToElementAssignment(ControllerPollingInfo pollingInfo, ModifierKeyFlags modifierKeyFlags)
			{
				this.pollingInfo = pollingInfo;
				this.modifierKeyFlags = modifierKeyFlags;
				return this.ToElementAssignment();
			}

			// Token: 0x060019A0 RID: 6560 RVA: 0x0006AF10 File Offset: 0x00069110
			public ElementAssignment ToElementAssignment()
			{
				return new ElementAssignment(this.controllerType, this.pollingInfo.elementType, this.pollingInfo.elementIdentifierId, this.axisRange, this.pollingInfo.keyboardKey, this.modifierKeyFlags, this.fieldInfo.actionId, (this.fieldInfo.axisRange == 2) ? 1 : 0, false, (this.aem != null) ? this.aem.id : (-1));
			}
		}

		// Token: 0x02000441 RID: 1089
		private class AxisCalibrator
		{
			// Token: 0x170004FA RID: 1274
			// (get) Token: 0x060019A1 RID: 6561 RVA: 0x00013E9F File Offset: 0x0001209F
			public bool isValid
			{
				get
				{
					return this.axis != null;
				}
			}

			// Token: 0x060019A2 RID: 6562 RVA: 0x0006AF94 File Offset: 0x00069194
			public AxisCalibrator(Joystick joystick, int axisIndex)
			{
				this.data = default(AxisCalibrationData);
				this.joystick = joystick;
				this.axisIndex = axisIndex;
				if (joystick != null && axisIndex >= 0 && joystick.axisCount > axisIndex)
				{
					this.axis = joystick.Axes[axisIndex];
					this.data = joystick.calibrationMap.GetAxis(axisIndex).GetData();
				}
				this.firstRun = true;
			}

			// Token: 0x060019A3 RID: 6563 RVA: 0x0006B004 File Offset: 0x00069204
			public void RecordMinMax()
			{
				if (this.axis == null)
				{
					return;
				}
				float valueRaw = this.axis.valueRaw;
				if (this.firstRun || valueRaw < this.data.min)
				{
					this.data.min = valueRaw;
				}
				if (this.firstRun || valueRaw > this.data.max)
				{
					this.data.max = valueRaw;
				}
				this.firstRun = false;
			}

			// Token: 0x060019A4 RID: 6564 RVA: 0x00013EAA File Offset: 0x000120AA
			public void RecordZero()
			{
				if (this.axis == null)
				{
					return;
				}
				this.data.zero = this.axis.valueRaw;
			}

			// Token: 0x060019A5 RID: 6565 RVA: 0x0006B074 File Offset: 0x00069274
			public void Commit()
			{
				if (this.axis == null)
				{
					return;
				}
				AxisCalibration axisCalibration = this.joystick.calibrationMap.GetAxis(this.axisIndex);
				if (axisCalibration == null)
				{
					return;
				}
				if ((double)Mathf.Abs(this.data.max - this.data.min) < 0.1)
				{
					return;
				}
				axisCalibration.SetData(this.data);
			}

			// Token: 0x04001C8E RID: 7310
			public AxisCalibrationData data;

			// Token: 0x04001C8F RID: 7311
			public readonly Joystick joystick;

			// Token: 0x04001C90 RID: 7312
			public readonly int axisIndex;

			// Token: 0x04001C91 RID: 7313
			private Controller.Axis axis;

			// Token: 0x04001C92 RID: 7314
			private bool firstRun;
		}

		// Token: 0x02000442 RID: 1090
		private class IndexedDictionary<TKey, TValue>
		{
			// Token: 0x170004FB RID: 1275
			// (get) Token: 0x060019A6 RID: 6566 RVA: 0x00013ECB File Offset: 0x000120CB
			public int Count
			{
				get
				{
					return this.list.Count;
				}
			}

			// Token: 0x060019A7 RID: 6567 RVA: 0x00013ED8 File Offset: 0x000120D8
			public IndexedDictionary()
			{
				this.list = new List<ControlMapper.IndexedDictionary<TKey, TValue>.Entry>();
			}

			// Token: 0x170004FC RID: 1276
			public TValue this[int index]
			{
				get
				{
					return this.list[index].value;
				}
			}

			// Token: 0x060019A9 RID: 6569 RVA: 0x0006B0DC File Offset: 0x000692DC
			public TValue Get(TKey key)
			{
				int num = this.IndexOfKey(key);
				if (num < 0)
				{
					throw new Exception("Key does not exist!");
				}
				return this.list[num].value;
			}

			// Token: 0x060019AA RID: 6570 RVA: 0x0006B114 File Offset: 0x00069314
			public bool TryGet(TKey key, out TValue value)
			{
				value = default(TValue);
				int num = this.IndexOfKey(key);
				if (num < 0)
				{
					return false;
				}
				value = this.list[num].value;
				return true;
			}

			// Token: 0x060019AB RID: 6571 RVA: 0x00013EFE File Offset: 0x000120FE
			public void Add(TKey key, TValue value)
			{
				if (this.ContainsKey(key))
				{
					throw new Exception("Key " + key.ToString() + " is already in use!");
				}
				this.list.Add(new ControlMapper.IndexedDictionary<TKey, TValue>.Entry(key, value));
			}

			// Token: 0x060019AC RID: 6572 RVA: 0x0006B150 File Offset: 0x00069350
			public int IndexOfKey(TKey key)
			{
				int count = this.list.Count;
				for (int i = 0; i < count; i++)
				{
					if (EqualityComparer<TKey>.Default.Equals(this.list[i].key, key))
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x060019AD RID: 6573 RVA: 0x0006B198 File Offset: 0x00069398
			public bool ContainsKey(TKey key)
			{
				int count = this.list.Count;
				for (int i = 0; i < count; i++)
				{
					if (EqualityComparer<TKey>.Default.Equals(this.list[i].key, key))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060019AE RID: 6574 RVA: 0x00013F3D File Offset: 0x0001213D
			public void Clear()
			{
				this.list.Clear();
			}

			// Token: 0x04001C93 RID: 7315
			private List<ControlMapper.IndexedDictionary<TKey, TValue>.Entry> list;

			// Token: 0x02000443 RID: 1091
			private class Entry
			{
				// Token: 0x060019AF RID: 6575 RVA: 0x00013F4A File Offset: 0x0001214A
				public Entry(TKey key, TValue value)
				{
					this.key = key;
					this.value = value;
				}

				// Token: 0x04001C94 RID: 7316
				public TKey key;

				// Token: 0x04001C95 RID: 7317
				public TValue value;
			}
		}

		// Token: 0x02000444 RID: 1092
		private enum LayoutElementSizeType
		{
			// Token: 0x04001C97 RID: 7319
			MinSize,
			// Token: 0x04001C98 RID: 7320
			PreferredSize
		}

		// Token: 0x02000445 RID: 1093
		private enum WindowType
		{
			// Token: 0x04001C9A RID: 7322
			None,
			// Token: 0x04001C9B RID: 7323
			ChooseJoystick,
			// Token: 0x04001C9C RID: 7324
			JoystickAssignmentConflict,
			// Token: 0x04001C9D RID: 7325
			ElementAssignment,
			// Token: 0x04001C9E RID: 7326
			ElementAssignmentPrePolling,
			// Token: 0x04001C9F RID: 7327
			ElementAssignmentPolling,
			// Token: 0x04001CA0 RID: 7328
			ElementAssignmentResult,
			// Token: 0x04001CA1 RID: 7329
			ElementAssignmentConflict,
			// Token: 0x04001CA2 RID: 7330
			Calibration,
			// Token: 0x04001CA3 RID: 7331
			CalibrateStep1,
			// Token: 0x04001CA4 RID: 7332
			CalibrateStep2
		}

		// Token: 0x02000446 RID: 1094
		private class InputGrid
		{
			// Token: 0x060019B0 RID: 6576 RVA: 0x00013F60 File Offset: 0x00012160
			public InputGrid()
			{
				this.list = new ControlMapper.InputGridEntryList();
				this.groups = new List<GameObject>();
			}

			// Token: 0x060019B1 RID: 6577 RVA: 0x00013F7E File Offset: 0x0001217E
			public void AddMapCategory(int mapCategoryId)
			{
				this.list.AddMapCategory(mapCategoryId);
			}

			// Token: 0x060019B2 RID: 6578 RVA: 0x00013F8C File Offset: 0x0001218C
			public void AddAction(int mapCategoryId, InputAction action, AxisRange axisRange)
			{
				this.list.AddAction(mapCategoryId, action, axisRange);
			}

			// Token: 0x060019B3 RID: 6579 RVA: 0x00013F9C File Offset: 0x0001219C
			public void AddActionCategory(int mapCategoryId, int actionCategoryId)
			{
				this.list.AddActionCategory(mapCategoryId, actionCategoryId);
			}

			// Token: 0x060019B4 RID: 6580 RVA: 0x00013FAB File Offset: 0x000121AB
			public void AddInputFieldSet(int mapCategoryId, InputAction action, AxisRange axisRange, ControllerType controllerType, GameObject fieldSetContainer)
			{
				this.list.AddInputFieldSet(mapCategoryId, action, axisRange, controllerType, fieldSetContainer);
			}

			// Token: 0x060019B5 RID: 6581 RVA: 0x00013FBF File Offset: 0x000121BF
			public void AddInputField(int mapCategoryId, InputAction action, AxisRange axisRange, ControllerType controllerType, int fieldIndex, ControlMapper.GUIInputField inputField)
			{
				this.list.AddInputField(mapCategoryId, action, axisRange, controllerType, fieldIndex, inputField);
			}

			// Token: 0x060019B6 RID: 6582 RVA: 0x00013FD5 File Offset: 0x000121D5
			public void AddGroup(GameObject group)
			{
				this.groups.Add(group);
			}

			// Token: 0x060019B7 RID: 6583 RVA: 0x00013FE3 File Offset: 0x000121E3
			public void AddActionLabel(int mapCategoryId, int actionId, AxisRange axisRange, ControlMapper.GUILabel label)
			{
				this.list.AddActionLabel(mapCategoryId, actionId, axisRange, label);
			}

			// Token: 0x060019B8 RID: 6584 RVA: 0x00013FF5 File Offset: 0x000121F5
			public void AddActionCategoryLabel(int mapCategoryId, int actionCategoryId, ControlMapper.GUILabel label)
			{
				this.list.AddActionCategoryLabel(mapCategoryId, actionCategoryId, label);
			}

			// Token: 0x060019B9 RID: 6585 RVA: 0x00014005 File Offset: 0x00012205
			public bool Contains(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int fieldIndex)
			{
				return this.list.Contains(mapCategoryId, actionId, axisRange, controllerType, fieldIndex);
			}

			// Token: 0x060019BA RID: 6586 RVA: 0x00014019 File Offset: 0x00012219
			public ControlMapper.GUIInputField GetGUIInputField(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int fieldIndex)
			{
				return this.list.GetGUIInputField(mapCategoryId, actionId, axisRange, controllerType, fieldIndex);
			}

			// Token: 0x060019BB RID: 6587 RVA: 0x0001402D File Offset: 0x0001222D
			public IEnumerable<ControlMapper.InputActionSet> GetActionSets(int mapCategoryId)
			{
				return this.list.GetActionSets(mapCategoryId);
			}

			// Token: 0x060019BC RID: 6588 RVA: 0x0001403B File Offset: 0x0001223B
			public void SetColumnHeight(int mapCategoryId, float height)
			{
				this.list.SetColumnHeight(mapCategoryId, height);
			}

			// Token: 0x060019BD RID: 6589 RVA: 0x0001404A File Offset: 0x0001224A
			public float GetColumnHeight(int mapCategoryId)
			{
				return this.list.GetColumnHeight(mapCategoryId);
			}

			// Token: 0x060019BE RID: 6590 RVA: 0x00014058 File Offset: 0x00012258
			public void SetFieldsActive(int mapCategoryId, bool state)
			{
				this.list.SetFieldsActive(mapCategoryId, state);
			}

			// Token: 0x060019BF RID: 6591 RVA: 0x00014067 File Offset: 0x00012267
			public void SetFieldLabel(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int index, string label)
			{
				this.list.SetLabel(mapCategoryId, actionId, axisRange, controllerType, index, label);
			}

			// Token: 0x060019C0 RID: 6592 RVA: 0x0006B1E0 File Offset: 0x000693E0
			public void PopulateField(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int controllerId, int index, int actionElementMapId, GameObject buttonDisplay, bool invert)
			{
				this.list.PopulateField(mapCategoryId, actionId, axisRange, controllerType, controllerId, index, actionElementMapId, buttonDisplay, invert);
			}

			// Token: 0x060019C1 RID: 6593 RVA: 0x0001407D File Offset: 0x0001227D
			public void SetFixedFieldData(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int controllerId)
			{
				this.list.SetFixedFieldData(mapCategoryId, actionId, axisRange, controllerType, controllerId);
			}

			// Token: 0x060019C2 RID: 6594 RVA: 0x00014091 File Offset: 0x00012291
			public void InitializeFields(int mapCategoryId)
			{
				this.list.InitializeFields(mapCategoryId);
			}

			// Token: 0x060019C3 RID: 6595 RVA: 0x0001409F File Offset: 0x0001229F
			public void Show(int mapCategoryId)
			{
				this.list.Show(mapCategoryId);
			}

			// Token: 0x060019C4 RID: 6596 RVA: 0x000140AD File Offset: 0x000122AD
			public void HideAll()
			{
				this.list.HideAll();
			}

			// Token: 0x060019C5 RID: 6597 RVA: 0x000140BA File Offset: 0x000122BA
			public void ClearLabels(int mapCategoryId)
			{
				this.list.ClearLabels(mapCategoryId);
			}

			// Token: 0x060019C6 RID: 6598 RVA: 0x0006B208 File Offset: 0x00069408
			private void ClearGroups()
			{
				for (int i = 0; i < this.groups.Count; i++)
				{
					if (!(this.groups[i] == null))
					{
						Object.Destroy(this.groups[i]);
					}
				}
			}

			// Token: 0x060019C7 RID: 6599 RVA: 0x000140C8 File Offset: 0x000122C8
			public void ClearAll()
			{
				this.ClearGroups();
				this.list.Clear();
			}

			// Token: 0x04001CA5 RID: 7333
			private ControlMapper.InputGridEntryList list;

			// Token: 0x04001CA6 RID: 7334
			private List<GameObject> groups;
		}

		// Token: 0x02000447 RID: 1095
		private class InputGridEntryList
		{
			// Token: 0x060019C8 RID: 6600 RVA: 0x000140DB File Offset: 0x000122DB
			public InputGridEntryList()
			{
				this.entries = new ControlMapper.IndexedDictionary<int, ControlMapper.InputGridEntryList.MapCategoryEntry>();
			}

			// Token: 0x060019C9 RID: 6601 RVA: 0x000140EE File Offset: 0x000122EE
			public void AddMapCategory(int mapCategoryId)
			{
				if (mapCategoryId < 0)
				{
					return;
				}
				if (this.entries.ContainsKey(mapCategoryId))
				{
					return;
				}
				this.entries.Add(mapCategoryId, new ControlMapper.InputGridEntryList.MapCategoryEntry());
			}

			// Token: 0x060019CA RID: 6602 RVA: 0x00014115 File Offset: 0x00012315
			public void AddAction(int mapCategoryId, InputAction action, AxisRange axisRange)
			{
				this.AddActionEntry(mapCategoryId, action, axisRange);
			}

			// Token: 0x060019CB RID: 6603 RVA: 0x0006B250 File Offset: 0x00069450
			private ControlMapper.InputGridEntryList.ActionEntry AddActionEntry(int mapCategoryId, InputAction action, AxisRange axisRange)
			{
				if (action == null)
				{
					return null;
				}
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return null;
				}
				return mapCategoryEntry.AddAction(action, axisRange);
			}

			// Token: 0x060019CC RID: 6604 RVA: 0x0006B27C File Offset: 0x0006947C
			public void AddActionLabel(int mapCategoryId, int actionId, AxisRange axisRange, ControlMapper.GUILabel label)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return;
				}
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = mapCategoryEntry.GetActionEntry(actionId, axisRange);
				if (actionEntry == null)
				{
					return;
				}
				actionEntry.SetLabel(label);
			}

			// Token: 0x060019CD RID: 6605 RVA: 0x00014121 File Offset: 0x00012321
			public void AddActionCategory(int mapCategoryId, int actionCategoryId)
			{
				this.AddActionCategoryEntry(mapCategoryId, actionCategoryId);
			}

			// Token: 0x060019CE RID: 6606 RVA: 0x0006B2B0 File Offset: 0x000694B0
			private ControlMapper.InputGridEntryList.ActionCategoryEntry AddActionCategoryEntry(int mapCategoryId, int actionCategoryId)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return null;
				}
				return mapCategoryEntry.AddActionCategory(actionCategoryId);
			}

			// Token: 0x060019CF RID: 6607 RVA: 0x0006B2D8 File Offset: 0x000694D8
			public void AddActionCategoryLabel(int mapCategoryId, int actionCategoryId, ControlMapper.GUILabel label)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return;
				}
				ControlMapper.InputGridEntryList.ActionCategoryEntry actionCategoryEntry = mapCategoryEntry.GetActionCategoryEntry(actionCategoryId);
				if (actionCategoryEntry == null)
				{
					return;
				}
				actionCategoryEntry.SetLabel(label);
			}

			// Token: 0x060019D0 RID: 6608 RVA: 0x0006B30C File Offset: 0x0006950C
			public void AddInputFieldSet(int mapCategoryId, InputAction action, AxisRange axisRange, ControllerType controllerType, GameObject fieldSetContainer)
			{
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = this.GetActionEntry(mapCategoryId, action, axisRange);
				if (actionEntry == null)
				{
					return;
				}
				actionEntry.AddInputFieldSet(controllerType, fieldSetContainer);
			}

			// Token: 0x060019D1 RID: 6609 RVA: 0x0006B334 File Offset: 0x00069534
			public void AddInputField(int mapCategoryId, InputAction action, AxisRange axisRange, ControllerType controllerType, int fieldIndex, ControlMapper.GUIInputField inputField)
			{
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = this.GetActionEntry(mapCategoryId, action, axisRange);
				if (actionEntry == null)
				{
					return;
				}
				actionEntry.AddInputField(controllerType, fieldIndex, inputField);
			}

			// Token: 0x060019D2 RID: 6610 RVA: 0x0001412C File Offset: 0x0001232C
			public bool Contains(int mapCategoryId, int actionId, AxisRange axisRange)
			{
				return this.GetActionEntry(mapCategoryId, actionId, axisRange) != null;
			}

			// Token: 0x060019D3 RID: 6611 RVA: 0x0006B35C File Offset: 0x0006955C
			public bool Contains(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int fieldIndex)
			{
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = this.GetActionEntry(mapCategoryId, actionId, axisRange);
				return actionEntry != null && actionEntry.Contains(controllerType, fieldIndex);
			}

			// Token: 0x060019D4 RID: 6612 RVA: 0x0006B384 File Offset: 0x00069584
			public void SetColumnHeight(int mapCategoryId, float height)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return;
				}
				mapCategoryEntry.columnHeight = height;
			}

			// Token: 0x060019D5 RID: 6613 RVA: 0x0006B3AC File Offset: 0x000695AC
			public float GetColumnHeight(int mapCategoryId)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return 0f;
				}
				return mapCategoryEntry.columnHeight;
			}

			// Token: 0x060019D6 RID: 6614 RVA: 0x0006B3D8 File Offset: 0x000695D8
			public ControlMapper.GUIInputField GetGUIInputField(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int fieldIndex)
			{
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = this.GetActionEntry(mapCategoryId, actionId, axisRange);
				if (actionEntry == null)
				{
					return null;
				}
				return actionEntry.GetGUIInputField(controllerType, fieldIndex);
			}

			// Token: 0x060019D7 RID: 6615 RVA: 0x0006B400 File Offset: 0x00069600
			private ControlMapper.InputGridEntryList.ActionEntry GetActionEntry(int mapCategoryId, int actionId, AxisRange axisRange)
			{
				if (actionId < 0)
				{
					return null;
				}
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return null;
				}
				return mapCategoryEntry.GetActionEntry(actionId, axisRange);
			}

			// Token: 0x060019D8 RID: 6616 RVA: 0x0001413A File Offset: 0x0001233A
			private ControlMapper.InputGridEntryList.ActionEntry GetActionEntry(int mapCategoryId, InputAction action, AxisRange axisRange)
			{
				if (action == null)
				{
					return null;
				}
				return this.GetActionEntry(mapCategoryId, action.id, axisRange);
			}

			// Token: 0x060019D9 RID: 6617 RVA: 0x0001414F File Offset: 0x0001234F
			public IEnumerable<ControlMapper.InputActionSet> GetActionSets(int mapCategoryId)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					yield break;
				}
				List<ControlMapper.InputGridEntryList.ActionEntry> list = mapCategoryEntry.actionList;
				int count = ((list != null) ? list.Count : 0);
				int num;
				for (int i = 0; i < count; i = num + 1)
				{
					yield return list[i].actionSet;
					num = i;
				}
				yield break;
			}

			// Token: 0x060019DA RID: 6618 RVA: 0x0006B430 File Offset: 0x00069630
			public void SetFieldsActive(int mapCategoryId, bool state)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return;
				}
				List<ControlMapper.InputGridEntryList.ActionEntry> actionList = mapCategoryEntry.actionList;
				int num = ((actionList != null) ? actionList.Count : 0);
				for (int i = 0; i < num; i++)
				{
					actionList[i].SetFieldsActive(state);
				}
			}

			// Token: 0x060019DB RID: 6619 RVA: 0x0006B47C File Offset: 0x0006967C
			public void SetLabel(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int index, string label)
			{
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = this.GetActionEntry(mapCategoryId, actionId, axisRange);
				if (actionEntry == null)
				{
					return;
				}
				actionEntry.SetFieldLabel(controllerType, index, label);
			}

			// Token: 0x060019DC RID: 6620 RVA: 0x0006B4A4 File Offset: 0x000696A4
			public void PopulateField(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int controllerId, int index, int actionElementMapId, GameObject buttonDisplay, bool invert)
			{
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = this.GetActionEntry(mapCategoryId, actionId, axisRange);
				if (actionEntry == null)
				{
					return;
				}
				actionEntry.PopulateField(controllerType, controllerId, index, actionElementMapId, buttonDisplay, invert);
			}

			// Token: 0x060019DD RID: 6621 RVA: 0x0006B4D4 File Offset: 0x000696D4
			public void SetFixedFieldData(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int controllerId)
			{
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = this.GetActionEntry(mapCategoryId, actionId, axisRange);
				if (actionEntry == null)
				{
					return;
				}
				actionEntry.SetFixedFieldData(controllerType, controllerId);
			}

			// Token: 0x060019DE RID: 6622 RVA: 0x0006B4FC File Offset: 0x000696FC
			public void InitializeFields(int mapCategoryId)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return;
				}
				List<ControlMapper.InputGridEntryList.ActionEntry> actionList = mapCategoryEntry.actionList;
				int num = ((actionList != null) ? actionList.Count : 0);
				for (int i = 0; i < num; i++)
				{
					actionList[i].Initialize();
				}
			}

			// Token: 0x060019DF RID: 6623 RVA: 0x0006B548 File Offset: 0x00069748
			public void Show(int mapCategoryId)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return;
				}
				mapCategoryEntry.SetAllActive(true);
			}

			// Token: 0x060019E0 RID: 6624 RVA: 0x0006B570 File Offset: 0x00069770
			public void HideAll()
			{
				for (int i = 0; i < this.entries.Count; i++)
				{
					this.entries[i].SetAllActive(false);
				}
			}

			// Token: 0x060019E1 RID: 6625 RVA: 0x0006B5A8 File Offset: 0x000697A8
			public void ClearLabels(int mapCategoryId)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return;
				}
				List<ControlMapper.InputGridEntryList.ActionEntry> actionList = mapCategoryEntry.actionList;
				int num = ((actionList != null) ? actionList.Count : 0);
				for (int i = 0; i < num; i++)
				{
					actionList[i].ClearLabels();
				}
			}

			// Token: 0x060019E2 RID: 6626 RVA: 0x00014166 File Offset: 0x00012366
			public void Clear()
			{
				this.entries.Clear();
			}

			// Token: 0x04001CA7 RID: 7335
			private ControlMapper.IndexedDictionary<int, ControlMapper.InputGridEntryList.MapCategoryEntry> entries;

			// Token: 0x02000448 RID: 1096
			private class MapCategoryEntry
			{
				// Token: 0x170004FD RID: 1277
				// (get) Token: 0x060019E3 RID: 6627 RVA: 0x00014173 File Offset: 0x00012373
				public List<ControlMapper.InputGridEntryList.ActionEntry> actionList
				{
					get
					{
						return this._actionList;
					}
				}

				// Token: 0x170004FE RID: 1278
				// (get) Token: 0x060019E4 RID: 6628 RVA: 0x0001417B File Offset: 0x0001237B
				public ControlMapper.IndexedDictionary<int, ControlMapper.InputGridEntryList.ActionCategoryEntry> actionCategoryList
				{
					get
					{
						return this._actionCategoryList;
					}
				}

				// Token: 0x170004FF RID: 1279
				// (get) Token: 0x060019E5 RID: 6629 RVA: 0x00014183 File Offset: 0x00012383
				// (set) Token: 0x060019E6 RID: 6630 RVA: 0x0001418B File Offset: 0x0001238B
				public float columnHeight
				{
					get
					{
						return this._columnHeight;
					}
					set
					{
						this._columnHeight = value;
					}
				}

				// Token: 0x060019E7 RID: 6631 RVA: 0x00014194 File Offset: 0x00012394
				public MapCategoryEntry()
				{
					this._actionList = new List<ControlMapper.InputGridEntryList.ActionEntry>();
					this._actionCategoryList = new ControlMapper.IndexedDictionary<int, ControlMapper.InputGridEntryList.ActionCategoryEntry>();
				}

				// Token: 0x060019E8 RID: 6632 RVA: 0x0006B5F4 File Offset: 0x000697F4
				public ControlMapper.InputGridEntryList.ActionEntry GetActionEntry(int actionId, AxisRange axisRange)
				{
					int num = this.IndexOfActionEntry(actionId, axisRange);
					if (num < 0)
					{
						return null;
					}
					return this._actionList[num];
				}

				// Token: 0x060019E9 RID: 6633 RVA: 0x0006B61C File Offset: 0x0006981C
				public int IndexOfActionEntry(int actionId, AxisRange axisRange)
				{
					int count = this._actionList.Count;
					for (int i = 0; i < count; i++)
					{
						if (this._actionList[i].Matches(actionId, axisRange))
						{
							return i;
						}
					}
					return -1;
				}

				// Token: 0x060019EA RID: 6634 RVA: 0x000141B2 File Offset: 0x000123B2
				public bool ContainsActionEntry(int actionId, AxisRange axisRange)
				{
					return this.IndexOfActionEntry(actionId, axisRange) >= 0;
				}

				// Token: 0x060019EB RID: 6635 RVA: 0x0006B65C File Offset: 0x0006985C
				public ControlMapper.InputGridEntryList.ActionEntry AddAction(InputAction action, AxisRange axisRange)
				{
					if (action == null)
					{
						return null;
					}
					if (this.ContainsActionEntry(action.id, axisRange))
					{
						return null;
					}
					this._actionList.Add(new ControlMapper.InputGridEntryList.ActionEntry(action, axisRange));
					return this._actionList[this._actionList.Count - 1];
				}

				// Token: 0x060019EC RID: 6636 RVA: 0x000141C2 File Offset: 0x000123C2
				public ControlMapper.InputGridEntryList.ActionCategoryEntry GetActionCategoryEntry(int actionCategoryId)
				{
					if (!this._actionCategoryList.ContainsKey(actionCategoryId))
					{
						return null;
					}
					return this._actionCategoryList.Get(actionCategoryId);
				}

				// Token: 0x060019ED RID: 6637 RVA: 0x000141E0 File Offset: 0x000123E0
				public ControlMapper.InputGridEntryList.ActionCategoryEntry AddActionCategory(int actionCategoryId)
				{
					if (actionCategoryId < 0)
					{
						return null;
					}
					if (this._actionCategoryList.ContainsKey(actionCategoryId))
					{
						return null;
					}
					this._actionCategoryList.Add(actionCategoryId, new ControlMapper.InputGridEntryList.ActionCategoryEntry(actionCategoryId));
					return this._actionCategoryList.Get(actionCategoryId);
				}

				// Token: 0x060019EE RID: 6638 RVA: 0x0006B6AC File Offset: 0x000698AC
				public void SetAllActive(bool state)
				{
					for (int i = 0; i < this._actionCategoryList.Count; i++)
					{
						this._actionCategoryList[i].SetActive(state);
					}
					for (int j = 0; j < this._actionList.Count; j++)
					{
						this._actionList[j].SetActive(state);
					}
				}

				// Token: 0x04001CA8 RID: 7336
				private List<ControlMapper.InputGridEntryList.ActionEntry> _actionList;

				// Token: 0x04001CA9 RID: 7337
				private ControlMapper.IndexedDictionary<int, ControlMapper.InputGridEntryList.ActionCategoryEntry> _actionCategoryList;

				// Token: 0x04001CAA RID: 7338
				private float _columnHeight;
			}

			// Token: 0x02000449 RID: 1097
			private class ActionEntry
			{
				// Token: 0x060019EF RID: 6639 RVA: 0x00014216 File Offset: 0x00012416
				public ActionEntry(InputAction action, AxisRange axisRange)
				{
					this.action = action;
					this.axisRange = axisRange;
					this.actionSet = new ControlMapper.InputActionSet(action.id, axisRange);
					this.fieldSets = new ControlMapper.IndexedDictionary<int, ControlMapper.InputGridEntryList.FieldSet>();
				}

				// Token: 0x060019F0 RID: 6640 RVA: 0x00014249 File Offset: 0x00012449
				public void SetLabel(ControlMapper.GUILabel label)
				{
					this.label = label;
				}

				// Token: 0x060019F1 RID: 6641 RVA: 0x00014252 File Offset: 0x00012452
				public bool Matches(int actionId, AxisRange axisRange)
				{
					return this.action.id == actionId && this.axisRange == axisRange;
				}

				// Token: 0x060019F2 RID: 6642 RVA: 0x00014270 File Offset: 0x00012470
				public void AddInputFieldSet(ControllerType controllerType, GameObject fieldSetContainer)
				{
					if (this.fieldSets.ContainsKey(controllerType))
					{
						return;
					}
					this.fieldSets.Add(controllerType, new ControlMapper.InputGridEntryList.FieldSet(fieldSetContainer));
				}

				// Token: 0x060019F3 RID: 6643 RVA: 0x0006B70C File Offset: 0x0006990C
				public void AddInputField(ControllerType controllerType, int fieldIndex, ControlMapper.GUIInputField inputField)
				{
					if (!this.fieldSets.ContainsKey(controllerType))
					{
						return;
					}
					ControlMapper.InputGridEntryList.FieldSet fieldSet = this.fieldSets.Get(controllerType);
					if (fieldSet.fields.ContainsKey(fieldIndex))
					{
						return;
					}
					fieldSet.fields.Add(fieldIndex, inputField);
				}

				// Token: 0x060019F4 RID: 6644 RVA: 0x0006B754 File Offset: 0x00069954
				public ControlMapper.GUIInputField GetGUIInputField(ControllerType controllerType, int fieldIndex)
				{
					if (!this.fieldSets.ContainsKey(controllerType))
					{
						return null;
					}
					if (!this.fieldSets.Get(controllerType).fields.ContainsKey(fieldIndex))
					{
						return null;
					}
					return this.fieldSets.Get(controllerType).fields.Get(fieldIndex);
				}

				// Token: 0x060019F5 RID: 6645 RVA: 0x00014293 File Offset: 0x00012493
				public bool Contains(ControllerType controllerType, int fieldId)
				{
					return this.fieldSets.ContainsKey(controllerType) && this.fieldSets.Get(controllerType).fields.ContainsKey(fieldId);
				}

				// Token: 0x060019F6 RID: 6646 RVA: 0x0006B7A4 File Offset: 0x000699A4
				public void SetFieldLabel(ControllerType controllerType, int index, string label)
				{
					if (!this.fieldSets.ContainsKey(controllerType))
					{
						return;
					}
					if (!this.fieldSets.Get(controllerType).fields.ContainsKey(index))
					{
						return;
					}
					this.fieldSets.Get(controllerType).fields.Get(index).SetLabel(label);
				}

				// Token: 0x060019F7 RID: 6647 RVA: 0x0006B7F8 File Offset: 0x000699F8
				public void PopulateField(ControllerType controllerType, int controllerId, int index, int actionElementMapId, GameObject buttonDisplay, bool invert)
				{
					if (!this.fieldSets.ContainsKey(controllerType))
					{
						return;
					}
					if (!this.fieldSets.Get(controllerType).fields.ContainsKey(index))
					{
						return;
					}
					ControlMapper.GUIInputField guiinputField = this.fieldSets.Get(controllerType).fields.Get(index);
					guiinputField.SetDisplay(buttonDisplay);
					guiinputField.actionElementMapId = actionElementMapId;
					guiinputField.controllerId = controllerId;
					if (guiinputField.hasToggle)
					{
						guiinputField.toggle.SetInteractible(true, false);
						guiinputField.toggle.SetToggleState(invert);
						guiinputField.toggle.actionElementMapId = actionElementMapId;
					}
				}

				// Token: 0x060019F8 RID: 6648 RVA: 0x0006B88C File Offset: 0x00069A8C
				public void SetFixedFieldData(ControllerType controllerType, int controllerId)
				{
					if (!this.fieldSets.ContainsKey(controllerType))
					{
						return;
					}
					ControlMapper.InputGridEntryList.FieldSet fieldSet = this.fieldSets.Get(controllerType);
					int count = fieldSet.fields.Count;
					for (int i = 0; i < count; i++)
					{
						fieldSet.fields[i].controllerId = controllerId;
					}
				}

				// Token: 0x060019F9 RID: 6649 RVA: 0x0006B8E0 File Offset: 0x00069AE0
				public void Initialize()
				{
					for (int i = 0; i < this.fieldSets.Count; i++)
					{
						ControlMapper.InputGridEntryList.FieldSet fieldSet = this.fieldSets[i];
						int count = fieldSet.fields.Count;
						for (int j = 0; j < count; j++)
						{
							ControlMapper.GUIInputField guiinputField = fieldSet.fields[j];
							if (guiinputField.hasToggle)
							{
								guiinputField.toggle.SetInteractible(false, false);
								guiinputField.toggle.SetToggleState(false);
								guiinputField.toggle.actionElementMapId = -1;
							}
							guiinputField.SetDisplay(null);
							guiinputField.actionElementMapId = -1;
							guiinputField.controllerId = -1;
						}
					}
				}

				// Token: 0x060019FA RID: 6650 RVA: 0x0006B988 File Offset: 0x00069B88
				public void SetActive(bool state)
				{
					if (this.label != null)
					{
						this.label.SetActive(state);
					}
					int count = this.fieldSets.Count;
					for (int i = 0; i < count; i++)
					{
						this.fieldSets[i].groupContainer.SetActive(state);
					}
				}

				// Token: 0x060019FB RID: 6651 RVA: 0x0006B9D8 File Offset: 0x00069BD8
				public void ClearLabels()
				{
					for (int i = 0; i < this.fieldSets.Count; i++)
					{
						ControlMapper.InputGridEntryList.FieldSet fieldSet = this.fieldSets[i];
						int count = fieldSet.fields.Count;
						for (int j = 0; j < count; j++)
						{
							fieldSet.fields[j].SetDisplay(null);
						}
					}
				}

				// Token: 0x060019FC RID: 6652 RVA: 0x0006BA34 File Offset: 0x00069C34
				public void SetFieldsActive(bool state)
				{
					for (int i = 0; i < this.fieldSets.Count; i++)
					{
						ControlMapper.InputGridEntryList.FieldSet fieldSet = this.fieldSets[i];
						int count = fieldSet.fields.Count;
						for (int j = 0; j < count; j++)
						{
							ControlMapper.GUIInputField guiinputField = fieldSet.fields[j];
							guiinputField.SetInteractible(state, false);
							if (guiinputField.hasToggle && (!state || guiinputField.toggle.actionElementMapId >= 0))
							{
								guiinputField.toggle.SetInteractible(state, false);
							}
						}
					}
				}

				// Token: 0x04001CAB RID: 7339
				private ControlMapper.IndexedDictionary<int, ControlMapper.InputGridEntryList.FieldSet> fieldSets;

				// Token: 0x04001CAC RID: 7340
				public ControlMapper.GUILabel label;

				// Token: 0x04001CAD RID: 7341
				public readonly InputAction action;

				// Token: 0x04001CAE RID: 7342
				public readonly AxisRange axisRange;

				// Token: 0x04001CAF RID: 7343
				public readonly ControlMapper.InputActionSet actionSet;
			}

			// Token: 0x0200044A RID: 1098
			private class FieldSet
			{
				// Token: 0x060019FD RID: 6653 RVA: 0x000142C1 File Offset: 0x000124C1
				public FieldSet(GameObject groupContainer)
				{
					this.groupContainer = groupContainer;
					this.fields = new ControlMapper.IndexedDictionary<int, ControlMapper.GUIInputField>();
				}

				// Token: 0x04001CB0 RID: 7344
				public readonly GameObject groupContainer;

				// Token: 0x04001CB1 RID: 7345
				public readonly ControlMapper.IndexedDictionary<int, ControlMapper.GUIInputField> fields;
			}

			// Token: 0x0200044B RID: 1099
			private class ActionCategoryEntry
			{
				// Token: 0x060019FE RID: 6654 RVA: 0x000142DB File Offset: 0x000124DB
				public ActionCategoryEntry(int actionCategoryId)
				{
					this.actionCategoryId = actionCategoryId;
				}

				// Token: 0x060019FF RID: 6655 RVA: 0x000142EA File Offset: 0x000124EA
				public void SetLabel(ControlMapper.GUILabel label)
				{
					this.label = label;
				}

				// Token: 0x06001A00 RID: 6656 RVA: 0x000142F3 File Offset: 0x000124F3
				public void SetActive(bool state)
				{
					if (this.label != null)
					{
						this.label.SetActive(state);
					}
				}

				// Token: 0x04001CB2 RID: 7346
				public readonly int actionCategoryId;

				// Token: 0x04001CB3 RID: 7347
				public ControlMapper.GUILabel label;
			}
		}

		// Token: 0x0200044D RID: 1101
		private class WindowManager
		{
			// Token: 0x17000502 RID: 1282
			// (get) Token: 0x06001A09 RID: 6665 RVA: 0x0006BBCC File Offset: 0x00069DCC
			public bool isWindowOpen
			{
				get
				{
					for (int i = this.windows.Count - 1; i >= 0; i--)
					{
						if (!(this.windows[i] == null))
						{
							return true;
						}
					}
					return false;
				}
			}

			// Token: 0x17000503 RID: 1283
			// (get) Token: 0x06001A0A RID: 6666 RVA: 0x0006BC08 File Offset: 0x00069E08
			public Window topWindow
			{
				get
				{
					for (int i = this.windows.Count - 1; i >= 0; i--)
					{
						if (!(this.windows[i] == null))
						{
							return this.windows[i];
						}
					}
					return null;
				}
			}

			// Token: 0x06001A0B RID: 6667 RVA: 0x0006BC50 File Offset: 0x00069E50
			public WindowManager(GameObject windowPrefab, GameObject faderPrefab, Transform parent)
			{
				this.windowPrefab = windowPrefab;
				this.parent = parent;
				this.windows = new List<Window>();
				this.fader = Object.Instantiate<GameObject>(faderPrefab);
				this.fader.transform.SetParent(parent, false);
				this.fader.GetComponent<RectTransform>().localScale = Vector2.one;
				this.SetFaderActive(false);
			}

			// Token: 0x06001A0C RID: 6668 RVA: 0x00014333 File Offset: 0x00012533
			public Window OpenWindow(string name, int width, int height)
			{
				Window window = this.InstantiateWindow(name, width, height);
				this.UpdateFader();
				return window;
			}

			// Token: 0x06001A0D RID: 6669 RVA: 0x00014344 File Offset: 0x00012544
			public Window OpenWindow(GameObject windowPrefab, string name)
			{
				if (windowPrefab == null)
				{
					Debug.LogError("Rewired Control Mapper: Window Prefab is null!");
					return null;
				}
				Window window = this.InstantiateWindow(name, windowPrefab);
				this.UpdateFader();
				return window;
			}

			// Token: 0x06001A0E RID: 6670 RVA: 0x0006BCBC File Offset: 0x00069EBC
			public void CloseTop()
			{
				for (int i = this.windows.Count - 1; i >= 0; i--)
				{
					if (!(this.windows[i] == null))
					{
						this.DestroyWindow(this.windows[i]);
						this.windows.RemoveAt(i);
						break;
					}
					this.windows.RemoveAt(i);
				}
				this.UpdateFader();
			}

			// Token: 0x06001A0F RID: 6671 RVA: 0x00014369 File Offset: 0x00012569
			public void CloseWindow(int windowId)
			{
				this.CloseWindow(this.GetWindow(windowId));
			}

			// Token: 0x06001A10 RID: 6672 RVA: 0x0006BD2C File Offset: 0x00069F2C
			public void CloseWindow(Window window)
			{
				if (window == null)
				{
					return;
				}
				for (int i = this.windows.Count - 1; i >= 0; i--)
				{
					if (this.windows[i] == null)
					{
						this.windows.RemoveAt(i);
					}
					else if (!(this.windows[i] != window))
					{
						this.DestroyWindow(this.windows[i]);
						this.windows.RemoveAt(i);
						break;
					}
				}
				this.UpdateFader();
				this.FocusTopWindow();
			}

			// Token: 0x06001A11 RID: 6673 RVA: 0x0006BDC0 File Offset: 0x00069FC0
			public void CloseAll()
			{
				this.SetFaderActive(false);
				for (int i = this.windows.Count - 1; i >= 0; i--)
				{
					if (this.windows[i] == null)
					{
						this.windows.RemoveAt(i);
					}
					else
					{
						this.DestroyWindow(this.windows[i]);
						this.windows.RemoveAt(i);
					}
				}
				this.UpdateFader();
			}

			// Token: 0x06001A12 RID: 6674 RVA: 0x0006BE34 File Offset: 0x0006A034
			public void CancelAll()
			{
				if (!this.isWindowOpen)
				{
					return;
				}
				for (int i = this.windows.Count - 1; i >= 0; i--)
				{
					if (!(this.windows[i] == null))
					{
						this.windows[i].Cancel();
					}
				}
				this.CloseAll();
			}

			// Token: 0x06001A13 RID: 6675 RVA: 0x0006BE90 File Offset: 0x0006A090
			public Window GetWindow(int windowId)
			{
				if (windowId < 0)
				{
					return null;
				}
				for (int i = this.windows.Count - 1; i >= 0; i--)
				{
					if (!(this.windows[i] == null) && this.windows[i].id == windowId)
					{
						return this.windows[i];
					}
				}
				return null;
			}

			// Token: 0x06001A14 RID: 6676 RVA: 0x00014378 File Offset: 0x00012578
			public bool IsFocused(int windowId)
			{
				return windowId >= 0 && !(this.topWindow == null) && this.topWindow.id == windowId;
			}

			// Token: 0x06001A15 RID: 6677 RVA: 0x0001439E File Offset: 0x0001259E
			public void Focus(int windowId)
			{
				this.Focus(this.GetWindow(windowId));
			}

			// Token: 0x06001A16 RID: 6678 RVA: 0x000143AD File Offset: 0x000125AD
			public void Focus(Window window)
			{
				if (window == null)
				{
					return;
				}
				window.TakeInputFocus();
				this.DefocusOtherWindows(window.id);
			}

			// Token: 0x06001A17 RID: 6679 RVA: 0x0006BEF4 File Offset: 0x0006A0F4
			private void DefocusOtherWindows(int focusedWindowId)
			{
				if (focusedWindowId < 0)
				{
					return;
				}
				for (int i = this.windows.Count - 1; i >= 0; i--)
				{
					if (!(this.windows[i] == null) && this.windows[i].id != focusedWindowId)
					{
						this.windows[i].Disable();
					}
				}
			}

			// Token: 0x06001A18 RID: 6680 RVA: 0x0006BF58 File Offset: 0x0006A158
			private void UpdateFader()
			{
				if (!this.isWindowOpen)
				{
					this.SetFaderActive(false);
					return;
				}
				if (this.topWindow.transform.parent == null)
				{
					return;
				}
				this.SetFaderActive(true);
				this.fader.transform.SetAsLastSibling();
				int siblingIndex = this.topWindow.transform.GetSiblingIndex();
				this.fader.transform.SetSiblingIndex(siblingIndex);
			}

			// Token: 0x06001A19 RID: 6681 RVA: 0x000143CB File Offset: 0x000125CB
			private void FocusTopWindow()
			{
				if (this.topWindow == null)
				{
					return;
				}
				this.topWindow.TakeInputFocus();
			}

			// Token: 0x06001A1A RID: 6682 RVA: 0x000143E7 File Offset: 0x000125E7
			private void SetFaderActive(bool state)
			{
				this.fader.SetActive(state);
			}

			// Token: 0x06001A1B RID: 6683 RVA: 0x0006BFC8 File Offset: 0x0006A1C8
			private Window InstantiateWindow(string name, int width, int height)
			{
				if (string.IsNullOrEmpty(name))
				{
					name = "Window";
				}
				GameObject gameObject = UITools.InstantiateGUIObject<Window>(this.windowPrefab, this.parent, name);
				if (gameObject == null)
				{
					return null;
				}
				Window component = gameObject.GetComponent<Window>();
				if (component != null)
				{
					component.Initialize(this.GetNewId(), new Func<int, bool>(this.IsFocused));
					this.windows.Add(component);
					component.SetSize(width, height);
				}
				return component;
			}

			// Token: 0x06001A1C RID: 6684 RVA: 0x0006C040 File Offset: 0x0006A240
			private Window InstantiateWindow(string name, GameObject windowPrefab)
			{
				if (string.IsNullOrEmpty(name))
				{
					name = "Window";
				}
				if (windowPrefab == null)
				{
					return null;
				}
				GameObject gameObject = UITools.InstantiateGUIObject<Window>(windowPrefab, this.parent, name);
				if (gameObject == null)
				{
					return null;
				}
				Window component = gameObject.GetComponent<Window>();
				if (component != null)
				{
					component.Initialize(this.GetNewId(), new Func<int, bool>(this.IsFocused));
					this.windows.Add(component);
				}
				return component;
			}

			// Token: 0x06001A1D RID: 6685 RVA: 0x000143F5 File Offset: 0x000125F5
			private void DestroyWindow(Window window)
			{
				if (window == null)
				{
					return;
				}
				Object.Destroy(window.gameObject);
			}

			// Token: 0x06001A1E RID: 6686 RVA: 0x0001440C File Offset: 0x0001260C
			private int GetNewId()
			{
				int num = this.idCounter;
				this.idCounter++;
				return num;
			}

			// Token: 0x06001A1F RID: 6687 RVA: 0x00014422 File Offset: 0x00012622
			public void ClearCompletely()
			{
				this.CloseAll();
				if (this.fader != null)
				{
					Object.Destroy(this.fader);
				}
			}

			// Token: 0x04001CBD RID: 7357
			private List<Window> windows;

			// Token: 0x04001CBE RID: 7358
			private GameObject windowPrefab;

			// Token: 0x04001CBF RID: 7359
			private Transform parent;

			// Token: 0x04001CC0 RID: 7360
			private GameObject fader;

			// Token: 0x04001CC1 RID: 7361
			private int idCounter;
		}
	}
}
