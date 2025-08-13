using System;
using UnityEngine;

// Token: 0x0200012C RID: 300
[AddComponentMenu("Dialogue Sequence/Name Input")]
public class DSNameInput : DialogueSequence
{
	// Token: 0x0600059C RID: 1436 RVA: 0x00006053 File Offset: 0x00004253
	public override YieldInstruction Run()
	{
		return UINameInput.ShowNameInputPrompt();
	}
}
