using System;
using UnityEngine;

// Token: 0x020000CC RID: 204
[AddComponentMenu("Dialogue Sequence/Choose Chained Sequencer")]
public class DSChooseChain : DialogueSequence
{
	// Token: 0x0600046B RID: 1131 RVA: 0x00018E93 File Offset: 0x00017093
	public override YieldInstruction Run()
	{
		base.GetComponent<DialogueSequencer>().chainedSequence = this.choices[DialogueManager.optionChosen];
		return base.Run();
	}

	// Token: 0x0400062E RID: 1582
	public DialogueSequencer[] choices;
}
