using System;
using UnityEngine;

// Token: 0x0200024B RID: 587
public class ItemFireableProjectile : ItemFireable
{
	// Token: 0x06000B00 RID: 2816 RVA: 0x0000A75E File Offset: 0x0000895E
	public override Vector3 GetSpawnPoint()
	{
		return this.projectileLaunchPoint.position;
	}

	// Token: 0x06000B01 RID: 2817 RVA: 0x0003EA60 File Offset: 0x0003CC60
	public override void Fire(Vector3 direction)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.projectile);
		gameObject.transform.position = this.projectileLaunchPoint.position;
		gameObject.transform.rotation = this.projectileLaunchPoint.rotation;
		gameObject.GetComponent<Rigidbody>().velocity = this.GetSpeed(1f) * direction;
		base.Fire(direction);
	}

	// Token: 0x04000DFF RID: 3583
	public GameObject projectile;

	// Token: 0x04000E00 RID: 3584
	public Transform projectileLaunchPoint;
}
