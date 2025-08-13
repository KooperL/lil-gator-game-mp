using System;
using UnityEngine;

// Token: 0x020001C3 RID: 451
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

	// Token: 0x04000BAB RID: 2987
	public Rigidbody rigidbody;

	// Token: 0x04000BAC RID: 2988
	public SpringJoint joint;

	// Token: 0x04000BAD RID: 2989
	public SpringJoint extendedJoint;

	// Token: 0x04000BAE RID: 2990
	public float extendedJointDistance = 0.5f;

	// Token: 0x04000BAF RID: 2991
	private float jointLength;

	// Token: 0x04000BB0 RID: 2992
	private float jointLengthVel;

	// Token: 0x04000BB1 RID: 2993
	public float reelSpeed = 0.25f;

	// Token: 0x04000BB2 RID: 2994
	public bool adapt = true;

	// Token: 0x04000BB3 RID: 2995
	public float adaptDistance = 1f;

	// Token: 0x04000BB4 RID: 2996
	public float maxAdaptSpeed = 10f;

	// Token: 0x04000BB5 RID: 2997
	public float minDistance = 1f;
}
