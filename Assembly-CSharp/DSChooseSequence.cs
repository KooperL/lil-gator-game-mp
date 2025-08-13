using System;
using UnityEngine;

// Token: 0x0200011A RID: 282
[AddComponentMenu("Dialogue Sequence/Choose Sequence")]
public class DSChooseSequence : DialogueSequence
{
	// Token: 0x0600055C RID: 1372 RVA: 0x0002D9BC File Offset: 0x0002BBBC
	public override YieldInstruction Run()
	{
		DialogueSequencer dialogueSequencer = this.choices[DialogueManager.optionChosen];
		if (dialogueSequencer != null)
		{
			return dialogueSequencer.StartSequence();
		}
		return null;
	}

	// Token: 0x0400075F RID: 1887
	public DialogueSequencer[] choices;
}
