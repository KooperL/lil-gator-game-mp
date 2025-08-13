using System;
using System.Collections.Generic;
using Rewired;
using RewiredConsts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000289 RID: 649
public class ItemGrid : MonoBehaviour
{
	// Token: 0x06000DD5 RID: 3541 RVA: 0x000430A9 File Offset: 0x000412A9
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

	// Token: 0x06000DD6 RID: 3542 RVA: 0x000430E0 File Offset: 0x000412E0
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

	// Token: 0x06000DD7 RID: 3543 RVA: 0x00043204 File Offset: 0x00041404
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

	// Token: 0x06000DD8 RID: 3544 RVA: 0x000432FC File Offset: 0x000414FC
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

	// Token: 0x06000DD9 RID: 3545 RVA: 0x0004335E File Offset: 0x0004155E
	private void OnEnable()
	{
		this.SelectIndex(this.selectedIndex);
	}

	// Token: 0x06000DDA RID: 3546 RVA: 0x0004336C File Offset: 0x0004156C
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

	// Token: 0x06000DDB RID: 3547 RVA: 0x00043404 File Offset: 0x00041604
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

	// Token: 0x0400123F RID: 4671
	public GameObject elementPrefab;

	// Token: 0x04001240 RID: 4672
	public Transform elementParent;

	// Token: 0x04001241 RID: 4673
	public int selectedIndex;

	// Token: 0x04001242 RID: 4674
	public ItemObject[] items;

	// Token: 0x04001243 RID: 4675
	public List<SelectableItem> elements;

	// Token: 0x04001244 RID: 4676
	public UISwapItemsMenu itemsMenu;

	// Token: 0x04001245 RID: 4677
	public UIPreventDeselection preventDeselection;

	// Token: 0x04001246 RID: 4678
	[Header("Multiple")]
	public bool hasMultipleSelected;

	// Token: 0x04001247 RID: 4679
	public int[] selectedIndices;

	// Token: 0x04001248 RID: 4680
	[ActionIdProperty(typeof(Action))]
	public int[] prompts;

	// Token: 0x04001249 RID: 4681
	[Header("Scrolling")]
	public ScrollRect scrollRect;

	// Token: 0x0400124A RID: 4682
	public UIScrollToSelected scrollToSelected;

	// Token: 0x0400124B RID: 4683
	public UIScrollSelected scrollSelected;
}
