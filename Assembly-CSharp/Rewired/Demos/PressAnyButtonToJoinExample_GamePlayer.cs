using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x02000343 RID: 835
	[AddComponentMenu("")]
	[RequireComponent(typeof(CharacterController))]
	public class PressAnyButtonToJoinExample_GamePlayer : MonoBehaviour
	{
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x00064565 File Offset: 0x00062765
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

		// Token: 0x06001796 RID: 6038 RVA: 0x00064580 File Offset: 0x00062780
		private void OnEnable()
		{
			this.cc = base.GetComponent<CharacterController>();
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x0006458E File Offset: 0x0006278E
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

		// Token: 0x06001798 RID: 6040 RVA: 0x000645B0 File Offset: 0x000627B0
		private void GetInput()
		{
			this.moveVector.x = this.player.GetAxis("Move Horizontal");
			this.moveVector.y = this.player.GetAxis("Move Vertical");
			this.fire = this.player.GetButtonDown("Fire");
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0006460C File Offset: 0x0006280C
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

		// Token: 0x0400195B RID: 6491
		public int playerId;

		// Token: 0x0400195C RID: 6492
		public float moveSpeed = 3f;

		// Token: 0x0400195D RID: 6493
		public float bulletSpeed = 15f;

		// Token: 0x0400195E RID: 6494
		public GameObject bulletPrefab;

		// Token: 0x0400195F RID: 6495
		private CharacterController cc;

		// Token: 0x04001960 RID: 6496
		private Vector3 moveVector;

		// Token: 0x04001961 RID: 6497
		private bool fire;
	}
}
