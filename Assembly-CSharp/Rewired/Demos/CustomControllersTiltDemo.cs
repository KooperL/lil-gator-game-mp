using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x02000494 RID: 1172
	[AddComponentMenu("")]
	public class CustomControllersTiltDemo : MonoBehaviour
	{
		// Token: 0x06001D1A RID: 7450 RVA: 0x00071F04 File Offset: 0x00070104
		private void Awake()
		{
			Screen.orientation = 3;
			this.player = ReInput.players.GetPlayer(0);
			ReInput.InputSourceUpdateEvent += this.OnInputUpdate;
			this.controller = (CustomController)this.player.controllers.GetControllerWithTag(20, "TiltController");
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x00071F5C File Offset: 0x0007015C
		private void Update()
		{
			if (this.target == null)
			{
				return;
			}
			Vector3 vector = Vector3.zero;
			vector.y = this.player.GetAxis("Tilt Vertical");
			vector.x = this.player.GetAxis("Tilt Horizontal");
			if (vector.sqrMagnitude > 1f)
			{
				vector.Normalize();
			}
			vector *= Time.deltaTime;
			this.target.Translate(vector * this.speed);
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x00071FE4 File Offset: 0x000701E4
		private void OnInputUpdate()
		{
			Vector3 acceleration = Input.acceleration;
			this.controller.SetAxisValue(0, acceleration.x);
			this.controller.SetAxisValue(1, acceleration.y);
			this.controller.SetAxisValue(2, acceleration.z);
		}

		// Token: 0x04001E80 RID: 7808
		public Transform target;

		// Token: 0x04001E81 RID: 7809
		public float speed = 10f;

		// Token: 0x04001E82 RID: 7810
		private CustomController controller;

		// Token: 0x04001E83 RID: 7811
		private Player player;
	}
}
