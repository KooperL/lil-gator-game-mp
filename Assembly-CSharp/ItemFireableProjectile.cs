using System;
using UnityEngine;

public class ItemFireableProjectile : ItemFireable
{
	// Token: 0x06000B4D RID: 2893 RVA: 0x0000AA9C File Offset: 0x00008C9C
	public override Vector3 GetSpawnPoint()
	{
		return this.projectileLaunchPoint.position;
	}

	// Token: 0x06000B4E RID: 2894 RVA: 0x00040830 File Offset: 0x0003EA30
	public override void Fire(Vector3 direction)
	{
		GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.projectile);
		gameObject.transform.position = this.projectileLaunchPoint.position;
		gameObject.transform.rotation = this.projectileLaunchPoint.rotation;
		gameObject.GetComponent<Rigidbody>().velocity = this.GetSpeed(1f) * direction;
		base.Fire(direction);
	}

	public GameObject projectile;

	public Transform projectileLaunchPoint;
}
