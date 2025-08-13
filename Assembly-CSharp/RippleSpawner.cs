using System;
using UnityEngine;

// Token: 0x020000FC RID: 252
public class RippleSpawner : MonoBehaviour, IManagedUpdate
{
	// Token: 0x0600052E RID: 1326 RVA: 0x0001BCF8 File Offset: 0x00019EF8
	private void Start()
	{
		this.nextTime = Time.time + Random.value / this.speed;
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x0001BD12 File Offset: 0x00019F12
	private void OnEnable()
	{
		FastUpdateManager.updateEveryNonFixed.Add(this);
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x0001BD1F File Offset: 0x00019F1F
	private void OnDisable()
	{
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x0001BD30 File Offset: 0x00019F30
	public void ManagedUpdate()
	{
		if (this.nextTime <= Time.time)
		{
			this.nextTime = Time.time + (0.5f + Random.value) / this.speed;
			EffectsManager.e.Ripple(base.transform.position, 1);
		}
	}

	// Token: 0x04000722 RID: 1826
	public float speed;

	// Token: 0x04000723 RID: 1827
	private float nextTime;
}
