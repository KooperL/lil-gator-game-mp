using System;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/Nested Sequence")]
public class DSSequence : DialogueSequence
{
	// Token: 0x060005DB RID: 1499 RVA: 0x00006365 File Offset: 0x00004565
	public override YieldInstruction Run()
	{
		return this.nestedSequence.StartSequence();
	}

	public DialogueSequencer nestedSequence;
}
