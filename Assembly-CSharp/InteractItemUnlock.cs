using System;
using UnityEngine.Events;

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

	public ItemObject item;

	public string itemName;

	public UnityEvent afterUnlock;
}
