using System;
using UnityEngine;

public class LoadSaveSlot : MonoBehaviour
{
	// Token: 0x06000175 RID: 373 RVA: 0x00003413 File Offset: 0x00001613
	public void LoadSlot()
	{
		GameData.g.LoadSaveFile(this.index);
	}

	public int index;

	public MainMenuToGameplay toGameplay;
}
