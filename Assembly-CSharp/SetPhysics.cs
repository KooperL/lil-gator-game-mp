using System;
using UnityEngine;

// Token: 0x02000235 RID: 565
public class SetPhysics : MonoBehaviour
{
	// Token: 0x06000C44 RID: 3140 RVA: 0x0003B020 File Offset: 0x00039220
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

	// Token: 0x06000C45 RID: 3141 RVA: 0x0003B0A8 File Offset: 0x000392A8
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

	// Token: 0x04000FFF RID: 4095
	private Rigidbody rigidbody;

	// Token: 0x04001000 RID: 4096
	public Vector3 localVelocity;

	// Token: 0x04001001 RID: 4097
	public Vector3 globalVelocity;

	// Token: 0x04001002 RID: 4098
	public Vector3 angularVelocity;

	// Token: 0x04001003 RID: 4099
	public bool resetOnDisable;

	// Token: 0x04001004 RID: 4100
	private Vector3 position;

	// Token: 0x04001005 RID: 4101
	private Quaternion rotation;
}
