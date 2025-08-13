using System;
using UnityEngine;

// Token: 0x020001A4 RID: 420
public class InteractionGeneric : MonoBehaviour, Interaction, InteractionHighlight
{
	// Token: 0x060007C7 RID: 1991 RVA: 0x00007B90 File Offset: 0x00005D90
	public Renderer[] GetHighlightedRenderer()
	{
		return new Renderer[] { this.highlightRenderer };
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x00007BA1 File Offset: 0x00005DA1
	public void Interact()
	{
		this.interactionBehaviour.enabled = true;
	}

	// Token: 0x04000A60 RID: 2656
	public Renderer highlightRenderer;

	// Token: 0x04000A61 RID: 2657
	public MonoBehaviour interactionBehaviour;
}
