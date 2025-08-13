using System;
using UnityEngine;

// Token: 0x0200017D RID: 381
public class MoneyItem : PersistentObject
{
	// Token: 0x060007DB RID: 2011 RVA: 0x0002643E File Offset: 0x0002463E
	private void OnTriggerEnter(Collider other)
	{
		this.Collect();
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x00026446 File Offset: 0x00024646
	private void Collect()
	{
		this.SaveTrue();
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000A10 RID: 2576
	public int cents = 1;
}
