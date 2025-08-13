using System;
using UnityEngine;

// Token: 0x02000244 RID: 580
public class RagdollWhenSpringed : MonoBehaviour
{
	// Token: 0x06000AE0 RID: 2784 RVA: 0x0000A5C0 File Offset: 0x000087C0
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
		this.joint = base.GetComponent<SpringJoint>();
	}

	// Token: 0x06000AE1 RID: 2785 RVA: 0x0003E2F8 File Offset: 0x0003C4F8
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

	// Token: 0x04000DCC RID: 3532
	private Rigidbody rigidbody;

	// Token: 0x04000DCD RID: 3533
	private SpringJoint joint;
}
