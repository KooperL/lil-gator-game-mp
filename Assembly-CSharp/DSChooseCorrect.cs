using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000115 RID: 277
[AddComponentMenu("Dialogue Sequence/Choose Correct Choice")]
public class DSChooseCorrect : DialogueSequence
{
	// Token: 0x0600054F RID: 1359 RVA: 0x00005D4E File Offset: 0x00003F4E
	public override YieldInstruction Run()
	{
		return CoroutineUtil.Start(this.RunChoiceLoop());
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x00005D5B File Offset: 0x00003F5B
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

	// Token: 0x0400074A RID: 1866
	public MultilingualTextDocument document;

	// Token: 0x0400074B RID: 1867
	public bool showPrompt = true;

	// Token: 0x0400074C RID: 1868
	public bool showPromptAfterWrongChoices = true;

	// Token: 0x0400074D RID: 1869
	public bool showCorrectDialogue;

	// Token: 0x0400074E RID: 1870
	[ChunkLookup("document")]
	public string prompt;

	// Token: 0x0400074F RID: 1871
	public DSChooseCorrect.Choice[] choices;

	// Token: 0x04000750 RID: 1872
	public DialogueActor[] actors;

	// Token: 0x04000751 RID: 1873
	public UnityEvent onChooseCorrectFirst;

	// Token: 0x04000752 RID: 1874
	public UnityEvent onChooseCorrectNotFirst;

	// Token: 0x04000753 RID: 1875
	private bool hasChosenIncorrectly;

	// Token: 0x02000116 RID: 278
	[Serializable]
	public struct Choice
	{
		// Token: 0x04000754 RID: 1876
		public bool isCorrect;

		// Token: 0x04000755 RID: 1877
		[ChunkLookup("document")]
		public string dialogue;

		// Token: 0x04000756 RID: 1878
		public bool isCancel;
	}
}
