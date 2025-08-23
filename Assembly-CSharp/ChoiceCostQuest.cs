using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ChoiceCostQuest : MonoBehaviour
{
	// (get) Token: 0x06000D8F RID: 3471 RVA: 0x0000C551 File Offset: 0x0000A751
	// (set) Token: 0x06000D90 RID: 3472 RVA: 0x0000C564 File Offset: 0x0000A764
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

	// Token: 0x06000D91 RID: 3473 RVA: 0x0000C577 File Offset: 0x0000A777
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000D92 RID: 3474 RVA: 0x0000C586 File Offset: 0x0000A786
	private IEnumerator RunConversation()
	{
		if (!this.State)
		{
			this.choices[0].resource.ForceShow = true;
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.promptText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			ChoiceCostQuest.Choice choice = this.choices[DialogueManager.optionChosen];
			if (choice.resource.Amount >= choice.cost)
			{
				choice.resource.Amount -= choice.cost;
				if (!string.IsNullOrEmpty(choice.response))
				{
					yield return base.StartCoroutine(DialogueManager.d.LoadChunk(choice.response, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
				}
				choice.onPurchase.Invoke();
				this.State = true;
			}
			else if (!string.IsNullOrEmpty(this.notEnoughText))
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.notEnoughText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			this.choices[0].resource.ForceShow = false;
			choice = default(ChoiceCostQuest.Choice);
		}
		else
		{
			if (!string.IsNullOrEmpty(this.afterText))
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.afterText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			this.onAfter.Invoke();
		}
		yield return null;
		yield break;
	}

	public string id;

	public ChoiceCostQuest.Choice[] choices;

	public string promptText;

	public string notEnoughText;

	public string afterText;

	public UnityEvent onAfter;

	public DialogueActor[] actors;

	[Serializable]
	public struct Choice
	{
		public ItemResource resource;

		public int cost;

		public string response;

		public UnityEvent onPurchase;
	}
}
