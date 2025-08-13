using System;
using UnityEngine;

// Token: 0x020001B7 RID: 439
public class UnlockItem : MonoBehaviour
{
	// Token: 0x0600083A RID: 2106 RVA: 0x000082DA File Offset: 0x000064DA
	public void Unlock()
	{
		ItemManager.i.UnlockItem(this.item.id);
	}

	// Token: 0x04000AE9 RID: 2793
	public ItemObject item;
}
