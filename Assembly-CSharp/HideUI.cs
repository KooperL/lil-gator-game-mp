using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200022F RID: 559
public class HideUI : MonoBehaviour
{
	// Token: 0x06000A87 RID: 2695 RVA: 0x0003D8D4 File Offset: 0x0003BAD4
	public static void SetUIHidden(bool isHidden)
	{
		HideUI.isUIHidden = isHidden;
		foreach (HideUI hideUI in HideUI.allHideUI)
		{
			hideUI.SetHidden(isHidden);
		}
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x0000A1D1 File Offset: 0x000083D1
	public static void Toggle()
	{
		HideUI.SetUIHidden(!HideUI.isUIHidden);
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x0000A1E0 File Offset: 0x000083E0
	private void Awake()
	{
		this.canvas = base.GetComponent<Canvas>();
		this.lineRenderer = base.GetComponent<LineRenderer>();
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x0000A1FA File Offset: 0x000083FA
	private void OnEnable()
	{
		HideUI.allHideUI.Add(this);
		this.SetHidden(HideUI.isUIHidden);
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x0000A212 File Offset: 0x00008412
	private void OnDisable()
	{
		if (HideUI.allHideUI.Contains(this))
		{
			HideUI.allHideUI.Remove(this);
		}
	}

	// Token: 0x06000A8C RID: 2700 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDestroy()
	{
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x0000A22D File Offset: 0x0000842D
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

	// Token: 0x04000D6D RID: 3437
	private static List<HideUI> allHideUI = new List<HideUI>();

	// Token: 0x04000D6E RID: 3438
	public static bool isUIHidden = false;

	// Token: 0x04000D6F RID: 3439
	private Canvas canvas;

	// Token: 0x04000D70 RID: 3440
	private LineRenderer lineRenderer;
}
