using System;
using UnityEngine;

namespace Rewired.Demos
{
	[AddComponentMenu("")]
	[RequireComponent(typeof(CharacterController))]
	public class EightPlayersExample_Player : MonoBehaviour
	{
		// Token: 0x06001DB0 RID: 7600 RVA: 0x00016B02 File Offset: 0x00014D02
		private void Awake()
		{
			this.cc = base.GetComponent<CharacterController>();
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x00016B10 File Offset: 0x00014D10
		private void Initialize()
		{
			this.player = ReInput.players.GetPlayer(this.playerId);
			this.initialized = true;
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x00016B2F File Offset: 0x00014D2F
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

		// Token: 0x06001DB3 RID: 7603 RVA: 0x00074B54 File Offset: 0x00072D54
		private void GetInput()
		{
			this.moveVector.x = this.player.GetAxis("Move Horizontal");
			this.moveVector.y = this.player.GetAxis("Move Vertical");
			this.fire = this.player.GetButtonDown("Fire");
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x00074BB0 File Offset: 0x00072DB0
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

		private Player player;

		private CharacterController cc;

		private Vector3 moveVector;

		private bool fire;

		[NonSerialized]
		private bool initialized;
	}
}
