using System;
using UnityEngine;

public class KeepWithinDistance : MonoBehaviour, IManagedUpdate
{
	// Token: 0x0600072E RID: 1838 RVA: 0x00023F0D File Offset: 0x0002210D
	private void Start()
	{
		this.ManagedUpdate();
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x00023F15 File Offset: 0x00022115
	private void OnEnable()
	{
		FastUpdateManager.fixedUpdate8.Add(this);
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x00023F22 File Offset: 0x00022122
	private void OnDisable()
	{
		FastUpdateManager.fixedUpdate8.Remove(this);
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x00023F30 File Offset: 0x00022130
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
