using System;
using UnityEngine;

public class UnlockItem : MonoBehaviour
{
	// Token: 0x0600087A RID: 2170 RVA: 0x000085E9 File Offset: 0x000067E9
	public void Unlock()
	{
		ItemManager.i.UnlockItem(this.item.id);
	}

	public ItemObject item;
}
