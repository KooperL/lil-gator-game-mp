using System;

public class QuestRewardItem : QuestReward
{
	// Token: 0x06000E57 RID: 3671 RVA: 0x0000CB8C File Offset: 0x0000AD8C
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
