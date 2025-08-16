using System;
using UnityEngine;

public class RagdollWhenSpringed : MonoBehaviour
{
	// Token: 0x06000B2C RID: 2860 RVA: 0x0000A8DF File Offset: 0x00008ADF
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
		this.joint = base.GetComponent<SpringJoint>();
	}

	// Token: 0x06000B2D RID: 2861 RVA: 0x0003FC6C File Offset: 0x0003DE6C
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
