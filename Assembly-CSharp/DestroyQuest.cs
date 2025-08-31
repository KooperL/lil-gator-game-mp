using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DestroyQuest : MonoBehaviour
{
	// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0003859C File Offset: 0x0003679C
	// (set) Token: 0x06000B7D RID: 2941 RVA: 0x000385AF File Offset: 0x000367AF
	public bool State
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

	// Token: 0x06000B7E RID: 2942 RVA: 0x000385C2 File Offset: 0x000367C2
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000B7F RID: 2943 RVA: 0x000385CA File Offset: 0x000367CA
	[ContextMenu("DebugState")]
	public void DebugState()
	{
		Debug.Log(this.State);
	}

	// Token: 0x06000B80 RID: 2944 RVA: 0x000385DC File Offset: 0x000367DC
	[ContextMenu("UpdateState")]
	private void UpdateState()
	{
		this.aliveEnemies = 0;
		for (int i = 0; i < this.targets.Length; i++)
		{
			if (!this.targets[i].IsBroken)
			{
				this.aliveEnemies++;
			}
		}
		if (this.crier != null)
		{
			this.crier.SetActive(this.aliveEnemies > 0);
		}
		foreach (DialogueActor dialogueActor in this.actors)
		{
			if (this.aliveEnemies == 0)
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

	// Token: 0x06000B81 RID: 2945 RVA: 0x00038698 File Offset: 0x00036898
	public void TargetDestroyed()
	{
		this.UpdateState();
		if (this.aliveEnemies == 0)
		{
			if (this.crier != null)
			{
				DialogueManager.d.CancelBubble();
			}
			if (!string.IsNullOrEmpty(this.completeCry))
			{
				DialogueManager.d.Bubble(this.completeCry, this.actors, 0f, false, false, true);
			}
		}
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x000386F7 File Offset: 0x000368F7
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x00038706 File Offset: 0x00036906
	public IEnumerator RunConversation()
	{
		DialogueManager.d.CancelBubble();
		if (this.aliveEnemies == 0)
		{
			if (!this.State)
			{
				this.State = true;
				if (!string.IsNullOrEmpty(this.rewardText))
				{
					yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.rewardText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
				}
				this.onReward.Invoke();
			}
			else
			{
				if (!string.IsNullOrEmpty(this.afterText))
				{
					yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.afterText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
				}
				this.onAfter.Invoke();
			}
		}
		else
		{
			if (!string.IsNullOrEmpty(this.beforeText))
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.beforeText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			this.onBefore.Invoke();
		}
		yield return null;
		yield break;
	}

	public string id;

	public BreakableObject[] targets;

	public DialogueActor[] actors;

	public GameObject crier;

	public string completeCry;

	public string beforeText;

	public string beforeState;

	public UnityEvent onBefore;

	public string rewardText;

	public UnityEvent onReward;

	public string afterText;

	public string afterState;

	public UnityEvent onAfter;

	private int aliveEnemies;
}
