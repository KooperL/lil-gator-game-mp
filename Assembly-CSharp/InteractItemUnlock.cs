using System;
using UnityEngine.Events;

public class InteractItemUnlock : PersistentObject, Interaction
{
	// Token: 0x060007DA RID: 2010 RVA: 0x00036698 File Offset: 0x00034898
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
