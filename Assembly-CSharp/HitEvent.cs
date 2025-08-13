using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000193 RID: 403
public class HitEvent : MonoBehaviour, IHit
{
	// Token: 0x0600078E RID: 1934 RVA: 0x000078E3 File Offset: 0x00005AE3
	public void Hit(Vector3 velocity, bool isHeavy = false)
	{
		if (this.cooldown == 0f || Time.time > this.lastHitTime + this.cooldown)
		{
			this.onHit.Invoke();
			this.lastHitTime = Time.time;
		}
	}

	// Token: 0x04000A0D RID: 2573
	public UnityEvent onHit;

	// Token: 0x04000A0E RID: 2574
	public float cooldown;

	// Token: 0x04000A0F RID: 2575
	private float lastHitTime = -1f;
}
