using System;
using UnityEngine;

namespace Rewired.Demos
{
	[AddComponentMenu("")]
	public class TouchControls1_ManipulateCube : MonoBehaviour
	{
		// Token: 0x06001E33 RID: 7731 RVA: 0x00076D10 File Offset: 0x00074F10
		private void OnEnable()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			Player player = ReInput.players.GetPlayer(0);
			if (player == null)
			{
				return;
			}
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMoveReceivedX), UpdateLoopType.Update, InputActionEventType.AxisActive, "Horizontal");
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMoveReceivedX), UpdateLoopType.Update, InputActionEventType.AxisInactive, "Horizontal");
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMoveReceivedY), UpdateLoopType.Update, InputActionEventType.AxisActive, "Vertical");
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMoveReceivedY), UpdateLoopType.Update, InputActionEventType.AxisInactive, "Vertical");
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnCycleColor), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "CycleColor");
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnCycleColorReverse), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "CycleColorReverse");
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnRotationReceivedX), UpdateLoopType.Update, InputActionEventType.AxisActive, "RotateHorizontal");
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnRotationReceivedX), UpdateLoopType.Update, InputActionEventType.AxisInactive, "RotateHorizontal");
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnRotationReceivedY), UpdateLoopType.Update, InputActionEventType.AxisActive, "RotateVertical");
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnRotationReceivedY), UpdateLoopType.Update, InputActionEventType.AxisInactive, "RotateVertical");
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x00076E38 File Offset: 0x00075038
		private void OnDisable()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			Player player = ReInput.players.GetPlayer(0);
			if (player == null)
			{
				return;
			}
			player.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnMoveReceivedX));
			player.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnMoveReceivedY));
			player.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnCycleColor));
			player.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnCycleColorReverse));
			player.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnRotationReceivedX));
			player.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnRotationReceivedY));
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x0001712F File Offset: 0x0001532F
		private void OnMoveReceivedX(InputActionEventData data)
		{
			this.OnMoveReceived(new Vector2(data.GetAxis(), 0f));
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x00017148 File Offset: 0x00015348
		private void OnMoveReceivedY(InputActionEventData data)
		{
			this.OnMoveReceived(new Vector2(0f, data.GetAxis()));
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x00017161 File Offset: 0x00015361
		private void OnRotationReceivedX(InputActionEventData data)
		{
			this.OnRotationReceived(new Vector2(data.GetAxis(), 0f));
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x0001717A File Offset: 0x0001537A
		private void OnRotationReceivedY(InputActionEventData data)
		{
			this.OnRotationReceived(new Vector2(0f, data.GetAxis()));
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x00017193 File Offset: 0x00015393
		private void OnCycleColor(InputActionEventData data)
		{
			this.OnCycleColor();
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x0001719B File Offset: 0x0001539B
		private void OnCycleColorReverse(InputActionEventData data)
		{
			this.OnCycleColorReverse();
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x000171A3 File Offset: 0x000153A3
		private void OnMoveReceived(Vector2 move)
		{
			base.transform.Translate(move * Time.deltaTime * this.moveSpeed, Space.World);
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x000171CC File Offset: 0x000153CC
		private void OnRotationReceived(Vector2 rotate)
		{
			rotate *= this.rotateSpeed;
			base.transform.Rotate(Vector3.up, -rotate.x, Space.World);
			base.transform.Rotate(Vector3.right, rotate.y, Space.World);
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x00076ECC File Offset: 0x000750CC
		private void OnCycleColor()
		{
			if (this.colors.Length == 0)
			{
				return;
			}
			this.currentColorIndex++;
			if (this.currentColorIndex >= this.colors.Length)
			{
				this.currentColorIndex = 0;
			}
			base.GetComponent<Renderer>().material.color = this.colors[this.currentColorIndex];
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x00076F2C File Offset: 0x0007512C
		private void OnCycleColorReverse()
		{
			if (this.colors.Length == 0)
			{
				return;
			}
			this.currentColorIndex--;
			if (this.currentColorIndex < 0)
			{
				this.currentColorIndex = this.colors.Length - 1;
			}
			base.GetComponent<Renderer>().material.color = this.colors[this.currentColorIndex];
		}

		public float rotateSpeed = 1f;

		public float moveSpeed = 1f;

		private int currentColorIndex;

		private Color[] colors = new Color[]
		{
			Color.white,
			Color.red,
			Color.green,
			Color.blue
		};
	}
}
