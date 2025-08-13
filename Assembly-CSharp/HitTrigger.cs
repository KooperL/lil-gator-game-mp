using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000139 RID: 313
public class HitTrigger : MonoBehaviour
{
	// Token: 0x0600066F RID: 1647 RVA: 0x00021283 File Offset: 0x0001F483
	public void OnTriggerStay(Collider other)
	{
		if (this.hitOnStay)
		{
			this.HitCollider(other);
		}
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x00021294 File Offset: 0x0001F494
	private void OnTriggerEnter(Collider other)
	{
		this.HitCollider(other);
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x000212A0 File Offset: 0x0001F4A0
	private void HitCollider(Collider other)
	{
		if (this.onHit != null)
		{
			this.onHit.Invoke();
		}
		IHit hit;
		if (other == null || !other.TryGetComponent<IHit>(out hit))
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

	// Token: 0x040008A8 RID: 2216
	public static Vector3 currentHitPoint = Vector3.zero;

	// Token: 0x040008A9 RID: 2217
	public HitTrigger.VelocitySource velocitySource;

	// Token: 0x040008AA RID: 2218
	public float velocityMultiplier = 1f;

	// Token: 0x040008AB RID: 2219
	public Vector3 velocityOffset = Vector3.zero;

	// Token: 0x040008AC RID: 2220
	public Vector3 localDirection;

	// Token: 0x040008AD RID: 2221
	public Rigidbody rigidbodySource;

	// Token: 0x040008AE RID: 2222
	public float radius;

	// Token: 0x040008AF RID: 2223
	public UnityEvent onHit;

	// Token: 0x040008B0 RID: 2224
	public bool ignoreHitPhysics;

	// Token: 0x040008B1 RID: 2225
	public Vector3 hitPosition;

	// Token: 0x040008B2 RID: 2226
	public bool isHeavy;

	// Token: 0x040008B3 RID: 2227
	public bool hitOnStay;

	// Token: 0x020003B3 RID: 947
	public enum VelocitySource
	{
		// Token: 0x04001B88 RID: 7048
		Direction,
		// Token: 0x04001B89 RID: 7049
		Rigidbody,
		// Token: 0x04001B8A RID: 7050
		Radial
	}
}
