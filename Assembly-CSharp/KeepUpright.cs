using System;
using UnityEngine;

public class KeepUpright : MonoBehaviour
{
	// Token: 0x06000C06 RID: 3078 RVA: 0x0000B333 File Offset: 0x00009533
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x00041E70 File Offset: 0x00040070
	private Vector3 GetForward()
	{
		Vector3 vector = Quaternion.FromToRotation(this.referenceRigidbody.rotation * this.referenceUp, Vector3.up) * this.referenceRigidbody.rotation * this.referenceForward;
		vector.y = 0f;
		if (vector != Vector3.zero)
		{
			vector.Normalize();
		}
		return vector;
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x0000B341 File Offset: 0x00009541
	public static Quaternion ShortestRotation(Quaternion a, Quaternion b)
	{
		if (Quaternion.Dot(a, b) < 0f)
		{
			return a * Quaternion.Inverse(KeepUpright.Multiply(b, -1f));
		}
		return a * Quaternion.Inverse(b);
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x00009D55 File Offset: 0x00007F55
	public static Quaternion Multiply(Quaternion input, float scalar)
	{
		return new Quaternion(input.x * scalar, input.y * scalar, input.z * scalar, input.w * scalar);
	}

	// Token: 0x06000C0A RID: 3082 RVA: 0x00041EDC File Offset: 0x000400DC
	private void FixedUpdate()
	{
		if (this.rigidbody.isKinematic)
		{
			return;
		}
		Vector3 vector = this.rigidbody.rotation * this.forwardDirection;
		float num;
		Vector3 vector2;
		KeepUpright.ShortestRotation(Quaternion.LookRotation(this.GetForward()), Quaternion.LookRotation(vector)).ToAngleAxis(out num, out vector2);
		vector2.Normalize();
		Vector3 vector3 = num * 0.017453292f * this.uprightSpringStrength * vector2 - this.uprightSpringDamper * this.rigidbody.angularVelocity;
		vector3 = Vector3.ClampMagnitude(vector3, this.maxTorque);
		this.rigidbody.AddTorque(vector3);
	}

	private Rigidbody rigidbody;

	public float uprightSpringStrength;

	public float uprightSpringDamper;

	public float maxTorque = 90f;

	public Rigidbody referenceRigidbody;

	public Vector3 referenceForward = Vector3.forward;

	public Vector3 referenceUp = Vector3.up;

	public Vector3 forwardDirection = Vector3.forward;
}
