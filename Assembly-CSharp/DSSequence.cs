using System;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/Nested Sequence")]
public class DSSequence : DialogueSequence
{
	// Token: 0x060004A4 RID: 1188 RVA: 0x00019BBF File Offset: 0x00017DBF
	public override YieldInstruction Run()
	{
		return this.nestedSequence.StartSequence();
	}

	public DialogueSequencer nestedSequence;
}
