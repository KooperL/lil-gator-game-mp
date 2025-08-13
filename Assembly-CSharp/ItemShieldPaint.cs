using System;
using UnityEngine;

// Token: 0x0200025B RID: 603
public class ItemShieldPaint : ItemShield
{
	// Token: 0x06000B6F RID: 2927 RVA: 0x0000AC5E File Offset: 0x00008E5E
	private void Start()
	{
		this.spawnDistance = Random.Range(this.minSpawnDistance, this.maxSpawnDistance);
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x0003F674 File Offset: 0x0003D874
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

	// Token: 0x04000E46 RID: 3654
	public SpawnPaintSplat paintSplat;

	// Token: 0x04000E47 RID: 3655
	public float minSpawnDistance = 0.25f;

	// Token: 0x04000E48 RID: 3656
	public float maxSpawnDistance = 2f;

	// Token: 0x04000E49 RID: 3657
	private float spawnDistance = 1f;

	// Token: 0x04000E4A RID: 3658
	private float distanceCounter;
}
