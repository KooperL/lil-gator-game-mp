using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x02000338 RID: 824
	[AddComponentMenu("")]
	public class CustomControllerDemo : MonoBehaviour
	{
		// Token: 0x06001736 RID: 5942 RVA: 0x000627E0 File Offset: 0x000609E0
		private void Awake()
		{
			if (SystemInfo.deviceType == DeviceType.Handheld && Screen.orientation != ScreenOrientation.LandscapeLeft)
			{
				Screen.orientation = ScreenOrientation.LandscapeLeft;
			}
			this.Initialize();
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x00062800 File Offset: 0x00060A00
		private void Initialize()
		{
			ReInput.InputSourceUpdateEvent += this.OnInputSourceUpdate;
			this.joysticks = base.GetComponentsInChildren<TouchJoystickExample>();
			this.buttons = base.GetComponentsInChildren<TouchButtonExample>();
			this.axisCount = this.joysticks.Length * 2;
			this.buttonCount = this.buttons.Length;
			this.axisValues = new float[this.axisCount];
			this.buttonValues = new bool[this.buttonCount];
			Player player = ReInput.players.GetPlayer(this.playerId);
			this.controller = player.controllers.GetControllerWithTag<CustomController>(this.controllerTag);
			if (this.controller == null)
			{
				Debug.LogError("A matching controller was not found for tag \"" + this.controllerTag + "\"");
			}
			if (this.controller.buttonCount != this.buttonValues.Length || this.controller.axisCount != this.axisValues.Length)
			{
				Debug.LogError("Controller has wrong number of elements!");
			}
			if (this.useUpdateCallbacks && this.controller != null)
			{
				this.controller.SetAxisUpdateCallback(new Func<int, float>(this.GetAxisValueCallback));
				this.controller.SetButtonUpdateCallback(new Func<int, bool>(this.GetButtonValueCallback));
			}
			this.initialized = true;
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x00062939 File Offset: 0x00060B39
		private void Update()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			if (!this.initialized)
			{
				this.Initialize();
			}
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x00062951 File Offset: 0x00060B51
		private void OnInputSourceUpdate()
		{
			this.GetSourceAxisValues();
			this.GetSourceButtonValues();
			if (!this.useUpdateCallbacks)
			{
				this.SetControllerAxisValues();
				this.SetControllerButtonValues();
			}
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x00062974 File Offset: 0x00060B74
		private void GetSourceAxisValues()
		{
			for (int i = 0; i < this.axisValues.Length; i++)
			{
				if (i % 2 != 0)
				{
					this.axisValues[i] = this.joysticks[i / 2].position.y;
				}
				else
				{
					this.axisValues[i] = this.joysticks[i / 2].position.x;
				}
			}
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x000629D4 File Offset: 0x00060BD4
		private void GetSourceButtonValues()
		{
			for (int i = 0; i < this.buttonValues.Length; i++)
			{
				this.buttonValues[i] = this.buttons[i].isPressed;
			}
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00062A0C File Offset: 0x00060C0C
		private void SetControllerAxisValues()
		{
			for (int i = 0; i < this.axisValues.Length; i++)
			{
				this.controller.SetAxisValue(i, this.axisValues[i]);
			}
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00062A40 File Offset: 0x00060C40
		private void SetControllerButtonValues()
		{
			for (int i = 0; i < this.buttonValues.Length; i++)
			{
				this.controller.SetButtonValue(i, this.buttonValues[i]);
			}
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x00062A74 File Offset: 0x00060C74
		private float GetAxisValueCallback(int index)
		{
			if (index >= this.axisValues.Length)
			{
				return 0f;
			}
			return this.axisValues[index];
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00062A8F File Offset: 0x00060C8F
		private bool GetButtonValueCallback(int index)
		{
			return index < this.buttonValues.Length && this.buttonValues[index];
		}

		// Token: 0x04001912 RID: 6418
		public int playerId;

		// Token: 0x04001913 RID: 6419
		public string controllerTag;

		// Token: 0x04001914 RID: 6420
		public bool useUpdateCallbacks;

		// Token: 0x04001915 RID: 6421
		private int buttonCount;

		// Token: 0x04001916 RID: 6422
		private int axisCount;

		// Token: 0x04001917 RID: 6423
		private float[] axisValues;

		// Token: 0x04001918 RID: 6424
		private bool[] buttonValues;

		// Token: 0x04001919 RID: 6425
		private TouchJoystickExample[] joysticks;

		// Token: 0x0400191A RID: 6426
		private TouchButtonExample[] buttons;

		// Token: 0x0400191B RID: 6427
		private CustomController controller;

		// Token: 0x0400191C RID: 6428
		[NonSerialized]
		private bool initialized;
	}
}
