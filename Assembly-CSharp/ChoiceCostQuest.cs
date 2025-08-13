using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200020E RID: 526
public class ChoiceCostQuest : MonoBehaviour
{
	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x06000B53 RID: 2899 RVA: 0x000381CE File Offset: 0x000363CE
	// (set) Token: 0x06000B54 RID: 2900 RVA: 0x000381E1 File Offset: 0x000363E1
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

	// Token: 0x06000B55 RID: 2901 RVA: 0x000381F4 File Offset: 0x000363F4
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000B56 RID: 2902 RVA: 0x00038203 File Offset: 0x00036403
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

	// Token: 0x04000F08 RID: 3848
	public string id;

	// Token: 0x04000F09 RID: 3849
	public ChoiceCostQuest.Choice[] choices;

	// Token: 0x04000F0A RID: 3850
	public string promptText;

	// Token: 0x04000F0B RID: 3851
	public string notEnoughText;

	// Token: 0x04000F0C RID: 3852
	public string afterText;

	// Token: 0x04000F0D RID: 3853
	public UnityEvent onAfter;

	// Token: 0x04000F0E RID: 3854
	public DialogueActor[] actors;

	// Token: 0x020003F6 RID: 1014
	[Serializable]
	public struct Choice
	{
		// Token: 0x04001CA4 RID: 7332
		public ItemResource resource;

		// Token: 0x04001CA5 RID: 7333
		public int cost;

		// Token: 0x04001CA6 RID: 7334
		public string response;

		// Token: 0x04001CA7 RID: 7335
		public UnityEvent onPurchase;
	}
}
