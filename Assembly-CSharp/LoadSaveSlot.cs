using System;
using UnityEngine;

// Token: 0x02000057 RID: 87
public class LoadSaveSlot : MonoBehaviour
{
	// Token: 0x06000142 RID: 322 RVA: 0x00007BCA File Offset: 0x00005DCA
	public void LoadSlot()
	{
		GameData.g.LoadSaveFile(this.index);
	}

	// Token: 0x040001C1 RID: 449
	public int index;

	// Token: 0x040001C2 RID: 450
	public MainMenuToGameplay toGameplay;
}
