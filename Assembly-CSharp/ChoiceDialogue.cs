using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ChoiceDialogue : MonoBehaviour
{
	// Token: 0x06000B58 RID: 2904 RVA: 0x0003821A File Offset: 0x0003641A
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x00038229 File Offset: 0x00036429
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
