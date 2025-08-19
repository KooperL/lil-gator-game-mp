using System;
using UnityEngine;

public class UISetUnlockedItems : MonoBehaviour
{
	// Token: 0x06001316 RID: 4886 RVA: 0x0005E1F4 File Offset: 0x0005C3F4
	public void Set()
	{
		foreach (ItemObject itemObject in ItemManager.i.items)
		{
			itemObject.IsUnlocked = this.unlockedItems.Contains(itemObject);
		}
		Player.itemManager.Refresh();
	}

	public ItemObject[] unlockedItems;

	public int braceletCount = 1;
}
