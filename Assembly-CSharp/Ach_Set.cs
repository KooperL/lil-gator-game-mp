using System;
using UnityEngine;

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

	public Achievement achievement;

	public bool setOnEnable;
}
