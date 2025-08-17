using System;
using UnityEngine;

[ExecuteInEditMode]
public class ProjectileTest : MonoBehaviour
{
	// Token: 0x06000A69 RID: 2665 RVA: 0x0003D18C File Offset: 0x0003B38C
	[ContextMenu("Launch")]
	public void LaunchProjectile()
	{
		Vector3 vector;
		if (PhysUtil.SolveProjectileVelocity(this.target.position - base.transform.position, this.projectileSpeed, out vector, 1f))
		{
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.projectile, base.transform.position, Quaternion.identity);
			gameObject.SetActive(true);
			gameObject.GetComponent<Rigidbody>().velocity = vector;
			return;
		}
		Debug.Log("Impossible projectile");
	}

	public GameObject projectile;

	public float projectileSpeed = 30f;

	public Transform target;
}
