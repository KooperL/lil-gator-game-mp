using System;
using UnityEngine;

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

	[ChunkLookup("document")]
	public string[] choices;

	public bool saveChoice;

	[ConditionalHide("saveChoice", true)]
	public string saveChoiceID;
}
