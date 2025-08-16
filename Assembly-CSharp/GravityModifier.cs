using System;
using UnityEngine;

public class GravityModifier : MonoBehaviour
{
	// Token: 0x06000B1C RID: 2844 RVA: 0x0000A7CA File Offset: 0x000089CA
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000B1D RID: 2845 RVA: 0x0000A7D8 File Offset: 0x000089D8
	private void FixedUpdate()
	{
		this.rigidbody.AddForce(this.gravityMod * Physics.gravity, ForceMode.Acceleration);
	}

	public float gravityMod = 1f;

	private Rigidbody rigidbody;
}
