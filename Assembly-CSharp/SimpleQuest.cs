using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000228 RID: 552
public class SimpleQuest : MonoBehaviour
{
	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x06000BDC RID: 3036 RVA: 0x000391B9 File Offset: 0x000373B9
	// (set) Token: 0x06000BDD RID: 3037 RVA: 0x000391CC File Offset: 0x000373CC
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

	// Token: 0x06000BDE RID: 3038 RVA: 0x000391DF File Offset: 0x000373DF
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000BDF RID: 3039 RVA: 0x000391E8 File Offset: 0x000373E8
	private void UpdateState()
	{
		bool state = this.State;
		if (this.crier != null)
		{
			this.crier.SetActive(!state);
		}
		foreach (DialogueActor dialogueActor in this.actors)
		{
			if (state)
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

	// Token: 0x06000BE0 RID: 3040 RVA: 0x00039269 File Offset: 0x00037469
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000BE1 RID: 3041 RVA: 0x00039278 File Offset: 0x00037478
	private IEnumerator RunConversation()
	{
		DialogueManager.d.CancelBubble();
		if (!this.State)
		{
			if (!string.IsNullOrEmpty(this.beforeText))
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.beforeText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			this.onReward.Invoke();
			this.State = true;
			this.UpdateState();
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

	// Token: 0x04000FAB RID: 4011
	public string id;

	// Token: 0x04000FAC RID: 4012
	[Space]
	public DialogueActor[] actors;

	// Token: 0x04000FAD RID: 4013
	public GameObject crier;

	// Token: 0x04000FAE RID: 4014
	public string beforeState;

	// Token: 0x04000FAF RID: 4015
	public string beforeText;

	// Token: 0x04000FB0 RID: 4016
	public UnityEvent onReward;

	// Token: 0x04000FB1 RID: 4017
	public string afterState;

	// Token: 0x04000FB2 RID: 4018
	public string afterText;

	// Token: 0x04000FB3 RID: 4019
	public UnityEvent onAfter;
}
