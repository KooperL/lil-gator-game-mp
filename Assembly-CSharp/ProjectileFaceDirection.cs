using System;
using UnityEngine;

public class ProjectileFaceDirection : MonoBehaviour
{
	// Token: 0x06000B29 RID: 2857 RVA: 0x0000A8F0 File Offset: 0x00008AF0
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000B2A RID: 2858 RVA: 0x0003FD70 File Offset: 0x0003DF70
	public void FixedUpdate()
	{
		float magnitude = this.rigidbody.velocity.magnitude;
		if (magnitude > 0.1f)
		{
			Quaternion quaternion = Quaternion.FromToRotation(Vector3.forward, this.rigidbody.velocity);
			this.rigidbody.rotation = Quaternion.Slerp(this.rigidbody.rotation, quaternion, Mathf.InverseLerp(0.1f, 1f, magnitude));
		}
	}

	public Vector3 localDirection;

	private Rigidbody rigidbody;
}
