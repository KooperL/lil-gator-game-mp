using System;
using UnityEngine;

// Token: 0x020001B3 RID: 435
public class SusieDialogue : MonoBehaviour, Interaction
{
	// Token: 0x0600081D RID: 2077 RVA: 0x000080C4 File Offset: 0x000062C4
	private void Awake()
	{
		this.boxCollider = base.GetComponent<BoxCollider>();
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x000080D2 File Offset: 0x000062D2
	private void OnEnable()
	{
		this.speedInterface = base.transform.parent.GetComponent<SpeedInterface>();
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x00036244 File Offset: 0x00034444
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

	// Token: 0x06000820 RID: 2080 RVA: 0x00036348 File Offset: 0x00034548
	private void FixedUpdate()
	{
		float speed = this.speedInterface.GetSpeed();
		if (!this.isSpeedTriggered && Mathf.Abs(speed) > this.speedThreshold)
		{
			this.isSpeedTriggered = true;
		}
	}

	// Token: 0x04000AC3 RID: 2755
	public DialogueActor actor;

	// Token: 0x04000AC4 RID: 2756
	public string dialogueChunkName;

	// Token: 0x04000AC5 RID: 2757
	public int dialogueCount = 1;

	// Token: 0x04000AC6 RID: 2758
	private int dialogueIndex;

	// Token: 0x04000AC7 RID: 2759
	public string dialogueAfterChunkName;

	// Token: 0x04000AC8 RID: 2760
	public int dialogueAfterCount = 2;

	// Token: 0x04000AC9 RID: 2761
	private int dialogueAfterIndex;

	// Token: 0x04000ACA RID: 2762
	private SpeedInterface speedInterface;

	// Token: 0x04000ACB RID: 2763
	private bool isSpeedTriggered;

	// Token: 0x04000ACC RID: 2764
	public float speedThreshold = 30f;

	// Token: 0x04000ACD RID: 2765
	public int moneyReward = 5;

	// Token: 0x04000ACE RID: 2766
	private BoxCollider boxCollider;
}
