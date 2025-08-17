using System;

public class QuestRewardCrafts : QuestReward
{
	// Token: 0x06000E55 RID: 3669 RVA: 0x0004D198 File Offset: 0x0004B398
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
