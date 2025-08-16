using System;
using UnityEngine;

public class Ach_Set : MonoBehaviour
{
	// Token: 0x060001F6 RID: 502 RVA: 0x000039DC File Offset: 0x00001BDC
	private void OnEnable()
	{
		if (this.setOnEnable)
		{
			this.UnlockAchievement();
		}
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x000039EC File Offset: 0x00001BEC
	public void UnlockAchievement()
	{
		this.achievement.UnlockAchievement();
	}

	public Achievement achievement;

	public bool setOnEnable;
}
