using System;
using UnityEngine;

// Token: 0x02000194 RID: 404
public class HitPhysics : MonoBehaviour, IHit
{
	// Token: 0x06000790 RID: 1936 RVA: 0x0000792F File Offset: 0x00005B2F
	private void OnValidate()
	{
		if (this.rigidbody == null)
		{
			this.rigidbody = base.GetComponentInParent<Rigidbody>();
		}
		if (this.collider == null)
		{
			this.collider = base.GetComponent<Collider>();
		}
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x00007965 File Offset: 0x00005B65
	private void Awake()
	{
		if (this.rigidbody == null)
		{
			this.rigidbody = base.GetComponent<Rigidbody>();
		}
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x000348E4 File Offset: 0x00032AE4
	public void Hit(Vector3 velocity, bool isHeavy = false)
	{
		if (this.randomizeForcePoint)
		{
			Bounds bounds = this.collider.bounds;
			Vector3 vector = bounds.center + bounds.extents.magnitude * Random.insideUnitSphere;
			this.rigidbody.AddForceAtPosition(this.forceMultiplier * velocity, vector, 1);
			return;
		}
		this.rigidbody.AddForce(this.forceMultiplier * velocity, 1);
	}

	// Token: 0x04000A10 RID: 2576
	public Rigidbody rigidbody;

	// Token: 0x04000A11 RID: 2577
	public Collider collider;

	// Token: 0x04000A12 RID: 2578
	public float forceMultiplier = 3f;

	// Token: 0x04000A13 RID: 2579
	public bool randomizeForcePoint = true;
}
