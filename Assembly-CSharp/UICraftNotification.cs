using System;
using UnityEngine;
using UnityEngine.Events;

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

	public UIItemDisplay itemDisplay;

	public UIHideBehavior hideBehavior;

	public float hideDelay = 5f;

	private float hideTime;

	private ItemObject[] items;

	private int itemIndex;

	private float nextItemTime = -1f;

	public float itemSlideshowDelay = 0.75f;

	public UnityEvent onLoad = new UnityEvent();
}
