using System;
using System.Collections.Generic;
using Rewired;
using RewiredConsts;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000362 RID: 866
public class ItemGrid : MonoBehaviour
{
	// Token: 0x06001098 RID: 4248 RVA: 0x0000E3E5 File Offset: 0x0000C5E5
	private void OnValidate()
	{
		if (this.preventDeselection == null)
		{
			this.preventDeselection = base.GetComponent<UIPreventDeselection>();
		}
		if (this.itemsMenu == null)
		{
			this.itemsMenu = base.GetComponentInParent<UISwapItemsMenu>();
		}
	}

	// Token: 0x06001099 RID: 4249 RVA: 0x00055288 File Offset: 0x00053488
	public void LoadElements(ItemObject[] itemData, int selectedIndex = 0)
	{
		this.items = itemData;
		this.selectedIndex = selectedIndex;
		if (this.elements == null)
		{
			this.elements = new List<SelectableItem>();
		}
		int i = 0;
		foreach (ItemObject itemObject in itemData)
		{
			if (i >= this.elements.Count)
			{
				this.elements.Add(Object.Instantiate<GameObject>(this.elementPrefab, this.elementParent).GetComponent<SelectableItem>());
				this.elements[i].itemsMenu = this.itemsMenu;
			}
			this.elements[i].LoadItem(itemObject);
			i++;
		}
		while (i < this.elements.Count)
		{
			this.elements[i].gameObject.SetActive(false);
			i++;
		}
		this.SelectIndex(selectedIndex);
	}

	// Token: 0x0600109A RID: 4250 RVA: 0x0005535C File Offset: 0x0005355C
	public void LoadElements(ItemObject[] itemData, int[] selectedIndices)
	{
		this.items = itemData;
		this.selectedIndices = selectedIndices;
		if (this.elements == null)
		{
			this.elements = new List<SelectableItem>();
		}
		int i = 0;
		foreach (ItemObject itemObject in itemData)
		{
			if (i >= this.elements.Count)
			{
				this.elements.Add(Object.Instantiate<GameObject>(this.elementPrefab, this.elementParent).GetComponent<SelectableItem>());
				this.elements[i].itemsMenu = this.itemsMenu;
			}
			this.elements[i].LoadItem(itemObject);
			i++;
		}
		while (i < this.elements.Count)
		{
			this.elements[i].gameObject.SetActive(false);
			i++;
		}
		int num = 0;
		foreach (int num2 in selectedIndices)
		{
			if (num2 != -1)
			{
				num = num2;
				break;
			}
		}
		this.SelectIndex(num);
	}

	// Token: 0x0600109B RID: 4251 RVA: 0x00055454 File Offset: 0x00053654
	public void RefreshElements()
	{
		for (int i = 0; i < this.items.Length; i++)
		{
			this.elements[i].LoadItem(this.items[i]);
			if (!this.hasMultipleSelected && this.itemsMenu.selectedItem == this.items[i])
			{
				this.selectedIndex = i;
			}
		}
	}

	// Token: 0x0600109C RID: 4252 RVA: 0x0000E41B File Offset: 0x0000C61B
	private void OnEnable()
	{
		this.SelectIndex(this.selectedIndex);
	}

	// Token: 0x0600109D RID: 4253 RVA: 0x000554B8 File Offset: 0x000536B8
	public void SelectIndex(int index)
	{
		if (this.selectedIndex >= this.items.Length)
		{
			return;
		}
		this.selectedIndex = index;
		if (base.gameObject.activeSelf)
		{
			this.itemsMenu.SelectItem(this.items[this.selectedIndex]);
		}
		this.preventDeselection.defaultSelection = this.elements[this.selectedIndex].gameObject;
		if (base.gameObject.activeSelf)
		{
			EventSystem.current.SetSelectedGameObject(this.elements[this.selectedIndex].gameObject);
		}
	}

	// Token: 0x0600109E RID: 4254 RVA: 0x00055550 File Offset: 0x00053750
	public void UpdateSelectedIndex(ItemObject newSelectedItem)
	{
		for (int i = 0; i < this.items.Length; i++)
		{
			if (this.items[i] == newSelectedItem)
			{
				this.SelectIndex(i);
				return;
			}
		}
	}

	// Token: 0x0400159F RID: 5535
	public GameObject elementPrefab;

	// Token: 0x040015A0 RID: 5536
	public Transform elementParent;

	// Token: 0x040015A1 RID: 5537
	public int selectedIndex;

	// Token: 0x040015A2 RID: 5538
	public ItemObject[] items;

	// Token: 0x040015A3 RID: 5539
	public List<SelectableItem> elements;

	// Token: 0x040015A4 RID: 5540
	public UISwapItemsMenu itemsMenu;

	// Token: 0x040015A5 RID: 5541
	public UIPreventDeselection preventDeselection;

	// Token: 0x040015A6 RID: 5542
	[Header("Multiple")]
	public bool hasMultipleSelected;

	// Token: 0x040015A7 RID: 5543
	public int[] selectedIndices;

	// Token: 0x040015A8 RID: 5544
	[ActionIdProperty(typeof(global::RewiredConsts.Action))]
	public int[] prompts;
}
