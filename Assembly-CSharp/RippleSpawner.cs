using System;
using UnityEngine;

public class RippleSpawner : MonoBehaviour, IManagedUpdate
{
	// Token: 0x0600067A RID: 1658 RVA: 0x00006AE1 File Offset: 0x00004CE1
	private void Start()
	{
		this.nextTime = Time.time + global::UnityEngine.Random.value / this.speed;
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x00002B40 File Offset: 0x00000D40
	private void OnEnable()
	{
		FastUpdateManager.updateEveryNonFixed.Add(this);
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x000028C1 File Offset: 0x00000AC1
	private void OnDisable()
	{
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x000319E0 File Offset: 0x0002FBE0
	public void ManagedUpdate()
	{
		if (this.nextTime <= Time.time)
		{
			this.nextTime = Time.time + (0.5f + global::UnityEngine.Random.value) / this.speed;
			EffectsManager.e.Ripple(base.transform.position, 1);
		}
	}

	public float speed;

	private float nextTime;
}
