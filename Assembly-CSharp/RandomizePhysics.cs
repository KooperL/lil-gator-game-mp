using System;
using UnityEngine;

public class RandomizePhysics : MonoBehaviour
{
	// Token: 0x06000F04 RID: 3844 RVA: 0x0000D136 File Offset: 0x0000B336
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000F05 RID: 3845 RVA: 0x0004E9F0 File Offset: 0x0004CBF0
	private void Start()
	{
		this.rigidbody.velocity += this.velocityMagnitude * global::UnityEngine.Random.insideUnitSphere + this.verticalOffset * Vector3.up + base.transform.rotation * this.directionalPush;
		this.rigidbody.angularVelocity += this.angularVelocityMagnitude * global::UnityEngine.Random.insideUnitSphere;
	}

	private Rigidbody rigidbody;

	public float velocityMagnitude = 7f;

	public float angularVelocityMagnitude = 120f;

	public float verticalOffset;

	public Vector3 directionalPush = Vector3.zero;
}
