using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020002DA RID: 730
public class SimpleQuest : MonoBehaviour
{
	// Token: 0x17000192 RID: 402
	// (get) Token: 0x06000E3F RID: 3647 RVA: 0x0000CA89 File Offset: 0x0000AC89
	// (set) Token: 0x06000E40 RID: 3648 RVA: 0x0000CA9C File Offset: 0x0000AC9C
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

	// Token: 0x06000E41 RID: 3649 RVA: 0x0000CAAF File Offset: 0x0000ACAF
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000E42 RID: 3650 RVA: 0x0004BD74 File Offset: 0x00049F74
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

	// Token: 0x06000E43 RID: 3651 RVA: 0x0000CAB7 File Offset: 0x0000ACB7
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000E44 RID: 3652 RVA: 0x0000CAC6 File Offset: 0x0000ACC6
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

	// Token: 0x0400127C RID: 4732
	public string id;

	// Token: 0x0400127D RID: 4733
	[Space]
	public DialogueActor[] actors;

	// Token: 0x0400127E RID: 4734
	public GameObject crier;

	// Token: 0x0400127F RID: 4735
	public string beforeState;

	// Token: 0x04001280 RID: 4736
	public string beforeText;

	// Token: 0x04001281 RID: 4737
	public UnityEvent onReward;

	// Token: 0x04001282 RID: 4738
	public string afterState;

	// Token: 0x04001283 RID: 4739
	public string afterText;

	// Token: 0x04001284 RID: 4740
	public UnityEvent onAfter;
}
