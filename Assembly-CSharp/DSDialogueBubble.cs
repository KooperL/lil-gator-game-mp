using System;
using UnityEngine;

// Token: 0x02000121 RID: 289
[AddComponentMenu("Dialogue Sequence/Dialogue (Bubble)")]
public class DSDialogueBubble : DialogueSequence
{
	// Token: 0x06000575 RID: 1397 RVA: 0x0002E200 File Offset: 0x0002C400
	public override YieldInstruction Run()
	{
		if (this.document != null)
		{
			return DialogueManager.d.Bubble(this.document.FetchChunk(this.dialogue), this.actors, this.delay, this.isImportant, this.hasInput, this.canInterrupt);
		}
		return DialogueManager.d.Bubble(this.dialogue, this.actors, this.delay, this.isImportant, this.hasInput, this.canInterrupt);
	}

	// Token: 0x0400077E RID: 1918
	public MultilingualTextDocument document;

	// Token: 0x0400077F RID: 1919
	[ChunkLookup("document")]
	public string dialogue;

	// Token: 0x04000780 RID: 1920
	public DialogueActor[] actors;

	// Token: 0x04000781 RID: 1921
	public float delay;

	// Token: 0x04000782 RID: 1922
	public bool isImportant = true;

	// Token: 0x04000783 RID: 1923
	public bool hasInput;

	// Token: 0x04000784 RID: 1924
	public bool canInterrupt = true;
}
