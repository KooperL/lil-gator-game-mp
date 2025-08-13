using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200008A RID: 138
public class Ach_CheckSpecifics : MonoBehaviour
{
	// Token: 0x060001D6 RID: 470 RVA: 0x000037EB File Offset: 0x000019EB
	private void Start()
	{
		this.CheckItems();
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x000037F3 File Offset: 0x000019F3
	private void OnEnable()
	{
		PlayerItemManager.onItemRefresh.AddListener(new UnityAction(this.CheckItems));
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x0000380B File Offset: 0x00001A0B
	private void OnDisable()
	{
		PlayerItemManager.onItemRefresh.RemoveListener(new UnityAction(this.CheckItems));
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x0001D7D8 File Offset: 0x0001B9D8
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

	// Token: 0x040002C1 RID: 705
	public Achievement bracelets;

	// Token: 0x040002C2 RID: 706
	private bool braceletsGet;

	// Token: 0x040002C3 RID: 707
	public Achievement allItems;

	// Token: 0x040002C4 RID: 708
	public ItemObject[] items;

	// Token: 0x040002C5 RID: 709
	private bool allItemsGet;
}
