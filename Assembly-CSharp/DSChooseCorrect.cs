using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000CD RID: 205
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

	// Token: 0x0400062F RID: 1583
	public MultilingualTextDocument document;

	// Token: 0x04000630 RID: 1584
	public bool showPrompt = true;

	// Token: 0x04000631 RID: 1585
	public bool showPromptAfterWrongChoices = true;

	// Token: 0x04000632 RID: 1586
	public bool showCorrectDialogue;

	// Token: 0x04000633 RID: 1587
	[ChunkLookup("document")]
	public string prompt;

	// Token: 0x04000634 RID: 1588
	public DSChooseCorrect.Choice[] choices;

	// Token: 0x04000635 RID: 1589
	public DialogueActor[] actors;

	// Token: 0x04000636 RID: 1590
	public UnityEvent onChooseCorrectFirst;

	// Token: 0x04000637 RID: 1591
	public UnityEvent onChooseCorrectNotFirst;

	// Token: 0x04000638 RID: 1592
	private bool hasChosenIncorrectly;

	// Token: 0x020003A0 RID: 928
	[Serializable]
	public struct Choice
	{
		// Token: 0x04001B35 RID: 6965
		public bool isCorrect;

		// Token: 0x04001B36 RID: 6966
		[ChunkLookup("document")]
		public string dialogue;

		// Token: 0x04001B37 RID: 6967
		public bool isCancel;
	}
}
