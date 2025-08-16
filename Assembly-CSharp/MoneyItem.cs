using System;
using UnityEngine;

public class MoneyItem : PersistentObject
{
	// Token: 0x0600097E RID: 2430 RVA: 0x00009339 File Offset: 0x00007539
	private void OnTriggerEnter(Collider other)
	{
		this.Collect();
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x00009341 File Offset: 0x00007541
	private void Collect()
	{
		this.SaveTrue();
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	public int cents = 1;
}
