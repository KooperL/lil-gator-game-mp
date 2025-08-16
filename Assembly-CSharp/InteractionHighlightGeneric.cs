using System;
using UnityEngine;

public class InteractionHighlightGeneric : MonoBehaviour, InteractionHighlight
{
	// Token: 0x0600080A RID: 2058 RVA: 0x00036A78 File Offset: 0x00034C78
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

	// Token: 0x0600080B RID: 2059 RVA: 0x00007EA9 File Offset: 0x000060A9
	private void Start()
	{
		if (base.GetComponent<Interaction>() == null)
		{
			base.enabled = false;
		}
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x00007EBA File Offset: 0x000060BA
	public Renderer[] GetHighlightedRenderer()
	{
		return this.highlightedRenderers;
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x00007EC2 File Offset: 0x000060C2
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
