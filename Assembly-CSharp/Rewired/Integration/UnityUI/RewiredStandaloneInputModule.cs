using System;
using System.Collections.Generic;
using Rewired.Components;
using Rewired.UI;
using Rewired.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Rewired.Integration.UnityUI
{
	// Token: 0x02000428 RID: 1064
	[AddComponentMenu("Rewired/Rewired Standalone Input Module")]
	public sealed class RewiredStandaloneInputModule : RewiredPointerInputModule
	{
		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x00011EA0 File Offset: 0x000100A0
		// (set) Token: 0x06001745 RID: 5957 RVA: 0x00011EA8 File Offset: 0x000100A8
		public InputManager_Base RewiredInputManager
		{
			get
			{
				return this.rewiredInputManager;
			}
			set
			{
				this.rewiredInputManager = value;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x00011EB1 File Offset: 0x000100B1
		// (set) Token: 0x06001747 RID: 5959 RVA: 0x00011EB9 File Offset: 0x000100B9
		public bool UseAllRewiredGamePlayers
		{
			get
			{
				return this.useAllRewiredGamePlayers;
			}
			set
			{
				bool flag = value != this.useAllRewiredGamePlayers;
				this.useAllRewiredGamePlayers = value;
				if (flag)
				{
					this.SetupRewiredVars();
				}
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x00011ED6 File Offset: 0x000100D6
		// (set) Token: 0x06001749 RID: 5961 RVA: 0x00011EDE File Offset: 0x000100DE
		public bool UseRewiredSystemPlayer
		{
			get
			{
				return this.useRewiredSystemPlayer;
			}
			set
			{
				bool flag = value != this.useRewiredSystemPlayer;
				this.useRewiredSystemPlayer = value;
				if (flag)
				{
					this.SetupRewiredVars();
				}
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600174A RID: 5962 RVA: 0x00011EFB File Offset: 0x000100FB
		// (set) Token: 0x0600174B RID: 5963 RVA: 0x00011F0D File Offset: 0x0001010D
		public int[] RewiredPlayerIds
		{
			get
			{
				return (int[])this.rewiredPlayerIds.Clone();
			}
			set
			{
				this.rewiredPlayerIds = ((value != null) ? ((int[])value.Clone()) : new int[0]);
				this.SetupRewiredVars();
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x00011F31 File Offset: 0x00010131
		// (set) Token: 0x0600174D RID: 5965 RVA: 0x00011F39 File Offset: 0x00010139
		public bool UsePlayingPlayersOnly
		{
			get
			{
				return this.usePlayingPlayersOnly;
			}
			set
			{
				this.usePlayingPlayersOnly = value;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x00011F42 File Offset: 0x00010142
		// (set) Token: 0x0600174F RID: 5967 RVA: 0x00011F4F File Offset: 0x0001014F
		public List<PlayerMouse> PlayerMice
		{
			get
			{
				return new List<PlayerMouse>(this.playerMice);
			}
			set
			{
				if (value == null)
				{
					this.playerMice = new List<PlayerMouse>();
					this.SetupRewiredVars();
					return;
				}
				this.playerMice = new List<PlayerMouse>(value);
				this.SetupRewiredVars();
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x00011F78 File Offset: 0x00010178
		// (set) Token: 0x06001751 RID: 5969 RVA: 0x00011F80 File Offset: 0x00010180
		public bool MoveOneElementPerAxisPress
		{
			get
			{
				return this.moveOneElementPerAxisPress;
			}
			set
			{
				this.moveOneElementPerAxisPress = value;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001752 RID: 5970 RVA: 0x00011F89 File Offset: 0x00010189
		// (set) Token: 0x06001753 RID: 5971 RVA: 0x00011F91 File Offset: 0x00010191
		public bool allowMouseInput
		{
			get
			{
				return this.m_allowMouseInput;
			}
			set
			{
				this.m_allowMouseInput = value;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x00011F9A File Offset: 0x0001019A
		// (set) Token: 0x06001755 RID: 5973 RVA: 0x00011FA2 File Offset: 0x000101A2
		public bool allowMouseInputIfTouchSupported
		{
			get
			{
				return this.m_allowMouseInputIfTouchSupported;
			}
			set
			{
				this.m_allowMouseInputIfTouchSupported = value;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x00011FAB File Offset: 0x000101AB
		// (set) Token: 0x06001757 RID: 5975 RVA: 0x00011FB3 File Offset: 0x000101B3
		public bool allowTouchInput
		{
			get
			{
				return this.m_allowTouchInput;
			}
			set
			{
				this.m_allowTouchInput = value;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x00011FBC File Offset: 0x000101BC
		// (set) Token: 0x06001759 RID: 5977 RVA: 0x00011FC4 File Offset: 0x000101C4
		public bool deselectIfBackgroundClicked
		{
			get
			{
				return this.m_deselectIfBackgroundClicked;
			}
			set
			{
				this.m_deselectIfBackgroundClicked = value;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x00011FCD File Offset: 0x000101CD
		// (set) Token: 0x0600175B RID: 5979 RVA: 0x00011FD5 File Offset: 0x000101D5
		private bool deselectBeforeSelecting
		{
			get
			{
				return this.m_deselectBeforeSelecting;
			}
			set
			{
				this.m_deselectBeforeSelecting = value;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x00011FDE File Offset: 0x000101DE
		// (set) Token: 0x0600175D RID: 5981 RVA: 0x00011FE6 File Offset: 0x000101E6
		public bool SetActionsById
		{
			get
			{
				return this.setActionsById;
			}
			set
			{
				if (this.setActionsById == value)
				{
					return;
				}
				this.setActionsById = value;
				this.SetupRewiredVars();
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x00011FFF File Offset: 0x000101FF
		// (set) Token: 0x0600175F RID: 5983 RVA: 0x00062CEC File Offset: 0x00060EEC
		public int HorizontalActionId
		{
			get
			{
				return this.horizontalActionId;
			}
			set
			{
				if (value == this.horizontalActionId)
				{
					return;
				}
				this.horizontalActionId = value;
				if (ReInput.isReady)
				{
					this.m_HorizontalAxis = ((ReInput.mapping.GetAction(value) != null) ? ReInput.mapping.GetAction(value).name : string.Empty);
				}
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x00012007 File Offset: 0x00010207
		// (set) Token: 0x06001761 RID: 5985 RVA: 0x00062D3C File Offset: 0x00060F3C
		public int VerticalActionId
		{
			get
			{
				return this.verticalActionId;
			}
			set
			{
				if (value == this.verticalActionId)
				{
					return;
				}
				this.verticalActionId = value;
				if (ReInput.isReady)
				{
					this.m_VerticalAxis = ((ReInput.mapping.GetAction(value) != null) ? ReInput.mapping.GetAction(value).name : string.Empty);
				}
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x0001200F File Offset: 0x0001020F
		// (set) Token: 0x06001763 RID: 5987 RVA: 0x00062D8C File Offset: 0x00060F8C
		public int SubmitActionId
		{
			get
			{
				return this.submitActionId;
			}
			set
			{
				if (value == this.submitActionId)
				{
					return;
				}
				this.submitActionId = value;
				if (ReInput.isReady)
				{
					this.m_SubmitButton = ((ReInput.mapping.GetAction(value) != null) ? ReInput.mapping.GetAction(value).name : string.Empty);
				}
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x00012017 File Offset: 0x00010217
		// (set) Token: 0x06001765 RID: 5989 RVA: 0x00062DDC File Offset: 0x00060FDC
		public int CancelActionId
		{
			get
			{
				return this.cancelActionId;
			}
			set
			{
				if (value == this.cancelActionId)
				{
					return;
				}
				this.cancelActionId = value;
				if (ReInput.isReady)
				{
					this.m_CancelButton = ((ReInput.mapping.GetAction(value) != null) ? ReInput.mapping.GetAction(value).name : string.Empty);
				}
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001766 RID: 5990 RVA: 0x0001201F File Offset: 0x0001021F
		protected override bool isMouseSupported
		{
			get
			{
				return base.isMouseSupported && this.m_allowMouseInput && (!this.isTouchSupported || this.m_allowMouseInputIfTouchSupported);
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x00011FAB File Offset: 0x000101AB
		private bool isTouchAllowed
		{
			get
			{
				return this.m_allowTouchInput;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x00012045 File Offset: 0x00010245
		// (set) Token: 0x06001769 RID: 5993 RVA: 0x0001204D File Offset: 0x0001024D
		[Obsolete("allowActivationOnMobileDevice has been deprecated. Use forceModuleActive instead")]
		public bool allowActivationOnMobileDevice
		{
			get
			{
				return this.m_ForceModuleActive;
			}
			set
			{
				this.m_ForceModuleActive = value;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x00012045 File Offset: 0x00010245
		// (set) Token: 0x0600176B RID: 5995 RVA: 0x0001204D File Offset: 0x0001024D
		public bool forceModuleActive
		{
			get
			{
				return this.m_ForceModuleActive;
			}
			set
			{
				this.m_ForceModuleActive = value;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x00012056 File Offset: 0x00010256
		// (set) Token: 0x0600176D RID: 5997 RVA: 0x0001205E File Offset: 0x0001025E
		public float inputActionsPerSecond
		{
			get
			{
				return this.m_InputActionsPerSecond;
			}
			set
			{
				this.m_InputActionsPerSecond = value;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600176E RID: 5998 RVA: 0x00012067 File Offset: 0x00010267
		// (set) Token: 0x0600176F RID: 5999 RVA: 0x0001206F File Offset: 0x0001026F
		public float repeatDelay
		{
			get
			{
				return this.m_RepeatDelay;
			}
			set
			{
				this.m_RepeatDelay = value;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x00012078 File Offset: 0x00010278
		// (set) Token: 0x06001771 RID: 6001 RVA: 0x00012080 File Offset: 0x00010280
		public string horizontalAxis
		{
			get
			{
				return this.m_HorizontalAxis;
			}
			set
			{
				if (this.m_HorizontalAxis == value)
				{
					return;
				}
				this.m_HorizontalAxis = value;
				if (ReInput.isReady)
				{
					this.horizontalActionId = ReInput.mapping.GetActionId(value);
				}
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x000120B0 File Offset: 0x000102B0
		// (set) Token: 0x06001773 RID: 6003 RVA: 0x000120B8 File Offset: 0x000102B8
		public string verticalAxis
		{
			get
			{
				return this.m_VerticalAxis;
			}
			set
			{
				if (this.m_VerticalAxis == value)
				{
					return;
				}
				this.m_VerticalAxis = value;
				if (ReInput.isReady)
				{
					this.verticalActionId = ReInput.mapping.GetActionId(value);
				}
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x000120E8 File Offset: 0x000102E8
		// (set) Token: 0x06001775 RID: 6005 RVA: 0x000120F0 File Offset: 0x000102F0
		public string submitButton
		{
			get
			{
				return this.m_SubmitButton;
			}
			set
			{
				if (this.m_SubmitButton == value)
				{
					return;
				}
				this.m_SubmitButton = value;
				if (ReInput.isReady)
				{
					this.submitActionId = ReInput.mapping.GetActionId(value);
				}
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x00012120 File Offset: 0x00010320
		// (set) Token: 0x06001777 RID: 6007 RVA: 0x00012128 File Offset: 0x00010328
		public string cancelButton
		{
			get
			{
				return this.m_CancelButton;
			}
			set
			{
				if (this.m_CancelButton == value)
				{
					return;
				}
				this.m_CancelButton = value;
				if (ReInput.isReady)
				{
					this.cancelActionId = ReInput.mapping.GetActionId(value);
				}
			}
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x00062E2C File Offset: 0x0006102C
		private RewiredStandaloneInputModule()
		{
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00062ED4 File Offset: 0x000610D4
		protected override void Awake()
		{
			base.Awake();
			this.isTouchSupported = base.defaultTouchInputSource.touchSupported;
			TouchInputModule component = base.GetComponent<TouchInputModule>();
			if (component != null)
			{
				component.enabled = false;
			}
			ReInput.InitializedEvent += this.OnRewiredInitialized;
			this.InitializeRewired();
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x00012158 File Offset: 0x00010358
		public override void UpdateModule()
		{
			this.CheckEditorRecompile();
			if (this.recompiling)
			{
				return;
			}
			if (!ReInput.isReady)
			{
				return;
			}
			if (!this.m_HasFocus)
			{
				this.ShouldIgnoreEventsOnNoFocus();
				return;
			}
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x000050C3 File Offset: 0x000032C3
		public override bool IsModuleSupported()
		{
			return true;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00062F28 File Offset: 0x00061128
		public override bool ShouldActivateModule()
		{
			if (!base.ShouldActivateModule())
			{
				return false;
			}
			if (this.recompiling)
			{
				return false;
			}
			if (!ReInput.isReady)
			{
				return false;
			}
			bool flag = this.m_ForceModuleActive;
			for (int i = 0; i < this.playerIds.Length; i++)
			{
				Player player = ReInput.players.GetPlayer(this.playerIds[i]);
				if (player != null && (!this.usePlayingPlayersOnly || player.isPlaying))
				{
					flag |= this.GetButtonDown(player, this.submitActionId);
					flag |= this.GetButtonDown(player, this.cancelActionId);
					if (this.moveOneElementPerAxisPress)
					{
						flag |= this.GetButtonDown(player, this.horizontalActionId) || this.GetNegativeButtonDown(player, this.horizontalActionId);
						flag |= this.GetButtonDown(player, this.verticalActionId) || this.GetNegativeButtonDown(player, this.verticalActionId);
					}
					else
					{
						flag |= !Mathf.Approximately(this.GetAxis(player, this.horizontalActionId), 0f);
						flag |= !Mathf.Approximately(this.GetAxis(player, this.verticalActionId), 0f);
					}
				}
			}
			if (this.isMouseSupported)
			{
				flag |= this.DidAnyMouseMove();
				flag |= this.GetMouseButtonDownOnAnyMouse(0);
			}
			if (this.isTouchAllowed)
			{
				for (int j = 0; j < base.defaultTouchInputSource.touchCount; j++)
				{
					Touch touch = base.defaultTouchInputSource.GetTouch(j);
					flag |= touch.phase == null || touch.phase == 1 || touch.phase == 2;
				}
			}
			return flag;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x000630B4 File Offset: 0x000612B4
		public override void ActivateModule()
		{
			if (!this.m_HasFocus && this.ShouldIgnoreEventsOnNoFocus())
			{
				return;
			}
			base.ActivateModule();
			GameObject gameObject = base.eventSystem.currentSelectedGameObject;
			if (gameObject == null)
			{
				gameObject = base.eventSystem.firstSelectedGameObject;
			}
			base.eventSystem.SetSelectedGameObject(gameObject, this.GetBaseEventData());
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x00012181 File Offset: 0x00010381
		public override void DeactivateModule()
		{
			base.DeactivateModule();
			base.ClearSelection();
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x0006310C File Offset: 0x0006130C
		public override void Process()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			if (!this.m_HasFocus && this.ShouldIgnoreEventsOnNoFocus())
			{
				return;
			}
			if (!base.enabled || !base.gameObject.activeInHierarchy)
			{
				return;
			}
			bool flag = this.SendUpdateEventToSelectedObject();
			if (base.eventSystem.sendNavigationEvents)
			{
				if (!flag)
				{
					flag |= this.SendMoveEventToSelectedObject();
				}
				if (!flag)
				{
					this.SendSubmitEventToSelectedObject();
				}
			}
			if (!this.ProcessTouchEvents() && this.isMouseSupported)
			{
				this.ProcessMouseEvents();
			}
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x00063188 File Offset: 0x00061388
		private bool ProcessTouchEvents()
		{
			if (!this.isTouchAllowed)
			{
				return false;
			}
			for (int i = 0; i < base.defaultTouchInputSource.touchCount; i++)
			{
				Touch touch = base.defaultTouchInputSource.GetTouch(i);
				if (touch.type != 1)
				{
					bool flag;
					bool flag2;
					PlayerPointerEventData touchPointerEventData = base.GetTouchPointerEventData(0, 0, touch, out flag, out flag2);
					this.ProcessTouchPress(touchPointerEventData, flag, flag2);
					if (!flag2)
					{
						this.ProcessMove(touchPointerEventData);
						this.ProcessDrag(touchPointerEventData);
					}
					else
					{
						base.RemovePointerData(touchPointerEventData);
					}
				}
			}
			return base.defaultTouchInputSource.touchCount > 0;
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x00063210 File Offset: 0x00061410
		private void ProcessTouchPress(PointerEventData pointerEvent, bool pressed, bool released)
		{
			GameObject gameObject = pointerEvent.pointerCurrentRaycast.gameObject;
			if (pressed)
			{
				pointerEvent.eligibleForClick = true;
				pointerEvent.delta = Vector2.zero;
				pointerEvent.dragging = false;
				pointerEvent.useDragThreshold = true;
				pointerEvent.pressPosition = pointerEvent.position;
				pointerEvent.pointerPressRaycast = pointerEvent.pointerCurrentRaycast;
				this.HandleMouseTouchDeselectionOnSelectionChanged(gameObject, pointerEvent);
				if (pointerEvent.pointerEnter != gameObject)
				{
					base.HandlePointerExitAndEnter(pointerEvent, gameObject);
					pointerEvent.pointerEnter = gameObject;
				}
				GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(gameObject, pointerEvent, ExecuteEvents.pointerDownHandler);
				if (gameObject2 == null)
				{
					gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				}
				double unscaledTime = ReInput.time.unscaledTime;
				if (gameObject2 == pointerEvent.lastPress)
				{
					if (unscaledTime - (double)pointerEvent.clickTime < 0.30000001192092896)
					{
						int num = pointerEvent.clickCount + 1;
						pointerEvent.clickCount = num;
					}
					else
					{
						pointerEvent.clickCount = 1;
					}
					pointerEvent.clickTime = (float)unscaledTime;
				}
				else
				{
					pointerEvent.clickCount = 1;
				}
				pointerEvent.pointerPress = gameObject2;
				pointerEvent.rawPointerPress = gameObject;
				pointerEvent.clickTime = (float)unscaledTime;
				pointerEvent.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
				if (pointerEvent.pointerDrag != null)
				{
					ExecuteEvents.Execute<IInitializePotentialDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.initializePotentialDrag);
				}
			}
			if (released)
			{
				ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
				GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				if (pointerEvent.pointerPress == eventHandler && pointerEvent.eligibleForClick)
				{
					ExecuteEvents.Execute<IPointerClickHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerClickHandler);
				}
				else if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					ExecuteEvents.ExecuteHierarchy<IDropHandler>(gameObject, pointerEvent, ExecuteEvents.dropHandler);
				}
				pointerEvent.eligibleForClick = false;
				pointerEvent.pointerPress = null;
				pointerEvent.rawPointerPress = null;
				if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					ExecuteEvents.Execute<IEndDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);
				}
				pointerEvent.dragging = false;
				pointerEvent.pointerDrag = null;
				if (pointerEvent.pointerDrag != null)
				{
					ExecuteEvents.Execute<IEndDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);
				}
				pointerEvent.pointerDrag = null;
				ExecuteEvents.ExecuteHierarchy<IPointerExitHandler>(pointerEvent.pointerEnter, pointerEvent, ExecuteEvents.pointerExitHandler);
				pointerEvent.pointerEnter = null;
			}
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x00063440 File Offset: 0x00061640
		private bool SendSubmitEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			if (this.recompiling)
			{
				return false;
			}
			BaseEventData baseEventData = this.GetBaseEventData();
			for (int i = 0; i < this.playerIds.Length; i++)
			{
				Player player = ReInput.players.GetPlayer(this.playerIds[i]);
				if (player != null && (!this.usePlayingPlayersOnly || player.isPlaying))
				{
					if (this.GetButtonDown(player, this.submitActionId))
					{
						ExecuteEvents.Execute<ISubmitHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.submitHandler);
						break;
					}
					if (this.GetButtonDown(player, this.cancelActionId))
					{
						ExecuteEvents.Execute<ICancelHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.cancelHandler);
						break;
					}
				}
			}
			return baseEventData.used;
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x00063508 File Offset: 0x00061708
		private Vector2 GetRawMoveVector()
		{
			if (this.recompiling)
			{
				return Vector2.zero;
			}
			Vector2 zero = Vector2.zero;
			for (int i = 0; i < this.playerIds.Length; i++)
			{
				Player player = ReInput.players.GetPlayer(this.playerIds[i]);
				if (player != null && (!this.usePlayingPlayersOnly || player.isPlaying))
				{
					float num = this.GetAxis(player, this.horizontalActionId);
					float num2 = this.GetAxis(player, this.verticalActionId);
					if (Mathf.Approximately(num, 0f))
					{
						num = 0f;
					}
					if (Mathf.Approximately(num2, 0f))
					{
						num2 = 0f;
					}
					if (this.moveOneElementPerAxisPress)
					{
						if (this.GetButtonDown(player, this.horizontalActionId) && num > 0f)
						{
							zero.x += 1f;
						}
						if (this.GetNegativeButtonDown(player, this.horizontalActionId) && num < 0f)
						{
							zero.x -= 1f;
						}
						if (this.GetButtonDown(player, this.verticalActionId) && num2 > 0f)
						{
							zero.y += 1f;
						}
						if (this.GetNegativeButtonDown(player, this.verticalActionId) && num2 < 0f)
						{
							zero.y -= 1f;
						}
					}
					else
					{
						if (this.GetButton(player, this.horizontalActionId) && num > 0f)
						{
							zero.x += 1f;
						}
						if (this.GetNegativeButton(player, this.horizontalActionId) && num < 0f)
						{
							zero.x -= 1f;
						}
						if (this.GetButton(player, this.verticalActionId) && num2 > 0f)
						{
							zero.y += 1f;
						}
						if (this.GetNegativeButton(player, this.verticalActionId) && num2 < 0f)
						{
							zero.y -= 1f;
						}
					}
				}
			}
			return zero;
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x00063708 File Offset: 0x00061908
		private bool SendMoveEventToSelectedObject()
		{
			if (this.recompiling)
			{
				return false;
			}
			double unscaledTime = ReInput.time.unscaledTime;
			Vector2 rawMoveVector = this.GetRawMoveVector();
			if (Mathf.Approximately(rawMoveVector.x, 0f) && Mathf.Approximately(rawMoveVector.y, 0f))
			{
				this.m_ConsecutiveMoveCount = 0;
				return false;
			}
			bool flag = Vector2.Dot(rawMoveVector, this.m_LastMoveVector) > 0f;
			bool flag2;
			bool flag3;
			this.CheckButtonOrKeyMovement(out flag2, out flag3);
			AxisEventData axisEventData = null;
			bool flag4 = flag2 || flag3;
			if (flag4)
			{
				axisEventData = this.GetAxisEventData(rawMoveVector.x, rawMoveVector.y, 0f);
				MoveDirection moveDir = axisEventData.moveDir;
				flag4 = ((moveDir == 1 || moveDir == 3) && flag3) || ((moveDir == null || moveDir == 2) && flag2);
			}
			if (!flag4)
			{
				if (this.m_RepeatDelay > 0f)
				{
					if (flag && this.m_ConsecutiveMoveCount == 1)
					{
						flag4 = unscaledTime > this.m_PrevActionTime + (double)this.m_RepeatDelay;
					}
					else
					{
						flag4 = unscaledTime > this.m_PrevActionTime + (double)(1f / this.m_InputActionsPerSecond);
					}
				}
				else
				{
					flag4 = unscaledTime > this.m_PrevActionTime + (double)(1f / this.m_InputActionsPerSecond);
				}
			}
			if (!flag4)
			{
				return false;
			}
			if (axisEventData == null)
			{
				axisEventData = this.GetAxisEventData(rawMoveVector.x, rawMoveVector.y, 0f);
			}
			if (axisEventData.moveDir != 4)
			{
				ExecuteEvents.Execute<IMoveHandler>(base.eventSystem.currentSelectedGameObject, axisEventData, ExecuteEvents.moveHandler);
				if (!flag)
				{
					this.m_ConsecutiveMoveCount = 0;
				}
				if (this.m_ConsecutiveMoveCount == 0 || (!flag2 && !flag3))
				{
					this.m_ConsecutiveMoveCount++;
				}
				this.m_PrevActionTime = unscaledTime;
				this.m_LastMoveVector = rawMoveVector;
			}
			else
			{
				this.m_ConsecutiveMoveCount = 0;
			}
			return axisEventData.used;
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x000638C0 File Offset: 0x00061AC0
		private void CheckButtonOrKeyMovement(out bool downHorizontal, out bool downVertical)
		{
			downHorizontal = false;
			downVertical = false;
			for (int i = 0; i < this.playerIds.Length; i++)
			{
				Player player = ReInput.players.GetPlayer(this.playerIds[i]);
				if (player != null && (!this.usePlayingPlayersOnly || player.isPlaying))
				{
					downHorizontal |= this.GetButtonDown(player, this.horizontalActionId) || this.GetNegativeButtonDown(player, this.horizontalActionId);
					downVertical |= this.GetButtonDown(player, this.verticalActionId) || this.GetNegativeButtonDown(player, this.verticalActionId);
				}
			}
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x00063954 File Offset: 0x00061B54
		private void ProcessMouseEvents()
		{
			for (int i = 0; i < this.playerIds.Length; i++)
			{
				Player player = ReInput.players.GetPlayer(this.playerIds[i]);
				if (player != null && (!this.usePlayingPlayersOnly || player.isPlaying))
				{
					int mouseInputSourceCount = base.GetMouseInputSourceCount(this.playerIds[i]);
					for (int j = 0; j < mouseInputSourceCount; j++)
					{
						this.ProcessMouseEvent(this.playerIds[i], j);
					}
				}
			}
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x000639C4 File Offset: 0x00061BC4
		private void ProcessMouseEvent(int playerId, int pointerIndex)
		{
			RewiredPointerInputModule.MouseState mousePointerEventData = this.GetMousePointerEventData(playerId, pointerIndex);
			if (mousePointerEventData == null)
			{
				return;
			}
			RewiredPointerInputModule.MouseButtonEventData eventData = mousePointerEventData.GetButtonState(0).eventData;
			this.ProcessMousePress(eventData);
			this.ProcessMove(eventData.buttonData);
			this.ProcessDrag(eventData.buttonData);
			this.ProcessMousePress(mousePointerEventData.GetButtonState(1).eventData);
			this.ProcessDrag(mousePointerEventData.GetButtonState(1).eventData.buttonData);
			this.ProcessMousePress(mousePointerEventData.GetButtonState(2).eventData);
			this.ProcessDrag(mousePointerEventData.GetButtonState(2).eventData.buttonData);
			IMouseInputSource mouseInputSource = base.GetMouseInputSource(playerId, pointerIndex);
			if (mouseInputSource == null)
			{
				return;
			}
			for (int i = 3; i < mouseInputSource.buttonCount; i++)
			{
				this.ProcessMousePress(mousePointerEventData.GetButtonState(i).eventData);
				this.ProcessDrag(mousePointerEventData.GetButtonState(i).eventData.buttonData);
			}
			if (!Mathf.Approximately(eventData.buttonData.scrollDelta.sqrMagnitude, 0f))
			{
				ExecuteEvents.ExecuteHierarchy<IScrollHandler>(ExecuteEvents.GetEventHandler<IScrollHandler>(eventData.buttonData.pointerCurrentRaycast.gameObject), eventData.buttonData, ExecuteEvents.scrollHandler);
			}
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00063AF0 File Offset: 0x00061CF0
		private bool SendUpdateEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			BaseEventData baseEventData = this.GetBaseEventData();
			ExecuteEvents.Execute<IUpdateSelectedHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.updateSelectedHandler);
			return baseEventData.used;
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00063B38 File Offset: 0x00061D38
		private void ProcessMousePress(RewiredPointerInputModule.MouseButtonEventData data)
		{
			PlayerPointerEventData buttonData = data.buttonData;
			if (base.GetMouseInputSource(buttonData.playerId, buttonData.inputSourceIndex) == null)
			{
				return;
			}
			GameObject gameObject = buttonData.pointerCurrentRaycast.gameObject;
			if (data.PressedThisFrame())
			{
				buttonData.eligibleForClick = true;
				buttonData.delta = Vector2.zero;
				buttonData.dragging = false;
				buttonData.useDragThreshold = true;
				buttonData.pressPosition = buttonData.position;
				buttonData.pointerPressRaycast = buttonData.pointerCurrentRaycast;
				this.HandleMouseTouchDeselectionOnSelectionChanged(gameObject, buttonData);
				GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(gameObject, buttonData, ExecuteEvents.pointerDownHandler);
				if (gameObject2 == null)
				{
					gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				}
				double unscaledTime = ReInput.time.unscaledTime;
				if (gameObject2 == buttonData.lastPress)
				{
					if (unscaledTime - (double)buttonData.clickTime < 0.30000001192092896)
					{
						PlayerPointerEventData playerPointerEventData = buttonData;
						int num = playerPointerEventData.clickCount + 1;
						playerPointerEventData.clickCount = num;
					}
					else
					{
						buttonData.clickCount = 1;
					}
					buttonData.clickTime = (float)unscaledTime;
				}
				else
				{
					buttonData.clickCount = 1;
				}
				buttonData.pointerPress = gameObject2;
				buttonData.rawPointerPress = gameObject;
				buttonData.clickTime = (float)unscaledTime;
				buttonData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
				if (buttonData.pointerDrag != null)
				{
					ExecuteEvents.Execute<IInitializePotentialDragHandler>(buttonData.pointerDrag, buttonData, ExecuteEvents.initializePotentialDrag);
				}
			}
			if (data.ReleasedThisFrame())
			{
				ExecuteEvents.Execute<IPointerUpHandler>(buttonData.pointerPress, buttonData, ExecuteEvents.pointerUpHandler);
				GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				if (buttonData.pointerPress == eventHandler && buttonData.eligibleForClick)
				{
					ExecuteEvents.Execute<IPointerClickHandler>(buttonData.pointerPress, buttonData, ExecuteEvents.pointerClickHandler);
				}
				else if (buttonData.pointerDrag != null && buttonData.dragging)
				{
					ExecuteEvents.ExecuteHierarchy<IDropHandler>(gameObject, buttonData, ExecuteEvents.dropHandler);
				}
				buttonData.eligibleForClick = false;
				buttonData.pointerPress = null;
				buttonData.rawPointerPress = null;
				if (buttonData.pointerDrag != null && buttonData.dragging)
				{
					ExecuteEvents.Execute<IEndDragHandler>(buttonData.pointerDrag, buttonData, ExecuteEvents.endDragHandler);
				}
				buttonData.dragging = false;
				buttonData.pointerDrag = null;
				if (gameObject != buttonData.pointerEnter)
				{
					base.HandlePointerExitAndEnter(buttonData, null);
					base.HandlePointerExitAndEnter(buttonData, gameObject);
				}
			}
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x00063D54 File Offset: 0x00061F54
		private void HandleMouseTouchDeselectionOnSelectionChanged(GameObject currentOverGo, BaseEventData pointerEvent)
		{
			if (this.m_deselectIfBackgroundClicked && this.m_deselectBeforeSelecting)
			{
				base.DeselectIfSelectionChanged(currentOverGo, pointerEvent);
				return;
			}
			GameObject eventHandler = ExecuteEvents.GetEventHandler<ISelectHandler>(currentOverGo);
			if (this.m_deselectIfBackgroundClicked)
			{
				if (eventHandler != base.eventSystem.currentSelectedGameObject && eventHandler != null)
				{
					base.eventSystem.SetSelectedGameObject(null, pointerEvent);
					return;
				}
			}
			else if (this.m_deselectBeforeSelecting && eventHandler != null && eventHandler != base.eventSystem.currentSelectedGameObject)
			{
				base.eventSystem.SetSelectedGameObject(null, pointerEvent);
			}
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x0001218F File Offset: 0x0001038F
		private void OnApplicationFocus(bool hasFocus)
		{
			this.m_HasFocus = hasFocus;
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00012198 File Offset: 0x00010398
		private bool ShouldIgnoreEventsOnNoFocus()
		{
			return !ReInput.isReady || ReInput.configuration.ignoreInputWhenAppNotInFocus;
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x000121AD File Offset: 0x000103AD
		protected override void OnDestroy()
		{
			base.OnDestroy();
			ReInput.InitializedEvent -= this.OnRewiredInitialized;
			ReInput.ShutDownEvent -= this.OnRewiredShutDown;
			ReInput.EditorRecompileEvent -= this.OnEditorRecompile;
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x00063DE4 File Offset: 0x00061FE4
		protected override bool IsDefaultPlayer(int playerId)
		{
			if (this.playerIds == null)
			{
				return false;
			}
			if (!ReInput.isReady)
			{
				return false;
			}
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < this.playerIds.Length; j++)
				{
					Player player = ReInput.players.GetPlayer(this.playerIds[j]);
					if (player != null && (i >= 1 || !this.usePlayingPlayersOnly || player.isPlaying) && (i >= 2 || player.controllers.hasMouse))
					{
						return this.playerIds[j] == playerId;
					}
				}
			}
			return false;
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x00063E6C File Offset: 0x0006206C
		private void InitializeRewired()
		{
			if (!ReInput.isReady)
			{
				Debug.LogError("Rewired is not initialized! Are you missing a Rewired Input Manager in your scene?");
				return;
			}
			ReInput.ShutDownEvent -= this.OnRewiredShutDown;
			ReInput.ShutDownEvent += this.OnRewiredShutDown;
			ReInput.EditorRecompileEvent -= this.OnEditorRecompile;
			ReInput.EditorRecompileEvent += this.OnEditorRecompile;
			this.SetupRewiredVars();
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x00063ED8 File Offset: 0x000620D8
		private void SetupRewiredVars()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			this.SetUpRewiredActions();
			if (this.useAllRewiredGamePlayers)
			{
				IList<Player> list = (this.useRewiredSystemPlayer ? ReInput.players.AllPlayers : ReInput.players.Players);
				this.playerIds = new int[list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					this.playerIds[i] = list[i].id;
				}
			}
			else
			{
				bool flag = false;
				List<int> list2 = new List<int>(this.rewiredPlayerIds.Length + 1);
				for (int j = 0; j < this.rewiredPlayerIds.Length; j++)
				{
					Player player = ReInput.players.GetPlayer(this.rewiredPlayerIds[j]);
					if (player != null && !list2.Contains(player.id))
					{
						list2.Add(player.id);
						if (player.id == 9999999)
						{
							flag = true;
						}
					}
				}
				if (this.useRewiredSystemPlayer && !flag)
				{
					list2.Insert(0, ReInput.players.GetSystemPlayer().id);
				}
				this.playerIds = list2.ToArray();
			}
			this.SetUpRewiredPlayerMice();
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00063FF8 File Offset: 0x000621F8
		private void SetUpRewiredPlayerMice()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			base.ClearMouseInputSources();
			for (int i = 0; i < this.playerMice.Count; i++)
			{
				PlayerMouse playerMouse = this.playerMice[i];
				if (!UnityTools.IsNullOrDestroyed<PlayerMouse>(playerMouse))
				{
					base.AddMouseInputSource(playerMouse);
				}
			}
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00064048 File Offset: 0x00062248
		private void SetUpRewiredActions()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			if (!this.setActionsById)
			{
				this.horizontalActionId = ReInput.mapping.GetActionId(this.m_HorizontalAxis);
				this.verticalActionId = ReInput.mapping.GetActionId(this.m_VerticalAxis);
				this.submitActionId = ReInput.mapping.GetActionId(this.m_SubmitButton);
				this.cancelActionId = ReInput.mapping.GetActionId(this.m_CancelButton);
				return;
			}
			InputAction inputAction = ReInput.mapping.GetAction(this.horizontalActionId);
			this.m_HorizontalAxis = ((inputAction != null) ? inputAction.name : string.Empty);
			if (inputAction == null)
			{
				this.horizontalActionId = -1;
			}
			inputAction = ReInput.mapping.GetAction(this.verticalActionId);
			this.m_VerticalAxis = ((inputAction != null) ? inputAction.name : string.Empty);
			if (inputAction == null)
			{
				this.verticalActionId = -1;
			}
			inputAction = ReInput.mapping.GetAction(this.submitActionId);
			this.m_SubmitButton = ((inputAction != null) ? inputAction.name : string.Empty);
			if (inputAction == null)
			{
				this.submitActionId = -1;
			}
			inputAction = ReInput.mapping.GetAction(this.cancelActionId);
			this.m_CancelButton = ((inputAction != null) ? inputAction.name : string.Empty);
			if (inputAction == null)
			{
				this.cancelActionId = -1;
			}
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x000121E8 File Offset: 0x000103E8
		private bool GetButton(Player player, int actionId)
		{
			return actionId >= 0 && player.GetButton(actionId);
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x000121F7 File Offset: 0x000103F7
		private bool GetButtonDown(Player player, int actionId)
		{
			return actionId >= 0 && player.GetButtonDown(actionId);
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x00012206 File Offset: 0x00010406
		private bool GetNegativeButton(Player player, int actionId)
		{
			return actionId >= 0 && player.GetNegativeButton(actionId);
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x00012215 File Offset: 0x00010415
		private bool GetNegativeButtonDown(Player player, int actionId)
		{
			return actionId >= 0 && player.GetNegativeButtonDown(actionId);
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x00012224 File Offset: 0x00010424
		private float GetAxis(Player player, int actionId)
		{
			if (actionId < 0)
			{
				return 0f;
			}
			return player.GetAxis(actionId);
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x00012237 File Offset: 0x00010437
		private void CheckEditorRecompile()
		{
			if (!this.recompiling)
			{
				return;
			}
			if (!ReInput.isReady)
			{
				return;
			}
			this.recompiling = false;
			this.InitializeRewired();
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x00012257 File Offset: 0x00010457
		private void OnEditorRecompile()
		{
			this.recompiling = true;
			this.ClearRewiredVars();
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x00012266 File Offset: 0x00010466
		private void ClearRewiredVars()
		{
			Array.Clear(this.playerIds, 0, this.playerIds.Length);
			base.ClearMouseInputSources();
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x00064184 File Offset: 0x00062384
		private bool DidAnyMouseMove()
		{
			for (int i = 0; i < this.playerIds.Length; i++)
			{
				int num = this.playerIds[i];
				Player player = ReInput.players.GetPlayer(num);
				if (player != null && (!this.usePlayingPlayersOnly || player.isPlaying))
				{
					int mouseInputSourceCount = base.GetMouseInputSourceCount(num);
					for (int j = 0; j < mouseInputSourceCount; j++)
					{
						IMouseInputSource mouseInputSource = base.GetMouseInputSource(num, j);
						if (mouseInputSource != null && mouseInputSource.screenPositionDelta.sqrMagnitude > 0f)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x00064210 File Offset: 0x00062410
		private bool GetMouseButtonDownOnAnyMouse(int buttonIndex)
		{
			for (int i = 0; i < this.playerIds.Length; i++)
			{
				int num = this.playerIds[i];
				Player player = ReInput.players.GetPlayer(num);
				if (player != null && (!this.usePlayingPlayersOnly || player.isPlaying))
				{
					int mouseInputSourceCount = base.GetMouseInputSourceCount(num);
					for (int j = 0; j < mouseInputSourceCount; j++)
					{
						IMouseInputSource mouseInputSource = base.GetMouseInputSource(num, j);
						if (mouseInputSource != null && mouseInputSource.GetButtonDown(buttonIndex))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x00012282 File Offset: 0x00010482
		private void OnRewiredInitialized()
		{
			this.InitializeRewired();
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0001228A File Offset: 0x0001048A
		private void OnRewiredShutDown()
		{
			this.ClearRewiredVars();
		}

		// Token: 0x04001B67 RID: 7015
		private const string DEFAULT_ACTION_MOVE_HORIZONTAL = "UIHorizontal";

		// Token: 0x04001B68 RID: 7016
		private const string DEFAULT_ACTION_MOVE_VERTICAL = "UIVertical";

		// Token: 0x04001B69 RID: 7017
		private const string DEFAULT_ACTION_SUBMIT = "UISubmit";

		// Token: 0x04001B6A RID: 7018
		private const string DEFAULT_ACTION_CANCEL = "UICancel";

		// Token: 0x04001B6B RID: 7019
		[Tooltip("(Optional) Link the Rewired Input Manager here for easier access to Player ids, etc.")]
		[SerializeField]
		private InputManager_Base rewiredInputManager;

		// Token: 0x04001B6C RID: 7020
		[SerializeField]
		[Tooltip("Use all Rewired game Players to control the UI. This does not include the System Player. If enabled, this setting overrides individual Player Ids set in Rewired Player Ids.")]
		private bool useAllRewiredGamePlayers;

		// Token: 0x04001B6D RID: 7021
		[SerializeField]
		[Tooltip("Allow the Rewired System Player to control the UI.")]
		private bool useRewiredSystemPlayer;

		// Token: 0x04001B6E RID: 7022
		[SerializeField]
		[Tooltip("A list of Player Ids that are allowed to control the UI. If Use All Rewired Game Players = True, this list will be ignored.")]
		private int[] rewiredPlayerIds = new int[1];

		// Token: 0x04001B6F RID: 7023
		[SerializeField]
		[Tooltip("Allow only Players with Player.isPlaying = true to control the UI.")]
		private bool usePlayingPlayersOnly;

		// Token: 0x04001B70 RID: 7024
		[SerializeField]
		[Tooltip("Player Mice allowed to interact with the UI. Each Player that owns a Player Mouse must also be allowed to control the UI or the Player Mouse will not function.")]
		private List<PlayerMouse> playerMice = new List<PlayerMouse>();

		// Token: 0x04001B71 RID: 7025
		[SerializeField]
		[Tooltip("Makes an axis press always move only one UI selection. Enable if you do not want to allow scrolling through UI elements by holding an axis direction.")]
		private bool moveOneElementPerAxisPress;

		// Token: 0x04001B72 RID: 7026
		[SerializeField]
		[Tooltip("If enabled, Action Ids will be used to set the Actions. If disabled, string names will be used to set the Actions.")]
		private bool setActionsById;

		// Token: 0x04001B73 RID: 7027
		[SerializeField]
		[Tooltip("Id of the horizontal Action for movement (if axis events are used).")]
		private int horizontalActionId = -1;

		// Token: 0x04001B74 RID: 7028
		[SerializeField]
		[Tooltip("Id of the vertical Action for movement (if axis events are used).")]
		private int verticalActionId = -1;

		// Token: 0x04001B75 RID: 7029
		[SerializeField]
		[Tooltip("Id of the Action used to submit.")]
		private int submitActionId = -1;

		// Token: 0x04001B76 RID: 7030
		[SerializeField]
		[Tooltip("Id of the Action used to cancel.")]
		private int cancelActionId = -1;

		// Token: 0x04001B77 RID: 7031
		[SerializeField]
		[Tooltip("Name of the horizontal axis for movement (if axis events are used).")]
		private string m_HorizontalAxis = "UIHorizontal";

		// Token: 0x04001B78 RID: 7032
		[SerializeField]
		[Tooltip("Name of the vertical axis for movement (if axis events are used).")]
		private string m_VerticalAxis = "UIVertical";

		// Token: 0x04001B79 RID: 7033
		[SerializeField]
		[Tooltip("Name of the action used to submit.")]
		private string m_SubmitButton = "UISubmit";

		// Token: 0x04001B7A RID: 7034
		[SerializeField]
		[Tooltip("Name of the action used to cancel.")]
		private string m_CancelButton = "UICancel";

		// Token: 0x04001B7B RID: 7035
		[SerializeField]
		[Tooltip("Number of selection changes allowed per second when a movement button/axis is held in a direction.")]
		private float m_InputActionsPerSecond = 10f;

		// Token: 0x04001B7C RID: 7036
		[SerializeField]
		[Tooltip("Delay in seconds before vertical/horizontal movement starts repeating continouously when a movement direction is held.")]
		private float m_RepeatDelay;

		// Token: 0x04001B7D RID: 7037
		[SerializeField]
		[Tooltip("Allows the mouse to be used to select elements.")]
		private bool m_allowMouseInput = true;

		// Token: 0x04001B7E RID: 7038
		[SerializeField]
		[Tooltip("Allows the mouse to be used to select elements if the device also supports touch control.")]
		private bool m_allowMouseInputIfTouchSupported = true;

		// Token: 0x04001B7F RID: 7039
		[SerializeField]
		[Tooltip("Allows touch input to be used to select elements.")]
		private bool m_allowTouchInput = true;

		// Token: 0x04001B80 RID: 7040
		[SerializeField]
		[Tooltip("Deselects the current selection on mouse/touch click when the pointer is not over a selectable object.")]
		private bool m_deselectIfBackgroundClicked = true;

		// Token: 0x04001B81 RID: 7041
		[SerializeField]
		[Tooltip("Deselects the current selection on mouse/touch click before selecting the next object.")]
		private bool m_deselectBeforeSelecting = true;

		// Token: 0x04001B82 RID: 7042
		[SerializeField]
		[FormerlySerializedAs("m_AllowActivationOnMobileDevice")]
		[Tooltip("Forces the module to always be active.")]
		private bool m_ForceModuleActive;

		// Token: 0x04001B83 RID: 7043
		[NonSerialized]
		private int[] playerIds;

		// Token: 0x04001B84 RID: 7044
		private bool recompiling;

		// Token: 0x04001B85 RID: 7045
		[NonSerialized]
		private bool isTouchSupported;

		// Token: 0x04001B86 RID: 7046
		[NonSerialized]
		private double m_PrevActionTime;

		// Token: 0x04001B87 RID: 7047
		[NonSerialized]
		private Vector2 m_LastMoveVector;

		// Token: 0x04001B88 RID: 7048
		[NonSerialized]
		private int m_ConsecutiveMoveCount;

		// Token: 0x04001B89 RID: 7049
		[NonSerialized]
		private bool m_HasFocus = true;

		// Token: 0x02000429 RID: 1065
		[Serializable]
		public class PlayerSetting
		{
			// Token: 0x0600179F RID: 6047 RVA: 0x00012292 File Offset: 0x00010492
			public PlayerSetting()
			{
			}

			// Token: 0x060017A0 RID: 6048 RVA: 0x0006428C File Offset: 0x0006248C
			private PlayerSetting(RewiredStandaloneInputModule.PlayerSetting other)
			{
				if (other == null)
				{
					throw new ArgumentNullException("other");
				}
				this.playerId = other.playerId;
				this.playerMice = new List<PlayerMouse>();
				if (other.playerMice != null)
				{
					foreach (PlayerMouse playerMouse in other.playerMice)
					{
						this.playerMice.Add(playerMouse);
					}
				}
			}

			// Token: 0x060017A1 RID: 6049 RVA: 0x000122A5 File Offset: 0x000104A5
			public RewiredStandaloneInputModule.PlayerSetting Clone()
			{
				return new RewiredStandaloneInputModule.PlayerSetting(this);
			}

			// Token: 0x04001B8A RID: 7050
			public int playerId;

			// Token: 0x04001B8B RID: 7051
			public List<PlayerMouse> playerMice = new List<PlayerMouse>();
		}
	}
}
