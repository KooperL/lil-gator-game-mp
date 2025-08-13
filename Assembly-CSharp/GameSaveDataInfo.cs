using System;

// Token: 0x0200011F RID: 287
[Serializable]
public struct GameSaveDataInfo
{
	// Token: 0x060005F1 RID: 1521 RVA: 0x0001F1E4 File Offset: 0x0001D3E4
	public GameSaveDataInfo(GameSaveData saveData)
	{
		this.v = saveData.v;
		this.isStarted = saveData != null && !string.IsNullOrEmpty(saveData.playerName);
		this.martin = (this.jill = (this.avery = (this.sis = (this.tom = false))));
		this.sword = (this.shield = (this.hat = (this.item = (this.itemR = -1))));
		this.percentObjects = (this.percentCharacters = (this.percentItems = 0));
		this.collectedCraftingMaterials = false;
		this.newGameIndex = 0;
		if (saveData.ints != null)
		{
			saveData.ints.TryGetValue("NewGameIndex", out this.newGameIndex);
		}
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

	// Token: 0x04000828 RID: 2088
	public int v;

	// Token: 0x04000829 RID: 2089
	public bool isStarted;

	// Token: 0x0400082A RID: 2090
	public string playerName;

	// Token: 0x0400082B RID: 2091
	public int craftingMaterials;

	// Token: 0x0400082C RID: 2092
	public int population;

	// Token: 0x0400082D RID: 2093
	public bool collectedCraftingMaterials;

	// Token: 0x0400082E RID: 2094
	public int bracelets;

	// Token: 0x0400082F RID: 2095
	public bool martin;

	// Token: 0x04000830 RID: 2096
	public bool jill;

	// Token: 0x04000831 RID: 2097
	public bool avery;

	// Token: 0x04000832 RID: 2098
	public bool sis;

	// Token: 0x04000833 RID: 2099
	public bool tom;

	// Token: 0x04000834 RID: 2100
	public int sword;

	// Token: 0x04000835 RID: 2101
	public int shield;

	// Token: 0x04000836 RID: 2102
	public int hat;

	// Token: 0x04000837 RID: 2103
	public int item;

	// Token: 0x04000838 RID: 2104
	public int itemR;

	// Token: 0x04000839 RID: 2105
	public int percentObjects;

	// Token: 0x0400083A RID: 2106
	public int percentCharacters;

	// Token: 0x0400083B RID: 2107
	public int percentItems;

	// Token: 0x0400083C RID: 2108
	public int newGameIndex;
}
