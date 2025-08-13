using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000363 RID: 867
public class SelectableItem : MonoBehaviour, ISelectHandler, IEventSystemHandler, ISubmitHandler, IPointerDownHandler, IPointerEnterHandler
{
	// Token: 0x060010A0 RID: 4256 RVA: 0x0000E429 File Offset: 0x0000C629
	private void OnValidate()
	{
		if (this.uiItemDisplay == null)
		{
			this.uiItemDisplay = base.GetComponent<UIItemDisplay>();
		}
	}

	// Token: 0x060010A1 RID: 4257 RVA: 0x0000E445 File Offset: 0x0000C645
	public void LoadItem(ItemObject item)
	{
		this.loadedItem = item;
		this.uiItemDisplay.LoadItem(item);
	}

	// Token: 0x060010A2 RID: 4258 RVA: 0x0000E45A File Offset: 0x0000C65A
	public void OnSelect(BaseEventData eventData)
	{
		if (this.itemsMenu.selectedItem != this.loadedItem)
		{
			this.selectedTime = Time.time;
		}
		this.itemsMenu.SelectItem(this.loadedItem);
	}

	// Token: 0x060010A3 RID: 4259 RVA: 0x0000E490 File Offset: 0x0000C690
	public void OnSubmit(BaseEventData eventData)
	{
		this.itemsMenu.SubmitItem(this.loadedItem);
	}

	// Token: 0x060010A4 RID: 4260 RVA: 0x0000E4A3 File Offset: 0x0000C6A3
	public void OnPointerDown(PointerEventData eventData)
	{
		if (this.selectedTime != Time.time)
		{
			this.itemsMenu.SubmitItem(this.loadedItem);
		}
	}

	// Token: 0x060010A5 RID: 4261 RVA: 0x0000E4C3 File Offset: 0x0000C6C3
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.itemsMenu.HighlightItem(this.loadedItem);
	}

	// Token: 0x040015A9 RID: 5545
	public ItemObject loadedItem;

	// Token: 0x040015AA RID: 5546
	public UIItemDisplay uiItemDisplay;

	// Token: 0x040015AB RID: 5547
	public UISwapItemsMenu itemsMenu;

	// Token: 0x040015AC RID: 5548
	private float selectedTime = -1f;
}
