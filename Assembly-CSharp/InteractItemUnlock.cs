using System;
using UnityEngine.Events;

// Token: 0x0200013A RID: 314
public class InteractItemUnlock : PersistentObject, Interaction
{
	// Token: 0x06000674 RID: 1652 RVA: 0x000213D0 File Offset: 0x0001F5D0
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

	// Token: 0x040008B4 RID: 2228
	public ItemObject item;

	// Token: 0x040008B5 RID: 2229
	public string itemName;

	// Token: 0x040008B6 RID: 2230
	public UnityEvent afterUnlock;
}
