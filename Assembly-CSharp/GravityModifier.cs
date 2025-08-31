using System;
using UnityEngine;

public class GravityModifier : MonoBehaviour
{
	// Token: 0x06000939 RID: 2361 RVA: 0x0002BE30 File Offset: 0x0002A030
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x0600093A RID: 2362 RVA: 0x0002BE3E File Offset: 0x0002A03E
	private void FixedUpdate()
	{
		this.rigidbody.AddForce(this.gravityMod * Physics.gravity, ForceMode.Acceleration);
	}

	public float gravityMod = 1f;

	private Rigidbody rigidbody;
}
