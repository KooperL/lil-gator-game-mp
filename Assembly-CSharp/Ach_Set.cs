using System;
using UnityEngine;

// Token: 0x0200008E RID: 142
public class Ach_Set : MonoBehaviour
{
	// Token: 0x060001E9 RID: 489 RVA: 0x000038F0 File Offset: 0x00001AF0
	private void OnEnable()
	{
		if (this.setOnEnable)
		{
			this.UnlockAchievement();
		}
	}

	// Token: 0x060001EA RID: 490 RVA: 0x00003900 File Offset: 0x00001B00
	public void UnlockAchievement()
	{
		this.achievement.UnlockAchievement();
	}

	// Token: 0x040002DA RID: 730
	public Achievement achievement;

	// Token: 0x040002DB RID: 731
	public bool setOnEnable;
}
