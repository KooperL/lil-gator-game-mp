using System;
using System.Collections;

// Token: 0x0200013C RID: 316
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

	// Token: 0x040008BA RID: 2234
	public ItemResource resource;

	// Token: 0x040008BB RID: 2235
	public int resourceAmount;

	// Token: 0x040008BC RID: 2236
	public bool showItemOnFirstPickup = true;

	// Token: 0x040008BD RID: 2237
	public string itemID;

	// Token: 0x040008BE RID: 2238
	public DSItem item;
}
