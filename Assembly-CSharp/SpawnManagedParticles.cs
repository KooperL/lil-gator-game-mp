using System;
using UnityEngine;

// Token: 0x02000104 RID: 260
public class SpawnManagedParticles : MonoBehaviour
{
	// Token: 0x06000554 RID: 1364 RVA: 0x0001C550 File Offset: 0x0001A750
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

	// Token: 0x04000752 RID: 1874
	public SpawnManagedParticles.ParticleType particleType;

	// Token: 0x04000753 RID: 1875
	public int particleCount = 5;

	// Token: 0x04000754 RID: 1876
	public bool overrideVelocity;

	// Token: 0x04000755 RID: 1877
	[ConditionalHide("overrideVelocity", true)]
	public Vector3 velocity;

	// Token: 0x020003AB RID: 939
	public enum ParticleType
	{
		// Token: 0x04001B62 RID: 7010
		Dust,
		// Token: 0x04001B63 RID: 7011
		FloorDust,
		// Token: 0x04001B64 RID: 7012
		Ripples
	}
}
