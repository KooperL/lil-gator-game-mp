using System;
using UnityEngine;

// Token: 0x02000166 RID: 358
public class LockItem : MonoBehaviour
{
	// Token: 0x06000760 RID: 1888 RVA: 0x00024A43 File Offset: 0x00022C43
	public void LockItemNowPlease()
	{
		this.item.IsUnlocked = false;
		this.item.IsShopUnlocked = false;
		Player.itemManager.Refresh();
	}

	// Token: 0x0400099F RID: 2463
	public ItemObject item;
}
