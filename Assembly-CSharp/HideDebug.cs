using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000189 RID: 393
public class HideDebug : MonoBehaviour
{
	// Token: 0x06000811 RID: 2065 RVA: 0x00026D80 File Offset: 0x00024F80
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

	// Token: 0x06000812 RID: 2066 RVA: 0x00026E04 File Offset: 0x00025004
	private void Start()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x00026E12 File Offset: 0x00025012
	private void OnDestroy()
	{
	}

	// Token: 0x04000A3D RID: 2621
	public static bool isHidden = false;

	// Token: 0x04000A3E RID: 2622
	public static List<HideDebug> allHideDebugs = new List<HideDebug>();
}
