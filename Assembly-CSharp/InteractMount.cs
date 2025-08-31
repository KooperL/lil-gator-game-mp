using System;
using UnityEngine;

public class InteractMount : MonoBehaviour, Interaction, InteractionHighlight
{
	// Token: 0x06000676 RID: 1654 RVA: 0x00021441 File Offset: 0x0001F641
	private void OnValidate()
	{
		if (this.highlight != null)
		{
			this.highlightedRenderers = new Renderer[1];
			this.highlightedRenderers[0] = this.highlight;
			this.highlight = null;
		}
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x00021472 File Offset: 0x0001F672
	public Renderer[] GetHighlightedRenderer()
	{
		if (this.mount.isFilled)
		{
			return null;
		}
		return this.highlightedRenderers;
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x00021489 File Offset: 0x0001F689
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
