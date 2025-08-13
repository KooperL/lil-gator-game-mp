using System;
using UnityEngine;

// Token: 0x02000114 RID: 276
[AddComponentMenu("Dialogue Sequence/Choose Chained Sequencer")]
public class DSChooseChain : DialogueSequence
{
	// Token: 0x0600054D RID: 1357 RVA: 0x00005D27 File Offset: 0x00003F27
	public override YieldInstruction Run()
	{
		base.GetComponent<DialogueSequencer>().chainedSequence = this.choices[DialogueManager.optionChosen];
		return base.Run();
	}

	// Token: 0x04000749 RID: 1865
	public DialogueSequencer[] choices;
}
