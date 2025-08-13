using System;
using UnityEngine;

// Token: 0x020000D5 RID: 213
[AddComponentMenu("Dialogue Sequence/Dialogue (Bubble)")]
public class DSDialogueBubble : DialogueSequence
{
	// Token: 0x06000484 RID: 1156 RVA: 0x000196B0 File Offset: 0x000178B0
	public override YieldInstruction Run()
	{
		if (this.document != null)
		{
			return DialogueManager.d.Bubble(this.document.FetchChunk(this.dialogue), this.actors, this.delay, this.isImportant, this.hasInput, this.canInterrupt);
		}
		return DialogueManager.d.Bubble(this.dialogue, this.actors, this.delay, this.isImportant, this.hasInput, this.canInterrupt);
	}

	// Token: 0x0400064D RID: 1613
	public MultilingualTextDocument document;

	// Token: 0x0400064E RID: 1614
	[ChunkLookup("document")]
	public string dialogue;

	// Token: 0x0400064F RID: 1615
	public DialogueActor[] actors;

	// Token: 0x04000650 RID: 1616
	public float delay;

	// Token: 0x04000651 RID: 1617
	public bool isImportant = true;

	// Token: 0x04000652 RID: 1618
	public bool hasInput;

	// Token: 0x04000653 RID: 1619
	public bool canInterrupt = true;
}
