using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x02000495 RID: 1173
	[AddComponentMenu("")]
	public class CustomControllerDemo : MonoBehaviour
	{
		// Token: 0x06001D1E RID: 7454 RVA: 0x00016463 File Offset: 0x00014663
		private void Awake()
		{
			if (SystemInfo.deviceType == 1 && Screen.orientation != 3)
			{
				Screen.orientation = 3;
			}
			this.Initialize();
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x00072030 File Offset: 0x00070230
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

		// Token: 0x06001D20 RID: 7456 RVA: 0x00016481 File Offset: 0x00014681
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

		// Token: 0x06001D21 RID: 7457 RVA: 0x00016499 File Offset: 0x00014699
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

		// Token: 0x06001D22 RID: 7458 RVA: 0x0007216C File Offset: 0x0007036C
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

		// Token: 0x06001D23 RID: 7459 RVA: 0x000721CC File Offset: 0x000703CC
		private void GetSourceButtonValues()
		{
			for (int i = 0; i < this.buttonValues.Length; i++)
			{
				this.buttonValues[i] = this.buttons[i].isPressed;
			}
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x00072204 File Offset: 0x00070404
		private void SetControllerAxisValues()
		{
			for (int i = 0; i < this.axisValues.Length; i++)
			{
				this.controller.SetAxisValue(i, this.axisValues[i]);
			}
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x00072238 File Offset: 0x00070438
		private void SetControllerButtonValues()
		{
			for (int i = 0; i < this.buttonValues.Length; i++)
			{
				this.controller.SetButtonValue(i, this.buttonValues[i]);
			}
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x000164BB File Offset: 0x000146BB
		private float GetAxisValueCallback(int index)
		{
			if (index >= this.axisValues.Length)
			{
				return 0f;
			}
			return this.axisValues[index];
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x000164D6 File Offset: 0x000146D6
		private bool GetButtonValueCallback(int index)
		{
			return index < this.buttonValues.Length && this.buttonValues[index];
		}

		// Token: 0x04001E84 RID: 7812
		public int playerId;

		// Token: 0x04001E85 RID: 7813
		public string controllerTag;

		// Token: 0x04001E86 RID: 7814
		public bool useUpdateCallbacks;

		// Token: 0x04001E87 RID: 7815
		private int buttonCount;

		// Token: 0x04001E88 RID: 7816
		private int axisCount;

		// Token: 0x04001E89 RID: 7817
		private float[] axisValues;

		// Token: 0x04001E8A RID: 7818
		private bool[] buttonValues;

		// Token: 0x04001E8B RID: 7819
		private TouchJoystickExample[] joysticks;

		// Token: 0x04001E8C RID: 7820
		private TouchButtonExample[] buttons;

		// Token: 0x04001E8D RID: 7821
		private CustomController controller;

		// Token: 0x04001E8E RID: 7822
		[NonSerialized]
		private bool initialized;
	}
}
