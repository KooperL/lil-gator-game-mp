using System;

public class QuestRewardCrafts : QuestReward
{
	// Token: 0x06000E56 RID: 3670 RVA: 0x0004D460 File Offset: 0x0004B660
	public override void GiveReward()
	{
		UIMenus.craftNotification.LoadItems(this.rewards);
		ItemObject[] array = this.rewards;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].IsShopUnlocked = true;
		}
	}

	public ItemObject[] rewards;
}
