using System;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Camera))]
public class BlitToScreen : MonoBehaviour
{
	// Token: 0x0600115F RID: 4447 RVA: 0x0000EDE8 File Offset: 0x0000CFE8
	private void Awake()
	{
		this.camera = base.GetComponent<Camera>();
		this.LoadScreen(new Vector2(384f, 216f));
	}

	// Token: 0x06001160 RID: 4448 RVA: 0x00058038 File Offset: 0x00056238
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

	// Token: 0x06001161 RID: 4449 RVA: 0x0000EE0B File Offset: 0x0000D00B
	private void OnPreRender()
	{
		this.camera.targetTexture = this.screenTexture;
	}

	// Token: 0x06001162 RID: 4450 RVA: 0x0000EE1E File Offset: 0x0000D01E
	private void OnPostRender()
	{
		this.camera.targetTexture = null;
	}

	private Camera camera;

	private CommandBuffer commandBuffer;

	public Material material;

	public RenderTexture screenTexture;
}
