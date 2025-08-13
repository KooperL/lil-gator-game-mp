using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000153 RID: 339
[ExecuteInEditMode]
[RequireComponent(typeof(Light))]
public class SetShadowMapAsGlobalTexture : MonoBehaviour
{
	// Token: 0x06000656 RID: 1622 RVA: 0x000068F5 File Offset: 0x00004AF5
	private void OnEnable()
	{
		this.lightComponent = base.GetComponent<Light>();
		this.SetupCommandBuffer();
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x00006909 File Offset: 0x00004B09
	private void OnDisable()
	{
		this.lightComponent.RemoveCommandBuffer(1, this.commandBuffer);
		this.ReleaseCommandBuffer();
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x00030730 File Offset: 0x0002E930
	private void SetupCommandBuffer()
	{
		this.commandBuffer = new CommandBuffer();
		RenderTargetIdentifier renderTargetIdentifier = 1;
		this.commandBuffer.SetGlobalTexture(this.textureSemanticName, renderTargetIdentifier);
		this.lightComponent.AddCommandBuffer(1, this.commandBuffer);
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x00006923 File Offset: 0x00004B23
	private void ReleaseCommandBuffer()
	{
		this.commandBuffer.Clear();
	}

	// Token: 0x0400087E RID: 2174
	public string textureSemanticName = "_SunCascadedShadowMap";

	// Token: 0x0400087F RID: 2175
	private RenderTexture shadowMapRenderTexture;

	// Token: 0x04000880 RID: 2176
	private CommandBuffer commandBuffer;

	// Token: 0x04000881 RID: 2177
	private Light lightComponent;
}
