using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x020000F3 RID: 243
public class PostProcessFog : MonoBehaviour
{
	// Token: 0x06000506 RID: 1286 RVA: 0x0001AE94 File Offset: 0x00019094
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
		this.camera.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, this.beforeEverythingBuffer);
		this.camera.AddCommandBuffer(CameraEvent.BeforeSkybox, this.commandBuffer);
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x0001AF31 File Offset: 0x00019131
	private void OnDisable()
	{
		if (this.camera != null)
		{
			this.camera.RemoveCommandBuffer(CameraEvent.BeforeForwardOpaque, this.beforeEverythingBuffer);
			this.camera.RemoveCommandBuffer(CameraEvent.BeforeSkybox, this.commandBuffer);
		}
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x0001AF67 File Offset: 0x00019167
	private void LateUpdate()
	{
		if (MainCamera.c != null)
		{
			this.postFogMaterial.SetMatrix(PostProcessFog._InverseView, MainCamera.c.cameraToWorldMatrix);
		}
		this.UpdateCommandBuffer();
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x0001AF98 File Offset: 0x00019198
	private void UpdateCommandBuffer()
	{
		this.commandBuffer.Clear();
		this.commandBuffer.SetGlobalTexture(PostProcessFog._CameraDepthTexture, BuiltinRenderTextureType.Depth);
		this.commandBuffer.GetTemporaryRT(PostProcessFog.fogBufferRTID, -1, -1, 0, FilterMode.Point, RenderTextureFormat.R16, RenderTextureReadWrite.Default, 1, false, RenderTextureMemoryless.None, true);
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
		this.commandBuffer.Blit(PostProcessFog.fogBufferRTID, BuiltinRenderTextureType.CameraTarget, this.postFogMaterial);
		this.commandBuffer.ReleaseTemporaryRT(PostProcessFog.backBufferRTID);
		this.commandBuffer.ReleaseTemporaryRT(PostProcessFog.fogBufferRTID);
	}

	// Token: 0x040006E5 RID: 1765
	public static List<FogLight> fogLights = new List<FogLight>();

	// Token: 0x040006E6 RID: 1766
	private Camera camera;

	// Token: 0x040006E7 RID: 1767
	private CommandBuffer commandBuffer;

	// Token: 0x040006E8 RID: 1768
	public Material postFogMaterial;

	// Token: 0x040006E9 RID: 1769
	public Material lightMaterial;

	// Token: 0x040006EA RID: 1770
	public Mesh lightMesh;

	// Token: 0x040006EB RID: 1771
	private static readonly int fogBufferRTID = Shader.PropertyToID("_FogBuffer");

	// Token: 0x040006EC RID: 1772
	private static readonly int backBufferRTID = Shader.PropertyToID("_BackBuffer");

	// Token: 0x040006ED RID: 1773
	private bool isInitialized;

	// Token: 0x040006EE RID: 1774
	private static readonly int _LightData = Shader.PropertyToID("_LightData");

	// Token: 0x040006EF RID: 1775
	private MaterialPropertyBlock block;

	// Token: 0x040006F0 RID: 1776
	private Vector4[] lightData = new Vector4[30];

	// Token: 0x040006F1 RID: 1777
	private Matrix4x4[] matrices = new Matrix4x4[30];

	// Token: 0x040006F2 RID: 1778
	private CommandBuffer beforeEverythingBuffer;

	// Token: 0x040006F3 RID: 1779
	private Plane[] cameraFrustumPlanes = new Plane[6];

	// Token: 0x040006F4 RID: 1780
	private static readonly int _InverseView = Shader.PropertyToID("_InverseView");

	// Token: 0x040006F5 RID: 1781
	public int fogLightCountPreview;

	// Token: 0x040006F6 RID: 1782
	private static readonly int _CameraDepthTexture = Shader.PropertyToID("_CameraDepthTexture");
}
