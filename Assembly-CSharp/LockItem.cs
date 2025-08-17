using System;
using UnityEngine;

public class LockItem : MonoBehaviour
{
	// Token: 0x060008F2 RID: 2290 RVA: 0x00008B44 File Offset: 0x00006D44
	public void LockItemNowPlease()
	{
		this.item.IsUnlocked = false;
		this.item.IsShopUnlocked = false;
		Player.itemManager.Refresh();
	}

	public ItemObject item;
}
