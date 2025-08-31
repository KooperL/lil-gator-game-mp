using System;
using UnityEngine;

public class ItemFireableProjectile : ItemFireable
{
	// Token: 0x06000969 RID: 2409 RVA: 0x0002CA8B File Offset: 0x0002AC8B
	public override Vector3 GetSpawnPoint()
	{
		return this.projectileLaunchPoint.position;
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x0002CA98 File Offset: 0x0002AC98
	public override void Fire(Vector3 direction)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.projectile);
		gameObject.transform.position = this.projectileLaunchPoint.position;
		gameObject.transform.rotation = this.projectileLaunchPoint.rotation;
		gameObject.GetComponent<Rigidbody>().velocity = this.GetSpeed(1f) * direction;
		base.Fire(direction);
	}

	public GameObject projectile;

	public Transform projectileLaunchPoint;
}
