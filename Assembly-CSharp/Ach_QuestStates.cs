using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200008C RID: 140
[AddComponentMenu("Achievement Tracker - QuestStates")]
public class Ach_QuestStates : MonoBehaviour
{
	// Token: 0x060001E1 RID: 481 RVA: 0x0000385B File Offset: 0x00001A5B
	public void Start()
	{
		this.hasUnlocked = false;
		if (this.questStates.StateID >= this.requiredIndex)
		{
			this.UnlockAchievement();
			return;
		}
		this.questStates.onStateChange.AddListener(new UnityAction<int>(this.OnQuestStateChange));
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x0000389A File Offset: 0x00001A9A
	private void OnQuestStateChange(int index)
	{
		if (!this.hasUnlocked && index >= this.requiredIndex)
		{
			this.UnlockAchievement();
		}
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x000038B3 File Offset: 0x00001AB3
	private void UnlockAchievement()
	{
		if (this.hasUnlocked)
		{
			return;
		}
		this.achievement.UnlockAchievement();
		this.hasUnlocked = true;
	}

	// Token: 0x040002CE RID: 718
	public Achievement achievement;

	// Token: 0x040002CF RID: 719
	public QuestStates questStates;

	// Token: 0x040002D0 RID: 720
	public int requiredIndex;

	// Token: 0x040002D1 RID: 721
	private bool hasUnlocked;
}
