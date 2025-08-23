using System;

public class InteractTalkToConditional : InteractTalkTo
{
	// Token: 0x06000806 RID: 2054 RVA: 0x00036E74 File Offset: 0x00035074
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
