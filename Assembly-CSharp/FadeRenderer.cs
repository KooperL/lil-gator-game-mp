using System;
using UnityEngine;

// Token: 0x02000110 RID: 272
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

	// Token: 0x040007A5 RID: 1957
	[Range(0f, 1f)]
	public float fade = 1f;

	// Token: 0x040007A6 RID: 1958
	public Renderer mainRenderer;

	// Token: 0x040007A7 RID: 1959
	public SpriteRenderer[] eyes;

	// Token: 0x040007A8 RID: 1960
	public bool useSharedMaterial;

	// Token: 0x040007A9 RID: 1961
	[ConditionalHide("useSharedMaterial", true)]
	public Material materialToFade;

	// Token: 0x040007AA RID: 1962
	private int fadeID = Shader.PropertyToID("_Fade");

	// Token: 0x040007AB RID: 1963
	private static MaterialPropertyBlock propertyBlock;
}
