using System;
using Steamworks;
using UnityEngine;

// Token: 0x02000224 RID: 548
public class PlatformSpecific : MonoBehaviour
{
	// Token: 0x06000A47 RID: 2631 RVA: 0x0003BF28 File Offset: 0x0003A128
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

	// Token: 0x04000CD6 RID: 3286
	public bool debugOnly;

	// Token: 0x04000CD7 RID: 3287
	public bool nx = true;

	// Token: 0x04000CD8 RID: 3288
	public bool pc = true;

	// Token: 0x04000CD9 RID: 3289
	public bool nxDebugOnly;

	// Token: 0x04000CDA RID: 3290
	public bool steamDeck = true;
}
