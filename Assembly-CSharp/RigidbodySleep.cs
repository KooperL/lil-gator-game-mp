using System;
using UnityEngine;

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

	private Rigidbody rigidbody;

	public bool isSleeping;
}
