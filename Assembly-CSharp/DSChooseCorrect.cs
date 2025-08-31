using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Dialogue Sequence/Choose Correct Choice")]
public class DSChooseCorrect : DialogueSequence
{
	// Token: 0x0600046D RID: 1133 RVA: 0x00018EBA File Offset: 0x000170BA
	public override YieldInstruction Run()
	{
		return CoroutineUtil.Start(this.RunChoiceLoop());
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00018EC7 File Offset: 0x000170C7
	private IEnumerator RunChoiceLoop()
	{
		if (this.showPrompt)
		{
			yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(this.prompt), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
		}
		DSChooseCorrect.Choice choice = this.choices[DialogueManager.optionChosen];
		if (choice.isCorrect)
		{
			if (this.showCorrectDialogue)
			{
				yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(choice.dialogue), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
			}
			if (this.hasChosenIncorrectly)
			{
				this.onChooseCorrectNotFirst.Invoke();
			}
			else
			{
				this.onChooseCorrectFirst.Invoke();
			}
			yield break;
		}
		if (choice.isCancel)
		{
			yield break;
		}
		this.hasChosenIncorrectly = true;
		while (!choice.isCorrect)
		{
			if (!string.IsNullOrEmpty(choice.dialogue))
			{
				yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(choice.dialogue), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
			}
			if (choice.isCancel)
			{
				yield break;
			}
			if (this.showPromptAfterWrongChoices)
			{
				yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(this.prompt), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
			}
			choice = this.choices[DialogueManager.optionChosen];
		}
		if (choice.isCorrect)
		{
			if (this.showCorrectDialogue)
			{
				yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(choice.dialogue), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
			}
			this.onChooseCorrectNotFirst.Invoke();
		}
		yield break;
	}

	public MultilingualTextDocument document;

	public bool showPrompt = true;

	public bool showPromptAfterWrongChoices = true;

	public bool showCorrectDialogue;

	[ChunkLookup("document")]
	public string prompt;

	public DSChooseCorrect.Choice[] choices;

	public DialogueActor[] actors;

	public UnityEvent onChooseCorrectFirst;

	public UnityEvent onChooseCorrectNotFirst;

	private bool hasChosenIncorrectly;

	[Serializable]
	public struct Choice
	{
		public bool isCorrect;

		[ChunkLookup("document")]
		public string dialogue;

		public bool isCancel;
	}
}
