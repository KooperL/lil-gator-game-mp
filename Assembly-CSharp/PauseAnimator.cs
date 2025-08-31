using System;
using System.Collections;
using UnityEngine;

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

	public float pauseTime = 0.1f;

	private WaitForSeconds waitForPause;

	private Animator animator;
}
