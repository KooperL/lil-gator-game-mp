using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableItem : MonoBehaviour, ISelectHandler, IEventSystemHandler, ISubmitHandler, IPointerDownHandler, IPointerEnterHandler
{
	// Token: 0x060010FB RID: 4347 RVA: 0x0000E792 File Offset: 0x0000C992
	private void OnValidate()
	{
		if (this.uiItemDisplay == null)
		{
			this.uiItemDisplay = base.GetComponent<UIItemDisplay>();
		}
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x0000E7AE File Offset: 0x0000C9AE
	public void LoadItem(ItemObject item)
	{
		this.loadedItem = item;
		this.uiItemDisplay.LoadItem(item);
	}

	// Token: 0x060010FD RID: 4349 RVA: 0x0000E7C3 File Offset: 0x0000C9C3
	public void OnSelect(BaseEventData eventData)
	{
		if (this.itemsMenu.selectedItem != this.loadedItem)
		{
			this.selectedTime = Time.time;
		}
		this.itemsMenu.SelectItem(this.loadedItem);
	}

	// Token: 0x060010FE RID: 4350 RVA: 0x0000E7F9 File Offset: 0x0000C9F9
	public void OnSubmit(BaseEventData eventData)
	{
		this.itemsMenu.SubmitItem(this.loadedItem);
	}

	// Token: 0x060010FF RID: 4351 RVA: 0x0000E80C File Offset: 0x0000CA0C
	public void OnPointerDown(PointerEventData eventData)
	{
		if (this.selectedTime != Time.time)
		{
			this.itemsMenu.SubmitItem(this.loadedItem);
		}
	}

	// Token: 0x06001100 RID: 4352 RVA: 0x0000E82C File Offset: 0x0000CA2C
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.itemsMenu.HighlightItem(this.loadedItem);
	}

	public ItemObject loadedItem;

	public UIItemDisplay uiItemDisplay;

	public UISwapItemsMenu itemsMenu;

	private float selectedTime = -1f;
}
