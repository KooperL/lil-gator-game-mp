using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x0200034A RID: 842
	[AddComponentMenu("")]
	public class TouchControls1_ManipulateCube : MonoBehaviour
	{
		// Token: 0x060017CE RID: 6094 RVA: 0x000657F4 File Offset: 0x000639F4
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

		// Token: 0x060017CF RID: 6095 RVA: 0x0006591C File Offset: 0x00063B1C
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

		// Token: 0x060017D0 RID: 6096 RVA: 0x000659AD File Offset: 0x00063BAD
		private void OnMoveReceivedX(InputActionEventData data)
		{
			this.OnMoveReceived(new Vector2(data.GetAxis(), 0f));
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x000659C6 File Offset: 0x00063BC6
		private void OnMoveReceivedY(InputActionEventData data)
		{
			this.OnMoveReceived(new Vector2(0f, data.GetAxis()));
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x000659DF File Offset: 0x00063BDF
		private void OnRotationReceivedX(InputActionEventData data)
		{
			this.OnRotationReceived(new Vector2(data.GetAxis(), 0f));
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x000659F8 File Offset: 0x00063BF8
		private void OnRotationReceivedY(InputActionEventData data)
		{
			this.OnRotationReceived(new Vector2(0f, data.GetAxis()));
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x00065A11 File Offset: 0x00063C11
		private void OnCycleColor(InputActionEventData data)
		{
			this.OnCycleColor();
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x00065A19 File Offset: 0x00063C19
		private void OnCycleColorReverse(InputActionEventData data)
		{
			this.OnCycleColorReverse();
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x00065A21 File Offset: 0x00063C21
		private void OnMoveReceived(Vector2 move)
		{
			base.transform.Translate(move * Time.deltaTime * this.moveSpeed, Space.World);
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x00065A4A File Offset: 0x00063C4A
		private void OnRotationReceived(Vector2 rotate)
		{
			rotate *= this.rotateSpeed;
			base.transform.Rotate(Vector3.up, -rotate.x, Space.World);
			base.transform.Rotate(Vector3.right, rotate.y, Space.World);
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00065A8C File Offset: 0x00063C8C
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

		// Token: 0x060017D9 RID: 6105 RVA: 0x00065AEC File Offset: 0x00063CEC
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

		// Token: 0x0400198C RID: 6540
		public float rotateSpeed = 1f;

		// Token: 0x0400198D RID: 6541
		public float moveSpeed = 1f;

		// Token: 0x0400198E RID: 6542
		private int currentColorIndex;

		// Token: 0x0400198F RID: 6543
		private Color[] colors = new Color[]
		{
			Color.white,
			Color.red,
			Color.green,
			Color.blue
		};
	}
}
