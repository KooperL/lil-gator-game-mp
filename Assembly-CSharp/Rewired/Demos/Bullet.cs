using System;
using UnityEngine;

namespace Rewired.Demos
{
	[AddComponentMenu("")]
	public class Bullet : MonoBehaviour
	{
		// Token: 0x06001DF3 RID: 7667 RVA: 0x00016DCE File Offset: 0x00014FCE
		private void Start()
		{
			if (this.lifeTime > 0f)
			{
				this.deathTime = Time.time + this.lifeTime;
				this.die = true;
			}
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x00016DF6 File Offset: 0x00014FF6
		private void Update()
		{
			if (this.die && Time.time >= this.deathTime)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		public float lifeTime = 3f;

		private bool die;

		private float deathTime;
	}
}
