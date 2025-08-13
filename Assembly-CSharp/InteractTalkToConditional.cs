using System;

// Token: 0x02000140 RID: 320
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

	// Token: 0x040008DA RID: 2266
	public int conditionalDialogueMinIndex = 1;

	// Token: 0x040008DB RID: 2267
	public InteractTalkToConditional.ConditionalDialogue[] conditionalDialogues;

	// Token: 0x020003B9 RID: 953
	[Serializable]
	public struct ConditionalDialogue
	{
		// Token: 0x04001B9C RID: 7068
		[ChunkLookup("document")]
		public string dialogue;

		// Token: 0x04001B9D RID: 7069
		public string condition;

		// Token: 0x04001B9E RID: 7070
		public string saveID;
	}
}
