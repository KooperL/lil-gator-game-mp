using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020002C5 RID: 709
public class MultiDestroyQuest : MonoBehaviour
{
	// Token: 0x17000183 RID: 387
	// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x0000C731 File Offset: 0x0000A931
	// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x0000C744 File Offset: 0x0000A944
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

	// Token: 0x06000DE6 RID: 3558 RVA: 0x0000C757 File Offset: 0x0000A957
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x0004B154 File Offset: 0x00049354
	public void UpdateState()
	{
		int num = -1;
		for (int i = 0; i < this.destroyQuests.Length; i++)
		{
			if (!this.destroyQuests[i].State)
			{
				num = i;
				break;
			}
		}
		for (int j = 0; j < this.destroyQuests.Length; j++)
		{
			this.destroyQuests[j].gameObject.SetActive(j == num);
		}
		bool state = this.State;
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

	// Token: 0x06000DE8 RID: 3560 RVA: 0x0000C75F File Offset: 0x0000A95F
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000DE9 RID: 3561 RVA: 0x0000C76E File Offset: 0x0000A96E
	private IEnumerator RunConversation()
	{
		yield return null;
		if (!this.State)
		{
			int index = -1;
			for (int i = 0; i < this.destroyQuests.Length; i++)
			{
				if (!this.destroyQuests[i].State)
				{
					index = i;
					break;
				}
			}
			yield return base.StartCoroutine(this.destroyQuests[index].RunConversation());
			if (this.destroyQuests[index].State)
			{
				if (index + 1 < this.destroyQuests.Length)
				{
					yield return Blackout.FadeIn();
					this.UpdateState();
					this.onFadeout.Invoke();
					yield return new WaitForSeconds(this.fadeoutLength);
					yield return Blackout.FadeOut();
					yield return base.StartCoroutine(this.destroyQuests[index + 1].RunConversation());
				}
				else
				{
					this.State = true;
					this.UpdateState();
				}
			}
		}
		else if (!string.IsNullOrEmpty(this.afterText))
		{
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.afterText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
		}
		yield break;
	}

	// Token: 0x04001221 RID: 4641
	public string id;

	// Token: 0x04001222 RID: 4642
	public DialogueActor[] actors;

	// Token: 0x04001223 RID: 4643
	public DestroyQuest[] destroyQuests;

	// Token: 0x04001224 RID: 4644
	public UnityEvent onFadeout;

	// Token: 0x04001225 RID: 4645
	public float fadeoutLength = 0.5f;

	// Token: 0x04001226 RID: 4646
	public string beforeState;

	// Token: 0x04001227 RID: 4647
	public string afterState;

	// Token: 0x04001228 RID: 4648
	public string afterText;
}
