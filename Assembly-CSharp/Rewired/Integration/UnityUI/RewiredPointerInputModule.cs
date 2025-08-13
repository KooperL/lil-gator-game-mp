using System;
using System.Collections.Generic;
using System.Text;
using Rewired.UI;
using Rewired.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rewired.Integration.UnityUI
{
	// Token: 0x02000422 RID: 1058
	public abstract class RewiredPointerInputModule : BaseInputModule
	{
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001708 RID: 5896 RVA: 0x00062124 File Offset: 0x00060324
		private RewiredPointerInputModule.UnityInputSource defaultInputSource
		{
			get
			{
				if (this.__m_DefaultInputSource == null)
				{
					return this.__m_DefaultInputSource = new RewiredPointerInputModule.UnityInputSource();
				}
				return this.__m_DefaultInputSource;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x00011BD3 File Offset: 0x0000FDD3
		private IMouseInputSource defaultMouseInputSource
		{
			get
			{
				return this.defaultInputSource;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x0600170A RID: 5898 RVA: 0x00011BD3 File Offset: 0x0000FDD3
		protected ITouchInputSource defaultTouchInputSource
		{
			get
			{
				return this.defaultInputSource;
			}
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00011BDB File Offset: 0x0000FDDB
		protected bool IsDefaultMouse(IMouseInputSource mouse)
		{
			return this.defaultMouseInputSource == mouse;
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x00062150 File Offset: 0x00060350
		public IMouseInputSource GetMouseInputSource(int playerId, int mouseIndex)
		{
			if (mouseIndex < 0)
			{
				throw new ArgumentOutOfRangeException("mouseIndex");
			}
			if (this.m_MouseInputSourcesList.Count == 0 && this.IsDefaultPlayer(playerId))
			{
				return this.defaultMouseInputSource;
			}
			int count = this.m_MouseInputSourcesList.Count;
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				IMouseInputSource mouseInputSource = this.m_MouseInputSourcesList[i];
				if (!UnityTools.IsNullOrDestroyed<IMouseInputSource>(mouseInputSource) && mouseInputSource.playerId == playerId)
				{
					if (mouseIndex == num)
					{
						return mouseInputSource;
					}
					num++;
				}
			}
			return null;
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00011BE6 File Offset: 0x0000FDE6
		public void RemoveMouseInputSource(IMouseInputSource source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.m_MouseInputSourcesList.Remove(source);
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x00011C03 File Offset: 0x0000FE03
		public void AddMouseInputSource(IMouseInputSource source)
		{
			if (UnityTools.IsNullOrDestroyed<IMouseInputSource>(source))
			{
				throw new ArgumentNullException("source");
			}
			this.m_MouseInputSourcesList.Add(source);
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x000621CC File Offset: 0x000603CC
		public int GetMouseInputSourceCount(int playerId)
		{
			if (this.m_MouseInputSourcesList.Count == 0 && this.IsDefaultPlayer(playerId))
			{
				return 1;
			}
			int count = this.m_MouseInputSourcesList.Count;
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				IMouseInputSource mouseInputSource = this.m_MouseInputSourcesList[i];
				if (!UnityTools.IsNullOrDestroyed<IMouseInputSource>(mouseInputSource) && mouseInputSource.playerId == playerId)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x00011C24 File Offset: 0x0000FE24
		public ITouchInputSource GetTouchInputSource(int playerId, int sourceIndex)
		{
			if (!UnityTools.IsNullOrDestroyed<ITouchInputSource>(this.m_UserDefaultTouchInputSource))
			{
				return this.m_UserDefaultTouchInputSource;
			}
			return this.defaultTouchInputSource;
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x00011C40 File Offset: 0x0000FE40
		public void RemoveTouchInputSource(ITouchInputSource source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (this.m_UserDefaultTouchInputSource == source)
			{
				this.m_UserDefaultTouchInputSource = null;
			}
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x00011C60 File Offset: 0x0000FE60
		public void AddTouchInputSource(ITouchInputSource source)
		{
			if (UnityTools.IsNullOrDestroyed<ITouchInputSource>(source))
			{
				throw new ArgumentNullException("source");
			}
			this.m_UserDefaultTouchInputSource = source;
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x00011C7C File Offset: 0x0000FE7C
		public int GetTouchInputSourceCount(int playerId)
		{
			if (!this.IsDefaultPlayer(playerId))
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x00011C8A File Offset: 0x0000FE8A
		protected void ClearMouseInputSources()
		{
			this.m_MouseInputSourcesList.Clear();
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x00062230 File Offset: 0x00060430
		protected virtual bool isMouseSupported
		{
			get
			{
				int count = this.m_MouseInputSourcesList.Count;
				if (count == 0)
				{
					return this.defaultMouseInputSource.enabled;
				}
				for (int i = 0; i < count; i++)
				{
					if (this.m_MouseInputSourcesList[i].enabled)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06001716 RID: 5910
		protected abstract bool IsDefaultPlayer(int playerId);

		// Token: 0x06001717 RID: 5911 RVA: 0x0006227C File Offset: 0x0006047C
		protected bool GetPointerData(int playerId, int pointerIndex, int pointerTypeId, out PlayerPointerEventData data, bool create, PointerEventType pointerEventType)
		{
			Dictionary<int, PlayerPointerEventData>[] array;
			if (!this.m_PlayerPointerData.TryGetValue(playerId, out array))
			{
				array = new Dictionary<int, PlayerPointerEventData>[pointerIndex + 1];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new Dictionary<int, PlayerPointerEventData>();
				}
				this.m_PlayerPointerData.Add(playerId, array);
			}
			if (pointerIndex >= array.Length)
			{
				Dictionary<int, PlayerPointerEventData>[] array2 = new Dictionary<int, PlayerPointerEventData>[pointerIndex + 1];
				for (int j = 0; j < array.Length; j++)
				{
					array2[j] = array[j];
				}
				array2[pointerIndex] = new Dictionary<int, PlayerPointerEventData>();
				array = array2;
				this.m_PlayerPointerData[playerId] = array;
			}
			Dictionary<int, PlayerPointerEventData> dictionary = array[pointerIndex];
			if (dictionary.TryGetValue(pointerTypeId, out data))
			{
				data.mouseSource = ((pointerEventType == PointerEventType.Mouse) ? this.GetMouseInputSource(playerId, pointerIndex) : null);
				data.touchSource = ((pointerEventType == PointerEventType.Touch) ? this.GetTouchInputSource(playerId, pointerIndex) : null);
				return false;
			}
			if (!create)
			{
				return false;
			}
			data = this.CreatePointerEventData(playerId, pointerIndex, pointerTypeId, pointerEventType);
			dictionary.Add(pointerTypeId, data);
			return true;
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x00062364 File Offset: 0x00060564
		private PlayerPointerEventData CreatePointerEventData(int playerId, int pointerIndex, int pointerTypeId, PointerEventType pointerEventType)
		{
			PlayerPointerEventData playerPointerEventData = new PlayerPointerEventData(base.eventSystem)
			{
				playerId = playerId,
				inputSourceIndex = pointerIndex,
				pointerId = pointerTypeId,
				sourceType = pointerEventType
			};
			if (pointerEventType == PointerEventType.Mouse)
			{
				playerPointerEventData.mouseSource = this.GetMouseInputSource(playerId, pointerIndex);
			}
			else if (pointerEventType == PointerEventType.Touch)
			{
				playerPointerEventData.touchSource = this.GetTouchInputSource(playerId, pointerIndex);
			}
			if (pointerTypeId == -1)
			{
				playerPointerEventData.buttonIndex = 0;
			}
			else if (pointerTypeId == -2)
			{
				playerPointerEventData.buttonIndex = 1;
			}
			else if (pointerTypeId == -3)
			{
				playerPointerEventData.buttonIndex = 2;
			}
			else if (pointerTypeId >= -2147483520 && pointerTypeId <= -2147483392)
			{
				playerPointerEventData.buttonIndex = pointerTypeId - -2147483520;
			}
			return playerPointerEventData;
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x00062408 File Offset: 0x00060608
		protected void RemovePointerData(PlayerPointerEventData data)
		{
			Dictionary<int, PlayerPointerEventData>[] array;
			if (this.m_PlayerPointerData.TryGetValue(data.playerId, out array) && data.inputSourceIndex < array.Length)
			{
				array[data.inputSourceIndex].Remove(data.pointerId);
			}
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0006244C File Offset: 0x0006064C
		protected PlayerPointerEventData GetTouchPointerEventData(int playerId, int touchDeviceIndex, Touch input, out bool pressed, out bool released)
		{
			PlayerPointerEventData playerPointerEventData;
			bool pointerData = this.GetPointerData(playerId, touchDeviceIndex, input.fingerId, out playerPointerEventData, true, PointerEventType.Touch);
			playerPointerEventData.Reset();
			pressed = pointerData || input.phase == 0;
			released = input.phase == 4 || input.phase == 3;
			if (pointerData)
			{
				playerPointerEventData.position = input.position;
			}
			if (pressed)
			{
				playerPointerEventData.delta = Vector2.zero;
			}
			else
			{
				playerPointerEventData.delta = input.position - playerPointerEventData.position;
			}
			playerPointerEventData.position = input.position;
			playerPointerEventData.button = 0;
			base.eventSystem.RaycastAll(playerPointerEventData, this.m_RaycastResultCache);
			RaycastResult raycastResult = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
			playerPointerEventData.pointerCurrentRaycast = raycastResult;
			this.m_RaycastResultCache.Clear();
			return playerPointerEventData;
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x00062520 File Offset: 0x00060720
		protected virtual RewiredPointerInputModule.MouseState GetMousePointerEventData(int playerId, int mouseIndex)
		{
			IMouseInputSource mouseInputSource = this.GetMouseInputSource(playerId, mouseIndex);
			if (mouseInputSource == null)
			{
				return null;
			}
			PlayerPointerEventData playerPointerEventData;
			bool pointerData = this.GetPointerData(playerId, mouseIndex, -1, out playerPointerEventData, true, PointerEventType.Mouse);
			playerPointerEventData.Reset();
			if (pointerData)
			{
				playerPointerEventData.position = mouseInputSource.screenPosition;
			}
			Vector2 screenPosition = mouseInputSource.screenPosition;
			if (mouseInputSource.locked || !mouseInputSource.enabled)
			{
				playerPointerEventData.position = new Vector2(-1f, -1f);
				playerPointerEventData.delta = Vector2.zero;
			}
			else
			{
				playerPointerEventData.delta = screenPosition - playerPointerEventData.position;
				playerPointerEventData.position = screenPosition;
			}
			playerPointerEventData.scrollDelta = mouseInputSource.wheelDelta;
			playerPointerEventData.button = 0;
			base.eventSystem.RaycastAll(playerPointerEventData, this.m_RaycastResultCache);
			RaycastResult raycastResult = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
			playerPointerEventData.pointerCurrentRaycast = raycastResult;
			this.m_RaycastResultCache.Clear();
			PlayerPointerEventData playerPointerEventData2;
			this.GetPointerData(playerId, mouseIndex, -2, out playerPointerEventData2, true, PointerEventType.Mouse);
			this.CopyFromTo(playerPointerEventData, playerPointerEventData2);
			playerPointerEventData2.button = 1;
			PlayerPointerEventData playerPointerEventData3;
			this.GetPointerData(playerId, mouseIndex, -3, out playerPointerEventData3, true, PointerEventType.Mouse);
			this.CopyFromTo(playerPointerEventData, playerPointerEventData3);
			playerPointerEventData3.button = 2;
			for (int i = 3; i < mouseInputSource.buttonCount; i++)
			{
				PlayerPointerEventData playerPointerEventData4;
				this.GetPointerData(playerId, mouseIndex, -2147483520 + i, out playerPointerEventData4, true, PointerEventType.Mouse);
				this.CopyFromTo(playerPointerEventData, playerPointerEventData4);
				playerPointerEventData4.button = -1;
			}
			this.m_MouseState.SetButtonState(0, this.StateForMouseButton(playerId, mouseIndex, 0), playerPointerEventData);
			this.m_MouseState.SetButtonState(1, this.StateForMouseButton(playerId, mouseIndex, 1), playerPointerEventData2);
			this.m_MouseState.SetButtonState(2, this.StateForMouseButton(playerId, mouseIndex, 2), playerPointerEventData3);
			for (int j = 3; j < mouseInputSource.buttonCount; j++)
			{
				PlayerPointerEventData playerPointerEventData5;
				this.GetPointerData(playerId, mouseIndex, -2147483520 + j, out playerPointerEventData5, false, PointerEventType.Mouse);
				this.m_MouseState.SetButtonState(j, this.StateForMouseButton(playerId, mouseIndex, j), playerPointerEventData5);
			}
			return this.m_MouseState;
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x000626FC File Offset: 0x000608FC
		protected PlayerPointerEventData GetLastPointerEventData(int playerId, int pointerIndex, int pointerTypeId, bool ignorePointerTypeId, PointerEventType pointerEventType)
		{
			if (!ignorePointerTypeId)
			{
				PlayerPointerEventData playerPointerEventData;
				this.GetPointerData(playerId, pointerIndex, pointerTypeId, out playerPointerEventData, false, pointerEventType);
				return playerPointerEventData;
			}
			Dictionary<int, PlayerPointerEventData>[] array;
			if (!this.m_PlayerPointerData.TryGetValue(playerId, out array))
			{
				return null;
			}
			if (pointerIndex >= array.Length)
			{
				return null;
			}
			using (Dictionary<int, PlayerPointerEventData>.Enumerator enumerator = array[pointerIndex].GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					KeyValuePair<int, PlayerPointerEventData> keyValuePair = enumerator.Current;
					return keyValuePair.Value;
				}
			}
			return null;
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x00062784 File Offset: 0x00060984
		private static bool ShouldStartDrag(Vector2 pressPos, Vector2 currentPos, float threshold, bool useDragThreshold)
		{
			return !useDragThreshold || (pressPos - currentPos).sqrMagnitude >= threshold * threshold;
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x000627B0 File Offset: 0x000609B0
		protected virtual void ProcessMove(PlayerPointerEventData pointerEvent)
		{
			GameObject gameObject;
			if (pointerEvent.sourceType == PointerEventType.Mouse)
			{
				IMouseInputSource mouseInputSource = this.GetMouseInputSource(pointerEvent.playerId, pointerEvent.inputSourceIndex);
				if (mouseInputSource != null)
				{
					gameObject = ((!mouseInputSource.enabled || mouseInputSource.locked) ? null : pointerEvent.pointerCurrentRaycast.gameObject);
				}
				else
				{
					gameObject = null;
				}
			}
			else
			{
				if (pointerEvent.sourceType != PointerEventType.Touch)
				{
					throw new NotImplementedException();
				}
				gameObject = pointerEvent.pointerCurrentRaycast.gameObject;
			}
			base.HandlePointerExitAndEnter(pointerEvent, gameObject);
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0006282C File Offset: 0x00060A2C
		protected virtual void ProcessDrag(PlayerPointerEventData pointerEvent)
		{
			if (!pointerEvent.IsPointerMoving() || pointerEvent.pointerDrag == null)
			{
				return;
			}
			if (pointerEvent.sourceType == PointerEventType.Mouse)
			{
				IMouseInputSource mouseInputSource = this.GetMouseInputSource(pointerEvent.playerId, pointerEvent.inputSourceIndex);
				if (mouseInputSource == null || mouseInputSource.locked || !mouseInputSource.enabled)
				{
					return;
				}
			}
			if (!pointerEvent.dragging && RewiredPointerInputModule.ShouldStartDrag(pointerEvent.pressPosition, pointerEvent.position, (float)base.eventSystem.pixelDragThreshold, pointerEvent.useDragThreshold))
			{
				ExecuteEvents.Execute<IBeginDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.beginDragHandler);
				pointerEvent.dragging = true;
			}
			if (pointerEvent.dragging)
			{
				if (pointerEvent.pointerPress != pointerEvent.pointerDrag)
				{
					ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
					pointerEvent.eligibleForClick = false;
					pointerEvent.pointerPress = null;
					pointerEvent.rawPointerPress = null;
				}
				ExecuteEvents.Execute<IDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.dragHandler);
			}
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x0006291C File Offset: 0x00060B1C
		public override bool IsPointerOverGameObject(int pointerTypeId)
		{
			foreach (KeyValuePair<int, Dictionary<int, PlayerPointerEventData>[]> keyValuePair in this.m_PlayerPointerData)
			{
				Dictionary<int, PlayerPointerEventData>[] value = keyValuePair.Value;
				for (int i = 0; i < value.Length; i++)
				{
					PlayerPointerEventData playerPointerEventData;
					if (value[i].TryGetValue(pointerTypeId, out playerPointerEventData) && playerPointerEventData.pointerEnter != null)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x000629A4 File Offset: 0x00060BA4
		protected void ClearSelection()
		{
			BaseEventData baseEventData = this.GetBaseEventData();
			foreach (KeyValuePair<int, Dictionary<int, PlayerPointerEventData>[]> keyValuePair in this.m_PlayerPointerData)
			{
				Dictionary<int, PlayerPointerEventData>[] value = keyValuePair.Value;
				for (int i = 0; i < value.Length; i++)
				{
					foreach (KeyValuePair<int, PlayerPointerEventData> keyValuePair2 in value[i])
					{
						base.HandlePointerExitAndEnter(keyValuePair2.Value, null);
					}
					value[i].Clear();
				}
			}
			base.eventSystem.SetSelectedGameObject(null, baseEventData);
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x00062A70 File Offset: 0x00060C70
		public override string ToString()
		{
			string text = "<b>Pointer Input Module of type: </b>";
			Type type = base.GetType();
			StringBuilder stringBuilder = new StringBuilder(text + ((type != null) ? type.ToString() : null));
			stringBuilder.AppendLine();
			foreach (KeyValuePair<int, Dictionary<int, PlayerPointerEventData>[]> keyValuePair in this.m_PlayerPointerData)
			{
				stringBuilder.AppendLine("<B>Player Id:</b> " + keyValuePair.Key.ToString());
				Dictionary<int, PlayerPointerEventData>[] value = keyValuePair.Value;
				for (int i = 0; i < value.Length; i++)
				{
					stringBuilder.AppendLine("<B>Pointer Index:</b> " + i.ToString());
					foreach (KeyValuePair<int, PlayerPointerEventData> keyValuePair2 in value[i])
					{
						stringBuilder.AppendLine("<B>Button Id:</b> " + keyValuePair2.Key.ToString());
						stringBuilder.AppendLine(keyValuePair2.Value.ToString());
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x00011C97 File Offset: 0x0000FE97
		protected void DeselectIfSelectionChanged(GameObject currentOverGo, BaseEventData pointerEvent)
		{
			if (ExecuteEvents.GetEventHandler<ISelectHandler>(currentOverGo) != base.eventSystem.currentSelectedGameObject)
			{
				base.eventSystem.SetSelectedGameObject(null, pointerEvent);
			}
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x00011CBE File Offset: 0x0000FEBE
		protected void CopyFromTo(PointerEventData from, PointerEventData to)
		{
			to.position = from.position;
			to.delta = from.delta;
			to.scrollDelta = from.scrollDelta;
			to.pointerCurrentRaycast = from.pointerCurrentRaycast;
			to.pointerEnter = from.pointerEnter;
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x00062BBC File Offset: 0x00060DBC
		protected PointerEventData.FramePressState StateForMouseButton(int playerId, int mouseIndex, int buttonId)
		{
			IMouseInputSource mouseInputSource = this.GetMouseInputSource(playerId, mouseIndex);
			if (mouseInputSource == null)
			{
				return 3;
			}
			bool buttonDown = mouseInputSource.GetButtonDown(buttonId);
			bool buttonUp = mouseInputSource.GetButtonUp(buttonId);
			if (buttonDown && buttonUp)
			{
				return 2;
			}
			if (buttonDown)
			{
				return 0;
			}
			if (buttonUp)
			{
				return 1;
			}
			return 3;
		}

		// Token: 0x04001B50 RID: 6992
		public const int kMouseLeftId = -1;

		// Token: 0x04001B51 RID: 6993
		public const int kMouseRightId = -2;

		// Token: 0x04001B52 RID: 6994
		public const int kMouseMiddleId = -3;

		// Token: 0x04001B53 RID: 6995
		public const int kFakeTouchesId = -4;

		// Token: 0x04001B54 RID: 6996
		private const int customButtonsStartingId = -2147483520;

		// Token: 0x04001B55 RID: 6997
		private const int customButtonsMaxCount = 128;

		// Token: 0x04001B56 RID: 6998
		private const int customButtonsLastId = -2147483392;

		// Token: 0x04001B57 RID: 6999
		private readonly List<IMouseInputSource> m_MouseInputSourcesList = new List<IMouseInputSource>();

		// Token: 0x04001B58 RID: 7000
		private Dictionary<int, Dictionary<int, PlayerPointerEventData>[]> m_PlayerPointerData = new Dictionary<int, Dictionary<int, PlayerPointerEventData>[]>();

		// Token: 0x04001B59 RID: 7001
		private ITouchInputSource m_UserDefaultTouchInputSource;

		// Token: 0x04001B5A RID: 7002
		private RewiredPointerInputModule.UnityInputSource __m_DefaultInputSource;

		// Token: 0x04001B5B RID: 7003
		private readonly RewiredPointerInputModule.MouseState m_MouseState = new RewiredPointerInputModule.MouseState();

		// Token: 0x02000423 RID: 1059
		protected class MouseState
		{
			// Token: 0x06001727 RID: 5927 RVA: 0x00062BFC File Offset: 0x00060DFC
			public bool AnyPressesThisFrame()
			{
				for (int i = 0; i < this.m_TrackedButtons.Count; i++)
				{
					if (this.m_TrackedButtons[i].eventData.PressedThisFrame())
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001728 RID: 5928 RVA: 0x00062C3C File Offset: 0x00060E3C
			public bool AnyReleasesThisFrame()
			{
				for (int i = 0; i < this.m_TrackedButtons.Count; i++)
				{
					if (this.m_TrackedButtons[i].eventData.ReleasedThisFrame())
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001729 RID: 5929 RVA: 0x00062C7C File Offset: 0x00060E7C
			public RewiredPointerInputModule.ButtonState GetButtonState(int button)
			{
				RewiredPointerInputModule.ButtonState buttonState = null;
				for (int i = 0; i < this.m_TrackedButtons.Count; i++)
				{
					if (this.m_TrackedButtons[i].button == button)
					{
						buttonState = this.m_TrackedButtons[i];
						break;
					}
				}
				if (buttonState == null)
				{
					buttonState = new RewiredPointerInputModule.ButtonState
					{
						button = button,
						eventData = new RewiredPointerInputModule.MouseButtonEventData()
					};
					this.m_TrackedButtons.Add(buttonState);
				}
				return buttonState;
			}

			// Token: 0x0600172A RID: 5930 RVA: 0x00011D25 File Offset: 0x0000FF25
			public void SetButtonState(int button, PointerEventData.FramePressState stateForMouseButton, PlayerPointerEventData data)
			{
				RewiredPointerInputModule.ButtonState buttonState = this.GetButtonState(button);
				buttonState.eventData.buttonState = stateForMouseButton;
				buttonState.eventData.buttonData = data;
			}

			// Token: 0x04001B5C RID: 7004
			private List<RewiredPointerInputModule.ButtonState> m_TrackedButtons = new List<RewiredPointerInputModule.ButtonState>();
		}

		// Token: 0x02000424 RID: 1060
		public class MouseButtonEventData
		{
			// Token: 0x0600172C RID: 5932 RVA: 0x00011D58 File Offset: 0x0000FF58
			public bool PressedThisFrame()
			{
				return this.buttonState == null || this.buttonState == 2;
			}

			// Token: 0x0600172D RID: 5933 RVA: 0x00011D6D File Offset: 0x0000FF6D
			public bool ReleasedThisFrame()
			{
				return this.buttonState == 1 || this.buttonState == 2;
			}

			// Token: 0x04001B5D RID: 7005
			public PointerEventData.FramePressState buttonState;

			// Token: 0x04001B5E RID: 7006
			public PlayerPointerEventData buttonData;
		}

		// Token: 0x02000425 RID: 1061
		protected class ButtonState
		{
			// Token: 0x17000436 RID: 1078
			// (get) Token: 0x0600172F RID: 5935 RVA: 0x00011D83 File Offset: 0x0000FF83
			// (set) Token: 0x06001730 RID: 5936 RVA: 0x00011D8B File Offset: 0x0000FF8B
			public RewiredPointerInputModule.MouseButtonEventData eventData
			{
				get
				{
					return this.m_EventData;
				}
				set
				{
					this.m_EventData = value;
				}
			}

			// Token: 0x17000437 RID: 1079
			// (get) Token: 0x06001731 RID: 5937 RVA: 0x00011D94 File Offset: 0x0000FF94
			// (set) Token: 0x06001732 RID: 5938 RVA: 0x00011D9C File Offset: 0x0000FF9C
			public int button
			{
				get
				{
					return this.m_Button;
				}
				set
				{
					this.m_Button = value;
				}
			}

			// Token: 0x04001B5F RID: 7007
			private int m_Button;

			// Token: 0x04001B60 RID: 7008
			private RewiredPointerInputModule.MouseButtonEventData m_EventData;
		}

		// Token: 0x02000426 RID: 1062
		private sealed class UnityInputSource : IMouseInputSource, ITouchInputSource
		{
			// Token: 0x17000438 RID: 1080
			// (get) Token: 0x06001734 RID: 5940 RVA: 0x00011DA5 File Offset: 0x0000FFA5
			int IMouseInputSource.playerId
			{
				get
				{
					this.TryUpdate();
					return 0;
				}
			}

			// Token: 0x17000439 RID: 1081
			// (get) Token: 0x06001735 RID: 5941 RVA: 0x00011DA5 File Offset: 0x0000FFA5
			int ITouchInputSource.playerId
			{
				get
				{
					this.TryUpdate();
					return 0;
				}
			}

			// Token: 0x1700043A RID: 1082
			// (get) Token: 0x06001736 RID: 5942 RVA: 0x00011DAE File Offset: 0x0000FFAE
			bool IMouseInputSource.enabled
			{
				get
				{
					this.TryUpdate();
					return true;
				}
			}

			// Token: 0x1700043B RID: 1083
			// (get) Token: 0x06001737 RID: 5943 RVA: 0x00011DB7 File Offset: 0x0000FFB7
			bool IMouseInputSource.locked
			{
				get
				{
					this.TryUpdate();
					return Cursor.lockState == 1;
				}
			}

			// Token: 0x1700043C RID: 1084
			// (get) Token: 0x06001738 RID: 5944 RVA: 0x00011DC7 File Offset: 0x0000FFC7
			int IMouseInputSource.buttonCount
			{
				get
				{
					this.TryUpdate();
					return 3;
				}
			}

			// Token: 0x06001739 RID: 5945 RVA: 0x00011DD0 File Offset: 0x0000FFD0
			bool IMouseInputSource.GetButtonDown(int button)
			{
				this.TryUpdate();
				return Input.GetMouseButtonDown(button);
			}

			// Token: 0x0600173A RID: 5946 RVA: 0x00011DDE File Offset: 0x0000FFDE
			bool IMouseInputSource.GetButtonUp(int button)
			{
				this.TryUpdate();
				return Input.GetMouseButtonUp(button);
			}

			// Token: 0x0600173B RID: 5947 RVA: 0x00011DEC File Offset: 0x0000FFEC
			bool IMouseInputSource.GetButton(int button)
			{
				this.TryUpdate();
				return Input.GetMouseButton(button);
			}

			// Token: 0x1700043D RID: 1085
			// (get) Token: 0x0600173C RID: 5948 RVA: 0x00011DFA File Offset: 0x0000FFFA
			Vector2 IMouseInputSource.screenPosition
			{
				get
				{
					this.TryUpdate();
					return Input.mousePosition;
				}
			}

			// Token: 0x1700043E RID: 1086
			// (get) Token: 0x0600173D RID: 5949 RVA: 0x00011E0C File Offset: 0x0001000C
			Vector2 IMouseInputSource.screenPositionDelta
			{
				get
				{
					this.TryUpdate();
					return this.m_MousePosition - this.m_MousePositionPrev;
				}
			}

			// Token: 0x1700043F RID: 1087
			// (get) Token: 0x0600173E RID: 5950 RVA: 0x00011E25 File Offset: 0x00010025
			Vector2 IMouseInputSource.wheelDelta
			{
				get
				{
					this.TryUpdate();
					return Input.mouseScrollDelta;
				}
			}

			// Token: 0x17000440 RID: 1088
			// (get) Token: 0x0600173F RID: 5951 RVA: 0x00011E32 File Offset: 0x00010032
			bool ITouchInputSource.touchSupported
			{
				get
				{
					this.TryUpdate();
					return Input.touchSupported;
				}
			}

			// Token: 0x17000441 RID: 1089
			// (get) Token: 0x06001740 RID: 5952 RVA: 0x00011E3F File Offset: 0x0001003F
			int ITouchInputSource.touchCount
			{
				get
				{
					this.TryUpdate();
					return Input.touchCount;
				}
			}

			// Token: 0x06001741 RID: 5953 RVA: 0x00011E4C File Offset: 0x0001004C
			Touch ITouchInputSource.GetTouch(int index)
			{
				this.TryUpdate();
				return Input.GetTouch(index);
			}

			// Token: 0x06001742 RID: 5954 RVA: 0x00011E5A File Offset: 0x0001005A
			private void TryUpdate()
			{
				if (Time.frameCount == this.m_LastUpdatedFrame)
				{
					return;
				}
				this.m_LastUpdatedFrame = Time.frameCount;
				this.m_MousePositionPrev = this.m_MousePosition;
				this.m_MousePosition = Input.mousePosition;
			}

			// Token: 0x04001B61 RID: 7009
			private Vector2 m_MousePosition;

			// Token: 0x04001B62 RID: 7010
			private Vector2 m_MousePositionPrev;

			// Token: 0x04001B63 RID: 7011
			private int m_LastUpdatedFrame = -1;
		}
	}
}
