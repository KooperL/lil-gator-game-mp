using System;
using UnityEngine;

// Token: 0x0200023D RID: 573
public class GravityModifier : MonoBehaviour
{
	// Token: 0x06000AD0 RID: 2768 RVA: 0x0000A4AB File Offset: 0x000086AB
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000AD1 RID: 2769 RVA: 0x0000A4B9 File Offset: 0x000086B9
	private void FixedUpdate()
	{
		this.rigidbody.AddForce(this.gravityMod * Physics.gravity, 5);
	}

	// Token: 0x04000DB3 RID: 3507
	public float gravityMod = 1f;

	// Token: 0x04000DB4 RID: 3508
	private Rigidbody rigidbody;
}
