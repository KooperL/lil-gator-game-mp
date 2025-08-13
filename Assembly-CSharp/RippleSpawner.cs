using System;
using UnityEngine;

// Token: 0x0200014F RID: 335
public class RippleSpawner : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06000640 RID: 1600 RVA: 0x0000681B File Offset: 0x00004A1B
	private void Start()
	{
		this.nextTime = Time.time + Random.value / this.speed;
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x00002ADC File Offset: 0x00000CDC
	private void OnEnable()
	{
		FastUpdateManager.updateEveryNonFixed.Add(this);
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x0000285D File Offset: 0x00000A5D
	private void OnDisable()
	{
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x00030460 File Offset: 0x0002E660
	public void ManagedUpdate()
	{
		if (this.nextTime <= Time.time)
		{
			this.nextTime = Time.time + (0.5f + Random.value) / this.speed;
			EffectsManager.e.Ripple(base.transform.position, 1);
		}
	}

	// Token: 0x04000869 RID: 2153
	public float speed;

	// Token: 0x0400086A RID: 2154
	private float nextTime;
}
