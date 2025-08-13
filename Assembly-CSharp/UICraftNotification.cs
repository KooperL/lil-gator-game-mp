using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020002C0 RID: 704
public class UICraftNotification : MonoBehaviour
{
	// Token: 0x06000ECB RID: 3787 RVA: 0x00046B30 File Offset: 0x00044D30
	public void LoadItems(ItemObject[] items)
	{
		this.ResetThing();
		this.items = items;
		this.itemDisplay.LoadItem(items[0]);
		this.itemIndex = 0;
		this.nextItemTime = Time.time + this.itemSlideshowDelay;
		this.onLoad.Invoke();
	}

	// Token: 0x06000ECC RID: 3788 RVA: 0x00046B7C File Offset: 0x00044D7C
	public void LoadItem(ItemObject item)
	{
		this.ResetThing();
		this.items = null;
		this.itemDisplay.LoadItem(item);
		this.onLoad.Invoke();
	}

	// Token: 0x06000ECD RID: 3789 RVA: 0x00046BA2 File Offset: 0x00044DA2
	private void ResetThing()
	{
		this.hideBehavior.Show();
		this.hideTime = Time.time + this.hideDelay;
	}

	// Token: 0x06000ECE RID: 3790 RVA: 0x00046BC4 File Offset: 0x00044DC4
	private void Update()
	{
		if (this.items != null && this.items.Length > 1 && Time.time > this.nextItemTime)
		{
			this.itemIndex++;
			if (this.itemIndex >= this.items.Length)
			{
				this.itemIndex = 0;
			}
			this.itemDisplay.LoadItem(this.items[this.itemIndex]);
			this.nextItemTime = Time.time + this.itemSlideshowDelay;
		}
		if (Time.time > this.hideTime && !this.hideBehavior.isHiding)
		{
			this.hideBehavior.Hide();
		}
	}

	// Token: 0x04001345 RID: 4933
	public UIItemDisplay itemDisplay;

	// Token: 0x04001346 RID: 4934
	public UIHideBehavior hideBehavior;

	// Token: 0x04001347 RID: 4935
	public float hideDelay = 5f;

	// Token: 0x04001348 RID: 4936
	private float hideTime;

	// Token: 0x04001349 RID: 4937
	private ItemObject[] items;

	// Token: 0x0400134A RID: 4938
	private int itemIndex;

	// Token: 0x0400134B RID: 4939
	private float nextItemTime = -1f;

	// Token: 0x0400134C RID: 4940
	public float itemSlideshowDelay = 0.75f;

	// Token: 0x0400134D RID: 4941
	public UnityEvent onLoad = new UnityEvent();
}
