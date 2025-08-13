using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x020004A5 RID: 1189
	[AddComponentMenu("")]
	[RequireComponent(typeof(CharacterController))]
	public class PressStartToJoinExample_GamePlayer : MonoBehaviour
	{
		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001D8D RID: 7565 RVA: 0x00016955 File Offset: 0x00014B55
		private Player player
		{
			get
			{
				return PressStartToJoinExample_Assigner.GetRewiredPlayer(this.gamePlayerId);
			}
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x00016962 File Offset: 0x00014B62
		private void OnEnable()
		{
			this.cc = base.GetComponent<CharacterController>();
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x00016970 File Offset: 0x00014B70
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

		// Token: 0x06001D90 RID: 7568 RVA: 0x00073BD8 File Offset: 0x00071DD8
		private void GetInput()
		{
			this.moveVector.x = this.player.GetAxis("Move Horizontal");
			this.moveVector.y = this.player.GetAxis("Move Vertical");
			this.fire = this.player.GetButtonDown("Fire");
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x00073C34 File Offset: 0x00071E34
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

		// Token: 0x04001EDD RID: 7901
		public int gamePlayerId;

		// Token: 0x04001EDE RID: 7902
		public float moveSpeed = 3f;

		// Token: 0x04001EDF RID: 7903
		public float bulletSpeed = 15f;

		// Token: 0x04001EE0 RID: 7904
		public GameObject bulletPrefab;

		// Token: 0x04001EE1 RID: 7905
		private CharacterController cc;

		// Token: 0x04001EE2 RID: 7906
		private Vector3 moveVector;

		// Token: 0x04001EE3 RID: 7907
		private bool fire;
	}
}
