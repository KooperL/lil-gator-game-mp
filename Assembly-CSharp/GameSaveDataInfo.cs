using System;

[Serializable]
public struct GameSaveDataInfo
{
	// Token: 0x06000755 RID: 1877 RVA: 0x000346E4 File Offset: 0x000328E4
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

	public int v;

	public bool isStarted;

	public string playerName;

	public int craftingMaterials;

	public int population;

	public bool collectedCraftingMaterials;

	public int bracelets;

	public bool martin;

	public bool jill;

	public bool avery;

	public bool sis;

	public bool tom;

	public int sword;

	public int shield;

	public int hat;

	public int item;

	public int itemR;

	public int percentObjects;

	public int percentCharacters;

	public int percentItems;

	public int newGameIndex;
}
