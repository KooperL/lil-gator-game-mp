using System;
using UnityEngine;

public class UISetUnlockedItems : MonoBehaviour
{
	// Token: 0x06001317 RID: 4887 RVA: 0x0005E4E0 File Offset: 0x0005C6E0
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
