using System;
using UnityEngine;

public class RetractSpring : MonoBehaviour
{
	// Token: 0x0600094C RID: 2380 RVA: 0x0002C224 File Offset: 0x0002A424
	private void OnEnable()
	{
		if (this.adapt)
		{
			this.jointLength = Mathf.Max(this.GetCurrentDistance() - this.adaptDistance, this.minDistance);
			this.joint.maxDistance = this.jointLength;
			if (this.extendedJoint != null)
			{
				this.extendedJoint.maxDistance = this.jointLength + this.extendedJointDistance;
			}
		}
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x0002C290 File Offset: 0x0002A490
	private Vector3 GetCurrentDelta()
	{
		Vector3 vector = this.rigidbody.position + this.rigidbody.rotation * this.joint.anchor;
		Vector3 vector2 = this.joint.connectedBody.position + this.joint.connectedBody.rotation * this.joint.connectedAnchor;
		return vector - vector2;
	}

	// Token: 0x0600094E RID: 2382 RVA: 0x0002C304 File Offset: 0x0002A504
	private float GetCurrentDistance()
	{
		Vector3 vector = this.rigidbody.position + this.rigidbody.rotation * this.joint.anchor;
		Vector3 vector2 = this.joint.connectedBody.position + this.joint.connectedBody.rotation * this.joint.connectedAnchor;
		return Vector3.Distance(vector, vector2);
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x0002C378 File Offset: 0x0002A578
	private void FixedUpdate()
	{
		float num = this.jointLength - this.reelSpeed * Time.deltaTime;
		if (this.adapt)
		{
			Vector3 currentDelta = this.GetCurrentDelta();
			Vector3 velocity = this.joint.connectedBody.velocity;
			float magnitude = currentDelta.magnitude;
			num = Mathf.Min(num, magnitude - this.adaptDistance);
		}
		this.jointLength = Mathf.SmoothDamp(this.jointLength, num, ref this.jointLengthVel, 0.25f);
		this.joint.maxDistance = this.jointLength;
		if (this.extendedJoint != null)
		{
			this.extendedJoint.maxDistance = this.jointLength + this.extendedJointDistance;
		}
	}

	public Rigidbody rigidbody;

	public SpringJoint joint;

	public SpringJoint extendedJoint;

	public float extendedJointDistance = 0.5f;

	private float jointLength;

	private float jointLengthVel;

	public float reelSpeed = 0.25f;

	public bool adapt = true;

	public float adaptDistance = 1f;

	public float maxAdaptSpeed = 10f;

	public float minDistance = 1f;
}
