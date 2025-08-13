using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020002AE RID: 686
public class ChoiceQuest : MonoBehaviour
{
	// Token: 0x17000160 RID: 352
	// (get) Token: 0x06000D56 RID: 3414 RVA: 0x0000C2CF File Offset: 0x0000A4CF
	// (set) Token: 0x06000D57 RID: 3415 RVA: 0x0000C2E2 File Offset: 0x0000A4E2
	private bool State
	{
		get
		{
			return GameData.g.ReadBool(this.id, false);
		}
		set
		{
			GameData.g.Write(this.id, value);
		}
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x0000C2F5 File Offset: 0x0000A4F5
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x00049968 File Offset: 0x00047B68
	private void UpdateState()
	{
		if (this.crier != null)
		{
			this.crier.SetActive(!this.State);
		}
		foreach (DialogueActor dialogueActor in this.actors)
		{
			if (this.State)
			{
				if (!string.IsNullOrEmpty(this.afterState))
				{
					dialogueActor.SetStateString(this.afterState);
				}
			}
			else if (!string.IsNullOrEmpty(this.beforeState))
			{
				dialogueActor.SetStateString(this.beforeState);
			}
		}
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x0000C2FD File Offset: 0x0000A4FD
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x0000C30C File Offset: 0x0000A50C
	private IEnumerator RunConversation()
	{
		if (!this.State)
		{
			int correct = 0;
			foreach (ChoiceQuest.Prompt prompt in this.prompts)
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(prompt.text, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
				ChoiceQuest.Prompt.Choice choice = prompt.choices[DialogueManager.optionChosen];
				if (choice.isCorrect)
				{
					int num = correct;
					correct = num + 1;
				}
				if (!string.IsNullOrEmpty(choice.response))
				{
					yield return base.StartCoroutine(DialogueManager.d.LoadChunk(choice.response, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
				}
				prompt = default(ChoiceQuest.Prompt);
			}
			ChoiceQuest.Prompt[] array = null;
			if (correct >= this.minCorrect)
			{
				this.State = true;
				if (!string.IsNullOrEmpty(this.rewardText))
				{
					yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.rewardText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
				}
				this.onReward.Invoke();
				this.UpdateState();
			}
		}
		else
		{
			if (!string.IsNullOrEmpty(this.afterText))
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.afterText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			this.onAfter.Invoke();
		}
		yield break;
	}

	// Token: 0x0400119C RID: 4508
	public string id;

	// Token: 0x0400119D RID: 4509
	public DialogueActor[] actors;

	// Token: 0x0400119E RID: 4510
	public ChoiceQuest.Prompt[] prompts;

	// Token: 0x0400119F RID: 4511
	public string beforeState;

	// Token: 0x040011A0 RID: 4512
	public int minCorrect = 1;

	// Token: 0x040011A1 RID: 4513
	public string rewardText;

	// Token: 0x040011A2 RID: 4514
	public UnityEvent onReward;

	// Token: 0x040011A3 RID: 4515
	public string afterText;

	// Token: 0x040011A4 RID: 4516
	public string afterState;

	// Token: 0x040011A5 RID: 4517
	public UnityEvent onAfter;

	// Token: 0x040011A6 RID: 4518
	public GameObject crier;

	// Token: 0x020002AF RID: 687
	[Serializable]
	public struct Prompt
	{
		// Token: 0x040011A7 RID: 4519
		public string text;

		// Token: 0x040011A8 RID: 4520
		public ChoiceQuest.Prompt.Choice[] choices;

		// Token: 0x020002B0 RID: 688
		[Serializable]
		public struct Choice
		{
			// Token: 0x040011A9 RID: 4521
			public bool isCorrect;

			// Token: 0x040011AA RID: 4522
			public string response;
		}
	}
}
