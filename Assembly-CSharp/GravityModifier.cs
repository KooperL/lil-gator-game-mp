using System;
using UnityEngine;

public class GravityModifier : MonoBehaviour
{
	// Token: 0x06000B1D RID: 2845 RVA: 0x0000A7E9 File Offset: 0x000089E9
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000B1E RID: 2846 RVA: 0x0000A7F7 File Offset: 0x000089F7
	private void FixedUpdate()
	{
		this.rigidbody.AddForce(this.gravityMod * Physics.gravity, ForceMode.Acceleration);
	}

	public float gravityMod = 1f;

	private Rigidbody rigidbody;
}
