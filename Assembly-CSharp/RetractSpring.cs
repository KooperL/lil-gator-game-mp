using System;
using UnityEngine;

// Token: 0x02000245 RID: 581
public class RetractSpring : MonoBehaviour
{
	// Token: 0x06000AE3 RID: 2787 RVA: 0x0003E3A0 File Offset: 0x0003C5A0
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

	// Token: 0x06000AE4 RID: 2788 RVA: 0x0003E40C File Offset: 0x0003C60C
	private Vector3 GetCurrentDelta()
	{
		Vector3 vector = this.rigidbody.position + this.rigidbody.rotation * this.joint.anchor;
		Vector3 vector2 = this.joint.connectedBody.position + this.joint.connectedBody.rotation * this.joint.connectedAnchor;
		return vector - vector2;
	}

	// Token: 0x06000AE5 RID: 2789 RVA: 0x0003E480 File Offset: 0x0003C680
	private float GetCurrentDistance()
	{
		Vector3 vector = this.rigidbody.position + this.rigidbody.rotation * this.joint.anchor;
		Vector3 vector2 = this.joint.connectedBody.position + this.joint.connectedBody.rotation * this.joint.connectedAnchor;
		return Vector3.Distance(vector, vector2);
	}

	// Token: 0x06000AE6 RID: 2790 RVA: 0x0003E4F4 File Offset: 0x0003C6F4
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

	// Token: 0x04000DCE RID: 3534
	public Rigidbody rigidbody;

	// Token: 0x04000DCF RID: 3535
	public SpringJoint joint;

	// Token: 0x04000DD0 RID: 3536
	public SpringJoint extendedJoint;

	// Token: 0x04000DD1 RID: 3537
	public float extendedJointDistance = 0.5f;

	// Token: 0x04000DD2 RID: 3538
	private float jointLength;

	// Token: 0x04000DD3 RID: 3539
	private float jointLengthVel;

	// Token: 0x04000DD4 RID: 3540
	public float reelSpeed = 0.25f;

	// Token: 0x04000DD5 RID: 3541
	public bool adapt = true;

	// Token: 0x04000DD6 RID: 3542
	public float adaptDistance = 1f;

	// Token: 0x04000DD7 RID: 3543
	public float maxAdaptSpeed = 10f;

	// Token: 0x04000DD8 RID: 3544
	public float minDistance = 1f;
}
