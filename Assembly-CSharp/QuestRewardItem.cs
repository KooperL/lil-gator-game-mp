using System;

public class QuestRewardItem : QuestReward
{
	// Token: 0x06000E57 RID: 3671 RVA: 0x0000CB77 File Offset: 0x0000AD77
	public override void GiveReward()
	{
		if (this.itemObject != null)
		{
			ItemManager.i.UnlockItem(this.itemObject.id);
			return;
		}
		ItemManager.i.UnlockItem(this.item);
	}

	public string item;

	public ItemObject itemObject;
}
