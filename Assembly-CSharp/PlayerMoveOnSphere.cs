using System;
using Cinemachine.Utility;
using UnityEngine;

public class PlayerMoveOnSphere : MonoBehaviour
{
	// Token: 0x06000160 RID: 352 RVA: 0x0001C558 File Offset: 0x0001A758
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

	public SphereCollider Sphere;

	public float speed = 5f;

	public bool rotatePlayer = true;

	public float rotationDamping = 0.5f;
}
