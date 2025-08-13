using System;

// Token: 0x0200021F RID: 543
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

	// Token: 0x04000F7C RID: 3964
	public string item;

	// Token: 0x04000F7D RID: 3965
	public ItemObject itemObject;
}
