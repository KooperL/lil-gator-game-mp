using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x020004A2 RID: 1186
	[AddComponentMenu("")]
	[RequireComponent(typeof(CharacterController))]
	public class PressAnyButtonToJoinExample_GamePlayer : MonoBehaviour
	{
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x000168B7 File Offset: 0x00014AB7
		private Player player
		{
			get
			{
				if (!ReInput.isReady)
				{
					return null;
				}
				return ReInput.players.GetPlayer(this.playerId);
			}
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x000168D2 File Offset: 0x00014AD2
		private void OnEnable()
		{
			this.cc = base.GetComponent<CharacterController>();
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x000168E0 File Offset: 0x00014AE0
		private void Update()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			if (this.player == null)
			{
				return;
			}
			this.GetInput();
			this.ProcessInput();
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x00073944 File Offset: 0x00071B44
		private void GetInput()
		{
			this.moveVector.x = this.player.GetAxis("Move Horizontal");
			this.moveVector.y = this.player.GetAxis("Move Vertical");
			this.fire = this.player.GetButtonDown("Fire");
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x000739A0 File Offset: 0x00071BA0
		private void ProcessInput()
		{
			if (this.moveVector.x != 0f || this.moveVector.y != 0f)
			{
				this.cc.Move(this.moveVector * this.moveSpeed * Time.deltaTime);
			}
			if (this.fire)
			{
				Object.Instantiate<GameObject>(this.bulletPrefab, base.transform.position + base.transform.right, base.transform.rotation).GetComponent<Rigidbody>().AddForce(base.transform.right * this.bulletSpeed, 2);
			}
		}

		// Token: 0x04001ED0 RID: 7888
		public int playerId;

		// Token: 0x04001ED1 RID: 7889
		public float moveSpeed = 3f;

		// Token: 0x04001ED2 RID: 7890
		public float bulletSpeed = 15f;

		// Token: 0x04001ED3 RID: 7891
		public GameObject bulletPrefab;

		// Token: 0x04001ED4 RID: 7892
		private CharacterController cc;

		// Token: 0x04001ED5 RID: 7893
		private Vector3 moveVector;

		// Token: 0x04001ED6 RID: 7894
		private bool fire;
	}
}
