using System;
using UnityEngine;

public class UnlockItem : MonoBehaviour
{
	// Token: 0x060006FC RID: 1788 RVA: 0x000234A4 File Offset: 0x000216A4
	public void Unlock()
	{
		ItemManager.i.UnlockItem(this.item.id);
	}

	public ItemObject item;
}
