using System;
using UnityEngine;

public class QuestRewardResources : QuestReward
{
	// Token: 0x06000BBA RID: 3002 RVA: 0x00038E14 File Offset: 0x00037014
	[ContextMenu("Give Reward")]
	public override void GiveReward()
	{
		this.resource.Amount += this.amount;
	}

	public ItemResource resource;

	public int amount;
}
