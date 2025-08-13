using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x02000345 RID: 837
	[AddComponentMenu("")]
	[RequireComponent(typeof(CharacterController))]
	public class PressStartToJoinExample_GamePlayer : MonoBehaviour
	{
		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x00064881 File Offset: 0x00062A81
		private Player player
		{
			get
			{
				return PressStartToJoinExample_Assigner.GetRewiredPlayer(this.gamePlayerId);
			}
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0006488E File Offset: 0x00062A8E
		private void OnEnable()
		{
			this.cc = base.GetComponent<CharacterController>();
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0006489C File Offset: 0x00062A9C
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

		// Token: 0x060017A4 RID: 6052 RVA: 0x000648BC File Offset: 0x00062ABC
		private void GetInput()
		{
			this.moveVector.x = this.player.GetAxis("Move Horizontal");
			this.moveVector.y = this.player.GetAxis("Move Vertical");
			this.fire = this.player.GetButtonDown("Fire");
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00064918 File Offset: 0x00062B18
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

		// Token: 0x04001966 RID: 6502
		public int gamePlayerId;

		// Token: 0x04001967 RID: 6503
		public float moveSpeed = 3f;

		// Token: 0x04001968 RID: 6504
		public float bulletSpeed = 15f;

		// Token: 0x04001969 RID: 6505
		public GameObject bulletPrefab;

		// Token: 0x0400196A RID: 6506
		private CharacterController cc;

		// Token: 0x0400196B RID: 6507
		private Vector3 moveVector;

		// Token: 0x0400196C RID: 6508
		private bool fire;
	}
}
