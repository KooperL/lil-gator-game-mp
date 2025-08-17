using System;
using UnityEngine;

public class FadeRenderer : MonoBehaviour
{
	// Token: 0x060006F2 RID: 1778 RVA: 0x0000707E File Offset: 0x0000527E
	private void OnEnable()
	{
		if (FadeRenderer.propertyBlock == null)
		{
			FadeRenderer.propertyBlock = new MaterialPropertyBlock();
		}
	}

	// Token: 0x060006F3 RID: 1779 RVA: 0x00007091 File Offset: 0x00005291
	private void Start()
	{
		this.SetFade(this.fade);
	}

	// Token: 0x060006F4 RID: 1780 RVA: 0x0000709F File Offset: 0x0000529F
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

	// Token: 0x060006F5 RID: 1781 RVA: 0x000070D8 File Offset: 0x000052D8
	public void UpdateFade()
	{
		if (this.materialToFade == null)
		{
			return;
		}
		this.SetFade(this.fade);
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x000330DC File Offset: 0x000312DC
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
