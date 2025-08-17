using System;
using UnityEngine;

public class LoopParticles : MonoBehaviour, IManagedUpdate, ICameraCut
{
	// Token: 0x0600065C RID: 1628 RVA: 0x00031330 File Offset: 0x0002F530
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

	// Token: 0x0600065D RID: 1629 RVA: 0x00006936 File Offset: 0x00004B36
	private void OnDisable()
	{
		FastUpdateManager.updateEvery4.Remove(this);
		CameraCutDetector.subscribers.Remove(this);
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x00006950 File Offset: 0x00004B50
	public void OnCameraCut()
	{
		this.ManagedUpdate();
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x0003139C File Offset: 0x0002F59C
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

	// Token: 0x06000660 RID: 1632 RVA: 0x00031468 File Offset: 0x0002F668
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
