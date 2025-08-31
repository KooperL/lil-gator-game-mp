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
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ExternalTools : IExternalTools
	{
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x0004E999 File Offset: 0x0004CB99
		// (set) Token: 0x0600124A RID: 4682 RVA: 0x0004E9A0 File Offset: 0x0004CBA0
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

		// Token: 0x0600124C RID: 4684 RVA: 0x0004E9B0 File Offset: 0x0004CBB0
		public void Destroy()
		{
		}

		// (get) Token: 0x0600124D RID: 4685 RVA: 0x0004E9B2 File Offset: 0x0004CBB2
		public bool isEditorPaused
		{
			get
			{
				return this._isEditorPaused;
			}
		}

		// (add) Token: 0x0600124E RID: 4686 RVA: 0x0004E9BA File Offset: 0x0004CBBA
		// (remove) Token: 0x0600124F RID: 4687 RVA: 0x0004E9D3 File Offset: 0x0004CBD3
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

		// Token: 0x06001250 RID: 4688 RVA: 0x0004E9EC File Offset: 0x0004CBEC
		public object GetPlatformInitializer()
		{
			return Main.GetPlatformInitializer();
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0004E9F3 File Offset: 0x0004CBF3
		public string GetFocusedEditorWindowTitle()
		{
			return string.Empty;
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0004E9FA File Offset: 0x0004CBFA
		public bool IsEditorSceneViewFocused()
		{
			return false;
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0004E9FD File Offset: 0x0004CBFD
		public bool LinuxInput_IsJoystickPreconfigured(string name)
		{
			return false;
		}

		// (add) Token: 0x06001254 RID: 4692 RVA: 0x0004EA00 File Offset: 0x0004CC00
		// (remove) Token: 0x06001255 RID: 4693 RVA: 0x0004EA38 File Offset: 0x0004CC38
		public event Action<uint, bool> XboxOneInput_OnGamepadStateChange;

		// Token: 0x06001256 RID: 4694 RVA: 0x0004EA6D File Offset: 0x0004CC6D
		public int XboxOneInput_GetUserIdForGamepad(uint id)
		{
			return 0;
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0004EA70 File Offset: 0x0004CC70
		public ulong XboxOneInput_GetControllerId(uint unityJoystickId)
		{
			return 0UL;
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0004EA74 File Offset: 0x0004CC74
		public bool XboxOneInput_IsGamepadActive(uint unityJoystickId)
		{
			return false;
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x0004EA77 File Offset: 0x0004CC77
		public string XboxOneInput_GetControllerType(ulong xboxControllerId)
		{
			return string.Empty;
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x0004EA7E File Offset: 0x0004CC7E
		public uint XboxOneInput_GetJoystickId(ulong xboxControllerId)
		{
			return 0U;
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0004EA81 File Offset: 0x0004CC81
		public void XboxOne_Gamepad_UpdatePlugin()
		{
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x0004EA83 File Offset: 0x0004CC83
		public bool XboxOne_Gamepad_SetGamepadVibration(ulong xboxOneJoystickId, float leftMotor, float rightMotor, float leftTriggerLevel, float rightTriggerLevel)
		{
			return false;
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0004EA86 File Offset: 0x0004CC86
		public void XboxOne_Gamepad_PulseVibrateMotor(ulong xboxOneJoystickId, int motorInt, float startLevel, float endLevel, ulong durationMS)
		{
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0004EA88 File Offset: 0x0004CC88
		public Vector3 PS4Input_GetLastAcceleration(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0004EA8F File Offset: 0x0004CC8F
		public Vector3 PS4Input_GetLastGyro(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0004EA96 File Offset: 0x0004CC96
		public Vector4 PS4Input_GetLastOrientation(int id)
		{
			return Vector4.zero;
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0004EA9D File Offset: 0x0004CC9D
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

		// Token: 0x06001262 RID: 4706 RVA: 0x0004EAB9 File Offset: 0x0004CCB9
		public void PS4Input_GetPadControllerInformation(int id, out float touchpixelDensity, out int touchResolutionX, out int touchResolutionY, out int analogDeadZoneLeft, out int analogDeadZoneright, out int connectionType)
		{
			touchpixelDensity = 0f;
			touchResolutionX = 0;
			touchResolutionY = 0;
			analogDeadZoneLeft = 0;
			analogDeadZoneright = 0;
			connectionType = 0;
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x0004EAD5 File Offset: 0x0004CCD5
		public void PS4Input_PadSetMotionSensorState(int id, bool bEnable)
		{
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x0004EAD7 File Offset: 0x0004CCD7
		public void PS4Input_PadSetTiltCorrectionState(int id, bool bEnable)
		{
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0004EAD9 File Offset: 0x0004CCD9
		public void PS4Input_PadSetAngularVelocityDeadbandState(int id, bool bEnable)
		{
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0004EADB File Offset: 0x0004CCDB
		public void PS4Input_PadSetLightBar(int id, int red, int green, int blue)
		{
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0004EADD File Offset: 0x0004CCDD
		public void PS4Input_PadResetLightBar(int id)
		{
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0004EADF File Offset: 0x0004CCDF
		public void PS4Input_PadSetVibration(int id, int largeMotor, int smallMotor)
		{
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x0004EAE1 File Offset: 0x0004CCE1
		public void PS4Input_PadResetOrientation(int id)
		{
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0004EAE3 File Offset: 0x0004CCE3
		public bool PS4Input_PadIsConnected(int id)
		{
			return false;
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0004EAE6 File Offset: 0x0004CCE6
		public void PS4Input_GetUsersDetails(int slot, object loggedInUser)
		{
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0004EAE8 File Offset: 0x0004CCE8
		public int PS4Input_GetDeviceClassForHandle(int handle)
		{
			return -1;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0004EAEB File Offset: 0x0004CCEB
		public string PS4Input_GetDeviceClassString(int intValue)
		{
			return null;
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0004EAEE File Offset: 0x0004CCEE
		public int PS4Input_PadGetUsersHandles2(int maxControllers, int[] handles)
		{
			return 0;
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0004EAF1 File Offset: 0x0004CCF1
		public void PS4Input_GetSpecialControllerInformation(int id, int padIndex, object controllerInformation)
		{
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0004EAF3 File Offset: 0x0004CCF3
		public Vector3 PS4Input_SpecialGetLastAcceleration(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0004EAFA File Offset: 0x0004CCFA
		public Vector3 PS4Input_SpecialGetLastGyro(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0004EB01 File Offset: 0x0004CD01
		public Vector4 PS4Input_SpecialGetLastOrientation(int id)
		{
			return Vector4.zero;
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0004EB08 File Offset: 0x0004CD08
		public int PS4Input_SpecialGetUsersHandles(int maxNumberControllers, int[] handles)
		{
			return 0;
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0004EB0B File Offset: 0x0004CD0B
		public int PS4Input_SpecialGetUsersHandles2(int maxNumberControllers, int[] handles)
		{
			return 0;
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x0004EB0E File Offset: 0x0004CD0E
		public bool PS4Input_SpecialIsConnected(int id)
		{
			return false;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0004EB11 File Offset: 0x0004CD11
		public void PS4Input_SpecialResetLightSphere(int id)
		{
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0004EB13 File Offset: 0x0004CD13
		public void PS4Input_SpecialResetOrientation(int id)
		{
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x0004EB15 File Offset: 0x0004CD15
		public void PS4Input_SpecialSetAngularVelocityDeadbandState(int id, bool bEnable)
		{
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x0004EB17 File Offset: 0x0004CD17
		public void PS4Input_SpecialSetLightSphere(int id, int red, int green, int blue)
		{
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0004EB19 File Offset: 0x0004CD19
		public void PS4Input_SpecialSetMotionSensorState(int id, bool bEnable)
		{
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x0004EB1B File Offset: 0x0004CD1B
		public void PS4Input_SpecialSetTiltCorrectionState(int id, bool bEnable)
		{
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x0004EB1D File Offset: 0x0004CD1D
		public void PS4Input_SpecialSetVibration(int id, int largeMotor, int smallMotor)
		{
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0004EB1F File Offset: 0x0004CD1F
		public Vector3 PS4Input_AimGetLastAcceleration(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x0004EB26 File Offset: 0x0004CD26
		public Vector3 PS4Input_AimGetLastGyro(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x0004EB2D File Offset: 0x0004CD2D
		public Vector4 PS4Input_AimGetLastOrientation(int id)
		{
			return Vector4.zero;
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x0004EB34 File Offset: 0x0004CD34
		public int PS4Input_AimGetUsersHandles(int maxNumberControllers, int[] handles)
		{
			return 0;
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x0004EB37 File Offset: 0x0004CD37
		public int PS4Input_AimGetUsersHandles2(int maxNumberControllers, int[] handles)
		{
			return 0;
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0004EB3A File Offset: 0x0004CD3A
		public bool PS4Input_AimIsConnected(int id)
		{
			return false;
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0004EB3D File Offset: 0x0004CD3D
		public void PS4Input_AimResetLightSphere(int id)
		{
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0004EB3F File Offset: 0x0004CD3F
		public void PS4Input_AimResetOrientation(int id)
		{
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0004EB41 File Offset: 0x0004CD41
		public void PS4Input_AimSetAngularVelocityDeadbandState(int id, bool bEnable)
		{
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0004EB43 File Offset: 0x0004CD43
		public void PS4Input_AimSetLightSphere(int id, int red, int green, int blue)
		{
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0004EB45 File Offset: 0x0004CD45
		public void PS4Input_AimSetMotionSensorState(int id, bool bEnable)
		{
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0004EB47 File Offset: 0x0004CD47
		public void PS4Input_AimSetTiltCorrectionState(int id, bool bEnable)
		{
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0004EB49 File Offset: 0x0004CD49
		public void PS4Input_AimSetVibration(int id, int largeMotor, int smallMotor)
		{
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0004EB4B File Offset: 0x0004CD4B
		public Vector3 PS4Input_GetLastMoveAcceleration(int id, int index)
		{
			return Vector3.zero;
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x0004EB52 File Offset: 0x0004CD52
		public Vector3 PS4Input_GetLastMoveGyro(int id, int index)
		{
			return Vector3.zero;
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x0004EB59 File Offset: 0x0004CD59
		public int PS4Input_MoveGetButtons(int id, int index)
		{
			return 0;
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0004EB5C File Offset: 0x0004CD5C
		public int PS4Input_MoveGetAnalogButton(int id, int index)
		{
			return 0;
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0004EB5F File Offset: 0x0004CD5F
		public bool PS4Input_MoveIsConnected(int id, int index)
		{
			return false;
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0004EB62 File Offset: 0x0004CD62
		public int PS4Input_MoveGetUsersMoveHandles(int maxNumberControllers, int[] primaryHandles, int[] secondaryHandles)
		{
			return 0;
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0004EB65 File Offset: 0x0004CD65
		public int PS4Input_MoveGetUsersMoveHandles(int maxNumberControllers, int[] primaryHandles)
		{
			return 0;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x0004EB68 File Offset: 0x0004CD68
		public int PS4Input_MoveGetUsersMoveHandles(int maxNumberControllers)
		{
			return 0;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0004EB6B File Offset: 0x0004CD6B
		public IntPtr PS4Input_MoveGetControllerInputForTracking()
		{
			return IntPtr.Zero;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0004EB72 File Offset: 0x0004CD72
		public int PS4Input_MoveSetLightSphere(int id, int index, int red, int green, int blue)
		{
			return 0;
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x0004EB75 File Offset: 0x0004CD75
		public int PS4Input_MoveSetVibration(int id, int index, int motor)
		{
			return 0;
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0004EB78 File Offset: 0x0004CD78
		public void GetDeviceVIDPIDs(out List<int> vids, out List<int> pids)
		{
			vids = new List<int>();
			pids = new List<int>();
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0004EB88 File Offset: 0x0004CD88
		public int GetAndroidAPILevel()
		{
			return -1;
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x0004EB8B File Offset: 0x0004CD8B
		public bool UnityUI_Graphic_GetRaycastTarget(object graphic)
		{
			return !(graphic as Graphic == null) && (graphic as Graphic).raycastTarget;
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x0004EBA8 File Offset: 0x0004CDA8
		public void UnityUI_Graphic_SetRaycastTarget(object graphic, bool value)
		{
			if (graphic as Graphic == null)
			{
				return;
			}
			(graphic as Graphic).raycastTarget = value;
		}

		// (get) Token: 0x06001299 RID: 4761 RVA: 0x0004EBC5 File Offset: 0x0004CDC5
		public bool UnityInput_IsTouchPressureSupported
		{
			get
			{
				return Input.touchPressureSupported;
			}
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0004EBCC File Offset: 0x0004CDCC
		public float UnityInput_GetTouchPressure(ref Touch touch)
		{
			return touch.pressure;
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0004EBD4 File Offset: 0x0004CDD4
		public float UnityInput_GetTouchMaximumPossiblePressure(ref Touch touch)
		{
			return touch.maximumPossiblePressure;
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0004EBDC File Offset: 0x0004CDDC
		public IControllerTemplate CreateControllerTemplate(Guid typeGuid, object payload)
		{
			return ControllerTemplateFactory.Create(typeGuid, payload);
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0004EBE5 File Offset: 0x0004CDE5
		public Type[] GetControllerTemplateTypes()
		{
			return ControllerTemplateFactory.templateTypes;
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0004EBEC File Offset: 0x0004CDEC
		public Type[] GetControllerTemplateInterfaceTypes()
		{
			return ControllerTemplateFactory.templateInterfaceTypes;
		}

		private static Func<object> _getPlatformInitializerDelegate;

		private bool _isEditorPaused;

		private Action<bool> _EditorPausedStateChangedEvent;
	}
}
