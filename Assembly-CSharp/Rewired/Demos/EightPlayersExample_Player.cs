using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x0200049C RID: 1180
	[AddComponentMenu("")]
	[RequireComponent(typeof(CharacterController))]
	public class EightPlayersExample_Player : MonoBehaviour
	{
		// Token: 0x06001D50 RID: 7504 RVA: 0x000166CC File Offset: 0x000148CC
		private void Awake()
		{
			this.cc = base.GetComponent<CharacterController>();
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x000166DA File Offset: 0x000148DA
		private void Initialize()
		{
			this.player = ReInput.players.GetPlayer(this.playerId);
			this.initialized = true;
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x000166F9 File Offset: 0x000148F9
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

		// Token: 0x06001D53 RID: 7507 RVA: 0x00072BA8 File Offset: 0x00070DA8
		private void GetInput()
		{
			this.moveVector.x = this.player.GetAxis("Move Horizontal");
			this.moveVector.y = this.player.GetAxis("Move Vertical");
			this.fire = this.player.GetButtonDown("Fire");
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x00072C04 File Offset: 0x00070E04
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

		// Token: 0x04001EAC RID: 7852
		public int playerId;

		// Token: 0x04001EAD RID: 7853
		public float moveSpeed = 3f;

		// Token: 0x04001EAE RID: 7854
		public float bulletSpeed = 15f;

		// Token: 0x04001EAF RID: 7855
		public GameObject bulletPrefab;

		// Token: 0x04001EB0 RID: 7856
		private Player player;

		// Token: 0x04001EB1 RID: 7857
		private CharacterController cc;

		// Token: 0x04001EB2 RID: 7858
		private Vector3 moveVector;

		// Token: 0x04001EB3 RID: 7859
		private bool fire;

		// Token: 0x04001EB4 RID: 7860
		[NonSerialized]
		private bool initialized;
	}
}
