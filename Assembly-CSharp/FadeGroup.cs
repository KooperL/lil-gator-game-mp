using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000164 RID: 356
public class FadeGroup : MonoBehaviour
{
	// Token: 0x060006A5 RID: 1701 RVA: 0x0003162C File Offset: 0x0002F82C
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

	// Token: 0x060006A6 RID: 1702 RVA: 0x00006CC0 File Offset: 0x00004EC0
	private void Start()
	{
		this.UpdateFade();
		if (this.fadeOnStart)
		{
			this.fadeTarget = this.initialFadeTarget;
		}
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x00006CDC File Offset: 0x00004EDC
	public void FadeIn()
	{
		base.gameObject.SetActive(true);
		this.fadeTarget = this.maxFade;
		this.UpdateFade();
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x00006CFC File Offset: 0x00004EFC
	public void FadeOut()
	{
		this.fadeTarget = 0f;
		this.UpdateFade();
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x000316DC File Offset: 0x0002F8DC
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

	// Token: 0x060006AA RID: 1706 RVA: 0x00031760 File Offset: 0x0002F960
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

	// Token: 0x060006AB RID: 1707 RVA: 0x000317EC File Offset: 0x0002F9EC
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

	// Token: 0x040008E7 RID: 2279
	private static FadeGroup currentGroup;

	// Token: 0x040008E8 RID: 2280
	public FadeRenderer[] fadeRenderers;

	// Token: 0x040008E9 RID: 2281
	public Renderer[] renderers;

	// Token: 0x040008EA RID: 2282
	private Material[] dynamicMaterials;

	// Token: 0x040008EB RID: 2283
	private bool isInitialized;

	// Token: 0x040008EC RID: 2284
	public float fade;

	// Token: 0x040008ED RID: 2285
	public bool fadeOnStart;

	// Token: 0x040008EE RID: 2286
	[ConditionalHide("fadeOnStart", true)]
	public float initialFadeTarget;

	// Token: 0x040008EF RID: 2287
	public float fadeInSpeed = 0.25f;

	// Token: 0x040008F0 RID: 2288
	public float fadeOutSpeed = 0.5f;

	// Token: 0x040008F1 RID: 2289
	private float fadeTarget;

	// Token: 0x040008F2 RID: 2290
	public float maxFade = 0.9f;

	// Token: 0x040008F3 RID: 2291
	public float maxObjectFade = 0.8f;

	// Token: 0x040008F4 RID: 2292
	private int fadeID = Shader.PropertyToID("_Fade");
}
