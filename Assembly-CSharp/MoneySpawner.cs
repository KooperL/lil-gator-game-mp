using System;
using UnityEngine;

public class MoneySpawner : MonoBehaviour
{
	// Token: 0x06000986 RID: 2438 RVA: 0x000093F9 File Offset: 0x000075F9
	private void Start()
	{
		if (this.spawnOnAwake)
		{
			this.SpawnMoney();
		}
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x00009409 File Offset: 0x00007609
	public void SpawnMoney()
	{
		int num = this.cents;
		if (this.minCents != 0)
		{
			global::UnityEngine.Random.Range(this.minCents, this.cents);
		}
	}

	public int cents;

	public int minCents;

	public bool spawnOnAwake = true;

	public Vector3 offset;
}
