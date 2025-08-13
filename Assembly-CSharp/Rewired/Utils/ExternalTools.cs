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
	// Token: 0x0200040C RID: 1036
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ExternalTools : IExternalTools
	{
		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x000110AF File Offset: 0x0000F2AF
		// (set) Token: 0x060015C5 RID: 5573 RVA: 0x000110B6 File Offset: 0x0000F2B6
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

		// Token: 0x060015C7 RID: 5575 RVA: 0x00002229 File Offset: 0x00000429
		public void Destroy()
		{
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x000110BE File Offset: 0x0000F2BE
		public bool isEditorPaused
		{
			get
			{
				return this._isEditorPaused;
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060015C9 RID: 5577 RVA: 0x000110C6 File Offset: 0x0000F2C6
		// (remove) Token: 0x060015CA RID: 5578 RVA: 0x000110DF File Offset: 0x0000F2DF
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

		// Token: 0x060015CB RID: 5579 RVA: 0x000110F8 File Offset: 0x0000F2F8
		public object GetPlatformInitializer()
		{
			return Main.GetPlatformInitializer();
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x000110FF File Offset: 0x0000F2FF
		public string GetFocusedEditorWindowTitle()
		{
			return string.Empty;
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x000039A2 File Offset: 0x00001BA2
		public bool IsEditorSceneViewFocused()
		{
			return false;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x000039A2 File Offset: 0x00001BA2
		public bool LinuxInput_IsJoystickPreconfigured(string name)
		{
			return false;
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060015CF RID: 5583 RVA: 0x0005F330 File Offset: 0x0005D530
		// (remove) Token: 0x060015D0 RID: 5584 RVA: 0x0005F368 File Offset: 0x0005D568
		public event Action<uint, bool> XboxOneInput_OnGamepadStateChange;

		// Token: 0x060015D1 RID: 5585 RVA: 0x000039A2 File Offset: 0x00001BA2
		public int XboxOneInput_GetUserIdForGamepad(uint id)
		{
			return 0;
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00011106 File Offset: 0x0000F306
		public ulong XboxOneInput_GetControllerId(uint unityJoystickId)
		{
			return 0UL;
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x000039A2 File Offset: 0x00001BA2
		public bool XboxOneInput_IsGamepadActive(uint unityJoystickId)
		{
			return false;
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x000110FF File Offset: 0x0000F2FF
		public string XboxOneInput_GetControllerType(ulong xboxControllerId)
		{
			return string.Empty;
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x000039A2 File Offset: 0x00001BA2
		public uint XboxOneInput_GetJoystickId(ulong xboxControllerId)
		{
			return 0U;
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00002229 File Offset: 0x00000429
		public void XboxOne_Gamepad_UpdatePlugin()
		{
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x000039A2 File Offset: 0x00001BA2
		public bool XboxOne_Gamepad_SetGamepadVibration(ulong xboxOneJoystickId, float leftMotor, float rightMotor, float leftTriggerLevel, float rightTriggerLevel)
		{
			return false;
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x00002229 File Offset: 0x00000429
		public void XboxOne_Gamepad_PulseVibrateMotor(ulong xboxOneJoystickId, int motorInt, float startLevel, float endLevel, ulong durationMS)
		{
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0001110A File Offset: 0x0000F30A
		public Vector3 PS4Input_GetLastAcceleration(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0001110A File Offset: 0x0000F30A
		public Vector3 PS4Input_GetLastGyro(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x00011111 File Offset: 0x0000F311
		public Vector4 PS4Input_GetLastOrientation(int id)
		{
			return Vector4.zero;
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x00011118 File Offset: 0x0000F318
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

		// Token: 0x060015DD RID: 5597 RVA: 0x00011134 File Offset: 0x0000F334
		public void PS4Input_GetPadControllerInformation(int id, out float touchpixelDensity, out int touchResolutionX, out int touchResolutionY, out int analogDeadZoneLeft, out int analogDeadZoneright, out int connectionType)
		{
			touchpixelDensity = 0f;
			touchResolutionX = 0;
			touchResolutionY = 0;
			analogDeadZoneLeft = 0;
			analogDeadZoneright = 0;
			connectionType = 0;
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_PadSetMotionSensorState(int id, bool bEnable)
		{
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_PadSetTiltCorrectionState(int id, bool bEnable)
		{
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_PadSetAngularVelocityDeadbandState(int id, bool bEnable)
		{
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_PadSetLightBar(int id, int red, int green, int blue)
		{
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_PadResetLightBar(int id)
		{
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_PadSetVibration(int id, int largeMotor, int smallMotor)
		{
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_PadResetOrientation(int id)
		{
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x000039A2 File Offset: 0x00001BA2
		public bool PS4Input_PadIsConnected(int id)
		{
			return false;
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_GetUsersDetails(int slot, object loggedInUser)
		{
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00011150 File Offset: 0x0000F350
		public int PS4Input_GetDeviceClassForHandle(int handle)
		{
			return -1;
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x0000614F File Offset: 0x0000434F
		public string PS4Input_GetDeviceClassString(int intValue)
		{
			return null;
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x000039A2 File Offset: 0x00001BA2
		public int PS4Input_PadGetUsersHandles2(int maxControllers, int[] handles)
		{
			return 0;
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_GetSpecialControllerInformation(int id, int padIndex, object controllerInformation)
		{
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x0001110A File Offset: 0x0000F30A
		public Vector3 PS4Input_SpecialGetLastAcceleration(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x0001110A File Offset: 0x0000F30A
		public Vector3 PS4Input_SpecialGetLastGyro(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00011111 File Offset: 0x0000F311
		public Vector4 PS4Input_SpecialGetLastOrientation(int id)
		{
			return Vector4.zero;
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x000039A2 File Offset: 0x00001BA2
		public int PS4Input_SpecialGetUsersHandles(int maxNumberControllers, int[] handles)
		{
			return 0;
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x000039A2 File Offset: 0x00001BA2
		public int PS4Input_SpecialGetUsersHandles2(int maxNumberControllers, int[] handles)
		{
			return 0;
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x000039A2 File Offset: 0x00001BA2
		public bool PS4Input_SpecialIsConnected(int id)
		{
			return false;
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_SpecialResetLightSphere(int id)
		{
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_SpecialResetOrientation(int id)
		{
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_SpecialSetAngularVelocityDeadbandState(int id, bool bEnable)
		{
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_SpecialSetLightSphere(int id, int red, int green, int blue)
		{
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_SpecialSetMotionSensorState(int id, bool bEnable)
		{
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_SpecialSetTiltCorrectionState(int id, bool bEnable)
		{
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_SpecialSetVibration(int id, int largeMotor, int smallMotor)
		{
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x0001110A File Offset: 0x0000F30A
		public Vector3 PS4Input_AimGetLastAcceleration(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x0001110A File Offset: 0x0000F30A
		public Vector3 PS4Input_AimGetLastGyro(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x00011111 File Offset: 0x0000F311
		public Vector4 PS4Input_AimGetLastOrientation(int id)
		{
			return Vector4.zero;
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x000039A2 File Offset: 0x00001BA2
		public int PS4Input_AimGetUsersHandles(int maxNumberControllers, int[] handles)
		{
			return 0;
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x000039A2 File Offset: 0x00001BA2
		public int PS4Input_AimGetUsersHandles2(int maxNumberControllers, int[] handles)
		{
			return 0;
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x000039A2 File Offset: 0x00001BA2
		public bool PS4Input_AimIsConnected(int id)
		{
			return false;
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_AimResetLightSphere(int id)
		{
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_AimResetOrientation(int id)
		{
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_AimSetAngularVelocityDeadbandState(int id, bool bEnable)
		{
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_AimSetLightSphere(int id, int red, int green, int blue)
		{
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_AimSetMotionSensorState(int id, bool bEnable)
		{
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_AimSetTiltCorrectionState(int id, bool bEnable)
		{
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00002229 File Offset: 0x00000429
		public void PS4Input_AimSetVibration(int id, int largeMotor, int smallMotor)
		{
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x0001110A File Offset: 0x0000F30A
		public Vector3 PS4Input_GetLastMoveAcceleration(int id, int index)
		{
			return Vector3.zero;
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x0001110A File Offset: 0x0000F30A
		public Vector3 PS4Input_GetLastMoveGyro(int id, int index)
		{
			return Vector3.zero;
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x000039A2 File Offset: 0x00001BA2
		public int PS4Input_MoveGetButtons(int id, int index)
		{
			return 0;
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x000039A2 File Offset: 0x00001BA2
		public int PS4Input_MoveGetAnalogButton(int id, int index)
		{
			return 0;
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x000039A2 File Offset: 0x00001BA2
		public bool PS4Input_MoveIsConnected(int id, int index)
		{
			return false;
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x000039A2 File Offset: 0x00001BA2
		public int PS4Input_MoveGetUsersMoveHandles(int maxNumberControllers, int[] primaryHandles, int[] secondaryHandles)
		{
			return 0;
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x000039A2 File Offset: 0x00001BA2
		public int PS4Input_MoveGetUsersMoveHandles(int maxNumberControllers, int[] primaryHandles)
		{
			return 0;
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x000039A2 File Offset: 0x00001BA2
		public int PS4Input_MoveGetUsersMoveHandles(int maxNumberControllers)
		{
			return 0;
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00011153 File Offset: 0x0000F353
		public IntPtr PS4Input_MoveGetControllerInputForTracking()
		{
			return IntPtr.Zero;
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x000039A2 File Offset: 0x00001BA2
		public int PS4Input_MoveSetLightSphere(int id, int index, int red, int green, int blue)
		{
			return 0;
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x000039A2 File Offset: 0x00001BA2
		public int PS4Input_MoveSetVibration(int id, int index, int motor)
		{
			return 0;
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x0001115A File Offset: 0x0000F35A
		public void GetDeviceVIDPIDs(out List<int> vids, out List<int> pids)
		{
			vids = new List<int>();
			pids = new List<int>();
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x00011150 File Offset: 0x0000F350
		public int GetAndroidAPILevel()
		{
			return -1;
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x0001116A File Offset: 0x0000F36A
		public bool UnityUI_Graphic_GetRaycastTarget(object graphic)
		{
			return !(graphic as Graphic == null) && (graphic as Graphic).raycastTarget;
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x00011187 File Offset: 0x0000F387
		public void UnityUI_Graphic_SetRaycastTarget(object graphic, bool value)
		{
			if (graphic as Graphic == null)
			{
				return;
			}
			(graphic as Graphic).raycastTarget = value;
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x000111A4 File Offset: 0x0000F3A4
		public bool UnityInput_IsTouchPressureSupported
		{
			get
			{
				return Input.touchPressureSupported;
			}
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x000111AB File Offset: 0x0000F3AB
		public float UnityInput_GetTouchPressure(ref Touch touch)
		{
			return touch.pressure;
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x000111B3 File Offset: 0x0000F3B3
		public float UnityInput_GetTouchMaximumPossiblePressure(ref Touch touch)
		{
			return touch.maximumPossiblePressure;
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x000111BB File Offset: 0x0000F3BB
		public IControllerTemplate CreateControllerTemplate(Guid typeGuid, object payload)
		{
			return ControllerTemplateFactory.Create(typeGuid, payload);
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x000111C4 File Offset: 0x0000F3C4
		public Type[] GetControllerTemplateTypes()
		{
			return ControllerTemplateFactory.templateTypes;
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x000111CB File Offset: 0x0000F3CB
		public Type[] GetControllerTemplateInterfaceTypes()
		{
			return ControllerTemplateFactory.templateInterfaceTypes;
		}

		// Token: 0x04001AFA RID: 6906
		private static Func<object> _getPlatformInitializerDelegate;

		// Token: 0x04001AFB RID: 6907
		private bool _isEditorPaused;

		// Token: 0x04001AFC RID: 6908
		private Action<bool> _EditorPausedStateChangedEvent;
	}
}
