using System;
using UnityEngine;

// Token: 0x02000222 RID: 546
public class QuestRewards : MonoBehaviour
{
	// Token: 0x06000BBC RID: 3004 RVA: 0x00038E38 File Offset: 0x00037038
	public void GiveAllRewards()
	{
		QuestReward[] components = base.GetComponents<QuestReward>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].GiveReward();
		}
	}
}
