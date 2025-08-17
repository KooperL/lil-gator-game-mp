using System;
using UnityEngine;

namespace Rewired.Demos
{
	[AddComponentMenu("")]
	[RequireComponent(typeof(CharacterController))]
	public class PressAnyButtonToJoinExample_GamePlayer : MonoBehaviour
	{
		// (get) Token: 0x06001DE0 RID: 7648 RVA: 0x00016CED File Offset: 0x00014EED
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

		// Token: 0x06001DE1 RID: 7649 RVA: 0x00016D08 File Offset: 0x00014F08
		private void OnEnable()
		{
			this.cc = base.GetComponent<CharacterController>();
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x00016D16 File Offset: 0x00014F16
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

		// Token: 0x06001DE3 RID: 7651 RVA: 0x000758F0 File Offset: 0x00073AF0
		private void GetInput()
		{
			this.moveVector.x = this.player.GetAxis("Move Horizontal");
			this.moveVector.y = this.player.GetAxis("Move Vertical");
			this.fire = this.player.GetButtonDown("Fire");
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x0007594C File Offset: 0x00073B4C
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

		public int playerId;

		public float moveSpeed = 3f;

		public float bulletSpeed = 15f;

		public GameObject bulletPrefab;

		private CharacterController cc;

		private Vector3 moveVector;

		private bool fire;
	}
}
