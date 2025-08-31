using System;
using UnityEngine;

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

	public CharacterProfile[] rewards;

	public bool isSilent;

	public bool overrideCount;

	public int rewardCount;
}
