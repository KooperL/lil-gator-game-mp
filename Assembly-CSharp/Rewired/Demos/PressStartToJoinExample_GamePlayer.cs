using System;
using UnityEngine;

namespace Rewired.Demos
{
	[AddComponentMenu("")]
	[RequireComponent(typeof(CharacterController))]
	public class PressStartToJoinExample_GamePlayer : MonoBehaviour
	{
		// (get) Token: 0x06001DEE RID: 7662 RVA: 0x00016D95 File Offset: 0x00014F95
		private Player player
		{
			get
			{
				return PressStartToJoinExample_Assigner.GetRewiredPlayer(this.gamePlayerId);
			}
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x00016DA2 File Offset: 0x00014FA2
		private void OnEnable()
		{
			this.cc = base.GetComponent<CharacterController>();
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x00016DB0 File Offset: 0x00014FB0
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

		// Token: 0x06001DF1 RID: 7665 RVA: 0x00075E4C File Offset: 0x0007404C
		private void GetInput()
		{
			this.moveVector.x = this.player.GetAxis("Move Horizontal");
			this.moveVector.y = this.player.GetAxis("Move Vertical");
			this.fire = this.player.GetButtonDown("Fire");
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x00075EA8 File Offset: 0x000740A8
		private void ProcessInput()
		{
			if (this.moveVector.x != 0f || this.moveVector.y != 0f)
			{
				this.cc.Move(this.moveVector * this.moveSpeed * Time.deltaTime);
			}
			if (this.fire)
			{
				global::UnityEngine.Object.Instantiate<GameObject>(this.bulletPrefab, base.transform.position + base.transform.right, base.transform.rotation).GetComponent<Rigidbody>().AddForce(base.transform.right * this.bulletSpeed, ForceMode.VelocityChange);
			}
		}

		public int gamePlayerId;

		public float moveSpeed = 3f;

		public float bulletSpeed = 15f;

		public GameObject bulletPrefab;

		private CharacterController cc;

		private Vector3 moveVector;

		private bool fire;
	}
}
