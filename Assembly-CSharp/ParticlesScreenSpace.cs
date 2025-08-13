using System;
using UnityEngine;
using UnityEngine.U2D;

// Token: 0x0200014C RID: 332
public class ParticlesScreenSpace : MonoBehaviour
{
	// Token: 0x06000636 RID: 1590 RVA: 0x00030384 File Offset: 0x0002E584
	private void OnValidate()
	{
		if (this.pixelPerfectCamera == null)
		{
			this.pixelPerfectCamera = Object.FindObjectOfType<PixelPerfectCamera>();
		}
		if (this.renderer == null)
		{
			this.renderer = base.GetComponent<ParticleSystemRenderer>();
		}
		if (this.mainCamera == null)
		{
			this.mainCamera = Camera.main;
		}
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x000067A6 File Offset: 0x000049A6
	private void Update()
	{
		this.UpdateSize();
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x000303E0 File Offset: 0x0002E5E0
	[ContextMenu("Update Size")]
	private void UpdateSize()
	{
		if (this.pixelPerfectCamera != null && this.pixelPerfectCamera.enabled)
		{
			this.apparentSize = this.pixelSize * (float)this.pixelPerfectCamera.pixelRatio / (float)Screen.width;
		}
		else
		{
			this.apparentSize = this.pixelSize / (float)Screen.width;
		}
		this.renderer.maxParticleSize = this.apparentSize;
		this.renderer.minParticleSize = this.apparentSize;
	}

	// Token: 0x04000860 RID: 2144
	public ParticleSystemRenderer renderer;

	// Token: 0x04000861 RID: 2145
	public PixelPerfectCamera pixelPerfectCamera;

	// Token: 0x04000862 RID: 2146
	public Camera mainCamera;

	// Token: 0x04000863 RID: 2147
	public float pixelSize;

	// Token: 0x04000864 RID: 2148
	public float apparentSize;
}
