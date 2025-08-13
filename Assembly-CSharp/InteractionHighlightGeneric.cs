using System;
using UnityEngine;

// Token: 0x020001A5 RID: 421
public class InteractionHighlightGeneric : MonoBehaviour, InteractionHighlight
{
	// Token: 0x060007CA RID: 1994 RVA: 0x000352F0 File Offset: 0x000334F0
	private void OnValidate()
	{
		if (this.highlightedRenderer != null)
		{
			this.highlightedRenderers = new Renderer[1];
			this.highlightedRenderers[0] = this.highlightedRenderer;
			this.highlightedRenderer = null;
		}
		if (this.highlightedRenderers == null)
		{
			this.highlightedRenderers = new Renderer[1];
		}
		if (this.highlightedRenderers[0] == null)
		{
			this.highlightedRenderers[0] = base.GetComponentInChildren<SkinnedMeshRenderer>();
		}
		if (this.highlightedRenderers[0] == null)
		{
			this.highlightedRenderers[0] = base.GetComponentInChildren<Renderer>();
		}
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x00007BAF File Offset: 0x00005DAF
	private void Start()
	{
		if (base.GetComponent<Interaction>() == null)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x00007BC0 File Offset: 0x00005DC0
	public Renderer[] GetHighlightedRenderer()
	{
		return this.highlightedRenderers;
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x00007BC8 File Offset: 0x00005DC8
	public void RenderersChanged()
	{
		if (PlayerInteract.currentHighlight == this)
		{
			PlayerInteract.CurrentHighlightChanged();
		}
	}

	// Token: 0x04000A62 RID: 2658
	[HideInInspector]
	public Renderer highlightedRenderer;

	// Token: 0x04000A63 RID: 2659
	public Renderer[] highlightedRenderers;
}
