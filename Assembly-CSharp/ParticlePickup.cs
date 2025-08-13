using System;
using UnityEngine;

// Token: 0x020000F8 RID: 248
public class ParticlePickup : MonoBehaviour, IOnTimeout
{
	// Token: 0x0600051F RID: 1311 RVA: 0x0001B69F File Offset: 0x0001989F
	private void OnValidate()
	{
		if (this.particleSystem == null)
		{
			this.particleSystem = base.GetComponent<ParticleSystem>();
		}
		if (this.particleSystemRenderer == null)
		{
			this.particleSystemRenderer = base.GetComponent<ParticleSystemRenderer>();
		}
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x0001B6D8 File Offset: 0x000198D8
	private void Start()
	{
		this.particles = new ParticleSystem.Particle[this.particleSystem.main.maxParticles];
		this.isPickingUp = new bool[this.particleSystem.main.maxParticles];
		this.pickupRadiusSqr = this.pickupRadius * this.pickupRadius;
		this.delayTime = Time.time + this.delay;
		this.autoPickupTime = Time.time + this.autoPickupDelay;
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x0001B758 File Offset: 0x00019958
	private void Update()
	{
		if (!this.isQuick && Time.time - TriggerPickup.quickPickupTime < 0.1f)
		{
			this.delayTime -= 0.5f * this.delay;
			this.autoPickupTime = this.delayTime;
			this.isQuick = true;
		}
		if (Time.time < this.delayTime)
		{
			return;
		}
		Vector3 vector = Player.Position;
		if (this.autoPickup)
		{
			if (Time.time < this.autoPickupTime && Vector3.Distance(base.transform.position, vector) > 17f)
			{
				this.resource.Amount += Mathf.RoundToInt(this.rewardPerPickup * (float)this.particleSystem.main.maxParticles);
				if (this.persistentObject != null)
				{
					this.persistentObject.SaveTrue();
				}
				Object.Destroy(base.gameObject);
				return;
			}
			if (Time.time > this.autoPickupTime && this.particleSystem.collision.enabled)
			{
				this.particleSystem.collision.enabled = false;
				ParticleSystem.LimitVelocityOverLifetimeModule limitVelocityOverLifetime = this.particleSystem.limitVelocityOverLifetime;
				limitVelocityOverLifetime.limit = 0f;
				limitVelocityOverLifetime.drag = 1f;
			}
			this.pickupRadiusSqr += 3f * Time.deltaTime;
		}
		if ((Time.time < this.autoPickupTime || !this.autoPickup) && this.particleSystemRenderer.bounds.SqrDistance(vector) > this.pickupRadiusSqr)
		{
			return;
		}
		int num = this.particleSystem.GetParticles(this.particles);
		if (num == 0)
		{
			Object.Destroy(base.gameObject);
			if (this.persistentObject != null)
			{
				this.persistentObject.SaveTrue();
			}
			return;
		}
		vector = Quaternion.Inverse(base.transform.rotation) * (vector - base.transform.position);
		bool flag = false;
		for (int i = 0; i < num; i++)
		{
			Vector3 vector2 = vector - this.particles[i].position;
			if (Time.time >= this.autoPickupTime || vector2.sqrMagnitude <= this.pickupRadiusSqr || this.particles[i].position.y <= -0.5f)
			{
				float magnitude = vector2.magnitude;
				if (magnitude < 0.25f)
				{
					this.particles[i].remainingLifetime = 0f;
					this.rewardOwed += this.rewardPerPickup;
				}
				else
				{
					this.particles[i].velocity = Vector3.RotateTowards(this.particles[i].velocity, 50f * vector2 / magnitude, 31.415928f * Time.deltaTime, this.pickupAcc * Time.deltaTime);
				}
				flag = true;
			}
		}
		if (flag)
		{
			this.particleSystem.SetParticles(this.particles, num);
		}
		int num2 = 0;
		if (this.rewardOwed > 1f)
		{
			num2 = Mathf.FloorToInt(this.rewardOwed);
			this.rewardOwed -= (float)num2;
			this.rewardGiven += num2;
		}
		if (num2 > 0)
		{
			this.resource.Amount += num2;
			if (this.audioSourceVariance != null)
			{
				this.audioSourceVariance.Play();
			}
		}
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x0001BADC File Offset: 0x00019CDC
	public void OnTimeout()
	{
		this.resource.SetAmountSecret(this.resource.Amount + Mathf.RoundToInt(this.rewardPerPickup * (float)this.particleSystem.main.maxParticles) - this.rewardGiven);
	}

	// Token: 0x04000706 RID: 1798
	public ParticleSystem particleSystem;

	// Token: 0x04000707 RID: 1799
	public ParticleSystemRenderer particleSystemRenderer;

	// Token: 0x04000708 RID: 1800
	public float pickupRadius = 0.5f;

	// Token: 0x04000709 RID: 1801
	private float pickupRadiusSqr;

	// Token: 0x0400070A RID: 1802
	public float rewardPerPickup = 0.5f;

	// Token: 0x0400070B RID: 1803
	private float rewardOwed;

	// Token: 0x0400070C RID: 1804
	private int rewardGiven;

	// Token: 0x0400070D RID: 1805
	public ItemResource resource;

	// Token: 0x0400070E RID: 1806
	private ParticleSystem.Particle[] particles;

	// Token: 0x0400070F RID: 1807
	private bool[] isPickingUp;

	// Token: 0x04000710 RID: 1808
	public float delay = 0.5f;

	// Token: 0x04000711 RID: 1809
	private float delayTime = -1f;

	// Token: 0x04000712 RID: 1810
	public bool autoPickup = true;

	// Token: 0x04000713 RID: 1811
	public float autoPickupDelay = 3f;

	// Token: 0x04000714 RID: 1812
	private float autoPickupTime = -1f;

	// Token: 0x04000715 RID: 1813
	public float pickupAcc = 30f;

	// Token: 0x04000716 RID: 1814
	public AudioSourceVariance audioSourceVariance;

	// Token: 0x04000717 RID: 1815
	public PersistentObject persistentObject;

	// Token: 0x04000718 RID: 1816
	private bool isQuick;
}
