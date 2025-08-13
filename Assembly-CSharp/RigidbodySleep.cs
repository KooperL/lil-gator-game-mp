using System;
using UnityEngine;

// Token: 0x020000FB RID: 251
[RequireComponent(typeof(Rigidbody))]
public class RigidbodySleep : MonoBehaviour
{
	// Token: 0x0600052B RID: 1323 RVA: 0x0001BCC4 File Offset: 0x00019EC4
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
		this.rigidbody.Sleep();
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x0001BCDD File Offset: 0x00019EDD
	private void FixedUpdate()
	{
		this.isSleeping = this.rigidbody.IsSleeping();
	}

	// Token: 0x04000720 RID: 1824
	private Rigidbody rigidbody;

	// Token: 0x04000721 RID: 1825
	public bool isSleeping;
}
