using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SimpleQuest : MonoBehaviour
{
	// (get) Token: 0x06000E8B RID: 3723 RVA: 0x0000CD7C File Offset: 0x0000AF7C
	// (set) Token: 0x06000E8C RID: 3724 RVA: 0x0000CD8F File Offset: 0x0000AF8F
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

	// Token: 0x06000E8D RID: 3725 RVA: 0x0000CDA2 File Offset: 0x0000AFA2
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000E8E RID: 3726 RVA: 0x0004D768 File Offset: 0x0004B968
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

	// Token: 0x06000E8F RID: 3727 RVA: 0x0000CDAA File Offset: 0x0000AFAA
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000E90 RID: 3728 RVA: 0x0000CDB9 File Offset: 0x0000AFB9
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

	public string id;

	[Space]
	public DialogueActor[] actors;

	public GameObject crier;

	public string beforeState;

	public string beforeText;

	public UnityEvent onReward;

	public string afterState;

	public string afterText;

	public UnityEvent onAfter;
}
