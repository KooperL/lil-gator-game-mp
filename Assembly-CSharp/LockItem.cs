using System;
using UnityEngine;

// Token: 0x020001D5 RID: 469
public class LockItem : MonoBehaviour
{
	// Token: 0x060008B2 RID: 2226 RVA: 0x0000881B File Offset: 0x00006A1B
	public void LockItemNowPlease()
	{
		this.item.IsUnlocked = false;
		this.item.IsShopUnlocked = false;
		Player.itemManager.Refresh();
	}

	// Token: 0x04000B49 RID: 2889
	public ItemObject item;
}
