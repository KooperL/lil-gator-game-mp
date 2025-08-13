using System;
using UnityEngine;

// Token: 0x020000E0 RID: 224
[AddComponentMenu("Dialogue Sequence/Nested Sequence")]
public class DSSequence : DialogueSequence
{
	// Token: 0x060004A4 RID: 1188 RVA: 0x00019BBF File Offset: 0x00017DBF
	public override YieldInstruction Run()
	{
		return this.nestedSequence.StartSequence();
	}

	// Token: 0x04000677 RID: 1655
	public DialogueSequencer nestedSequence;
}
