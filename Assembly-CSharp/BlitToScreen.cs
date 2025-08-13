using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x020002A2 RID: 674
[RequireComponent(typeof(Camera))]
public class BlitToScreen : MonoBehaviour
{
	// Token: 0x06000E3B RID: 3643 RVA: 0x000445A2 File Offset: 0x000427A2
	private void Awake()
	{
		this.camera = base.GetComponent<Camera>();
		this.LoadScreen(new Vector2(384f, 216f));
	}

	// Token: 0x06000E3C RID: 3644 RVA: 0x000445C8 File Offset: 0x000427C8
	public void LoadScreen(Vector2 screenSize)
	{
		if (this.screenTexture != null)
		{
			if ((float)this.screenTexture.height == screenSize.y * 16f && (float)this.screenTexture.width == screenSize.x * 16f)
			{
				return;
			}
			this.screenTexture.Release();
		}
		this.screenTexture = new RenderTexture(Mathf.FloorToInt(screenSize.x), Mathf.FloorToInt(screenSize.y), 24, RenderTextureFormat.ARGB32);
		this.screenTexture.filterMode = FilterMode.Point;
		this.screenTexture.Create();
		this.material.mainTexture = this.screenTexture;
		this.commandBuffer = new CommandBuffer();
		this.commandBuffer.name = "Blit to screen";
		this.commandBuffer.SetRenderTarget(null);
		this.commandBuffer.ClearRenderTarget(false, true, Color.black);
		this.commandBuffer.Blit(this.screenTexture, null, this.material);
		this.camera.AddCommandBuffer(CameraEvent.AfterEverything, this.commandBuffer);
		this.camera.targetTexture = this.screenTexture;
		HighlightsFX component = base.GetComponent<HighlightsFX>();
		if (component)
		{
			component.RecreateCommandBuffer();
		}
	}

	// Token: 0x06000E3D RID: 3645 RVA: 0x00044704 File Offset: 0x00042904
	private void OnPreRender()
	{
		this.camera.targetTexture = this.screenTexture;
	}

	// Token: 0x06000E3E RID: 3646 RVA: 0x00044717 File Offset: 0x00042917
	private void OnPostRender()
	{
		this.camera.targetTexture = null;
	}

	// Token: 0x0400129D RID: 4765
	private Camera camera;

	// Token: 0x0400129E RID: 4766
	private CommandBuffer commandBuffer;

	// Token: 0x0400129F RID: 4767
	public Material material;

	// Token: 0x040012A0 RID: 4768
	public RenderTexture screenTexture;
}
