using System;
using UnityEngine;
using UnityEngine.Events;

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

	public UnityEvent onHit;

	public float cooldown;

	private float lastHitTime = -1f;
}
