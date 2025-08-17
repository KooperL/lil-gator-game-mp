using System;
using System.Collections.Generic;
using Rewired;
using RewiredConsts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemGrid : MonoBehaviour
{
	// Token: 0x060010F3 RID: 4339 RVA: 0x0000E74E File Offset: 0x0000C94E
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

	// Token: 0x060010F4 RID: 4340 RVA: 0x000571AC File Offset: 0x000553AC
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
				this.elements.Add(global::UnityEngine.Object.Instantiate<GameObject>(this.elementPrefab, this.elementParent).GetComponent<SelectableItem>());
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
		if (this.scrollRect != null)
		{
			bool flag = itemData.Length > 12;
			this.scrollRect.enabled = flag;
			this.scrollToSelected.enabled = flag;
			this.scrollSelected.enabled = flag;
			if (!flag)
			{
				base.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			}
		}
	}

	// Token: 0x060010F5 RID: 4341 RVA: 0x000572D0 File Offset: 0x000554D0
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
				this.elements.Add(global::UnityEngine.Object.Instantiate<GameObject>(this.elementPrefab, this.elementParent).GetComponent<SelectableItem>());
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

	// Token: 0x060010F6 RID: 4342 RVA: 0x000573C8 File Offset: 0x000555C8
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

	// Token: 0x060010F7 RID: 4343 RVA: 0x0000E784 File Offset: 0x0000C984
	private void OnEnable()
	{
		this.SelectIndex(this.selectedIndex);
	}

	// Token: 0x060010F8 RID: 4344 RVA: 0x0005742C File Offset: 0x0005562C
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

	// Token: 0x060010F9 RID: 4345 RVA: 0x000574C4 File Offset: 0x000556C4
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

	public GameObject elementPrefab;

	public Transform elementParent;

	public int selectedIndex;

	public ItemObject[] items;

	public List<SelectableItem> elements;

	public UISwapItemsMenu itemsMenu;

	public UIPreventDeselection preventDeselection;

	[Header("Multiple")]
	public bool hasMultipleSelected;

	public int[] selectedIndices;

	[ActionIdProperty(typeof(global::RewiredConsts.Action))]
	public int[] prompts;

	[Header("Scrolling")]
	public ScrollRect scrollRect;

	public UIScrollToSelected scrollToSelected;

	public UIScrollSelected scrollSelected;
}
