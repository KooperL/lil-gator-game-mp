using System;
using UnityEngine;

// Token: 0x02000126 RID: 294
[AddComponentMenu("Dialogue Sequence/UI Display (Sequence)")]
public class DSDisplay : DialogueSequence
{
	// Token: 0x06000582 RID: 1410 RVA: 0x00005F41 File Offset: 0x00004141
	public override YieldInstruction Run()
	{
		return base.StartCoroutine(this.display.RunDisplay());
	}

	// Token: 0x04000793 RID: 1939
	public UIDisplay display;
}
