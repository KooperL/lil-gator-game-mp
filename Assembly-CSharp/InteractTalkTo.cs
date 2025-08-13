using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200013F RID: 319
[AddComponentMenu("Interaction/Talk To NPC")]
public class InteractTalkTo : MonoBehaviour, Interaction
{
	// Token: 0x06000685 RID: 1669 RVA: 0x000215F0 File Offset: 0x0001F7F0
	private void OnValidate()
	{
		if (this.actors == null || this.actors.Length == 0)
		{
			DialogueActor dialogueActor = base.GetComponent<DialogueActor>();
			if (dialogueActor == null)
			{
				dialogueActor = base.GetComponentInParent<DialogueActor>();
			}
			if (dialogueActor != null)
			{
				this.actors = new DialogueActor[] { dialogueActor };
			}
		}
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x0002163E File Offset: 0x0001F83E
	[ContextMenu("Move to interaction")]
	public void MoveToInteraction()
	{
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x00021640 File Offset: 0x0001F840
	public void Start()
	{
		if (this.saveDialogueIndex)
		{
			this.dialogueIndex = GameData.g.ReadInt(this.dialogueIndexKey, 0);
		}
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x00021661 File Offset: 0x0001F861
	[ContextMenu("Fix Actors")]
	public void FixActors()
	{
		this.actors = new DialogueActor[1];
		this.actors[0] = base.GetComponent<DialogueActor>();
		if (this.actors[0] == null)
		{
			this.actors[0] = base.GetComponentInParent<DialogueActor>();
		}
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x0002169B File Offset: 0x0001F89B
	public void Interact()
	{
		CoroutineUtil.Start(this.RunDialogueChunk(this.GetDialogue()));
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x000216AF File Offset: 0x0001F8AF
	private IEnumerator RunDialogueChunk(string dialogue)
	{
		if (this.fadeBefore)
		{
			if (!this.isBubble)
			{
				Game.DialogueDepth++;
			}
			yield return Blackout.FadeIn();
			if (!this.isBubble)
			{
				Game.DialogueDepth--;
			}
		}
		GameObject[] array = this.objectsWhileTalking;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(true);
		}
		if (this.fadeBefore)
		{
			Blackout.FadeOut();
		}
		if (this.document == null)
		{
			yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(dialogue, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
		}
		else if (this.isBubble)
		{
			yield return DialogueManager.d.Bubble(this.document.FetchChunk(dialogue), this.actors, 0f, true, this.hasInput, true);
		}
		else
		{
			yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(dialogue), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
		}
		if (this.choiceResults != null && this.choiceResults.Length != 0 && DialogueManager.optionChosen > -1 && DialogueManager.optionChosen < this.choiceResults.Length)
		{
			InteractTalkTo.ChoiceResult choiceResult = this.choiceResults[DialogueManager.optionChosen];
			if (choiceResult.choiceEvent != null)
			{
				choiceResult.choiceEvent.Invoke();
			}
			if (!string.IsNullOrEmpty(choiceResult.dialogue))
			{
				if (this.document == null)
				{
					yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(choiceResult.dialogue, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
				}
				else
				{
					yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(choiceResult.dialogue), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
				}
			}
		}
		if (this.fadeAfter)
		{
			if (!this.isBubble)
			{
				Game.DialogueDepth++;
			}
			yield return Blackout.FadeIn();
			if (!this.isBubble)
			{
				Game.DialogueDepth--;
			}
		}
		array = this.objectsWhileTalking;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(false);
		}
		if (this.after != null)
		{
			this.after.Invoke();
		}
		Blackout.FadeOut();
		yield break;
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x000216C8 File Offset: 0x0001F8C8
	protected virtual string GetDialogue()
	{
		string text = this.dialogues[this.dialogueIndex];
		this.dialogueIndex++;
		if (this.dialogueIndex >= this.dialogues.Length)
		{
			if (this.loopDialogue)
			{
				this.dialogueIndex = 0;
			}
			else
			{
				this.dialogueIndex = this.dialogues.Length - 1;
			}
		}
		if (this.saveDialogueIndex)
		{
			GameData.g.Write(this.dialogueIndexKey, this.dialogueIndex);
		}
		return text;
	}

	// Token: 0x040008CC RID: 2252
	public DialogueActor[] actors;

	// Token: 0x040008CD RID: 2253
	public MultilingualTextDocument document;

	// Token: 0x040008CE RID: 2254
	[ChunkLookup("document")]
	public string[] dialogues;

	// Token: 0x040008CF RID: 2255
	[ReadOnly]
	public int dialogueIndex;

	// Token: 0x040008D0 RID: 2256
	public bool loopDialogue = true;

	// Token: 0x040008D1 RID: 2257
	public bool saveDialogueIndex;

	// Token: 0x040008D2 RID: 2258
	public string dialogueIndexKey;

	// Token: 0x040008D3 RID: 2259
	public bool isBubble;

	// Token: 0x040008D4 RID: 2260
	public bool hasInput = true;

	// Token: 0x040008D5 RID: 2261
	public GameObject[] objectsWhileTalking;

	// Token: 0x040008D6 RID: 2262
	public UnityEvent after;

	// Token: 0x040008D7 RID: 2263
	public InteractTalkTo.ChoiceResult[] choiceResults;

	// Token: 0x040008D8 RID: 2264
	public bool fadeBefore;

	// Token: 0x040008D9 RID: 2265
	public bool fadeAfter;

	// Token: 0x020003B7 RID: 951
	[Serializable]
	public struct ChoiceResult
	{
		// Token: 0x04001B96 RID: 7062
		public UnityEvent choiceEvent;

		// Token: 0x04001B97 RID: 7063
		[ChunkLookup("document")]
		public string dialogue;
	}
}
