using System;
using UnityEngine;

[AddComponentMenu("Quest Reward NPCs")]
public class QuestRewardNPCs : QuestReward
{
	// Token: 0x06000E59 RID: 3673 RVA: 0x0004D040 File Offset: 0x0004B240
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

	public CharacterProfile[] rewards;

	public bool isSilent;

	public bool overrideCount;

	public int rewardCount;
}
