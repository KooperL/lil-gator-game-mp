using System;
using UnityEngine;

// Token: 0x02000141 RID: 321
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

	// Token: 0x040008DC RID: 2268
	public Renderer highlightRenderer;

	// Token: 0x040008DD RID: 2269
	public MonoBehaviour interactionBehaviour;
}
