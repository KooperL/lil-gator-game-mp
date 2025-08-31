using System;
using UnityEngine;

public class LoadSaveSlot : MonoBehaviour
{
	// Token: 0x06000142 RID: 322 RVA: 0x00007BCA File Offset: 0x00005DCA
	public void LoadSlot()
	{
		GameData.g.LoadSaveFile(this.index);
	}

	public int index;

	public MainMenuToGameplay toGameplay;
}
