using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200003D RID: 61
public class PauseAnimator : MonoBehaviour
{
	// Token: 0x060000D1 RID: 209 RVA: 0x00002B2E File Offset: 0x00000D2E
	public YieldInstruction DoThePause()
	{
		return base.StartCoroutine(this.RunThePause());
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00002B3C File Offset: 0x00000D3C
	private IEnumerator RunThePause()
	{
		if (this.waitForPause == null)
		{
			this.waitForPause = new WaitForSeconds(this.pauseTime);
		}
		yield return this.waitForPause;
		yield break;
	}

	// Token: 0x04000132 RID: 306
	public float pauseTime = 0.1f;

	// Token: 0x04000133 RID: 307
	private WaitForSeconds waitForPause;

	// Token: 0x04000134 RID: 308
	private Animator animator;
}
