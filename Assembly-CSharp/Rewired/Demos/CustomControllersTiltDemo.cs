using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x02000337 RID: 823
	[AddComponentMenu("")]
	public class CustomControllersTiltDemo : MonoBehaviour
	{
		// Token: 0x06001732 RID: 5938 RVA: 0x000626A4 File Offset: 0x000608A4
		private void Awake()
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			this.player = ReInput.players.GetPlayer(0);
			ReInput.InputSourceUpdateEvent += this.OnInputUpdate;
			this.controller = (CustomController)this.player.controllers.GetControllerWithTag(ControllerType.Custom, "TiltController");
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x000626FC File Offset: 0x000608FC
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

		// Token: 0x06001734 RID: 5940 RVA: 0x00062784 File Offset: 0x00060984
		private void OnInputUpdate()
		{
			Vector3 acceleration = Input.acceleration;
			this.controller.SetAxisValue(0, acceleration.x);
			this.controller.SetAxisValue(1, acceleration.y);
			this.controller.SetAxisValue(2, acceleration.z);
		}

		// Token: 0x0400190E RID: 6414
		public Transform target;

		// Token: 0x0400190F RID: 6415
		public float speed = 10f;

		// Token: 0x04001910 RID: 6416
		private CustomController controller;

		// Token: 0x04001911 RID: 6417
		private Player player;
	}
}
