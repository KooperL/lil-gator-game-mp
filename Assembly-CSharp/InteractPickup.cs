using System;
using System.Collections;

public class InteractPickup : PersistentObject, Interaction
{
	// Token: 0x060007DF RID: 2015 RVA: 0x0003625C File Offset: 0x0003445C
	public void Interact()
	{
		if (this.resource != null)
		{
			this.resource.Amount += this.resourceAmount;
		}
		this.SaveTrue();
		base.gameObject.SetActive(false);
		if (this.showItemOnFirstPickup && !GameData.g.ReadBool(this.itemID, false))
		{
			CoroutineUtil.Start(this.RunWithItem());
			return;
		}
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x00007D48 File Offset: 0x00005F48
	private IEnumerator RunWithItem()
	{
		this.item.Activate();
		GameData.g.Write(this.itemID, true);
		yield return this.item.Run();
		this.item.Deactivate();
		yield break;
	}

	public ItemResource resource;

	public int resourceAmount;

	public bool showItemOnFirstPickup = true;

	public string itemID;

	public DSItem item;
}
