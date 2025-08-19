using System;
using UnityEngine;
using UnityEngine.Events;

public class Ach_ItemSet : MonoBehaviour
{
	// Token: 0x060001E8 RID: 488 RVA: 0x0001E27C File Offset: 0x0001C47C
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

	// Token: 0x060001E9 RID: 489 RVA: 0x0000390F File Offset: 0x00001B0F
	private void Start()
	{
		this.CheckItems();
	}

	// Token: 0x060001EA RID: 490 RVA: 0x00003917 File Offset: 0x00001B17
	private void OnEnable()
	{
		PlayerItemManager.onItemRefresh.AddListener(new UnityAction(this.CheckItems));
	}

	// Token: 0x060001EB RID: 491 RVA: 0x0000392F File Offset: 0x00001B2F
	private void OnDisable()
	{
		PlayerItemManager.onItemRefresh.RemoveListener(new UnityAction(this.CheckItems));
	}

	// Token: 0x060001EC RID: 492 RVA: 0x0001E384 File Offset: 0x0001C584
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

	public Achievement achievement;

	public ItemObject[] items;

	private int hatIndex;

	private int swordIndex;

	private int shieldIndex;

	private int item1Index;

	private int item2Index;

	private bool hasInitialized;
}
