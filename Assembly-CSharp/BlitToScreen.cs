using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x0200037C RID: 892
[RequireComponent(typeof(Camera))]
public class BlitToScreen : MonoBehaviour
{
	// Token: 0x060010FF RID: 4351 RVA: 0x0000E9FF File Offset: 0x0000CBFF
	private void Awake()
	{
		this.camera = base.GetComponent<Camera>();
		this.LoadScreen(new Vector2(384f, 216f));
	}

	// Token: 0x06001100 RID: 4352 RVA: 0x00056078 File Offset: 0x00054278
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
		this.screenTexture = new RenderTexture(Mathf.FloorToInt(screenSize.x), Mathf.FloorToInt(screenSize.y), 24, 0);
		this.screenTexture.filterMode = 0;
		this.screenTexture.Create();
		this.material.mainTexture = this.screenTexture;
		this.commandBuffer = new CommandBuffer();
		this.commandBuffer.name = "Blit to screen";
		this.commandBuffer.SetRenderTarget(null);
		this.commandBuffer.ClearRenderTarget(false, true, Color.black);
		this.commandBuffer.Blit(this.screenTexture, null, this.material);
		this.camera.AddCommandBuffer(20, this.commandBuffer);
		this.camera.targetTexture = this.screenTexture;
		HighlightsFX component = base.GetComponent<HighlightsFX>();
		if (component)
		{
			component.RecreateCommandBuffer();
		}
	}

	// Token: 0x06001101 RID: 4353 RVA: 0x0000EA22 File Offset: 0x0000CC22
	private void OnPreRender()
	{
		this.camera.targetTexture = this.screenTexture;
	}

	// Token: 0x06001102 RID: 4354 RVA: 0x0000EA35 File Offset: 0x0000CC35
	private void OnPostRender()
	{
		this.camera.targetTexture = null;
	}

	// Token: 0x040015FA RID: 5626
	private Camera camera;

	// Token: 0x040015FB RID: 5627
	private CommandBuffer commandBuffer;

	// Token: 0x040015FC RID: 5628
	public Material material;

	// Token: 0x040015FD RID: 5629
	public RenderTexture screenTexture;
}
