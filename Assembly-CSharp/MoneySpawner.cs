using System;
using UnityEngine;

// Token: 0x0200017F RID: 383
public class MoneySpawner : MonoBehaviour
{
	// Token: 0x060007E3 RID: 2019 RVA: 0x000264FE File Offset: 0x000246FE
	private void Start()
	{
		if (this.spawnOnAwake)
		{
			this.SpawnMoney();
		}
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x0002650E File Offset: 0x0002470E
	public void SpawnMoney()
	{
		int num = this.cents;
		if (this.minCents != 0)
		{
			Random.Range(this.minCents, this.cents);
		}
	}

	// Token: 0x04000A1B RID: 2587
	public int cents;

	// Token: 0x04000A1C RID: 2588
	public int minCents;

	// Token: 0x04000A1D RID: 2589
	public bool spawnOnAwake = true;

	// Token: 0x04000A1E RID: 2590
	public Vector3 offset;
}
