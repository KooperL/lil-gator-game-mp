using System;
using UnityEngine;

// Token: 0x020002CF RID: 719
[AddComponentMenu("Quest Reward NPCs")]
public class QuestRewardNPCs : QuestReward
{
	// Token: 0x06000E0D RID: 3597 RVA: 0x0004B64C File Offset: 0x0004984C
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

	// Token: 0x04001241 RID: 4673
	public CharacterProfile[] rewards;

	// Token: 0x04001242 RID: 4674
	public bool isSilent;

	// Token: 0x04001243 RID: 4675
	public bool overrideCount;

	// Token: 0x04001244 RID: 4676
	public int rewardCount;
}
