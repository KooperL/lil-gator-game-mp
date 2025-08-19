using System;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
	// Token: 0x06000C0F RID: 3087 RVA: 0x0000B3F1 File Offset: 0x000095F1
	private void Start()
	{
		this.mainCameraTransform = MainCamera.t;
	}

	// Token: 0x06000C10 RID: 3088 RVA: 0x000420F4 File Offset: 0x000402F4
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

	public float lookAtWeight = 0.5f;

	private Transform mainCameraTransform;

	public Vector3 forward = Vector3.forward;

	public Vector3 up = Vector3.up;

	public bool lockUp = true;
}
