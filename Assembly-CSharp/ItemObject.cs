using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200003D RID: 61
[CreateAssetMenu]
public class ItemObject : ScriptableObject
{
	// Token: 0x17000007 RID: 7
	// (get) Token: 0x060000F5 RID: 245 RVA: 0x00006781 File Offset: 0x00004981
	public string DisplayName
	{
		get
		{
			return this.Name;
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x060000F6 RID: 246 RVA: 0x00006789 File Offset: 0x00004989
	// (set) Token: 0x060000F7 RID: 247 RVA: 0x000067A8 File Offset: 0x000049A8
	public bool IsUnlocked
	{
		get
		{
			return this.unlockedAtStart || GameData.g.ReadBool(this.id, false);
		}
		set
		{
			GameData.g.Write(this.id, value);
			if (value && this.onItemUnlocked != null)
			{
				this.onItemUnlocked.Invoke();
			}
			if (value && SpeedrunData.ShouldTrack)
			{
				string text = this.document.FetchString(this.nameID, Language.English);
				if (!SpeedrunData.unlockedItems.Contains(text))
				{
					SpeedrunData.unlockedItems.Add(text);
				}
			}
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x060000F8 RID: 248 RVA: 0x00006811 File Offset: 0x00004A11
	// (set) Token: 0x060000F9 RID: 249 RVA: 0x00006838 File Offset: 0x00004A38
	public bool IsShopUnlocked
	{
		get
		{
			return this.hasShopEntry && (this.shopUnlockedAtStart || GameData.g.ReadBool(this.shopID, false));
		}
		set
		{
			if (this.hasShopEntry)
			{
				GameData.g.Write(this.shopID, value);
			}
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x060000FA RID: 250 RVA: 0x00006853 File Offset: 0x00004A53
	public string Name
	{
		get
		{
			if (this.document != null)
			{
				return this.document.FetchString(this.nameID, Language.Auto);
			}
			return this.nameID;
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x060000FB RID: 251 RVA: 0x0000687D File Offset: 0x00004A7D
	public string Description
	{
		get
		{
			if (this.document != null)
			{
				return this.document.FetchString(this.descriptionID, Language.Auto);
			}
			return this.descriptionID;
		}
	}

	// Token: 0x0400014E RID: 334
	public MultilingualTextDocument document;

	// Token: 0x0400014F RID: 335
	[HideInInspector]
	public string displayName;

	// Token: 0x04000150 RID: 336
	[TextLookup("document")]
	public string nameID;

	// Token: 0x04000151 RID: 337
	[TextLookup("document")]
	public string descriptionID;

	// Token: 0x04000152 RID: 338
	public GameObject prefab;

	// Token: 0x04000153 RID: 339
	public Sprite sprite;

	// Token: 0x04000154 RID: 340
	public bool unlockedAtStart;

	// Token: 0x04000155 RID: 341
	[Header("ID")]
	public bool automaticID = true;

	// Token: 0x04000156 RID: 342
	public int intID = -1;

	// Token: 0x04000157 RID: 343
	public string id;

	// Token: 0x04000158 RID: 344
	[Header("Shop")]
	public bool hasShopEntry = true;

	// Token: 0x04000159 RID: 345
	public bool shopUnlockedAtStart;

	// Token: 0x0400015A RID: 346
	public string shopID;

	// Token: 0x0400015B RID: 347
	public int shopCost;

	// Token: 0x0400015C RID: 348
	public ItemManager.ItemType itemType;

	// Token: 0x0400015D RID: 349
	[HideInInspector]
	public UnityEvent onItemUnlocked;

	// Token: 0x0400015E RID: 350
	public bool ignoreUnlockAll;
}
