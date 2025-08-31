using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableItem : MonoBehaviour, ISelectHandler, IEventSystemHandler, ISubmitHandler, IPointerDownHandler, IPointerEnterHandler
{
	// Token: 0x06000DDD RID: 3549 RVA: 0x00043444 File Offset: 0x00041644
	private void OnValidate()
	{
		if (this.uiItemDisplay == null)
		{
			this.uiItemDisplay = base.GetComponent<UIItemDisplay>();
		}
	}

	// Token: 0x06000DDE RID: 3550 RVA: 0x00043460 File Offset: 0x00041660
	public void LoadItem(ItemObject item)
	{
		this.loadedItem = item;
		this.uiItemDisplay.LoadItem(item);
	}

	// Token: 0x06000DDF RID: 3551 RVA: 0x00043475 File Offset: 0x00041675
	public void OnSelect(BaseEventData eventData)
	{
		if (this.itemsMenu.selectedItem != this.loadedItem)
		{
			this.selectedTime = Time.time;
		}
		this.itemsMenu.SelectItem(this.loadedItem);
	}

	// Token: 0x06000DE0 RID: 3552 RVA: 0x000434AB File Offset: 0x000416AB
	public void OnSubmit(BaseEventData eventData)
	{
		this.itemsMenu.SubmitItem(this.loadedItem);
	}

	// Token: 0x06000DE1 RID: 3553 RVA: 0x000434BE File Offset: 0x000416BE
	public void OnPointerDown(PointerEventData eventData)
	{
		if (this.selectedTime != Time.time)
		{
			this.itemsMenu.SubmitItem(this.loadedItem);
		}
	}

	// Token: 0x06000DE2 RID: 3554 RVA: 0x000434DE File Offset: 0x000416DE
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.itemsMenu.HighlightItem(this.loadedItem);
	}

	public ItemObject loadedItem;

	public UIItemDisplay uiItemDisplay;

	public UISwapItemsMenu itemsMenu;

	private float selectedTime = -1f;
}
