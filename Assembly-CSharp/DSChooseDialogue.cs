using System;
using UnityEngine;

// Token: 0x020000CE RID: 206
[AddComponentMenu("Dialogue Sequence/Choose Dialogue")]
public class DSChooseDialogue : DSDialogue
{
	// Token: 0x06000470 RID: 1136 RVA: 0x00018EEC File Offset: 0x000170EC
	public override YieldInstruction Run()
	{
		int optionChosen = DialogueManager.optionChosen;
		this.dialogue = this.choices[optionChosen];
		if (this.saveChoice)
		{
			GameData.g.Write(this.saveChoiceID, optionChosen);
		}
		return base.Run();
	}

	// Token: 0x04000639 RID: 1593
	[ChunkLookup("document")]
	public string[] choices;

	// Token: 0x0400063A RID: 1594
	public bool saveChoice;

	// Token: 0x0400063B RID: 1595
	[ConditionalHide("saveChoice", true)]
	public string saveChoiceID;
}
