using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000DB RID: 219
[AddComponentMenu("Dialogue Sequence/Fade")]
public class DSFade : DialogueSequence
{
	// Token: 0x06000493 RID: 1171 RVA: 0x000199F1 File Offset: 0x00017BF1
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

	// Token: 0x06000494 RID: 1172 RVA: 0x00019A2B File Offset: 0x00017C2B
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

	// Token: 0x04000663 RID: 1635
	public bool fadeIn = true;

	// Token: 0x04000664 RID: 1636
	public bool fadeOut = true;

	// Token: 0x04000665 RID: 1637
	public bool waitForFadeOut;
}
