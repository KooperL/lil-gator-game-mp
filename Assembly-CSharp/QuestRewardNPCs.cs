using System;
using UnityEngine;

// Token: 0x02000220 RID: 544
[AddComponentMenu("Quest Reward NPCs")]
public class QuestRewardNPCs : QuestReward
{
	// Token: 0x06000BB8 RID: 3000 RVA: 0x00038DA8 File Offset: 0x00036FA8
	[ContextMenu("Give Reward")]
	public override void GiveReward()
	{
		if (TownNPCManager.t == null)
		{
			return;
		}
		if (this.isSilent)
		{
			TownNPCManager.t.RewardNPCsSilently(this.rewards);
			return;
		}
		if (this.overrideCount)
		{
			TownNPCManager.t.RewardNPCs(this.rewards, this.rewardCount);
			return;
		}
		TownNPCManager.t.RewardNPCs(this.rewards, -1);
	}

	// Token: 0x04000F7E RID: 3966
	public CharacterProfile[] rewards;

	// Token: 0x04000F7F RID: 3967
	public bool isSilent;

	// Token: 0x04000F80 RID: 3968
	public bool overrideCount;

	// Token: 0x04000F81 RID: 3969
	public int rewardCount;
}
