using System;
using UnityEngine;

public class HitPhysics : MonoBehaviour, IHit
{
	// Token: 0x0600066B RID: 1643 RVA: 0x0002119D File Offset: 0x0001F39D
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

	// Token: 0x0600066C RID: 1644 RVA: 0x000211D3 File Offset: 0x0001F3D3
	private void Awake()
	{
		if (this.rigidbody == null)
		{
			this.rigidbody = base.GetComponent<Rigidbody>();
		}
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x000211F0 File Offset: 0x0001F3F0
	public void Hit(Vector3 velocity, bool isHeavy = false)
	{
		if (this.randomizeForcePoint)
		{
			Bounds bounds = this.collider.bounds;
			Vector3 vector = bounds.center + bounds.extents.magnitude * Random.insideUnitSphere;
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
