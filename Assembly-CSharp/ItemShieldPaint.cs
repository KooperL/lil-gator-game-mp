using System;
using UnityEngine;

public class ItemShieldPaint : ItemShield
{
	// Token: 0x06000BBB RID: 3003 RVA: 0x0000AF51 File Offset: 0x00009151
	private void Start()
	{
		this.spawnDistance = global::UnityEngine.Random.Range(this.minSpawnDistance, this.maxSpawnDistance);
	}

	// Token: 0x06000BBC RID: 3004 RVA: 0x00041068 File Offset: 0x0003F268
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
