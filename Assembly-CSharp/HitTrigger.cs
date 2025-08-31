using System;
using UnityEngine;
using UnityEngine.Events;

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
