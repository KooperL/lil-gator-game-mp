using System;
using System.Collections.Generic;
using System.ComponentModel;
using Rewired.Internal;
using Rewired.Utils.Interfaces;
using Rewired.Utils.Platforms.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.Utils
{
	[EditorBrowsable(1)]
	public class ExternalTools : IExternalTools
	{
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x000114AC File Offset: 0x0000F6AC
		// (set) Token: 0x06001625 RID: 5669 RVA: 0x000114B3 File Offset: 0x0000F6B3
		public static Func<object> getPlatformInitializerDelegate
		{
			get
			{
				return ExternalTools._getPlatformInitializerDelegate;
			}
			set
			{
				ExternalTools._getPlatformInitializerDelegate = value;
			}
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x00002229 File Offset: 0x00000429
		public void Destroy()
		{
		}

		// (get) Token: 0x06001628 RID: 5672 RVA: 0x000114BB File Offset: 0x0000F6BB
		public bool isEditorPaused
		{
			get
			{
				return this._isEditorPaused;
			}
		}

		// (add) Token: 0x06001629 RID: 5673 RVA: 0x000114C3 File Offset: 0x0000F6C3
		// (remove) Token: 0x0600162A RID: 5674 RVA: 0x000114DC File Offset: 0x0000F6DC
		public event Action<bool> EditorPausedStateChangedEvent
		{
			add
			{
				this._EditorPausedStateChangedEvent = (Action<bool>)Delegate.Combine(this._EditorPausedStateChangedEvent, value);
			}
			remove
			{
				this._EditorPausedStateChangedEvent = (Action<bool>)Delegate.Remove(this._EditorPausedStateChangedEvent, value);
			}
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x000114F5 File Offset: 0x0000F6F5
		public object GetPlatformInitializer()
		{
			return Main.GetPlatformInitializer();
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x000114FC File Offset: 0x0000F6FC
		public string GetFocusedEditorWindowTitle()
		{
			return string.Empty;
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x00003A8E File Offset: 0x00001C8E
		public bool IsEditorSceneViewFocused()
		{
			return false;
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x00003A8E File Offset: 0x00001C8E
		public bool LinuxInput_IsJoystickPreconfigured(string name)
		{
			return false;
		}

		// (add) Token: 0x0600162F RID: 5679 RVA: 0x00061358 File Offset: 0x0005F558
		// (remove) Token: 0x06001630 RID: 5680 RVA: 0x00061390 File Offset: 0x0005F590
		public event Action<uint, bool> XboxOneInput_OnGamepadStateChange;

		// Token: 0x06001631 RID: 5681 RVA: 0x00003A8E File Offset: 0x00001C8E
		public int XboxOneInput_GetUserIdForGamepad(uint id)
		{
			return 0;
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x00011503 File Offset: 0x0000F703
		public ulong XboxOneInput_GetControllerId(uint unityJoystickId)
		{
			return 0UL;
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x00003A8E File Offset: 0x00001C8E
		public bool XboxOneInput_IsGamepadActive(uint unityJoystickId)
		{
			return false;
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x000114FC File Offset: 0x0000F6FC
		public string XboxOneInput_GetControllerType(ulong xboxControllerId)
		{
			return string.Empty;
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x00003A8E File Offset: 0x00001C8E
		public uint XboxOneInput_GetJoystickId(ulong xboxControllerId)
		{
			return 0U;
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x00002229 File Offset: 0x00000429
		public void XboxOne_Gamepad_UpdatePlugin()
		{
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x00003A8E File Offset: 0x00001C8E
		public bool XboxOne_Gamepad_SetGamepadVibration(ulong xboxOneJoystickId, float leftMotor, float rightMotor, float leftTriggerLevel, float rightTriggerLevel)
		{
			return false;
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x00002229 File Offset: 0x00000429
		public void XboxOne_Gamepad_PulseVibrateMotor(ulong xboxOneJoystickId, int motorInt, float startLevel, float endLevel, ulong durationMS)
		{
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x00011507 File Offset: 0x0000F707
		public Vector3 PS4Input_GetLastAcceleration(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x00011507 File Offset: 0x0000F707
		public Vector3 PS4Input_GetLastGyro(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x0001150E File Offset: 0x0000F70E
		public Vector4 PS4Input_GetLastOrientation(int id)
		{
			return Vector4.zero;
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x00011515 File Offset: 0x0000F715
		public void PS4Input_GetLastTouchData(int id, out int touchNum, out int touch0x, out int touch0y, out int touch0id, out int touch1x, out int touch1y, out int touch1id)
		{
			touchNum = 0;
			touch0x = 0;
			touch0y = 0;
			touch0id = 0;
			touch1x = 0;
			touch1y = 0;
			touch1id = 0;
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x00011531 File Offset: 0x0000F731
		public void PS4Input_GetPadControllerInformation(int id, out float touchpixelDensity, out int touchResolutionX, out int touchResolutionY, out int analogDeadZoneLeft, out int analogDeadZoneright, out int connectionType)
		{
			touchpixelDensity = 0f;
			touchResolutionX = 0;
			touchResolutionY = 0;
			analogDeadZoneLeft = 0;
			analogDeadZoneright = 0;
			connectionType = 0;
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_PadSetMotionSensorState(int id, bool bEnable)
		{
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_PadSetTiltCorrectionState(int id, bool bEnable)
		{
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_PadSetAngularVelocityDeadbandState(int id, bool bEnable)
		{
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_PadSetLightBar(int id, int red, int green, int blue)
		{
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_PadResetLightBar(int id)
		{
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_PadSetVibration(int id, int largeMotor, int smallMotor)
		{
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_PadResetOrientation(int id)
		{
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x00003A8E File Offset: 0x00001C8E
		public bool PS4Input_PadIsConnected(int id)
		{
			return false;
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_GetUsersDetails(int slot, object loggedInUser)
		{
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x0001154D File Offset: 0x0000F74D
		public int PS4Input_GetDeviceClassForHandle(int handle)
		{
			return -1;
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x00006415 File Offset: 0x00004615
		public string PS4Input_GetDeviceClassString(int intValue)
		{
			return null;
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x00003A8E File Offset: 0x00001C8E
		public int PS4Input_PadGetUsersHandles2(int maxControllers, int[] handles)
		{
			return 0;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_GetSpecialControllerInformation(int id, int padIndex, object controllerInformation)
		{
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x00011507 File Offset: 0x0000F707
		public Vector3 PS4Input_SpecialGetLastAcceleration(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00011507 File Offset: 0x0000F707
		public Vector3 PS4Input_SpecialGetLastGyro(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x0001150E File Offset: 0x0000F70E
		public Vector4 PS4Input_SpecialGetLastOrientation(int id)
		{
			return Vector4.zero;
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x00003A8E File Offset: 0x00001C8E
		public int PS4Input_SpecialGetUsersHandles(int maxNumberControllers, int[] handles)
		{
			return 0;
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x00003A8E File Offset: 0x00001C8E
		public int PS4Input_SpecialGetUsersHandles2(int maxNumberControllers, int[] handles)
		{
			return 0;
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x00003A8E File Offset: 0x00001C8E
		public bool PS4Input_SpecialIsConnected(int id)
		{
			return false;
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_SpecialResetLightSphere(int id)
		{
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_SpecialResetOrientation(int id)
		{
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_SpecialSetAngularVelocityDeadbandState(int id, bool bEnable)
		{
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_SpecialSetLightSphere(int id, int red, int green, int blue)
		{
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_SpecialSetMotionSensorState(int id, bool bEnable)
		{
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_SpecialSetTiltCorrectionState(int id, bool bEnable)
		{
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_SpecialSetVibration(int id, int largeMotor, int smallMotor)
		{
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00011507 File Offset: 0x0000F707
		public Vector3 PS4Input_AimGetLastAcceleration(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x00011507 File Offset: 0x0000F707
		public Vector3 PS4Input_AimGetLastGyro(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0001150E File Offset: 0x0000F70E
		public Vector4 PS4Input_AimGetLastOrientation(int id)
		{
			return Vector4.zero;
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x00003A8E File Offset: 0x00001C8E
		public int PS4Input_AimGetUsersHandles(int maxNumberControllers, int[] handles)
		{
			return 0;
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x00003A8E File Offset: 0x00001C8E
		public int PS4Input_AimGetUsersHandles2(int maxNumberControllers, int[] handles)
		{
			return 0;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x00003A8E File Offset: 0x00001C8E
		public bool PS4Input_AimIsConnected(int id)
		{
			return false;
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_AimResetLightSphere(int id)
		{
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_AimResetOrientation(int id)
		{
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_AimSetAngularVelocityDeadbandState(int id, bool bEnable)
		{
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_AimSetLightSphere(int id, int red, int green, int blue)
		{
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_AimSetMotionSensorState(int id, bool bEnable)
		{
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_AimSetTiltCorrectionState(int id, bool bEnable)
		{
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_AimSetVibration(int id, int largeMotor, int smallMotor)
		{
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x00011507 File Offset: 0x0000F707
		public Vector3 PS4Input_GetLastMoveAcceleration(int id, int index)
		{
			return Vector3.zero;
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00011507 File Offset: 0x0000F707
		public Vector3 PS4Input_GetLastMoveGyro(int id, int index)
		{
			return Vector3.zero;
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x00003A8E File Offset: 0x00001C8E
		public int PS4Input_MoveGetButtons(int id, int index)
		{
			return 0;
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00003A8E File Offset: 0x00001C8E
		public int PS4Input_MoveGetAnalogButton(int id, int index)
		{
			return 0;
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00003A8E File Offset: 0x00001C8E
		public bool PS4Input_MoveIsConnected(int id, int index)
		{
			return false;
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00003A8E File Offset: 0x00001C8E
		public int PS4Input_MoveGetUsersMoveHandles(int maxNumberControllers, int[] primaryHandles, int[] secondaryHandles)
		{
			return 0;
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x00003A8E File Offset: 0x00001C8E
		public int PS4Input_MoveGetUsersMoveHandles(int maxNumberControllers, int[] primaryHandles)
		{
			return 0;
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x00003A8E File Offset: 0x00001C8E
		public int PS4Input_MoveGetUsersMoveHandles(int maxNumberControllers)
		{
			return 0;
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x00011550 File Offset: 0x0000F750
		public IntPtr PS4Input_MoveGetControllerInputForTracking()
		{
			return IntPtr.Zero;
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x00003A8E File Offset: 0x00001C8E
		public int PS4Input_MoveSetLightSphere(int id, int index, int red, int green, int blue)
		{
			return 0;
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00003A8E File Offset: 0x00001C8E
		public int PS4Input_MoveSetVibration(int id, int index, int motor)
		{
			return 0;
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x00011557 File Offset: 0x0000F757
		public void GetDeviceVIDPIDs(out List<int> vids, out List<int> pids)
		{
			vids = new List<int>();
			pids = new List<int>();
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x0001154D File Offset: 0x0000F74D
		public int GetAndroidAPILevel()
		{
			return -1;
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x00011567 File Offset: 0x0000F767
		public bool UnityUI_Graphic_GetRaycastTarget(object graphic)
		{
			return !(graphic as Graphic == null) && (graphic as Graphic).raycastTarget;
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x00011584 File Offset: 0x0000F784
		public void UnityUI_Graphic_SetRaycastTarget(object graphic, bool value)
		{
			if (graphic as Graphic == null)
			{
				return;
			}
			(graphic as Graphic).raycastTarget = value;
		}

		// (get) Token: 0x06001674 RID: 5748 RVA: 0x000115A1 File Offset: 0x0000F7A1
		public bool UnityInput_IsTouchPressureSupported
		{
			get
			{
				return Input.touchPressureSupported;
			}
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x000115A8 File Offset: 0x0000F7A8
		public float UnityInput_GetTouchPressure(ref Touch touch)
		{
			return touch.pressure;
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x000115B0 File Offset: 0x0000F7B0
		public float UnityInput_GetTouchMaximumPossiblePressure(ref Touch touch)
		{
			return touch.maximumPossiblePressure;
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x000115B8 File Offset: 0x0000F7B8
		public IControllerTemplate CreateControllerTemplate(Guid typeGuid, object payload)
		{
			return ControllerTemplateFactory.Create(typeGuid, payload);
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x000115C1 File Offset: 0x0000F7C1
		public Type[] GetControllerTemplateTypes()
		{
			return ControllerTemplateFactory.templateTypes;
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x000115C8 File Offset: 0x0000F7C8
		public Type[] GetControllerTemplateInterfaceTypes()
		{
			return ControllerTemplateFactory.templateInterfaceTypes;
		}

		private static Func<object> _getPlatformInitializerDelegate;

		private bool _isEditorPaused;

		private Action<bool> _EditorPausedStateChangedEvent;
	}
}
