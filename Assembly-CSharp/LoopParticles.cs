using System;
using UnityEngine;

public class LoopParticles : MonoBehaviour, IManagedUpdate, ICameraCut
{
	// Token: 0x06000510 RID: 1296 RVA: 0x0001B2FC File Offset: 0x000194FC
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

	// Token: 0x06000511 RID: 1297 RVA: 0x0001B365 File Offset: 0x00019565
	private void OnDisable()
	{
		FastUpdateManager.updateEvery4.Remove(this);
		CameraCutDetector.subscribers.Remove(this);
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x0001B37F File Offset: 0x0001957F
	public void OnCameraCut()
	{
		this.ManagedUpdate();
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x0001B388 File Offset: 0x00019588
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

	// Token: 0x06000514 RID: 1300 RVA: 0x0001B454 File Offset: 0x00019654
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

	public Vector3 loopSize;

	private Bounds bounds;

	private ParticleSystem particleSystem;

	private ParticleSystem.Particle[] particles;

	public int smoothing;

	private int[] smoothCounter;
}
