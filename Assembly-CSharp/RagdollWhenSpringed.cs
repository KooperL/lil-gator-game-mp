using System;
using UnityEngine;

public class RagdollWhenSpringed : MonoBehaviour
{
	// Token: 0x06000B2C RID: 2860 RVA: 0x0000A8FE File Offset: 0x00008AFE
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
		this.joint = base.GetComponent<SpringJoint>();
	}

	// Token: 0x06000B2D RID: 2861 RVA: 0x0003FDDC File Offset: 0x0003DFDC
	private void FixedUpdate()
	{
		Vector3 vector = this.rigidbody.position + this.rigidbody.rotation * this.joint.anchor;
		Vector3 vector2 = this.joint.connectedBody.position + this.joint.connectedBody.rotation * this.joint.connectedAnchor;
		if (Vector3.Distance(vector, vector2) >= this.joint.maxDistance)
		{
			Player.movement.Ragdoll(0.25f * this.rigidbody.velocity);
			base.enabled = false;
		}
	}

	private Rigidbody rigidbody;

	private SpringJoint joint;
}
