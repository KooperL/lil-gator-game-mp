using System;
using UnityEngine;

// Token: 0x020001D4 RID: 468
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

	// Token: 0x04000C0E RID: 3086
	public SpawnPaintSplat paintSplat;

	// Token: 0x04000C0F RID: 3087
	public float minSpawnDistance = 0.25f;

	// Token: 0x04000C10 RID: 3088
	public float maxSpawnDistance = 2f;

	// Token: 0x04000C11 RID: 3089
	private float spawnDistance = 1f;

	// Token: 0x04000C12 RID: 3090
	private float distanceCounter;
}
