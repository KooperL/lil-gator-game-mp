using System;
using System.Collections;
using UnityEngine;

public class PauseAnimator : MonoBehaviour
{
	// Token: 0x060000D9 RID: 217 RVA: 0x00002B92 File Offset: 0x00000D92
	public YieldInstruction DoThePause()
	{
		return base.StartCoroutine(this.RunThePause());
	}

	// Token: 0x060000DA RID: 218 RVA: 0x00002BA0 File Offset: 0x00000DA0
	private IEnumerator RunThePause()
	{
		if (this.waitForPause == null)
		{
			this.waitForPause = new WaitForSeconds(this.pauseTime);
		}
		yield return this.waitForPause;
		yield break;
	}

	public float pauseTime = 0.1f;

	private WaitForSeconds waitForPause;

	private Animator animator;
}
