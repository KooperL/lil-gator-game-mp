using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000137 RID: 311
public class HitEvent : MonoBehaviour, IHit
{
	// Token: 0x06000669 RID: 1641 RVA: 0x00021151 File Offset: 0x0001F351
	public void Hit(Vector3 velocity, bool isHeavy = false)
	{
		if (this.cooldown == 0f || Time.time > this.lastHitTime + this.cooldown)
		{
			this.onHit.Invoke();
			this.lastHitTime = Time.time;
		}
	}

	// Token: 0x040008A1 RID: 2209
	public UnityEvent onHit;

	// Token: 0x040008A2 RID: 2210
	public float cooldown;

	// Token: 0x040008A3 RID: 2211
	private float lastHitTime = -1f;
}
