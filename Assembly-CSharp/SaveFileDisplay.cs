using System;
using UnityEngine;
using UnityEngine.UI;

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

	public GameObject rootObject;

	public Button button;

	public Text fileIndex;

	public Image background;

	public Sprite[] backgroundFileSprites;

	public GameObject newGameRoot;

	public GameObject existingSaveRoot;

	public Text playerName;

	public GameObject craftingMaterialsRoot;

	public Text craftingMaterials;

	public GameObject populationRoot;

	public Text population;

	public GameObject[] bracelets;

	[Space]
	public Image sword;

	public Image shield;

	public Image hat;

	public Image item;

	public Image itemR;

	[Space]
	public Image tom;

	public Image jill;

	public Image avery;

	public Image martin;

	public Image sis;

	[Space]
	public Image selectIcon;

	public Sprite loadIcon;

	public Sprite copyIcon;

	public Sprite eraseIcon;

	[Space]
	public Image newGamePlus;
}
