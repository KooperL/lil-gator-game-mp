using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020002C1 RID: 705
public class FetchQuest : MonoBehaviour
{
	// Token: 0x1700017C RID: 380
	// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0000C673 File Offset: 0x0000A873
	// (set) Token: 0x06000DCD RID: 3533 RVA: 0x0000C686 File Offset: 0x0000A886
	private int State
	{
		get
		{
			return GameData.g.ReadInt(this.id, 0);
		}
		set
		{
			GameData.g.Write(this.id, value);
		}
	}

	// Token: 0x1700017D RID: 381
	// (get) Token: 0x06000DCE RID: 3534 RVA: 0x0000C699 File Offset: 0x0000A899
	private bool IsItemFetched
	{
		get
		{
			return GameData.g.ReadBool(this.fetchItemID, false);
		}
	}

	// Token: 0x06000DCF RID: 3535 RVA: 0x0000C6AC File Offset: 0x0000A8AC
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000DD0 RID: 3536 RVA: 0x0004AD7C File Offset: 0x00048F7C
	private void UpdateState()
	{
		bool isItemFetched = this.IsItemFetched;
		if (this.crier != null)
		{
			this.crier.SetActive(!isItemFetched);
		}
		foreach (DialogueActor dialogueActor in this.actors)
		{
			if (this.State == 2)
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

	// Token: 0x06000DD1 RID: 3537 RVA: 0x0000C6B4 File Offset: 0x0000A8B4
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000DD2 RID: 3538 RVA: 0x0000C6C3 File Offset: 0x0000A8C3
	private IEnumerator RunConversation()
	{
		DialogueManager.d.CancelBubble();
		int state = this.State;
		bool isItemFetched = this.IsItemFetched;
		if (state == 0 || !isItemFetched)
		{
			if (state == 1 && !string.IsNullOrEmpty(this.repeatText))
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.repeatText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			else if (!string.IsNullOrEmpty(this.beforeText))
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.beforeText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			this.onBefore.Invoke();
			this.State = 1;
		}
		if (state != 2 && isItemFetched)
		{
			if (!string.IsNullOrEmpty(this.rewardText))
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.rewardText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			this.onReward.Invoke();
			this.State = 2;
		}
		if (state == 2)
		{
			if (!string.IsNullOrEmpty(this.afterText))
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.afterText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			this.onAfter.Invoke();
		}
		this.UpdateState();
		yield return null;
		yield break;
	}

	// Token: 0x04001202 RID: 4610
	public string id;

	// Token: 0x04001203 RID: 4611
	public string fetchItemID;

	// Token: 0x04001204 RID: 4612
	public DialogueActor[] actors;

	// Token: 0x04001205 RID: 4613
	public GameObject crier;

	// Token: 0x04001206 RID: 4614
	public string completeCry;

	// Token: 0x04001207 RID: 4615
	public string beforeText;

	// Token: 0x04001208 RID: 4616
	public UnityEvent onBefore;

	// Token: 0x04001209 RID: 4617
	public string repeatText;

	// Token: 0x0400120A RID: 4618
	public string rewardText;

	// Token: 0x0400120B RID: 4619
	public UnityEvent onReward;

	// Token: 0x0400120C RID: 4620
	public string afterText;

	// Token: 0x0400120D RID: 4621
	public UnityEvent onAfter;

	// Token: 0x0400120E RID: 4622
	public string beforeState;

	// Token: 0x0400120F RID: 4623
	public string afterState;
}
