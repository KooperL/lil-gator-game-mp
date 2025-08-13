using System;
using UnityEngine;

// Token: 0x0200014B RID: 331
public class ParticlePickup : MonoBehaviour, IOnTimeout
{
	// Token: 0x06000631 RID: 1585 RVA: 0x00006770 File Offset: 0x00004970
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

	// Token: 0x06000632 RID: 1586 RVA: 0x0002FECC File Offset: 0x0002E0CC
	private void Start()
	{
		this.particles = new ParticleSystem.Particle[this.particleSystem.main.maxParticles];
		this.isPickingUp = new bool[this.particleSystem.main.maxParticles];
		this.pickupRadiusSqr = this.pickupRadius * this.pickupRadius;
		this.delayTime = Time.time + this.delay;
		this.autoPickupTime = Time.time + this.autoPickupDelay;
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x0002FF4C File Offset: 0x0002E14C
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

	// Token: 0x06000634 RID: 1588 RVA: 0x000302D0 File Offset: 0x0002E4D0
	public void OnTimeout()
	{
		this.resource.SetAmountSecret(this.resource.Amount + Mathf.RoundToInt(this.rewardPerPickup * (float)this.particleSystem.main.maxParticles) - this.rewardGiven);
	}

	// Token: 0x0400084D RID: 2125
	public ParticleSystem particleSystem;

	// Token: 0x0400084E RID: 2126
	public ParticleSystemRenderer particleSystemRenderer;

	// Token: 0x0400084F RID: 2127
	public float pickupRadius = 0.5f;

	// Token: 0x04000850 RID: 2128
	private float pickupRadiusSqr;

	// Token: 0x04000851 RID: 2129
	public float rewardPerPickup = 0.5f;

	// Token: 0x04000852 RID: 2130
	private float rewardOwed;

	// Token: 0x04000853 RID: 2131
	private int rewardGiven;

	// Token: 0x04000854 RID: 2132
	public ItemResource resource;

	// Token: 0x04000855 RID: 2133
	private ParticleSystem.Particle[] particles;

	// Token: 0x04000856 RID: 2134
	private bool[] isPickingUp;

	// Token: 0x04000857 RID: 2135
	public float delay = 0.5f;

	// Token: 0x04000858 RID: 2136
	private float delayTime = -1f;

	// Token: 0x04000859 RID: 2137
	public bool autoPickup = true;

	// Token: 0x0400085A RID: 2138
	public float autoPickupDelay = 3f;

	// Token: 0x0400085B RID: 2139
	private float autoPickupTime = -1f;

	// Token: 0x0400085C RID: 2140
	public float pickupAcc = 30f;

	// Token: 0x0400085D RID: 2141
	public AudioSourceVariance audioSourceVariance;

	// Token: 0x0400085E RID: 2142
	public PersistentObject persistentObject;

	// Token: 0x0400085F RID: 2143
	private bool isQuick;
}
