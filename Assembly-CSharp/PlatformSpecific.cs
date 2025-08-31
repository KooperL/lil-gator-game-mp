using System;
using Steamworks;
using UnityEngine;

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

	public bool debugOnly;

	public bool nx = true;

	public bool pc = true;

	public bool nxDebugOnly;

	public bool steamDeck = true;
}
