using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200020F RID: 527
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

	// Token: 0x04000F0F RID: 3855
	public DialogueActor[] actors;

	// Token: 0x04000F10 RID: 3856
	public ChoiceDialogue.Prompt[] prompts;

	// Token: 0x020003F8 RID: 1016
	[Serializable]
	public struct Prompt
	{
		// Token: 0x04001CAC RID: 7340
		public string text;

		// Token: 0x04001CAD RID: 7341
		public ChoiceDialogue.Prompt.Choice[] choices;

		// Token: 0x020004BD RID: 1213
		[Serializable]
		public struct Choice
		{
			// Token: 0x04001FAA RID: 8106
			public string response;

			// Token: 0x04001FAB RID: 8107
			public UnityEvent onChosen;
		}
	}
}
