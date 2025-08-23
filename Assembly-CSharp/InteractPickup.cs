using System;
using System.Collections;

public class InteractPickup : PersistentObject, Interaction
{
	// Token: 0x060007E0 RID: 2016 RVA: 0x00036704 File Offset: 0x00034904
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

	// Token: 0x060007E1 RID: 2017 RVA: 0x00007D5D File Offset: 0x00005F5D
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
