using System;

// Token: 0x020002CE RID: 718
public class QuestRewardItem : QuestReward
{
	// Token: 0x06000E0B RID: 3595 RVA: 0x0000C884 File Offset: 0x0000AA84
	public override void GiveReward()
	{
		if (this.itemObject != null)
		{
			ItemManager.i.UnlockItem(this.itemObject.id);
			return;
		}
		ItemManager.i.UnlockItem(this.item);
	}

	// Token: 0x0400123F RID: 4671
	public string item;

	// Token: 0x04001240 RID: 4672
	public ItemObject itemObject;
}
