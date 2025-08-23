using System;
using System.Collections.Generic;
using Rewired;
using RewiredConsts;
using UnityEngine;
using UnityEngine.Events;

public class InputHelper : MonoBehaviour
{
	// (get) Token: 0x060007AF RID: 1967 RVA: 0x00007A53 File Offset: 0x00005C53
	// (set) Token: 0x060007B0 RID: 1968 RVA: 0x00007A71 File Offset: 0x00005C71
	public static InputHelper i
	{
		get
		{
			if (InputHelper.instance == null)
			{
				InputHelper.instance = global::UnityEngine.Object.FindObjectOfType<InputHelper>();
			}
			return InputHelper.instance;
		}
		set
		{
			InputHelper.instance = value;
		}
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x00002229 File Offset: 0x00000429
	private void Awake()
	{
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x00007A79 File Offset: 0x00005C79
	private void OnEnable()
	{
		InputHelper.i = this;
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x00035C70 File Offset: 0x00033E70
	private void Start()
	{
		this.rePlayer = ReInput.players.GetPlayer(0);
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnAnyInput), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed);
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnAnyInput), UpdateLoopType.Update, InputActionEventType.NegativeButtonJustPressed);
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x00035CC4 File Offset: 0x00033EC4
	private void OnAnyInput(InputActionEventData obj)
	{
		if (this.ignoreActions.Contains(obj.actionId))
		{
			return;
		}
		if (!obj.IsCurrentInputSource(InputHelper.lastActiveControllerType, InputHelper.lastActiveControllerID))
		{
			switch (InputHelper.lastActiveControllerType)
			{
			case ControllerType.Keyboard:
				if (obj.IsCurrentInputSource(ControllerType.Mouse))
				{
					return;
				}
				break;
			case ControllerType.Mouse:
				if (obj.IsCurrentInputSource(ControllerType.Keyboard))
				{
					return;
				}
				break;
			case ControllerType.Joystick:
				if (obj.IsCurrentInputSource(ControllerType.Keyboard))
				{
					InputHelper.lastActiveControllerID = 0;
					InputHelper.lastActiveControllerType = ControllerType.Keyboard;
					InputHelper.onLastActiveControllerChanged.Invoke();
					return;
				}
				if (obj.IsCurrentInputSource(ControllerType.Mouse))
				{
					InputHelper.lastActiveControllerID = 0;
					InputHelper.lastActiveControllerType = ControllerType.Mouse;
					InputHelper.onLastActiveControllerChanged.Invoke();
					return;
				}
				break;
			}
			foreach (Joystick joystick in this.rePlayer.controllers.Joysticks)
			{
				if (obj.IsCurrentInputSource(ControllerType.Joystick, joystick.id))
				{
					InputHelper.lastActiveControllerID = joystick.id;
					InputHelper.lastActiveControllerType = ControllerType.Joystick;
					InputHelper.onLastActiveControllerChanged.Invoke();
					break;
				}
			}
		}
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x00007A81 File Offset: 0x00005C81
	public void UpdateInput()
	{
		InputHelper.onLastActiveControllerChanged.Invoke();
	}

	public static ControllerType lastActiveControllerType = ControllerType.Keyboard;

	public static int lastActiveControllerID = 0;

	public static UnityEvent onLastActiveControllerChanged = new UnityEvent();

	private static InputHelper instance;

	public static int inputMode = 1;

	private Guid controllerGuid;

	public static List<UIButtonObject> activeButtonObjects = new List<UIButtonObject>();

	public static List<UIButtonDisplay> activeButtonDisplays = new List<UIButtonDisplay>();

	private global::Rewired.Player rePlayer;

	[ActionIdProperty(typeof(global::RewiredConsts.Action))]
	public int[] ignoreActions;

	private Guid dualShock2 = new Guid("c3ad3cad-c7cf-4ca8-8c2e-e3df8d9960bb");

	private Guid dualShock3 = new Guid("71dfe6c8-9e81-428f-a58e-c7e664b7fbed");

	private Guid dualShock4 = new Guid("cd9718bf-a87a-44bc-8716-60a0def28a9f");

	private Guid dualSense = new Guid("5286706d-19b4-4a45-b635-207ce78d8394");
}
