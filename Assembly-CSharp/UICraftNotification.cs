using System;
using UnityEngine;
using UnityEngine.Events;

public class UICraftNotification : MonoBehaviour
{
	// Token: 0x06001203 RID: 4611 RVA: 0x0005A004 File Offset: 0x00058204
	public void LoadItems(ItemObject[] items)
	{
		this.ResetThing();
		this.items = items;
		this.itemDisplay.LoadItem(items[0]);
		this.itemIndex = 0;
		this.nextItemTime = Time.time + this.itemSlideshowDelay;
		this.onLoad.Invoke();
	}

	// Token: 0x06001204 RID: 4612 RVA: 0x0000F4C2 File Offset: 0x0000D6C2
	public void LoadItem(ItemObject item)
	{
		this.ResetThing();
		this.items = null;
		this.itemDisplay.LoadItem(item);
		this.onLoad.Invoke();
	}

	// Token: 0x06001205 RID: 4613 RVA: 0x0000F4E8 File Offset: 0x0000D6E8
	private void ResetThing()
	{
		this.hideBehavior.Show();
		this.hideTime = Time.time + this.hideDelay;
	}

	// Token: 0x06001206 RID: 4614 RVA: 0x0005A050 File Offset: 0x00058250
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
