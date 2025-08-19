using System;
using UnityEngine;
using UnityEngine.Events;

public class StickToSurface : MonoBehaviour
{
	// Token: 0x06000B37 RID: 2871 RVA: 0x0000A933 File Offset: 0x00008B33
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

	// Token: 0x06000B38 RID: 2872 RVA: 0x0004013C File Offset: 0x0003E33C
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

	// Token: 0x06000B39 RID: 2873 RVA: 0x0000A961 File Offset: 0x00008B61
	private void Unstick()
	{
		this.isStuck = false;
		this.rigidbody.isKinematic = false;
	}

	// Token: 0x06000B3A RID: 2874 RVA: 0x0004028C File Offset: 0x0003E48C
	private bool ShouldUnstick()
	{
		return this.stuckCollider == null || this.stuckObject == null || !this.stuckObject.activeInHierarchy || (this.stuckRigidbody != null && !this.stuckRigidbody.isKinematic);
	}

	// Token: 0x06000B3B RID: 2875 RVA: 0x000402E8 File Offset: 0x0003E4E8
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

	public Rigidbody rigidbody;

	public UnityEvent onStick;

	public UnityEvent onUnstick;

	public Vector3 stickNormal = Vector3.up;

	public float stickDistance = 0.1f;

	private bool isStuck;

	private Collider stuckCollider;

	private GameObject stuckObject;

	private Rigidbody stuckRigidbody;

	private bool isStuckToMovingCollider;

	private Transform stuckTransform;

	private Vector3 stuckLocalPosition;

	private Quaternion stuckLocalRotation;
}
