using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000077 RID: 119
public class SaveFileDisplay : MonoBehaviour
{
	// Token: 0x06000182 RID: 386 RVA: 0x0001C3E0 File Offset: 0x0001A5E0
	public void SetButton(bool isInteractable, Color nonInteractiveColor, bool shouldShow = true)
	{
		this.button.interactable = isInteractable;
		ColorBlock colors = this.button.colors;
		colors.disabledColor = nonInteractiveColor;
		this.button.colors = colors;
		this.rootObject.SetActive(shouldShow);
	}

	// Token: 0x06000183 RID: 387 RVA: 0x00003488 File Offset: 0x00001688
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

	// Token: 0x06000184 RID: 388 RVA: 0x0001C428 File Offset: 0x0001A628
	public void Load(GameSaveDataInfo info, int fileIndex)
	{
		this.fileIndex.text = (fileIndex + 1).ToString("0");
		this.background.sprite = this.backgroundFileSprites[fileIndex];
		this.newGameRoot.SetActive(!info.isStarted);
		this.existingSaveRoot.SetActive(info.isStarted);
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

	// Token: 0x06000185 RID: 389 RVA: 0x000034C5 File Offset: 0x000016C5
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

	// Token: 0x04000246 RID: 582
	public GameObject rootObject;

	// Token: 0x04000247 RID: 583
	public Button button;

	// Token: 0x04000248 RID: 584
	public Text fileIndex;

	// Token: 0x04000249 RID: 585
	public Image background;

	// Token: 0x0400024A RID: 586
	public Sprite[] backgroundFileSprites;

	// Token: 0x0400024B RID: 587
	public GameObject newGameRoot;

	// Token: 0x0400024C RID: 588
	public GameObject existingSaveRoot;

	// Token: 0x0400024D RID: 589
	public Text playerName;

	// Token: 0x0400024E RID: 590
	public GameObject craftingMaterialsRoot;

	// Token: 0x0400024F RID: 591
	public Text craftingMaterials;

	// Token: 0x04000250 RID: 592
	public GameObject populationRoot;

	// Token: 0x04000251 RID: 593
	public Text population;

	// Token: 0x04000252 RID: 594
	public GameObject[] bracelets;

	// Token: 0x04000253 RID: 595
	[Space]
	public Image sword;

	// Token: 0x04000254 RID: 596
	public Image shield;

	// Token: 0x04000255 RID: 597
	public Image hat;

	// Token: 0x04000256 RID: 598
	public Image item;

	// Token: 0x04000257 RID: 599
	public Image itemR;

	// Token: 0x04000258 RID: 600
	[Space]
	public Image tom;

	// Token: 0x04000259 RID: 601
	public Image jill;

	// Token: 0x0400025A RID: 602
	public Image avery;

	// Token: 0x0400025B RID: 603
	public Image martin;

	// Token: 0x0400025C RID: 604
	public Image sis;

	// Token: 0x0400025D RID: 605
	[Space]
	public Image selectIcon;

	// Token: 0x0400025E RID: 606
	public Sprite loadIcon;

	// Token: 0x0400025F RID: 607
	public Sprite copyIcon;

	// Token: 0x04000260 RID: 608
	public Sprite eraseIcon;
}
