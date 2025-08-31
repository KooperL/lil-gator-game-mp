using System;
using UnityEngine;

public class RagdollWhenSpringed : MonoBehaviour
{
	// Token: 0x06000949 RID: 2377 RVA: 0x0002C157 File Offset: 0x0002A357
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
		this.joint = base.GetComponent<SpringJoint>();
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x0002C174 File Offset: 0x0002A374
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
