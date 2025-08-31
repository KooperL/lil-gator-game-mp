using System;
using UnityEngine;

public class ItemShieldPaint : ItemShield
{
	// Token: 0x060009C6 RID: 2502 RVA: 0x0002D743 File Offset: 0x0002B943
	private void Start()
	{
		this.spawnDistance = Random.Range(this.minSpawnDistance, this.maxSpawnDistance);
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x0002D75C File Offset: 0x0002B95C
	public override void WhileSledding()
	{
		if (Player.movement.HasGroundContact)
		{
			this.distanceCounter += Time.deltaTime * Player.movement.velocity.magnitude;
		}
		if (this.distanceCounter > this.spawnDistance)
		{
			this.distanceCounter -= this.spawnDistance;
			this.paintSplat.Spawn();
			this.spawnDistance = Random.Range(this.minSpawnDistance, this.maxSpawnDistance);
		}
	}

	public SpawnPaintSplat paintSplat;

	public float minSpawnDistance = 0.25f;

	public float maxSpawnDistance = 2f;

	private float spawnDistance = 1f;

	private float distanceCounter;
}
