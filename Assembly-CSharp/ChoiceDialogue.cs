using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ChoiceDialogue : MonoBehaviour
{
	// Token: 0x06000D99 RID: 3481 RVA: 0x0000C58D File Offset: 0x0000A78D
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000D9A RID: 3482 RVA: 0x0000C59C File Offset: 0x0000A79C
	private IEnumerator RunConversation()
	{
		foreach (ChoiceDialogue.Prompt prompt in this.prompts)
		{
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk(prompt.text, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			ChoiceDialogue.Prompt.Choice choice = prompt.choices[DialogueManager.optionChosen];
			if (!string.IsNullOrEmpty(choice.response))
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(choice.response, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			choice.onChosen.Invoke();
			choice = default(ChoiceDialogue.Prompt.Choice);
			prompt = default(ChoiceDialogue.Prompt);
		}
		ChoiceDialogue.Prompt[] array = null;
		yield break;
	}

	public DialogueActor[] actors;

	public ChoiceDialogue.Prompt[] prompts;

	[Serializable]
	public struct Prompt
	{
		public string text;

		public ChoiceDialogue.Prompt.Choice[] choices;

		[Serializable]
		public struct Choice
		{
			public string response;

			public UnityEvent onChosen;
		}
	}
}
