using System;
using UnityEngine;
using UnityEngine.Events;

public class HitEvent : MonoBehaviour, IHit
{
	// Token: 0x060007CE RID: 1998 RVA: 0x00007BDD File Offset: 0x00005DDD
	public void Hit(Vector3 velocity, bool isHeavy = false)
	{
		if (this.cooldown == 0f || Time.time > this.lastHitTime + this.cooldown)
		{
			this.onHit.Invoke();
			this.lastHitTime = Time.time;
		}
	}

	public UnityEvent onHit;

	public float cooldown;

	private float lastHitTime = -1f;
}
