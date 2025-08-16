using System;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
	// Token: 0x0600119B RID: 4507 RVA: 0x0000F01C File Offset: 0x0000D21C
	public void Action()
	{
		if (Game.AllowedToSave)
		{
			GameData.g.WriteToDisk();
		}
		Settings.s.WriteToDisk();
		Application.Quit();
	}
}
