using System;
using UnityEngine;

// Token: 0x020002EB RID: 747
public class RandomizePhysics : MonoBehaviour
{
	// Token: 0x06000EB8 RID: 3768 RVA: 0x0000CE43 File Offset: 0x0000B043
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000EB9 RID: 3769 RVA: 0x0004CFFC File Offset: 0x0004B1FC
	private void Start()
	{
		this.rigidbody.velocity += this.velocityMagnitude * Random.insideUnitSphere + this.verticalOffset * Vector3.up + base.transform.rotation * this.directionalPush;
		this.rigidbody.angularVelocity += this.angularVelocityMagnitude * Random.insideUnitSphere;
	}

	// Token: 0x040012CD RID: 4813
	private Rigidbody rigidbody;

	// Token: 0x040012CE RID: 4814
	public float velocityMagnitude = 7f;

	// Token: 0x040012CF RID: 4815
	public float angularVelocityMagnitude = 120f;

	// Token: 0x040012D0 RID: 4816
	public float verticalOffset;

	// Token: 0x040012D1 RID: 4817
	public Vector3 directionalPush = Vector3.zero;
}
