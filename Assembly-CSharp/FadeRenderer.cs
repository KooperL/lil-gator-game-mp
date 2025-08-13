using System;
using UnityEngine;

// Token: 0x02000167 RID: 359
public class FadeRenderer : MonoBehaviour
{
	// Token: 0x060006B8 RID: 1720 RVA: 0x00006DB8 File Offset: 0x00004FB8
	private void OnEnable()
	{
		if (FadeRenderer.propertyBlock == null)
		{
			FadeRenderer.propertyBlock = new MaterialPropertyBlock();
		}
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x00006DCB File Offset: 0x00004FCB
	private void Start()
	{
		this.SetFade(this.fade);
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x00006DD9 File Offset: 0x00004FD9
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

	// Token: 0x060006BB RID: 1723 RVA: 0x00006E12 File Offset: 0x00005012
	public void UpdateFade()
	{
		if (this.materialToFade == null)
		{
			return;
		}
		this.SetFade(this.fade);
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x000319E0 File Offset: 0x0002FBE0
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

	// Token: 0x040008FF RID: 2303
	[Range(0f, 1f)]
	public float fade = 1f;

	// Token: 0x04000900 RID: 2304
	public Renderer mainRenderer;

	// Token: 0x04000901 RID: 2305
	public SpriteRenderer[] eyes;

	// Token: 0x04000902 RID: 2306
	public bool useSharedMaterial;

	// Token: 0x04000903 RID: 2307
	[ConditionalHide("useSharedMaterial", true)]
	public Material materialToFade;

	// Token: 0x04000904 RID: 2308
	private int fadeID = Shader.PropertyToID("_Fade");

	// Token: 0x04000905 RID: 2309
	private static MaterialPropertyBlock propertyBlock;
}
