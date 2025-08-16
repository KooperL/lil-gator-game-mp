using System;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
[RequireComponent(typeof(Light))]
public class SetShadowMapAsGlobalTexture : MonoBehaviour
{
	// Token: 0x06000690 RID: 1680 RVA: 0x00006BBB File Offset: 0x00004DBB
	private void OnEnable()
	{
		this.lightComponent = base.GetComponent<Light>();
		this.SetupCommandBuffer();
	}

	// Token: 0x06000691 RID: 1681 RVA: 0x00006BCF File Offset: 0x00004DCF
	private void OnDisable()
	{
		this.lightComponent.RemoveCommandBuffer(LightEvent.AfterShadowMap, this.commandBuffer);
		this.ReleaseCommandBuffer();
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x00031CB0 File Offset: 0x0002FEB0
	private void SetupCommandBuffer()
	{
		this.commandBuffer = new CommandBuffer();
		RenderTargetIdentifier renderTargetIdentifier = BuiltinRenderTextureType.CurrentActive;
		this.commandBuffer.SetGlobalTexture(this.textureSemanticName, renderTargetIdentifier);
		this.lightComponent.AddCommandBuffer(LightEvent.AfterShadowMap, this.commandBuffer);
	}

	// Token: 0x06000693 RID: 1683 RVA: 0x00006BE9 File Offset: 0x00004DE9
	private void ReleaseCommandBuffer()
	{
		this.commandBuffer.Clear();
	}

	public string textureSemanticName = "_SunCascadedShadowMap";

	private RenderTexture shadowMapRenderTexture;

	private CommandBuffer commandBuffer;

	private Light lightComponent;
}
