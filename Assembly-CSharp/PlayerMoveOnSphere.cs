using System;
using Cinemachine.Utility;
using UnityEngine;

// Token: 0x02000051 RID: 81
public class PlayerMoveOnSphere : MonoBehaviour
{
	// Token: 0x06000133 RID: 307 RVA: 0x000077E0 File Offset: 0x000059E0
	private void Update()
	{
		Vector3 vector = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
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
			Vector3 vector2 = base.transform.forward.ProjectOntoPlane(normalized);
			base.transform.position = this.Sphere.transform.position + normalized * (this.Sphere.radius + base.transform.localScale.y / 2f);
			base.transform.rotation = Quaternion.LookRotation(vector2, normalized);
		}
	}

	// Token: 0x040001AF RID: 431
	public SphereCollider Sphere;

	// Token: 0x040001B0 RID: 432
	public float speed = 5f;

	// Token: 0x040001B1 RID: 433
	public bool rotatePlayer = true;

	// Token: 0x040001B2 RID: 434
	public float rotationDamping = 0.5f;
}
