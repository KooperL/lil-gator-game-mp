using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000032 RID: 50
public class PauseAnimator : MonoBehaviour
{
	// Token: 0x060000C4 RID: 196 RVA: 0x00005AA7 File Offset: 0x00003CA7
	public YieldInstruction DoThePause()
	{
		return base.StartCoroutine(this.RunThePause());
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x00005AB5 File Offset: 0x00003CB5
	private IEnumerator RunThePause()
	{
		if (this.waitForPause == null)
		{
			this.waitForPause = new WaitForSeconds(this.pauseTime);
		}
		yield return this.waitForPause;
		yield break;
	}

	// Token: 0x04000108 RID: 264
	public float pauseTime = 0.1f;

	// Token: 0x04000109 RID: 265
	private WaitForSeconds waitForPause;

	// Token: 0x0400010A RID: 266
	private Animator animator;
}
