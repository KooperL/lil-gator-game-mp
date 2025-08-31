using System;

public class QuestRewardCrafts : QuestReward
{
	// Token: 0x06000BB4 RID: 2996 RVA: 0x00038D24 File Offset: 0x00036F24
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
