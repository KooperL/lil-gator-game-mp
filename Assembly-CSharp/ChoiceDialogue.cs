using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020002AA RID: 682
public class ChoiceDialogue : MonoBehaviour
{
	// Token: 0x06000D4D RID: 3405 RVA: 0x0000C29A File Offset: 0x0000A49A
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x0000C2A9 File Offset: 0x0000A4A9
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

	// Token: 0x0400118F RID: 4495
	public DialogueActor[] actors;

	// Token: 0x04001190 RID: 4496
	public ChoiceDialogue.Prompt[] prompts;

	// Token: 0x020002AB RID: 683
	[Serializable]
	public struct Prompt
	{
		// Token: 0x04001191 RID: 4497
		public string text;

		// Token: 0x04001192 RID: 4498
		public ChoiceDialogue.Prompt.Choice[] choices;

		// Token: 0x020002AC RID: 684
		[Serializable]
		public struct Choice
		{
			// Token: 0x04001193 RID: 4499
			public string response;

			// Token: 0x04001194 RID: 4500
			public UnityEvent onChosen;
		}
	}
}
