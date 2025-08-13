using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000B4 RID: 180
public class InteractDialogueGeneric : MonoBehaviour, Interaction
{
	// Token: 0x060003E5 RID: 997 RVA: 0x00016F64 File Offset: 0x00015164
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

	// Token: 0x060003E6 RID: 998 RVA: 0x00016FA2 File Offset: 0x000151A2
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x00016FB1 File Offset: 0x000151B1
	private IEnumerator RunConversation()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.dialogueChunkName, this.actors, this.background, true));
		this.afterDialogue.Invoke();
		yield break;
	}

	// Token: 0x0400056D RID: 1389
	public DialogueActor[] actors;

	// Token: 0x0400056E RID: 1390
	public string dialogueChunkName;

	// Token: 0x0400056F RID: 1391
	public DialogueManager.DialogueBoxBackground background;

	// Token: 0x04000570 RID: 1392
	public UnityEvent afterDialogue;
}
