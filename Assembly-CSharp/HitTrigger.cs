using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000195 RID: 405
public class HitTrigger : MonoBehaviour
{
	// Token: 0x06000794 RID: 1940 RVA: 0x0000799B File Offset: 0x00005B9B
	public void OnTriggerStay(Collider other)
	{
		if (this.hitOnStay)
		{
			this.HitCollider(other);
		}
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x000079AC File Offset: 0x00005BAC
	private void OnTriggerEnter(Collider other)
	{
		this.HitCollider(other);
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x00034960 File Offset: 0x00032B60
	private void HitCollider(Collider other)
	{
		if (this.onHit != null)
		{
			this.onHit.Invoke();
		}
		IHit hit;
		if (other == null || !other.TryGetComponent<IHit>(ref hit))
		{
			return;
		}
		if (this.ignoreHitPhysics && hit is HitPhysics)
		{
			return;
		}
		HitTrigger.currentHitPoint = base.transform.TransformPoint(this.hitPosition);
		Vector3 vector = Vector3.zero;
		switch (this.velocitySource)
		{
		case HitTrigger.VelocitySource.Direction:
			vector = base.transform.TransformDirection(this.localDirection);
			break;
		case HitTrigger.VelocitySource.Rigidbody:
			vector = this.rigidbodySource.velocity;
			break;
		case HitTrigger.VelocitySource.Radial:
		{
			Vector3 vector2 = other.bounds.center - HitTrigger.currentHitPoint;
			vector = vector2.normalized * (this.radius - vector2.magnitude) / this.radius;
			break;
		}
		}
		vector *= this.velocityMultiplier;
		vector += this.velocityOffset;
		hit.Hit(vector, this.isHeavy);
	}

	// Token: 0x04000A14 RID: 2580
	public static Vector3 currentHitPoint = Vector3.zero;

	// Token: 0x04000A15 RID: 2581
	public HitTrigger.VelocitySource velocitySource;

	// Token: 0x04000A16 RID: 2582
	public float velocityMultiplier = 1f;

	// Token: 0x04000A17 RID: 2583
	public Vector3 velocityOffset = Vector3.zero;

	// Token: 0x04000A18 RID: 2584
	public Vector3 localDirection;

	// Token: 0x04000A19 RID: 2585
	public Rigidbody rigidbodySource;

	// Token: 0x04000A1A RID: 2586
	public float radius;

	// Token: 0x04000A1B RID: 2587
	public UnityEvent onHit;

	// Token: 0x04000A1C RID: 2588
	public bool ignoreHitPhysics;

	// Token: 0x04000A1D RID: 2589
	public Vector3 hitPosition;

	// Token: 0x04000A1E RID: 2590
	public bool isHeavy;

	// Token: 0x04000A1F RID: 2591
	public bool hitOnStay;

	// Token: 0x02000196 RID: 406
	public enum VelocitySource
	{
		// Token: 0x04000A21 RID: 2593
		Direction,
		// Token: 0x04000A22 RID: 2594
		Rigidbody,
		// Token: 0x04000A23 RID: 2595
		Radial
	}
}
