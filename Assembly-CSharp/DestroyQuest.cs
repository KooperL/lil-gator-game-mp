using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020002BE RID: 702
public class DestroyQuest : MonoBehaviour
{
	// Token: 0x17000179 RID: 377
	// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x0000C5CD File Offset: 0x0000A7CD
	// (set) Token: 0x06000DBA RID: 3514 RVA: 0x0000C5E0 File Offset: 0x0000A7E0
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

	// Token: 0x06000DBB RID: 3515 RVA: 0x0000C5F3 File Offset: 0x0000A7F3
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000DBC RID: 3516 RVA: 0x0000C5FB File Offset: 0x0000A7FB
	[ContextMenu("DebugState")]
	public void DebugState()
	{
		Debug.Log(this.State);
	}

	// Token: 0x06000DBD RID: 3517 RVA: 0x0004AAAC File Offset: 0x00048CAC
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

	// Token: 0x06000DBE RID: 3518 RVA: 0x0004AB68 File Offset: 0x00048D68
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

	// Token: 0x06000DBF RID: 3519 RVA: 0x0000C60D File Offset: 0x0000A80D
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000DC0 RID: 3520 RVA: 0x0000C61C File Offset: 0x0000A81C
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

	// Token: 0x040011EE RID: 4590
	public string id;

	// Token: 0x040011EF RID: 4591
	public BreakableObject[] targets;

	// Token: 0x040011F0 RID: 4592
	public DialogueActor[] actors;

	// Token: 0x040011F1 RID: 4593
	public GameObject crier;

	// Token: 0x040011F2 RID: 4594
	public string completeCry;

	// Token: 0x040011F3 RID: 4595
	public string beforeText;

	// Token: 0x040011F4 RID: 4596
	public string beforeState;

	// Token: 0x040011F5 RID: 4597
	public UnityEvent onBefore;

	// Token: 0x040011F6 RID: 4598
	public string rewardText;

	// Token: 0x040011F7 RID: 4599
	public UnityEvent onReward;

	// Token: 0x040011F8 RID: 4600
	public string afterText;

	// Token: 0x040011F9 RID: 4601
	public string afterState;

	// Token: 0x040011FA RID: 4602
	public UnityEvent onAfter;

	// Token: 0x040011FB RID: 4603
	private int aliveEnemies;
}
