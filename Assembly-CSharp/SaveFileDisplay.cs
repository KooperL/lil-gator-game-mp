using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200005B RID: 91
public class SaveFileDisplay : MonoBehaviour
{
	// Token: 0x06000151 RID: 337 RVA: 0x00007D64 File Offset: 0x00005F64
	public void SetButton(bool isInteractable, Color nonInteractiveColor, bool shouldShow = true)
	{
		this.button.interactable = isInteractable;
		ColorBlock colors = this.button.colors;
		colors.disabledColor = nonInteractiveColor;
		this.button.colors = colors;
		this.rootObject.SetActive(shouldShow);
	}

	// Token: 0x06000152 RID: 338 RVA: 0x00007DA9 File Offset: 0x00005FA9
	public void SetIconState(bool isCopying, bool isErasing)
	{
		if (isCopying)
		{
			this.selectIcon.sprite = this.copyIcon;
			return;
		}
		if (isErasing)
		{
			this.selectIcon.sprite = this.eraseIcon;
			return;
		}
		this.selectIcon.sprite = this.loadIcon;
	}

	// Token: 0x06000153 RID: 339 RVA: 0x00007DE8 File Offset: 0x00005FE8
	public void Load(GameSaveDataInfo info, int fileIndex)
	{
		this.fileIndex.text = (fileIndex + 1).ToString("0");
		this.background.sprite = this.backgroundFileSprites[fileIndex];
		this.newGameRoot.SetActive(!info.isStarted);
		this.existingSaveRoot.SetActive(info.isStarted);
		this.newGamePlus.gameObject.SetActive(info.newGameIndex != 0);
		if (info.isStarted)
		{
			this.playerName.text = info.playerName;
			this.craftingMaterialsRoot.SetActive(info.collectedCraftingMaterials || info.craftingMaterials > 0);
			if (info.sis)
			{
				this.craftingMaterials.text = info.percentObjects.ToString() + "%";
			}
			else
			{
				this.craftingMaterials.text = info.craftingMaterials.ToString("0");
			}
			this.populationRoot.SetActive(info.population > 0);
			if (info.sis)
			{
				this.population.text = info.percentCharacters.ToString() + "%";
			}
			else
			{
				this.population.text = info.population.ToString("0");
			}
			this.SetItemImage(this.sword, info.sword);
			this.SetItemImage(this.shield, info.shield);
			this.SetItemImage(this.hat, info.hat);
			this.SetItemImage(this.item, info.item);
			this.SetItemImage(this.itemR, info.itemR);
			this.sis.gameObject.SetActive(info.sis);
			this.martin.gameObject.SetActive(info.martin);
			this.jill.gameObject.SetActive(info.jill);
			this.avery.gameObject.SetActive(info.avery);
			this.tom.gameObject.SetActive(info.tom);
			for (int i = 0; i < 4; i++)
			{
				this.bracelets[i].SetActive(info.bracelets > i);
			}
		}
	}

	// Token: 0x06000154 RID: 340 RVA: 0x00008028 File Offset: 0x00006228
	public void SetItemImage(Image itemImage, int index)
	{
		if (index == -1)
		{
			itemImage.gameObject.SetActive(false);
			return;
		}
		itemImage.gameObject.SetActive(true);
		itemImage.sprite = ItemManager.i.items[index].sprite;
	}

	// Token: 0x040001D8 RID: 472
	public GameObject rootObject;

	// Token: 0x040001D9 RID: 473
	public Button button;

	// Token: 0x040001DA RID: 474
	public Text fileIndex;

	// Token: 0x040001DB RID: 475
	public Image background;

	// Token: 0x040001DC RID: 476
	public Sprite[] backgroundFileSprites;

	// Token: 0x040001DD RID: 477
	public GameObject newGameRoot;

	// Token: 0x040001DE RID: 478
	public GameObject existingSaveRoot;

	// Token: 0x040001DF RID: 479
	public Text playerName;

	// Token: 0x040001E0 RID: 480
	public GameObject craftingMaterialsRoot;

	// Token: 0x040001E1 RID: 481
	public Text craftingMaterials;

	// Token: 0x040001E2 RID: 482
	public GameObject populationRoot;

	// Token: 0x040001E3 RID: 483
	public Text population;

	// Token: 0x040001E4 RID: 484
	public GameObject[] bracelets;

	// Token: 0x040001E5 RID: 485
	[Space]
	public Image sword;

	// Token: 0x040001E6 RID: 486
	public Image shield;

	// Token: 0x040001E7 RID: 487
	public Image hat;

	// Token: 0x040001E8 RID: 488
	public Image item;

	// Token: 0x040001E9 RID: 489
	public Image itemR;

	// Token: 0x040001EA RID: 490
	[Space]
	public Image tom;

	// Token: 0x040001EB RID: 491
	public Image jill;

	// Token: 0x040001EC RID: 492
	public Image avery;

	// Token: 0x040001ED RID: 493
	public Image martin;

	// Token: 0x040001EE RID: 494
	public Image sis;

	// Token: 0x040001EF RID: 495
	[Space]
	public Image selectIcon;

	// Token: 0x040001F0 RID: 496
	public Sprite loadIcon;

	// Token: 0x040001F1 RID: 497
	public Sprite copyIcon;

	// Token: 0x040001F2 RID: 498
	public Sprite eraseIcon;

	// Token: 0x040001F3 RID: 499
	[Space]
	public Image newGamePlus;
}
