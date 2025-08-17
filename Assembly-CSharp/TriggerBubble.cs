using System;
using UnityEngine;

public class TriggerBubble : MonoBehaviour
{
	// Token: 0x06000554 RID: 1364 RVA: 0x0002E7B4 File Offset: 0x0002C9B4
	public void Interact()
	{
		if (this.document != null)
		{
			DialogueManager.d.Bubble(this.document.FetchChunk(this.text), this.actors, this.delay, this.isImportant, this.hasInput, this.canInterrupt);
			return;
		}
		DialogueManager.d.Bubble(this.text, this.actors, this.delay, this.isImportant, this.hasInput, this.canInterrupt);
	}

	public DialogueActor[] actors;

	public MultilingualTextDocument document;

	[ChunkLookup("document")]
	public string text;

	public float delay;

	public bool isImportant;

	public bool hasInput;

	public bool canInterrupt;
}
