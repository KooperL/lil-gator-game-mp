using System;
using System.Collections;

// Token: 0x02000199 RID: 409
public class InteractPickup : PersistentObject, Interaction
{
	// Token: 0x0600079F RID: 1951 RVA: 0x00034AD4 File Offset: 0x00032CD4
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

	// Token: 0x060007A0 RID: 1952 RVA: 0x00007A4E File Offset: 0x00005C4E
	private IEnumerator RunWithItem()
	{
		this.item.Activate();
		GameData.g.Write(this.itemID, true);
		yield return this.item.Run();
		this.item.Deactivate();
		yield break;
	}

	// Token: 0x04000A2A RID: 2602
	public ItemResource resource;

	// Token: 0x04000A2B RID: 2603
	public int resourceAmount;

	// Token: 0x04000A2C RID: 2604
	public bool showItemOnFirstPickup = true;

	// Token: 0x04000A2D RID: 2605
	public string itemID;

	// Token: 0x04000A2E RID: 2606
	public DSItem item;
}
