using System;
using UnityEngine;

// Token: 0x0200021F RID: 543
[ExecuteInEditMode]
public class ProjectileTest : MonoBehaviour
{
	// Token: 0x06000A1F RID: 2591 RVA: 0x0003B6E0 File Offset: 0x000398E0
	[ContextMenu("Launch")]
	public void LaunchProjectile()
	{
		Vector3 vector;
		if (PhysUtil.SolveProjectileVelocity(this.target.position - base.transform.position, this.projectileSpeed, out vector, 1f))
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.projectile, base.transform.position, Quaternion.identity);
			gameObject.SetActive(true);
			gameObject.GetComponent<Rigidbody>().velocity = vector;
			return;
		}
		Debug.Log("Impossible projectile");
	}

	// Token: 0x04000CA0 RID: 3232
	public GameObject projectile;

	// Token: 0x04000CA1 RID: 3233
	public float projectileSpeed = 30f;

	// Token: 0x04000CA2 RID: 3234
	public Transform target;
}
