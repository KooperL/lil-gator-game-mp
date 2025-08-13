using System;
using UnityEngine;

// Token: 0x02000132 RID: 306
public class Breakable : PersistentObject, IHit
{
	// Token: 0x06000652 RID: 1618 RVA: 0x00020929 File Offset: 0x0001EB29
	public void Hit(Vector3 velocity, bool isHeavy = false)
	{
		Object.Instantiate<GameObject>(this.broken, base.transform.position, Quaternion.identity);
		if (this.cents > 0)
		{
			this.SpawnMoney();
		}
		this.SaveTrue();
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x00020967 File Offset: 0x0001EB67
	public void SpawnMoney()
	{
		int num = this.cents;
		if (this.minCents != 0)
		{
			Random.Range(this.minCents, this.cents);
		}
	}

	// Token: 0x04000880 RID: 2176
	public GameObject broken;

	// Token: 0x04000881 RID: 2177
	public int cents;

	// Token: 0x04000882 RID: 2178
	public int minCents;

	// Token: 0x04000883 RID: 2179
	public Vector3 offset;
}
