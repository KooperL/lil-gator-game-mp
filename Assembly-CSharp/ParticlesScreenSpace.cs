using System;
using UnityEngine;
using UnityEngine.U2D;

public class ParticlesScreenSpace : MonoBehaviour
{
	// Token: 0x06000524 RID: 1316 RVA: 0x0001BB90 File Offset: 0x00019D90
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

	// Token: 0x06000525 RID: 1317 RVA: 0x0001BBE9 File Offset: 0x00019DE9
	private void Update()
	{
		this.UpdateSize();
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x0001BBF4 File Offset: 0x00019DF4
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

	public ParticleSystemRenderer renderer;

	public PixelPerfectCamera pixelPerfectCamera;

	public Camera mainCamera;

	public float pixelSize;

	public float apparentSize;
}
