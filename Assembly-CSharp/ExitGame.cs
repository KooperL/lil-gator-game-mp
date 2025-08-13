using System;
using UnityEngine;

// Token: 0x02000387 RID: 903
public class ExitGame : MonoBehaviour
{
	// Token: 0x0600113B RID: 4411 RVA: 0x0000EC48 File Offset: 0x0000CE48
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
