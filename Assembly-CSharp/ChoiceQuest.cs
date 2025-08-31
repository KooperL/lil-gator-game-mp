using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ChoiceQuest : MonoBehaviour
{
	// (get) Token: 0x06000B5B RID: 2907 RVA: 0x00038240 File Offset: 0x00036440
	// (set) Token: 0x06000B5C RID: 2908 RVA: 0x00038253 File Offset: 0x00036453
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

	// Token: 0x06000B5D RID: 2909 RVA: 0x00038266 File Offset: 0x00036466
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000B5E RID: 2910 RVA: 0x00038270 File Offset: 0x00036470
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

	// Token: 0x06000B5F RID: 2911 RVA: 0x000382F4 File Offset: 0x000364F4
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x00038303 File Offset: 0x00036503
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
