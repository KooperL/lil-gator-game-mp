using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000146 RID: 326
public class PostProcessFog : MonoBehaviour
{
	// Token: 0x06000618 RID: 1560 RVA: 0x0002F880 File Offset: 0x0002DA80
	private void OnEnable()
	{
		if (!this.isInitialized)
		{
			this.camera = MainCamera.c;
			this.commandBuffer = new CommandBuffer();
			this.commandBuffer.name = "Post Process Fog";
			this.block = new MaterialPropertyBlock();
			this.beforeEverythingBuffer = new CommandBuffer();
			this.commandBuffer.name = "Disable fog for depth rendering";
			this.beforeEverythingBuffer.DisableShaderKeyword("FOG_EXP");
		}
		this.UpdateCommandBuffer();
		this.camera.AddCommandBuffer(10, this.beforeEverythingBuffer);
		this.camera.AddCommandBuffer(14, this.commandBuffer);
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x000065BF File Offset: 0x000047BF
	private void OnDisable()
	{
		if (this.camera != null)
		{
			this.camera.RemoveCommandBuffer(10, this.beforeEverythingBuffer);
			this.camera.RemoveCommandBuffer(14, this.commandBuffer);
		}
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x000065F5 File Offset: 0x000047F5
	private void LateUpdate()
	{
		if (MainCamera.c != null)
		{
			this.postFogMaterial.SetMatrix(PostProcessFog._InverseView, MainCamera.c.cameraToWorldMatrix);
		}
		this.UpdateCommandBuffer();
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x0002F920 File Offset: 0x0002DB20
	private void UpdateCommandBuffer()
	{
		this.commandBuffer.Clear();
		this.commandBuffer.SetGlobalTexture(PostProcessFog._CameraDepthTexture, 3);
		this.commandBuffer.GetTemporaryRT(PostProcessFog.fogBufferRTID, -1, -1, 0, 0, 28, 0, 1, false, 0, true);
		this.commandBuffer.SetRenderTarget(PostProcessFog.fogBufferRTID);
		this.commandBuffer.ClearRenderTarget(false, true, Color.clear);
		GeometryUtility.CalculateFrustumPlanes(MainCamera.c, this.cameraFrustumPlanes);
		int num = 0;
		int num2 = 0;
		while (num2 < PostProcessFog.fogLights.Count && num < this.matrices.Length)
		{
			Vector3 vector;
			float num3;
			float num4;
			float num5;
			if (PostProcessFog.fogLights[num2].GetLightData(this.cameraFrustumPlanes, out vector, out num3, out num4, out num5))
			{
				this.matrices[num] = Matrix4x4.TRS(vector, Quaternion.identity, num3 * Vector3.one);
				this.lightData[num] = new Vector4(num3, num4, num5, 0f);
				num++;
			}
			num2++;
		}
		this.block.SetVectorArray(PostProcessFog._LightData, this.lightData);
		this.commandBuffer.DrawMeshInstanced(this.lightMesh, 0, this.lightMaterial, 0, this.matrices, num, this.block);
		this.fogLightCountPreview = num;
		this.commandBuffer.EnableShaderKeyword("FOG_EXP");
		this.commandBuffer.Blit(PostProcessFog.fogBufferRTID, 2, this.postFogMaterial);
		this.commandBuffer.ReleaseTemporaryRT(PostProcessFog.backBufferRTID);
		this.commandBuffer.ReleaseTemporaryRT(PostProcessFog.fogBufferRTID);
	}

	// Token: 0x0400082C RID: 2092
	public static List<FogLight> fogLights = new List<FogLight>();

	// Token: 0x0400082D RID: 2093
	private Camera camera;

	// Token: 0x0400082E RID: 2094
	private CommandBuffer commandBuffer;

	// Token: 0x0400082F RID: 2095
	public Material postFogMaterial;

	// Token: 0x04000830 RID: 2096
	public Material lightMaterial;

	// Token: 0x04000831 RID: 2097
	public Mesh lightMesh;

	// Token: 0x04000832 RID: 2098
	private static readonly int fogBufferRTID = Shader.PropertyToID("_FogBuffer");

	// Token: 0x04000833 RID: 2099
	private static readonly int backBufferRTID = Shader.PropertyToID("_BackBuffer");

	// Token: 0x04000834 RID: 2100
	private bool isInitialized;

	// Token: 0x04000835 RID: 2101
	private static readonly int _LightData = Shader.PropertyToID("_LightData");

	// Token: 0x04000836 RID: 2102
	private MaterialPropertyBlock block;

	// Token: 0x04000837 RID: 2103
	private Vector4[] lightData = new Vector4[30];

	// Token: 0x04000838 RID: 2104
	private Matrix4x4[] matrices = new Matrix4x4[30];

	// Token: 0x04000839 RID: 2105
	private CommandBuffer beforeEverythingBuffer;

	// Token: 0x0400083A RID: 2106
	private Plane[] cameraFrustumPlanes = new Plane[6];

	// Token: 0x0400083B RID: 2107
	private static readonly int _InverseView = Shader.PropertyToID("_InverseView");

	// Token: 0x0400083C RID: 2108
	public int fogLightCountPreview;

	// Token: 0x0400083D RID: 2109
	private static readonly int _CameraDepthTexture = Shader.PropertyToID("_CameraDepthTexture");
}
