using System;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Achievement Tracker - QuestStates")]
public class Ach_QuestStates : MonoBehaviour
{
	// Token: 0x060001EE RID: 494 RVA: 0x00003947 File Offset: 0x00001B47
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

	// Token: 0x060001EF RID: 495 RVA: 0x00003986 File Offset: 0x00001B86
	private void OnQuestStateChange(int index)
	{
		if (!this.hasUnlocked && index >= this.requiredIndex)
		{
			this.UnlockAchievement();
		}
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x0000399F File Offset: 0x00001B9F
	private void UnlockAchievement()
	{
		if (this.hasUnlocked)
		{
			return;
		}
		this.achievement.UnlockAchievement();
		this.hasUnlocked = true;
	}

	public Achievement achievement;

	public QuestStates questStates;

	public int requiredIndex;

	private bool hasUnlocked;
}
