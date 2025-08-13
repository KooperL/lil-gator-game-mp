using System;
using UnityEngine;

// Token: 0x02000198 RID: 408
public class InteractMount : MonoBehaviour, Interaction, InteractionHighlight
{
	// Token: 0x0600079B RID: 1947 RVA: 0x000079DF File Offset: 0x00005BDF
	private void OnValidate()
	{
		if (this.highlight != null)
		{
			this.highlightedRenderers = new Renderer[1];
			this.highlightedRenderers[0] = this.highlight;
			this.highlight = null;
		}
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x00007A10 File Offset: 0x00005C10
	public Renderer[] GetHighlightedRenderer()
	{
		if (this.mount.isFilled)
		{
			return null;
		}
		return this.highlightedRenderers;
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x00007A27 File Offset: 0x00005C27
	public void Interact()
	{
		if (this.mount.isFilled)
		{
			return;
		}
		this.mount.InviteActor(Player.animator.GetComponent<DialogueActor>(), false, true);
	}

	// Token: 0x04000A27 RID: 2599
	public ActorMount mount;

	// Token: 0x04000A28 RID: 2600
	[HideInInspector]
	public Renderer highlight;

	// Token: 0x04000A29 RID: 2601
	public Renderer[] highlightedRenderers;
}
