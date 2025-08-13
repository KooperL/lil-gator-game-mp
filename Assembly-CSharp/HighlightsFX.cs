using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000355 RID: 853
[RequireComponent(typeof(Camera))]
public class HighlightsFX : MonoBehaviour
{
	// Token: 0x0600107E RID: 4222 RVA: 0x00054C60 File Offset: 0x00052E60
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

	// Token: 0x0600107F RID: 4223 RVA: 0x0000E2EC File Offset: 0x0000C4EC
	public void ClearRenderer()
	{
		this.objectRenderers = null;
		this.RecreateCommandBuffer();
	}

	// Token: 0x06001080 RID: 4224 RVA: 0x0000E2FB File Offset: 0x0000C4FB
	public void AddExcluders(Renderer renderer)
	{
		this.m_objectExcluders.Add(renderer);
		this.RecreateCommandBuffer();
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x0000E30F File Offset: 0x0000C50F
	public void RemoveExcluders(Renderer renderer)
	{
		this.m_objectExcluders.Remove(renderer);
		this.RecreateCommandBuffer();
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x0000E324 File Offset: 0x0000C524
	public void ClearOutlineData()
	{
		this.m_objectRenderers.Clear();
		this.m_objectExcluders.Clear();
		this.RecreateCommandBuffer();
	}

	// Token: 0x06001083 RID: 4227 RVA: 0x00002229 File Offset: 0x00000429
	private void CleanRenderers()
	{
	}

	// Token: 0x06001084 RID: 4228 RVA: 0x0000E342 File Offset: 0x0000C542
	[ContextMenu("LoadShaders")]
	public void FindShaders()
	{
		this.m_highlightShader = Shader.Find("Custom/Highlight");
		this.m_blurShader = Shader.Find("Hidden/FastBlur");
	}

	// Token: 0x06001085 RID: 4229 RVA: 0x00054CDC File Offset: 0x00052EDC
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
		this.m_camera.depthTextureMode = 1;
		this.m_camera.AddCommandBuffer(this.BufferDrawEvent, this.m_commandBuffer);
	}

	// Token: 0x06001086 RID: 4230 RVA: 0x0000E364 File Offset: 0x0000C564
	private void OnDisable()
	{
		this.m_camera.RemoveCommandBuffer(this.BufferDrawEvent, this.m_commandBuffer);
	}

	// Token: 0x06001087 RID: 4231 RVA: 0x00054DF4 File Offset: 0x00052FF4
	public void RecreateCommandBuffer()
	{
		this.m_commandBuffer.Clear();
		if (this.objectRenderers == null || this.objectRenderers.Length == 0 || !this.objectRenderers[0].gameObject.activeInHierarchy)
		{
			return;
		}
		this.m_commandBuffer.GetTemporaryRT(this.m_highlightRTID, -1, -1, 0, 0, 0, 0, 1, false, 0, true);
		this.m_commandBuffer.SetRenderTarget(this.m_highlightRTID, 1);
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
		this.m_commandBuffer.GetTemporaryRT(this.m_blurredRTID, num2, num3, 0, 1, 0, 0, 1, false, 0, true);
		this.m_commandBuffer.GetTemporaryRT(this.m_temporaryRTID, num2, num3, 0, 1, 0, 0, 1, false, 0, true);
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
		this.m_commandBuffer.Blit(this.m_highlightRTID, 2, this.m_highlightMaterial, (int)this.m_selectionType);
		this.m_commandBuffer.ReleaseTemporaryRT(this.m_temporaryRTID);
		this.m_commandBuffer.ReleaseTemporaryRT(this.m_highlightRTID);
	}

	// Token: 0x06001088 RID: 4232 RVA: 0x0000E37D File Offset: 0x0000C57D
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

	// Token: 0x04001561 RID: 5473
	public static HighlightsFX h;

	// Token: 0x04001562 RID: 5474
	[Header("Outline Settings")]
	public HighlightsFX.HighlightType m_selectionType;

	// Token: 0x04001563 RID: 5475
	public HighlightsFX.FillType m_fillType = HighlightsFX.FillType.Outline;

	// Token: 0x04001564 RID: 5476
	public HighlightsFX.RTResolution m_resolution = HighlightsFX.RTResolution.Full;

	// Token: 0x04001565 RID: 5477
	[Range(0f, 1f)]
	public float m_controlValue = 0.5f;

	// Token: 0x04001566 RID: 5478
	public CameraEvent BufferDrawEvent = 18;

	// Token: 0x04001567 RID: 5479
	[Header("BlurOptimized Settings")]
	public HighlightsFX.BlurType blurType;

	// Token: 0x04001568 RID: 5480
	[Range(0f, 2f)]
	public int downsample;

	// Token: 0x04001569 RID: 5481
	[Range(0f, 30f)]
	public float blurSize = 3f;

	// Token: 0x0400156A RID: 5482
	[Range(1f, 4f)]
	public int blurIterations = 2;

	// Token: 0x0400156B RID: 5483
	public Texture2D noiseTexture;

	// Token: 0x0400156C RID: 5484
	private CommandBuffer m_commandBuffer;

	// Token: 0x0400156D RID: 5485
	public float m_fadeInControlValue;

	// Token: 0x0400156E RID: 5486
	private int m_highlightRTID;

	// Token: 0x0400156F RID: 5487
	private int m_blurredRTID;

	// Token: 0x04001570 RID: 5488
	private int m_temporaryRTID;

	// Token: 0x04001571 RID: 5489
	private int m_backBufferRTID;

	// Token: 0x04001572 RID: 5490
	private Dictionary<Renderer, HighlightsFX.OutlineData> m_objectRenderers;

	// Token: 0x04001573 RID: 5491
	private List<Renderer> m_objectExcluders;

	// Token: 0x04001574 RID: 5492
	public Shader m_highlightShader;

	// Token: 0x04001575 RID: 5493
	public Shader m_blurShader;

	// Token: 0x04001576 RID: 5494
	private Material m_highlightMaterial;

	// Token: 0x04001577 RID: 5495
	private Material m_blurMaterial;

	// Token: 0x04001578 RID: 5496
	private Camera m_camera;

	// Token: 0x04001579 RID: 5497
	private int m_RTWidth = 512;

	// Token: 0x0400157A RID: 5498
	private int m_RTHeight = 512;

	// Token: 0x0400157B RID: 5499
	public Renderer[] objectRenderers;

	// Token: 0x0400157C RID: 5500
	private Color objectColor;

	// Token: 0x0400157D RID: 5501
	private HighlightsFX.SortingType objectSorting;

	// Token: 0x0400157E RID: 5502
	private BlitToScreen blitToScreen;

	// Token: 0x0400157F RID: 5503
	private int controlValueID = Shader.PropertyToID("_ControlValue");

	// Token: 0x04001580 RID: 5504
	private int noiseFadeID = Shader.PropertyToID("_NoiseFade");

	// Token: 0x04001581 RID: 5505
	private IEnumerator fadeInCoroutine;

	// Token: 0x02000356 RID: 854
	public enum HighlightType
	{
		// Token: 0x04001583 RID: 5507
		Glow,
		// Token: 0x04001584 RID: 5508
		Solid
	}

	// Token: 0x02000357 RID: 855
	public enum SortingType
	{
		// Token: 0x04001586 RID: 5510
		Overlay = 3,
		// Token: 0x04001587 RID: 5511
		DepthFiltered
	}

	// Token: 0x02000358 RID: 856
	public enum DepthInvertPass
	{
		// Token: 0x04001589 RID: 5513
		StencilMapper = 5,
		// Token: 0x0400158A RID: 5514
		StencilDrawer
	}

	// Token: 0x02000359 RID: 857
	public enum FillType
	{
		// Token: 0x0400158C RID: 5516
		Fill,
		// Token: 0x0400158D RID: 5517
		Outline
	}

	// Token: 0x0200035A RID: 858
	public enum RTResolution
	{
		// Token: 0x0400158F RID: 5519
		Quarter = 4,
		// Token: 0x04001590 RID: 5520
		Half = 2,
		// Token: 0x04001591 RID: 5521
		Full = 1
	}

	// Token: 0x0200035B RID: 859
	public enum BlurType
	{
		// Token: 0x04001593 RID: 5523
		StandardGauss,
		// Token: 0x04001594 RID: 5524
		SgxGauss
	}

	// Token: 0x0200035C RID: 860
	public struct OutlineData
	{
		// Token: 0x04001595 RID: 5525
		public Color color;

		// Token: 0x04001596 RID: 5526
		public HighlightsFX.SortingType sortingType;
	}
}
