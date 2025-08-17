using System;
using UnityEngine;

public class InteractionGeneric : MonoBehaviour, Interaction, InteractionHighlight
{
	// Token: 0x06000807 RID: 2055 RVA: 0x00007E9F File Offset: 0x0000609F
	public Renderer[] GetHighlightedRenderer()
	{
		return new Renderer[] { this.highlightRenderer };
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x00007EB0 File Offset: 0x000060B0
	public void Interact()
	{
		this.interactionBehaviour.enabled = true;
	}

	public Renderer highlightRenderer;

	public MonoBehaviour interactionBehaviour;
}
