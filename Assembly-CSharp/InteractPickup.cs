using System;
using System.Collections;

public class InteractPickup : PersistentObject, Interaction
{
	// Token: 0x0600067A RID: 1658 RVA: 0x000214B8 File Offset: 0x0001F6B8
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

	// Token: 0x0600067B RID: 1659 RVA: 0x00021525 File Offset: 0x0001F725
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
