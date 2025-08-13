using System;
using UnityEngine;

// Token: 0x02000153 RID: 339
public class UnlockItem : MonoBehaviour
{
	// Token: 0x060006FC RID: 1788 RVA: 0x000234A4 File Offset: 0x000216A4
	public void Unlock()
	{
		ItemManager.i.UnlockItem(this.item.id);
	}

	// Token: 0x04000961 RID: 2401
	public ItemObject item;
}
