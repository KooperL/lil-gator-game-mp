using System;
using UnityEngine;

// Token: 0x020000C6 RID: 198
public class TriggerBubble : MonoBehaviour
{
	// Token: 0x06000450 RID: 1104 RVA: 0x00018AF8 File Offset: 0x00016CF8
	public void Interact()
	{
		if (this.document != null)
		{
			DialogueManager.d.Bubble(this.document.FetchChunk(this.text), this.actors, this.delay, this.isImportant, this.hasInput, this.canInterrupt);
			return;
		}
		DialogueManager.d.Bubble(this.text, this.actors, this.delay, this.isImportant, this.hasInput, this.canInterrupt);
	}

	// Token: 0x04000604 RID: 1540
	public DialogueActor[] actors;

	// Token: 0x04000605 RID: 1541
	public MultilingualTextDocument document;

	// Token: 0x04000606 RID: 1542
	[ChunkLookup("document")]
	public string text;

	// Token: 0x04000607 RID: 1543
	public float delay;

	// Token: 0x04000608 RID: 1544
	public bool isImportant;

	// Token: 0x04000609 RID: 1545
	public bool hasInput;

	// Token: 0x0400060A RID: 1546
	public bool canInterrupt;
}
