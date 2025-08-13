using System;
using UnityEngine;

// Token: 0x0200018D RID: 397
public class Breakable : PersistentObject, IHit
{
	// Token: 0x06000777 RID: 1911 RVA: 0x000077B0 File Offset: 0x000059B0
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

	// Token: 0x06000778 RID: 1912 RVA: 0x000077EE File Offset: 0x000059EE
	public void SpawnMoney()
	{
		int num = this.cents;
		if (this.minCents != 0)
		{
			Random.Range(this.minCents, this.cents);
		}
	}

	// Token: 0x040009EA RID: 2538
	public GameObject broken;

	// Token: 0x040009EB RID: 2539
	public int cents;

	// Token: 0x040009EC RID: 2540
	public int minCents;

	// Token: 0x040009ED RID: 2541
	public Vector3 offset;
}
