using System;
using UnityEngine;

namespace Rewired.Demos
{
	[AddComponentMenu("")]
	[RequireComponent(typeof(CharacterController))]
	public class CustomControllerDemo_Player : MonoBehaviour
	{
		// (get) Token: 0x06001D89 RID: 7561 RVA: 0x00016923 File Offset: 0x00014B23
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

		// Token: 0x06001D8A RID: 7562 RVA: 0x00016949 File Offset: 0x00014B49
		private void Awake()
		{
			this.cc = base.GetComponent<CharacterController>();
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x00074218 File Offset: 0x00072418
		private void Update()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			Vector2 vector = new Vector2(this.player.GetAxis("Move Horizontal"), this.player.GetAxis("Move Vertical"));
			this.cc.Move(vector * this.speed * Time.deltaTime);
			if (this.player.GetButtonDown("Fire"))
			{
				Vector3 vector2 = Vector3.Scale(new Vector3(1f, 0f, 0f), base.transform.right);
				global::UnityEngine.Object.Instantiate<GameObject>(this.bulletPrefab, base.transform.position + vector2, Quaternion.identity).GetComponent<Rigidbody>().velocity = new Vector3(this.bulletSpeed * base.transform.right.x, 0f, 0f);
			}
			if (this.player.GetButtonDown("Change Color"))
			{
				Renderer component = base.GetComponent<Renderer>();
				Material material = component.material;
				material.color = new Color(global::UnityEngine.Random.Range(0f, 1f), global::UnityEngine.Random.Range(0f, 1f), global::UnityEngine.Random.Range(0f, 1f), 1f);
				component.material = material;
			}
		}

		public int playerId;

		public float speed = 1f;

		public float bulletSpeed = 20f;

		public GameObject bulletPrefab;

		private Player _player;

		private CharacterController cc;
	}
}
