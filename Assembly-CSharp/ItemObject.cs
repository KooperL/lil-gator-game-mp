using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200004F RID: 79
[CreateAssetMenu]
public class ItemObject : ScriptableObject
{
	// Token: 0x17000012 RID: 18
	// (get) Token: 0x0600011A RID: 282 RVA: 0x00002E97 File Offset: 0x00001097
	public string DisplayName
	{
		get
		{
			return this.Name;
		}
	}

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x0600011B RID: 283 RVA: 0x00002E9F File Offset: 0x0000109F
	// (set) Token: 0x0600011C RID: 284 RVA: 0x0001B13C File Offset: 0x0001933C
	public bool IsUnlocked
	{
		get
		{
			GameData.g.Write(this.id, true);
			return true;
		}
		set
		{
			GameData.g.Write(this.id, true);
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

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x0600011D RID: 285 RVA: 0x00002EB3 File Offset: 0x000010B3
	// (set) Token: 0x0600011E RID: 286 RVA: 0x00002EDA File Offset: 0x000010DA
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

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x0600011F RID: 287 RVA: 0x00002EF5 File Offset: 0x000010F5
	public string Name
	{
		get
		{
			if (this.document != null)
			{
				return this.document.FetchString(this.nameID, Language.English);
			}
			return this.nameID;
		}
	}

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x06000120 RID: 288 RVA: 0x00002F1E File Offset: 0x0000111E
	public string Description
	{
		get
		{
			if (this.document != null)
			{
				return this.document.FetchString(this.descriptionID, Language.English);
			}
			return this.descriptionID;
		}
	}

	// Token: 0x04000195 RID: 405
	public MultilingualTextDocument document;

	// Token: 0x04000196 RID: 406
	[HideInInspector]
	public string displayName;

	// Token: 0x04000197 RID: 407
	[TextLookup("document")]
	public string nameID;

	// Token: 0x04000198 RID: 408
	[TextLookup("document")]
	public string descriptionID;

	// Token: 0x04000199 RID: 409
	public GameObject prefab;

	// Token: 0x0400019A RID: 410
	public Sprite sprite;

	// Token: 0x0400019B RID: 411
	public bool unlockedAtStart = true;

	// Token: 0x0400019C RID: 412
	[Header("ID")]
	public bool automaticID = true;

	// Token: 0x0400019D RID: 413
	public int intID = -1;

	// Token: 0x0400019E RID: 414
	public string id;

	// Token: 0x0400019F RID: 415
	[Header("Shop")]
	public bool hasShopEntry = true;

	// Token: 0x040001A0 RID: 416
	public bool shopUnlockedAtStart;

	// Token: 0x040001A1 RID: 417
	public string shopID;

	// Token: 0x040001A2 RID: 418
	public int shopCost;

	// Token: 0x040001A3 RID: 419
	public ItemManager.ItemType itemType;

	// Token: 0x040001A4 RID: 420
	[HideInInspector]
	public UnityEvent onItemUnlocked;

	// Token: 0x040001A5 RID: 421
	public bool ignoreUnlockAll;
}
