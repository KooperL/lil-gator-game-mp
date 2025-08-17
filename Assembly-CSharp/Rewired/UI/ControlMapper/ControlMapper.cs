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
	[AddComponentMenu("")]
	public class ControlMapper : MonoBehaviour
	{
		// (add) Token: 0x0600182F RID: 6191 RVA: 0x000129D6 File Offset: 0x00010BD6
		// (remove) Token: 0x06001830 RID: 6192 RVA: 0x000129EF File Offset: 0x00010BEF
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

		// (add) Token: 0x06001831 RID: 6193 RVA: 0x00012A08 File Offset: 0x00010C08
		// (remove) Token: 0x06001832 RID: 6194 RVA: 0x00012A21 File Offset: 0x00010C21
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

		// (add) Token: 0x06001833 RID: 6195 RVA: 0x00012A3A File Offset: 0x00010C3A
		// (remove) Token: 0x06001834 RID: 6196 RVA: 0x00012A53 File Offset: 0x00010C53
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

		// (add) Token: 0x06001835 RID: 6197 RVA: 0x00012A6C File Offset: 0x00010C6C
		// (remove) Token: 0x06001836 RID: 6198 RVA: 0x00012A85 File Offset: 0x00010C85
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

		// (add) Token: 0x06001837 RID: 6199 RVA: 0x00012A9E File Offset: 0x00010C9E
		// (remove) Token: 0x06001838 RID: 6200 RVA: 0x00012AB7 File Offset: 0x00010CB7
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

		// (add) Token: 0x06001839 RID: 6201 RVA: 0x00012AD0 File Offset: 0x00010CD0
		// (remove) Token: 0x0600183A RID: 6202 RVA: 0x00012AE9 File Offset: 0x00010CE9
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

		// (add) Token: 0x0600183B RID: 6203 RVA: 0x00012B02 File Offset: 0x00010D02
		// (remove) Token: 0x0600183C RID: 6204 RVA: 0x00012B10 File Offset: 0x00010D10
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

		// (add) Token: 0x0600183D RID: 6205 RVA: 0x00012B1E File Offset: 0x00010D1E
		// (remove) Token: 0x0600183E RID: 6206 RVA: 0x00012B2C File Offset: 0x00010D2C
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

		// (add) Token: 0x0600183F RID: 6207 RVA: 0x00012B3A File Offset: 0x00010D3A
		// (remove) Token: 0x06001840 RID: 6208 RVA: 0x00012B48 File Offset: 0x00010D48
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

		// (add) Token: 0x06001841 RID: 6209 RVA: 0x00012B56 File Offset: 0x00010D56
		// (remove) Token: 0x06001842 RID: 6210 RVA: 0x00012B64 File Offset: 0x00010D64
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

		// (add) Token: 0x06001843 RID: 6211 RVA: 0x00012B72 File Offset: 0x00010D72
		// (remove) Token: 0x06001844 RID: 6212 RVA: 0x00012B80 File Offset: 0x00010D80
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

		// (add) Token: 0x06001845 RID: 6213 RVA: 0x00012B8E File Offset: 0x00010D8E
		// (remove) Token: 0x06001846 RID: 6214 RVA: 0x00012B9C File Offset: 0x00010D9C
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

		// (get) Token: 0x06001847 RID: 6215 RVA: 0x00012BAA File Offset: 0x00010DAA
		// (set) Token: 0x06001848 RID: 6216 RVA: 0x00012BB2 File Offset: 0x00010DB2
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

		// (get) Token: 0x06001849 RID: 6217 RVA: 0x00012BC2 File Offset: 0x00010DC2
		// (set) Token: 0x0600184A RID: 6218 RVA: 0x00012BCA File Offset: 0x00010DCA
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
					global::UnityEngine.Object.DontDestroyOnLoad(base.transform.gameObject);
				}
				this._dontDestroyOnLoad = value;
			}
		}

		// (get) Token: 0x0600184B RID: 6219 RVA: 0x00012BEF File Offset: 0x00010DEF
		// (set) Token: 0x0600184C RID: 6220 RVA: 0x00012BF7 File Offset: 0x00010DF7
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

		// (get) Token: 0x0600184D RID: 6221 RVA: 0x00012C07 File Offset: 0x00010E07
		// (set) Token: 0x0600184E RID: 6222 RVA: 0x00012C0F File Offset: 0x00010E0F
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

		// (get) Token: 0x0600184F RID: 6223 RVA: 0x00012C1F File Offset: 0x00010E1F
		// (set) Token: 0x06001850 RID: 6224 RVA: 0x00012C27 File Offset: 0x00010E27
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

		// (get) Token: 0x06001851 RID: 6225 RVA: 0x00012C37 File Offset: 0x00010E37
		// (set) Token: 0x06001852 RID: 6226 RVA: 0x00012C50 File Offset: 0x00010E50
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

		// (get) Token: 0x06001853 RID: 6227 RVA: 0x00012C60 File Offset: 0x00010E60
		// (set) Token: 0x06001854 RID: 6228 RVA: 0x00012C68 File Offset: 0x00010E68
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

		// (get) Token: 0x06001855 RID: 6229 RVA: 0x00012C78 File Offset: 0x00010E78
		// (set) Token: 0x06001856 RID: 6230 RVA: 0x00012C80 File Offset: 0x00010E80
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

		// (get) Token: 0x06001857 RID: 6231 RVA: 0x00012C90 File Offset: 0x00010E90
		// (set) Token: 0x06001858 RID: 6232 RVA: 0x00012C98 File Offset: 0x00010E98
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

		// (get) Token: 0x06001859 RID: 6233 RVA: 0x00012CA8 File Offset: 0x00010EA8
		// (set) Token: 0x0600185A RID: 6234 RVA: 0x00012CB0 File Offset: 0x00010EB0
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

		// (get) Token: 0x0600185B RID: 6235 RVA: 0x00012CC0 File Offset: 0x00010EC0
		// (set) Token: 0x0600185C RID: 6236 RVA: 0x00012CC8 File Offset: 0x00010EC8
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

		// (get) Token: 0x0600185D RID: 6237 RVA: 0x00012CD8 File Offset: 0x00010ED8
		// (set) Token: 0x0600185E RID: 6238 RVA: 0x00012CE0 File Offset: 0x00010EE0
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

		// (get) Token: 0x0600185F RID: 6239 RVA: 0x00012CF0 File Offset: 0x00010EF0
		// (set) Token: 0x06001860 RID: 6240 RVA: 0x00012CF8 File Offset: 0x00010EF8
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

		// (get) Token: 0x06001861 RID: 6241 RVA: 0x00012D08 File Offset: 0x00010F08
		// (set) Token: 0x06001862 RID: 6242 RVA: 0x00012D10 File Offset: 0x00010F10
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

		// (get) Token: 0x06001863 RID: 6243 RVA: 0x00012D20 File Offset: 0x00010F20
		// (set) Token: 0x06001864 RID: 6244 RVA: 0x00012D28 File Offset: 0x00010F28
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

		// (get) Token: 0x06001865 RID: 6245 RVA: 0x00012D38 File Offset: 0x00010F38
		// (set) Token: 0x06001866 RID: 6246 RVA: 0x00012D40 File Offset: 0x00010F40
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

		// (get) Token: 0x06001867 RID: 6247 RVA: 0x00012D50 File Offset: 0x00010F50
		// (set) Token: 0x06001868 RID: 6248 RVA: 0x00012D58 File Offset: 0x00010F58
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

		// (get) Token: 0x06001869 RID: 6249 RVA: 0x00012D68 File Offset: 0x00010F68
		// (set) Token: 0x0600186A RID: 6250 RVA: 0x00012D70 File Offset: 0x00010F70
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

		// (get) Token: 0x0600186B RID: 6251 RVA: 0x00012D80 File Offset: 0x00010F80
		// (set) Token: 0x0600186C RID: 6252 RVA: 0x00012D88 File Offset: 0x00010F88
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

		// (get) Token: 0x0600186D RID: 6253 RVA: 0x00012D98 File Offset: 0x00010F98
		// (set) Token: 0x0600186E RID: 6254 RVA: 0x00012DA0 File Offset: 0x00010FA0
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

		// (get) Token: 0x0600186F RID: 6255 RVA: 0x00012DB0 File Offset: 0x00010FB0
		// (set) Token: 0x06001870 RID: 6256 RVA: 0x00012DB8 File Offset: 0x00010FB8
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

		// (get) Token: 0x06001871 RID: 6257 RVA: 0x00012DC8 File Offset: 0x00010FC8
		// (set) Token: 0x06001872 RID: 6258 RVA: 0x00012DD0 File Offset: 0x00010FD0
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

		// (get) Token: 0x06001873 RID: 6259 RVA: 0x00012DE0 File Offset: 0x00010FE0
		// (set) Token: 0x06001874 RID: 6260 RVA: 0x00012DE8 File Offset: 0x00010FE8
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

		// (get) Token: 0x06001875 RID: 6261 RVA: 0x00012DF8 File Offset: 0x00010FF8
		// (set) Token: 0x06001876 RID: 6262 RVA: 0x00012E00 File Offset: 0x00011000
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

		// (get) Token: 0x06001877 RID: 6263 RVA: 0x00012E10 File Offset: 0x00011010
		// (set) Token: 0x06001878 RID: 6264 RVA: 0x00012E18 File Offset: 0x00011018
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

		// (get) Token: 0x06001879 RID: 6265 RVA: 0x00012E28 File Offset: 0x00011028
		// (set) Token: 0x0600187A RID: 6266 RVA: 0x00012E30 File Offset: 0x00011030
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

		// (get) Token: 0x0600187B RID: 6267 RVA: 0x00012E40 File Offset: 0x00011040
		// (set) Token: 0x0600187C RID: 6268 RVA: 0x00012E48 File Offset: 0x00011048
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

		// (get) Token: 0x0600187D RID: 6269 RVA: 0x00012E58 File Offset: 0x00011058
		// (set) Token: 0x0600187E RID: 6270 RVA: 0x00012E60 File Offset: 0x00011060
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

		// (get) Token: 0x0600187F RID: 6271 RVA: 0x00012E70 File Offset: 0x00011070
		// (set) Token: 0x06001880 RID: 6272 RVA: 0x00012E78 File Offset: 0x00011078
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

		// (get) Token: 0x06001881 RID: 6273 RVA: 0x00012E88 File Offset: 0x00011088
		// (set) Token: 0x06001882 RID: 6274 RVA: 0x00012E90 File Offset: 0x00011090
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

		// (get) Token: 0x06001883 RID: 6275 RVA: 0x00012EA0 File Offset: 0x000110A0
		// (set) Token: 0x06001884 RID: 6276 RVA: 0x00012EA8 File Offset: 0x000110A8
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

		// (get) Token: 0x06001885 RID: 6277 RVA: 0x00012EB8 File Offset: 0x000110B8
		// (set) Token: 0x06001886 RID: 6278 RVA: 0x00012EC0 File Offset: 0x000110C0
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

		// (get) Token: 0x06001887 RID: 6279 RVA: 0x00012ED0 File Offset: 0x000110D0
		// (set) Token: 0x06001888 RID: 6280 RVA: 0x00012ED8 File Offset: 0x000110D8
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

		// (get) Token: 0x06001889 RID: 6281 RVA: 0x00012EE8 File Offset: 0x000110E8
		// (set) Token: 0x0600188A RID: 6282 RVA: 0x00012EF0 File Offset: 0x000110F0
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

		// (get) Token: 0x0600188B RID: 6283 RVA: 0x00012F00 File Offset: 0x00011100
		// (set) Token: 0x0600188C RID: 6284 RVA: 0x00012F08 File Offset: 0x00011108
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

		// (get) Token: 0x0600188D RID: 6285 RVA: 0x00012F18 File Offset: 0x00011118
		// (set) Token: 0x0600188E RID: 6286 RVA: 0x00012F20 File Offset: 0x00011120
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

		// (get) Token: 0x0600188F RID: 6287 RVA: 0x00012F30 File Offset: 0x00011130
		// (set) Token: 0x06001890 RID: 6288 RVA: 0x00012F38 File Offset: 0x00011138
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

		// (get) Token: 0x06001891 RID: 6289 RVA: 0x00012F48 File Offset: 0x00011148
		// (set) Token: 0x06001892 RID: 6290 RVA: 0x00012F50 File Offset: 0x00011150
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

		// (get) Token: 0x06001893 RID: 6291 RVA: 0x00012F79 File Offset: 0x00011179
		// (set) Token: 0x06001894 RID: 6292 RVA: 0x00012F81 File Offset: 0x00011181
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

		// (get) Token: 0x06001895 RID: 6293 RVA: 0x00012F91 File Offset: 0x00011191
		// (set) Token: 0x06001896 RID: 6294 RVA: 0x00012F99 File Offset: 0x00011199
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

		// (get) Token: 0x06001897 RID: 6295 RVA: 0x00012FA9 File Offset: 0x000111A9
		// (set) Token: 0x06001898 RID: 6296 RVA: 0x00012FB1 File Offset: 0x000111B1
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

		// (get) Token: 0x06001899 RID: 6297 RVA: 0x00012FC1 File Offset: 0x000111C1
		// (set) Token: 0x0600189A RID: 6298 RVA: 0x00012FC9 File Offset: 0x000111C9
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

		// (get) Token: 0x0600189B RID: 6299 RVA: 0x00012FD9 File Offset: 0x000111D9
		// (set) Token: 0x0600189C RID: 6300 RVA: 0x00012FE1 File Offset: 0x000111E1
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

		// (get) Token: 0x0600189D RID: 6301 RVA: 0x00012FF1 File Offset: 0x000111F1
		// (set) Token: 0x0600189E RID: 6302 RVA: 0x00012FF9 File Offset: 0x000111F9
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

		// (get) Token: 0x0600189F RID: 6303 RVA: 0x00013009 File Offset: 0x00011209
		// (set) Token: 0x060018A0 RID: 6304 RVA: 0x00013011 File Offset: 0x00011211
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

		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x00013021 File Offset: 0x00011221
		// (set) Token: 0x060018A2 RID: 6306 RVA: 0x00013029 File Offset: 0x00011229
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

		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x00013032 File Offset: 0x00011232
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

		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x00013072 File Offset: 0x00011272
		private bool isFocused
		{
			get
			{
				return this.initialized && !this.windowManager.isWindowOpen;
			}
		}

		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x0001308C File Offset: 0x0001128C
		private bool inputAllowed
		{
			get
			{
				return this.blockInputOnFocusEndTime <= Time.unscaledTime;
			}
		}

		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x00066F00 File Offset: 0x00065100
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

		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x00066F34 File Offset: 0x00065134
		private int inputGridWidth
		{
			get
			{
				return this._actionLabelWidth + (this._showKeyboard ? this._keyboardColMaxWidth : 0) + (this._showMouse ? this._mouseColMaxWidth : 0) + (this._showControllers ? this._controllerColMaxWidth : 0) + (this.inputGridColumnCount - 1) * this._inputColumnSpacing;
			}
		}

		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x0001309E File Offset: 0x0001129E
		private Player currentPlayer
		{
			get
			{
				return ReInput.players.GetPlayer(this.currentPlayerId);
			}
		}

		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x000130B0 File Offset: 0x000112B0
		private InputCategory currentMapCategory
		{
			get
			{
				return ReInput.mapping.GetMapCategory(this.currentMapCategoryId);
			}
		}

		// (get) Token: 0x060018AA RID: 6314 RVA: 0x00066F90 File Offset: 0x00065190
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

		// (get) Token: 0x060018AB RID: 6315 RVA: 0x000130C2 File Offset: 0x000112C2
		private Joystick currentJoystick
		{
			get
			{
				return ReInput.controllers.GetJoystick(this.currentJoystickId);
			}
		}

		// (get) Token: 0x060018AC RID: 6316 RVA: 0x000130D4 File Offset: 0x000112D4
		private bool isJoystickSelected
		{
			get
			{
				return this.currentJoystickId >= 0;
			}
		}

		// (get) Token: 0x060018AD RID: 6317 RVA: 0x000130E2 File Offset: 0x000112E2
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

		// (get) Token: 0x060018AE RID: 6318 RVA: 0x000130FD File Offset: 0x000112FD
		private bool showSettings
		{
			get
			{
				return this._showInputBehaviorSettings && this._inputBehaviorSettings.Length != 0;
			}
		}

		// (get) Token: 0x060018AF RID: 6319 RVA: 0x00013113 File Offset: 0x00011313
		private bool showMapCategories
		{
			get
			{
				return this._mappingSets != null && this._mappingSets.Length > 1;
			}
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x0001312D File Offset: 0x0001132D
		private void Awake()
		{
			if (this._dontDestroyOnLoad)
			{
				global::UnityEngine.Object.DontDestroyOnLoad(base.transform.gameObject);
			}
			this.PreInitialize();
			if (this.isOpen)
			{
				this.Initialize();
				this.Open(true);
			}
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00013162 File Offset: 0x00011362
		private void Start()
		{
			if (this._openOnStart)
			{
				this.Open(false);
			}
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x00013173 File Offset: 0x00011373
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

		// Token: 0x060018B3 RID: 6323 RVA: 0x00066FDC File Offset: 0x000651DC
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

		// Token: 0x060018B4 RID: 6324 RVA: 0x0001318D File Offset: 0x0001138D
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

		// Token: 0x060018B5 RID: 6325 RVA: 0x00067058 File Offset: 0x00065258
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
				this._rewiredInputManager = global::UnityEngine.Object.FindObjectOfType<InputManager>();
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

		// Token: 0x060018B6 RID: 6326 RVA: 0x00067348 File Offset: 0x00065548
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

		// Token: 0x060018B7 RID: 6327 RVA: 0x000131AE File Offset: 0x000113AE
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

		// Token: 0x060018B8 RID: 6328 RVA: 0x000131AE File Offset: 0x000113AE
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

		// Token: 0x060018B9 RID: 6329 RVA: 0x000131CE File Offset: 0x000113CE
		private void OnJoystickPreDisconnect(ControllerStatusChangedEventArgs args)
		{
			if (!this.initialized)
			{
				return;
			}
			bool showControllers = this._showControllers;
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x00067398 File Offset: 0x00065598
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

		// Token: 0x060018BB RID: 6331 RVA: 0x00067508 File Offset: 0x00065708
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

		// Token: 0x060018BC RID: 6332 RVA: 0x000131E0 File Offset: 0x000113E0
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

		// Token: 0x060018BD RID: 6333 RVA: 0x00013207 File Offset: 0x00011407
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

		// Token: 0x060018BE RID: 6334 RVA: 0x0001322A File Offset: 0x0001142A
		private void OnControllerSelected(int joystickId)
		{
			if (!this.initialized)
			{
				return;
			}
			this.currentJoystickId = joystickId;
			this.Redraw(true, true);
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x00013244 File Offset: 0x00011444
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

		// Token: 0x060018C0 RID: 6336 RVA: 0x00013279 File Offset: 0x00011479
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

		// Token: 0x060018C1 RID: 6337 RVA: 0x00013296 File Offset: 0x00011496
		private void OnRestoreDefaults()
		{
			if (!this.initialized)
			{
				return;
			}
			this.ShowRestoreDefaultsWindow();
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x000132A7 File Offset: 0x000114A7
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

		// Token: 0x060018C3 RID: 6339 RVA: 0x000132D1 File Offset: 0x000114D1
		private void OnScreenOpenActionPressed(InputActionEventData data)
		{
			this.Open();
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x000132D9 File Offset: 0x000114D9
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

		// Token: 0x060018C5 RID: 6341 RVA: 0x000132FD File Offset: 0x000114FD
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

		// Token: 0x060018C6 RID: 6342 RVA: 0x00013338 File Offset: 0x00011538
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

		// Token: 0x060018C7 RID: 6343 RVA: 0x0001334F File Offset: 0x0001154F
		private void OnRemoveElementAssignment(int windowId, ControllerMap map, ActionElementMap aem)
		{
			if (map == null || aem == null)
			{
				return;
			}
			map.DeleteElementMap(aem.id);
			this.CloseWindow(windowId);
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x000675B0 File Offset: 0x000657B0
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

		// Token: 0x060018C9 RID: 6345 RVA: 0x0001336C File Offset: 0x0001156C
		private void OnControllerAssignmentConfirmed(int windowId, Player player, int controllerId)
		{
			if (windowId < 0 || player == null || controllerId < 0)
			{
				return;
			}
			this.AssignController(player, controllerId);
			this.CloseWindow(windowId);
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x00067630 File Offset: 0x00065830
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

		// Token: 0x060018CB RID: 6347 RVA: 0x00067690 File Offset: 0x00065890
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

		// Token: 0x060018CC RID: 6348 RVA: 0x00013389 File Offset: 0x00011589
		private void OnElementAssignmentAddConfirmed(int windowId, ControlMapper.InputMapping mapping, ElementAssignment assignment)
		{
			if (this.currentPlayer == null || mapping == null)
			{
				return;
			}
			mapping.map.ReplaceOrCreateElementMap(assignment);
			this.CloseWindow(windowId);
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x000678CC File Offset: 0x00065ACC
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

		// Token: 0x060018CE RID: 6350 RVA: 0x0006796C File Offset: 0x00065B6C
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

		// Token: 0x060018CF RID: 6351 RVA: 0x00067A48 File Offset: 0x00065C48
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

		// Token: 0x060018D0 RID: 6352 RVA: 0x00067B34 File Offset: 0x00065D34
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

		// Token: 0x060018D1 RID: 6353 RVA: 0x00067C40 File Offset: 0x00065E40
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

		// Token: 0x060018D2 RID: 6354 RVA: 0x00067D58 File Offset: 0x00065F58
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

		// Token: 0x060018D3 RID: 6355 RVA: 0x00067EDC File Offset: 0x000660DC
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

		// Token: 0x060018D4 RID: 6356 RVA: 0x00067F94 File Offset: 0x00066194
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

		// Token: 0x060018D5 RID: 6357 RVA: 0x0006804C File Offset: 0x0006624C
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

		// Token: 0x060018D6 RID: 6358 RVA: 0x0006812C File Offset: 0x0006632C
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

		// Token: 0x060018D7 RID: 6359 RVA: 0x00068320 File Offset: 0x00066520
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

		// Token: 0x060018D8 RID: 6360 RVA: 0x000133AB File Offset: 0x000115AB
		private void ShowCreateNewElementAssignmentWindow(InputFieldInfo fieldInfo, InputAction action, ControllerMap map, string actionName)
		{
			if (this.inputGrid.GetGUIInputField(this.currentMapCategoryId, action.id, fieldInfo.axisRange, fieldInfo.controllerType, fieldInfo.intData) == null)
			{
				return;
			}
			this.OnBeginElementAssignment(fieldInfo, map, null, actionName);
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x00068514 File Offset: 0x00066714
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

		// Token: 0x060018DA RID: 6362 RVA: 0x00068624 File Offset: 0x00066824
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

		// Token: 0x060018DB RID: 6363 RVA: 0x00068690 File Offset: 0x00066890
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

		// Token: 0x060018DC RID: 6364 RVA: 0x000687A4 File Offset: 0x000669A4
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

		// Token: 0x060018DD RID: 6365 RVA: 0x000688BC File Offset: 0x00066ABC
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

		// Token: 0x060018DE RID: 6366 RVA: 0x000689D0 File Offset: 0x00066BD0
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

		// Token: 0x060018DF RID: 6367 RVA: 0x00068C48 File Offset: 0x00066E48
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

		// Token: 0x060018E0 RID: 6368 RVA: 0x00068E00 File Offset: 0x00067000
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

		// Token: 0x060018E1 RID: 6369 RVA: 0x00068ED0 File Offset: 0x000670D0
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

		// Token: 0x060018E2 RID: 6370 RVA: 0x00069038 File Offset: 0x00067238
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

		// Token: 0x060018E3 RID: 6371 RVA: 0x000691A0 File Offset: 0x000673A0
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

		// Token: 0x060018E4 RID: 6372 RVA: 0x00069250 File Offset: 0x00067450
		private void ShowRestoreDefaultsWindow()
		{
			if (this.currentPlayer == null)
			{
				return;
			}
			this.OpenModal(this._language.restoreDefaultsWindowTitle, this._language.restoreDefaultsWindowMessage, this._language.yes, new Action<int>(this.OnRestoreDefaultsConfirmed), this._language.no, new Action<int>(this.OnWindowCancel), true);
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x000692B4 File Offset: 0x000674B4
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

		// Token: 0x060018E6 RID: 6374 RVA: 0x00069304 File Offset: 0x00067504
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

		// Token: 0x060018E7 RID: 6375 RVA: 0x000695C0 File Offset: 0x000677C0
		private void RefreshInputGridStructure()
		{
			if (this.currentMappingSet == null)
			{
				return;
			}
			this.inputGrid.HideAll();
			this.inputGrid.Show(this.currentMappingSet.mapCategoryId);
			this.references.inputGridInnerGroup.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.inputGrid.GetColumnHeight(this.currentMappingSet.mapCategoryId));
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x00069624 File Offset: 0x00067824
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

		// Token: 0x060018E9 RID: 6377 RVA: 0x000697BC File Offset: 0x000679BC
		private void CreateActionLabelColumn()
		{
			Transform transform = this.CreateNewColumnGroup("ActionLabelColumn", this.references.inputGridInnerGroup, this._actionLabelWidth).transform;
			this.references.inputGridActionColumn = transform;
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x000133E4 File Offset: 0x000115E4
		private void CreateKeyboardInputFieldColumn()
		{
			if (!this._showKeyboard)
			{
				return;
			}
			this.CreateInputFieldColumn("KeyboardColumn", 0, this._keyboardColMaxWidth, this._keyboardInputFieldCount, true);
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x00013408 File Offset: 0x00011608
		private void CreateMouseInputFieldColumn()
		{
			if (!this._showMouse)
			{
				return;
			}
			this.CreateInputFieldColumn("MouseColumn", 1, this._mouseColMaxWidth, this._mouseInputFieldCount, false);
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x0001342C File Offset: 0x0001162C
		private void CreateControllerInputFieldColumn()
		{
			if (!this._showControllers)
			{
				return;
			}
			this.CreateInputFieldColumn("ControllerColumn", 2, this._controllerColMaxWidth, this._controllerInputFieldCount, false);
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x000697F8 File Offset: 0x000679F8
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

		// Token: 0x060018EE RID: 6382 RVA: 0x000052F6 File Offset: 0x000034F6
		private bool ShouldDisplayElement(int elementIndex)
		{
			return true;
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x00069860 File Offset: 0x00067A60
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
									guilabel.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)this._inputRowHeight);
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
											guilabel2.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)this._inputRowHeight);
											this.inputGrid.AddActionLabel(mappingSet.mapCategoryId, inputAction.id, 0, guilabel2);
											num -= this._inputRowHeight;
										}
										if (this._showSplitAxisInputFields)
										{
											string actionName = this._language.GetActionName(inputAction.id, 1);
											ControlMapper.GUILabel guilabel2 = this.CreateLabel(actionName, inputGridActionColumn, new Vector2(0f, (float)num));
											guilabel2.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)this._inputRowHeight);
											this.inputGrid.AddActionLabel(mappingSet.mapCategoryId, inputAction.id, 1, guilabel2);
											num -= this._inputRowHeight;
											string actionName2 = this._language.GetActionName(inputAction.id, 2);
											guilabel2 = this.CreateLabel(actionName2, inputGridActionColumn, new Vector2(0f, (float)num));
											guilabel2.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)this._inputRowHeight);
											this.inputGrid.AddActionLabel(mappingSet.mapCategoryId, inputAction.id, 2, guilabel2);
											num -= this._inputRowHeight;
										}
									}
									else if (inputAction.type == 1)
									{
										ControlMapper.GUILabel guilabel2 = this.CreateLabel(this._language.GetActionName(inputAction.id), inputGridActionColumn, new Vector2(0f, (float)num));
										guilabel2.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)this._inputRowHeight);
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
												guilabel3.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)this._inputRowHeight);
												this.inputGrid.AddActionLabel(mappingSet.mapCategoryId, action.id, 0, guilabel3);
												num -= this._inputRowHeight;
											}
											if (this._showSplitAxisInputFields)
											{
												ControlMapper.GUILabel guilabel3 = this.CreateLabel(this._language.GetActionName(action.id, 1), inputGridActionColumn, new Vector2(0f, (float)num));
												guilabel3.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)this._inputRowHeight);
												this.inputGrid.AddActionLabel(mappingSet.mapCategoryId, action.id, 1, guilabel3);
												num -= this._inputRowHeight;
												guilabel3 = this.CreateLabel(this._language.GetActionName(action.id, 2), inputGridActionColumn, new Vector2(0f, (float)num));
												guilabel3.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)this._inputRowHeight);
												this.inputGrid.AddActionLabel(mappingSet.mapCategoryId, action.id, 2, guilabel3);
												num -= this._inputRowHeight;
											}
										}
										else if (action.type == 1)
										{
											ControlMapper.GUILabel guilabel3 = this.CreateLabel(this._language.GetActionName(action.id), inputGridActionColumn, new Vector2(0f, (float)num));
											guilabel3.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)this._inputRowHeight);
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

		// Token: 0x060018F0 RID: 6384 RVA: 0x00069DF8 File Offset: 0x00067FF8
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

		// Token: 0x060018F1 RID: 6385 RVA: 0x00069E7C File Offset: 0x0006807C
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

		// Token: 0x060018F2 RID: 6386 RVA: 0x0006A140 File Offset: 0x00068340
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
			component.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)this._inputRowHeight);
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

		// Token: 0x060018F3 RID: 6387 RVA: 0x0006A2BC File Offset: 0x000684BC
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

		// Token: 0x060018F4 RID: 6388 RVA: 0x0006A420 File Offset: 0x00068620
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

		// Token: 0x060018F5 RID: 6389 RVA: 0x0006A644 File Offset: 0x00068844
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

		// Token: 0x060018F6 RID: 6390 RVA: 0x0006A688 File Offset: 0x00068888
		private void ResetInputGridScrollBar()
		{
			this.references.inputGridInnerGroup.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			this.references.inputGridVScrollbar.value = 1f;
			this.references.inputGridScrollRect.verticalScrollbarVisibility = 1;
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0006A6D8 File Offset: 0x000688D8
		private void CreateLayout()
		{
			this.references.playersGroup.gameObject.SetActive(this.showPlayers);
			this.references.controllerGroup.gameObject.SetActive(this._showControllers);
			this.references.assignedControllersGroup.gameObject.SetActive(this._showControllers && this.ShowAssignedControllers());
			this.references.settingsAndMapCategoriesGroup.gameObject.SetActive(this.showSettings || this.showMapCategories);
			this.references.settingsGroup.gameObject.SetActive(this.showSettings);
			this.references.mapCategoriesGroup.gameObject.SetActive(this.showMapCategories);
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x00013450 File Offset: 0x00011650
		private void Draw()
		{
			this.DrawPlayersGroup();
			this.DrawControllersGroup();
			this.DrawSettingsGroup();
			this.DrawMapCategoriesGroup();
			this.DrawWindowButtonsGroup();
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x0006A7A0 File Offset: 0x000689A0
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

		// Token: 0x060018FA RID: 6394 RVA: 0x0006A8AC File Offset: 0x00068AAC
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

		// Token: 0x060018FB RID: 6395 RVA: 0x0006AA14 File Offset: 0x00068C14
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

		// Token: 0x060018FC RID: 6396 RVA: 0x0006AAC8 File Offset: 0x00068CC8
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

		// Token: 0x060018FD RID: 6397 RVA: 0x0006ABEC File Offset: 0x00068DEC
		private void DrawWindowButtonsGroup()
		{
			this.references.doneButton.GetComponent<ButtonInfo>().text.text = this._language.doneButtonLabel;
			this.references.restoreDefaultsButton.GetComponent<ButtonInfo>().text.text = this._language.restoreDefaultsButtonLabel;
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x00013470 File Offset: 0x00011670
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

		// Token: 0x060018FF RID: 6399 RVA: 0x0006AC44 File Offset: 0x00068E44
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

		// Token: 0x06001900 RID: 6400 RVA: 0x0006ACA8 File Offset: 0x00068EA8
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
						global::UnityEngine.Object.Destroy(guibutton.gameObject);
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

		// Token: 0x06001901 RID: 6401 RVA: 0x0006B090 File Offset: 0x00069290
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

		// Token: 0x06001902 RID: 6402 RVA: 0x000134AE File Offset: 0x000116AE
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

		// Token: 0x06001903 RID: 6403 RVA: 0x000134C8 File Offset: 0x000116C8
		private void ForceRefresh()
		{
			if (this.windowManager.isWindowOpen)
			{
				this.CloseAllWindows();
				return;
			}
			this.Redraw(false, false);
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0006B0F4 File Offset: 0x000692F4
		private void CreateInputCategoryRow(ref int rowCount, InputCategory category)
		{
			this.CreateLabel(this._language.GetMapCategoryName(category.id), this.references.inputGridActionColumn, new Vector2(0f, (float)(rowCount * this._inputRowHeight) * -1f));
			rowCount++;
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x000134E6 File Offset: 0x000116E6
		private ControlMapper.GUILabel CreateLabel(string labelText, Transform parent, Vector2 offset)
		{
			return this.CreateLabel(this.prefabs.inputGridLabel, labelText, parent, offset);
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x0006B144 File Offset: 0x00069344
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

		// Token: 0x06001907 RID: 6407 RVA: 0x000134FC File Offset: 0x000116FC
		private ControlMapper.GUIButton CreateButton(string labelText, Transform parent, Vector2 offset)
		{
			ControlMapper.GUIButton guibutton = new ControlMapper.GUIButton(this.InstantiateGUIObject(this.prefabs.button, parent, offset));
			guibutton.SetLabel(labelText);
			return guibutton;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x0001351D File Offset: 0x0001171D
		private ControlMapper.GUIButton CreateFitButton(string labelText, Transform parent, Vector2 offset)
		{
			ControlMapper.GUIButton guibutton = new ControlMapper.GUIButton(this.InstantiateGUIObject(this.prefabs.fitButton, parent, offset));
			guibutton.SetLabel(labelText);
			return guibutton;
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x0001353E File Offset: 0x0001173E
		private ControlMapper.GUIInputField CreateInputField(Transform parent, Vector2 offset, string label, int actionId, AxisRange axisRange, ControllerType controllerType, int fieldIndex)
		{
			ControlMapper.GUIInputField guiinputField = this.CreateInputField(parent, offset);
			guiinputField.SetFieldInfoData(actionId, axisRange, controllerType, fieldIndex);
			guiinputField.SetOnClickCallback(this.inputFieldActivatedDelegate);
			guiinputField.fieldInfo.OnSelectedEvent += this.OnUIElementSelected;
			return guiinputField;
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x00013579 File Offset: 0x00011779
		private ControlMapper.GUIInputField CreateInputField(Transform parent, Vector2 offset)
		{
			return new ControlMapper.GUIInputField(this.InstantiateGUIObject(this.prefabs.inputGridFieldButton, parent, offset));
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00013593 File Offset: 0x00011793
		private ControlMapper.GUIToggle CreateToggle(GameObject prefab, Transform parent, Vector2 offset, string label, int actionId, AxisRange axisRange, ControllerType controllerType, int fieldIndex)
		{
			ControlMapper.GUIToggle guitoggle = this.CreateToggle(prefab, parent, offset);
			guitoggle.SetToggleInfoData(actionId, axisRange, controllerType, fieldIndex);
			guitoggle.SetOnSubmitCallback(this.inputFieldInvertToggleStateChangedDelegate);
			guitoggle.toggleInfo.OnSelectedEvent += this.OnUIElementSelected;
			return guitoggle;
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x000135CF File Offset: 0x000117CF
		private ControlMapper.GUIToggle CreateToggle(GameObject prefab, Transform parent, Vector2 offset)
		{
			return new ControlMapper.GUIToggle(this.InstantiateGUIObject(prefab, parent, offset));
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x0006B188 File Offset: 0x00069388
		private GameObject InstantiateGUIObject(GameObject prefab, Transform parent, Vector2 offset)
		{
			if (prefab == null)
			{
				Debug.LogError("Rewired Control Mapper: Prefab is null!");
				return null;
			}
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(prefab);
			return this.InitializeNewGUIGameObject(gameObject, parent, offset);
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x0006B1BC File Offset: 0x000693BC
		private GameObject CreateNewGUIObject(string name, Transform parent, Vector2 offset)
		{
			GameObject gameObject = new GameObject();
			gameObject.name = name;
			gameObject.AddComponent<RectTransform>();
			return this.InitializeNewGUIGameObject(gameObject, parent, offset);
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x0006B1E8 File Offset: 0x000693E8
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

		// Token: 0x06001910 RID: 6416 RVA: 0x0006B240 File Offset: 0x00069440
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

		// Token: 0x06001911 RID: 6417 RVA: 0x000135DF File Offset: 0x000117DF
		private Window OpenWindow(bool closeOthers)
		{
			return this.OpenWindow(string.Empty, closeOthers);
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0006B2AC File Offset: 0x000694AC
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

		// Token: 0x06001913 RID: 6419 RVA: 0x000135ED File Offset: 0x000117ED
		private Window OpenWindow(GameObject windowPrefab, bool closeOthers)
		{
			return this.OpenWindow(windowPrefab, string.Empty, closeOthers);
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x0006B2F4 File Offset: 0x000694F4
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

		// Token: 0x06001915 RID: 6421 RVA: 0x0006B330 File Offset: 0x00069530
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

		// Token: 0x06001916 RID: 6422 RVA: 0x000135FC File Offset: 0x000117FC
		private void CloseWindow(int windowId)
		{
			if (!this.windowManager.isWindowOpen)
			{
				return;
			}
			this.windowManager.CloseWindow(windowId);
			this.ChildWindowClosed();
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x0001361E File Offset: 0x0001181E
		private void CloseTopWindow()
		{
			if (!this.windowManager.isWindowOpen)
			{
				return;
			}
			this.windowManager.CloseTop();
			this.ChildWindowClosed();
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x0001363F File Offset: 0x0001183F
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

		// Token: 0x06001919 RID: 6425 RVA: 0x00013666 File Offset: 0x00011866
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

		// Token: 0x0600191A RID: 6426 RVA: 0x000136A3 File Offset: 0x000118A3
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

		// Token: 0x0600191B RID: 6427 RVA: 0x0006B43C File Offset: 0x0006963C
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

		// Token: 0x0600191C RID: 6428 RVA: 0x0006B4A8 File Offset: 0x000696A8
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

		// Token: 0x0600191D RID: 6429 RVA: 0x000136E0 File Offset: 0x000118E0
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

		// Token: 0x0600191E RID: 6430 RVA: 0x0006B5CC File Offset: 0x000697CC
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

		// Token: 0x0600191F RID: 6431 RVA: 0x0006B660 File Offset: 0x00069860
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
				if (keyboardKey != KeyCode.AltGr)
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
					else if (controllerPollingInfo.keyboardKey == KeyCode.None)
					{
						controllerPollingInfo = controllerPollingInfo3;
					}
				}
			}
			if (controllerPollingInfo.keyboardKey == KeyCode.None)
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
						label = this._language.ModifierKeyFlagsToString(modifierKeyFlags);
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

		// Token: 0x06001920 RID: 6432 RVA: 0x0006B7AC File Offset: 0x000699AC
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

		// Token: 0x06001921 RID: 6433 RVA: 0x0006B820 File Offset: 0x00069A20
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

		// Token: 0x06001922 RID: 6434 RVA: 0x0006B888 File Offset: 0x00069A88
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

		// Token: 0x06001923 RID: 6435 RVA: 0x0001370D File Offset: 0x0001190D
		private void EndAxisCalibration()
		{
			if (this.pendingAxisCalibration == null)
			{
				return;
			}
			this.pendingAxisCalibration.Commit();
			this.pendingAxisCalibration = null;
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x0001372A File Offset: 0x0001192A
		private void SetUISelection(GameObject selection)
		{
			if (EventSystem.current == null)
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(selection);
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x00013745 File Offset: 0x00011945
		private void RestoreLastUISelection()
		{
			if (this.lastUISelection == null || !this.lastUISelection.activeInHierarchy)
			{
				this.SetDefaultUISelection();
				return;
			}
			this.SetUISelection(this.lastUISelection);
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x00013775 File Offset: 0x00011975
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

		// Token: 0x06001927 RID: 6439 RVA: 0x0006B8DC File Offset: 0x00069ADC
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

		// Token: 0x06001928 RID: 6440 RVA: 0x000137B1 File Offset: 0x000119B1
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

		// Token: 0x06001929 RID: 6441 RVA: 0x000137D0 File Offset: 0x000119D0
		private void OnUIElementSelected(GameObject selectedObject)
		{
			this.lastUISelection = selectedObject;
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x000137D9 File Offset: 0x000119D9
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

		// Token: 0x0600192B RID: 6443 RVA: 0x0001380E File Offset: 0x00011A0E
		public void Toggle()
		{
			if (this.isOpen)
			{
				this.Close(true);
				return;
			}
			this.Open();
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00013826 File Offset: 0x00011A26
		public void Open()
		{
			this.Open(false);
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x0006B998 File Offset: 0x00069B98
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

		// Token: 0x0600192E RID: 6446 RVA: 0x0006BA24 File Offset: 0x00069C24
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

		// Token: 0x0600192F RID: 6447 RVA: 0x0001382F File Offset: 0x00011A2F
		private void Clear()
		{
			this.windowManager.CancelAll();
			this.lastUISelection = null;
			this.pendingInputMapping = null;
			this.pendingAxisCalibration = null;
			this.InputPollingStopped();
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x00013857 File Offset: 0x00011A57
		private void ClearCompletely()
		{
			this.Clear();
			this.ClearSpawnedObjects();
			this.ClearAllVars();
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x0006BA9C File Offset: 0x00069C9C
		private void ClearSpawnedObjects()
		{
			this.windowManager.ClearCompletely();
			this.inputGrid.ClearAll();
			foreach (ControlMapper.GUIButton guibutton in this.playerButtons)
			{
				global::UnityEngine.Object.Destroy(guibutton.gameObject);
			}
			this.playerButtons.Clear();
			foreach (ControlMapper.GUIButton guibutton2 in this.mapCategoryButtons)
			{
				global::UnityEngine.Object.Destroy(guibutton2.gameObject);
			}
			this.mapCategoryButtons.Clear();
			foreach (ControlMapper.GUIButton guibutton3 in this.assignedControllerButtons)
			{
				global::UnityEngine.Object.Destroy(guibutton3.gameObject);
			}
			this.assignedControllerButtons.Clear();
			if (this.assignedControllerButtonsPlaceholder != null)
			{
				global::UnityEngine.Object.Destroy(this.assignedControllerButtonsPlaceholder.gameObject);
				this.assignedControllerButtonsPlaceholder = null;
			}
			foreach (GameObject gameObject in this.miscInstantiatedObjects)
			{
				global::UnityEngine.Object.Destroy(gameObject);
			}
			this.miscInstantiatedObjects.Clear();
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x0001386B File Offset: 0x00011A6B
		private void ClearVarsOnPlayerChange()
		{
			this.currentJoystickId = -1;
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x0001386B File Offset: 0x00011A6B
		private void ClearVarsOnJoystickChange()
		{
			this.currentJoystickId = -1;
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x0006BC1C File Offset: 0x00069E1C
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

		// Token: 0x06001935 RID: 6453 RVA: 0x00013874 File Offset: 0x00011A74
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

		// Token: 0x06001936 RID: 6454 RVA: 0x0006BCA8 File Offset: 0x00069EA8
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

		// Token: 0x06001937 RID: 6455 RVA: 0x0006BCE4 File Offset: 0x00069EE4
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

		// Token: 0x06001938 RID: 6456 RVA: 0x0006BD58 File Offset: 0x00069F58
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

		// Token: 0x06001939 RID: 6457 RVA: 0x0006BDB0 File Offset: 0x00069FB0
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

		// Token: 0x0600193A RID: 6458 RVA: 0x0006BDE0 File Offset: 0x00069FE0
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

		// Token: 0x0600193B RID: 6459 RVA: 0x0006BE34 File Offset: 0x0006A034
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

		// Token: 0x0600193C RID: 6460 RVA: 0x0006BE90 File Offset: 0x0006A090
		private void SubscribeMenuControlInputEvents()
		{
			this.SubscribeRewiredInputEventAllPlayers(this._screenToggleAction, new Action<InputActionEventData>(this.OnScreenToggleActionPressed));
			this.SubscribeRewiredInputEventAllPlayers(this._screenOpenAction, new Action<InputActionEventData>(this.OnScreenOpenActionPressed));
			this.SubscribeRewiredInputEventAllPlayers(this._screenCloseAction, new Action<InputActionEventData>(this.OnScreenCloseActionPressed));
			this.SubscribeRewiredInputEventAllPlayers(this._universalCancelAction, new Action<InputActionEventData>(this.OnUniversalCancelActionPressed));
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x0006BF00 File Offset: 0x0006A100
		private void UnsubscribeMenuControlInputEvents()
		{
			this.UnsubscribeRewiredInputEventAllPlayers(this._screenToggleAction, new Action<InputActionEventData>(this.OnScreenToggleActionPressed));
			this.UnsubscribeRewiredInputEventAllPlayers(this._screenOpenAction, new Action<InputActionEventData>(this.OnScreenOpenActionPressed));
			this.UnsubscribeRewiredInputEventAllPlayers(this._screenCloseAction, new Action<InputActionEventData>(this.OnScreenCloseActionPressed));
			this.UnsubscribeRewiredInputEventAllPlayers(this._universalCancelAction, new Action<InputActionEventData>(this.OnUniversalCancelActionPressed));
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x0006BF70 File Offset: 0x0006A170
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

		// Token: 0x0600193F RID: 6463 RVA: 0x0006BFF4 File Offset: 0x0006A1F4
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

		// Token: 0x06001940 RID: 6464 RVA: 0x0001389A File Offset: 0x00011A9A
		private int GetMaxControllersPerPlayer()
		{
			if (this._rewiredInputManager.userData.ConfigVars.autoAssignJoysticks)
			{
				return this._rewiredInputManager.userData.ConfigVars.maxJoysticksPerPlayer;
			}
			return this._maxControllersPerPlayer;
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x000138CF File Offset: 0x00011ACF
		private bool ShowAssignedControllers()
		{
			return this._showControllers && (this._showAssignedControllers || this.GetMaxControllersPerPlayer() != 1);
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x000138F1 File Offset: 0x00011AF1
		private void InspectorPropertyChanged(bool reset = false)
		{
			if (reset)
			{
				this.Reset();
			}
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x0006C080 File Offset: 0x0006A280
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

		// Token: 0x06001944 RID: 6468 RVA: 0x0006C128 File Offset: 0x0006A328
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

		// Token: 0x06001945 RID: 6469 RVA: 0x000138FC File Offset: 0x00011AFC
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

		// Token: 0x06001946 RID: 6470 RVA: 0x00013938 File Offset: 0x00011B38
		private bool IsAllowedAssignment(ControlMapper.InputMapping pendingInputMapping, ControllerPollingInfo pollingInfo)
		{
			return pendingInputMapping != null && (pendingInputMapping.axisRange != null || this._showSplitAxisInputFields || pollingInfo.elementType != 1);
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x0001395C File Offset: 0x00011B5C
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

		// Token: 0x06001948 RID: 6472 RVA: 0x00013993 File Offset: 0x00011B93
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

		// Token: 0x06001949 RID: 6473 RVA: 0x000139CA File Offset: 0x00011BCA
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

		// Token: 0x0600194A RID: 6474 RVA: 0x0006C16C File Offset: 0x0006A36C
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

		// Token: 0x0600194B RID: 6475 RVA: 0x000139FA File Offset: 0x00011BFA
		private bool SwapIsSameInputRange(ControllerElementType origElementType, AxisRange origAxisRange, Pole origAxisContribution, ControllerElementType conflictElementType, AxisRange conflictAxisRange, Pole conflictAxisContribution)
		{
			return ((origElementType == 1 || (origElementType == null && origAxisRange != null)) && (conflictElementType == 1 || (conflictElementType == null && conflictAxisRange != null)) && conflictAxisContribution == origAxisContribution) || (origElementType == null && origAxisRange == null && conflictElementType == null && conflictAxisRange == null);
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x00013A2B File Offset: 0x00011C2B
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

		// Token: 0x0600194D RID: 6477 RVA: 0x00013A6B File Offset: 0x00011C6B
		public static LanguageDataBase GetLanguage()
		{
			if (ControlMapper.Instance == null)
			{
				return null;
			}
			return ControlMapper.Instance._language;
		}

		public static bool isActive;

		public const int versionMajor = 1;

		public const int versionMinor = 1;

		public const bool usesTMPro = false;

		private const float blockInputOnFocusTimeout = 0.1f;

		private const string buttonIdentifier_playerSelection = "PlayerSelection";

		private const string buttonIdentifier_removeController = "RemoveController";

		private const string buttonIdentifier_assignController = "AssignController";

		private const string buttonIdentifier_calibrateController = "CalibrateController";

		private const string buttonIdentifier_editInputBehaviors = "EditInputBehaviors";

		private const string buttonIdentifier_mapCategorySelection = "MapCategorySelection";

		private const string buttonIdentifier_assignedControllerSelection = "AssignedControllerSelection";

		private const string buttonIdentifier_done = "Done";

		private const string buttonIdentifier_restoreDefaults = "RestoreDefaults";

		[SerializeField]
		[Tooltip("Must be assigned a Rewired Input Manager scene object or prefab.")]
		private InputManager _rewiredInputManager;

		[SerializeField]
		[Tooltip("Set to True to prevent the Game Object from being destroyed when a new scene is loaded.\n\nNOTE: Changing this value from True to False at runtime will have no effect because Object.DontDestroyOnLoad cannot be undone once set.")]
		private bool _dontDestroyOnLoad;

		[SerializeField]
		[Tooltip("Open the control mapping screen immediately on start. Mainly used for testing.")]
		private bool _openOnStart;

		[SerializeField]
		[Tooltip("The Layout of the Keyboard Maps to be displayed.")]
		private int _keyboardMapDefaultLayout;

		[SerializeField]
		[Tooltip("The Layout of the Mouse Maps to be displayed.")]
		private int _mouseMapDefaultLayout;

		[SerializeField]
		[Tooltip("The Layout of the Mouse Maps to be displayed.")]
		private int _joystickMapDefaultLayout;

		[SerializeField]
		public ControlMapper.MappingSet[] _mappingSets = new ControlMapper.MappingSet[] { ControlMapper.MappingSet.Default };

		[SerializeField]
		[Tooltip("Display a selectable list of Players. If your game only supports 1 player, you can disable this.")]
		private bool _showPlayers = true;

		[SerializeField]
		[Tooltip("Display the Controller column for input mapping.")]
		private bool _showControllers = true;

		[SerializeField]
		[Tooltip("Display the Keyboard column for input mapping.")]
		private bool _showKeyboard = true;

		[SerializeField]
		[Tooltip("Display the Mouse column for input mapping.")]
		private bool _showMouse = true;

		[SerializeField]
		[Tooltip("The maximum number of controllers allowed to be assigned to a Player. If set to any value other than 1, a selectable list of currently-assigned controller will be displayed to the user. [0 = infinite]")]
		private int _maxControllersPerPlayer = 1;

		[SerializeField]
		[Tooltip("Display section labels for each Action Category in the input field grid. Only applies if Action Categories are used to display the Action list.")]
		private bool _showActionCategoryLabels;

		[SerializeField]
		[Tooltip("The number of input fields to display for the keyboard. If you want to support alternate mappings on the same device, set this to 2 or more.")]
		private int _keyboardInputFieldCount = 2;

		[SerializeField]
		[Tooltip("The number of input fields to display for the mouse. If you want to support alternate mappings on the same device, set this to 2 or more.")]
		private int _mouseInputFieldCount = 1;

		[SerializeField]
		[Tooltip("The number of input fields to display for joysticks. If you want to support alternate mappings on the same device, set this to 2 or more.")]
		private int _controllerInputFieldCount = 1;

		[SerializeField]
		[Tooltip("Display a full-axis input assignment field for every axis-type Action in the input field grid. Also displays an invert toggle for the user  to invert the full-axis assignment direction.\n\n*IMPORTANT*: This field is required if you have made any full-axis assignments in the Rewired Input Manager or in saved XML user data. Disabling this field when you have full-axis assignments will result in the inability for the user to view, remove, or modify these full-axis assignments. In addition, these assignments may cause conflicts when trying to remap the same axes to Actions.")]
		private bool _showFullAxisInputFields = true;

		[SerializeField]
		[Tooltip("Display a positive and negative input assignment field for every axis-type Action in the input field grid.\n\n*IMPORTANT*: These fields are required to assign buttons, keyboard keys, and hat or D-Pad directions to axis-type Actions. If you have made any split-axis assignments or button/key/D-pad assignments to axis-type Actions in the Rewired Input Manager or in saved XML user data, disabling these fields will result in the inability for the user to view, remove, or modify these assignments. In addition, these assignments may cause conflicts when trying to remap the same elements to Actions.")]
		private bool _showSplitAxisInputFields = true;

		[SerializeField]
		[Tooltip("If enabled, when an element assignment conflict is found, an option will be displayed that allows the user to make the conflicting assignment anyway.")]
		private bool _allowElementAssignmentConflicts;

		[SerializeField]
		[Tooltip("If enabled, when an element assignment conflict is found, an option will be displayed that allows the user to swap conflicting assignments. This only applies to the first conflicting assignment found. This option will not be displayed if allowElementAssignmentConflicts is true.")]
		private bool _allowElementAssignmentSwap;

		[SerializeField]
		[Tooltip("The width in relative pixels of the Action label column.")]
		private int _actionLabelWidth = 200;

		[SerializeField]
		[Tooltip("The width in relative pixels of the Keyboard column.")]
		private int _keyboardColMaxWidth = 360;

		[SerializeField]
		[Tooltip("The width in relative pixels of the Mouse column.")]
		private int _mouseColMaxWidth = 200;

		[SerializeField]
		[Tooltip("The width in relative pixels of the Controller column.")]
		private int _controllerColMaxWidth = 200;

		[SerializeField]
		[Tooltip("The height in relative pixels of the input grid button rows.")]
		private int _inputRowHeight = 40;

		[SerializeField]
		[Tooltip("The padding of the input grid button rows.")]
		private RectOffset _inputRowPadding = new RectOffset();

		[SerializeField]
		[Tooltip("The width in relative pixels of spacing between input fields in a single column.")]
		private int _inputRowFieldSpacing;

		[SerializeField]
		[Tooltip("The width in relative pixels of spacing between columns.")]
		private int _inputColumnSpacing = 40;

		[SerializeField]
		[Tooltip("The height in relative pixels of the space between Action Category sections. Only applicable if Show Action Category Labels is checked.")]
		private int _inputRowCategorySpacing = 20;

		[SerializeField]
		[Tooltip("The width in relative pixels of the invert toggle buttons.")]
		private int _invertToggleWidth = 40;

		[SerializeField]
		[Tooltip("The width in relative pixels of generated popup windows.")]
		private int _defaultWindowWidth = 500;

		[SerializeField]
		[Tooltip("The height in relative pixels of generated popup windows.")]
		private int _defaultWindowHeight = 400;

		[SerializeField]
		[Tooltip("The time in seconds the user has to press an element on a controller when assigning a controller to a Player. If this time elapses with no user input a controller, the assignment will be canceled.")]
		private float _controllerAssignmentTimeout = 5f;

		[SerializeField]
		[Tooltip("The time in seconds the user has to press an element on a controller while waiting for axes to be centered before assigning input.")]
		private float _preInputAssignmentTimeout = 5f;

		[SerializeField]
		[Tooltip("The time in seconds the user has to press an element on a controller when assigning input. If this time elapses with no user input on the target controller, the assignment will be canceled.")]
		private float _inputAssignmentTimeout = 5f;

		[SerializeField]
		[Tooltip("The time in seconds the user has to press an element on a controller during calibration.")]
		private float _axisCalibrationTimeout = 5f;

		[SerializeField]
		[Tooltip("If checked, mouse X-axis movement will always be ignored during input assignment. Check this if you don't want the horizontal mouse axis to be user-assignable to any Actions.")]
		private bool _ignoreMouseXAxisAssignment = true;

		[SerializeField]
		[Tooltip("If checked, mouse Y-axis movement will always be ignored during input assignment. Check this if you don't want the vertical mouse axis to be user-assignable to any Actions.")]
		private bool _ignoreMouseYAxisAssignment = true;

		[SerializeField]
		[Tooltip("An Action that when activated will alternately close or open the main screen as long as no popup windows are open.")]
		private int _screenToggleAction = -1;

		[SerializeField]
		[Tooltip("An Action that when activated will open the main screen if it is closed.")]
		private int _screenOpenAction = -1;

		[SerializeField]
		[Tooltip("An Action that when activated will close the main screen as long as no popup windows are open.")]
		private int _screenCloseAction = -1;

		[SerializeField]
		[Tooltip("An Action that when activated will cancel and close any open popup window. Use with care because the element assigned to this Action can never be mapped by the user (because it would just cancel his assignment).")]
		private int _universalCancelAction = -1;

		[SerializeField]
		[Tooltip("If enabled, Universal Cancel will also close the main screen if pressed when no windows are open.")]
		private bool _universalCancelClosesScreen = true;

		[SerializeField]
		[Tooltip("If checked, controls will be displayed which will allow the user to customize certain Input Behavior settings.")]
		private bool _showInputBehaviorSettings;

		[SerializeField]
		[Tooltip("Customizable settings for user-modifiable Input Behaviors. This can be used for settings like Mouse Look Sensitivity.")]
		private ControlMapper.InputBehaviorSettings[] _inputBehaviorSettings;

		[SerializeField]
		[Tooltip("If enabled, UI elements will be themed based on the settings in Theme Settings.")]
		private bool _useThemeSettings = true;

		[SerializeField]
		[Tooltip("Must be assigned a ThemeSettings object. Used to theme UI elements.")]
		private ThemeSettings _themeSettings;

		[SerializeField]
		[Tooltip("Must be assigned a LanguageData object. Used to retrieve language entries for UI elements.")]
		private LanguageDataBase _language;

		[SerializeField]
		private UIButtonDisplaySettings _buttonDisplaySettings;

		[SerializeField]
		[Tooltip("A list of prefabs. You should not have to modify this.")]
		private ControlMapper.Prefabs prefabs;

		[SerializeField]
		[Tooltip("A list of references to elements in the hierarchy. You should not have to modify this.")]
		private ControlMapper.References references;

		[SerializeField]
		[Tooltip("Show the label for the Players button group?")]
		private bool _showPlayersGroupLabel = true;

		[SerializeField]
		[Tooltip("Show the label for the Controller button group?")]
		private bool _showControllerGroupLabel = true;

		[SerializeField]
		[Tooltip("Show the label for the Assigned Controllers button group?")]
		private bool _showAssignedControllersGroupLabel = true;

		[SerializeField]
		[Tooltip("Show the label for the Settings button group?")]
		private bool _showSettingsGroupLabel = true;

		[SerializeField]
		[Tooltip("Show the label for the Map Categories button group?")]
		private bool _showMapCategoriesGroupLabel = true;

		[SerializeField]
		[Tooltip("Show the label for the current controller name?")]
		private bool _showControllerNameLabel = true;

		[SerializeField]
		[Tooltip("Show the Assigned Controllers group? If joystick auto-assignment is enabled in the Rewired Input Manager and the max joysticks per player is set to any value other than 1, the Assigned Controllers group will always be displayed.")]
		private bool _showAssignedControllers = true;

		private Action _ScreenClosedEvent;

		private Action _ScreenOpenedEvent;

		private Action _PopupWindowOpenedEvent;

		private Action _PopupWindowClosedEvent;

		private Action _InputPollingStartedEvent;

		private Action _InputPollingEndedEvent;

		[SerializeField]
		[Tooltip("Event sent when the UI is closed.")]
		private UnityEvent _onScreenClosed;

		[SerializeField]
		[Tooltip("Event sent when the UI is opened.")]
		private UnityEvent _onScreenOpened;

		[SerializeField]
		[Tooltip("Event sent when a popup window is closed.")]
		private UnityEvent _onPopupWindowClosed;

		[SerializeField]
		[Tooltip("Event sent when a popup window is opened.")]
		private UnityEvent _onPopupWindowOpened;

		[SerializeField]
		[Tooltip("Event sent when polling for input has started.")]
		private UnityEvent _onInputPollingStarted;

		[SerializeField]
		[Tooltip("Event sent when polling for input has ended.")]
		private UnityEvent _onInputPollingEnded;

		private static ControlMapper Instance;

		private bool initialized;

		private int playerCount;

		private ControlMapper.InputGrid inputGrid;

		private ControlMapper.WindowManager windowManager;

		private int currentPlayerId;

		private int currentMapCategoryId;

		private List<ControlMapper.GUIButton> playerButtons;

		private List<ControlMapper.GUIButton> mapCategoryButtons;

		private List<ControlMapper.GUIButton> assignedControllerButtons;

		private ControlMapper.GUIButton assignedControllerButtonsPlaceholder;

		private List<GameObject> miscInstantiatedObjects;

		private GameObject canvas;

		private GameObject lastUISelection;

		private int currentJoystickId = -1;

		private float blockInputOnFocusEndTime;

		private bool isPollingForInput;

		private ControlMapper.InputMapping pendingInputMapping;

		private ControlMapper.AxisCalibrator pendingAxisCalibration;

		private Action<InputFieldInfo> inputFieldActivatedDelegate;

		private Action<ToggleInfo, bool> inputFieldInvertToggleStateChangedDelegate;

		private Action _restoreDefaultsDelegate;

		private bool isPreInitialized;

		private abstract class GUIElement
		{
			// (get) Token: 0x0600194F RID: 6479 RVA: 0x00013A86 File Offset: 0x00011C86
			// (set) Token: 0x06001950 RID: 6480 RVA: 0x00013A8E File Offset: 0x00011C8E
			public RectTransform rectTransform { get; private set; }

			// Token: 0x06001951 RID: 6481 RVA: 0x0006C554 File Offset: 0x0006A754
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

			// Token: 0x06001952 RID: 6482 RVA: 0x0006C5D8 File Offset: 0x0006A7D8
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

			// Token: 0x06001953 RID: 6483 RVA: 0x00013A97 File Offset: 0x00011C97
			public virtual void SetInteractible(bool state, bool playTransition)
			{
				this.SetInteractible(state, playTransition, false);
			}

			// Token: 0x06001954 RID: 6484 RVA: 0x0006C648 File Offset: 0x0006A848
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

			// Token: 0x06001955 RID: 6485 RVA: 0x0006C6CC File Offset: 0x0006A8CC
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

			// Token: 0x06001956 RID: 6486 RVA: 0x0006C718 File Offset: 0x0006A918
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

			// Token: 0x06001957 RID: 6487 RVA: 0x00013AA2 File Offset: 0x00011CA2
			public virtual void SetLabel(string label)
			{
				if (this.text == null)
				{
					return;
				}
				this.text.text = label;
			}

			// Token: 0x06001958 RID: 6488 RVA: 0x00013ABF File Offset: 0x00011CBF
			public virtual string GetLabel()
			{
				if (this.text == null)
				{
					return string.Empty;
				}
				return this.text.text;
			}

			// Token: 0x06001959 RID: 6489 RVA: 0x00013AE0 File Offset: 0x00011CE0
			public virtual void AddChild(ControlMapper.GUIElement child)
			{
				this.children.Add(child);
			}

			// Token: 0x0600195A RID: 6490 RVA: 0x00013AEE File Offset: 0x00011CEE
			public void SetElementInfoData(string identifier, int intData)
			{
				if (this.uiElementInfo == null)
				{
					return;
				}
				this.uiElementInfo.identifier = identifier;
				this.uiElementInfo.intData = intData;
			}

			// Token: 0x0600195B RID: 6491 RVA: 0x00013B17 File Offset: 0x00011D17
			public virtual void SetActive(bool state)
			{
				if (this.gameObject == null)
				{
					return;
				}
				this.gameObject.SetActive(state);
			}

			// Token: 0x0600195C RID: 6492 RVA: 0x0006C77C File Offset: 0x0006A97C
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

			public readonly GameObject gameObject;

			protected readonly Text text;

			public readonly Selectable selectable;

			protected readonly UIElementInfo uiElementInfo;

			protected bool permanentStateSet;

			protected readonly List<ControlMapper.GUIElement> children;
		}

		private class GUIButton : ControlMapper.GUIElement
		{
			// (get) Token: 0x0600195D RID: 6493 RVA: 0x00013B34 File Offset: 0x00011D34
			protected Button button
			{
				get
				{
					return this.selectable as Button;
				}
			}

			// (get) Token: 0x0600195E RID: 6494 RVA: 0x00013B41 File Offset: 0x00011D41
			public ButtonInfo buttonInfo
			{
				get
				{
					return this.uiElementInfo as ButtonInfo;
				}
			}

			// Token: 0x0600195F RID: 6495 RVA: 0x00013B4E File Offset: 0x00011D4E
			public GUIButton(GameObject gameObject)
				: base(gameObject)
			{
				this.Init();
			}

			// Token: 0x06001960 RID: 6496 RVA: 0x00013B5E File Offset: 0x00011D5E
			public GUIButton(Button button, Text label)
				: base(button, label)
			{
				this.Init();
			}

			// Token: 0x06001961 RID: 6497 RVA: 0x00013B6F File Offset: 0x00011D6F
			public void SetButtonInfoData(string identifier, int intData)
			{
				base.SetElementInfoData(identifier, intData);
			}

			// Token: 0x06001962 RID: 6498 RVA: 0x0006C814 File Offset: 0x0006AA14
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

		private class GUIInputField : ControlMapper.GUIElement
		{
			// (get) Token: 0x06001965 RID: 6501 RVA: 0x00013B34 File Offset: 0x00011D34
			protected Button button
			{
				get
				{
					return this.selectable as Button;
				}
			}

			// (get) Token: 0x06001966 RID: 6502 RVA: 0x00013B91 File Offset: 0x00011D91
			public InputFieldInfo fieldInfo
			{
				get
				{
					return this.uiElementInfo as InputFieldInfo;
				}
			}

			// (get) Token: 0x06001967 RID: 6503 RVA: 0x00013B9E File Offset: 0x00011D9E
			public bool hasToggle
			{
				get
				{
					return this.toggle != null;
				}
			}

			// (get) Token: 0x06001968 RID: 6504 RVA: 0x00013BA9 File Offset: 0x00011DA9
			// (set) Token: 0x06001969 RID: 6505 RVA: 0x00013BB1 File Offset: 0x00011DB1
			public ControlMapper.GUIToggle toggle { get; private set; }

			// (get) Token: 0x0600196A RID: 6506 RVA: 0x00013BBA File Offset: 0x00011DBA
			// (set) Token: 0x0600196B RID: 6507 RVA: 0x00013BD7 File Offset: 0x00011DD7
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

			// (get) Token: 0x0600196C RID: 6508 RVA: 0x00013BF4 File Offset: 0x00011DF4
			// (set) Token: 0x0600196D RID: 6509 RVA: 0x00013C11 File Offset: 0x00011E11
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

			// Token: 0x0600196E RID: 6510 RVA: 0x00013B4E File Offset: 0x00011D4E
			public GUIInputField(GameObject gameObject)
				: base(gameObject)
			{
				this.Init();
			}

			// Token: 0x0600196F RID: 6511 RVA: 0x00013B5E File Offset: 0x00011D5E
			public GUIInputField(Button button, Text label)
				: base(button, label)
			{
				this.Init();
			}

			// Token: 0x06001970 RID: 6512 RVA: 0x0006C860 File Offset: 0x0006AA60
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

			// Token: 0x06001971 RID: 6513 RVA: 0x0006C8B0 File Offset: 0x0006AAB0
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

			// Token: 0x06001972 RID: 6514 RVA: 0x00013C2E File Offset: 0x00011E2E
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

			// Token: 0x06001973 RID: 6515 RVA: 0x00013C5B File Offset: 0x00011E5B
			public void AddToggle(ControlMapper.GUIToggle toggle)
			{
				if (toggle == null)
				{
					return;
				}
				this.toggle = toggle;
			}

			// Token: 0x06001974 RID: 6516 RVA: 0x0006C8FC File Offset: 0x0006AAFC
			public void SetDisplay(GameObject buttonDisplay)
			{
				if (this.buttonDisplay != null)
				{
					global::UnityEngine.Object.Destroy(this.buttonDisplay);
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

			// Token: 0x06001975 RID: 6517 RVA: 0x0006C9B8 File Offset: 0x0006ABB8
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

			public GameObject buttonDisplay;
		}

		private class GUIToggle : ControlMapper.GUIElement
		{
			// (get) Token: 0x06001978 RID: 6520 RVA: 0x00013C80 File Offset: 0x00011E80
			protected Toggle toggle
			{
				get
				{
					return this.selectable as Toggle;
				}
			}

			// (get) Token: 0x06001979 RID: 6521 RVA: 0x00013C8D File Offset: 0x00011E8D
			public ToggleInfo toggleInfo
			{
				get
				{
					return this.uiElementInfo as ToggleInfo;
				}
			}

			// (get) Token: 0x0600197A RID: 6522 RVA: 0x00013C9A File Offset: 0x00011E9A
			// (set) Token: 0x0600197B RID: 6523 RVA: 0x00013CB7 File Offset: 0x00011EB7
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

			// Token: 0x0600197C RID: 6524 RVA: 0x00013B4E File Offset: 0x00011D4E
			public GUIToggle(GameObject gameObject)
				: base(gameObject)
			{
				this.Init();
			}

			// Token: 0x0600197D RID: 6525 RVA: 0x00013B5E File Offset: 0x00011D5E
			public GUIToggle(Toggle toggle, Text label)
				: base(toggle, label)
			{
				this.Init();
			}

			// Token: 0x0600197E RID: 6526 RVA: 0x0006CA0C File Offset: 0x0006AC0C
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

			// Token: 0x0600197F RID: 6527 RVA: 0x0006CA5C File Offset: 0x0006AC5C
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

			// Token: 0x06001980 RID: 6528 RVA: 0x00013CD4 File Offset: 0x00011ED4
			public void SetToggleState(bool state)
			{
				if (this.toggle == null)
				{
					return;
				}
				this.toggle.isOn = state;
			}
		}

		private class GUILabel
		{
			// (get) Token: 0x06001983 RID: 6531 RVA: 0x00013CF1 File Offset: 0x00011EF1
			// (set) Token: 0x06001984 RID: 6532 RVA: 0x00013CF9 File Offset: 0x00011EF9
			public GameObject gameObject { get; private set; }

			// (get) Token: 0x06001985 RID: 6533 RVA: 0x00013D02 File Offset: 0x00011F02
			// (set) Token: 0x06001986 RID: 6534 RVA: 0x00013D0A File Offset: 0x00011F0A
			private Text text { get; set; }

			// (get) Token: 0x06001987 RID: 6535 RVA: 0x00013D13 File Offset: 0x00011F13
			// (set) Token: 0x06001988 RID: 6536 RVA: 0x00013D1B File Offset: 0x00011F1B
			public RectTransform rectTransform { get; private set; }

			// Token: 0x06001989 RID: 6537 RVA: 0x00013D24 File Offset: 0x00011F24
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

			// Token: 0x0600198A RID: 6538 RVA: 0x00013D53 File Offset: 0x00011F53
			public GUILabel(Text label)
			{
				this.text = label;
				this.Check();
			}

			// Token: 0x0600198B RID: 6539 RVA: 0x00013D69 File Offset: 0x00011F69
			public void SetSize(int width, int height)
			{
				if (this.text == null)
				{
					return;
				}
				this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)width);
				this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)height);
			}

			// Token: 0x0600198C RID: 6540 RVA: 0x00013D96 File Offset: 0x00011F96
			public void SetWidth(int width)
			{
				if (this.text == null)
				{
					return;
				}
				this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)width);
			}

			// Token: 0x0600198D RID: 6541 RVA: 0x00013DB5 File Offset: 0x00011FB5
			public void SetHeight(int height)
			{
				if (this.text == null)
				{
					return;
				}
				this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)height);
			}

			// Token: 0x0600198E RID: 6542 RVA: 0x00013DD4 File Offset: 0x00011FD4
			public void SetLabel(string label)
			{
				if (this.text == null)
				{
					return;
				}
				this.text.text = label;
			}

			// Token: 0x0600198F RID: 6543 RVA: 0x00013DF1 File Offset: 0x00011FF1
			public void SetFontStyle(FontStyle style)
			{
				if (this.text == null)
				{
					return;
				}
				this.text.fontStyle = style;
			}

			// Token: 0x06001990 RID: 6544 RVA: 0x00013E0E File Offset: 0x0001200E
			public void SetTextAlignment(TextAnchor alignment)
			{
				if (this.text == null)
				{
					return;
				}
				this.text.alignment = alignment;
			}

			// Token: 0x06001991 RID: 6545 RVA: 0x00013E2B File Offset: 0x0001202B
			public void SetActive(bool state)
			{
				if (this.gameObject == null)
				{
					return;
				}
				this.gameObject.SetActive(state);
			}

			// Token: 0x06001992 RID: 6546 RVA: 0x0006CB78 File Offset: 0x0006AD78
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

		[Serializable]
		public class MappingSet
		{
			// (get) Token: 0x06001993 RID: 6547 RVA: 0x00013E48 File Offset: 0x00012048
			public int mapCategoryId
			{
				get
				{
					return this._mapCategoryId;
				}
			}

			// (get) Token: 0x06001994 RID: 6548 RVA: 0x00013E50 File Offset: 0x00012050
			public ControlMapper.MappingSet.ActionListMode actionListMode
			{
				get
				{
					return this._actionListMode;
				}
			}

			// (get) Token: 0x06001995 RID: 6549 RVA: 0x00013E58 File Offset: 0x00012058
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

			// (get) Token: 0x06001996 RID: 6550 RVA: 0x00013E83 File Offset: 0x00012083
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

			// (get) Token: 0x06001997 RID: 6551 RVA: 0x00013EAE File Offset: 0x000120AE
			public bool isValid
			{
				get
				{
					return this._mapCategoryId >= 0 && ReInput.mapping.GetMapCategory(this._mapCategoryId) != null;
				}
			}

			// Token: 0x06001998 RID: 6552 RVA: 0x00013ECE File Offset: 0x000120CE
			public MappingSet()
			{
				this._mapCategoryId = -1;
				this._actionCategoryIds = new int[0];
				this._actionIds = new int[0];
				this._actionListMode = ControlMapper.MappingSet.ActionListMode.ActionCategory;
			}

			// Token: 0x06001999 RID: 6553 RVA: 0x00013EFC File Offset: 0x000120FC
			private MappingSet(int mapCategoryId, ControlMapper.MappingSet.ActionListMode actionListMode, int[] actionCategoryIds, int[] actionIds)
			{
				this._mapCategoryId = mapCategoryId;
				this._actionListMode = actionListMode;
				this._actionCategoryIds = actionCategoryIds;
				this._actionIds = actionIds;
			}

			// (get) Token: 0x0600199A RID: 6554 RVA: 0x00013F21 File Offset: 0x00012121
			public static ControlMapper.MappingSet Default
			{
				get
				{
					return new ControlMapper.MappingSet(0, ControlMapper.MappingSet.ActionListMode.ActionCategory, new int[1], new int[0]);
				}
			}

			[SerializeField]
			[Tooltip("The Map Category that will be displayed to the user for remapping.")]
			private int _mapCategoryId;

			[SerializeField]
			[Tooltip("Choose whether you want to list Actions to display for this Map Category by individual Action or by all the Actions in an Action Category.")]
			private ControlMapper.MappingSet.ActionListMode _actionListMode;

			[SerializeField]
			private int[] _actionCategoryIds;

			[SerializeField]
			private int[] _actionIds;

			private IList<int> _actionCategoryIdsReadOnly;

			private IList<int> _actionIdsReadOnly;

			public enum ActionListMode
			{
				ActionCategory,
				Action
			}
		}

		[Serializable]
		public class InputBehaviorSettings
		{
			// (get) Token: 0x0600199B RID: 6555 RVA: 0x00013F36 File Offset: 0x00012136
			public int inputBehaviorId
			{
				get
				{
					return this._inputBehaviorId;
				}
			}

			// (get) Token: 0x0600199C RID: 6556 RVA: 0x00013F3E File Offset: 0x0001213E
			public bool showJoystickAxisSensitivity
			{
				get
				{
					return this._showJoystickAxisSensitivity;
				}
			}

			// (get) Token: 0x0600199D RID: 6557 RVA: 0x00013F46 File Offset: 0x00012146
			public bool showMouseXYAxisSensitivity
			{
				get
				{
					return this._showMouseXYAxisSensitivity;
				}
			}

			// (get) Token: 0x0600199E RID: 6558 RVA: 0x00013F4E File Offset: 0x0001214E
			public string labelLanguageKey
			{
				get
				{
					return this._labelLanguageKey;
				}
			}

			// (get) Token: 0x0600199F RID: 6559 RVA: 0x00013F56 File Offset: 0x00012156
			public string joystickAxisSensitivityLabelLanguageKey
			{
				get
				{
					return this._joystickAxisSensitivityLabelLanguageKey;
				}
			}

			// (get) Token: 0x060019A0 RID: 6560 RVA: 0x00013F5E File Offset: 0x0001215E
			public string mouseXYAxisSensitivityLabelLanguageKey
			{
				get
				{
					return this._mouseXYAxisSensitivityLabelLanguageKey;
				}
			}

			// (get) Token: 0x060019A1 RID: 6561 RVA: 0x00013F66 File Offset: 0x00012166
			public Sprite joystickAxisSensitivityIcon
			{
				get
				{
					return this._joystickAxisSensitivityIcon;
				}
			}

			// (get) Token: 0x060019A2 RID: 6562 RVA: 0x00013F6E File Offset: 0x0001216E
			public Sprite mouseXYAxisSensitivityIcon
			{
				get
				{
					return this._mouseXYAxisSensitivityIcon;
				}
			}

			// (get) Token: 0x060019A3 RID: 6563 RVA: 0x00013F76 File Offset: 0x00012176
			public float joystickAxisSensitivityMin
			{
				get
				{
					return this._joystickAxisSensitivityMin;
				}
			}

			// (get) Token: 0x060019A4 RID: 6564 RVA: 0x00013F7E File Offset: 0x0001217E
			public float joystickAxisSensitivityMax
			{
				get
				{
					return this._joystickAxisSensitivityMax;
				}
			}

			// (get) Token: 0x060019A5 RID: 6565 RVA: 0x00013F86 File Offset: 0x00012186
			public float mouseXYAxisSensitivityMin
			{
				get
				{
					return this._mouseXYAxisSensitivityMin;
				}
			}

			// (get) Token: 0x060019A6 RID: 6566 RVA: 0x00013F8E File Offset: 0x0001218E
			public float mouseXYAxisSensitivityMax
			{
				get
				{
					return this._mouseXYAxisSensitivityMax;
				}
			}

			// (get) Token: 0x060019A7 RID: 6567 RVA: 0x00013F96 File Offset: 0x00012196
			public bool isValid
			{
				get
				{
					return this._inputBehaviorId >= 0 && (this._showJoystickAxisSensitivity || this._showMouseXYAxisSensitivity);
				}
			}

			[SerializeField]
			[Tooltip("The Input Behavior that will be displayed to the user for modification.")]
			private int _inputBehaviorId = -1;

			[SerializeField]
			[Tooltip("If checked, a slider will be displayed so the user can change this value.")]
			private bool _showJoystickAxisSensitivity = true;

			[SerializeField]
			[Tooltip("If checked, a slider will be displayed so the user can change this value.")]
			private bool _showMouseXYAxisSensitivity = true;

			[SerializeField]
			[Tooltip("If set to a non-blank value, this key will be used to look up the name in Language to be displayed as the title for the Input Behavior control set. Otherwise, the name field of the InputBehavior will be used.")]
			private string _labelLanguageKey = string.Empty;

			[SerializeField]
			[Tooltip("If set to a non-blank value, this name will be displayed above the individual slider control. Otherwise, no name will be displayed.")]
			private string _joystickAxisSensitivityLabelLanguageKey = string.Empty;

			[SerializeField]
			[Tooltip("If set to a non-blank value, this key will be used to look up the name in Language to be displayed above the individual slider control. Otherwise, no name will be displayed.")]
			private string _mouseXYAxisSensitivityLabelLanguageKey = string.Empty;

			[SerializeField]
			[Tooltip("The icon to display next to the slider. Set to none for no icon.")]
			private Sprite _joystickAxisSensitivityIcon;

			[SerializeField]
			[Tooltip("The icon to display next to the slider. Set to none for no icon.")]
			private Sprite _mouseXYAxisSensitivityIcon;

			[SerializeField]
			[Tooltip("Minimum value the user is allowed to set for this property.")]
			private float _joystickAxisSensitivityMin;

			[SerializeField]
			[Tooltip("Maximum value the user is allowed to set for this property.")]
			private float _joystickAxisSensitivityMax = 2f;

			[SerializeField]
			[Tooltip("Minimum value the user is allowed to set for this property.")]
			private float _mouseXYAxisSensitivityMin;

			[SerializeField]
			[Tooltip("Maximum value the user is allowed to set for this property.")]
			private float _mouseXYAxisSensitivityMax = 2f;
		}

		[Serializable]
		private class Prefabs
		{
			// (get) Token: 0x060019A9 RID: 6569 RVA: 0x00013FB3 File Offset: 0x000121B3
			public GameObject button
			{
				get
				{
					return this._button;
				}
			}

			// (get) Token: 0x060019AA RID: 6570 RVA: 0x00013FBB File Offset: 0x000121BB
			public GameObject fitButton
			{
				get
				{
					return this._fitButton;
				}
			}

			// (get) Token: 0x060019AB RID: 6571 RVA: 0x00013FC3 File Offset: 0x000121C3
			public GameObject inputGridLabel
			{
				get
				{
					return this._inputGridLabel;
				}
			}

			// (get) Token: 0x060019AC RID: 6572 RVA: 0x00013FCB File Offset: 0x000121CB
			public GameObject inputGridHeaderLabel
			{
				get
				{
					return this._inputGridHeaderLabel;
				}
			}

			// (get) Token: 0x060019AD RID: 6573 RVA: 0x00013FD3 File Offset: 0x000121D3
			public GameObject inputGridFieldButton
			{
				get
				{
					return this._inputGridFieldButton;
				}
			}

			// (get) Token: 0x060019AE RID: 6574 RVA: 0x00013FDB File Offset: 0x000121DB
			public GameObject inputGridFieldInvertToggle
			{
				get
				{
					return this._inputGridFieldInvertToggle;
				}
			}

			// (get) Token: 0x060019AF RID: 6575 RVA: 0x00013FE3 File Offset: 0x000121E3
			public GameObject window
			{
				get
				{
					return this._window;
				}
			}

			// (get) Token: 0x060019B0 RID: 6576 RVA: 0x00013FEB File Offset: 0x000121EB
			public GameObject windowTitleText
			{
				get
				{
					return this._windowTitleText;
				}
			}

			// (get) Token: 0x060019B1 RID: 6577 RVA: 0x00013FF3 File Offset: 0x000121F3
			public GameObject windowContentText
			{
				get
				{
					return this._windowContentText;
				}
			}

			// (get) Token: 0x060019B2 RID: 6578 RVA: 0x00013FFB File Offset: 0x000121FB
			public GameObject fader
			{
				get
				{
					return this._fader;
				}
			}

			// (get) Token: 0x060019B3 RID: 6579 RVA: 0x00014003 File Offset: 0x00012203
			public GameObject calibrationWindow
			{
				get
				{
					return this._calibrationWindow;
				}
			}

			// (get) Token: 0x060019B4 RID: 6580 RVA: 0x0001400B File Offset: 0x0001220B
			public GameObject inputBehaviorsWindow
			{
				get
				{
					return this._inputBehaviorsWindow;
				}
			}

			// (get) Token: 0x060019B5 RID: 6581 RVA: 0x00014013 File Offset: 0x00012213
			public GameObject centerStickGraphic
			{
				get
				{
					return this._centerStickGraphic;
				}
			}

			// (get) Token: 0x060019B6 RID: 6582 RVA: 0x0001401B File Offset: 0x0001221B
			public GameObject moveStickGraphic
			{
				get
				{
					return this._moveStickGraphic;
				}
			}

			// Token: 0x060019B7 RID: 6583 RVA: 0x0006CC24 File Offset: 0x0006AE24
			public bool Check()
			{
				return !(this._button == null) && !(this._fitButton == null) && !(this._inputGridLabel == null) && !(this._inputGridHeaderLabel == null) && !(this._inputGridFieldButton == null) && !(this._inputGridFieldInvertToggle == null) && !(this._window == null) && !(this._windowTitleText == null) && !(this._windowContentText == null) && !(this._fader == null) && !(this._calibrationWindow == null) && !(this._inputBehaviorsWindow == null);
			}

			[SerializeField]
			private GameObject _button;

			[SerializeField]
			private GameObject _fitButton;

			[SerializeField]
			private GameObject _inputGridLabel;

			[SerializeField]
			private GameObject _inputGridHeaderLabel;

			[SerializeField]
			private GameObject _inputGridFieldButton;

			[SerializeField]
			private GameObject _inputGridFieldInvertToggle;

			[SerializeField]
			private GameObject _window;

			[SerializeField]
			private GameObject _windowTitleText;

			[SerializeField]
			private GameObject _windowContentText;

			[SerializeField]
			private GameObject _fader;

			[SerializeField]
			private GameObject _calibrationWindow;

			[SerializeField]
			private GameObject _inputBehaviorsWindow;

			[SerializeField]
			private GameObject _centerStickGraphic;

			[SerializeField]
			private GameObject _moveStickGraphic;
		}

		[Serializable]
		private class References
		{
			// (get) Token: 0x060019B9 RID: 6585 RVA: 0x00014023 File Offset: 0x00012223
			public Canvas canvas
			{
				get
				{
					return this._canvas;
				}
			}

			// (get) Token: 0x060019BA RID: 6586 RVA: 0x0001402B File Offset: 0x0001222B
			public CanvasGroup mainCanvasGroup
			{
				get
				{
					return this._mainCanvasGroup;
				}
			}

			// (get) Token: 0x060019BB RID: 6587 RVA: 0x00014033 File Offset: 0x00012233
			public Transform mainContent
			{
				get
				{
					return this._mainContent;
				}
			}

			// (get) Token: 0x060019BC RID: 6588 RVA: 0x0001403B File Offset: 0x0001223B
			public Transform mainContentInner
			{
				get
				{
					return this._mainContentInner;
				}
			}

			// (get) Token: 0x060019BD RID: 6589 RVA: 0x00014043 File Offset: 0x00012243
			public UIGroup playersGroup
			{
				get
				{
					return this._playersGroup;
				}
			}

			// (get) Token: 0x060019BE RID: 6590 RVA: 0x0001404B File Offset: 0x0001224B
			public Transform controllerGroup
			{
				get
				{
					return this._controllerGroup;
				}
			}

			// (get) Token: 0x060019BF RID: 6591 RVA: 0x00014053 File Offset: 0x00012253
			public Transform controllerGroupLabelGroup
			{
				get
				{
					return this._controllerGroupLabelGroup;
				}
			}

			// (get) Token: 0x060019C0 RID: 6592 RVA: 0x0001405B File Offset: 0x0001225B
			public UIGroup controllerSettingsGroup
			{
				get
				{
					return this._controllerSettingsGroup;
				}
			}

			// (get) Token: 0x060019C1 RID: 6593 RVA: 0x00014063 File Offset: 0x00012263
			public UIGroup assignedControllersGroup
			{
				get
				{
					return this._assignedControllersGroup;
				}
			}

			// (get) Token: 0x060019C2 RID: 6594 RVA: 0x0001406B File Offset: 0x0001226B
			public Transform settingsAndMapCategoriesGroup
			{
				get
				{
					return this._settingsAndMapCategoriesGroup;
				}
			}

			// (get) Token: 0x060019C3 RID: 6595 RVA: 0x00014073 File Offset: 0x00012273
			public UIGroup settingsGroup
			{
				get
				{
					return this._settingsGroup;
				}
			}

			// (get) Token: 0x060019C4 RID: 6596 RVA: 0x0001407B File Offset: 0x0001227B
			public UIGroup mapCategoriesGroup
			{
				get
				{
					return this._mapCategoriesGroup;
				}
			}

			// (get) Token: 0x060019C5 RID: 6597 RVA: 0x00014083 File Offset: 0x00012283
			public Transform inputGridGroup
			{
				get
				{
					return this._inputGridGroup;
				}
			}

			// (get) Token: 0x060019C6 RID: 6598 RVA: 0x0001408B File Offset: 0x0001228B
			public Transform inputGridContainer
			{
				get
				{
					return this._inputGridContainer;
				}
			}

			// (get) Token: 0x060019C7 RID: 6599 RVA: 0x00014093 File Offset: 0x00012293
			public Transform inputGridHeadersGroup
			{
				get
				{
					return this._inputGridHeadersGroup;
				}
			}

			// (get) Token: 0x060019C8 RID: 6600 RVA: 0x0001409B File Offset: 0x0001229B
			public Scrollbar inputGridVScrollbar
			{
				get
				{
					return this._inputGridVScrollbar;
				}
			}

			// (get) Token: 0x060019C9 RID: 6601 RVA: 0x000140A3 File Offset: 0x000122A3
			public ScrollRect inputGridScrollRect
			{
				get
				{
					return this._inputGridScrollRect;
				}
			}

			// (get) Token: 0x060019CA RID: 6602 RVA: 0x000140AB File Offset: 0x000122AB
			public Transform inputGridInnerGroup
			{
				get
				{
					return this._inputGridInnerGroup;
				}
			}

			// (get) Token: 0x060019CB RID: 6603 RVA: 0x000140B3 File Offset: 0x000122B3
			public Text controllerNameLabel
			{
				get
				{
					return this._controllerNameLabel;
				}
			}

			// (get) Token: 0x060019CC RID: 6604 RVA: 0x000140BB File Offset: 0x000122BB
			public Button removeControllerButton
			{
				get
				{
					return this._removeControllerButton;
				}
			}

			// (get) Token: 0x060019CD RID: 6605 RVA: 0x000140C3 File Offset: 0x000122C3
			public Button assignControllerButton
			{
				get
				{
					return this._assignControllerButton;
				}
			}

			// (get) Token: 0x060019CE RID: 6606 RVA: 0x000140CB File Offset: 0x000122CB
			public Button calibrateControllerButton
			{
				get
				{
					return this._calibrateControllerButton;
				}
			}

			// (get) Token: 0x060019CF RID: 6607 RVA: 0x000140D3 File Offset: 0x000122D3
			public Button doneButton
			{
				get
				{
					return this._doneButton;
				}
			}

			// (get) Token: 0x060019D0 RID: 6608 RVA: 0x000140DB File Offset: 0x000122DB
			public Button restoreDefaultsButton
			{
				get
				{
					return this._restoreDefaultsButton;
				}
			}

			// (get) Token: 0x060019D1 RID: 6609 RVA: 0x000140E3 File Offset: 0x000122E3
			public Selectable defaultSelection
			{
				get
				{
					return this._defaultSelection;
				}
			}

			// (get) Token: 0x060019D2 RID: 6610 RVA: 0x000140EB File Offset: 0x000122EB
			public GameObject[] fixedSelectableUIElements
			{
				get
				{
					return this._fixedSelectableUIElements;
				}
			}

			// (get) Token: 0x060019D3 RID: 6611 RVA: 0x000140F3 File Offset: 0x000122F3
			public Image mainBackgroundImage
			{
				get
				{
					return this._mainBackgroundImage;
				}
			}

			// (get) Token: 0x060019D4 RID: 6612 RVA: 0x000140FB File Offset: 0x000122FB
			// (set) Token: 0x060019D5 RID: 6613 RVA: 0x00014103 File Offset: 0x00012303
			public LayoutElement inputGridLayoutElement { get; set; }

			// (get) Token: 0x060019D6 RID: 6614 RVA: 0x0001410C File Offset: 0x0001230C
			// (set) Token: 0x060019D7 RID: 6615 RVA: 0x00014114 File Offset: 0x00012314
			public Transform inputGridActionColumn { get; set; }

			// (get) Token: 0x060019D8 RID: 6616 RVA: 0x0001411D File Offset: 0x0001231D
			// (set) Token: 0x060019D9 RID: 6617 RVA: 0x00014125 File Offset: 0x00012325
			public Transform inputGridKeyboardColumn { get; set; }

			// (get) Token: 0x060019DA RID: 6618 RVA: 0x0001412E File Offset: 0x0001232E
			// (set) Token: 0x060019DB RID: 6619 RVA: 0x00014136 File Offset: 0x00012336
			public Transform inputGridMouseColumn { get; set; }

			// (get) Token: 0x060019DC RID: 6620 RVA: 0x0001413F File Offset: 0x0001233F
			// (set) Token: 0x060019DD RID: 6621 RVA: 0x00014147 File Offset: 0x00012347
			public Transform inputGridControllerColumn { get; set; }

			// (get) Token: 0x060019DE RID: 6622 RVA: 0x00014150 File Offset: 0x00012350
			// (set) Token: 0x060019DF RID: 6623 RVA: 0x00014158 File Offset: 0x00012358
			public Transform inputGridHeader1 { get; set; }

			// (get) Token: 0x060019E0 RID: 6624 RVA: 0x00014161 File Offset: 0x00012361
			// (set) Token: 0x060019E1 RID: 6625 RVA: 0x00014169 File Offset: 0x00012369
			public Transform inputGridHeader2 { get; set; }

			// (get) Token: 0x060019E2 RID: 6626 RVA: 0x00014172 File Offset: 0x00012372
			// (set) Token: 0x060019E3 RID: 6627 RVA: 0x0001417A File Offset: 0x0001237A
			public Transform inputGridHeader3 { get; set; }

			// (get) Token: 0x060019E4 RID: 6628 RVA: 0x00014183 File Offset: 0x00012383
			// (set) Token: 0x060019E5 RID: 6629 RVA: 0x0001418B File Offset: 0x0001238B
			public Transform inputGridHeader4 { get; set; }

			// Token: 0x060019E6 RID: 6630 RVA: 0x0006CCE4 File Offset: 0x0006AEE4
			public bool Check()
			{
				return !(this._canvas == null) && !(this._mainCanvasGroup == null) && !(this._mainContent == null) && !(this._mainContentInner == null) && !(this._playersGroup == null) && !(this._controllerGroup == null) && !(this._controllerGroupLabelGroup == null) && !(this._controllerSettingsGroup == null) && !(this._assignedControllersGroup == null) && !(this._settingsAndMapCategoriesGroup == null) && !(this._settingsGroup == null) && !(this._mapCategoriesGroup == null) && !(this._inputGridGroup == null) && !(this._inputGridContainer == null) && !(this._inputGridHeadersGroup == null) && !(this._inputGridVScrollbar == null) && !(this._inputGridScrollRect == null) && !(this._inputGridInnerGroup == null) && !(this._controllerNameLabel == null) && !(this._removeControllerButton == null) && !(this._assignControllerButton == null) && !(this._calibrateControllerButton == null) && !(this._doneButton == null) && !(this._restoreDefaultsButton == null) && !(this._defaultSelection == null);
			}

			[SerializeField]
			private Canvas _canvas;

			[SerializeField]
			private CanvasGroup _mainCanvasGroup;

			[SerializeField]
			private Transform _mainContent;

			[SerializeField]
			private Transform _mainContentInner;

			[SerializeField]
			private UIGroup _playersGroup;

			[SerializeField]
			private Transform _controllerGroup;

			[SerializeField]
			private Transform _controllerGroupLabelGroup;

			[SerializeField]
			private UIGroup _controllerSettingsGroup;

			[SerializeField]
			private UIGroup _assignedControllersGroup;

			[SerializeField]
			private Transform _settingsAndMapCategoriesGroup;

			[SerializeField]
			private UIGroup _settingsGroup;

			[SerializeField]
			private UIGroup _mapCategoriesGroup;

			[SerializeField]
			private Transform _inputGridGroup;

			[SerializeField]
			private Transform _inputGridContainer;

			[SerializeField]
			private Transform _inputGridHeadersGroup;

			[SerializeField]
			private Scrollbar _inputGridVScrollbar;

			[SerializeField]
			private ScrollRect _inputGridScrollRect;

			[SerializeField]
			private Transform _inputGridInnerGroup;

			[SerializeField]
			private Text _controllerNameLabel;

			[SerializeField]
			private Button _removeControllerButton;

			[SerializeField]
			private Button _assignControllerButton;

			[SerializeField]
			private Button _calibrateControllerButton;

			[SerializeField]
			private Button _doneButton;

			[SerializeField]
			private Button _restoreDefaultsButton;

			[SerializeField]
			private Selectable _defaultSelection;

			[SerializeField]
			private GameObject[] _fixedSelectableUIElements;

			[SerializeField]
			private Image _mainBackgroundImage;
		}

		private class InputActionSet
		{
			// (get) Token: 0x060019E8 RID: 6632 RVA: 0x00014194 File Offset: 0x00012394
			public int actionId
			{
				get
				{
					return this._actionId;
				}
			}

			// (get) Token: 0x060019E9 RID: 6633 RVA: 0x0001419C File Offset: 0x0001239C
			public AxisRange axisRange
			{
				get
				{
					return this._axisRange;
				}
			}

			// Token: 0x060019EA RID: 6634 RVA: 0x000141A4 File Offset: 0x000123A4
			public InputActionSet(int actionId, AxisRange axisRange)
			{
				this._actionId = actionId;
				this._axisRange = axisRange;
			}

			private int _actionId;

			private AxisRange _axisRange;
		}

		private class InputMapping
		{
			// (get) Token: 0x060019EB RID: 6635 RVA: 0x000141BA File Offset: 0x000123BA
			// (set) Token: 0x060019EC RID: 6636 RVA: 0x000141C2 File Offset: 0x000123C2
			public string actionName { get; private set; }

			// (get) Token: 0x060019ED RID: 6637 RVA: 0x000141CB File Offset: 0x000123CB
			// (set) Token: 0x060019EE RID: 6638 RVA: 0x000141D3 File Offset: 0x000123D3
			public InputFieldInfo fieldInfo { get; private set; }

			// (get) Token: 0x060019EF RID: 6639 RVA: 0x000141DC File Offset: 0x000123DC
			// (set) Token: 0x060019F0 RID: 6640 RVA: 0x000141E4 File Offset: 0x000123E4
			public ControllerMap map { get; private set; }

			// (get) Token: 0x060019F1 RID: 6641 RVA: 0x000141ED File Offset: 0x000123ED
			// (set) Token: 0x060019F2 RID: 6642 RVA: 0x000141F5 File Offset: 0x000123F5
			public ActionElementMap aem { get; private set; }

			// (get) Token: 0x060019F3 RID: 6643 RVA: 0x000141FE File Offset: 0x000123FE
			// (set) Token: 0x060019F4 RID: 6644 RVA: 0x00014206 File Offset: 0x00012406
			public ControllerType controllerType { get; private set; }

			// (get) Token: 0x060019F5 RID: 6645 RVA: 0x0001420F File Offset: 0x0001240F
			// (set) Token: 0x060019F6 RID: 6646 RVA: 0x00014217 File Offset: 0x00012417
			public int controllerId { get; private set; }

			// (get) Token: 0x060019F7 RID: 6647 RVA: 0x00014220 File Offset: 0x00012420
			// (set) Token: 0x060019F8 RID: 6648 RVA: 0x00014228 File Offset: 0x00012428
			public ControllerPollingInfo pollingInfo { get; set; }

			// (get) Token: 0x060019F9 RID: 6649 RVA: 0x00014231 File Offset: 0x00012431
			// (set) Token: 0x060019FA RID: 6650 RVA: 0x00014239 File Offset: 0x00012439
			public ModifierKeyFlags modifierKeyFlags { get; set; }

			// (get) Token: 0x060019FB RID: 6651 RVA: 0x0006CE80 File Offset: 0x0006B080
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

			// (get) Token: 0x060019FC RID: 6652 RVA: 0x0006CEC8 File Offset: 0x0006B0C8
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

			// Token: 0x060019FD RID: 6653 RVA: 0x00014242 File Offset: 0x00012442
			public InputMapping(string actionName, InputFieldInfo fieldInfo, ControllerMap map, ActionElementMap aem, ControllerType controllerType, int controllerId)
			{
				this.actionName = actionName;
				this.fieldInfo = fieldInfo;
				this.map = map;
				this.aem = aem;
				this.controllerType = controllerType;
				this.controllerId = controllerId;
			}

			// Token: 0x060019FE RID: 6654 RVA: 0x00014277 File Offset: 0x00012477
			public ElementAssignment ToElementAssignment(ControllerPollingInfo pollingInfo)
			{
				this.pollingInfo = pollingInfo;
				return this.ToElementAssignment();
			}

			// Token: 0x060019FF RID: 6655 RVA: 0x00014286 File Offset: 0x00012486
			public ElementAssignment ToElementAssignment(ControllerPollingInfo pollingInfo, ModifierKeyFlags modifierKeyFlags)
			{
				this.pollingInfo = pollingInfo;
				this.modifierKeyFlags = modifierKeyFlags;
				return this.ToElementAssignment();
			}

			// Token: 0x06001A00 RID: 6656 RVA: 0x0006CF38 File Offset: 0x0006B138
			public ElementAssignment ToElementAssignment()
			{
				return new ElementAssignment(this.controllerType, this.pollingInfo.elementType, this.pollingInfo.elementIdentifierId, this.axisRange, this.pollingInfo.keyboardKey, this.modifierKeyFlags, this.fieldInfo.actionId, (this.fieldInfo.axisRange == 2) ? 1 : 0, false, (this.aem != null) ? this.aem.id : (-1));
			}
		}

		private class AxisCalibrator
		{
			// (get) Token: 0x06001A01 RID: 6657 RVA: 0x0001429C File Offset: 0x0001249C
			public bool isValid
			{
				get
				{
					return this.axis != null;
				}
			}

			// Token: 0x06001A02 RID: 6658 RVA: 0x0006CFBC File Offset: 0x0006B1BC
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

			// Token: 0x06001A03 RID: 6659 RVA: 0x0006D02C File Offset: 0x0006B22C
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

			// Token: 0x06001A04 RID: 6660 RVA: 0x000142A7 File Offset: 0x000124A7
			public void RecordZero()
			{
				if (this.axis == null)
				{
					return;
				}
				this.data.zero = this.axis.valueRaw;
			}

			// Token: 0x06001A05 RID: 6661 RVA: 0x0006D09C File Offset: 0x0006B29C
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

			public AxisCalibrationData data;

			public readonly Joystick joystick;

			public readonly int axisIndex;

			private Controller.Axis axis;

			private bool firstRun;
		}

		private class IndexedDictionary<TKey, TValue>
		{
			// (get) Token: 0x06001A06 RID: 6662 RVA: 0x000142C8 File Offset: 0x000124C8
			public int Count
			{
				get
				{
					return this.list.Count;
				}
			}

			// Token: 0x06001A07 RID: 6663 RVA: 0x000142D5 File Offset: 0x000124D5
			public IndexedDictionary()
			{
				this.list = new List<ControlMapper.IndexedDictionary<TKey, TValue>.Entry>();
			}

			public TValue this[int index]
			{
				get
				{
					return this.list[index].value;
				}
			}

			// Token: 0x06001A09 RID: 6665 RVA: 0x0006D104 File Offset: 0x0006B304
			public TValue Get(TKey key)
			{
				int num = this.IndexOfKey(key);
				if (num < 0)
				{
					throw new Exception("Key does not exist!");
				}
				return this.list[num].value;
			}

			// Token: 0x06001A0A RID: 6666 RVA: 0x0006D13C File Offset: 0x0006B33C
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

			// Token: 0x06001A0B RID: 6667 RVA: 0x000142FB File Offset: 0x000124FB
			public void Add(TKey key, TValue value)
			{
				if (this.ContainsKey(key))
				{
					throw new Exception("Key " + key.ToString() + " is already in use!");
				}
				this.list.Add(new ControlMapper.IndexedDictionary<TKey, TValue>.Entry(key, value));
			}

			// Token: 0x06001A0C RID: 6668 RVA: 0x0006D178 File Offset: 0x0006B378
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

			// Token: 0x06001A0D RID: 6669 RVA: 0x0006D1C0 File Offset: 0x0006B3C0
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

			// Token: 0x06001A0E RID: 6670 RVA: 0x0001433A File Offset: 0x0001253A
			public void Clear()
			{
				this.list.Clear();
			}

			private List<ControlMapper.IndexedDictionary<TKey, TValue>.Entry> list;

			private class Entry
			{
				// Token: 0x06001A0F RID: 6671 RVA: 0x00014347 File Offset: 0x00012547
				public Entry(TKey key, TValue value)
				{
					this.key = key;
					this.value = value;
				}

				public TKey key;

				public TValue value;
			}
		}

		private enum LayoutElementSizeType
		{
			MinSize,
			PreferredSize
		}

		private enum WindowType
		{
			None,
			ChooseJoystick,
			JoystickAssignmentConflict,
			ElementAssignment,
			ElementAssignmentPrePolling,
			ElementAssignmentPolling,
			ElementAssignmentResult,
			ElementAssignmentConflict,
			Calibration,
			CalibrateStep1,
			CalibrateStep2
		}

		private class InputGrid
		{
			// Token: 0x06001A10 RID: 6672 RVA: 0x0001435D File Offset: 0x0001255D
			public InputGrid()
			{
				this.list = new ControlMapper.InputGridEntryList();
				this.groups = new List<GameObject>();
			}

			// Token: 0x06001A11 RID: 6673 RVA: 0x0001437B File Offset: 0x0001257B
			public void AddMapCategory(int mapCategoryId)
			{
				this.list.AddMapCategory(mapCategoryId);
			}

			// Token: 0x06001A12 RID: 6674 RVA: 0x00014389 File Offset: 0x00012589
			public void AddAction(int mapCategoryId, InputAction action, AxisRange axisRange)
			{
				this.list.AddAction(mapCategoryId, action, axisRange);
			}

			// Token: 0x06001A13 RID: 6675 RVA: 0x00014399 File Offset: 0x00012599
			public void AddActionCategory(int mapCategoryId, int actionCategoryId)
			{
				this.list.AddActionCategory(mapCategoryId, actionCategoryId);
			}

			// Token: 0x06001A14 RID: 6676 RVA: 0x000143A8 File Offset: 0x000125A8
			public void AddInputFieldSet(int mapCategoryId, InputAction action, AxisRange axisRange, ControllerType controllerType, GameObject fieldSetContainer)
			{
				this.list.AddInputFieldSet(mapCategoryId, action, axisRange, controllerType, fieldSetContainer);
			}

			// Token: 0x06001A15 RID: 6677 RVA: 0x000143BC File Offset: 0x000125BC
			public void AddInputField(int mapCategoryId, InputAction action, AxisRange axisRange, ControllerType controllerType, int fieldIndex, ControlMapper.GUIInputField inputField)
			{
				this.list.AddInputField(mapCategoryId, action, axisRange, controllerType, fieldIndex, inputField);
			}

			// Token: 0x06001A16 RID: 6678 RVA: 0x000143D2 File Offset: 0x000125D2
			public void AddGroup(GameObject group)
			{
				this.groups.Add(group);
			}

			// Token: 0x06001A17 RID: 6679 RVA: 0x000143E0 File Offset: 0x000125E0
			public void AddActionLabel(int mapCategoryId, int actionId, AxisRange axisRange, ControlMapper.GUILabel label)
			{
				this.list.AddActionLabel(mapCategoryId, actionId, axisRange, label);
			}

			// Token: 0x06001A18 RID: 6680 RVA: 0x000143F2 File Offset: 0x000125F2
			public void AddActionCategoryLabel(int mapCategoryId, int actionCategoryId, ControlMapper.GUILabel label)
			{
				this.list.AddActionCategoryLabel(mapCategoryId, actionCategoryId, label);
			}

			// Token: 0x06001A19 RID: 6681 RVA: 0x00014402 File Offset: 0x00012602
			public bool Contains(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int fieldIndex)
			{
				return this.list.Contains(mapCategoryId, actionId, axisRange, controllerType, fieldIndex);
			}

			// Token: 0x06001A1A RID: 6682 RVA: 0x00014416 File Offset: 0x00012616
			public ControlMapper.GUIInputField GetGUIInputField(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int fieldIndex)
			{
				return this.list.GetGUIInputField(mapCategoryId, actionId, axisRange, controllerType, fieldIndex);
			}

			// Token: 0x06001A1B RID: 6683 RVA: 0x0001442A File Offset: 0x0001262A
			public IEnumerable<ControlMapper.InputActionSet> GetActionSets(int mapCategoryId)
			{
				return this.list.GetActionSets(mapCategoryId);
			}

			// Token: 0x06001A1C RID: 6684 RVA: 0x00014438 File Offset: 0x00012638
			public void SetColumnHeight(int mapCategoryId, float height)
			{
				this.list.SetColumnHeight(mapCategoryId, height);
			}

			// Token: 0x06001A1D RID: 6685 RVA: 0x00014447 File Offset: 0x00012647
			public float GetColumnHeight(int mapCategoryId)
			{
				return this.list.GetColumnHeight(mapCategoryId);
			}

			// Token: 0x06001A1E RID: 6686 RVA: 0x00014455 File Offset: 0x00012655
			public void SetFieldsActive(int mapCategoryId, bool state)
			{
				this.list.SetFieldsActive(mapCategoryId, state);
			}

			// Token: 0x06001A1F RID: 6687 RVA: 0x00014464 File Offset: 0x00012664
			public void SetFieldLabel(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int index, string label)
			{
				this.list.SetLabel(mapCategoryId, actionId, axisRange, controllerType, index, label);
			}

			// Token: 0x06001A20 RID: 6688 RVA: 0x0006D208 File Offset: 0x0006B408
			public void PopulateField(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int controllerId, int index, int actionElementMapId, GameObject buttonDisplay, bool invert)
			{
				this.list.PopulateField(mapCategoryId, actionId, axisRange, controllerType, controllerId, index, actionElementMapId, buttonDisplay, invert);
			}

			// Token: 0x06001A21 RID: 6689 RVA: 0x0001447A File Offset: 0x0001267A
			public void SetFixedFieldData(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int controllerId)
			{
				this.list.SetFixedFieldData(mapCategoryId, actionId, axisRange, controllerType, controllerId);
			}

			// Token: 0x06001A22 RID: 6690 RVA: 0x0001448E File Offset: 0x0001268E
			public void InitializeFields(int mapCategoryId)
			{
				this.list.InitializeFields(mapCategoryId);
			}

			// Token: 0x06001A23 RID: 6691 RVA: 0x0001449C File Offset: 0x0001269C
			public void Show(int mapCategoryId)
			{
				this.list.Show(mapCategoryId);
			}

			// Token: 0x06001A24 RID: 6692 RVA: 0x000144AA File Offset: 0x000126AA
			public void HideAll()
			{
				this.list.HideAll();
			}

			// Token: 0x06001A25 RID: 6693 RVA: 0x000144B7 File Offset: 0x000126B7
			public void ClearLabels(int mapCategoryId)
			{
				this.list.ClearLabels(mapCategoryId);
			}

			// Token: 0x06001A26 RID: 6694 RVA: 0x0006D230 File Offset: 0x0006B430
			private void ClearGroups()
			{
				for (int i = 0; i < this.groups.Count; i++)
				{
					if (!(this.groups[i] == null))
					{
						global::UnityEngine.Object.Destroy(this.groups[i]);
					}
				}
			}

			// Token: 0x06001A27 RID: 6695 RVA: 0x000144C5 File Offset: 0x000126C5
			public void ClearAll()
			{
				this.ClearGroups();
				this.list.Clear();
			}

			private ControlMapper.InputGridEntryList list;

			private List<GameObject> groups;
		}

		private class InputGridEntryList
		{
			// Token: 0x06001A28 RID: 6696 RVA: 0x000144D8 File Offset: 0x000126D8
			public InputGridEntryList()
			{
				this.entries = new ControlMapper.IndexedDictionary<int, ControlMapper.InputGridEntryList.MapCategoryEntry>();
			}

			// Token: 0x06001A29 RID: 6697 RVA: 0x000144EB File Offset: 0x000126EB
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

			// Token: 0x06001A2A RID: 6698 RVA: 0x00014512 File Offset: 0x00012712
			public void AddAction(int mapCategoryId, InputAction action, AxisRange axisRange)
			{
				this.AddActionEntry(mapCategoryId, action, axisRange);
			}

			// Token: 0x06001A2B RID: 6699 RVA: 0x0006D278 File Offset: 0x0006B478
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

			// Token: 0x06001A2C RID: 6700 RVA: 0x0006D2A4 File Offset: 0x0006B4A4
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

			// Token: 0x06001A2D RID: 6701 RVA: 0x0001451E File Offset: 0x0001271E
			public void AddActionCategory(int mapCategoryId, int actionCategoryId)
			{
				this.AddActionCategoryEntry(mapCategoryId, actionCategoryId);
			}

			// Token: 0x06001A2E RID: 6702 RVA: 0x0006D2D8 File Offset: 0x0006B4D8
			private ControlMapper.InputGridEntryList.ActionCategoryEntry AddActionCategoryEntry(int mapCategoryId, int actionCategoryId)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return null;
				}
				return mapCategoryEntry.AddActionCategory(actionCategoryId);
			}

			// Token: 0x06001A2F RID: 6703 RVA: 0x0006D300 File Offset: 0x0006B500
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

			// Token: 0x06001A30 RID: 6704 RVA: 0x0006D334 File Offset: 0x0006B534
			public void AddInputFieldSet(int mapCategoryId, InputAction action, AxisRange axisRange, ControllerType controllerType, GameObject fieldSetContainer)
			{
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = this.GetActionEntry(mapCategoryId, action, axisRange);
				if (actionEntry == null)
				{
					return;
				}
				actionEntry.AddInputFieldSet(controllerType, fieldSetContainer);
			}

			// Token: 0x06001A31 RID: 6705 RVA: 0x0006D35C File Offset: 0x0006B55C
			public void AddInputField(int mapCategoryId, InputAction action, AxisRange axisRange, ControllerType controllerType, int fieldIndex, ControlMapper.GUIInputField inputField)
			{
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = this.GetActionEntry(mapCategoryId, action, axisRange);
				if (actionEntry == null)
				{
					return;
				}
				actionEntry.AddInputField(controllerType, fieldIndex, inputField);
			}

			// Token: 0x06001A32 RID: 6706 RVA: 0x00014529 File Offset: 0x00012729
			public bool Contains(int mapCategoryId, int actionId, AxisRange axisRange)
			{
				return this.GetActionEntry(mapCategoryId, actionId, axisRange) != null;
			}

			// Token: 0x06001A33 RID: 6707 RVA: 0x0006D384 File Offset: 0x0006B584
			public bool Contains(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int fieldIndex)
			{
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = this.GetActionEntry(mapCategoryId, actionId, axisRange);
				return actionEntry != null && actionEntry.Contains(controllerType, fieldIndex);
			}

			// Token: 0x06001A34 RID: 6708 RVA: 0x0006D3AC File Offset: 0x0006B5AC
			public void SetColumnHeight(int mapCategoryId, float height)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return;
				}
				mapCategoryEntry.columnHeight = height;
			}

			// Token: 0x06001A35 RID: 6709 RVA: 0x0006D3D4 File Offset: 0x0006B5D4
			public float GetColumnHeight(int mapCategoryId)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return 0f;
				}
				return mapCategoryEntry.columnHeight;
			}

			// Token: 0x06001A36 RID: 6710 RVA: 0x0006D400 File Offset: 0x0006B600
			public ControlMapper.GUIInputField GetGUIInputField(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int fieldIndex)
			{
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = this.GetActionEntry(mapCategoryId, actionId, axisRange);
				if (actionEntry == null)
				{
					return null;
				}
				return actionEntry.GetGUIInputField(controllerType, fieldIndex);
			}

			// Token: 0x06001A37 RID: 6711 RVA: 0x0006D428 File Offset: 0x0006B628
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

			// Token: 0x06001A38 RID: 6712 RVA: 0x00014537 File Offset: 0x00012737
			private ControlMapper.InputGridEntryList.ActionEntry GetActionEntry(int mapCategoryId, InputAction action, AxisRange axisRange)
			{
				if (action == null)
				{
					return null;
				}
				return this.GetActionEntry(mapCategoryId, action.id, axisRange);
			}

			// Token: 0x06001A39 RID: 6713 RVA: 0x0001454C File Offset: 0x0001274C
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

			// Token: 0x06001A3A RID: 6714 RVA: 0x0006D458 File Offset: 0x0006B658
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

			// Token: 0x06001A3B RID: 6715 RVA: 0x0006D4A4 File Offset: 0x0006B6A4
			public void SetLabel(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int index, string label)
			{
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = this.GetActionEntry(mapCategoryId, actionId, axisRange);
				if (actionEntry == null)
				{
					return;
				}
				actionEntry.SetFieldLabel(controllerType, index, label);
			}

			// Token: 0x06001A3C RID: 6716 RVA: 0x0006D4CC File Offset: 0x0006B6CC
			public void PopulateField(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int controllerId, int index, int actionElementMapId, GameObject buttonDisplay, bool invert)
			{
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = this.GetActionEntry(mapCategoryId, actionId, axisRange);
				if (actionEntry == null)
				{
					return;
				}
				actionEntry.PopulateField(controllerType, controllerId, index, actionElementMapId, buttonDisplay, invert);
			}

			// Token: 0x06001A3D RID: 6717 RVA: 0x0006D4FC File Offset: 0x0006B6FC
			public void SetFixedFieldData(int mapCategoryId, int actionId, AxisRange axisRange, ControllerType controllerType, int controllerId)
			{
				ControlMapper.InputGridEntryList.ActionEntry actionEntry = this.GetActionEntry(mapCategoryId, actionId, axisRange);
				if (actionEntry == null)
				{
					return;
				}
				actionEntry.SetFixedFieldData(controllerType, controllerId);
			}

			// Token: 0x06001A3E RID: 6718 RVA: 0x0006D524 File Offset: 0x0006B724
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

			// Token: 0x06001A3F RID: 6719 RVA: 0x0006D570 File Offset: 0x0006B770
			public void Show(int mapCategoryId)
			{
				ControlMapper.InputGridEntryList.MapCategoryEntry mapCategoryEntry;
				if (!this.entries.TryGet(mapCategoryId, out mapCategoryEntry))
				{
					return;
				}
				mapCategoryEntry.SetAllActive(true);
			}

			// Token: 0x06001A40 RID: 6720 RVA: 0x0006D598 File Offset: 0x0006B798
			public void HideAll()
			{
				for (int i = 0; i < this.entries.Count; i++)
				{
					this.entries[i].SetAllActive(false);
				}
			}

			// Token: 0x06001A41 RID: 6721 RVA: 0x0006D5D0 File Offset: 0x0006B7D0
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

			// Token: 0x06001A42 RID: 6722 RVA: 0x00014563 File Offset: 0x00012763
			public void Clear()
			{
				this.entries.Clear();
			}

			private ControlMapper.IndexedDictionary<int, ControlMapper.InputGridEntryList.MapCategoryEntry> entries;

			private class MapCategoryEntry
			{
				// (get) Token: 0x06001A43 RID: 6723 RVA: 0x00014570 File Offset: 0x00012770
				public List<ControlMapper.InputGridEntryList.ActionEntry> actionList
				{
					get
					{
						return this._actionList;
					}
				}

				// (get) Token: 0x06001A44 RID: 6724 RVA: 0x00014578 File Offset: 0x00012778
				public ControlMapper.IndexedDictionary<int, ControlMapper.InputGridEntryList.ActionCategoryEntry> actionCategoryList
				{
					get
					{
						return this._actionCategoryList;
					}
				}

				// (get) Token: 0x06001A45 RID: 6725 RVA: 0x00014580 File Offset: 0x00012780
				// (set) Token: 0x06001A46 RID: 6726 RVA: 0x00014588 File Offset: 0x00012788
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

				// Token: 0x06001A47 RID: 6727 RVA: 0x00014591 File Offset: 0x00012791
				public MapCategoryEntry()
				{
					this._actionList = new List<ControlMapper.InputGridEntryList.ActionEntry>();
					this._actionCategoryList = new ControlMapper.IndexedDictionary<int, ControlMapper.InputGridEntryList.ActionCategoryEntry>();
				}

				// Token: 0x06001A48 RID: 6728 RVA: 0x0006D61C File Offset: 0x0006B81C
				public ControlMapper.InputGridEntryList.ActionEntry GetActionEntry(int actionId, AxisRange axisRange)
				{
					int num = this.IndexOfActionEntry(actionId, axisRange);
					if (num < 0)
					{
						return null;
					}
					return this._actionList[num];
				}

				// Token: 0x06001A49 RID: 6729 RVA: 0x0006D644 File Offset: 0x0006B844
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

				// Token: 0x06001A4A RID: 6730 RVA: 0x000145AF File Offset: 0x000127AF
				public bool ContainsActionEntry(int actionId, AxisRange axisRange)
				{
					return this.IndexOfActionEntry(actionId, axisRange) >= 0;
				}

				// Token: 0x06001A4B RID: 6731 RVA: 0x0006D684 File Offset: 0x0006B884
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

				// Token: 0x06001A4C RID: 6732 RVA: 0x000145BF File Offset: 0x000127BF
				public ControlMapper.InputGridEntryList.ActionCategoryEntry GetActionCategoryEntry(int actionCategoryId)
				{
					if (!this._actionCategoryList.ContainsKey(actionCategoryId))
					{
						return null;
					}
					return this._actionCategoryList.Get(actionCategoryId);
				}

				// Token: 0x06001A4D RID: 6733 RVA: 0x000145DD File Offset: 0x000127DD
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

				// Token: 0x06001A4E RID: 6734 RVA: 0x0006D6D4 File Offset: 0x0006B8D4
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

				private List<ControlMapper.InputGridEntryList.ActionEntry> _actionList;

				private ControlMapper.IndexedDictionary<int, ControlMapper.InputGridEntryList.ActionCategoryEntry> _actionCategoryList;

				private float _columnHeight;
			}

			private class ActionEntry
			{
				// Token: 0x06001A4F RID: 6735 RVA: 0x00014613 File Offset: 0x00012813
				public ActionEntry(InputAction action, AxisRange axisRange)
				{
					this.action = action;
					this.axisRange = axisRange;
					this.actionSet = new ControlMapper.InputActionSet(action.id, axisRange);
					this.fieldSets = new ControlMapper.IndexedDictionary<int, ControlMapper.InputGridEntryList.FieldSet>();
				}

				// Token: 0x06001A50 RID: 6736 RVA: 0x00014646 File Offset: 0x00012846
				public void SetLabel(ControlMapper.GUILabel label)
				{
					this.label = label;
				}

				// Token: 0x06001A51 RID: 6737 RVA: 0x0001464F File Offset: 0x0001284F
				public bool Matches(int actionId, AxisRange axisRange)
				{
					return this.action.id == actionId && this.axisRange == axisRange;
				}

				// Token: 0x06001A52 RID: 6738 RVA: 0x0001466D File Offset: 0x0001286D
				public void AddInputFieldSet(ControllerType controllerType, GameObject fieldSetContainer)
				{
					if (this.fieldSets.ContainsKey(controllerType))
					{
						return;
					}
					this.fieldSets.Add(controllerType, new ControlMapper.InputGridEntryList.FieldSet(fieldSetContainer));
				}

				// Token: 0x06001A53 RID: 6739 RVA: 0x0006D734 File Offset: 0x0006B934
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

				// Token: 0x06001A54 RID: 6740 RVA: 0x0006D77C File Offset: 0x0006B97C
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

				// Token: 0x06001A55 RID: 6741 RVA: 0x00014690 File Offset: 0x00012890
				public bool Contains(ControllerType controllerType, int fieldId)
				{
					return this.fieldSets.ContainsKey(controllerType) && this.fieldSets.Get(controllerType).fields.ContainsKey(fieldId);
				}

				// Token: 0x06001A56 RID: 6742 RVA: 0x0006D7CC File Offset: 0x0006B9CC
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

				// Token: 0x06001A57 RID: 6743 RVA: 0x0006D820 File Offset: 0x0006BA20
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

				// Token: 0x06001A58 RID: 6744 RVA: 0x0006D8B4 File Offset: 0x0006BAB4
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

				// Token: 0x06001A59 RID: 6745 RVA: 0x0006D908 File Offset: 0x0006BB08
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

				// Token: 0x06001A5A RID: 6746 RVA: 0x0006D9B0 File Offset: 0x0006BBB0
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

				// Token: 0x06001A5B RID: 6747 RVA: 0x0006DA00 File Offset: 0x0006BC00
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

				// Token: 0x06001A5C RID: 6748 RVA: 0x0006DA5C File Offset: 0x0006BC5C
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

				private ControlMapper.IndexedDictionary<int, ControlMapper.InputGridEntryList.FieldSet> fieldSets;

				public ControlMapper.GUILabel label;

				public readonly InputAction action;

				public readonly AxisRange axisRange;

				public readonly ControlMapper.InputActionSet actionSet;
			}

			private class FieldSet
			{
				// Token: 0x06001A5D RID: 6749 RVA: 0x000146BE File Offset: 0x000128BE
				public FieldSet(GameObject groupContainer)
				{
					this.groupContainer = groupContainer;
					this.fields = new ControlMapper.IndexedDictionary<int, ControlMapper.GUIInputField>();
				}

				public readonly GameObject groupContainer;

				public readonly ControlMapper.IndexedDictionary<int, ControlMapper.GUIInputField> fields;
			}

			private class ActionCategoryEntry
			{
				// Token: 0x06001A5E RID: 6750 RVA: 0x000146D8 File Offset: 0x000128D8
				public ActionCategoryEntry(int actionCategoryId)
				{
					this.actionCategoryId = actionCategoryId;
				}

				// Token: 0x06001A5F RID: 6751 RVA: 0x000146E7 File Offset: 0x000128E7
				public void SetLabel(ControlMapper.GUILabel label)
				{
					this.label = label;
				}

				// Token: 0x06001A60 RID: 6752 RVA: 0x000146F0 File Offset: 0x000128F0
				public void SetActive(bool state)
				{
					if (this.label != null)
					{
						this.label.SetActive(state);
					}
				}

				public readonly int actionCategoryId;

				public ControlMapper.GUILabel label;
			}
		}

		private class WindowManager
		{
			// (get) Token: 0x06001A69 RID: 6761 RVA: 0x0006DBF4 File Offset: 0x0006BDF4
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

			// (get) Token: 0x06001A6A RID: 6762 RVA: 0x0006DC30 File Offset: 0x0006BE30
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

			// Token: 0x06001A6B RID: 6763 RVA: 0x0006DC78 File Offset: 0x0006BE78
			public WindowManager(GameObject windowPrefab, GameObject faderPrefab, Transform parent)
			{
				this.windowPrefab = windowPrefab;
				this.parent = parent;
				this.windows = new List<Window>();
				this.fader = global::UnityEngine.Object.Instantiate<GameObject>(faderPrefab);
				this.fader.transform.SetParent(parent, false);
				this.fader.GetComponent<RectTransform>().localScale = Vector2.one;
				this.SetFaderActive(false);
			}

			// Token: 0x06001A6C RID: 6764 RVA: 0x00014730 File Offset: 0x00012930
			public Window OpenWindow(string name, int width, int height)
			{
				Window window = this.InstantiateWindow(name, width, height);
				this.UpdateFader();
				return window;
			}

			// Token: 0x06001A6D RID: 6765 RVA: 0x00014741 File Offset: 0x00012941
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

			// Token: 0x06001A6E RID: 6766 RVA: 0x0006DCE4 File Offset: 0x0006BEE4
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

			// Token: 0x06001A6F RID: 6767 RVA: 0x00014766 File Offset: 0x00012966
			public void CloseWindow(int windowId)
			{
				this.CloseWindow(this.GetWindow(windowId));
			}

			// Token: 0x06001A70 RID: 6768 RVA: 0x0006DD54 File Offset: 0x0006BF54
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

			// Token: 0x06001A71 RID: 6769 RVA: 0x0006DDE8 File Offset: 0x0006BFE8
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

			// Token: 0x06001A72 RID: 6770 RVA: 0x0006DE5C File Offset: 0x0006C05C
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

			// Token: 0x06001A73 RID: 6771 RVA: 0x0006DEB8 File Offset: 0x0006C0B8
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

			// Token: 0x06001A74 RID: 6772 RVA: 0x00014775 File Offset: 0x00012975
			public bool IsFocused(int windowId)
			{
				return windowId >= 0 && !(this.topWindow == null) && this.topWindow.id == windowId;
			}

			// Token: 0x06001A75 RID: 6773 RVA: 0x0001479B File Offset: 0x0001299B
			public void Focus(int windowId)
			{
				this.Focus(this.GetWindow(windowId));
			}

			// Token: 0x06001A76 RID: 6774 RVA: 0x000147AA File Offset: 0x000129AA
			public void Focus(Window window)
			{
				if (window == null)
				{
					return;
				}
				window.TakeInputFocus();
				this.DefocusOtherWindows(window.id);
			}

			// Token: 0x06001A77 RID: 6775 RVA: 0x0006DF1C File Offset: 0x0006C11C
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

			// Token: 0x06001A78 RID: 6776 RVA: 0x0006DF80 File Offset: 0x0006C180
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

			// Token: 0x06001A79 RID: 6777 RVA: 0x000147C8 File Offset: 0x000129C8
			private void FocusTopWindow()
			{
				if (this.topWindow == null)
				{
					return;
				}
				this.topWindow.TakeInputFocus();
			}

			// Token: 0x06001A7A RID: 6778 RVA: 0x000147E4 File Offset: 0x000129E4
			private void SetFaderActive(bool state)
			{
				this.fader.SetActive(state);
			}

			// Token: 0x06001A7B RID: 6779 RVA: 0x0006DFF0 File Offset: 0x0006C1F0
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

			// Token: 0x06001A7C RID: 6780 RVA: 0x0006E068 File Offset: 0x0006C268
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

			// Token: 0x06001A7D RID: 6781 RVA: 0x000147F2 File Offset: 0x000129F2
			private void DestroyWindow(Window window)
			{
				if (window == null)
				{
					return;
				}
				global::UnityEngine.Object.Destroy(window.gameObject);
			}

			// Token: 0x06001A7E RID: 6782 RVA: 0x00014809 File Offset: 0x00012A09
			private int GetNewId()
			{
				int num = this.idCounter;
				this.idCounter++;
				return num;
			}

			// Token: 0x06001A7F RID: 6783 RVA: 0x0001481F File Offset: 0x00012A1F
			public void ClearCompletely()
			{
				this.CloseAll();
				if (this.fader != null)
				{
					global::UnityEngine.Object.Destroy(this.fader);
				}
			}

			private List<Window> windows;

			private GameObject windowPrefab;

			private Transform parent;

			private GameObject fader;

			private int idCounter;
		}
	}
}
