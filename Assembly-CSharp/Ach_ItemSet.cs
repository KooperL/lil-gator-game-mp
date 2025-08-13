using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200008B RID: 139
public class Ach_ItemSet : MonoBehaviour
{
	// Token: 0x060001DB RID: 475 RVA: 0x0001D85C File Offset: 0x0001BA5C
	private void Initialize()
	{
		this.hasInitialized = true;
		this.hatIndex = (this.swordIndex = (this.shieldIndex = (this.item1Index = (this.item2Index = -1))));
		foreach (ItemObject itemObject in this.items)
		{
			switch (itemObject.itemType)
			{
			case ItemManager.ItemType.Primary:
				ItemManager.i.items.TryFindIndex(itemObject, out this.swordIndex);
				break;
			case ItemManager.ItemType.Hat:
				ItemManager.i.items.TryFindIndex(itemObject, out this.hatIndex);
				break;
			case ItemManager.ItemType.Secondary:
				ItemManager.i.items.TryFindIndex(itemObject, out this.shieldIndex);
				break;
			case ItemManager.ItemType.Item:
				if (this.item1Index == -1)
				{
					ItemManager.i.items.TryFindIndex(itemObject, out this.item1Index);
				}
				else
				{
					ItemManager.i.items.TryFindIndex(itemObject, out this.item2Index);
				}
				break;
			}
		}
	}

	// Token: 0x060001DC RID: 476 RVA: 0x00003823 File Offset: 0x00001A23
	private void Start()
	{
		this.CheckItems();
	}

	// Token: 0x060001DD RID: 477 RVA: 0x0000382B File Offset: 0x00001A2B
	private void OnEnable()
	{
		PlayerItemManager.onItemRefresh.AddListener(new UnityAction(this.CheckItems));
	}

	// Token: 0x060001DE RID: 478 RVA: 0x00003843 File Offset: 0x00001A43
	private void OnDisable()
	{
		PlayerItemManager.onItemRefresh.RemoveListener(new UnityAction(this.CheckItems));
	}

	// Token: 0x060001DF RID: 479 RVA: 0x0001D964 File Offset: 0x0001BB64
	private void CheckItems()
	{
		if (!this.hasInitialized)
		{
			this.Initialize();
		}
		if (this.hatIndex != -1 && ItemManager.i.HatIndex != this.hatIndex)
		{
			return;
		}
		if (this.swordIndex != -1 && ItemManager.i.PrimaryIndex != this.swordIndex)
		{
			return;
		}
		if (this.shieldIndex != -1 && ItemManager.i.SecondaryIndex != this.shieldIndex)
		{
			return;
		}
		if (this.item1Index != -1 && ItemManager.i.ItemIndex != this.item1Index && ItemManager.i.ItemIndex_R != this.item1Index)
		{
			return;
		}
		if (this.item2Index != -1 && ItemManager.i.ItemIndex != this.item2Index && ItemManager.i.ItemIndex_R != this.item2Index)
		{
			return;
		}
		this.achievement.UnlockAchievement();
	}

	// Token: 0x040002C6 RID: 710
	public Achievement achievement;

	// Token: 0x040002C7 RID: 711
	public ItemObject[] items;

	// Token: 0x040002C8 RID: 712
	private int hatIndex;

	// Token: 0x040002C9 RID: 713
	private int swordIndex;

	// Token: 0x040002CA RID: 714
	private int shieldIndex;

	// Token: 0x040002CB RID: 715
	private int item1Index;

	// Token: 0x040002CC RID: 716
	private int item2Index;

	// Token: 0x040002CD RID: 717
	private bool hasInitialized;
}
