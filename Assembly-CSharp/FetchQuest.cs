using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FetchQuest : MonoBehaviour
{
	// (get) Token: 0x06000E18 RID: 3608 RVA: 0x0000C966 File Offset: 0x0000AB66
	// (set) Token: 0x06000E19 RID: 3609 RVA: 0x0000C979 File Offset: 0x0000AB79
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

	// (get) Token: 0x06000E1A RID: 3610 RVA: 0x0000C98C File Offset: 0x0000AB8C
	private bool IsItemFetched
	{
		get
		{
			return GameData.g.ReadBool(this.fetchItemID, false);
		}
	}

	// Token: 0x06000E1B RID: 3611 RVA: 0x0000C99F File Offset: 0x0000AB9F
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x0004C770 File Offset: 0x0004A970
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

	// Token: 0x06000E1D RID: 3613 RVA: 0x0000C9A7 File Offset: 0x0000ABA7
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000E1E RID: 3614 RVA: 0x0000C9B6 File Offset: 0x0000ABB6
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

	public string id;

	public string fetchItemID;

	public DialogueActor[] actors;

	public GameObject crier;

	public string completeCry;

	public string beforeText;

	public UnityEvent onBefore;

	public string repeatText;

	public string rewardText;

	public UnityEvent onReward;

	public string afterText;

	public UnityEvent onAfter;

	public string beforeState;

	public string afterState;
}
