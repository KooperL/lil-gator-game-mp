using System;
using UnityEngine;

// Token: 0x020001F5 RID: 501
public class MoneySpawner : MonoBehaviour
{
	// Token: 0x06000945 RID: 2373 RVA: 0x000090DD File Offset: 0x000072DD
	private void Start()
	{
		if (this.spawnOnAwake)
		{
			this.SpawnMoney();
		}
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x000090ED File Offset: 0x000072ED
	public void SpawnMoney()
	{
		int num = this.cents;
		if (this.minCents != 0)
		{
			Random.Range(this.minCents, this.cents);
		}
	}

	// Token: 0x04000BF7 RID: 3063
	public int cents;

	// Token: 0x04000BF8 RID: 3064
	public int minCents;

	// Token: 0x04000BF9 RID: 3065
	public bool spawnOnAwake = true;

	// Token: 0x04000BFA RID: 3066
	public Vector3 offset;
}
