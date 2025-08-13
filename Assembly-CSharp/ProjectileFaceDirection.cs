using System;
using UnityEngine;

// Token: 0x02000243 RID: 579
public class ProjectileFaceDirection : MonoBehaviour
{
	// Token: 0x06000ADD RID: 2781 RVA: 0x0000A5B2 File Offset: 0x000087B2
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x0003E28C File Offset: 0x0003C48C
	public void FixedUpdate()
	{
		float magnitude = this.rigidbody.velocity.magnitude;
		if (magnitude > 0.1f)
		{
			Quaternion quaternion = Quaternion.FromToRotation(Vector3.forward, this.rigidbody.velocity);
			this.rigidbody.rotation = Quaternion.Slerp(this.rigidbody.rotation, quaternion, Mathf.InverseLerp(0.1f, 1f, magnitude));
		}
	}

	// Token: 0x04000DCA RID: 3530
	public Vector3 localDirection;

	// Token: 0x04000DCB RID: 3531
	private Rigidbody rigidbody;
}
