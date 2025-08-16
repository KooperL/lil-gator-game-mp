using System;
using UnityEngine;

public class UINewGamePlusMessage : MonoBehaviour
{
	// Token: 0x060001A3 RID: 419 RVA: 0x00003609 File Offset: 0x00001809
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
