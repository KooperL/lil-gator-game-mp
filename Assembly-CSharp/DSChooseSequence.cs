using System;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/Choose Sequence")]
public class DSChooseSequence : DialogueSequence
{
	// Token: 0x06000596 RID: 1430 RVA: 0x0002F0B8 File Offset: 0x0002D2B8
	public override YieldInstruction Run()
	{
		DialogueSequencer dialogueSequencer = this.choices[DialogueManager.optionChosen];
		if (dialogueSequencer != null)
		{
			return dialogueSequencer.StartSequence();
		}
		return null;
	}

	public DialogueSequencer[] choices;
}
