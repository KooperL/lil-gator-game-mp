using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessFog : MonoBehaviour
{
	// Token: 0x06000652 RID: 1618 RVA: 0x00030E00 File Offset: 0x0002F000
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

	// Token: 0x06000653 RID: 1619 RVA: 0x00006885 File Offset: 0x00004A85
	private void OnDisable()
	{
		if (this.camera != null)
		{
			this.camera.RemoveCommandBuffer(CameraEvent.BeforeForwardOpaque, this.beforeEverythingBuffer);
			this.camera.RemoveCommandBuffer(CameraEvent.BeforeSkybox, this.commandBuffer);
		}
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x000068BB File Offset: 0x00004ABB
	private void LateUpdate()
	{
		if (MainCamera.c != null)
		{
			this.postFogMaterial.SetMatrix(PostProcessFog._InverseView, MainCamera.c.cameraToWorldMatrix);
		}
		this.UpdateCommandBuffer();
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x00030EA0 File Offset: 0x0002F0A0
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

	public static List<FogLight> fogLights = new List<FogLight>();

	private Camera camera;

	private CommandBuffer commandBuffer;

	public Material postFogMaterial;

	public Material lightMaterial;

	public Mesh lightMesh;

	private static readonly int fogBufferRTID = Shader.PropertyToID("_FogBuffer");

	private static readonly int backBufferRTID = Shader.PropertyToID("_BackBuffer");

	private bool isInitialized;

	private static readonly int _LightData = Shader.PropertyToID("_LightData");

	private MaterialPropertyBlock block;

	private Vector4[] lightData = new Vector4[30];

	private Matrix4x4[] matrices = new Matrix4x4[30];

	private CommandBuffer beforeEverythingBuffer;

	private Plane[] cameraFrustumPlanes = new Plane[6];

	private static readonly int _InverseView = Shader.PropertyToID("_InverseView");

	public int fogLightCountPreview;

	private static readonly int _CameraDepthTexture = Shader.PropertyToID("_CameraDepthTexture");
}
