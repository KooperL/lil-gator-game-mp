using System;
using System.Collections.Generic;
using UnityEngine;

public class HideUI : MonoBehaviour
{
	// Token: 0x06000AD1 RID: 2769 RVA: 0x0003F1EC File Offset: 0x0003D3EC
	public static void SetUIHidden(bool isHidden)
	{
		HideUI.isUIHidden = isHidden;
		foreach (HideUI hideUI in HideUI.allHideUI)
		{
			hideUI.SetHidden(isHidden);
		}
	}

	// Token: 0x06000AD2 RID: 2770 RVA: 0x0000A4F0 File Offset: 0x000086F0
	public static void Toggle()
	{
		HideUI.SetUIHidden(!HideUI.isUIHidden);
	}

	// Token: 0x06000AD3 RID: 2771 RVA: 0x0000A4FF File Offset: 0x000086FF
	private void Awake()
	{
		this.canvas = base.GetComponent<Canvas>();
		this.lineRenderer = base.GetComponent<LineRenderer>();
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x0000A519 File Offset: 0x00008719
	private void OnEnable()
	{
		HideUI.allHideUI.Add(this);
		this.SetHidden(HideUI.isUIHidden);
	}

	// Token: 0x06000AD5 RID: 2773 RVA: 0x0000A531 File Offset: 0x00008731
	private void OnDisable()
	{
		if (HideUI.allHideUI.Contains(this))
		{
			HideUI.allHideUI.Remove(this);
		}
	}

	// Token: 0x06000AD6 RID: 2774 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDestroy()
	{
	}

	// Token: 0x06000AD7 RID: 2775 RVA: 0x0000A54C File Offset: 0x0000874C
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

	private static List<HideUI> allHideUI = new List<HideUI>();

	public static bool isUIHidden = false;

	private Canvas canvas;

	private LineRenderer lineRenderer;
}
