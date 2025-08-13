using System;
using UnityEngine;

// Token: 0x02000091 RID: 145
[CreateAssetMenu]
public class Achievement : ScriptableObject
{
	// Token: 0x060001F4 RID: 500 RVA: 0x000039A2 File Offset: 0x00001BA2
	public bool IsAchievementUnlocked()
	{
		return false;
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x000039A5 File Offset: 0x00001BA5
	[ContextMenu("Unlock Achievement")]
	public void SetProgress(int progress)
	{
		AchievementManager.MarkAchievementProgress(this, progress);
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x000039AE File Offset: 0x00001BAE
	[ContextMenu("Unlock Achievement")]
	public void UnlockAchievement()
	{
		AchievementManager.MarkAchievementToUnlock(this);
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x000039B6 File Offset: 0x00001BB6
	[ContextMenu("Lock Achievement")]
	public void LockAchievement()
	{
		AchievementManager.a.LockAchievement(this);
	}

	// Token: 0x040002E2 RID: 738
	public string steamID;

	// Token: 0x040002E3 RID: 739
	[Header("Progressive")]
	public bool isProgressive;

	// Token: 0x040002E4 RID: 740
	public int maxProgress;
}
