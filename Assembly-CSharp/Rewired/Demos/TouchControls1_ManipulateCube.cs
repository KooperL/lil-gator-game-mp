using System;
using UnityEngine;

namespace Rewired.Demos
{
	[AddComponentMenu("")]
	public class TouchControls1_ManipulateCube : MonoBehaviour
	{
		// Token: 0x06001E32 RID: 7730 RVA: 0x00076A48 File Offset: 0x00074C48
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
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMoveReceivedX), 0, 33, "Horizontal");
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMoveReceivedX), 0, 34, "Horizontal");
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMoveReceivedY), 0, 33, "Vertical");
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMoveReceivedY), 0, 34, "Vertical");
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnCycleColor), 0, 3, "CycleColor");
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnCycleColorReverse), 0, 3, "CycleColorReverse");
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnRotationReceivedX), 0, 33, "RotateHorizontal");
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnRotationReceivedX), 0, 34, "RotateHorizontal");
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnRotationReceivedY), 0, 33, "RotateVertical");
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnRotationReceivedY), 0, 34, "RotateVertical");
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x00076B70 File Offset: 0x00074D70
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

		// Token: 0x06001E34 RID: 7732 RVA: 0x00017125 File Offset: 0x00015325
		private void OnMoveReceivedX(InputActionEventData data)
		{
			this.OnMoveReceived(new Vector2(data.GetAxis(), 0f));
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x0001713E File Offset: 0x0001533E
		private void OnMoveReceivedY(InputActionEventData data)
		{
			this.OnMoveReceived(new Vector2(0f, data.GetAxis()));
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x00017157 File Offset: 0x00015357
		private void OnRotationReceivedX(InputActionEventData data)
		{
			this.OnRotationReceived(new Vector2(data.GetAxis(), 0f));
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x00017170 File Offset: 0x00015370
		private void OnRotationReceivedY(InputActionEventData data)
		{
			this.OnRotationReceived(new Vector2(0f, data.GetAxis()));
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x00017189 File Offset: 0x00015389
		private void OnCycleColor(InputActionEventData data)
		{
			this.OnCycleColor();
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x00017191 File Offset: 0x00015391
		private void OnCycleColorReverse(InputActionEventData data)
		{
			this.OnCycleColorReverse();
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x00017199 File Offset: 0x00015399
		private void OnMoveReceived(Vector2 move)
		{
			base.transform.Translate(move * Time.deltaTime * this.moveSpeed, Space.World);
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x000171C2 File Offset: 0x000153C2
		private void OnRotationReceived(Vector2 rotate)
		{
			rotate *= this.rotateSpeed;
			base.transform.Rotate(Vector3.up, -rotate.x, Space.World);
			base.transform.Rotate(Vector3.right, rotate.y, Space.World);
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x00076C04 File Offset: 0x00074E04
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

		// Token: 0x06001E3D RID: 7741 RVA: 0x00076C64 File Offset: 0x00074E64
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
