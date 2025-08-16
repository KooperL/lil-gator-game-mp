using System;
using UnityEngine;

public class QuestRewards : MonoBehaviour
{
	// Token: 0x06000E5D RID: 3677 RVA: 0x0004D0A4 File Offset: 0x0004B2A4
	public void GiveAllRewards()
	{
		QuestReward[] components = base.GetComponents<QuestReward>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].GiveReward();
		}
	}
}
