using System;
using UnityEngine;

// Token: 0x0200012F RID: 303
[AddComponentMenu("Dialogue Sequence/Wait")]
public class DSWait : DialogueSequence
{
	// Token: 0x060005A3 RID: 1443 RVA: 0x000060AC File Offset: 0x000042AC
	public override YieldInstruction Run()
	{
		if (this.waitForSeconds == null)
		{
			this.waitForSeconds = new WaitForSeconds(this.seconds);
		}
		return this.waitForSeconds;
	}

	// Token: 0x040007AF RID: 1967
	public float seconds = 1f;

	// Token: 0x040007B0 RID: 1968
	private WaitForSeconds waitForSeconds;
}
