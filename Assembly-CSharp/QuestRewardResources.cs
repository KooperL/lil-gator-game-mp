using System;
using UnityEngine;

public class QuestRewardResources : QuestReward
{
	// Token: 0x06000E5B RID: 3675 RVA: 0x0000CBAF File Offset: 0x0000ADAF
	[ContextMenu("Give Reward")]
	public override void GiveReward()
	{
		this.resource.Amount += this.amount;
	}

	public ItemResource resource;

	public int amount;
}
