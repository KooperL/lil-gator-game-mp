using System;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/Dialogue (Bubble)")]
public class DSDialogueBubble : DialogueSequence
{
	// Token: 0x060005AF RID: 1455 RVA: 0x0002F8D8 File Offset: 0x0002DAD8
	public override YieldInstruction Run()
	{
		if (this.document != null)
		{
			return DialogueManager.d.Bubble(this.document.FetchChunk(this.dialogue), this.actors, this.delay, this.isImportant, this.hasInput, this.canInterrupt);
		}
		return DialogueManager.d.Bubble(this.dialogue, this.actors, this.delay, this.isImportant, this.hasInput, this.canInterrupt);
	}

	public MultilingualTextDocument document;

	[ChunkLookup("document")]
	public string dialogue;

	public DialogueActor[] actors;

	public float delay;

	public bool isImportant = true;

	public bool hasInput;

	public bool canInterrupt = true;
}
