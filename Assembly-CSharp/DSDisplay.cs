using System;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/UI Display (Sequence)")]
public class DSDisplay : DialogueSequence
{
	// Token: 0x06000491 RID: 1169 RVA: 0x000199D6 File Offset: 0x00017BD6
	public override YieldInstruction Run()
	{
		return base.StartCoroutine(this.display.RunDisplay());
	}

	public UIDisplay display;
}
