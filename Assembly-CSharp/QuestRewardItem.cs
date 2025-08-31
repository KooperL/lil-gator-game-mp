using System;

public class QuestRewardItem : QuestReward
{
	// Token: 0x06000BB6 RID: 2998 RVA: 0x00038D67 File Offset: 0x00036F67
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
