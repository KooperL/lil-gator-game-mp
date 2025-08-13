using System;
using UnityEngine;

// Token: 0x0200012B RID: 299
[AddComponentMenu("Dialogue Sequence/Load Choice")]
public class DSLoadChoice : DSDialogue
{
	// Token: 0x0600059A RID: 1434 RVA: 0x0002E6C8 File Offset: 0x0002C8C8
	public override YieldInstruction Run()
	{
		int num = GameData.g.ReadInt(this.savedChoice, 0);
		this.dialogue = this.choices[num];
		return base.Run();
	}

	// Token: 0x040007AA RID: 1962
	[ChunkLookup("document")]
	public string[] choices;

	// Token: 0x040007AB RID: 1963
	public string savedChoice;
}
