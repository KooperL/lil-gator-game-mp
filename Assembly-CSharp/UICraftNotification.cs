using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020003A2 RID: 930
public class UICraftNotification : MonoBehaviour
{
	// Token: 0x060011A3 RID: 4515 RVA: 0x00058064 File Offset: 0x00056264
	public void LoadItems(ItemObject[] items)
	{
		this.ResetThing();
		this.items = items;
		this.itemDisplay.LoadItem(items[0]);
		this.itemIndex = 0;
		this.nextItemTime = Time.time + this.itemSlideshowDelay;
		this.onLoad.Invoke();
	}

	// Token: 0x060011A4 RID: 4516 RVA: 0x0000F0CF File Offset: 0x0000D2CF
	public void LoadItem(ItemObject item)
	{
		this.ResetThing();
		this.items = null;
		this.itemDisplay.LoadItem(item);
		this.onLoad.Invoke();
	}

	// Token: 0x060011A5 RID: 4517 RVA: 0x0000F0F5 File Offset: 0x0000D2F5
	private void ResetThing()
	{
		this.hideBehavior.Show();
		this.hideTime = Time.time + this.hideDelay;
	}

	// Token: 0x060011A6 RID: 4518 RVA: 0x000580B0 File Offset: 0x000562B0
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

	// Token: 0x040016BB RID: 5819
	public UIItemDisplay itemDisplay;

	// Token: 0x040016BC RID: 5820
	public UIHideBehavior hideBehavior;

	// Token: 0x040016BD RID: 5821
	public float hideDelay = 5f;

	// Token: 0x040016BE RID: 5822
	private float hideTime;

	// Token: 0x040016BF RID: 5823
	private ItemObject[] items;

	// Token: 0x040016C0 RID: 5824
	private int itemIndex;

	// Token: 0x040016C1 RID: 5825
	private float nextItemTime = -1f;

	// Token: 0x040016C2 RID: 5826
	public float itemSlideshowDelay = 0.75f;

	// Token: 0x040016C3 RID: 5827
	public UnityEvent onLoad = new UnityEvent();
}
