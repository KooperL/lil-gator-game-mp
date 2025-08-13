using System;

// Token: 0x020002CD RID: 717
public class QuestRewardCrafts : QuestReward
{
	// Token: 0x06000E09 RID: 3593 RVA: 0x0004B610 File Offset: 0x00049810
	public override void GiveReward()
	{
		UIMenus.craftNotification.LoadItems(this.rewards);
		ItemObject[] array = this.rewards;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].IsShopUnlocked = true;
		}
	}

	// Token: 0x0400123E RID: 4670
	public ItemObject[] rewards;
}
