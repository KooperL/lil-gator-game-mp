using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200006C RID: 108
[AddComponentMenu("Achievement Tracker - QuestStates")]
public class Ach_QuestStates : MonoBehaviour
{
	// Token: 0x060001AB RID: 427 RVA: 0x0000997E File Offset: 0x00007B7E
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

	// Token: 0x060001AC RID: 428 RVA: 0x000099BD File Offset: 0x00007BBD
	private void OnQuestStateChange(int index)
	{
		if (!this.hasUnlocked && index >= this.requiredIndex)
		{
			this.UnlockAchievement();
		}
	}

	// Token: 0x060001AD RID: 429 RVA: 0x000099D6 File Offset: 0x00007BD6
	private void UnlockAchievement()
	{
		if (this.hasUnlocked)
		{
			return;
		}
		this.achievement.UnlockAchievement();
		this.hasUnlocked = true;
	}

	// Token: 0x0400024A RID: 586
	public Achievement achievement;

	// Token: 0x0400024B RID: 587
	public QuestStates questStates;

	// Token: 0x0400024C RID: 588
	public int requiredIndex;

	// Token: 0x0400024D RID: 589
	private bool hasUnlocked;
}
