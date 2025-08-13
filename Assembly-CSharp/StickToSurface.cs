using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000248 RID: 584
public class StickToSurface : MonoBehaviour
{
	// Token: 0x06000AEB RID: 2795 RVA: 0x0000A5F5 File Offset: 0x000087F5
	private void OnCollisionEnter(Collision collision)
	{
		if (this.isStuck)
		{
			return;
		}
		if (collision.rigidbody != null && !collision.rigidbody.isKinematic)
		{
			return;
		}
		this.Stick(collision);
	}

	// Token: 0x06000AEC RID: 2796 RVA: 0x0003E658 File Offset: 0x0003C858
	private void Stick(Collision collision)
	{
		this.isStuck = true;
		this.rigidbody.isKinematic = true;
		Vector3 point = collision.contacts[0].point;
		Vector3 vector = collision.contacts[0].normal;
		if (Vector3.Dot(vector, this.rigidbody.velocity) > 0f)
		{
			vector *= -1f;
		}
		this.rigidbody.position = point + this.stickDistance * vector;
		this.rigidbody.rotation = Quaternion.FromToRotation(this.stickNormal, vector);
		this.rigidbody.velocity = Vector3.zero;
		this.stuckCollider = collision.collider;
		this.stuckObject = this.stuckCollider.gameObject;
		this.stuckRigidbody = collision.rigidbody;
		this.isStuckToMovingCollider = this.stuckObject.layer == 6;
		if (this.isStuckToMovingCollider)
		{
			this.stuckTransform = collision.collider.transform;
			this.stuckLocalPosition = this.stuckTransform.InverseTransformPoint(this.rigidbody.position);
			this.stuckLocalRotation = this.stuckTransform.rotation.Inverse() * this.rigidbody.rotation;
		}
		this.onStick.Invoke();
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x0000A623 File Offset: 0x00008823
	private void Unstick()
	{
		this.isStuck = false;
		this.rigidbody.isKinematic = false;
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x0003E7A8 File Offset: 0x0003C9A8
	private bool ShouldUnstick()
	{
		return this.stuckCollider == null || this.stuckObject == null || !this.stuckObject.activeInHierarchy || (this.stuckRigidbody != null && !this.stuckRigidbody.isKinematic);
	}

	// Token: 0x06000AEF RID: 2799 RVA: 0x0003E804 File Offset: 0x0003CA04
	private void Update()
	{
		if (this.isStuck)
		{
			if (this.ShouldUnstick())
			{
				this.Unstick();
				return;
			}
			if (this.isStuck && this.isStuckToMovingCollider)
			{
				base.transform.position = this.stuckTransform.TransformPoint(this.stuckLocalPosition);
				base.transform.rotation = this.stuckTransform.rotation * this.stuckLocalRotation;
			}
		}
	}

	// Token: 0x04000DE0 RID: 3552
	public Rigidbody rigidbody;

	// Token: 0x04000DE1 RID: 3553
	public UnityEvent onStick;

	// Token: 0x04000DE2 RID: 3554
	public UnityEvent onUnstick;

	// Token: 0x04000DE3 RID: 3555
	public Vector3 stickNormal = Vector3.up;

	// Token: 0x04000DE4 RID: 3556
	public float stickDistance = 0.1f;

	// Token: 0x04000DE5 RID: 3557
	private bool isStuck;

	// Token: 0x04000DE6 RID: 3558
	private Collider stuckCollider;

	// Token: 0x04000DE7 RID: 3559
	private GameObject stuckObject;

	// Token: 0x04000DE8 RID: 3560
	private Rigidbody stuckRigidbody;

	// Token: 0x04000DE9 RID: 3561
	private bool isStuckToMovingCollider;

	// Token: 0x04000DEA RID: 3562
	private Transform stuckTransform;

	// Token: 0x04000DEB RID: 3563
	private Vector3 stuckLocalPosition;

	// Token: 0x04000DEC RID: 3564
	private Quaternion stuckLocalRotation;
}
