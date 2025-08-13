using System;
using UnityEngine;

// Token: 0x02000071 RID: 113
public class LoadSaveSlot : MonoBehaviour
{
	// Token: 0x0600016D RID: 365 RVA: 0x00003370 File Offset: 0x00001570
	public void LoadSlot()
	{
		GameData.g.LoadSaveFile(this.index);
	}

	// Token: 0x04000228 RID: 552
	public int index;

	// Token: 0x04000229 RID: 553
	public MainMenuToGameplay toGameplay;
}
