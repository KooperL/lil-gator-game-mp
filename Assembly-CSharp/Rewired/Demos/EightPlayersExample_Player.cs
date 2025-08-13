using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x0200033D RID: 829
	[AddComponentMenu("")]
	[RequireComponent(typeof(CharacterController))]
	public class EightPlayersExample_Player : MonoBehaviour
	{
		// Token: 0x06001765 RID: 5989 RVA: 0x000635C7 File Offset: 0x000617C7
		private void Awake()
		{
			this.cc = base.GetComponent<CharacterController>();
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x000635D5 File Offset: 0x000617D5
		private void Initialize()
		{
			this.player = ReInput.players.GetPlayer(this.playerId);
			this.initialized = true;
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x000635F4 File Offset: 0x000617F4
		private void Update()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			if (!this.initialized)
			{
				this.Initialize();
			}
			this.GetInput();
			this.ProcessInput();
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x00063618 File Offset: 0x00061818
		private void GetInput()
		{
			this.moveVector.x = this.player.GetAxis("Move Horizontal");
			this.moveVector.y = this.player.GetAxis("Move Vertical");
			this.fire = this.player.GetButtonDown("Fire");
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00063674 File Offset: 0x00061874
		private void ProcessInput()
		{
			if (this.moveVector.x != 0f || this.moveVector.y != 0f)
			{
				this.cc.Move(this.moveVector * this.moveSpeed * Time.deltaTime);
			}
			if (this.fire)
			{
				Object.Instantiate<GameObject>(this.bulletPrefab, base.transform.position + base.transform.right, base.transform.rotation).GetComponent<Rigidbody>().AddForce(base.transform.right * this.bulletSpeed, ForceMode.VelocityChange);
			}
		}

		// Token: 0x04001937 RID: 6455
		public int playerId;

		// Token: 0x04001938 RID: 6456
		public float moveSpeed = 3f;

		// Token: 0x04001939 RID: 6457
		public float bulletSpeed = 15f;

		// Token: 0x0400193A RID: 6458
		public GameObject bulletPrefab;

		// Token: 0x0400193B RID: 6459
		private Player player;

		// Token: 0x0400193C RID: 6460
		private CharacterController cc;

		// Token: 0x0400193D RID: 6461
		private Vector3 moveVector;

		// Token: 0x0400193E RID: 6462
		private bool fire;

		// Token: 0x0400193F RID: 6463
		[NonSerialized]
		private bool initialized;
	}
}
