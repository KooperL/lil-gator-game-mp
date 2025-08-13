using System;
using UnityEngine;

// Token: 0x020000DD RID: 221
[AddComponentMenu("Dialogue Sequence/Load Choice")]
public class DSLoadChoice : DSDialogue
{
	// Token: 0x0600049D RID: 1181 RVA: 0x00019B28 File Offset: 0x00017D28
	public override YieldInstruction Run()
	{
		int num = GameData.g.ReadInt(this.savedChoice, 0);
		this.dialogue = this.choices[num];
		return base.Run();
	}

	// Token: 0x04000673 RID: 1651
	[ChunkLookup("document")]
	public string[] choices;

	// Token: 0x04000674 RID: 1652
	public string savedChoice;
}
