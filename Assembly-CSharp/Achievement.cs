using System;
using UnityEngine;

// Token: 0x02000071 RID: 113
[CreateAssetMenu]
public class Achievement : ScriptableObject
{
	// Token: 0x060001BE RID: 446 RVA: 0x00009BC1 File Offset: 0x00007DC1
	public bool IsAchievementUnlocked()
	{
		return false;
	}

	// Token: 0x060001BF RID: 447 RVA: 0x00009BC4 File Offset: 0x00007DC4
	[ContextMenu("Unlock Achievement")]
	public void SetProgress(int progress)
	{
		AchievementManager.MarkAchievementProgress(this, progress);
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x00009BCD File Offset: 0x00007DCD
	[ContextMenu("Unlock Achievement")]
	public void UnlockAchievement()
	{
		AchievementManager.MarkAchievementToUnlock(this);
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x00009BD5 File Offset: 0x00007DD5
	[ContextMenu("Lock Achievement")]
	public void LockAchievement()
	{
		AchievementManager.a.LockAchievement(this);
	}

	// Token: 0x0400025E RID: 606
	public string steamID;

	// Token: 0x0400025F RID: 607
	[Header("Progressive")]
	public bool isProgressive;

	// Token: 0x04000260 RID: 608
	public int maxProgress;
}
