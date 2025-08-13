using System;
using UnityEngine;

// Token: 0x020002E0 RID: 736
public class UISetUnlockedItems : MonoBehaviour
{
	// Token: 0x06000F96 RID: 3990 RVA: 0x0004AD90 File Offset: 0x00048F90
	public void Set()
	{
		foreach (ItemObject itemObject in ItemManager.i.items)
		{
			itemObject.IsUnlocked = this.unlockedItems.Contains(itemObject);
		}
		Player.itemManager.Refresh();
	}

	// Token: 0x04001473 RID: 5235
	public ItemObject[] unlockedItems;

	// Token: 0x04001474 RID: 5236
	public int braceletCount = 1;
}
