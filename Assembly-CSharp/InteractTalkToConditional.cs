using System;

// Token: 0x020001A2 RID: 418
public class InteractTalkToConditional : InteractTalkTo
{
	// Token: 0x060007C5 RID: 1989 RVA: 0x00035244 File Offset: 0x00033444
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

	// Token: 0x04000A5B RID: 2651
	public int conditionalDialogueMinIndex = 1;

	// Token: 0x04000A5C RID: 2652
	public InteractTalkToConditional.ConditionalDialogue[] conditionalDialogues;

	// Token: 0x020001A3 RID: 419
	[Serializable]
	public struct ConditionalDialogue
	{
		// Token: 0x04000A5D RID: 2653
		[ChunkLookup("document")]
		public string dialogue;

		// Token: 0x04000A5E RID: 2654
		public string condition;

		// Token: 0x04000A5F RID: 2655
		public string saveID;
	}
}
