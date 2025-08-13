using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003D0 RID: 976
public class UIShop : MonoBehaviour
{
	// Token: 0x060012C4 RID: 4804 RVA: 0x0005C354 File Offset: 0x0005A554
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

	// Token: 0x060012C5 RID: 4805 RVA: 0x0000FE13 File Offset: 0x0000E013
	public void Deactivate()
	{
		this.itemResource.ForceShow = false;
		base.gameObject.SetActive(false);
		this.description.Clear(false);
	}

	// Token: 0x060012C6 RID: 4806 RVA: 0x0000FE39 File Offset: 0x0000E039
	public void SetDescription(string descriptionText)
	{
		this.description.Clear(false);
		if (!string.IsNullOrEmpty(descriptionText))
		{
			this.description.Load(descriptionText, this.actor, false, 0f);
		}
	}

	// Token: 0x060012C7 RID: 4807 RVA: 0x0000FE68 File Offset: 0x0000E068
	public void Buy()
	{
		this.shop.Buy();
	}

	// Token: 0x060012C8 RID: 4808 RVA: 0x0000FE75 File Offset: 0x0000E075
	public void Exit()
	{
		this.shop.Deactivate();
	}

	// Token: 0x060012C9 RID: 4809 RVA: 0x0000FE82 File Offset: 0x0000E082
	public void MoveRight()
	{
		this.shop.MoveSelection(true);
	}

	// Token: 0x060012CA RID: 4810 RVA: 0x0000FE90 File Offset: 0x0000E090
	public void MoveLeft()
	{
		this.shop.MoveSelection(false);
	}

	// Token: 0x0400182B RID: 6187
	private Shop.ShopItem shopItem;

	// Token: 0x0400182C RID: 6188
	private Shop shop;

	// Token: 0x0400182D RID: 6189
	public DialogueBox description;

	// Token: 0x0400182E RID: 6190
	public ItemResource itemResource;

	// Token: 0x0400182F RID: 6191
	public Text costText;

	// Token: 0x04001830 RID: 6192
	public DialogueActor actor;

	// Token: 0x04001831 RID: 6193
	public Image costIcon;

	// Token: 0x04001832 RID: 6194
	public Text nameText;

	// Token: 0x04001833 RID: 6195
	public Button confirmButton;
}
