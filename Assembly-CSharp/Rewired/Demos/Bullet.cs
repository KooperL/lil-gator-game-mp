using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x020004A6 RID: 1190
	[AddComponentMenu("")]
	public class Bullet : MonoBehaviour
	{
		// Token: 0x06001D93 RID: 7571 RVA: 0x000169AD File Offset: 0x00014BAD
		private void Start()
		{
			if (this.lifeTime > 0f)
			{
				this.deathTime = Time.time + this.lifeTime;
				this.die = true;
			}
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x000169D5 File Offset: 0x00014BD5
		private void Update()
		{
			if (this.die && Time.time >= this.deathTime)
			{
				Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x04001EE4 RID: 7908
		public float lifeTime = 3f;

		// Token: 0x04001EE5 RID: 7909
		private bool die;

		// Token: 0x04001EE6 RID: 7910
		private float deathTime;
	}
}
