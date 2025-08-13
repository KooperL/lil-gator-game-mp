using System;
using System.Collections.Generic;
using Rewired;
using RewiredConsts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020003E1 RID: 993
public class UISwapItemsMenu : MonoBehaviour, ICheckCancel
{
	// Token: 0x06001325 RID: 4901 RVA: 0x0005D7DC File Offset: 0x0005B9DC
	public void Activate()
	{
		if (this.rePlayer == null)
		{
			this.rePlayer = ReInput.players.GetPlayer(0);
		}
		this.eventSystem = EventSystem.current;
		this.itemMenuLayer = global::Player.animator.GetLayerIndex("Item Menu");
		Game.State = GameState.ItemMenu;
		base.gameObject.SetActive(true);
		if (this.useMenuCamera)
		{
			global::Player.itemManager.menuCamera.SetActive(true);
		}
		this.resource.ForceShow = true;
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnEquipLeft), 0, 3, this.equipLeftAction);
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnEquipRight), 0, 3, this.equipRightAction);
		this.UpdateInventories();
	}

	// Token: 0x06001326 RID: 4902 RVA: 0x0005D89C File Offset: 0x0005BA9C
	public void Deactivate()
	{
		if (this == null || !Application.isPlaying)
		{
			return;
		}
		this.resource.ForceShow = false;
		this.SetEquipWindow(false);
		this.SetShowItemState(ItemManager.ItemType.Misc);
		if (this.useMenuCamera && global::Player.itemManager != null)
		{
			global::Player.itemManager.menuCamera.SetActive(false);
		}
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnEquipLeft));
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnEquipRight));
	}

	// Token: 0x06001327 RID: 4903 RVA: 0x00010428 File Offset: 0x0000E628
	private void Update()
	{
		this.ForceCorrectSelection();
	}

	// Token: 0x06001328 RID: 4904 RVA: 0x00010430 File Offset: 0x0000E630
	private void ForceCorrectSelection()
	{
		this.eventSystem.currentSelectedGameObject == null;
	}

	// Token: 0x06001329 RID: 4905 RVA: 0x00010444 File Offset: 0x0000E644
	public void OnCancel(BaseEventData eventData)
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600132A RID: 4906 RVA: 0x0005D928 File Offset: 0x0005BB28
	private void UpdateInventories()
	{
		List<ItemObject> list = new List<ItemObject>();
		List<ItemObject> list2 = new List<ItemObject>();
		List<ItemObject> list3 = new List<ItemObject>();
		List<ItemObject> list4 = new List<ItemObject>();
		this.equippedHat = (this.equippedPrimary = (this.equippedSecondary = (this.equippedItem = (this.equippedItem_r = null))));
		int num4;
		int num3;
		int num2;
		int num = (num2 = (num3 = (num4 = 0)));
		for (int i = 0; i < ItemManager.i.items.Length; i++)
		{
			ItemObject itemObject = ItemManager.i.items[i];
			if (itemObject.itemType != ItemManager.ItemType.Misc && itemObject.itemType != ItemManager.ItemType.Equippable)
			{
				bool flag = false;
				if (this.showAll && (itemObject.IsUnlocked || itemObject.IsShopUnlocked))
				{
					flag = true;
				}
				else if (this.isShop && !itemObject.IsUnlocked && itemObject.IsShopUnlocked)
				{
					flag = true;
				}
				else if (!this.isShop && itemObject.IsUnlocked)
				{
					flag = true;
				}
				if (flag)
				{
					switch (itemObject.itemType)
					{
					case ItemManager.ItemType.Primary:
						list2.Add(itemObject);
						if (i == ItemManager.i.PrimaryIndex)
						{
							this.equippedPrimary = itemObject;
							num = list2.Count - 1;
						}
						break;
					case ItemManager.ItemType.Hat:
						list.Add(itemObject);
						if (i == ItemManager.i.HatIndex)
						{
							this.equippedHat = itemObject;
							num2 = list.Count - 1;
						}
						break;
					case ItemManager.ItemType.Secondary:
						list3.Add(itemObject);
						if (i == ItemManager.i.SecondaryIndex)
						{
							this.equippedSecondary = itemObject;
							num3 = list3.Count - 1;
						}
						break;
					case ItemManager.ItemType.Item:
						list4.Add(itemObject);
						if (i == ItemManager.i.ItemIndex)
						{
							this.equippedItem = itemObject;
							if (num4 == 0)
							{
								num4 = list4.Count - 1;
							}
						}
						if (i == ItemManager.i.ItemIndex_R)
						{
							this.equippedItem_r = itemObject;
							if (num4 == 0)
							{
								num4 = list4.Count - 1;
							}
						}
						break;
					}
				}
			}
		}
		if (this.hatBar != null)
		{
			this.hatBar.LoadElements(list.ToArray(), num2);
		}
		if (this.primaryBar != null)
		{
			this.primaryBar.LoadElements(list2.ToArray(), num);
		}
		if (this.secondaryBar != null)
		{
			this.secondaryBar.LoadElements(list3.ToArray(), num3);
		}
		if (this.itemBar != null)
		{
			this.itemBar.LoadElements(list4.ToArray(), num4);
		}
		if (this.hatGrid != null)
		{
			this.hatGrid.LoadElements(list.ToArray(), num2);
		}
		if (this.primaryGrid != null)
		{
			this.primaryGrid.LoadElements(list2.ToArray(), num);
		}
		if (this.secondaryGrid != null)
		{
			this.secondaryGrid.LoadElements(list3.ToArray(), num3);
		}
		if (this.itemGrid != null)
		{
			this.itemGrid.LoadElements(list4.ToArray(), num4);
		}
	}

	// Token: 0x0600132B RID: 4907 RVA: 0x0005DC38 File Offset: 0x0005BE38
	public void SelectItem(ItemObject item)
	{
		this.selectedItem = item;
		this.itemDisplay.LoadItem(item);
		if (this.equipButton != null)
		{
			this.equipButton.SetActive(item != this.placeholderItem && item.IsUnlocked && !ItemManager.i.IsEquipped(item));
		}
		if (this.purchaseButton != null)
		{
			this.purchaseButton.SetActive(item != this.placeholderItem && !item.IsUnlocked);
		}
		if (this.isShop || this.showAll)
		{
			if (this.costText != null)
			{
				this.costText.text = string.Format("${0}", item.shopCost);
			}
			if (this.costIcon != null)
			{
				this.costIcon.sprite = this.resource.sprite;
			}
			if (this.costColored != null)
			{
				this.costColored.color = ((this.resource.Amount >= item.shopCost) ? this.resource.color : Color.grey);
			}
		}
		this.SetButtonPromptState(item.IsUnlocked ? UISwapItemsMenu.ButtonPromptState.Equip : UISwapItemsMenu.ButtonPromptState.Craft);
		this.ShowItem();
	}

	// Token: 0x0600132C RID: 4908 RVA: 0x0001044B File Offset: 0x0000E64B
	public void SubmitItem(ItemObject item)
	{
		if (item == this.placeholderItem)
		{
			return;
		}
		if (!item.IsUnlocked)
		{
			this.selectedItem = item;
			this.PurchaseItem();
			return;
		}
		this.EquipItem();
	}

	// Token: 0x0600132D RID: 4909 RVA: 0x00010478 File Offset: 0x0000E678
	public void HighlightItem(ItemObject item)
	{
		if (item == this.selectedItem)
		{
			this.SelectItem(item);
			return;
		}
		this.SetButtonPromptState(UISwapItemsMenu.ButtonPromptState.Select);
	}

	// Token: 0x0600132E RID: 4910 RVA: 0x0005DD88 File Offset: 0x0005BF88
	public void PurchaseItem()
	{
		if (this.resource.Amount >= this.selectedItem.shopCost)
		{
			this.resource.Amount -= this.selectedItem.shopCost;
			this.selectedItem.IsUnlocked = true;
			this.SelectItem(this.selectedItem);
			this.RefreshSelectedBar();
			this.EquipItem();
		}
	}

	// Token: 0x0600132F RID: 4911 RVA: 0x0005DDF0 File Offset: 0x0005BFF0
	public void EquipItem()
	{
		if (this.selectedItem.itemType == ItemManager.ItemType.Item)
		{
			if (ItemManager.i.IsEquipped(this.selectedItem))
			{
				ItemManager.i.SwapItemEquipSlots();
			}
			else if (this.equippedItem == null)
			{
				ItemManager.i.EquipItem(this.selectedItem, 0);
			}
			else
			{
				if (!(this.equippedItem_r == null))
				{
					this.SetEquipWindow(true);
					return;
				}
				ItemManager.i.EquipItem(this.selectedItem, 1);
			}
			if (this.equipSound != null)
			{
				this.equipSound.Play();
			}
			this.SelectItem(this.selectedItem);
			this.RefreshSelectedBar();
			return;
		}
		if (!ItemManager.i.IsEquipped(this.selectedItem))
		{
			if (this.equipSound != null)
			{
				this.equipSound.Play();
			}
			ItemManager.i.EquipItem(this.selectedItem);
			this.SelectItem(this.selectedItem);
			this.RefreshSelectedBar();
		}
	}

	// Token: 0x06001330 RID: 4912 RVA: 0x00010497 File Offset: 0x0000E697
	private void OnEquipLeft(InputActionEventData obj)
	{
		this.EquipIntoSlot(0);
	}

	// Token: 0x06001331 RID: 4913 RVA: 0x000104A0 File Offset: 0x0000E6A0
	private void OnEquipRight(InputActionEventData obj)
	{
		this.EquipIntoSlot(1);
	}

	// Token: 0x06001332 RID: 4914 RVA: 0x0005DEF0 File Offset: 0x0005C0F0
	public void EquipIntoSlot(int slot)
	{
		if (!this.isEquipWindowActive)
		{
			return;
		}
		UITabNavigation.bufferTime = Time.time;
		ItemManager.i.EquipItem(this.selectedItem, slot);
		if (slot != 0)
		{
			if (slot == 1)
			{
				this.equippedItem_r = this.selectedItem;
			}
		}
		else
		{
			this.equippedItem = this.selectedItem;
		}
		this.SetEquipWindow(false);
		if (this.equipSound != null)
		{
			this.equipSound.Play();
		}
		this.SelectItem(this.selectedItem);
		this.RefreshSelectedBar();
	}

	// Token: 0x06001333 RID: 4915 RVA: 0x0005DF78 File Offset: 0x0005C178
	private void SetEquipWindow(bool isActive)
	{
		if (this.isEquipWindowActive == isActive)
		{
			return;
		}
		this.isEquipWindowActive = isActive;
		if (isActive)
		{
			this.equipLeftDisplay.LoadItem(this.equippedItem);
			this.equipRightDisplay.LoadItem(this.equippedItem_r);
			this.toBeEquippedDisplay.LoadItem(this.selectedItem);
		}
		else
		{
			this.itemGrid.UpdateSelectedIndex(this.selectedItem);
		}
		GameObject[] array = this.hideDuringEquipWindow;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(!isActive);
		}
		array = this.showDuringEquipWindow;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(isActive);
		}
	}

	// Token: 0x06001334 RID: 4916 RVA: 0x000104A9 File Offset: 0x0000E6A9
	public bool TryCancel()
	{
		if (this.isEquipWindowActive)
		{
			this.SetEquipWindow(false);
			return false;
		}
		return true;
	}

	// Token: 0x06001335 RID: 4917 RVA: 0x000104BD File Offset: 0x0000E6BD
	public void ShowItem()
	{
		this.SetShowItemState(this.selectedItem.itemType);
	}

	// Token: 0x06001336 RID: 4918 RVA: 0x0005E020 File Offset: 0x0005C220
	private void SetShowItemState(ItemManager.ItemType itemType)
	{
		if (global::Player.animator == null)
		{
			return;
		}
		if (itemType != ItemManager.ItemType.Primary)
		{
			if (itemType != ItemManager.ItemType.Secondary)
			{
				global::Player.animator.Play(this.showNothingID, this.itemMenuLayer);
				PlayerItemManager itemManager = global::Player.itemManager;
				if (itemManager == null)
				{
					return;
				}
				itemManager.SetEquippedState(PlayerItemManager.EquippedState.None, false);
				return;
			}
			else
			{
				global::Player.animator.Play(this.showShieldID, this.itemMenuLayer);
				PlayerItemManager itemManager2 = global::Player.itemManager;
				if (itemManager2 == null)
				{
					return;
				}
				itemManager2.SetEquippedState(PlayerItemManager.EquippedState.SwordAndShield, false);
				return;
			}
		}
		else
		{
			global::Player.animator.Play(this.showSwordID, this.itemMenuLayer);
			PlayerItemManager itemManager3 = global::Player.itemManager;
			if (itemManager3 == null)
			{
				return;
			}
			itemManager3.SetEquippedState(PlayerItemManager.EquippedState.SwordAndShield, false);
			return;
		}
	}

	// Token: 0x06001337 RID: 4919 RVA: 0x0005E0BC File Offset: 0x0005C2BC
	private void RefreshSelectedBar()
	{
		switch (this.selectedItem.itemType)
		{
		case ItemManager.ItemType.Primary:
			if (this.primaryBar != null)
			{
				this.primaryBar.RefreshElements();
			}
			if (this.primaryGrid != null)
			{
				this.primaryGrid.RefreshElements();
				return;
			}
			break;
		case ItemManager.ItemType.Hat:
			if (this.hatBar != null)
			{
				this.hatBar.RefreshElements();
			}
			if (this.hatGrid != null)
			{
				this.hatGrid.RefreshElements();
				return;
			}
			break;
		case ItemManager.ItemType.Secondary:
			if (this.secondaryBar != null)
			{
				this.secondaryBar.RefreshElements();
			}
			if (this.secondaryGrid != null)
			{
				this.secondaryGrid.RefreshElements();
				return;
			}
			break;
		case ItemManager.ItemType.Item:
			if (this.itemBar != null)
			{
				this.itemBar.RefreshElements();
			}
			if (this.itemGrid != null)
			{
				this.itemGrid.RefreshElements();
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x06001338 RID: 4920 RVA: 0x000104D0 File Offset: 0x0000E6D0
	private void SetButtonPromptState(UISwapItemsMenu.ButtonPromptState state)
	{
		this.buttonPromptSelect.SetActive(state == UISwapItemsMenu.ButtonPromptState.Select);
		this.buttonPromptCraft.SetActive(state == UISwapItemsMenu.ButtonPromptState.Craft);
		this.buttonPromptEquip.SetActive(state == UISwapItemsMenu.ButtonPromptState.Equip);
	}

	// Token: 0x040018A6 RID: 6310
	[Header("Slide Bars (Deprecated)")]
	public SlideBar hatBar;

	// Token: 0x040018A7 RID: 6311
	public SlideBar primaryBar;

	// Token: 0x040018A8 RID: 6312
	public SlideBar secondaryBar;

	// Token: 0x040018A9 RID: 6313
	public SlideBar itemBar;

	// Token: 0x040018AA RID: 6314
	[Header("Item Grids")]
	public ItemGrid hatGrid;

	// Token: 0x040018AB RID: 6315
	public ItemGrid primaryGrid;

	// Token: 0x040018AC RID: 6316
	public ItemGrid secondaryGrid;

	// Token: 0x040018AD RID: 6317
	public ItemGrid itemGrid;

	// Token: 0x040018AE RID: 6318
	private ItemObject equippedHat;

	// Token: 0x040018AF RID: 6319
	private ItemObject equippedPrimary;

	// Token: 0x040018B0 RID: 6320
	private ItemObject equippedSecondary;

	// Token: 0x040018B1 RID: 6321
	private ItemObject equippedItem;

	// Token: 0x040018B2 RID: 6322
	private ItemObject equippedItem_r;

	// Token: 0x040018B3 RID: 6323
	public bool showAll;

	// Token: 0x040018B4 RID: 6324
	public bool useMenuCamera;

	// Token: 0x040018B5 RID: 6325
	public Text displayName;

	// Token: 0x040018B6 RID: 6326
	public UITextBox description;

	// Token: 0x040018B7 RID: 6327
	public UIItemDisplay itemDisplay;

	// Token: 0x040018B8 RID: 6328
	[ReadOnly]
	public ItemObject selectedItem;

	// Token: 0x040018B9 RID: 6329
	public ItemObject placeholderItem;

	// Token: 0x040018BA RID: 6330
	public GameObject equipButton;

	// Token: 0x040018BB RID: 6331
	[Header("Shop")]
	public bool isShop;

	// Token: 0x040018BC RID: 6332
	public ItemResource resource;

	// Token: 0x040018BD RID: 6333
	public GameObject purchaseButton;

	// Token: 0x040018BE RID: 6334
	public Image costIcon;

	// Token: 0x040018BF RID: 6335
	public Text costText;

	// Token: 0x040018C0 RID: 6336
	public Image costColored;

	// Token: 0x040018C1 RID: 6337
	[Header("Button Prompts")]
	public GameObject buttonPrompts;

	// Token: 0x040018C2 RID: 6338
	public GameObject buttonPromptSelect;

	// Token: 0x040018C3 RID: 6339
	public GameObject buttonPromptCraft;

	// Token: 0x040018C4 RID: 6340
	public GameObject buttonPromptEquip;

	// Token: 0x040018C5 RID: 6341
	[Header("Sound Effects")]
	public AudioSourceVariance equipSound;

	// Token: 0x040018C6 RID: 6342
	[Header("Equip Sound Window Stuff")]
	public GameObject[] hideDuringEquipWindow;

	// Token: 0x040018C7 RID: 6343
	public GameObject[] showDuringEquipWindow;

	// Token: 0x040018C8 RID: 6344
	public UIItemDisplay equipLeftDisplay;

	// Token: 0x040018C9 RID: 6345
	public UIItemDisplay equipRightDisplay;

	// Token: 0x040018CA RID: 6346
	public UIItemDisplay toBeEquippedDisplay;

	// Token: 0x040018CB RID: 6347
	[ActionIdProperty(typeof(global::RewiredConsts.Action))]
	public int equipLeftAction;

	// Token: 0x040018CC RID: 6348
	[ActionIdProperty(typeof(global::RewiredConsts.Action))]
	public int equipRightAction;

	// Token: 0x040018CD RID: 6349
	private int itemMenuLayer;

	// Token: 0x040018CE RID: 6350
	private EventSystem eventSystem;

	// Token: 0x040018CF RID: 6351
	private global::Rewired.Player rePlayer;

	// Token: 0x040018D0 RID: 6352
	private bool isEquipWindowActive;

	// Token: 0x040018D1 RID: 6353
	private readonly int showNothingID = Animator.StringToHash("None");

	// Token: 0x040018D2 RID: 6354
	private readonly int showSwordID = Animator.StringToHash("Show Sword");

	// Token: 0x040018D3 RID: 6355
	private readonly int showShieldID = Animator.StringToHash("Show Shield");

	// Token: 0x020003E2 RID: 994
	private enum ButtonPromptState
	{
		// Token: 0x040018D5 RID: 6357
		Select,
		// Token: 0x040018D6 RID: 6358
		Craft,
		// Token: 0x040018D7 RID: 6359
		Equip
	}
}
