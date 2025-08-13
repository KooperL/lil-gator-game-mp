using System;
using UnityEngine;

// Token: 0x020001BE RID: 446
public class KeepWithinDistance : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06000878 RID: 2168 RVA: 0x000085D7 File Offset: 0x000067D7
	private void Start()
	{
		this.ManagedUpdate();
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x000085DF File Offset: 0x000067DF
	private void OnEnable()
	{
		FastUpdateManager.fixedUpdate8.Add(this);
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x000085EC File Offset: 0x000067EC
	private void OnDisable()
	{
		FastUpdateManager.fixedUpdate8.Remove(this);
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x000372C0 File Offset: 0x000354C0
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

	// Token: 0x04000B0C RID: 2828
	public float maxDistance = 5f;

	// Token: 0x04000B0D RID: 2829
	public Rigidbody rigidbody;

	// Token: 0x04000B0E RID: 2830
	public Rigidbody target;

	// Token: 0x04000B0F RID: 2831
	public bool cancelVelocity = true;
}
