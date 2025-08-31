using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MultiDestroyQuest : MonoBehaviour
{
	// (get) Token: 0x06000B95 RID: 2965 RVA: 0x000388CE File Offset: 0x00036ACE
	// (set) Token: 0x06000B96 RID: 2966 RVA: 0x000388E1 File Offset: 0x00036AE1
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

	// Token: 0x06000B97 RID: 2967 RVA: 0x000388F4 File Offset: 0x00036AF4
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000B98 RID: 2968 RVA: 0x000388FC File Offset: 0x00036AFC
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

	// Token: 0x06000B99 RID: 2969 RVA: 0x000389BC File Offset: 0x00036BBC
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000B9A RID: 2970 RVA: 0x000389CB File Offset: 0x00036BCB
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

	public string id;

	public DialogueActor[] actors;

	public DestroyQuest[] destroyQuests;

	public UnityEvent onFadeout;

	public float fadeoutLength = 0.5f;

	public string beforeState;

	public string afterState;

	public string afterText;
}
