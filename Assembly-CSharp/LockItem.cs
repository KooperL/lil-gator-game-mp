using System;
using UnityEngine;

public class LockItem : MonoBehaviour
{
	// Token: 0x060008F3 RID: 2291 RVA: 0x00008B4E File Offset: 0x00006D4E
	public void LockItemNowPlease()
	{
		this.item.IsUnlocked = false;
		this.item.IsShopUnlocked = false;
		Player.itemManager.Refresh();
	}

	public ItemObject item;
}
