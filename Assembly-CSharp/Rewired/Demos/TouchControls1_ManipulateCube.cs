using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x020004B2 RID: 1202
	[AddComponentMenu("")]
	public class TouchControls1_ManipulateCube : MonoBehaviour
	{
		// Token: 0x06001DD2 RID: 7634 RVA: 0x00074A9C File Offset: 0x00072C9C
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

		// Token: 0x06001DD3 RID: 7635 RVA: 0x00074BC4 File Offset: 0x00072DC4
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

		// Token: 0x06001DD4 RID: 7636 RVA: 0x00016CEF File Offset: 0x00014EEF
		private void OnMoveReceivedX(InputActionEventData data)
		{
			this.OnMoveReceived(new Vector2(data.GetAxis(), 0f));
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x00016D08 File Offset: 0x00014F08
		private void OnMoveReceivedY(InputActionEventData data)
		{
			this.OnMoveReceived(new Vector2(0f, data.GetAxis()));
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x00016D21 File Offset: 0x00014F21
		private void OnRotationReceivedX(InputActionEventData data)
		{
			this.OnRotationReceived(new Vector2(data.GetAxis(), 0f));
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x00016D3A File Offset: 0x00014F3A
		private void OnRotationReceivedY(InputActionEventData data)
		{
			this.OnRotationReceived(new Vector2(0f, data.GetAxis()));
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x00016D53 File Offset: 0x00014F53
		private void OnCycleColor(InputActionEventData data)
		{
			this.OnCycleColor();
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x00016D5B File Offset: 0x00014F5B
		private void OnCycleColorReverse(InputActionEventData data)
		{
			this.OnCycleColorReverse();
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x00016D63 File Offset: 0x00014F63
		private void OnMoveReceived(Vector2 move)
		{
			base.transform.Translate(move * Time.deltaTime * this.moveSpeed, 0);
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x00016D8C File Offset: 0x00014F8C
		private void OnRotationReceived(Vector2 rotate)
		{
			rotate *= this.rotateSpeed;
			base.transform.Rotate(Vector3.up, -rotate.x, 0);
			base.transform.Rotate(Vector3.right, rotate.y, 0);
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x00074C58 File Offset: 0x00072E58
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

		// Token: 0x06001DDD RID: 7645 RVA: 0x00074CB8 File Offset: 0x00072EB8
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

		// Token: 0x04001F22 RID: 7970
		public float rotateSpeed = 1f;

		// Token: 0x04001F23 RID: 7971
		public float moveSpeed = 1f;

		// Token: 0x04001F24 RID: 7972
		private int currentColorIndex;

		// Token: 0x04001F25 RID: 7973
		private Color[] colors = new Color[]
		{
			Color.white,
			Color.red,
			Color.green,
			Color.blue
		};
	}
}
