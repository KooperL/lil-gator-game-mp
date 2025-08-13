using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001C5 RID: 453
public class StickToSurface : MonoBehaviour
{
	// Token: 0x06000954 RID: 2388 RVA: 0x0002C4FF File Offset: 0x0002A6FF
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

	// Token: 0x06000955 RID: 2389 RVA: 0x0002C530 File Offset: 0x0002A730
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

	// Token: 0x06000956 RID: 2390 RVA: 0x0002C67F File Offset: 0x0002A87F
	private void Unstick()
	{
		this.isStuck = false;
		this.rigidbody.isKinematic = false;
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x0002C694 File Offset: 0x0002A894
	private bool ShouldUnstick()
	{
		return this.stuckCollider == null || this.stuckObject == null || !this.stuckObject.activeInHierarchy || (this.stuckRigidbody != null && !this.stuckRigidbody.isKinematic);
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x0002C6F0 File Offset: 0x0002A8F0
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

	// Token: 0x04000BBA RID: 3002
	public Rigidbody rigidbody;

	// Token: 0x04000BBB RID: 3003
	public UnityEvent onStick;

	// Token: 0x04000BBC RID: 3004
	public UnityEvent onUnstick;

	// Token: 0x04000BBD RID: 3005
	public Vector3 stickNormal = Vector3.up;

	// Token: 0x04000BBE RID: 3006
	public float stickDistance = 0.1f;

	// Token: 0x04000BBF RID: 3007
	private bool isStuck;

	// Token: 0x04000BC0 RID: 3008
	private Collider stuckCollider;

	// Token: 0x04000BC1 RID: 3009
	private GameObject stuckObject;

	// Token: 0x04000BC2 RID: 3010
	private Rigidbody stuckRigidbody;

	// Token: 0x04000BC3 RID: 3011
	private bool isStuckToMovingCollider;

	// Token: 0x04000BC4 RID: 3012
	private Transform stuckTransform;

	// Token: 0x04000BC5 RID: 3013
	private Vector3 stuckLocalPosition;

	// Token: 0x04000BC6 RID: 3014
	private Quaternion stuckLocalRotation;
}
