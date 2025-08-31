using System;
using System.Collections.Generic;
using Rewired;
using RewiredConsts;
using UnityEngine;
using UnityEngine.Events;

public class InputHelper : MonoBehaviour
{
	// (get) Token: 0x06000649 RID: 1609 RVA: 0x000206F5 File Offset: 0x0001E8F5
	// (set) Token: 0x0600064A RID: 1610 RVA: 0x00020713 File Offset: 0x0001E913
	public static InputHelper i
	{
		get
		{
			if (InputHelper.instance == null)
			{
				InputHelper.instance = Object.FindObjectOfType<InputHelper>();
			}
			return InputHelper.instance;
		}
		set
		{
			InputHelper.instance = value;
		}
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x0002071B File Offset: 0x0001E91B
	private void Awake()
	{
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x0002071D File Offset: 0x0001E91D
	private void OnEnable()
	{
		InputHelper.i = this;
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x00020728 File Offset: 0x0001E928
	private void Start()
	{
		this.rePlayer = ReInput.players.GetPlayer(0);
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnAnyInput), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed);
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnAnyInput), UpdateLoopType.Update, InputActionEventType.NegativeButtonJustPressed);
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x0002077C File Offset: 0x0001E97C
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

	// Token: 0x0600064F RID: 1615 RVA: 0x00020898 File Offset: 0x0001EA98
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

	[ActionIdProperty(typeof(Action))]
	public int[] ignoreActions;

	private Guid dualShock2 = new Guid("c3ad3cad-c7cf-4ca8-8c2e-e3df8d9960bb");

	private Guid dualShock3 = new Guid("71dfe6c8-9e81-428f-a58e-c7e664b7fbed");

	private Guid dualShock4 = new Guid("cd9718bf-a87a-44bc-8716-60a0def28a9f");

	private Guid dualSense = new Guid("5286706d-19b4-4a45-b635-207ce78d8394");
}
