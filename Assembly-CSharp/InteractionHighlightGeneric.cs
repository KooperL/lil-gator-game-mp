using System;
using UnityEngine;

// Token: 0x02000142 RID: 322
public class InteractionHighlightGeneric : MonoBehaviour, InteractionHighlight
{
	// Token: 0x06000692 RID: 1682 RVA: 0x0002183C File Offset: 0x0001FA3C
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

	// Token: 0x06000693 RID: 1683 RVA: 0x000218C8 File Offset: 0x0001FAC8
	private void Start()
	{
		if (base.GetComponent<Interaction>() == null)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000694 RID: 1684 RVA: 0x000218D9 File Offset: 0x0001FAD9
	public Renderer[] GetHighlightedRenderer()
	{
		return this.highlightedRenderers;
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x000218E1 File Offset: 0x0001FAE1
	public void RenderersChanged()
	{
		if (PlayerInteract.currentHighlight == this)
		{
			PlayerInteract.CurrentHighlightChanged();
		}
	}

	// Token: 0x040008DE RID: 2270
	[HideInInspector]
	public Renderer highlightedRenderer;

	// Token: 0x040008DF RID: 2271
	public Renderer[] highlightedRenderers;
}
