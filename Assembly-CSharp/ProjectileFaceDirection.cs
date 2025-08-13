using System;
using UnityEngine;

// Token: 0x020001C1 RID: 449
public class ProjectileFaceDirection : MonoBehaviour
{
	// Token: 0x06000946 RID: 2374 RVA: 0x0002C0D4 File Offset: 0x0002A2D4
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x0002C0E4 File Offset: 0x0002A2E4
	public void FixedUpdate()
	{
		float magnitude = this.rigidbody.velocity.magnitude;
		if (magnitude > 0.1f)
		{
			Quaternion quaternion = Quaternion.FromToRotation(Vector3.forward, this.rigidbody.velocity);
			this.rigidbody.rotation = Quaternion.Slerp(this.rigidbody.rotation, quaternion, Mathf.InverseLerp(0.1f, 1f, magnitude));
		}
	}

	// Token: 0x04000BA7 RID: 2983
	public Vector3 localDirection;

	// Token: 0x04000BA8 RID: 2984
	private Rigidbody rigidbody;
}
