using System;
using UnityEngine;

public class ItemShieldPaint : ItemShield
{
	// Token: 0x06000BBC RID: 3004 RVA: 0x0000AF70 File Offset: 0x00009170
	private void Start()
	{
		this.spawnDistance = global::UnityEngine.Random.Range(this.minSpawnDistance, this.maxSpawnDistance);
	}

	// Token: 0x06000BBD RID: 3005 RVA: 0x000414C4 File Offset: 0x0003F6C4
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
			this.spawnDistance = global::UnityEngine.Random.Range(this.minSpawnDistance, this.maxSpawnDistance);
		}
	}

	public SpawnPaintSplat paintSplat;

	public float minSpawnDistance = 0.25f;

	public float maxSpawnDistance = 2f;

	private float spawnDistance = 1f;

	private float distanceCounter;
}
