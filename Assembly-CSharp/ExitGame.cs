using System;
using UnityEngine;

// Token: 0x020002AA RID: 682
public class ExitGame : MonoBehaviour
{
	// Token: 0x06000E6B RID: 3691 RVA: 0x0004505D File Offset: 0x0004325D
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
