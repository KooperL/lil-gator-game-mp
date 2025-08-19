using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InteractDialogueGeneric : MonoBehaviour, Interaction
{
	// Token: 0x060004B9 RID: 1209 RVA: 0x0002C414 File Offset: 0x0002A614
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

	// Token: 0x060004BA RID: 1210 RVA: 0x00005744 File Offset: 0x00003944
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x00005753 File Offset: 0x00003953
	private IEnumerator RunConversation()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.dialogueChunkName, this.actors, this.background, true));
		this.afterDialogue.Invoke();
		yield break;
	}

	public DialogueActor[] actors;

	public string dialogueChunkName;

	public DialogueManager.DialogueBoxBackground background;

	public UnityEvent afterDialogue;
}
