using System;
using UnityEngine;

// Token: 0x0200013B RID: 315
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

	// Token: 0x040008B7 RID: 2231
	public ActorMount mount;

	// Token: 0x040008B8 RID: 2232
	[HideInInspector]
	public Renderer highlight;

	// Token: 0x040008B9 RID: 2233
	public Renderer[] highlightedRenderers;
}
