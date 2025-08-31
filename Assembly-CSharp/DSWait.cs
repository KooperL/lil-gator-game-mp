using System;
using UnityEngine;

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

	public float seconds = 1f;

	private WaitForSeconds waitForSeconds;
}
