using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001B3 RID: 435
public class HideUI : MonoBehaviour
{
	// Token: 0x06000900 RID: 2304 RVA: 0x0002B3C4 File Offset: 0x000295C4
	public static void SetUIHidden(bool isHidden)
	{
		HideUI.isUIHidden = isHidden;
		foreach (HideUI hideUI in HideUI.allHideUI)
		{
			hideUI.SetHidden(isHidden);
		}
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x0002B41C File Offset: 0x0002961C
	public static void Toggle()
	{
		HideUI.SetUIHidden(!HideUI.isUIHidden);
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x0002B42B File Offset: 0x0002962B
	private void Awake()
	{
		this.canvas = base.GetComponent<Canvas>();
		this.lineRenderer = base.GetComponent<LineRenderer>();
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x0002B445 File Offset: 0x00029645
	private void OnEnable()
	{
		HideUI.allHideUI.Add(this);
		this.SetHidden(HideUI.isUIHidden);
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x0002B45D File Offset: 0x0002965D
	private void OnDisable()
	{
		if (HideUI.allHideUI.Contains(this))
		{
			HideUI.allHideUI.Remove(this);
		}
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x0002B478 File Offset: 0x00029678
	private void OnDestroy()
	{
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x0002B47A File Offset: 0x0002967A
	public void SetHidden(bool isHidden)
	{
		if (this.canvas != null)
		{
			this.canvas.enabled = !isHidden;
		}
		if (this.lineRenderer != null)
		{
			this.lineRenderer.enabled = !isHidden;
		}
	}

	// Token: 0x04000B65 RID: 2917
	private static List<HideUI> allHideUI = new List<HideUI>();

	// Token: 0x04000B66 RID: 2918
	public static bool isUIHidden = false;

	// Token: 0x04000B67 RID: 2919
	private Canvas canvas;

	// Token: 0x04000B68 RID: 2920
	private LineRenderer lineRenderer;
}
