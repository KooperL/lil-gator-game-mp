using System;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/UI Display (Sequence)")]
public class DSDisplay : DialogueSequence
{
	// Token: 0x060005BC RID: 1468 RVA: 0x00006207 File Offset: 0x00004407
	public override YieldInstruction Run()
	{
		return base.StartCoroutine(this.display.RunDisplay());
	}

	public UIDisplay display;
}
