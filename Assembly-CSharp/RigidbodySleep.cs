using System;
using UnityEngine;

// Token: 0x0200014E RID: 334
[RequireComponent(typeof(Rigidbody))]
public class RigidbodySleep : MonoBehaviour
{
	// Token: 0x0600063D RID: 1597 RVA: 0x000067EF File Offset: 0x000049EF
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
		this.rigidbody.Sleep();
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x00006808 File Offset: 0x00004A08
	private void FixedUpdate()
	{
		this.isSleeping = this.rigidbody.IsSleeping();
	}

	// Token: 0x04000867 RID: 2151
	private Rigidbody rigidbody;

	// Token: 0x04000868 RID: 2152
	public bool isSleeping;
}
