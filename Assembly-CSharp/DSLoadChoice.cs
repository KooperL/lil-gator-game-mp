using System;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/Load Choice")]
public class DSLoadChoice : DSDialogue
{
	// Token: 0x060005D4 RID: 1492 RVA: 0x0002FDA0 File Offset: 0x0002DFA0
	public override YieldInstruction Run()
	{
		int num = GameData.g.ReadInt(this.savedChoice, 0);
		this.dialogue = this.choices[num];
		return base.Run();
	}

	[ChunkLookup("document")]
	public string[] choices;

	public string savedChoice;
}
