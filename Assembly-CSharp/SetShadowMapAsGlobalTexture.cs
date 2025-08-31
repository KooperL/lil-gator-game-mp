using System;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
[RequireComponent(typeof(Light))]
public class SetShadowMapAsGlobalTexture : MonoBehaviour
{
	// Token: 0x06000544 RID: 1348 RVA: 0x0001C0D7 File Offset: 0x0001A2D7
	private void OnEnable()
	{
		this.lightComponent = base.GetComponent<Light>();
		this.SetupCommandBuffer();
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x0001C0EB File Offset: 0x0001A2EB
	private void OnDisable()
	{
		this.lightComponent.RemoveCommandBuffer(LightEvent.AfterShadowMap, this.commandBuffer);
		this.ReleaseCommandBuffer();
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x0001C108 File Offset: 0x0001A308
	private void SetupCommandBuffer()
	{
		this.commandBuffer = new CommandBuffer();
		RenderTargetIdentifier renderTargetIdentifier = BuiltinRenderTextureType.CurrentActive;
		this.commandBuffer.SetGlobalTexture(this.textureSemanticName, renderTargetIdentifier);
		this.lightComponent.AddCommandBuffer(LightEvent.AfterShadowMap, this.commandBuffer);
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x0001C14B File Offset: 0x0001A34B
	private void ReleaseCommandBuffer()
	{
		this.commandBuffer.Clear();
	}

	public string textureSemanticName = "_SunCascadedShadowMap";

	private RenderTexture shadowMapRenderTexture;

	private CommandBuffer commandBuffer;

	private Light lightComponent;
}
