using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002E2 RID: 738
public class UIShop : MonoBehaviour
{
	// Token: 0x06000FA4 RID: 4004 RVA: 0x0004AF98 File Offset: 0x00049198
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

	// Token: 0x06000FA5 RID: 4005 RVA: 0x0004B083 File Offset: 0x00049283
	public void Deactivate()
	{
		this.itemResource.ForceShow = false;
		base.gameObject.SetActive(false);
		this.description.Clear(false);
	}

	// Token: 0x06000FA6 RID: 4006 RVA: 0x0004B0A9 File Offset: 0x000492A9
	public void SetDescription(string descriptionText)
	{
		this.description.Clear(false);
		if (!string.IsNullOrEmpty(descriptionText))
		{
			this.description.Load(descriptionText, this.actor, false, 0f);
		}
	}

	// Token: 0x06000FA7 RID: 4007 RVA: 0x0004B0D8 File Offset: 0x000492D8
	public void Buy()
	{
		this.shop.Buy();
	}

	// Token: 0x06000FA8 RID: 4008 RVA: 0x0004B0E5 File Offset: 0x000492E5
	public void Exit()
	{
		this.shop.Deactivate();
	}

	// Token: 0x06000FA9 RID: 4009 RVA: 0x0004B0F2 File Offset: 0x000492F2
	public void MoveRight()
	{
		this.shop.MoveSelection(true);
	}

	// Token: 0x06000FAA RID: 4010 RVA: 0x0004B100 File Offset: 0x00049300
	public void MoveLeft()
	{
		this.shop.MoveSelection(false);
	}

	// Token: 0x04001479 RID: 5241
	private Shop.ShopItem shopItem;

	// Token: 0x0400147A RID: 5242
	private Shop shop;

	// Token: 0x0400147B RID: 5243
	public DialogueBox description;

	// Token: 0x0400147C RID: 5244
	public ItemResource itemResource;

	// Token: 0x0400147D RID: 5245
	public Text costText;

	// Token: 0x0400147E RID: 5246
	public DialogueActor actor;

	// Token: 0x0400147F RID: 5247
	public Image costIcon;

	// Token: 0x04001480 RID: 5248
	public Text nameText;

	// Token: 0x04001481 RID: 5249
	public Button confirmButton;
}
