using System;
using UnityEngine;

public class LockItem : MonoBehaviour
{
	// Token: 0x06000760 RID: 1888 RVA: 0x00024A43 File Offset: 0x00022C43
	public void LockItemNowPlease()
	{
		this.item.IsUnlocked = false;
		this.item.IsShopUnlocked = false;
		Player.itemManager.Refresh();
	}

	public ItemObject item;
}
