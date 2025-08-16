using System;
using System.Collections.Generic;
using UnityEngine;

public class FadeGroup : MonoBehaviour
{
	// Token: 0x060006DF RID: 1759 RVA: 0x00032BAC File Offset: 0x00030DAC
	[ContextMenu("Find Renderers")]
	public void FindRenderers()
	{
		this.fadeRenderers = base.transform.GetComponentsInChildren<FadeRenderer>();
		Renderer[] componentsInChildren = base.transform.GetComponentsInChildren<Renderer>();
		List<Renderer> list = new List<Renderer>();
		foreach (Renderer renderer in componentsInChildren)
		{
			bool flag = false;
			if (!(renderer is ParticleSystemRenderer))
			{
				FadeRenderer[] array2 = this.fadeRenderers;
				int j = 0;
				while (j < array2.Length)
				{
					FadeRenderer fadeRenderer = array2[j];
					if (!(renderer == fadeRenderer.mainRenderer))
					{
						Renderer[] eyes = fadeRenderer.eyes;
						if (!eyes.Contains(renderer))
						{
							j++;
							continue;
						}
					}
					flag = true;
					break;
				}
				if (!flag)
				{
					list.Add(renderer);
				}
			}
		}
		this.renderers = list.ToArray();
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x00006F86 File Offset: 0x00005186
	private void Start()
	{
		this.UpdateFade();
		if (this.fadeOnStart)
		{
			this.fadeTarget = this.initialFadeTarget;
		}
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x00006FA2 File Offset: 0x000051A2
	public void FadeIn()
	{
		base.gameObject.SetActive(true);
		this.fadeTarget = this.maxFade;
		this.UpdateFade();
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x00006FC2 File Offset: 0x000051C2
	public void FadeOut()
	{
		this.fadeTarget = 0f;
		this.UpdateFade();
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x00032C5C File Offset: 0x00030E5C
	private void Update()
	{
		if (this.fade != this.fadeTarget)
		{
			float num = ((this.fadeTarget > this.fade) ? this.fadeInSpeed : this.fadeOutSpeed);
			this.fade = Mathf.MoveTowards(this.fade, this.fadeTarget, Time.deltaTime * num);
			this.UpdateFade();
			if (this.fade == this.fadeTarget && this.fade == 0f)
			{
				base.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x00032CE0 File Offset: 0x00030EE0
	private void Initialize()
	{
		List<Material> list = new List<Material>();
		List<Material> list2 = new List<Material>();
		foreach (Renderer renderer in this.renderers)
		{
			int num;
			if (!list.TryFindIndex(renderer.sharedMaterial, out num))
			{
				list.Add(renderer.sharedMaterial);
				list2.Add(renderer.material);
				num = list2.Count - 1;
			}
			renderer.sharedMaterial = list2[num];
		}
		this.dynamicMaterials = list2.ToArray();
		this.isInitialized = true;
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x00032D6C File Offset: 0x00030F6C
	private void UpdateFade()
	{
		if (!this.isInitialized)
		{
			this.Initialize();
		}
		FadeRenderer[] array = this.fadeRenderers;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetFade(Mathf.SmoothStep(0f, this.maxFade, this.fade));
		}
		Material[] array2 = this.dynamicMaterials;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].SetFloat(this.fadeID, Mathf.SmoothStep(0f, this.maxObjectFade, this.fade));
		}
	}

	private static FadeGroup currentGroup;

	public FadeRenderer[] fadeRenderers;

	public Renderer[] renderers;

	private Material[] dynamicMaterials;

	private bool isInitialized;

	public float fade;

	public bool fadeOnStart;

	[ConditionalHide("fadeOnStart", true)]
	public float initialFadeTarget;

	public float fadeInSpeed = 0.25f;

	public float fadeOutSpeed = 0.5f;

	private float fadeTarget;

	public float maxFade = 0.9f;

	public float maxObjectFade = 0.8f;

	private int fadeID = Shader.PropertyToID("_Fade");
}
