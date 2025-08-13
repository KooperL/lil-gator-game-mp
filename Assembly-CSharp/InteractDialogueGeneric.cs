using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000F2 RID: 242
public class InteractDialogueGeneric : MonoBehaviour, Interaction
{
	// Token: 0x06000492 RID: 1170 RVA: 0x0002B2E8 File Offset: 0x000294E8
	private void OnValidate()
	{
		if (this.actors == null || this.actors.Length == 0)
		{
			DialogueActor component = base.GetComponent<DialogueActor>();
			if (component != null)
			{
				this.actors = new DialogueActor[] { component };
			}
		}
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x00005511 File Offset: 0x00003711
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x00005520 File Offset: 0x00003720
	private IEnumerator RunConversation()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.dialogueChunkName, this.actors, this.background, true));
		this.afterDialogue.Invoke();
		yield break;
	}

	// Token: 0x0400067C RID: 1660
	public DialogueActor[] actors;

	// Token: 0x0400067D RID: 1661
	public string dialogueChunkName;

	// Token: 0x0400067E RID: 1662
	public DialogueManager.DialogueBoxBackground background;

	// Token: 0x0400067F RID: 1663
	public UnityEvent afterDialogue;
}
