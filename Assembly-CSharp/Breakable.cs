using System;
using UnityEngine;

public class Breakable : PersistentObject, IHit
{
	// Token: 0x060007B7 RID: 1975 RVA: 0x00007ABF File Offset: 0x00005CBF
	public void Hit(Vector3 velocity, bool isHeavy = false)
	{
		global::UnityEngine.Object.Instantiate<GameObject>(this.broken, base.transform.position, Quaternion.identity);
		if (this.cents > 0)
		{
			this.SpawnMoney();
		}
		this.SaveTrue();
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x00007AFD File Offset: 0x00005CFD
	public void SpawnMoney()
	{
		int num = this.cents;
		if (this.minCents != 0)
		{
			global::UnityEngine.Random.Range(this.minCents, this.cents);
		}
	}

	public GameObject broken;

	public int cents;

	public int minCents;

	public Vector3 offset;
}
