using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DestroyQuest : MonoBehaviour
{
	// (get) Token: 0x06000E05 RID: 3589 RVA: 0x0000C8C0 File Offset: 0x0000AAC0
	// (set) Token: 0x06000E06 RID: 3590 RVA: 0x0000C8D3 File Offset: 0x0000AAD3
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

	// Token: 0x06000E07 RID: 3591 RVA: 0x0000C8E6 File Offset: 0x0000AAE6
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000E08 RID: 3592 RVA: 0x0000C8EE File Offset: 0x0000AAEE
	[ContextMenu("DebugState")]
	public void DebugState()
	{
		Debug.Log(this.State);
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x0004C4A0 File Offset: 0x0004A6A0
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

	// Token: 0x06000E0A RID: 3594 RVA: 0x0004C55C File Offset: 0x0004A75C
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

	// Token: 0x06000E0B RID: 3595 RVA: 0x0000C900 File Offset: 0x0000AB00
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000E0C RID: 3596 RVA: 0x0000C90F File Offset: 0x0000AB0F
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
