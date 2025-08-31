using System;
using UnityEngine;

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

	public MultilingualTextDocument document;

	[ChunkLookup("document")]
	public string dialogue;

	public DialogueActor[] actors;

	public float delay;

	public bool isImportant = true;

	public bool hasInput;

	public bool canInterrupt = true;
}
