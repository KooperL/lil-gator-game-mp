using System;
using UnityEngine;

// Token: 0x0200022D RID: 557
public class RandomizePhysics : MonoBehaviour
{
	// Token: 0x06000C0D RID: 3085 RVA: 0x0003979D File Offset: 0x0003799D
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000C0E RID: 3086 RVA: 0x000397AC File Offset: 0x000379AC
	private void Start()
	{
		this.rigidbody.velocity += this.velocityMagnitude * Random.insideUnitSphere + this.verticalOffset * Vector3.up + base.transform.rotation * this.directionalPush;
		this.rigidbody.angularVelocity += this.angularVelocityMagnitude * Random.insideUnitSphere;
	}

	// Token: 0x04000FD6 RID: 4054
	private Rigidbody rigidbody;

	// Token: 0x04000FD7 RID: 4055
	public float velocityMagnitude = 7f;

	// Token: 0x04000FD8 RID: 4056
	public float angularVelocityMagnitude = 120f;

	// Token: 0x04000FD9 RID: 4057
	public float verticalOffset;

	// Token: 0x04000FDA RID: 4058
	public Vector3 directionalPush = Vector3.zero;
}
