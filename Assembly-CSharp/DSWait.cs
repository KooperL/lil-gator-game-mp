using System;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/Wait")]
public class DSWait : DialogueSequence
{
	// Token: 0x060005DD RID: 1501 RVA: 0x00006372 File Offset: 0x00004572
	public override YieldInstruction Run()
	{
		if (this.waitForSeconds == null)
		{
			this.waitForSeconds = new WaitForSeconds(this.seconds);
		}
		return this.waitForSeconds;
	}

	public float seconds = 1f;

	private WaitForSeconds waitForSeconds;
}
