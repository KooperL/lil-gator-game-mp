using System;
using UnityEngine;

namespace Rewired.Demos
{
	[AddComponentMenu("")]
	public class Bullet : MonoBehaviour
	{
		// Token: 0x060017A7 RID: 6055 RVA: 0x000649E8 File Offset: 0x00062BE8
		private void Start()
		{
			if (this.lifeTime > 0f)
			{
				this.deathTime = Time.time + this.lifeTime;
				this.die = true;
			}
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x00064A10 File Offset: 0x00062C10
		private void Update()
		{
			if (this.die && Time.time >= this.deathTime)
			{
				Object.Destroy(base.gameObject);
			}
		}

		public float lifeTime = 3f;

		private bool die;

		private float deathTime;
	}
}
