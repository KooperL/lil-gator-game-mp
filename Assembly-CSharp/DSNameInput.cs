using System;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/Name Input")]
public class DSNameInput : DialogueSequence
{
	// Token: 0x0600049F RID: 1183 RVA: 0x00019B63 File Offset: 0x00017D63
	public override YieldInstruction Run()
	{
		return UINameInput.ShowNameInputPrompt();
	}
}
