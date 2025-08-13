using System;
using UnityEngine;

// Token: 0x02000118 RID: 280
[AddComponentMenu("Dialogue Sequence/Choose Dialogue")]
public class DSChooseDialogue : DSDialogue
{
	// Token: 0x06000558 RID: 1368 RVA: 0x0002D958 File Offset: 0x0002BB58
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

	// Token: 0x0400075B RID: 1883
	[ChunkLookup("document")]
	public string[] choices;

	// Token: 0x0400075C RID: 1884
	public bool saveChoice;

	// Token: 0x0400075D RID: 1885
	[ConditionalHide("saveChoice", true)]
	public string saveChoiceID;
}
