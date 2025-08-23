using System;
using UnityEngine;

namespace Rewired.Demos
{
	[AddComponentMenu("")]
	public class CustomControllersTiltDemo : MonoBehaviour
	{
		// Token: 0x06001D7B RID: 7547 RVA: 0x00074178 File Offset: 0x00072378
		private void Awake()
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			this.player = ReInput.players.GetPlayer(0);
			ReInput.InputSourceUpdateEvent += this.OnInputUpdate;
			this.controller = (CustomController)this.player.controllers.GetControllerWithTag(ControllerType.Custom, "TiltController");
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x000741D0 File Offset: 0x000723D0
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

		// Token: 0x06001D7D RID: 7549 RVA: 0x00074258 File Offset: 0x00072458
		private void OnInputUpdate()
		{
			Vector3 acceleration = Input.acceleration;
			this.controller.SetAxisValue(0, acceleration.x);
			this.controller.SetAxisValue(1, acceleration.y);
			this.controller.SetAxisValue(2, acceleration.z);
		}

		public Transform target;

		public float speed = 10f;

		private CustomController controller;

		private Player player;
	}
}
