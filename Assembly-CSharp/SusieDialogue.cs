using System;
using UnityEngine;

public class SusieDialogue : MonoBehaviour, Interaction
{
	// Token: 0x060006DF RID: 1759 RVA: 0x00022BD5 File Offset: 0x00020DD5
	private void Awake()
	{
		this.boxCollider = base.GetComponent<BoxCollider>();
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x00022BE3 File Offset: 0x00020DE3
	private void OnEnable()
	{
		this.speedInterface = base.transform.parent.GetComponent<SpeedInterface>();
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x00022BFC File Offset: 0x00020DFC
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

	// Token: 0x060006E2 RID: 1762 RVA: 0x00022D00 File Offset: 0x00020F00
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
