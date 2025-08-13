using System;

// Token: 0x02000178 RID: 376
[Serializable]
public struct GameSaveDataInfo
{
	// Token: 0x06000715 RID: 1813 RVA: 0x00032FE8 File Offset: 0x000311E8
	public GameSaveDataInfo(GameSaveData saveData)
	{
		this.v = saveData.v;
		this.isStarted = saveData != null && !string.IsNullOrEmpty(saveData.playerName);
		this.martin = (this.jill = (this.avery = (this.sis = (this.tom = false))));
		this.sword = (this.shield = (this.hat = (this.item = (this.itemR = -1))));
		this.percentObjects = (this.percentCharacters = (this.percentItems = 0));
		this.collectedCraftingMaterials = false;
		if (this.isStarted)
		{
			this.playerName = saveData.playerName;
			if (!saveData.ints.TryGetValue("CraftingMaterials", out this.craftingMaterials))
			{
				this.craftingMaterials = -1;
			}
			if (!saveData.ints.TryGetValue("TotalPopulation", out this.population))
			{
				this.population = -1;
			}
			if (!saveData.ints.TryGetValue("Bracelets", out this.bracelets))
			{
				this.bracelets = -1;
			}
			if (!saveData.bools.TryGetValue("CraftingMaterialItemGet", out this.collectedCraftingMaterials))
			{
				this.collectedCraftingMaterials = false;
			}
			if (!saveData.ints.TryGetValue("PrimaryIndex", out this.sword))
			{
				this.sword = -1;
			}
			if (!saveData.ints.TryGetValue("SecondaryIndex", out this.shield))
			{
				this.shield = -1;
			}
			if (saveData.ints.TryGetValue("HatIndex", out this.hat))
			{
				if (this.hat == 0)
				{
					bool flag = false;
					saveData.bools.TryGetValue("Item_17", out flag);
					if (!flag)
					{
						this.hat = -1;
					}
				}
			}
			else
			{
				this.hat = -1;
			}
			if (!saveData.ints.TryGetValue("ItemIndex", out this.item))
			{
				this.item = -1;
			}
			if (!saveData.ints.TryGetValue("ItemIndex_R", out this.itemR))
			{
				this.itemR = -1;
			}
			if (!saveData.bools.TryGetValue("NPC_BigSis", out this.sis))
			{
				this.sis = false;
			}
			if (!saveData.bools.TryGetValue("NPC_Tut_Horse", out this.martin))
			{
				this.martin = false;
			}
			if (!saveData.bools.TryGetValue("NPC_Tut_Frog", out this.avery))
			{
				this.avery = false;
			}
			if (!saveData.bools.TryGetValue("NPC_Tut_Dog", out this.jill))
			{
				this.jill = false;
			}
			int num;
			if (saveData.ints.TryGetValue("TownState", out num))
			{
				this.tom = num >= 2;
			}
			else
			{
				this.tom = false;
			}
			if (!saveData.ints.TryGetValue("Completion_Objects", out this.percentObjects))
			{
				this.percentObjects = 0;
			}
			if (!saveData.ints.TryGetValue("Completion_Characters", out this.percentCharacters))
			{
				this.percentCharacters = 0;
			}
			if (!saveData.ints.TryGetValue("Completion_Items", out this.percentItems))
			{
				this.percentItems = 0;
				return;
			}
		}
		else
		{
			this.playerName = "";
			this.craftingMaterials = -1;
			this.population = -1;
			this.bracelets = -1;
		}
	}

	// Token: 0x04000988 RID: 2440
	public int v;

	// Token: 0x04000989 RID: 2441
	public bool isStarted;

	// Token: 0x0400098A RID: 2442
	public string playerName;

	// Token: 0x0400098B RID: 2443
	public int craftingMaterials;

	// Token: 0x0400098C RID: 2444
	public int population;

	// Token: 0x0400098D RID: 2445
	public bool collectedCraftingMaterials;

	// Token: 0x0400098E RID: 2446
	public int bracelets;

	// Token: 0x0400098F RID: 2447
	public bool martin;

	// Token: 0x04000990 RID: 2448
	public bool jill;

	// Token: 0x04000991 RID: 2449
	public bool avery;

	// Token: 0x04000992 RID: 2450
	public bool sis;

	// Token: 0x04000993 RID: 2451
	public bool tom;

	// Token: 0x04000994 RID: 2452
	public int sword;

	// Token: 0x04000995 RID: 2453
	public int shield;

	// Token: 0x04000996 RID: 2454
	public int hat;

	// Token: 0x04000997 RID: 2455
	public int item;

	// Token: 0x04000998 RID: 2456
	public int itemR;

	// Token: 0x04000999 RID: 2457
	public int percentObjects;

	// Token: 0x0400099A RID: 2458
	public int percentCharacters;

	// Token: 0x0400099B RID: 2459
	public int percentItems;
}
