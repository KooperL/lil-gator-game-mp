using System;
using UnityEngine;

public class KeepWithinDistance : MonoBehaviour, IManagedUpdate
{
	// Token: 0x060008B8 RID: 2232 RVA: 0x000088FE File Offset: 0x00006AFE
	private void Start()
	{
		this.ManagedUpdate();
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x00008906 File Offset: 0x00006B06
	private void OnEnable()
	{
		FastUpdateManager.fixedUpdate8.Add(this);
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x00008913 File Offset: 0x00006B13
	private void OnDisable()
	{
		FastUpdateManager.fixedUpdate8.Remove(this);
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x00038C0C File Offset: 0x00036E0C
	public void ManagedUpdate()
	{
		if (this.rigidbody == null || this.target == null)
		{
			return;
		}
		if (Vector3.Distance(this.rigidbody.position, this.target.position) > this.maxDistance)
		{
			this.rigidbody.position = Vector3.MoveTowards(this.target.position, this.rigidbody.position, this.maxDistance);
			if (this.cancelVelocity)
			{
				this.rigidbody.velocity = Vector3.zero;
				this.rigidbody.angularVelocity = Vector3.zero;
			}
		}
	}

	public float maxDistance = 5f;

	public Rigidbody rigidbody;

	public Rigidbody target;

	public bool cancelVelocity = true;
}
