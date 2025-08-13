using System;
using UnityEngine;

// Token: 0x02000268 RID: 616
public class LookAtCamera : MonoBehaviour
{
	// Token: 0x06000BC3 RID: 3011 RVA: 0x0000B0DF File Offset: 0x000092DF
	private void Start()
	{
		this.mainCameraTransform = MainCamera.t;
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x00040590 File Offset: 0x0003E790
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

	// Token: 0x04000EA1 RID: 3745
	public float lookAtWeight = 0.5f;

	// Token: 0x04000EA2 RID: 3746
	private Transform mainCameraTransform;

	// Token: 0x04000EA3 RID: 3747
	public Vector3 forward = Vector3.forward;

	// Token: 0x04000EA4 RID: 3748
	public Vector3 up = Vector3.up;

	// Token: 0x04000EA5 RID: 3749
	public bool lockUp = true;
}
