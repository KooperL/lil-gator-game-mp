using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000294 RID: 660
public class ProofReadDialogue : MonoBehaviour, Interaction
{
	// Token: 0x06000CE8 RID: 3304 RVA: 0x0000C004 File Offset: 0x0000A204
	public void Interact()
	{
		base.StartCoroutine(this.ProofReadDocuments());
	}

	// Token: 0x06000CE9 RID: 3305 RVA: 0x0000C013 File Offset: 0x0000A213
	private IEnumerator ProofReadDocuments()
	{
		Game.DialogueDepth++;
		this.setPlayer.SetActive(true);
		yield return null;
		this.uiRoot.SetActive(true);
		foreach (MultilingualTextDocument multilingualTextDocument in this.documents)
		{
			this.documentTitle.text = multilingualTextDocument.name;
			foreach (DialogueChunk dialogueChunk in multilingualTextDocument.chunks)
			{
				this.chunkName.text = dialogueChunk.name;
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(dialogueChunk, this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, true, false));
				DialogueManager.d.cue = false;
				DialogueActor.playerActor.ClearEmote(true, false);
				DialogueActor.playerActor.SetStateAndPosition(0, 0, false, false);
				foreach (DialogueActor dialogueActor in this.actors)
				{
					DialogueActor.playerActor.ClearEmote(true, false);
					DialogueActor.playerActor.SetStateAndPosition(0, 0, false, false);
				}
			}
			DialogueChunk[] array2 = null;
		}
		MultilingualTextDocument[] array = null;
		this.uiRoot.SetActive(false);
		this.setPlayer.SetActive(false);
		Game.DialogueDepth--;
		yield break;
	}

	// Token: 0x04001145 RID: 4421
	public MultilingualTextDocument[] documents;

	// Token: 0x04001146 RID: 4422
	public DialogueActor[] actors;

	// Token: 0x04001147 RID: 4423
	public GameObject setPlayer;

	// Token: 0x04001148 RID: 4424
	[Header("UI")]
	public GameObject uiRoot;

	// Token: 0x04001149 RID: 4425
	public Text documentTitle;

	// Token: 0x0400114A RID: 4426
	public Text chunkName;
}
