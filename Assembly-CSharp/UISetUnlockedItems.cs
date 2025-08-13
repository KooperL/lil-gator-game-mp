using System;
using UnityEngine;

// Token: 0x020003CE RID: 974
public class UISetUnlockedItems : MonoBehaviour
{
	// Token: 0x060012B6 RID: 4790 RVA: 0x0005C1F0 File Offset: 0x0005A3F0
	public void Set()
	{
		foreach (ItemObject itemObject in ItemManager.i.items)
		{
			itemObject.IsUnlocked = this.unlockedItems.Contains(itemObject);
		}
		Player.itemManager.Refresh();
	}

	// Token: 0x04001825 RID: 6181
	public ItemObject[] unlockedItems;

	// Token: 0x04001826 RID: 6182
	public int braceletCount = 1;
}
