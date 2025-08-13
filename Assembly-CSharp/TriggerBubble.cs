using System;
using UnityEngine;

// Token: 0x0200010C RID: 268
public class TriggerBubble : MonoBehaviour
{
	// Token: 0x06000527 RID: 1319 RVA: 0x0002D1C8 File Offset: 0x0002B3C8
	public void Interact()
	{
		if (this.document != null)
		{
			DialogueManager.d.Bubble(this.document.FetchChunk(this.text), this.actors, this.delay, this.isImportant, this.hasInput, this.canInterrupt);
			return;
		}
		DialogueManager.d.Bubble(this.text, this.actors, this.delay, this.isImportant, this.hasInput, this.canInterrupt);
	}

	// Token: 0x0400071D RID: 1821
	public DialogueActor[] actors;

	// Token: 0x0400071E RID: 1822
	public MultilingualTextDocument document;

	// Token: 0x0400071F RID: 1823
	[ChunkLookup("document")]
	public string text;

	// Token: 0x04000720 RID: 1824
	public float delay;

	// Token: 0x04000721 RID: 1825
	public bool isImportant;

	// Token: 0x04000722 RID: 1826
	public bool hasInput;

	// Token: 0x04000723 RID: 1827
	public bool canInterrupt;
}
