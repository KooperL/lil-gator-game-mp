using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x02000496 RID: 1174
	[AddComponentMenu("")]
	[RequireComponent(typeof(CharacterController))]
	public class CustomControllerDemo_Player : MonoBehaviour
	{
		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x000164ED File Offset: 0x000146ED
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

		// Token: 0x06001D2A RID: 7466 RVA: 0x00016513 File Offset: 0x00014713
		private void Awake()
		{
			this.cc = base.GetComponent<CharacterController>();
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x0007226C File Offset: 0x0007046C
		private void Update()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			Vector2 vector;
			vector..ctor(this.player.GetAxis("Move Horizontal"), this.player.GetAxis("Move Vertical"));
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

		// Token: 0x04001E8F RID: 7823
		public int playerId;

		// Token: 0x04001E90 RID: 7824
		public float speed = 1f;

		// Token: 0x04001E91 RID: 7825
		public float bulletSpeed = 20f;

		// Token: 0x04001E92 RID: 7826
		public GameObject bulletPrefab;

		// Token: 0x04001E93 RID: 7827
		private Player _player;

		// Token: 0x04001E94 RID: 7828
		private CharacterController cc;
	}
}
