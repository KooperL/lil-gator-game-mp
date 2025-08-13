using System;
using UnityEngine;

// Token: 0x020000E1 RID: 225
[AddComponentMenu("Dialogue Sequence/Wait")]
public class DSWait : DialogueSequence
{
	// Token: 0x060004A6 RID: 1190 RVA: 0x00019BD4 File Offset: 0x00017DD4
	public override YieldInstruction Run()
	{
		if (this.waitForSeconds == null)
		{
			this.waitForSeconds = new WaitForSeconds(this.seconds);
		}
		return this.waitForSeconds;
	}

	// Token: 0x04000678 RID: 1656
	public float seconds = 1f;

	// Token: 0x04000679 RID: 1657
	private WaitForSeconds waitForSeconds;
}
