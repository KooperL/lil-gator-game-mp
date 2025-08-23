using System;
using UnityEngine;

public class QuestRewards : MonoBehaviour
{
	// Token: 0x06000E5E RID: 3678 RVA: 0x0004D500 File Offset: 0x0004B700
	public void GiveAllRewards()
	{
		QuestReward[] components = base.GetComponents<QuestReward>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].GiveReward();
		}
	}
}
