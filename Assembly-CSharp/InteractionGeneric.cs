using System;
using UnityEngine;

public class InteractionGeneric : MonoBehaviour, Interaction, InteractionHighlight
{
	// Token: 0x0600068F RID: 1679 RVA: 0x00021813 File Offset: 0x0001FA13
	public Renderer[] GetHighlightedRenderer()
	{
		return new Renderer[] { this.highlightRenderer };
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x00021824 File Offset: 0x0001FA24
	public void Interact()
	{
		this.interactionBehaviour.enabled = true;
	}

	public Renderer highlightRenderer;

	public MonoBehaviour interactionBehaviour;
}
