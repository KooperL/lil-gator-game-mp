using System;
using UnityEngine;

public class MoneyItem : PersistentObject
{
	// Token: 0x0600097E RID: 2430 RVA: 0x0000934E File Offset: 0x0000754E
	private void OnTriggerEnter(Collider other)
	{
		this.Collect();
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x00009356 File Offset: 0x00007556
	private void Collect()
	{
		this.SaveTrue();
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	public int cents = 1;
}
