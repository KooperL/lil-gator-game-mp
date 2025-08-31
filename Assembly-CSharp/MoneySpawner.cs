using System;
using UnityEngine;

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

	public int cents;

	public int minCents;

	public bool spawnOnAwake = true;

	public Vector3 offset;
}
