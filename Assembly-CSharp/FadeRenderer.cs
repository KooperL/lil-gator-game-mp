using System;
using UnityEngine;

public class FadeRenderer : MonoBehaviour
{
	// Token: 0x06000594 RID: 1428 RVA: 0x0001D509 File Offset: 0x0001B709
	private void OnEnable()
	{
		if (FadeRenderer.propertyBlock == null)
		{
			FadeRenderer.propertyBlock = new MaterialPropertyBlock();
		}
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x0001D51C File Offset: 0x0001B71C
	private void Start()
	{
		this.SetFade(this.fade);
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x0001D52A File Offset: 0x0001B72A
	public virtual void OnValidate()
	{
		if (this.mainRenderer == null)
		{
			this.mainRenderer = base.GetComponentInChildren<SkinnedMeshRenderer>();
		}
		if (this.eyes == null || this.eyes.Length == 0)
		{
			this.eyes = base.GetComponentsInChildren<SpriteRenderer>();
		}
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x0001D563 File Offset: 0x0001B763
	public void UpdateFade()
	{
		if (this.materialToFade == null)
		{
			return;
		}
		this.SetFade(this.fade);
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x0001D580 File Offset: 0x0001B780
	public virtual void SetFade(float fade)
	{
		this.fade = fade;
		if (fade == 0f)
		{
			this.mainRenderer.enabled = false;
		}
		else
		{
			this.mainRenderer.enabled = true;
			FadeRenderer.propertyBlock.SetFloat(this.fadeID, fade);
			this.mainRenderer.SetPropertyBlock(FadeRenderer.propertyBlock);
		}
		SpriteRenderer[] array = this.eyes;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = fade > 0.5f;
		}
	}

	[Range(0f, 1f)]
	public float fade = 1f;

	public Renderer mainRenderer;

	public SpriteRenderer[] eyes;

	public bool useSharedMaterial;

	[ConditionalHide("useSharedMaterial", true)]
	public Material materialToFade;

	private int fadeID = Shader.PropertyToID("_Fade");

	private static MaterialPropertyBlock propertyBlock;
}
