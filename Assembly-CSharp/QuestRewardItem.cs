using System;

public class QuestRewardItem : QuestReward
{
	// Token: 0x06000E58 RID: 3672 RVA: 0x0000CB96 File Offset: 0x0000AD96
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
