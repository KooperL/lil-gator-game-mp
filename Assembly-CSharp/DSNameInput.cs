using System;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/Name Input")]
public class DSNameInput : DialogueSequence
{
	// Token: 0x060005D6 RID: 1494 RVA: 0x00006319 File Offset: 0x00004519
	public override YieldInstruction Run()
	{
		return UINameInput.ShowNameInputPrompt();
	}
}
