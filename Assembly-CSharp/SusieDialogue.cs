using System;
using UnityEngine;

public class SusieDialogue : MonoBehaviour, Interaction
{
	// Token: 0x0600085D RID: 2141 RVA: 0x000083D3 File Offset: 0x000065D3
	private void Awake()
	{
		this.boxCollider = base.GetComponent<BoxCollider>();
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x000083E1 File Offset: 0x000065E1
	private void OnEnable()
	{
		this.speedInterface = base.transform.parent.GetComponent<SpeedInterface>();
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x00037B88 File Offset: 0x00035D88
	public void Interact()
	{
		if (this.isSpeedTriggered)
		{
			this.dialogueAfterIndex = Mathf.Min(this.dialogueAfterCount, this.dialogueAfterIndex + 1);
			if (this.dialogueAfterCount > 1)
			{
				base.StartCoroutine(DialogueManager.d.LoadChunk(this.dialogueAfterChunkName + this.dialogueAfterIndex.ToString("0"), this.actor, DialogueManager.DialogueBoxBackground.Standard, true));
				return;
			}
			base.StartCoroutine(DialogueManager.d.LoadChunk(this.dialogueAfterChunkName, this.actor, DialogueManager.DialogueBoxBackground.Standard, true));
			return;
		}
		else
		{
			this.dialogueIndex = Mathf.Min(this.dialogueCount, this.dialogueIndex + 1);
			if (this.dialogueCount > 1)
			{
				base.StartCoroutine(DialogueManager.d.LoadChunk(this.dialogueChunkName + this.dialogueIndex.ToString("0"), this.actor, DialogueManager.DialogueBoxBackground.Standard, true));
				return;
			}
			base.StartCoroutine(DialogueManager.d.LoadChunk(this.dialogueChunkName, this.actor, DialogueManager.DialogueBoxBackground.Standard, true));
			return;
		}
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x00037C8C File Offset: 0x00035E8C
	private void FixedUpdate()
	{
		float speed = this.speedInterface.GetSpeed();
		if (!this.isSpeedTriggered && Mathf.Abs(speed) > this.speedThreshold)
		{
			this.isSpeedTriggered = true;
		}
	}

	public DialogueActor actor;

	public string dialogueChunkName;

	public int dialogueCount = 1;

	private int dialogueIndex;

	public string dialogueAfterChunkName;

	public int dialogueAfterCount = 2;

	private int dialogueAfterIndex;

	private SpeedInterface speedInterface;

	private bool isSpeedTriggered;

	public float speedThreshold = 30f;

	public int moneyReward = 5;

	private BoxCollider boxCollider;
}
