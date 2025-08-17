using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SimpleQuest : MonoBehaviour
{
	// (get) Token: 0x06000E8B RID: 3723 RVA: 0x0000CD91 File Offset: 0x0000AF91
	// (set) Token: 0x06000E8C RID: 3724 RVA: 0x0000CDA4 File Offset: 0x0000AFA4
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

	// Token: 0x06000E8D RID: 3725 RVA: 0x0000CDB7 File Offset: 0x0000AFB7
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000E8E RID: 3726 RVA: 0x0004D8FC File Offset: 0x0004BAFC
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

	// Token: 0x06000E8F RID: 3727 RVA: 0x0000CDBF File Offset: 0x0000AFBF
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000E90 RID: 3728 RVA: 0x0000CDCE File Offset: 0x0000AFCE
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
