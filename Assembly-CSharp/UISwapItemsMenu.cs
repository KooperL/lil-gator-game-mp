using System;
using System.Collections.Generic;
using Rewired;
using RewiredConsts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISwapItemsMenu : MonoBehaviour, ICheckCancel
{
	// Token: 0x06001385 RID: 4997 RVA: 0x0005F670 File Offset: 0x0005D870
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
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnEquipLeft), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, this.equipLeftAction);
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnEquipRight), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, this.equipRightAction);
		this.UpdateInventories();
	}

	// Token: 0x06001386 RID: 4998 RVA: 0x0005F730 File Offset: 0x0005D930
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

	// Token: 0x06001387 RID: 4999 RVA: 0x00010810 File Offset: 0x0000EA10
	private void Update()
	{
		this.ForceCorrectSelection();
	}

	// Token: 0x06001388 RID: 5000 RVA: 0x00010818 File Offset: 0x0000EA18
	private void ForceCorrectSelection()
	{
		this.eventSystem.currentSelectedGameObject == null;
	}

	// Token: 0x06001389 RID: 5001 RVA: 0x0001082C File Offset: 0x0000EA2C
	public void OnCancel(BaseEventData eventData)
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600138A RID: 5002 RVA: 0x0005F7BC File Offset: 0x0005D9BC
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

	// Token: 0x0600138B RID: 5003 RVA: 0x0005FACC File Offset: 0x0005DCCC
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

	// Token: 0x0600138C RID: 5004 RVA: 0x00010833 File Offset: 0x0000EA33
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

	// Token: 0x0600138D RID: 5005 RVA: 0x00010860 File Offset: 0x0000EA60
	public void HighlightItem(ItemObject item)
	{
		if (item == this.selectedItem)
		{
			this.SelectItem(item);
			return;
		}
		this.SetButtonPromptState(UISwapItemsMenu.ButtonPromptState.Select);
	}

	// Token: 0x0600138E RID: 5006 RVA: 0x0005FC1C File Offset: 0x0005DE1C
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

	// Token: 0x0600138F RID: 5007 RVA: 0x0005FC84 File Offset: 0x0005DE84
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

	// Token: 0x06001390 RID: 5008 RVA: 0x0001087F File Offset: 0x0000EA7F
	private void OnEquipLeft(InputActionEventData obj)
	{
		this.EquipIntoSlot(0);
	}

	// Token: 0x06001391 RID: 5009 RVA: 0x00010888 File Offset: 0x0000EA88
	private void OnEquipRight(InputActionEventData obj)
	{
		this.EquipIntoSlot(1);
	}

	// Token: 0x06001392 RID: 5010 RVA: 0x0005FD84 File Offset: 0x0005DF84
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

	// Token: 0x06001393 RID: 5011 RVA: 0x0005FE0C File Offset: 0x0005E00C
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

	// Token: 0x06001394 RID: 5012 RVA: 0x00010891 File Offset: 0x0000EA91
	public bool TryCancel()
	{
		if (this.isEquipWindowActive)
		{
			this.SetEquipWindow(false);
			return false;
		}
		return true;
	}

	// Token: 0x06001395 RID: 5013 RVA: 0x000108A5 File Offset: 0x0000EAA5
	public void ShowItem()
	{
		this.SetShowItemState(this.selectedItem.itemType);
	}

	// Token: 0x06001396 RID: 5014 RVA: 0x0005FEB4 File Offset: 0x0005E0B4
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

	// Token: 0x06001397 RID: 5015 RVA: 0x0005FF50 File Offset: 0x0005E150
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

	// Token: 0x06001398 RID: 5016 RVA: 0x000108B8 File Offset: 0x0000EAB8
	private void SetButtonPromptState(UISwapItemsMenu.ButtonPromptState state)
	{
		this.buttonPromptSelect.SetActive(state == UISwapItemsMenu.ButtonPromptState.Select);
		this.buttonPromptCraft.SetActive(state == UISwapItemsMenu.ButtonPromptState.Craft);
		this.buttonPromptEquip.SetActive(state == UISwapItemsMenu.ButtonPromptState.Equip);
	}

	[Header("Slide Bars (Deprecated)")]
	public SlideBar hatBar;

	public SlideBar primaryBar;

	public SlideBar secondaryBar;

	public SlideBar itemBar;

	[Header("Item Grids")]
	public ItemGrid hatGrid;

	public ItemGrid primaryGrid;

	public ItemGrid secondaryGrid;

	public ItemGrid itemGrid;

	private ItemObject equippedHat;

	private ItemObject equippedPrimary;

	private ItemObject equippedSecondary;

	private ItemObject equippedItem;

	private ItemObject equippedItem_r;

	public bool showAll;

	public bool useMenuCamera;

	public Text displayName;

	public UITextBox description;

	public UIItemDisplay itemDisplay;

	[ReadOnly]
	public ItemObject selectedItem;

	public ItemObject placeholderItem;

	public GameObject equipButton;

	[Header("Shop")]
	public bool isShop;

	public ItemResource resource;

	public GameObject purchaseButton;

	public Image costIcon;

	public Text costText;

	public Image costColored;

	[Header("Button Prompts")]
	public GameObject buttonPrompts;

	public GameObject buttonPromptSelect;

	public GameObject buttonPromptCraft;

	public GameObject buttonPromptEquip;

	[Header("Sound Effects")]
	public AudioSourceVariance equipSound;

	[Header("Equip Sound Window Stuff")]
	public GameObject[] hideDuringEquipWindow;

	public GameObject[] showDuringEquipWindow;

	public UIItemDisplay equipLeftDisplay;

	public UIItemDisplay equipRightDisplay;

	public UIItemDisplay toBeEquippedDisplay;

	[ActionIdProperty(typeof(global::RewiredConsts.Action))]
	public int equipLeftAction;

	[ActionIdProperty(typeof(global::RewiredConsts.Action))]
	public int equipRightAction;

	private int itemMenuLayer;

	private EventSystem eventSystem;

	private global::Rewired.Player rePlayer;

	private bool isEquipWindowActive;

	private readonly int showNothingID = Animator.StringToHash("None");

	private readonly int showSwordID = Animator.StringToHash("Show Sword");

	private readonly int showShieldID = Animator.StringToHash("Show Shield");

	private enum ButtonPromptState
	{
		Select,
		Craft,
		Equip
	}
}
