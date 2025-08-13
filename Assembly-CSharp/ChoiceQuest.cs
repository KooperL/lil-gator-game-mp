using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000210 RID: 528
public class ChoiceQuest : MonoBehaviour
{
	// Token: 0x170000B8 RID: 184
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

	// Token: 0x04000F11 RID: 3857
	public string id;

	// Token: 0x04000F12 RID: 3858
	public DialogueActor[] actors;

	// Token: 0x04000F13 RID: 3859
	public ChoiceQuest.Prompt[] prompts;

	// Token: 0x04000F14 RID: 3860
	public string beforeState;

	// Token: 0x04000F15 RID: 3861
	public int minCorrect = 1;

	// Token: 0x04000F16 RID: 3862
	public string rewardText;

	// Token: 0x04000F17 RID: 3863
	public UnityEvent onReward;

	// Token: 0x04000F18 RID: 3864
	public string afterText;

	// Token: 0x04000F19 RID: 3865
	public string afterState;

	// Token: 0x04000F1A RID: 3866
	public UnityEvent onAfter;

	// Token: 0x04000F1B RID: 3867
	public GameObject crier;

	// Token: 0x020003FA RID: 1018
	[Serializable]
	public struct Prompt
	{
		// Token: 0x04001CB5 RID: 7349
		public string text;

		// Token: 0x04001CB6 RID: 7350
		public ChoiceQuest.Prompt.Choice[] choices;

		// Token: 0x020004BE RID: 1214
		[Serializable]
		public struct Choice
		{
			// Token: 0x04001FAC RID: 8108
			public bool isCorrect;

			// Token: 0x04001FAD RID: 8109
			public string response;
		}
	}
}
