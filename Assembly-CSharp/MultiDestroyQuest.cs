using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MultiDestroyQuest : MonoBehaviour
{
	// (get) Token: 0x06000E30 RID: 3632 RVA: 0x0000CA39 File Offset: 0x0000AC39
	// (set) Token: 0x06000E31 RID: 3633 RVA: 0x0000CA4C File Offset: 0x0000AC4C
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

	// Token: 0x06000E32 RID: 3634 RVA: 0x0000CA5F File Offset: 0x0000AC5F
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000E33 RID: 3635 RVA: 0x0004CCDC File Offset: 0x0004AEDC
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

	// Token: 0x06000E34 RID: 3636 RVA: 0x0000CA67 File Offset: 0x0000AC67
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000E35 RID: 3637 RVA: 0x0000CA76 File Offset: 0x0000AC76
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
