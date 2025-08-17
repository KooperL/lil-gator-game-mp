using System;
using UnityEngine;

public class SetPhysics : MonoBehaviour
{
	// Token: 0x06000F3F RID: 3903 RVA: 0x000502F4 File Offset: 0x0004E4F4
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
		this.rigidbody.AddRelativeForce(this.localVelocity, ForceMode.VelocityChange);
		this.rigidbody.AddForce(this.globalVelocity, ForceMode.VelocityChange);
		this.rigidbody.AddRelativeTorque(this.angularVelocity, ForceMode.VelocityChange);
	}

	// Token: 0x06000F40 RID: 3904 RVA: 0x0005037C File Offset: 0x0004E57C
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

	private Rigidbody rigidbody;

	public Vector3 localVelocity;

	public Vector3 globalVelocity;

	public Vector3 angularVelocity;

	public bool resetOnDisable;

	private Vector3 position;

	private Quaternion rotation;
}
