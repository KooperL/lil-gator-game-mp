using System;
using UnityEngine;

// Token: 0x02000221 RID: 545
public class QuestRewardResources : QuestReward
{
	// Token: 0x06000BBA RID: 3002 RVA: 0x00038E14 File Offset: 0x00037014
	[ContextMenu("Give Reward")]
	public override void GiveReward()
	{
		this.resource.Amount += this.amount;
	}

	// Token: 0x04000F82 RID: 3970
	public ItemResource resource;

	// Token: 0x04000F83 RID: 3971
	public int amount;
}
