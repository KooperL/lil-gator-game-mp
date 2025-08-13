using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000215 RID: 533
public class FetchQuest : MonoBehaviour
{
	// Token: 0x170000BC RID: 188
	// (get) Token: 0x06000B89 RID: 2953 RVA: 0x000387A4 File Offset: 0x000369A4
	// (set) Token: 0x06000B8A RID: 2954 RVA: 0x000387B7 File Offset: 0x000369B7
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

	// Token: 0x170000BD RID: 189
	// (get) Token: 0x06000B8B RID: 2955 RVA: 0x000387CA File Offset: 0x000369CA
	private bool IsItemFetched
	{
		get
		{
			return GameData.g.ReadBool(this.fetchItemID, false);
		}
	}

	// Token: 0x06000B8C RID: 2956 RVA: 0x000387DD File Offset: 0x000369DD
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x000387E8 File Offset: 0x000369E8
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

	// Token: 0x06000B8E RID: 2958 RVA: 0x0003886F File Offset: 0x00036A6F
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x0003887E File Offset: 0x00036A7E
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

	// Token: 0x04000F4B RID: 3915
	public string id;

	// Token: 0x04000F4C RID: 3916
	public string fetchItemID;

	// Token: 0x04000F4D RID: 3917
	public DialogueActor[] actors;

	// Token: 0x04000F4E RID: 3918
	public GameObject crier;

	// Token: 0x04000F4F RID: 3919
	public string completeCry;

	// Token: 0x04000F50 RID: 3920
	public string beforeText;

	// Token: 0x04000F51 RID: 3921
	public UnityEvent onBefore;

	// Token: 0x04000F52 RID: 3922
	public string repeatText;

	// Token: 0x04000F53 RID: 3923
	public string rewardText;

	// Token: 0x04000F54 RID: 3924
	public UnityEvent onReward;

	// Token: 0x04000F55 RID: 3925
	public string afterText;

	// Token: 0x04000F56 RID: 3926
	public UnityEvent onAfter;

	// Token: 0x04000F57 RID: 3927
	public string beforeState;

	// Token: 0x04000F58 RID: 3928
	public string afterState;
}
