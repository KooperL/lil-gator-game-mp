using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020002A7 RID: 679
public class ChoiceCostQuest : MonoBehaviour
{
	// Token: 0x1700015B RID: 347
	// (get) Token: 0x06000D42 RID: 3394 RVA: 0x0000C23F File Offset: 0x0000A43F
	// (set) Token: 0x06000D43 RID: 3395 RVA: 0x0000C252 File Offset: 0x0000A452
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

	// Token: 0x06000D44 RID: 3396 RVA: 0x0000C265 File Offset: 0x0000A465
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000D45 RID: 3397 RVA: 0x0000C274 File Offset: 0x0000A474
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

	// Token: 0x04001180 RID: 4480
	public string id;

	// Token: 0x04001181 RID: 4481
	public ChoiceCostQuest.Choice[] choices;

	// Token: 0x04001182 RID: 4482
	public string promptText;

	// Token: 0x04001183 RID: 4483
	public string notEnoughText;

	// Token: 0x04001184 RID: 4484
	public string afterText;

	// Token: 0x04001185 RID: 4485
	public UnityEvent onAfter;

	// Token: 0x04001186 RID: 4486
	public DialogueActor[] actors;

	// Token: 0x020002A8 RID: 680
	[Serializable]
	public struct Choice
	{
		// Token: 0x04001187 RID: 4487
		public ItemResource resource;

		// Token: 0x04001188 RID: 4488
		public int cost;

		// Token: 0x04001189 RID: 4489
		public string response;

		// Token: 0x0400118A RID: 4490
		public UnityEvent onPurchase;
	}
}
