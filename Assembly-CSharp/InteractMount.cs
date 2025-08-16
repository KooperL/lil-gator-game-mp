using System;
using UnityEngine;

public class InteractMount : MonoBehaviour, Interaction, InteractionHighlight
{
	// Token: 0x060007DB RID: 2011 RVA: 0x00007CD9 File Offset: 0x00005ED9
	private void OnValidate()
	{
		if (this.highlight != null)
		{
			this.highlightedRenderers = new Renderer[1];
			this.highlightedRenderers[0] = this.highlight;
			this.highlight = null;
		}
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x00007D0A File Offset: 0x00005F0A
	public Renderer[] GetHighlightedRenderer()
	{
		if (this.mount.isFilled)
		{
			return null;
		}
		return this.highlightedRenderers;
	}

	// Token: 0x060007DD RID: 2013 RVA: 0x00007D21 File Offset: 0x00005F21
	public void Interact()
	{
		if (this.mount.isFilled)
		{
			return;
		}
		this.mount.InviteActor(Player.animator.GetComponent<DialogueActor>(), false, true);
	}

	public ActorMount mount;

	[HideInInspector]
	public Renderer highlight;

	public Renderer[] highlightedRenderers;
}
