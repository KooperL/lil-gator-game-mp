using System;

public class InteractTalkToConditional : InteractTalkTo
{
	// Token: 0x0600068D RID: 1677 RVA: 0x00021758 File Offset: 0x0001F958
	protected override string GetDialogue()
	{
		if (this.dialogueIndex >= this.conditionalDialogueMinIndex)
		{
			foreach (InteractTalkToConditional.ConditionalDialogue conditionalDialogue in this.conditionalDialogues)
			{
				if (!GameData.g.ReadBool(conditionalDialogue.saveID, false) && GameData.g.ReadBool(conditionalDialogue.condition, false))
				{
					GameData.g.Write(conditionalDialogue.saveID, true);
					return conditionalDialogue.dialogue;
				}
			}
		}
		else
		{
			this.dialogueIndex++;
		}
		if (this.saveDialogueIndex)
		{
			GameData.g.Write(this.dialogueIndexKey, this.dialogueIndex);
		}
		return this.dialogues[0];
	}

	public int conditionalDialogueMinIndex = 1;

	public InteractTalkToConditional.ConditionalDialogue[] conditionalDialogues;

	[Serializable]
	public struct ConditionalDialogue
	{
		[ChunkLookup("document")]
		public string dialogue;

		public string condition;

		public string saveID;
	}
}
