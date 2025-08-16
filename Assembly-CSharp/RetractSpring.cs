using System;
using UnityEngine;

public class RetractSpring : MonoBehaviour
{
	// Token: 0x06000B2F RID: 2863 RVA: 0x0003FD14 File Offset: 0x0003DF14
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

	// Token: 0x06000B30 RID: 2864 RVA: 0x0003FD80 File Offset: 0x0003DF80
	private Vector3 GetCurrentDelta()
	{
		Vector3 vector = this.rigidbody.position + this.rigidbody.rotation * this.joint.anchor;
		Vector3 vector2 = this.joint.connectedBody.position + this.joint.connectedBody.rotation * this.joint.connectedAnchor;
		return vector - vector2;
	}

	// Token: 0x06000B31 RID: 2865 RVA: 0x0003FDF4 File Offset: 0x0003DFF4
	private float GetCurrentDistance()
	{
		Vector3 vector = this.rigidbody.position + this.rigidbody.rotation * this.joint.anchor;
		Vector3 vector2 = this.joint.connectedBody.position + this.joint.connectedBody.rotation * this.joint.connectedAnchor;
		return Vector3.Distance(vector, vector2);
	}

	// Token: 0x06000B32 RID: 2866 RVA: 0x0003FE68 File Offset: 0x0003E068
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
