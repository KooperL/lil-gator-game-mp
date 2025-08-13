using System;
using System.Collections.Generic;
using Rewired;
using RewiredConsts;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200018C RID: 396
public class InputHelper : MonoBehaviour
{
	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x0600076E RID: 1902 RVA: 0x00007744 File Offset: 0x00005944
	// (set) Token: 0x0600076F RID: 1903 RVA: 0x00007762 File Offset: 0x00005962
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

	// Token: 0x06000770 RID: 1904 RVA: 0x00002229 File Offset: 0x00000429
	private void Awake()
	{
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x0000776A File Offset: 0x0000596A
	private void OnEnable()
	{
		InputHelper.i = this;
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x00034040 File Offset: 0x00032240
	private void Start()
	{
		this.rePlayer = ReInput.players.GetPlayer(0);
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnAnyInput), 0, 3);
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnAnyInput), 0, 19);
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x00034094 File Offset: 0x00032294
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
			case 0:
				if (obj.IsCurrentInputSource(1))
				{
					return;
				}
				break;
			case 1:
				if (obj.IsCurrentInputSource(0))
				{
					return;
				}
				break;
			case 2:
				if (obj.IsCurrentInputSource(0))
				{
					InputHelper.lastActiveControllerID = 0;
					InputHelper.lastActiveControllerType = 0;
					InputHelper.onLastActiveControllerChanged.Invoke();
					return;
				}
				if (obj.IsCurrentInputSource(1))
				{
					InputHelper.lastActiveControllerID = 0;
					InputHelper.lastActiveControllerType = 1;
					InputHelper.onLastActiveControllerChanged.Invoke();
					return;
				}
				break;
			}
			foreach (Joystick joystick in this.rePlayer.controllers.Joysticks)
			{
				if (obj.IsCurrentInputSource(2, joystick.id))
				{
					InputHelper.lastActiveControllerID = joystick.id;
					InputHelper.lastActiveControllerType = 2;
					InputHelper.onLastActiveControllerChanged.Invoke();
					break;
				}
			}
		}
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x00007772 File Offset: 0x00005972
	public void UpdateInput()
	{
		InputHelper.onLastActiveControllerChanged.Invoke();
	}

	// Token: 0x040009DC RID: 2524
	public static ControllerType lastActiveControllerType = 0;

	// Token: 0x040009DD RID: 2525
	public static int lastActiveControllerID = 0;

	// Token: 0x040009DE RID: 2526
	public static UnityEvent onLastActiveControllerChanged = new UnityEvent();

	// Token: 0x040009DF RID: 2527
	private static InputHelper instance;

	// Token: 0x040009E0 RID: 2528
	public static int inputMode = 1;

	// Token: 0x040009E1 RID: 2529
	private Guid controllerGuid;

	// Token: 0x040009E2 RID: 2530
	public static List<UIButtonObject> activeButtonObjects = new List<UIButtonObject>();

	// Token: 0x040009E3 RID: 2531
	public static List<UIButtonDisplay> activeButtonDisplays = new List<UIButtonDisplay>();

	// Token: 0x040009E4 RID: 2532
	private global::Rewired.Player rePlayer;

	// Token: 0x040009E5 RID: 2533
	[ActionIdProperty(typeof(global::RewiredConsts.Action))]
	public int[] ignoreActions;

	// Token: 0x040009E6 RID: 2534
	private Guid dualShock2 = new Guid("c3ad3cad-c7cf-4ca8-8c2e-e3df8d9960bb");

	// Token: 0x040009E7 RID: 2535
	private Guid dualShock3 = new Guid("71dfe6c8-9e81-428f-a58e-c7e664b7fbed");

	// Token: 0x040009E8 RID: 2536
	private Guid dualShock4 = new Guid("cd9718bf-a87a-44bc-8716-60a0def28a9f");

	// Token: 0x040009E9 RID: 2537
	private Guid dualSense = new Guid("5286706d-19b4-4a45-b635-207ce78d8394");
}
