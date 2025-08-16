using System;
using System.Collections.Generic;
using UnityEngine;

public class HideDebug : MonoBehaviour
{
	// Token: 0x060009C1 RID: 2497 RVA: 0x0003B5B0 File Offset: 0x000397B0
	public static void ToggleHidden()
	{
		HideDebug.isHidden = !HideDebug.isHidden;
		while (HideDebug.allHideDebugs.Contains(null))
		{
			HideDebug.allHideDebugs.Remove(null);
		}
		foreach (HideDebug hideDebug in HideDebug.allHideDebugs)
		{
			hideDebug.gameObject.SetActive(!HideDebug.isHidden);
		}
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x0000968D File Offset: 0x0000788D
	private void Start()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDestroy()
	{
	}

	public static bool isHidden = false;

	public static List<HideDebug> allHideDebugs = new List<HideDebug>();
}
