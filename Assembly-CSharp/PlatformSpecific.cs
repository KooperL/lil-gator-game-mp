using System;
using Steamworks;
using UnityEngine;

public class PlatformSpecific : MonoBehaviour
{
	// Token: 0x06000A91 RID: 2705 RVA: 0x0003D840 File Offset: 0x0003BA40
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

	public bool debugOnly;

	public bool nx = true;

	public bool pc = true;

	public bool nxDebugOnly;

	public bool steamDeck = true;
}
