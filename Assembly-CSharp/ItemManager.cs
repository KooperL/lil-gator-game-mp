using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
	// (get) Token: 0x0600087C RID: 2172 RVA: 0x000085EC File Offset: 0x000067EC
	// (set) Token: 0x0600087D RID: 2173 RVA: 0x00038078 File Offset: 0x00036278
	public int BraceletsCollected
	{
		get
		{
			return this.gameData.ReadInt("Bracelets", 0);
		}
		set
		{
			value = Mathf.Min(4, value);
			this.gameData.Write("Bracelets", value);
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

	// (get) Token: 0x0600087E RID: 2174 RVA: 0x000085FF File Offset: 0x000067FF
	public static bool HasInfiniteStamina
	{
		get
		{
			return ItemManager.i.BraceletsCollected == 4;
		}
	}

	// (get) Token: 0x0600087F RID: 2175 RVA: 0x0000860E File Offset: 0x0000680E
	public GameData gameData
	{
		get
		{
			return GameData.g;
		}
	}

	// (get) Token: 0x06000880 RID: 2176 RVA: 0x00008615 File Offset: 0x00006815
	// (set) Token: 0x06000881 RID: 2177 RVA: 0x00008628 File Offset: 0x00006828
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

	// (get) Token: 0x06000882 RID: 2178 RVA: 0x0000863B File Offset: 0x0000683B
	// (set) Token: 0x06000883 RID: 2179 RVA: 0x0000864E File Offset: 0x0000684E
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

	// (get) Token: 0x06000884 RID: 2180 RVA: 0x00008661 File Offset: 0x00006861
	// (set) Token: 0x06000885 RID: 2181 RVA: 0x00008674 File Offset: 0x00006874
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

	// (get) Token: 0x06000886 RID: 2182 RVA: 0x00008687 File Offset: 0x00006887
	// (set) Token: 0x06000887 RID: 2183 RVA: 0x000380E8 File Offset: 0x000362E8
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

	// (get) Token: 0x06000888 RID: 2184 RVA: 0x0000869A File Offset: 0x0000689A
	// (set) Token: 0x06000889 RID: 2185 RVA: 0x00038164 File Offset: 0x00036364
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

	// (get) Token: 0x0600088A RID: 2186 RVA: 0x000086AD File Offset: 0x000068AD
	// (set) Token: 0x0600088B RID: 2187 RVA: 0x000086BF File Offset: 0x000068BF
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

	// (get) Token: 0x0600088C RID: 2188 RVA: 0x000086D1 File Offset: 0x000068D1
	// (set) Token: 0x0600088D RID: 2189 RVA: 0x000086E3 File Offset: 0x000068E3
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

	// Token: 0x0600088E RID: 2190 RVA: 0x000381E0 File Offset: 0x000363E0
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

	// Token: 0x0600088F RID: 2191 RVA: 0x00038234 File Offset: 0x00036434
	private void Awake()
	{
		ItemManager.i = this;
		this.itemDic = new Dictionary<string, ItemObject>();
		for (int i = 0; i < this.items.Length; i++)
		{
			this.itemDic.Add(this.items[i].name, this.items[i]);
		}
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x00038288 File Offset: 0x00036488
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

	// Token: 0x06000891 RID: 2193 RVA: 0x000086F5 File Offset: 0x000068F5
	private void Start()
	{
		this.playerItemManager = Player.itemManager;
		this.Load();
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x000382E4 File Offset: 0x000364E4
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

	// Token: 0x06000893 RID: 2195 RVA: 0x00008708 File Offset: 0x00006908
	private bool IsIndexInvalid(int index, ItemManager.ItemType itemType)
	{
		return index < 0 || index >= this.items.Length || this.items[index].itemType != itemType || !this.items[index].IsUnlocked;
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x0000873D File Offset: 0x0000693D
	public void GetPrimary(out string id, out GameObject prefab)
	{
		this.PrimaryIndex = this.GetItemVariant(ItemManager.ItemType.Primary, this.PrimaryIndex, out id, out prefab, -1);
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x00008755 File Offset: 0x00006955
	public string GetPrimaryID()
	{
		return this.items[this.PrimaryIndex].id;
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x00008769 File Offset: 0x00006969
	public void GetSecondary(out string id, out GameObject prefab)
	{
		this.SecondaryIndex = this.GetItemVariant(ItemManager.ItemType.Secondary, this.SecondaryIndex, out id, out prefab, -1);
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x00008781 File Offset: 0x00006981
	public void GetHat(out string id, out GameObject prefab)
	{
		this.HatIndex = this.GetItemVariant(ItemManager.ItemType.Hat, this.HatIndex, out id, out prefab, -1);
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x00008799 File Offset: 0x00006999
	public void GetItem(out string id, out GameObject prefab)
	{
		this.ItemIndex = this.GetItemVariant(ItemManager.ItemType.Item, this.ItemIndex, out id, out prefab, 0);
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x000087B1 File Offset: 0x000069B1
	public void GetItem_R(out string id, out GameObject prefab)
	{
		this.ItemIndex_R = this.GetItemVariant(ItemManager.ItemType.Item, this.ItemIndex_R, out id, out prefab, 1);
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x00038370 File Offset: 0x00036570
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

	// Token: 0x0600089B RID: 2203 RVA: 0x00038438 File Offset: 0x00036638
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

	// Token: 0x0600089C RID: 2204 RVA: 0x000087C9 File Offset: 0x000069C9
	public bool IsItemUnlocked(string itemName)
	{
		return this.gameData.ReadBool(itemName, false);
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x000087D8 File Offset: 0x000069D8
	public void GiveItem(ItemObject item, bool equip = true)
	{
		item.IsUnlocked = true;
		if (equip)
		{
			this.EquipItem(item);
		}
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x00038494 File Offset: 0x00036694
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

	// Token: 0x0600089F RID: 2207 RVA: 0x000384E8 File Offset: 0x000366E8
	public void EquipItem(string itemID, bool refreshNow = true)
	{
		ItemObject itemObject;
		if (this.itemDic.TryGetValue(itemID, out itemObject))
		{
			this.EquipItem(itemObject, refreshNow);
		}
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x00038510 File Offset: 0x00036710
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

	// Token: 0x060008A1 RID: 2209 RVA: 0x000087EB File Offset: 0x000069EB
	public bool IsEquippedAlt(ItemObject item)
	{
		return item.itemType == ItemManager.ItemType.Item && (this.ItemIndex_R >= 0 && this.items[this.ItemIndex_R] == item);
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x00008819 File Offset: 0x00006A19
	public void EquipItem(ItemObject item)
	{
		this.EquipItem(item, true);
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x00038584 File Offset: 0x00036784
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

	// Token: 0x060008A4 RID: 2212 RVA: 0x000386D4 File Offset: 0x000368D4
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

	// Token: 0x060008A5 RID: 2213 RVA: 0x00038770 File Offset: 0x00036970
	public void SwapItemEquipSlots()
	{
		int itemIndex = this.ItemIndex;
		this.ItemIndex = this.ItemIndex_R;
		this.ItemIndex_R = itemIndex;
		Player.itemManager.Refresh();
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x00008823 File Offset: 0x00006A23
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

	// Token: 0x060008A7 RID: 2215 RVA: 0x00008839 File Offset: 0x00006A39
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

	// Token: 0x060008A8 RID: 2216 RVA: 0x00008856 File Offset: 0x00006A56
	public void SetUnlocked(string itemName)
	{
		this.gameData.Write(itemName, true);
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x000387A4 File Offset: 0x000369A4
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

	// Token: 0x060008AA RID: 2218 RVA: 0x00008865 File Offset: 0x00006A65
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

	public static ItemManager i;

	private PlayerItemManager playerItemManager;

	public UIItemGet uiItemGet;

	public int rocksCollected = 10;

	public UINumberToSprite rocksCollectedUI;

	public AudioClip rockSound;

	public const int totalBracelets = 4;

	public ItemObject[] items;

	public Dictionary<string, ItemObject> itemDic;

	public ButtonTutorialUI itemTutorial_left;

	public ButtonTutorialUI itemTutorial_right;

	public string[] itemStrings;

	[Serializable]
	public struct ItemVariant
	{
		public GameObject prefab;

		public string id;

		public int priority;
	}

	[Serializable]
	public struct ItemInfo
	{
		public string name;

		public string displayName;

		public Sprite sprite;

		public ItemManager.ItemType type;
	}

	public enum ItemType
	{
		Primary = 1,
		Hat,
		Secondary,
		Item,
		Equippable,
		Misc = 0
	}
}
