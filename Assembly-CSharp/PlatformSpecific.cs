using System;
using Steamworks;
using UnityEngine;

// Token: 0x020001AA RID: 426
public class PlatformSpecific : MonoBehaviour
{
	// Token: 0x060008C6 RID: 2246 RVA: 0x000296BC File Offset: 0x000278BC
	private void OnEnable()
	{
		bool flag = true;
		if (this.debugOnly)
		{
			flag = false;
		}
		if (!this.pc)
		{
			flag = false;
		}
		if (SteamManager.Initialized && SteamUtils.IsSteamRunningOnSteamDeck() && !this.steamDeck)
		{
			flag = false;
		}
		if (!flag)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x04000AD6 RID: 2774
	public bool debugOnly;

	// Token: 0x04000AD7 RID: 2775
	public bool nx = true;

	// Token: 0x04000AD8 RID: 2776
	public bool pc = true;

	// Token: 0x04000AD9 RID: 2777
	public bool nxDebugOnly;

	// Token: 0x04000ADA RID: 2778
	public bool steamDeck = true;
}
