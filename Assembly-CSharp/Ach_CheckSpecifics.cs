using System;
using UnityEngine;
using UnityEngine.Events;

public class Ach_CheckSpecifics : MonoBehaviour
{
	// Token: 0x060001E3 RID: 483 RVA: 0x000038D7 File Offset: 0x00001AD7
	private void Start()
	{
		this.CheckItems();
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x000038DF File Offset: 0x00001ADF
	private void OnEnable()
	{
		PlayerItemManager.onItemRefresh.AddListener(new UnityAction(this.CheckItems));
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x000038F7 File Offset: 0x00001AF7
	private void OnDisable()
	{
		PlayerItemManager.onItemRefresh.RemoveListener(new UnityAction(this.CheckItems));
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x0001E230 File Offset: 0x0001C430
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
