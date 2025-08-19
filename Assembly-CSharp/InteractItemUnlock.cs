using System;
using UnityEngine.Events;

public class InteractItemUnlock : PersistentObject, Interaction
{
	// Token: 0x060007D9 RID: 2009 RVA: 0x000363AC File Offset: 0x000345AC
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
