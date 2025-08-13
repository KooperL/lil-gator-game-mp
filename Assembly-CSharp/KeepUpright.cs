using System;
using UnityEngine;

// Token: 0x02000266 RID: 614
public class KeepUpright : MonoBehaviour
{
	// Token: 0x06000BBA RID: 3002 RVA: 0x0000B040 File Offset: 0x00009240
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000BBB RID: 3003 RVA: 0x0004047C File Offset: 0x0003E67C
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

	// Token: 0x06000BBC RID: 3004 RVA: 0x0000B04E File Offset: 0x0000924E
	public static Quaternion ShortestRotation(Quaternion a, Quaternion b)
	{
		if (Quaternion.Dot(a, b) < 0f)
		{
			return a * Quaternion.Inverse(KeepUpright.Multiply(b, -1f));
		}
		return a * Quaternion.Inverse(b);
	}

	// Token: 0x06000BBD RID: 3005 RVA: 0x00009A36 File Offset: 0x00007C36
	public static Quaternion Multiply(Quaternion input, float scalar)
	{
		return new Quaternion(input.x * scalar, input.y * scalar, input.z * scalar, input.w * scalar);
	}

	// Token: 0x06000BBE RID: 3006 RVA: 0x000404E8 File Offset: 0x0003E6E8
	private void FixedUpdate()
	{
		if (this.rigidbody.isKinematic)
		{
			return;
		}
		Vector3 vector = this.rigidbody.rotation * this.forwardDirection;
		float num;
		Vector3 vector2;
		KeepUpright.ShortestRotation(Quaternion.LookRotation(this.GetForward()), Quaternion.LookRotation(vector)).ToAngleAxis(ref num, ref vector2);
		vector2.Normalize();
		Vector3 vector3 = num * 0.017453292f * this.uprightSpringStrength * vector2 - this.uprightSpringDamper * this.rigidbody.angularVelocity;
		vector3 = Vector3.ClampMagnitude(vector3, this.maxTorque);
		this.rigidbody.AddTorque(vector3);
	}

	// Token: 0x04000E98 RID: 3736
	private Rigidbody rigidbody;

	// Token: 0x04000E99 RID: 3737
	public float uprightSpringStrength;

	// Token: 0x04000E9A RID: 3738
	public float uprightSpringDamper;

	// Token: 0x04000E9B RID: 3739
	public float maxTorque = 90f;

	// Token: 0x04000E9C RID: 3740
	public Rigidbody referenceRigidbody;

	// Token: 0x04000E9D RID: 3741
	public Vector3 referenceForward = Vector3.forward;

	// Token: 0x04000E9E RID: 3742
	public Vector3 referenceUp = Vector3.up;

	// Token: 0x04000E9F RID: 3743
	public Vector3 forwardDirection = Vector3.forward;
}
