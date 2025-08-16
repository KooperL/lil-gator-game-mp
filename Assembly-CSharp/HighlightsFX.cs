using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Camera))]
public class HighlightsFX : MonoBehaviour
{
	// Token: 0x060010D9 RID: 4313 RVA: 0x000569F0 File Offset: 0x00054BF0
	public void SetRenderer(Renderer[] renderers, Color col, HighlightsFX.SortingType sorting = HighlightsFX.SortingType.Overlay, bool fadeIn = true)
	{
		this.objectRenderers = renderers;
		this.objectColor = col;
		this.objectSorting = sorting;
		this.m_highlightMaterial.SetTexture("_CutoffTex", renderers[0].material.GetTexture("_MainTex"));
		this.RecreateCommandBuffer();
		if (fadeIn)
		{
			if (this.fadeInCoroutine != null)
			{
				base.StopCoroutine(this.fadeInCoroutine);
			}
			this.fadeInCoroutine = this.FadeInMaterial();
			base.StartCoroutine(this.fadeInCoroutine);
		}
	}

	// Token: 0x060010DA RID: 4314 RVA: 0x0000E640 File Offset: 0x0000C840
	public void ClearRenderer()
	{
		this.objectRenderers = null;
		this.RecreateCommandBuffer();
	}

	// Token: 0x060010DB RID: 4315 RVA: 0x0000E64F File Offset: 0x0000C84F
	public void AddExcluders(Renderer renderer)
	{
		this.m_objectExcluders.Add(renderer);
		this.RecreateCommandBuffer();
	}

	// Token: 0x060010DC RID: 4316 RVA: 0x0000E663 File Offset: 0x0000C863
	public void RemoveExcluders(Renderer renderer)
	{
		this.m_objectExcluders.Remove(renderer);
		this.RecreateCommandBuffer();
	}

	// Token: 0x060010DD RID: 4317 RVA: 0x0000E678 File Offset: 0x0000C878
	public void ClearOutlineData()
	{
		this.m_objectRenderers.Clear();
		this.m_objectExcluders.Clear();
		this.RecreateCommandBuffer();
	}

	// Token: 0x060010DE RID: 4318 RVA: 0x00002229 File Offset: 0x00000429
	private void CleanRenderers()
	{
	}

	// Token: 0x060010DF RID: 4319 RVA: 0x0000E696 File Offset: 0x0000C896
	[ContextMenu("LoadShaders")]
	public void FindShaders()
	{
		this.m_highlightShader = Shader.Find("Custom/Highlight");
		this.m_blurShader = Shader.Find("Hidden/FastBlur");
	}

	// Token: 0x060010E0 RID: 4320 RVA: 0x00056A6C File Offset: 0x00054C6C
	private void OnEnable()
	{
		HighlightsFX.h = this;
		this.m_objectRenderers = new Dictionary<Renderer, HighlightsFX.OutlineData>();
		this.m_objectExcluders = new List<Renderer>();
		this.m_commandBuffer = new CommandBuffer();
		this.m_commandBuffer.name = "HighlightFX Command Buffer";
		this.m_highlightRTID = Shader.PropertyToID("_HighlightRT");
		this.m_blurredRTID = Shader.PropertyToID("_BlurredRT");
		this.m_temporaryRTID = Shader.PropertyToID("_TemporaryRT");
		this.m_backBufferRTID = Shader.PropertyToID("_BackBufferRT");
		this.m_RTWidth = (int)((float)Screen.width / (float)this.m_resolution);
		this.m_RTHeight = (int)((float)Screen.height / (float)this.m_resolution);
		this.m_highlightMaterial = new Material(this.m_highlightShader);
		this.m_blurMaterial = new Material(this.m_blurShader);
		this.m_highlightMaterial.SetTexture("_NoiseTex", this.noiseTexture);
		this.m_camera = base.GetComponent<Camera>();
		this.m_camera.depthTextureMode = DepthTextureMode.Depth;
		this.m_camera.AddCommandBuffer(this.BufferDrawEvent, this.m_commandBuffer);
	}

	// Token: 0x060010E1 RID: 4321 RVA: 0x0000E6B8 File Offset: 0x0000C8B8
	private void OnDisable()
	{
		this.m_camera.RemoveCommandBuffer(this.BufferDrawEvent, this.m_commandBuffer);
	}

	// Token: 0x060010E2 RID: 4322 RVA: 0x00056B84 File Offset: 0x00054D84
	public void RecreateCommandBuffer()
	{
		this.m_commandBuffer.Clear();
		if (this.objectRenderers == null || this.objectRenderers.Length == 0 || !this.objectRenderers[0].gameObject.activeInHierarchy)
		{
			return;
		}
		this.m_commandBuffer.GetTemporaryRT(this.m_highlightRTID, -1, -1, 0, FilterMode.Point, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, 1, false, RenderTextureMemoryless.None, true);
		this.m_commandBuffer.SetRenderTarget(this.m_highlightRTID, BuiltinRenderTextureType.CurrentActive);
		this.m_commandBuffer.ClearRenderTarget(false, true, Color.clear);
		this.m_commandBuffer.SetGlobalColor("_Color", this.objectColor);
		foreach (Renderer renderer in this.objectRenderers)
		{
			this.m_commandBuffer.DrawRenderer(renderer, this.m_highlightMaterial, 0, (int)this.objectSorting);
		}
		this.downsample = 0;
		float num = 1f / (1f * (float)(1 << this.downsample));
		this.m_RTWidth = (int)((float)Screen.currentResolution.width / (float)this.m_resolution);
		this.m_RTHeight = (int)((float)Screen.currentResolution.height / (float)this.m_resolution);
		int num2 = this.m_RTWidth >> this.downsample;
		int num3 = this.m_RTHeight >> this.downsample;
		this.m_commandBuffer.GetTemporaryRT(this.m_blurredRTID, num2, num3, 0, FilterMode.Bilinear, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, 1, false, RenderTextureMemoryless.None, true);
		this.m_commandBuffer.GetTemporaryRT(this.m_temporaryRTID, num2, num3, 0, FilterMode.Bilinear, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, 1, false, RenderTextureMemoryless.None, true);
		this.m_commandBuffer.Blit(this.m_highlightRTID, this.m_temporaryRTID, this.m_blurMaterial, 0);
		int num4 = ((this.blurType == HighlightsFX.BlurType.StandardGauss) ? 0 : 2);
		for (int j = 0; j < this.blurIterations; j++)
		{
			float num5 = (float)j * 1f;
			float num6 = this.blurSize * num + num5;
			float num7 = -this.blurSize * num - num5;
			this.m_commandBuffer.SetGlobalVector("_Parameter", new Vector4(num6, num7));
			this.m_commandBuffer.Blit(this.m_temporaryRTID, this.m_blurredRTID, this.m_blurMaterial, 1 + num4);
			this.m_commandBuffer.Blit(this.m_blurredRTID, this.m_temporaryRTID, this.m_blurMaterial, 2 + num4);
		}
		this.m_commandBuffer.ReleaseTemporaryRT(this.m_blurredRTID);
		this.m_commandBuffer.SetGlobalTexture("_SecondaryTex", this.m_temporaryRTID);
		this.m_commandBuffer.SetGlobalFloat("_ControlValue", this.m_controlValue);
		this.m_commandBuffer.Blit(this.m_highlightRTID, BuiltinRenderTextureType.CameraTarget, this.m_highlightMaterial, (int)this.m_selectionType);
		this.m_commandBuffer.ReleaseTemporaryRT(this.m_temporaryRTID);
		this.m_commandBuffer.ReleaseTemporaryRT(this.m_highlightRTID);
	}

	// Token: 0x060010E3 RID: 4323 RVA: 0x0000E6D1 File Offset: 0x0000C8D1
	private IEnumerator FadeInMaterial()
	{
		float fade = 0.5f;
		while (fade < 1f)
		{
			fade = Mathf.MoveTowards(fade, 1f, 2f * Time.deltaTime);
			this.m_highlightMaterial.SetFloat(this.noiseFadeID, fade);
			yield return null;
		}
		yield return null;
		yield break;
	}

	public static HighlightsFX h;

	[Header("Outline Settings")]
	public HighlightsFX.HighlightType m_selectionType;

	public HighlightsFX.FillType m_fillType = HighlightsFX.FillType.Outline;

	public HighlightsFX.RTResolution m_resolution = HighlightsFX.RTResolution.Full;

	[Range(0f, 1f)]
	public float m_controlValue = 0.5f;

	public CameraEvent BufferDrawEvent = CameraEvent.BeforeImageEffects;

	[Header("BlurOptimized Settings")]
	public HighlightsFX.BlurType blurType;

	[Range(0f, 2f)]
	public int downsample;

	[Range(0f, 30f)]
	public float blurSize = 3f;

	[Range(1f, 4f)]
	public int blurIterations = 2;

	public Texture2D noiseTexture;

	private CommandBuffer m_commandBuffer;

	public float m_fadeInControlValue;

	private int m_highlightRTID;

	private int m_blurredRTID;

	private int m_temporaryRTID;

	private int m_backBufferRTID;

	private Dictionary<Renderer, HighlightsFX.OutlineData> m_objectRenderers;

	private List<Renderer> m_objectExcluders;

	public Shader m_highlightShader;

	public Shader m_blurShader;

	private Material m_highlightMaterial;

	private Material m_blurMaterial;

	private Camera m_camera;

	private int m_RTWidth = 512;

	private int m_RTHeight = 512;

	public Renderer[] objectRenderers;

	private Color objectColor;

	private HighlightsFX.SortingType objectSorting;

	private BlitToScreen blitToScreen;

	private int controlValueID = Shader.PropertyToID("_ControlValue");

	private int noiseFadeID = Shader.PropertyToID("_NoiseFade");

	private IEnumerator fadeInCoroutine;

	public enum HighlightType
	{
		Glow,
		Solid
	}

	public enum SortingType
	{
		Overlay = 3,
		DepthFiltered
	}

	public enum DepthInvertPass
	{
		StencilMapper = 5,
		StencilDrawer
	}

	public enum FillType
	{
		Fill,
		Outline
	}

	public enum RTResolution
	{
		Quarter = 4,
		Half = 2,
		Full = 1
	}

	public enum BlurType
	{
		StandardGauss,
		SgxGauss
	}

	public struct OutlineData
	{
		public Color color;

		public HighlightsFX.SortingType sortingType;
	}
}
