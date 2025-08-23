using System;
using System.Collections.Generic;
using UnityEngine;

public class HideUI : MonoBehaviour
{
	// Token: 0x06000AD2 RID: 2770 RVA: 0x0003F648 File Offset: 0x0003D848
	public static void SetUIHidden(bool isHidden)
	{
		HideUI.isUIHidden = isHidden;
		foreach (HideUI hideUI in HideUI.allHideUI)
		{
			hideUI.SetHidden(isHidden);
		}
	}

	// Token: 0x06000AD3 RID: 2771 RVA: 0x0000A50F File Offset: 0x0000870F
	public static void Toggle()
	{
		HideUI.SetUIHidden(!HideUI.isUIHidden);
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x0000A51E File Offset: 0x0000871E
	private void Awake()
	{
		this.canvas = base.GetComponent<Canvas>();
		this.lineRenderer = base.GetComponent<LineRenderer>();
	}

	// Token: 0x06000AD5 RID: 2773 RVA: 0x0000A538 File Offset: 0x00008738
	private void OnEnable()
	{
		HideUI.allHideUI.Add(this);
		this.SetHidden(HideUI.isUIHidden);
	}

	// Token: 0x06000AD6 RID: 2774 RVA: 0x0000A550 File Offset: 0x00008750
	private void OnDisable()
	{
		if (HideUI.allHideUI.Contains(this))
		{
			HideUI.allHideUI.Remove(this);
		}
	}

	// Token: 0x06000AD7 RID: 2775 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDestroy()
	{
	}

	// Token: 0x06000AD8 RID: 2776 RVA: 0x0000A56B File Offset: 0x0000876B
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
