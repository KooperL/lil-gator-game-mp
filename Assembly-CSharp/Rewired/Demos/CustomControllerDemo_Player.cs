using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x02000339 RID: 825
	[AddComponentMenu("")]
	[RequireComponent(typeof(CharacterController))]
	public class CustomControllerDemo_Player : MonoBehaviour
	{
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001741 RID: 5953 RVA: 0x00062AAE File Offset: 0x00060CAE
		private Player player
		{
			get
			{
				if (this._player == null)
				{
					this._player = ReInput.players.GetPlayer(this.playerId);
				}
				return this._player;
			}
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x00062AD4 File Offset: 0x00060CD4
		private void Awake()
		{
			this.cc = base.GetComponent<CharacterController>();
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x00062AE4 File Offset: 0x00060CE4
		private void Update()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			Vector2 vector = new Vector2(this.player.GetAxis("Move Horizontal"), this.player.GetAxis("Move Vertical"));
			this.cc.Move(vector * this.speed * Time.deltaTime);
			if (this.player.GetButtonDown("Fire"))
			{
				Vector3 vector2 = Vector3.Scale(new Vector3(1f, 0f, 0f), base.transform.right);
				Object.Instantiate<GameObject>(this.bulletPrefab, base.transform.position + vector2, Quaternion.identity).GetComponent<Rigidbody>().velocity = new Vector3(this.bulletSpeed * base.transform.right.x, 0f, 0f);
			}
			if (this.player.GetButtonDown("Change Color"))
			{
				Renderer component = base.GetComponent<Renderer>();
				Material material = component.material;
				material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
				component.material = material;
			}
		}

		// Token: 0x0400191D RID: 6429
		public int playerId;

		// Token: 0x0400191E RID: 6430
		public float speed = 1f;

		// Token: 0x0400191F RID: 6431
		public float bulletSpeed = 20f;

		// Token: 0x04001920 RID: 6432
		public GameObject bulletPrefab;

		// Token: 0x04001921 RID: 6433
		private Player _player;

		// Token: 0x04001922 RID: 6434
		private CharacterController cc;
	}
}
