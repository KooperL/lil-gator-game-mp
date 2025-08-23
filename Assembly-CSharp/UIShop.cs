using System;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
	// Token: 0x06001325 RID: 4901 RVA: 0x0005E644 File Offset: 0x0005C844
	public void Load(Shop.ShopItem loadedItem, Shop sourceShop)
	{
		this.shopItem = loadedItem;
		this.shop = sourceShop;
		this.actor = sourceShop.actor;
		base.gameObject.SetActive(true);
		this.SetDescription(this.shopItem.item.Description);
		this.nameText.text = this.shopItem.item.DisplayName;
		this.itemResource = sourceShop.itemResource;
		this.itemResource.ForceShow = true;
		this.costText.text = loadedItem.cost.ToString("0");
		if (this.costIcon != null)
		{
			this.costIcon.sprite = this.itemResource.sprite;
		}
		if (this.confirmButton != null)
		{
			this.confirmButton.interactable = sourceShop.itemResource.Amount >= loadedItem.cost;
		}
	}

	// Token: 0x06001326 RID: 4902 RVA: 0x0001021A File Offset: 0x0000E41A
	public void Deactivate()
	{
		this.itemResource.ForceShow = false;
		base.gameObject.SetActive(false);
		this.description.Clear(false);
	}

	// Token: 0x06001327 RID: 4903 RVA: 0x00010240 File Offset: 0x0000E440
	public void SetDescription(string descriptionText)
	{
		this.description.Clear(false);
		if (!string.IsNullOrEmpty(descriptionText))
		{
			this.description.Load(descriptionText, this.actor, false, 0f);
		}
	}

	// Token: 0x06001328 RID: 4904 RVA: 0x0001026F File Offset: 0x0000E46F
	public void Buy()
	{
		this.shop.Buy();
	}

	// Token: 0x06001329 RID: 4905 RVA: 0x0001027C File Offset: 0x0000E47C
	public void Exit()
	{
		this.shop.Deactivate();
	}

	// Token: 0x0600132A RID: 4906 RVA: 0x00010289 File Offset: 0x0000E489
	public void MoveRight()
	{
		this.shop.MoveSelection(true);
	}

	// Token: 0x0600132B RID: 4907 RVA: 0x00010297 File Offset: 0x0000E497
	public void MoveLeft()
	{
		this.shop.MoveSelection(false);
	}

	private Shop.ShopItem shopItem;

	private Shop shop;

	public DialogueBox description;

	public ItemResource itemResource;

	public Text costText;

	public DialogueActor actor;

	public Image costIcon;

	public Text nameText;

	public Button confirmButton;
}
