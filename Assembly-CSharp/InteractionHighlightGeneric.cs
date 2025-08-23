using System;
using UnityEngine;

public class InteractionHighlightGeneric : MonoBehaviour, InteractionHighlight
{
	// Token: 0x0600080B RID: 2059 RVA: 0x00036F20 File Offset: 0x00035120
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

	// Token: 0x0600080C RID: 2060 RVA: 0x00007EBE File Offset: 0x000060BE
	private void Start()
	{
		if (base.GetComponent<Interaction>() == null)
		{
			base.enabled = false;
		}
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x00007ECF File Offset: 0x000060CF
	public Renderer[] GetHighlightedRenderer()
	{
		return this.highlightedRenderers;
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x00007ED7 File Offset: 0x000060D7
	public void RenderersChanged()
	{
		if (PlayerInteract.currentHighlight == this)
		{
			PlayerInteract.CurrentHighlightChanged();
		}
	}

	[HideInInspector]
	public Renderer highlightedRenderer;

	public Renderer[] highlightedRenderers;
}
