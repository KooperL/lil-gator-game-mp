using System;
using UnityEngine;

// Token: 0x020001DF RID: 479
public class LookAtCamera : MonoBehaviour
{
	// Token: 0x06000A0E RID: 2574 RVA: 0x0002E99A File Offset: 0x0002CB9A
	private void Start()
	{
		this.mainCameraTransform = MainCamera.t;
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x0002E9A8 File Offset: 0x0002CBA8
	private void LateUpdate()
	{
		Vector3 vector = this.mainCameraTransform.position - base.transform.position;
		if (this.lockUp)
		{
			Vector3 vector2 = base.transform.parent.InverseTransformDirection(vector);
			vector2.y = 0f;
			vector = base.transform.parent.TransformDirection(vector2);
		}
		Quaternion quaternion = Quaternion.FromToRotation(base.transform.TransformDirection(this.forward), vector);
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, quaternion * base.transform.rotation, this.lookAtWeight);
	}

	// Token: 0x04000C63 RID: 3171
	public float lookAtWeight = 0.5f;

	// Token: 0x04000C64 RID: 3172
	private Transform mainCameraTransform;

	// Token: 0x04000C65 RID: 3173
	public Vector3 forward = Vector3.forward;

	// Token: 0x04000C66 RID: 3174
	public Vector3 up = Vector3.up;

	// Token: 0x04000C67 RID: 3175
	public bool lockUp = true;
}
