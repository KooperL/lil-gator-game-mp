using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProofReadDialogue : MonoBehaviour, Interaction
{
	// Token: 0x06000B23 RID: 2851 RVA: 0x00037B3B File Offset: 0x00035D3B
	public void Interact()
	{
		base.StartCoroutine(this.ProofReadDocuments());
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x00037B4A File Offset: 0x00035D4A
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

	public MultilingualTextDocument[] documents;

	public DialogueActor[] actors;

	public GameObject setPlayer;

	[Header("UI")]
	public GameObject uiRoot;

	public Text documentTitle;

	public Text chunkName;
}
