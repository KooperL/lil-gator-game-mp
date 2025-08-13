using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000285 RID: 645
[RequireComponent(typeof(Camera))]
public class HighlightsFX : MonoBehaviour
{
	// Token: 0x06000DC2 RID: 3522 RVA: 0x00042A48 File Offset: 0x00040C48
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

	// Token: 0x06000DC3 RID: 3523 RVA: 0x00042AC3 File Offset: 0x00040CC3
	public void ClearRenderer()
	{
		this.objectRenderers = null;
		this.RecreateCommandBuffer();
	}

	// Token: 0x06000DC4 RID: 3524 RVA: 0x00042AD2 File Offset: 0x00040CD2
	public void AddExcluders(Renderer renderer)
	{
		this.m_objectExcluders.Add(renderer);
		this.RecreateCommandBuffer();
	}

	// Token: 0x06000DC5 RID: 3525 RVA: 0x00042AE6 File Offset: 0x00040CE6
	public void RemoveExcluders(Renderer renderer)
	{
		this.m_objectExcluders.Remove(renderer);
		this.RecreateCommandBuffer();
	}

	// Token: 0x06000DC6 RID: 3526 RVA: 0x00042AFB File Offset: 0x00040CFB
	public void ClearOutlineData()
	{
		this.m_objectRenderers.Clear();
		this.m_objectExcluders.Clear();
		this.RecreateCommandBuffer();
	}

	// Token: 0x06000DC7 RID: 3527 RVA: 0x00042B19 File Offset: 0x00040D19
	private void CleanRenderers()
	{
	}

	// Token: 0x06000DC8 RID: 3528 RVA: 0x00042B1B File Offset: 0x00040D1B
	[ContextMenu("LoadShaders")]
	public void FindShaders()
	{
		this.m_highlightShader = Shader.Find("Custom/Highlight");
		this.m_blurShader = Shader.Find("Hidden/FastBlur");
	}

	// Token: 0x06000DC9 RID: 3529 RVA: 0x00042B40 File Offset: 0x00040D40
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

	// Token: 0x06000DCA RID: 3530 RVA: 0x00042C55 File Offset: 0x00040E55
	private void OnDisable()
	{
		this.m_camera.RemoveCommandBuffer(this.BufferDrawEvent, this.m_commandBuffer);
	}

	// Token: 0x06000DCB RID: 3531 RVA: 0x00042C70 File Offset: 0x00040E70
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

	// Token: 0x06000DCC RID: 3532 RVA: 0x00042F6C File Offset: 0x0004116C
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

	// Token: 0x0400121A RID: 4634
	public static HighlightsFX h;

	// Token: 0x0400121B RID: 4635
	[Header("Outline Settings")]
	public HighlightsFX.HighlightType m_selectionType;

	// Token: 0x0400121C RID: 4636
	public HighlightsFX.FillType m_fillType = HighlightsFX.FillType.Outline;

	// Token: 0x0400121D RID: 4637
	public HighlightsFX.RTResolution m_resolution = HighlightsFX.RTResolution.Full;

	// Token: 0x0400121E RID: 4638
	[Range(0f, 1f)]
	public float m_controlValue = 0.5f;

	// Token: 0x0400121F RID: 4639
	public CameraEvent BufferDrawEvent = CameraEvent.BeforeImageEffects;

	// Token: 0x04001220 RID: 4640
	[Header("BlurOptimized Settings")]
	public HighlightsFX.BlurType blurType;

	// Token: 0x04001221 RID: 4641
	[Range(0f, 2f)]
	public int downsample;

	// Token: 0x04001222 RID: 4642
	[Range(0f, 30f)]
	public float blurSize = 3f;

	// Token: 0x04001223 RID: 4643
	[Range(1f, 4f)]
	public int blurIterations = 2;

	// Token: 0x04001224 RID: 4644
	public Texture2D noiseTexture;

	// Token: 0x04001225 RID: 4645
	private CommandBuffer m_commandBuffer;

	// Token: 0x04001226 RID: 4646
	public float m_fadeInControlValue;

	// Token: 0x04001227 RID: 4647
	private int m_highlightRTID;

	// Token: 0x04001228 RID: 4648
	private int m_blurredRTID;

	// Token: 0x04001229 RID: 4649
	private int m_temporaryRTID;

	// Token: 0x0400122A RID: 4650
	private int m_backBufferRTID;

	// Token: 0x0400122B RID: 4651
	private Dictionary<Renderer, HighlightsFX.OutlineData> m_objectRenderers;

	// Token: 0x0400122C RID: 4652
	private List<Renderer> m_objectExcluders;

	// Token: 0x0400122D RID: 4653
	public Shader m_highlightShader;

	// Token: 0x0400122E RID: 4654
	public Shader m_blurShader;

	// Token: 0x0400122F RID: 4655
	private Material m_highlightMaterial;

	// Token: 0x04001230 RID: 4656
	private Material m_blurMaterial;

	// Token: 0x04001231 RID: 4657
	private Camera m_camera;

	// Token: 0x04001232 RID: 4658
	private int m_RTWidth = 512;

	// Token: 0x04001233 RID: 4659
	private int m_RTHeight = 512;

	// Token: 0x04001234 RID: 4660
	public Renderer[] objectRenderers;

	// Token: 0x04001235 RID: 4661
	private Color objectColor;

	// Token: 0x04001236 RID: 4662
	private HighlightsFX.SortingType objectSorting;

	// Token: 0x04001237 RID: 4663
	private BlitToScreen blitToScreen;

	// Token: 0x04001238 RID: 4664
	private int controlValueID = Shader.PropertyToID("_ControlValue");

	// Token: 0x04001239 RID: 4665
	private int noiseFadeID = Shader.PropertyToID("_NoiseFade");

	// Token: 0x0400123A RID: 4666
	private IEnumerator fadeInCoroutine;

	// Token: 0x0200042E RID: 1070
	public enum HighlightType
	{
		// Token: 0x04001D7C RID: 7548
		Glow,
		// Token: 0x04001D7D RID: 7549
		Solid
	}

	// Token: 0x0200042F RID: 1071
	public enum SortingType
	{
		// Token: 0x04001D7F RID: 7551
		Overlay = 3,
		// Token: 0x04001D80 RID: 7552
		DepthFiltered
	}

	// Token: 0x02000430 RID: 1072
	public enum DepthInvertPass
	{
		// Token: 0x04001D82 RID: 7554
		StencilMapper = 5,
		// Token: 0x04001D83 RID: 7555
		StencilDrawer
	}

	// Token: 0x02000431 RID: 1073
	public enum FillType
	{
		// Token: 0x04001D85 RID: 7557
		Fill,
		// Token: 0x04001D86 RID: 7558
		Outline
	}

	// Token: 0x02000432 RID: 1074
	public enum RTResolution
	{
		// Token: 0x04001D88 RID: 7560
		Quarter = 4,
		// Token: 0x04001D89 RID: 7561
		Half = 2,
		// Token: 0x04001D8A RID: 7562
		Full = 1
	}

	// Token: 0x02000433 RID: 1075
	public enum BlurType
	{
		// Token: 0x04001D8C RID: 7564
		StandardGauss,
		// Token: 0x04001D8D RID: 7565
		SgxGauss
	}

	// Token: 0x02000434 RID: 1076
	public struct OutlineData
	{
		// Token: 0x04001D8E RID: 7566
		public Color color;

		// Token: 0x04001D8F RID: 7567
		public HighlightsFX.SortingType sortingType;
	}
}
