using System;
using UnityEngine;

// Token: 0x0200012E RID: 302
[AddComponentMenu("Dialogue Sequence/Nested Sequence")]
public class DSSequence : DialogueSequence
{
	// Token: 0x060005A1 RID: 1441 RVA: 0x0000609F File Offset: 0x0000429F
	public override YieldInstruction Run()
	{
		return this.nestedSequence.StartSequence();
	}

	// Token: 0x040007AE RID: 1966
	public DialogueSequencer nestedSequence;
}
