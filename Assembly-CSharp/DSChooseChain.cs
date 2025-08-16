using System;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/Choose Chained Sequencer")]
public class DSChooseChain : DialogueSequence
{
	// Token: 0x06000587 RID: 1415 RVA: 0x00005FED File Offset: 0x000041ED
	public override YieldInstruction Run()
	{
		base.GetComponent<DialogueSequencer>().chainedSequence = this.choices[DialogueManager.optionChosen];
		return base.Run();
	}

	public DialogueSequencer[] choices;
}
