using System;
using UnityEngine;
using UnityEngine.Events;

public class Ach_CheckSpecifics : MonoBehaviour
{
	// Token: 0x060001A0 RID: 416 RVA: 0x0000969B File Offset: 0x0000789B
	private void Start()
	{
		this.CheckItems();
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x000096A3 File Offset: 0x000078A3
	private void OnEnable()
	{
		PlayerItemManager.onItemRefresh.AddListener(new UnityAction(this.CheckItems));
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x000096BB File Offset: 0x000078BB
	private void OnDisable()
	{
		PlayerItemManager.onItemRefresh.RemoveListener(new UnityAction(this.CheckItems));
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x000096D4 File Offset: 0x000078D4
	private void CheckItems()
	{
		if (!this.braceletsGet)
		{
			int braceletsCollected = ItemManager.i.BraceletsCollected;
			this.bracelets.SetProgress(braceletsCollected);
			if (braceletsCollected == 4)
			{
				this.bracelets.UnlockAchievement();
				this.braceletsGet = true;
			}
		}
		if (!this.allItemsGet)
		{
			bool flag = true;
			ItemObject[] array = this.items;
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].IsUnlocked)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				this.allItems.UnlockAchievement();
				this.allItemsGet = true;
			}
		}
	}

	public Achievement bracelets;

	private bool braceletsGet;

	public Achievement allItems;

	public ItemObject[] items;

	private bool allItemsGet;
}
