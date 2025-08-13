using System;
using UnityEngine;

// Token: 0x020002D1 RID: 721
public class QuestRewards : MonoBehaviour
{
	// Token: 0x06000E11 RID: 3601 RVA: 0x0004B6B0 File Offset: 0x000498B0
	public void GiveAllRewards()
	{
		QuestReward[] components = base.GetComponents<QuestReward>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].GiveReward();
		}
	}
}
