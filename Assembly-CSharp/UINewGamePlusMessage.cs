using System;
using UnityEngine;

// Token: 0x02000061 RID: 97
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

	// Token: 0x04000210 RID: 528
	private const string newGamePlusMessageKey = "HasDisplayedNewGamePlusMessage";

	// Token: 0x04000211 RID: 529
	public UISubMenu newGamePlusMessageMenu;
}
