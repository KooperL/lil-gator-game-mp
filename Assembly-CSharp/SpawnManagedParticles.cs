using System;
using UnityEngine;

// Token: 0x02000157 RID: 343
public class SpawnManagedParticles : MonoBehaviour
{
	// Token: 0x06000666 RID: 1638 RVA: 0x00030A8C File Offset: 0x0002EC8C
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

	// Token: 0x04000899 RID: 2201
	public SpawnManagedParticles.ParticleType particleType;

	// Token: 0x0400089A RID: 2202
	public int particleCount = 5;

	// Token: 0x0400089B RID: 2203
	public bool overrideVelocity;

	// Token: 0x0400089C RID: 2204
	[ConditionalHide("overrideVelocity", true)]
	public Vector3 velocity;

	// Token: 0x02000158 RID: 344
	public enum ParticleType
	{
		// Token: 0x0400089E RID: 2206
		Dust,
		// Token: 0x0400089F RID: 2207
		FloorDust,
		// Token: 0x040008A0 RID: 2208
		Ripples
	}
}
