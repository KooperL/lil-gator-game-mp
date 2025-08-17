using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ChoiceQuest : MonoBehaviour
{
	// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x0000C5D7 File Offset: 0x0000A7D7
	// (set) Token: 0x06000DA3 RID: 3491 RVA: 0x0000C5EA File Offset: 0x0000A7EA
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

	// Token: 0x06000DA4 RID: 3492 RVA: 0x0000C5FD File Offset: 0x0000A7FD
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000DA5 RID: 3493 RVA: 0x0004B4F0 File Offset: 0x000496F0
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

	// Token: 0x06000DA6 RID: 3494 RVA: 0x0000C605 File Offset: 0x0000A805
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000DA7 RID: 3495 RVA: 0x0000C614 File Offset: 0x0000A814
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

	public string id;

	public DialogueActor[] actors;

	public ChoiceQuest.Prompt[] prompts;

	public string beforeState;

	public int minCorrect = 1;

	public string rewardText;

	public UnityEvent onReward;

	public string afterText;

	public string afterState;

	public UnityEvent onAfter;

	public GameObject crier;

	[Serializable]
	public struct Prompt
	{
		public string text;

		public ChoiceQuest.Prompt.Choice[] choices;

		[Serializable]
		public struct Choice
		{
			public bool isCorrect;

			public string response;
		}
	}
}
