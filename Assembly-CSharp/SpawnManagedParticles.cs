using System;
using UnityEngine;

public class SpawnManagedParticles : MonoBehaviour
{
	// Token: 0x060006A0 RID: 1696 RVA: 0x0003219C File Offset: 0x0003039C
	public void OnEnable()
	{
		if (Time.timeSinceLevelLoad < 0.5f)
		{
			return;
		}
		SpawnManagedParticles.ParticleType particleType = this.particleType;
		if (particleType != SpawnManagedParticles.ParticleType.Dust)
		{
			if (particleType != SpawnManagedParticles.ParticleType.FloorDust)
			{
				return;
			}
			EffectsManager.e.FloorDust(base.transform.position, this.particleCount, Vector3.up);
			return;
		}
		else
		{
			if (this.overrideVelocity)
			{
				EffectsManager.e.Dust(base.transform.position, this.particleCount, this.velocity, 0f);
				return;
			}
			EffectsManager.e.Dust(base.transform.position, this.particleCount);
			return;
		}
	}

	public SpawnManagedParticles.ParticleType particleType;

	public int particleCount = 5;

	public bool overrideVelocity;

	[ConditionalHide("overrideVelocity", true)]
	public Vector3 velocity;

	public enum ParticleType
	{
		Dust,
		FloorDust,
		Ripples
	}
}
