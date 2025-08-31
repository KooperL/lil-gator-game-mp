using System;
using UnityEngine;

[ExecuteInEditMode]
public class ProjectileTest : MonoBehaviour
{
	// Token: 0x0600089E RID: 2206 RVA: 0x00028BA4 File Offset: 0x00026DA4
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

	public GameObject projectile;

	public float projectileSpeed = 30f;

	public Transform target;
}
