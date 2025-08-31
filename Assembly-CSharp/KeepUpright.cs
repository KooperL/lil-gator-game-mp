using System;
using UnityEngine;

public class KeepUpright : MonoBehaviour
{
	// Token: 0x06000A05 RID: 2565 RVA: 0x0002E7B8 File Offset: 0x0002C9B8
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x0002E7C8 File Offset: 0x0002C9C8
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

	// Token: 0x06000A07 RID: 2567 RVA: 0x0002E832 File Offset: 0x0002CA32
	public static Quaternion ShortestRotation(Quaternion a, Quaternion b)
	{
		if (Quaternion.Dot(a, b) < 0f)
		{
			return a * Quaternion.Inverse(KeepUpright.Multiply(b, -1f));
		}
		return a * Quaternion.Inverse(b);
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x0002E865 File Offset: 0x0002CA65
	public static Quaternion Multiply(Quaternion input, float scalar)
	{
		return new Quaternion(input.x * scalar, input.y * scalar, input.z * scalar, input.w * scalar);
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x0002E88C File Offset: 0x0002CA8C
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
