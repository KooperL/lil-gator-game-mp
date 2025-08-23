using System;
using UnityEngine;

public class MoneyItem : PersistentObject
{
	// Token: 0x0600097F RID: 2431 RVA: 0x00009358 File Offset: 0x00007558
	private void OnTriggerEnter(Collider other)
	{
		this.Collect();
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x00009360 File Offset: 0x00007560
	private void Collect()
	{
		this.SaveTrue();
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	public int cents = 1;
}
