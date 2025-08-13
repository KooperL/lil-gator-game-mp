using System;
using UnityEngine;

// Token: 0x0200006E RID: 110
public class Ach_Set : MonoBehaviour
{
	// Token: 0x060001B3 RID: 435 RVA: 0x00009A82 File Offset: 0x00007C82
	private void OnEnable()
	{
		if (this.setOnEnable)
		{
			this.UnlockAchievement();
		}
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x00009A92 File Offset: 0x00007C92
	public void UnlockAchievement()
	{
		this.achievement.UnlockAchievement();
	}

	// Token: 0x04000256 RID: 598
	public Achievement achievement;

	// Token: 0x04000257 RID: 599
	public bool setOnEnable;
}
