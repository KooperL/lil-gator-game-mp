using System;
using Cinemachine.Utility;
using UnityEngine;

// Token: 0x0200006A RID: 106
public class PlayerMoveOnSphere : MonoBehaviour
{
	// Token: 0x06000158 RID: 344 RVA: 0x0001BD68 File Offset: 0x00019F68
	private void Update()
	{
		Vector3 vector;
		vector..ctor(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
		if (vector.magnitude > 0f)
		{
			vector = Camera.main.transform.rotation * vector;
			if (vector.magnitude > 0.001f)
			{
				base.transform.position += vector * (this.speed * Time.deltaTime);
				if (this.rotatePlayer)
				{
					float num = Damper.Damp(1f, this.rotationDamping, Time.deltaTime);
					Quaternion quaternion = Quaternion.LookRotation(vector.normalized, base.transform.up);
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, quaternion, num);
				}
			}
		}
		if (this.Sphere != null)
		{
			Vector3 normalized = (base.transform.position - this.Sphere.transform.position).normalized;
			Vector3 vector2 = UnityVectorExtensions.ProjectOntoPlane(base.transform.forward, normalized);
			base.transform.position = this.Sphere.transform.position + normalized * (this.Sphere.radius + base.transform.localScale.y / 2f);
			base.transform.rotation = Quaternion.LookRotation(vector2, normalized);
		}
	}

	// Token: 0x04000213 RID: 531
	public SphereCollider Sphere;

	// Token: 0x04000214 RID: 532
	public float speed = 5f;

	// Token: 0x04000215 RID: 533
	public bool rotatePlayer = true;

	// Token: 0x04000216 RID: 534
	public float rotationDamping = 0.5f;
}
