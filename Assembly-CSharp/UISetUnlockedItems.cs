using System;
using UnityEngine;

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

	public ItemObject[] unlockedItems;

	public int braceletCount = 1;
}
