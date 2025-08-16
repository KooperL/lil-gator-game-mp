using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodySleep : MonoBehaviour
{
	// Token: 0x06000677 RID: 1655 RVA: 0x00006AB5 File Offset: 0x00004CB5
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
		this.rigidbody.Sleep();
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x00006ACE File Offset: 0x00004CCE
	private void FixedUpdate()
	{
		this.isSleeping = this.rigidbody.IsSleeping();
	}

	private Rigidbody rigidbody;

	public bool isSleeping;
}
