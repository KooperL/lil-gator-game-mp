using System;
using UnityEngine;

public class QuestRewardResources : QuestReward
{
	// Token: 0x06000E5B RID: 3675 RVA: 0x0000CBCE File Offset: 0x0000ADCE
	[ContextMenu("Give Reward")]
	public override void GiveReward()
	{
		this.resource.Amount += this.amount;
	}

	public ItemResource resource;

	public int amount;
}
