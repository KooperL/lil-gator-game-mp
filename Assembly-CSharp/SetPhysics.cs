using System;
using UnityEngine;

// Token: 0x020002F4 RID: 756
public class SetPhysics : MonoBehaviour
{
	// Token: 0x06000EE3 RID: 3811 RVA: 0x0004E4A4 File Offset: 0x0004C6A4
	public void OnEnable()
	{
		if (this.rigidbody == null)
		{
			this.rigidbody = base.GetComponent<Rigidbody>();
		}
		if (this.resetOnDisable)
		{
			this.position = this.rigidbody.position;
			this.rotation = this.rigidbody.rotation;
		}
		this.rigidbody.AddRelativeForce(this.localVelocity, 2);
		this.rigidbody.AddForce(this.globalVelocity, 2);
		this.rigidbody.AddRelativeTorque(this.angularVelocity, 2);
	}

	// Token: 0x06000EE4 RID: 3812 RVA: 0x0004E52C File Offset: 0x0004C72C
	private void OnDisable()
	{
		if (this.resetOnDisable && this.rigidbody != null)
		{
			this.rigidbody.position = this.position;
			this.rigidbody.rotation = this.rotation;
			this.rigidbody.velocity = Vector3.zero;
			this.rigidbody.angularVelocity = Vector3.zero;
		}
	}

	// Token: 0x04001300 RID: 4864
	private Rigidbody rigidbody;

	// Token: 0x04001301 RID: 4865
	public Vector3 localVelocity;

	// Token: 0x04001302 RID: 4866
	public Vector3 globalVelocity;

	// Token: 0x04001303 RID: 4867
	public Vector3 angularVelocity;

	// Token: 0x04001304 RID: 4868
	public bool resetOnDisable;

	// Token: 0x04001305 RID: 4869
	private Vector3 position;

	// Token: 0x04001306 RID: 4870
	private Quaternion rotation;
}
