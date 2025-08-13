using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200019F RID: 415
[AddComponentMenu("Interaction/Talk To NPC")]
public class InteractTalkTo : MonoBehaviour, Interaction
{
	// Token: 0x060007B7 RID: 1975 RVA: 0x00034E70 File Offset: 0x00033070
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

	// Token: 0x060007B8 RID: 1976 RVA: 0x00002229 File Offset: 0x00000429
	[ContextMenu("Move to interaction")]
	public void MoveToInteraction()
	{
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x00007ACF File Offset: 0x00005CCF
	public void Start()
	{
		if (this.saveDialogueIndex)
		{
			this.dialogueIndex = GameData.g.ReadInt(this.dialogueIndexKey, 0);
		}
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x00007AF0 File Offset: 0x00005CF0
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

	// Token: 0x060007BB RID: 1979 RVA: 0x00007B2A File Offset: 0x00005D2A
	public void Interact()
	{
		CoroutineUtil.Start(this.RunDialogueChunk(this.GetDialogue()));
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x00007B3E File Offset: 0x00005D3E
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

	// Token: 0x060007BD RID: 1981 RVA: 0x00034EC0 File Offset: 0x000330C0
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

	// Token: 0x04000A47 RID: 2631
	public DialogueActor[] actors;

	// Token: 0x04000A48 RID: 2632
	public MultilingualTextDocument document;

	// Token: 0x04000A49 RID: 2633
	[ChunkLookup("document")]
	public string[] dialogues;

	// Token: 0x04000A4A RID: 2634
	[ReadOnly]
	public int dialogueIndex;

	// Token: 0x04000A4B RID: 2635
	public bool loopDialogue = true;

	// Token: 0x04000A4C RID: 2636
	public bool saveDialogueIndex;

	// Token: 0x04000A4D RID: 2637
	public string dialogueIndexKey;

	// Token: 0x04000A4E RID: 2638
	public bool isBubble;

	// Token: 0x04000A4F RID: 2639
	public bool hasInput = true;

	// Token: 0x04000A50 RID: 2640
	public GameObject[] objectsWhileTalking;

	// Token: 0x04000A51 RID: 2641
	public UnityEvent after;

	// Token: 0x04000A52 RID: 2642
	public InteractTalkTo.ChoiceResult[] choiceResults;

	// Token: 0x04000A53 RID: 2643
	public bool fadeBefore;

	// Token: 0x04000A54 RID: 2644
	public bool fadeAfter;

	// Token: 0x020001A0 RID: 416
	[Serializable]
	public struct ChoiceResult
	{
		// Token: 0x04000A55 RID: 2645
		public UnityEvent choiceEvent;

		// Token: 0x04000A56 RID: 2646
		[ChunkLookup("document")]
		public string dialogue;
	}
}
