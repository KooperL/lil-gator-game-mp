using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class ItemObject : ScriptableObject
{
	// (get) Token: 0x06000122 RID: 290 RVA: 0x00002F36 File Offset: 0x00001136
	public string DisplayName
	{
		get
		{
			return this.Name;
		}
	}

	// (get) Token: 0x06000123 RID: 291 RVA: 0x00002F3E File Offset: 0x0000113E
	// (set) Token: 0x06000124 RID: 292 RVA: 0x0001B92C File Offset: 0x00019B2C
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

	// (get) Token: 0x06000125 RID: 293 RVA: 0x00002F5B File Offset: 0x0000115B
	// (set) Token: 0x06000126 RID: 294 RVA: 0x00002F82 File Offset: 0x00001182
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

	// (get) Token: 0x06000127 RID: 295 RVA: 0x00002F9D File Offset: 0x0000119D
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

	// (get) Token: 0x06000128 RID: 296 RVA: 0x00002FC7 File Offset: 0x000011C7
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

	public MultilingualTextDocument document;

	[HideInInspector]
	public string displayName;

	[TextLookup("document")]
	public string nameID;

	[TextLookup("document")]
	public string descriptionID;

	public GameObject prefab;

	public Sprite sprite;

	public bool unlockedAtStart;

	[Header("ID")]
	public bool automaticID = true;

	public int intID = -1;

	public string id;

	[Header("Shop")]
	public bool hasShopEntry = true;

	public bool shopUnlockedAtStart;

	public string shopID;

	public int shopCost;

	public ItemManager.ItemType itemType;

	[HideInInspector]
	public UnityEvent onItemUnlocked;

	public bool ignoreUnlockAll;
}
