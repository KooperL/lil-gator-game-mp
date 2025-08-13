using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001B8 RID: 440
public class ItemManager : MonoBehaviour
{
	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x0600083C RID: 2108 RVA: 0x000082F2 File Offset: 0x000064F2
	// (set) Token: 0x0600083D RID: 2109 RVA: 0x000368F0 File Offset: 0x00034AF0
	public int BraceletsCollected
	{
		get
		{
			this.gameData.Write("Bracelets", 4);
			return 4;
		}
		set
		{
			this.gameData.Write("Bracelets", 4);
			Player.itemManager.Refresh();
			if (SpeedrunData.ShouldTrack)
			{
				for (int i = 0; i < value; i++)
				{
					string text = string.Format("Bracelet {0:0}", i + 1);
					if (!SpeedrunData.unlockedItems.Contains(text))
					{
						SpeedrunData.unlockedItems.Add(text);
					}
				}
			}
		}
	}

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x0600083E RID: 2110 RVA: 0x000050C3 File Offset: 0x000032C3
	public static bool HasInfiniteStamina
	{
		get
		{
			return true;
		}
	}

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x0600083F RID: 2111 RVA: 0x00008306 File Offset: 0x00006506
	public GameData gameData
	{
		get
		{
			return GameData.g;
		}
	}

	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x06000840 RID: 2112 RVA: 0x0000830D File Offset: 0x0000650D
	// (set) Token: 0x06000841 RID: 2113 RVA: 0x00008320 File Offset: 0x00006520
	public int PrimaryIndex
	{
		get
		{
			return this.gameData.ReadInt("PrimaryIndex", 0);
		}
		set
		{
			this.gameData.Write("PrimaryIndex", value);
		}
	}

	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x06000842 RID: 2114 RVA: 0x00008333 File Offset: 0x00006533
	// (set) Token: 0x06000843 RID: 2115 RVA: 0x00008346 File Offset: 0x00006546
	public int SecondaryIndex
	{
		get
		{
			return this.gameData.ReadInt("SecondaryIndex", 0);
		}
		set
		{
			this.gameData.Write("SecondaryIndex", value);
		}
	}

	// Token: 0x170000CA RID: 202
	// (get) Token: 0x06000844 RID: 2116 RVA: 0x00008359 File Offset: 0x00006559
	// (set) Token: 0x06000845 RID: 2117 RVA: 0x0000836C File Offset: 0x0000656C
	public int HatIndex
	{
		get
		{
			return this.gameData.ReadInt("HatIndex", 0);
		}
		set
		{
			this.gameData.Write("HatIndex", value);
		}
	}

	// Token: 0x170000CB RID: 203
	// (get) Token: 0x06000846 RID: 2118 RVA: 0x0000837F File Offset: 0x0000657F
	// (set) Token: 0x06000847 RID: 2119 RVA: 0x00036958 File Offset: 0x00034B58
	public int ItemIndex
	{
		get
		{
			return this.gameData.ReadInt("ItemIndex", -1);
		}
		set
		{
			this.gameData.Write("ItemIndex", value);
			if (value != -1 && this.itemTutorial_left != null && !this.HasItemLeftTutorialTriggered)
			{
				this.HasItemLeftTutorialTriggered = true;
				this.itemTutorial_left.Activate();
			}
			if (value == -1 && this.itemTutorial_left != null && this.itemTutorial_left.enabled)
			{
				this.HasItemLeftTutorialTriggered = false;
				this.itemTutorial_left.Hide();
			}
		}
	}

	// Token: 0x170000CC RID: 204
	// (get) Token: 0x06000848 RID: 2120 RVA: 0x00008392 File Offset: 0x00006592
	// (set) Token: 0x06000849 RID: 2121 RVA: 0x000369D4 File Offset: 0x00034BD4
	public int ItemIndex_R
	{
		get
		{
			return this.gameData.ReadInt("ItemIndex_R", -1);
		}
		set
		{
			this.gameData.Write("ItemIndex_R", value);
			if (value != -1 && this.itemTutorial_right != null && !this.HasItemRightTutorialTriggered)
			{
				this.HasItemRightTutorialTriggered = true;
				this.itemTutorial_right.Activate();
			}
			if (value == -1 && this.itemTutorial_right != null && this.itemTutorial_right.enabled)
			{
				this.HasItemRightTutorialTriggered = false;
				this.itemTutorial_right.Hide();
			}
		}
	}

	// Token: 0x170000CD RID: 205
	// (get) Token: 0x0600084A RID: 2122 RVA: 0x000083A5 File Offset: 0x000065A5
	// (set) Token: 0x0600084B RID: 2123 RVA: 0x000083B7 File Offset: 0x000065B7
	private bool HasItemLeftTutorialTriggered
	{
		get
		{
			return GameData.g.ReadBool("ItemLeftTutorial", false);
		}
		set
		{
			GameData.g.Write("ItemLeftTutorial", value);
		}
	}

	// Token: 0x170000CE RID: 206
	// (get) Token: 0x0600084C RID: 2124 RVA: 0x000083C9 File Offset: 0x000065C9
	// (set) Token: 0x0600084D RID: 2125 RVA: 0x000083DB File Offset: 0x000065DB
	private bool HasItemRightTutorialTriggered
	{
		get
		{
			return GameData.g.ReadBool("ItemRightTutorial", false);
		}
		set
		{
			GameData.g.Write("ItemRightTutorial", value);
		}
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x00036A50 File Offset: 0x00034C50
	[ContextMenu("Remove Duplicate Items")]
	public void RemoveDuplicateItems()
	{
		List<ItemObject> list = new List<ItemObject>();
		foreach (ItemObject itemObject in this.items)
		{
			if (itemObject != null && !list.Contains(itemObject))
			{
				list.Add(itemObject);
			}
		}
		this.items = list.ToArray();
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x00036AA4 File Offset: 0x00034CA4
	private void Awake()
	{
		ItemManager.i = this;
		this.itemDic = new Dictionary<string, ItemObject>();
		for (int i = 0; i < this.items.Length; i++)
		{
			this.itemDic.Add(this.items[i].name, this.items[i]);
		}
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x00036AF8 File Offset: 0x00034CF8
	private void OnEnable()
	{
		ItemManager.i = this;
		if (this.itemDic == null)
		{
			this.itemDic = new Dictionary<string, ItemObject>();
			for (int i = 0; i < this.items.Length; i++)
			{
				this.itemDic.Add(this.items[i].name, this.items[i]);
			}
		}
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x000083ED File Offset: 0x000065ED
	private void Start()
	{
		this.playerItemManager = Player.itemManager;
		this.Load();
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x00036B54 File Offset: 0x00034D54
	private void Load()
	{
		if (this.IsIndexInvalid(this.PrimaryIndex, ItemManager.ItemType.Primary))
		{
			this.PrimaryIndex = -1;
		}
		if (this.IsIndexInvalid(this.SecondaryIndex, ItemManager.ItemType.Secondary))
		{
			this.SecondaryIndex = -1;
		}
		if (this.IsIndexInvalid(this.HatIndex, ItemManager.ItemType.Hat))
		{
			this.HatIndex = -1;
		}
		if (this.IsIndexInvalid(this.ItemIndex, ItemManager.ItemType.Item))
		{
			this.ItemIndex = -1;
		}
		if (this.IsIndexInvalid(this.ItemIndex_R, ItemManager.ItemType.Item) || this.ItemIndex_R == this.ItemIndex)
		{
			this.ItemIndex_R = -1;
		}
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x00008400 File Offset: 0x00006600
	private bool IsIndexInvalid(int index, ItemManager.ItemType itemType)
	{
		return index < 0 || index >= this.items.Length || this.items[index].itemType != itemType || !this.items[index].IsUnlocked;
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x00008435 File Offset: 0x00006635
	public void GetPrimary(out string id, out GameObject prefab)
	{
		this.PrimaryIndex = this.GetItemVariant(ItemManager.ItemType.Primary, this.PrimaryIndex, out id, out prefab, -1);
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x0000844D File Offset: 0x0000664D
	public string GetPrimaryID()
	{
		return this.items[this.PrimaryIndex].id;
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x00008461 File Offset: 0x00006661
	public void GetSecondary(out string id, out GameObject prefab)
	{
		this.SecondaryIndex = this.GetItemVariant(ItemManager.ItemType.Secondary, this.SecondaryIndex, out id, out prefab, -1);
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x00008479 File Offset: 0x00006679
	public void GetHat(out string id, out GameObject prefab)
	{
		this.HatIndex = this.GetItemVariant(ItemManager.ItemType.Hat, this.HatIndex, out id, out prefab, -1);
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x00008491 File Offset: 0x00006691
	public void GetItem(out string id, out GameObject prefab)
	{
		this.ItemIndex = this.GetItemVariant(ItemManager.ItemType.Item, this.ItemIndex, out id, out prefab, 0);
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x000084A9 File Offset: 0x000066A9
	public void GetItem_R(out string id, out GameObject prefab)
	{
		this.ItemIndex_R = this.GetItemVariant(ItemManager.ItemType.Item, this.ItemIndex_R, out id, out prefab, 1);
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x00036BE0 File Offset: 0x00034DE0
	public int GetItemVariant(ItemManager.ItemType itemType, int index, out string id, out GameObject prefab, int itemIndex = -1)
	{
		id = "";
		prefab = null;
		if (index == -1 || !this.items[index].IsUnlocked || this.items[index].itemType != itemType)
		{
			this.Cycle(itemType, ref index);
			if (itemIndex != -1 && index == ((itemIndex == 0) ? this.ItemIndex_R : this.ItemIndex))
			{
				this.Cycle(itemType, ref index);
			}
			if (itemIndex != -1 && index == ((itemIndex == 0) ? this.ItemIndex_R : this.ItemIndex))
			{
				index = -1;
			}
		}
		if (index != -1 && this.items[index].IsUnlocked && this.items[index].itemType == itemType)
		{
			id = this.items[index].id;
			prefab = this.items[index].prefab;
		}
		return index;
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x00036CA8 File Offset: 0x00034EA8
	public void Cycle(ItemManager.ItemType itemType, ref int index)
	{
		int num = index;
		do
		{
			index++;
			if (index >= this.items.Length)
			{
				if (num == -1)
				{
					index = -1;
				}
				else
				{
					index = 0;
				}
			}
		}
		while (index != -1 && (!this.items[index].IsUnlocked || this.items[index].itemType != itemType) && index != num);
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x000084C1 File Offset: 0x000066C1
	public bool IsItemUnlocked(string itemName)
	{
		return this.gameData.ReadBool(itemName, false);
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x000084D0 File Offset: 0x000066D0
	public void GiveItem(ItemObject item, bool equip = true)
	{
		item.IsUnlocked = true;
		if (equip)
		{
			this.EquipItem(item);
		}
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x00036D04 File Offset: 0x00034F04
	public Coroutine UnlockItem(string itemID)
	{
		this.EquipItem(itemID, true);
		if (itemID != null && itemID == "Bracelet")
		{
			return this.GetBracelet();
		}
		this.SetUnlocked(itemID);
		UIItem.RefreshAll();
		Player.itemManager.Refresh();
		return base.StartCoroutine(this.ItemGetSequence(itemID, ""));
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x00036D58 File Offset: 0x00034F58
	public void EquipItem(string itemID, bool refreshNow = true)
	{
		ItemObject itemObject;
		if (this.itemDic.TryGetValue(itemID, out itemObject))
		{
			this.EquipItem(itemObject, refreshNow);
		}
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x00036D80 File Offset: 0x00034F80
	public bool IsEquipped(ItemObject item)
	{
		int num = -1;
		switch (item.itemType)
		{
		case ItemManager.ItemType.Primary:
			num = this.PrimaryIndex;
			break;
		case ItemManager.ItemType.Hat:
			num = this.HatIndex;
			break;
		case ItemManager.ItemType.Secondary:
			num = this.SecondaryIndex;
			break;
		case ItemManager.ItemType.Item:
			num = this.ItemIndex;
			if (this.IsEquippedAlt(item))
			{
				return true;
			}
			break;
		}
		return num >= 0 && this.items[num] == item;
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x000084E3 File Offset: 0x000066E3
	public bool IsEquippedAlt(ItemObject item)
	{
		return item.itemType == ItemManager.ItemType.Item && (this.ItemIndex_R >= 0 && this.items[this.ItemIndex_R] == item);
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x00008511 File Offset: 0x00006711
	public void EquipItem(ItemObject item)
	{
		this.EquipItem(item, true);
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x00036DF4 File Offset: 0x00034FF4
	public void EquipItem(ItemObject item, bool refreshNow = true)
	{
		if (!Player.itemManager.usePersistentItems)
		{
			switch (item.itemType)
			{
			case ItemManager.ItemType.Primary:
				Player.itemManager.nonPersistentPrimary = item;
				break;
			case ItemManager.ItemType.Hat:
				Player.itemManager.nonPersistentHat = item;
				break;
			case ItemManager.ItemType.Secondary:
				Player.itemManager.nonPersistentSecondary = item;
				break;
			case ItemManager.ItemType.Item:
				Player.itemManager.nonPersistentItem = item;
				break;
			}
			Player.itemManager.Refresh();
			return;
		}
		if (!item.IsUnlocked)
		{
			this.SetUnlocked(item.id);
		}
		bool flag = false;
		int num = -1;
		for (int i = 0; i < this.items.Length; i++)
		{
			if (this.items[i] == item)
			{
				num = i;
				break;
			}
		}
		switch (item.itemType)
		{
		case ItemManager.ItemType.Primary:
			if (this.PrimaryIndex != num)
			{
				this.PrimaryIndex = num;
				flag = true;
			}
			break;
		case ItemManager.ItemType.Hat:
			if (this.HatIndex != num)
			{
				this.HatIndex = num;
				flag = true;
			}
			break;
		case ItemManager.ItemType.Secondary:
			if (this.SecondaryIndex != num)
			{
				this.SecondaryIndex = num;
				flag = true;
			}
			break;
		case ItemManager.ItemType.Item:
			if (this.ItemIndex != num && this.ItemIndex_R != num)
			{
				if (this.ItemIndex == -1)
				{
					this.ItemIndex = num;
				}
				else
				{
					this.ItemIndex_R = num;
				}
				flag = true;
			}
			break;
		}
		if (refreshNow && flag)
		{
			Player.itemManager.Refresh();
		}
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x00036F44 File Offset: 0x00035144
	public void EquipItem(ItemObject item, int slot)
	{
		if (item.itemType != ItemManager.ItemType.Item)
		{
			return;
		}
		if (!item.IsUnlocked)
		{
			this.SetUnlocked(item.id);
		}
		int num = -1;
		for (int i = 0; i < this.items.Length; i++)
		{
			if (this.items[i] == item)
			{
				num = i;
				break;
			}
		}
		int num2;
		if (slot != 0)
		{
			if (slot != 1)
			{
				num2 = -1;
			}
			else
			{
				num2 = this.ItemIndex_R;
			}
		}
		else
		{
			num2 = this.ItemIndex;
		}
		if (num2 != num)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					this.ItemIndex_R = num;
				}
			}
			else
			{
				this.ItemIndex = num;
			}
			Player.itemManager.Refresh();
		}
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x00036FE0 File Offset: 0x000351E0
	public void SwapItemEquipSlots()
	{
		int itemIndex = this.ItemIndex;
		this.ItemIndex = this.ItemIndex_R;
		this.ItemIndex_R = itemIndex;
		Player.itemManager.Refresh();
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x0000851B File Offset: 0x0000671B
	private IEnumerator TutorialItemGet(string itemName)
	{
		yield return base.StartCoroutine(this.ItemGetSequence(itemName, ""));
		string text = "";
		int num = 0;
		if (!GameData.g.ReadBool("Hat1", false))
		{
			text = "Hat";
			num++;
		}
		if (!GameData.g.ReadBool("Triangle1", false))
		{
			text = "Triangle";
			num++;
		}
		if (!GameData.g.ReadBool("Sword1", false))
		{
			text = "Sword";
			num++;
		}
		switch (num)
		{
		case 0:
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_FoundAll", null, DialogueManager.DialogueBoxBackground.Standard, true));
			break;
		case 1:
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_Last" + text, null, DialogueManager.DialogueBoxBackground.Standard, true));
			break;
		}
		yield break;
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x00008531 File Offset: 0x00006731
	private IEnumerator ItemGetSequence(string itemName, string dialogueName = "")
	{
		if (dialogueName == "")
		{
			dialogueName = "ItemUnlock" + itemName;
		}
		Game.DialogueDepth++;
		ItemObject itemObject;
		if (this.itemDic.TryGetValue(itemName, out itemObject))
		{
			this.uiItemGet.Activate(itemObject.sprite, itemObject.DisplayName);
		}
		else
		{
			this.uiItemGet.Activate(null, "Item");
		}
		yield return new WaitForSeconds(0.5f);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk(dialogueName, null, DialogueManager.DialogueBoxBackground.Standard, true));
		this.uiItemGet.Deactivate();
		Game.DialogueDepth--;
		yield break;
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x0000854E File Offset: 0x0000674E
	public void SetUnlocked(string itemName)
	{
		this.gameData.Write(itemName, true);
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x00037014 File Offset: 0x00035214
	private Coroutine GetBracelet()
	{
		int braceletsCollected = this.BraceletsCollected;
		this.BraceletsCollected = braceletsCollected + 1;
		Player.itemManager.Refresh();
		if (this.BraceletsCollected == 1)
		{
			return base.StartCoroutine(this.ItemGetSequence("Bracelet", ""));
		}
		return base.StartCoroutine(this.ItemGetSequence("Bracelet", "ItemUnlockAnotherBracelet"));
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x0000855D File Offset: 0x0000675D
	private Coroutine GetTrash()
	{
		if (!this.IsItemUnlocked("Trash"))
		{
			this.SetUnlocked("Trash");
			Player.itemManager.Refresh();
			return base.StartCoroutine(this.ItemGetSequence("Trash", ""));
		}
		return null;
	}

	// Token: 0x04000AEA RID: 2794
	public static ItemManager i;

	// Token: 0x04000AEB RID: 2795
	private PlayerItemManager playerItemManager;

	// Token: 0x04000AEC RID: 2796
	public UIItemGet uiItemGet;

	// Token: 0x04000AED RID: 2797
	public int rocksCollected = 10;

	// Token: 0x04000AEE RID: 2798
	public UINumberToSprite rocksCollectedUI;

	// Token: 0x04000AEF RID: 2799
	public AudioClip rockSound;

	// Token: 0x04000AF0 RID: 2800
	public const int totalBracelets = 4;

	// Token: 0x04000AF1 RID: 2801
	public ItemObject[] items;

	// Token: 0x04000AF2 RID: 2802
	public Dictionary<string, ItemObject> itemDic;

	// Token: 0x04000AF3 RID: 2803
	public ButtonTutorialUI itemTutorial_left;

	// Token: 0x04000AF4 RID: 2804
	public ButtonTutorialUI itemTutorial_right;

	// Token: 0x020001B9 RID: 441
	[Serializable]
	public struct ItemVariant
	{
		// Token: 0x04000AF5 RID: 2805
		public GameObject prefab;

		// Token: 0x04000AF6 RID: 2806
		public string id;

		// Token: 0x04000AF7 RID: 2807
		public int priority;
	}

	// Token: 0x020001BA RID: 442
	[Serializable]
	public struct ItemInfo
	{
		// Token: 0x04000AF8 RID: 2808
		public string name;

		// Token: 0x04000AF9 RID: 2809
		public string displayName;

		// Token: 0x04000AFA RID: 2810
		public Sprite sprite;

		// Token: 0x04000AFB RID: 2811
		public ItemManager.ItemType type;
	}

	// Token: 0x020001BB RID: 443
	public enum ItemType
	{
		// Token: 0x04000AFD RID: 2813
		Primary = 1,
		// Token: 0x04000AFE RID: 2814
		Hat,
		// Token: 0x04000AFF RID: 2815
		Secondary,
		// Token: 0x04000B00 RID: 2816
		Item,
		// Token: 0x04000B01 RID: 2817
		Equippable,
		// Token: 0x04000B02 RID: 2818
		Misc = 0
	}
}
