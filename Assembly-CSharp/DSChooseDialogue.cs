using System;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/Choose Dialogue")]
public class DSChooseDialogue : DSDialogue
{
	// Token: 0x06000592 RID: 1426 RVA: 0x0002F030 File Offset: 0x0002D230
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
