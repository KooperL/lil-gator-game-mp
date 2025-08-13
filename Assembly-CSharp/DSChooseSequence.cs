using System;
using UnityEngine;

// Token: 0x020000D0 RID: 208
[AddComponentMenu("Dialogue Sequence/Choose Sequence")]
public class DSChooseSequence : DialogueSequence
{
	// Token: 0x06000474 RID: 1140 RVA: 0x00018F60 File Offset: 0x00017160
	public override YieldInstruction Run()
	{
		DialogueSequencer dialogueSequencer = this.choices[DialogueManager.optionChosen];
		if (dialogueSequencer != null)
		{
			return dialogueSequencer.StartSequence();
		}
		return null;
	}

	// Token: 0x0400063D RID: 1597
	public DialogueSequencer[] choices;
}
