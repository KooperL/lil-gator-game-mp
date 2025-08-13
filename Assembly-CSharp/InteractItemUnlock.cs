using System;
using UnityEngine.Events;

// Token: 0x02000197 RID: 407
public class InteractItemUnlock : PersistentObject, Interaction
{
	// Token: 0x06000799 RID: 1945 RVA: 0x00034A68 File Offset: 0x00032C68
	public void Interact()
	{
		if (this.item != null)
		{
			ItemManager.i.UnlockItem(this.item.id);
		}
		else
		{
			ItemManager.i.UnlockItem(this.itemName);
		}
		base.gameObject.SetActive(false);
		this.SaveTrue();
		if (this.afterUnlock != null)
		{
			this.afterUnlock.Invoke();
		}
	}

	// Token: 0x04000A24 RID: 2596
	public ItemObject item;

	// Token: 0x04000A25 RID: 2597
	public string itemName;

	// Token: 0x04000A26 RID: 2598
	public UnityEvent afterUnlock;
}
