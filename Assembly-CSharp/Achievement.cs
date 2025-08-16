using System;
using UnityEngine;

[CreateAssetMenu]
public class Achievement : ScriptableObject
{
	// Token: 0x06000201 RID: 513 RVA: 0x00003A8E File Offset: 0x00001C8E
	public bool IsAchievementUnlocked()
	{
		return false;
	}

	// Token: 0x06000202 RID: 514 RVA: 0x00003A91 File Offset: 0x00001C91
	[ContextMenu("Unlock Achievement")]
	public void SetProgress(int progress)
	{
		AchievementManager.MarkAchievementProgress(this, progress);
	}

	// Token: 0x06000203 RID: 515 RVA: 0x00003A9A File Offset: 0x00001C9A
	[ContextMenu("Unlock Achievement")]
	public void UnlockAchievement()
	{
		AchievementManager.MarkAchievementToUnlock(this);
	}

	// Token: 0x06000204 RID: 516 RVA: 0x00003AA2 File Offset: 0x00001CA2
	[ContextMenu("Lock Achievement")]
	public void LockAchievement()
	{
		AchievementManager.a.LockAchievement(this);
	}

	public string steamID;

	[Header("Progressive")]
	public bool isProgressive;

	public int maxProgress;
}
