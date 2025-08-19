using System;
using UnityEngine;
using UnityEngine.Events;

public class HitTrigger : MonoBehaviour
{
	// Token: 0x060007D4 RID: 2004 RVA: 0x00007CAA File Offset: 0x00005EAA
	public void OnTriggerStay(Collider other)
	{
		if (this.hitOnStay)
		{
			this.HitCollider(other);
		}
	}

	// Token: 0x060007D5 RID: 2005 RVA: 0x00007CBB File Offset: 0x00005EBB
	private void OnTriggerEnter(Collider other)
	{
		this.HitCollider(other);
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x000362A4 File Offset: 0x000344A4
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

	public static Vector3 currentHitPoint = Vector3.zero;

	public HitTrigger.VelocitySource velocitySource;

	public float velocityMultiplier = 1f;

	public Vector3 velocityOffset = Vector3.zero;

	public Vector3 localDirection;

	public Rigidbody rigidbodySource;

	public float radius;

	public UnityEvent onHit;

	public bool ignoreHitPhysics;

	public Vector3 hitPosition;

	public bool isHeavy;

	public bool hitOnStay;

	public enum VelocitySource
	{
		Direction,
		Rigidbody,
		Radial
	}
}
