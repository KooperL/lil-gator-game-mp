using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
	// (get) Token: 0x060006FE RID: 1790 RVA: 0x000234C4 File Offset: 0x000216C4
	// (set) Token: 0x060006FF RID: 1791 RVA: 0x000234D8 File Offset: 0x000216D8
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

	// (get) Token: 0x06000700 RID: 1792 RVA: 0x00023547 File Offset: 0x00021747
	public static bool HasInfiniteStamina
	{
		get
		{
			return ItemManager.i.BraceletsCollected == 4;
		}
	}

	// (get) Token: 0x06000701 RID: 1793 RVA: 0x00023556 File Offset: 0x00021756
	public GameData gameData
	{
		get
		{
			return GameData.g;
		}
	}

	// (get) Token: 0x06000702 RID: 1794 RVA: 0x0002355D File Offset: 0x0002175D
	// (set) Token: 0x06000703 RID: 1795 RVA: 0x00023570 File Offset: 0x00021770
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

	// (get) Token: 0x06000704 RID: 1796 RVA: 0x00023583 File Offset: 0x00021783
	// (set) Token: 0x06000705 RID: 1797 RVA: 0x00023596 File Offset: 0x00021796
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

	// (get) Token: 0x06000706 RID: 1798 RVA: 0x000235A9 File Offset: 0x000217A9
	// (set) Token: 0x06000707 RID: 1799 RVA: 0x000235BC File Offset: 0x000217BC
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

	// (get) Token: 0x06000708 RID: 1800 RVA: 0x000235CF File Offset: 0x000217CF
	// (set) Token: 0x06000709 RID: 1801 RVA: 0x000235E4 File Offset: 0x000217E4
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

	// (get) Token: 0x0600070A RID: 1802 RVA: 0x0002365F File Offset: 0x0002185F
	// (set) Token: 0x0600070B RID: 1803 RVA: 0x00023674 File Offset: 0x00021874
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

	// (get) Token: 0x0600070C RID: 1804 RVA: 0x000236EF File Offset: 0x000218EF
	// (set) Token: 0x0600070D RID: 1805 RVA: 0x00023701 File Offset: 0x00021901
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

	// (get) Token: 0x0600070E RID: 1806 RVA: 0x00023713 File Offset: 0x00021913
	// (set) Token: 0x0600070F RID: 1807 RVA: 0x00023725 File Offset: 0x00021925
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

	// Token: 0x06000710 RID: 1808 RVA: 0x00023738 File Offset: 0x00021938
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

	// Token: 0x06000711 RID: 1809 RVA: 0x0002378C File Offset: 0x0002198C
	private void Awake()
	{
		ItemManager.i = this;
		this.itemDic = new Dictionary<string, ItemObject>();
		for (int i = 0; i < this.items.Length; i++)
		{
			this.itemDic.Add(this.items[i].name, this.items[i]);
		}
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x000237E0 File Offset: 0x000219E0
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

	// Token: 0x06000713 RID: 1811 RVA: 0x00023839 File Offset: 0x00021A39
	private void Start()
	{
		this.playerItemManager = Player.itemManager;
		this.Load();
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x0002384C File Offset: 0x00021A4C
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

	// Token: 0x06000715 RID: 1813 RVA: 0x000238D5 File Offset: 0x00021AD5
	private bool IsIndexInvalid(int index, ItemManager.ItemType itemType)
	{
		return index < 0 || index >= this.items.Length || this.items[index].itemType != itemType || !this.items[index].IsUnlocked;
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x0002390A File Offset: 0x00021B0A
	public void GetPrimary(out string id, out GameObject prefab)
	{
		this.PrimaryIndex = this.GetItemVariant(ItemManager.ItemType.Primary, this.PrimaryIndex, out id, out prefab, -1);
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x00023922 File Offset: 0x00021B22
	public string GetPrimaryID()
	{
		return this.items[this.PrimaryIndex].id;
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x00023936 File Offset: 0x00021B36
	public void GetSecondary(out string id, out GameObject prefab)
	{
		this.SecondaryIndex = this.GetItemVariant(ItemManager.ItemType.Secondary, this.SecondaryIndex, out id, out prefab, -1);
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x0002394E File Offset: 0x00021B4E
	public void GetHat(out string id, out GameObject prefab)
	{
		this.HatIndex = this.GetItemVariant(ItemManager.ItemType.Hat, this.HatIndex, out id, out prefab, -1);
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x00023966 File Offset: 0x00021B66
	public void GetItem(out string id, out GameObject prefab)
	{
		this.ItemIndex = this.GetItemVariant(ItemManager.ItemType.Item, this.ItemIndex, out id, out prefab, 0);
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x0002397E File Offset: 0x00021B7E
	public void GetItem_R(out string id, out GameObject prefab)
	{
		this.ItemIndex_R = this.GetItemVariant(ItemManager.ItemType.Item, this.ItemIndex_R, out id, out prefab, 1);
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x00023998 File Offset: 0x00021B98
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

	// Token: 0x0600071D RID: 1821 RVA: 0x00023A60 File Offset: 0x00021C60
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

	// Token: 0x0600071E RID: 1822 RVA: 0x00023AB9 File Offset: 0x00021CB9
	public bool IsItemUnlocked(string itemName)
	{
		return this.gameData.ReadBool(itemName, false);
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x00023AC8 File Offset: 0x00021CC8
	public void GiveItem(ItemObject item, bool equip = true)
	{
		item.IsUnlocked = true;
		if (equip)
		{
			this.EquipItem(item);
		}
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x00023ADC File Offset: 0x00021CDC
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

	// Token: 0x06000721 RID: 1825 RVA: 0x00023B30 File Offset: 0x00021D30
	public void EquipItem(string itemID, bool refreshNow = true)
	{
		ItemObject itemObject;
		if (this.itemDic.TryGetValue(itemID, out itemObject))
		{
			this.EquipItem(itemObject, refreshNow);
		}
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x00023B58 File Offset: 0x00021D58
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

	// Token: 0x06000723 RID: 1827 RVA: 0x00023BC9 File Offset: 0x00021DC9
	public bool IsEquippedAlt(ItemObject item)
	{
		return item.itemType == ItemManager.ItemType.Item && (this.ItemIndex_R >= 0 && this.items[this.ItemIndex_R] == item);
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x00023BF7 File Offset: 0x00021DF7
	public void EquipItem(ItemObject item)
	{
		this.EquipItem(item, true);
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x00023C04 File Offset: 0x00021E04
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

	// Token: 0x06000726 RID: 1830 RVA: 0x00023D54 File Offset: 0x00021F54
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

	// Token: 0x06000727 RID: 1831 RVA: 0x00023DF0 File Offset: 0x00021FF0
	public void SwapItemEquipSlots()
	{
		int itemIndex = this.ItemIndex;
		this.ItemIndex = this.ItemIndex_R;
		this.ItemIndex_R = itemIndex;
		Player.itemManager.Refresh();
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x00023E21 File Offset: 0x00022021
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

	// Token: 0x06000729 RID: 1833 RVA: 0x00023E37 File Offset: 0x00022037
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

	// Token: 0x0600072A RID: 1834 RVA: 0x00023E54 File Offset: 0x00022054
	public void SetUnlocked(string itemName)
	{
		this.gameData.Write(itemName, true);
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x00023E64 File Offset: 0x00022064
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

	// Token: 0x0600072C RID: 1836 RVA: 0x00023EC1 File Offset: 0x000220C1
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
