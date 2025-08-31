using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class ItemObject : ScriptableObject
{
	// (get) Token: 0x060000F5 RID: 245 RVA: 0x00006781 File Offset: 0x00004981
	public string DisplayName
	{
		get
		{
			return this.Name;
		}
	}

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
