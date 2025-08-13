using System;
using UnityEngine;

// Token: 0x02000148 RID: 328
public class LoopParticles : MonoBehaviour, IManagedUpdate, ICameraCut
{
	// Token: 0x06000622 RID: 1570 RVA: 0x0002FC34 File Offset: 0x0002DE34
	private void OnEnable()
	{
		this.particleSystem = base.GetComponent<ParticleSystem>();
		this.particles = new ParticleSystem.Particle[this.particleSystem.main.maxParticles];
		if (this.smoothing > 0)
		{
			this.smoothCounter = new int[this.particles.Length];
		}
		FastUpdateManager.updateEvery4.Add(this);
		CameraCutDetector.subscribers.Add(this);
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x00006670 File Offset: 0x00004870
	private void OnDisable()
	{
		FastUpdateManager.updateEvery4.Remove(this);
		CameraCutDetector.subscribers.Remove(this);
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x0000668A File Offset: 0x0000488A
	public void OnCameraCut()
	{
		this.ManagedUpdate();
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x0002FCA0 File Offset: 0x0002DEA0
	public void ManagedUpdate()
	{
		int num = this.particleSystem.GetParticles(this.particles);
		Vector3 position = base.transform.position;
		this.bounds = new Bounds(position, this.loopSize);
		for (int i = 0; i < num; i++)
		{
			Vector3 vector = this.particles[i].position;
			if (!this.bounds.Contains(vector))
			{
				if (this.smoothing > 0)
				{
					this.smoothCounter[i]++;
					if (this.smoothCounter[i] < this.smoothing + 1)
					{
						goto IL_00A3;
					}
					this.smoothCounter[i] = 0;
				}
				vector = this.LoopBounds(vector);
				this.particles[i].position = vector;
			}
			IL_00A3:;
		}
		this.particleSystem.SetParticles(this.particles, num);
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x0002FD6C File Offset: 0x0002DF6C
	private Vector3 LoopBounds(Vector3 point)
	{
		for (int i = 0; i < 3; i++)
		{
			if (this.bounds.min[i] != this.bounds.max[i])
			{
				while (this.bounds.min[i] > point[i])
				{
					ref Vector3 ptr = ref point;
					int num = i;
					ptr[num] += this.loopSize[i];
				}
				while (this.bounds.max[i] < point[i])
				{
					ref Vector3 ptr = ref point;
					int num = i;
					ptr[num] -= this.loopSize[i];
				}
			}
		}
		return point;
	}

	// Token: 0x04000842 RID: 2114
	public Vector3 loopSize;

	// Token: 0x04000843 RID: 2115
	private Bounds bounds;

	// Token: 0x04000844 RID: 2116
	private ParticleSystem particleSystem;

	// Token: 0x04000845 RID: 2117
	private ParticleSystem.Particle[] particles;

	// Token: 0x04000846 RID: 2118
	public int smoothing;

	// Token: 0x04000847 RID: 2119
	private int[] smoothCounter;
}
