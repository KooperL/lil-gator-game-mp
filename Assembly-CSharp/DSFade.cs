using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/Fade")]
public class DSFade : DialogueSequence
{
	// Token: 0x060005BE RID: 1470 RVA: 0x0000621A File Offset: 0x0000441A
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

	// Token: 0x060005BF RID: 1471 RVA: 0x00006254 File Offset: 0x00004454
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

	public bool fadeIn = true;

	public bool fadeOut = true;

	public bool waitForFadeOut;
}
