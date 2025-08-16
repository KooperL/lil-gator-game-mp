using System;
using UnityEngine;
using UnityEngine.U2D;

public class ParticlesScreenSpace : MonoBehaviour
{
	// Token: 0x06000670 RID: 1648 RVA: 0x00031904 File Offset: 0x0002FB04
	private void OnValidate()
	{
		if (this.pixelPerfectCamera == null)
		{
			this.pixelPerfectCamera = global::UnityEngine.Object.FindObjectOfType<PixelPerfectCamera>();
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

	// Token: 0x06000671 RID: 1649 RVA: 0x00006A6C File Offset: 0x00004C6C
	private void Update()
	{
		this.UpdateSize();
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x00031960 File Offset: 0x0002FB60
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
