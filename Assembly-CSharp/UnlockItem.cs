using System;
using UnityEngine;

public class UnlockItem : MonoBehaviour
{
	// Token: 0x0600087A RID: 2170 RVA: 0x000085F3 File Offset: 0x000067F3
	public void Unlock()
	{
		ItemManager.i.UnlockItem(this.item.id);
	}

	public ItemObject item;
}
