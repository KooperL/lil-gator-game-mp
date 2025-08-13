using System;
using UnityEngine;

// Token: 0x020002D0 RID: 720
public class QuestRewardResources : QuestReward
{
	// Token: 0x06000E0F RID: 3599 RVA: 0x0000C8BC File Offset: 0x0000AABC
	[ContextMenu("Give Reward")]
	public override void GiveReward()
	{
		this.resource.Amount += this.amount;
	}

	// Token: 0x04001245 RID: 4677
	public ItemResource resource;

	// Token: 0x04001246 RID: 4678
	public int amount;
}
