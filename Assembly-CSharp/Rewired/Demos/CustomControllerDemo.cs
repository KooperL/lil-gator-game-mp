using System;
using UnityEngine;

namespace Rewired.Demos
{
	[AddComponentMenu("")]
	public class CustomControllerDemo : MonoBehaviour
	{
		// Token: 0x06001D7E RID: 7550 RVA: 0x00016899 File Offset: 0x00014A99
		private void Awake()
		{
			if (SystemInfo.deviceType == DeviceType.Handheld && Screen.orientation != ScreenOrientation.LandscapeLeft)
			{
				Screen.orientation = ScreenOrientation.LandscapeLeft;
			}
			this.Initialize();
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x00073FDC File Offset: 0x000721DC
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

		// Token: 0x06001D80 RID: 7552 RVA: 0x000168B7 File Offset: 0x00014AB7
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

		// Token: 0x06001D81 RID: 7553 RVA: 0x000168CF File Offset: 0x00014ACF
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

		// Token: 0x06001D82 RID: 7554 RVA: 0x00074118 File Offset: 0x00072318
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

		// Token: 0x06001D83 RID: 7555 RVA: 0x00074178 File Offset: 0x00072378
		private void GetSourceButtonValues()
		{
			for (int i = 0; i < this.buttonValues.Length; i++)
			{
				this.buttonValues[i] = this.buttons[i].isPressed;
			}
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x000741B0 File Offset: 0x000723B0
		private void SetControllerAxisValues()
		{
			for (int i = 0; i < this.axisValues.Length; i++)
			{
				this.controller.SetAxisValue(i, this.axisValues[i]);
			}
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x000741E4 File Offset: 0x000723E4
		private void SetControllerButtonValues()
		{
			for (int i = 0; i < this.buttonValues.Length; i++)
			{
				this.controller.SetButtonValue(i, this.buttonValues[i]);
			}
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x000168F1 File Offset: 0x00014AF1
		private float GetAxisValueCallback(int index)
		{
			if (index >= this.axisValues.Length)
			{
				return 0f;
			}
			return this.axisValues[index];
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x0001690C File Offset: 0x00014B0C
		private bool GetButtonValueCallback(int index)
		{
			return index < this.buttonValues.Length && this.buttonValues[index];
		}

		public int playerId;

		public string controllerTag;

		public bool useUpdateCallbacks;

		private int buttonCount;

		private int axisCount;

		private float[] axisValues;

		private bool[] buttonValues;

		private TouchJoystickExample[] joysticks;

		private TouchButtonExample[] buttons;

		private CustomController controller;

		[NonSerialized]
		private bool initialized;
	}
}
