using System;
using UnityEngine;

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

	private Rigidbody rigidbody;

	public float velocityMagnitude = 7f;

	public float angularVelocityMagnitude = 120f;

	public float verticalOffset;

	public Vector3 directionalPush = Vector3.zero;
}
