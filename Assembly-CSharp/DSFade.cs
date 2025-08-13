using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000127 RID: 295
[AddComponentMenu("Dialogue Sequence/Fade")]
public class DSFade : DialogueSequence
{
	// Token: 0x06000584 RID: 1412 RVA: 0x00005F54 File Offset: 0x00004154
	public override YieldInstruction Run()
	{
		if (this.fadeIn && !this.fadeOut)
		{
			return Blackout.FadeIn();
		}
		if (this.fadeOut && !this.fadeIn)
		{
			return Blackout.FadeOut();
		}
		return base.StartCoroutine(this.RunFade());
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x00005F8E File Offset: 0x0000418E
	private IEnumerator RunFade()
	{
		yield return Blackout.FadeIn();
		if (this.waitForFadeOut)
		{
			yield return Blackout.FadeOut();
		}
		else
		{
			Blackout.FadeOut();
		}
		yield break;
	}

	// Token: 0x04000794 RID: 1940
	public bool fadeIn = true;

	// Token: 0x04000795 RID: 1941
	public bool fadeOut = true;

	// Token: 0x04000796 RID: 1942
	public bool waitForFadeOut;
}
