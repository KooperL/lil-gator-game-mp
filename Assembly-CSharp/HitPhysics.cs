using System;
using UnityEngine;

public class HitPhysics : MonoBehaviour, IHit
{
	// Token: 0x060007D0 RID: 2000 RVA: 0x00007C3E File Offset: 0x00005E3E
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

	// Token: 0x060007D1 RID: 2001 RVA: 0x00007C74 File Offset: 0x00005E74
	private void Awake()
	{
		if (this.rigidbody == null)
		{
			this.rigidbody = base.GetComponent<Rigidbody>();
		}
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x0003624C File Offset: 0x0003444C
	public void Hit(Vector3 velocity, bool isHeavy = false)
	{
		if (this.randomizeForcePoint)
		{
			Bounds bounds = this.collider.bounds;
			Vector3 vector = bounds.center + bounds.extents.magnitude * global::UnityEngine.Random.insideUnitSphere;
			this.rigidbody.AddForceAtPosition(this.forceMultiplier * velocity, vector, ForceMode.Impulse);
			return;
		}
		this.rigidbody.AddForce(this.forceMultiplier * velocity, ForceMode.Impulse);
	}

	public Rigidbody rigidbody;

	public Collider collider;

	public float forceMultiplier = 3f;

	public bool randomizeForcePoint = true;
}
