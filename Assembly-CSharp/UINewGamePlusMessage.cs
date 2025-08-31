using System;
using UnityEngine;

public class UINewGamePlusMessage : MonoBehaviour
{
	// Token: 0x0600016A RID: 362 RVA: 0x00008752 File Offset: 0x00006952
	private void Start()
	{
		if (Settings.s.ReadBool("HasDisplayedNewGamePlusMessage", false))
		{
			return;
		}
		if (!FileUtil.HasCompletedSaveData())
		{
			return;
		}
		this.newGamePlusMessageMenu.Activate();
		Settings.s.Write("HasDisplayedNewGamePlusMessage", true);
	}

	private const string newGamePlusMessageKey = "HasDisplayedNewGamePlusMessage";

	public UISubMenu newGamePlusMessageMenu;
}
