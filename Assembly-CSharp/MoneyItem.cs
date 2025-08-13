using System;
using UnityEngine;

// Token: 0x020001F3 RID: 499
public class MoneyItem : PersistentObject
{
	// Token: 0x0600093D RID: 2365 RVA: 0x0000901D File Offset: 0x0000721D
	private void OnTriggerEnter(Collider other)
	{
		this.Collect();
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x00009025 File Offset: 0x00007225
	private void Collect()
	{
		this.SaveTrue();
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000BEC RID: 3052
	public int cents = 1;
}
