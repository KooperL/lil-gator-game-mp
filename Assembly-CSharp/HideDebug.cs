using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001FF RID: 511
public class HideDebug : MonoBehaviour
{
	// Token: 0x0600097B RID: 2427 RVA: 0x00039D7C File Offset: 0x00037F7C
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

	// Token: 0x0600097C RID: 2428 RVA: 0x00009344 File Offset: 0x00007544
	private void Start()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDestroy()
	{
	}

	// Token: 0x04000C26 RID: 3110
	public static bool isHidden = false;

	// Token: 0x04000C27 RID: 3111
	public static List<HideDebug> allHideDebugs = new List<HideDebug>();
}
