using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000E2 RID: 226
public class DialogueBubbleTrigger : MonoBehaviour
{
	// Token: 0x06000410 RID: 1040 RVA: 0x00028D10 File Offset: 0x00026F10
	private void OnValidate()
	{
		DialogueActor dialogueActor;
		if ((this.actors == null || this.actors.Length == 0) && base.TryGetComponent<DialogueActor>(ref dialogueActor))
		{
			this.actors = new DialogueActor[1];
			this.actors[0] = dialogueActor;
		}
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x00004FB4 File Offset: 0x000031B4
	[ContextMenu("Fix Actor")]
	public void FixActor()
	{
		this.actors = new DialogueActor[1];
		this.actors[0] = base.GetComponent<DialogueActor>();
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x00004FD0 File Offset: 0x000031D0
	private void Awake()
	{
		this.minimumDistanceSqr = this.minimumDistance * this.minimumDistance;
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x00004FE5 File Offset: 0x000031E5
	private void OnTriggerEnter(Collider other)
	{
		if (this.onEnter)
		{
			this.Trigger();
		}
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x00004FF5 File Offset: 0x000031F5
	private void OnTriggerStay(Collider other)
	{
		if (this.onStay)
		{
			this.Trigger();
		}
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x00005005 File Offset: 0x00003205
	private void OnTriggerExit(Collider other)
	{
		if (this.cancelOnExit && this.waitForBubble != null)
		{
			DialogueManager.d.CancelBubble();
			this.waitForBubble.MoveNext();
		}
		if (this.onExit)
		{
			this.Trigger();
		}
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x00028D50 File Offset: 0x00026F50
	[ContextMenu("Trigger")]
	public void Trigger()
	{
		if (this.waitForBubble != null)
		{
			return;
		}
		if (Time.time > this.nextMessageTime && (this.interruptBubble || !DialogueManager.d.IsInAmbientDialogue) && (Mathf.Approximately(this.minimumDistanceSqr, 0f) || (Player.Position - base.transform.position).sqrMagnitude > this.minimumDistanceSqr) && (this.whileInDialogue || Game.DialogueDepth == 0))
		{
			YieldInstruction yieldInstruction = null;
			if (this.document == null)
			{
				yieldInstruction = DialogueManager.d.Bubble(this.dialogue[this.index], this.actors, 0f, false, this.hasInput, this.interruptBubble);
			}
			else if (!string.IsNullOrEmpty(this.dialogue[this.index]))
			{
				yieldInstruction = DialogueManager.d.Bubble(this.document.FetchChunk(this.dialogue[this.index]), this.actors, 0f, false, this.hasInput, this.interruptBubble);
			}
			this.index++;
			if (this.index >= this.dialogue.Length)
			{
				if (this.loop)
				{
					this.index = 0;
				}
				else
				{
					this.index = this.dialogue.Length - 1;
				}
			}
			this.nextMessageTime = Time.time + this.minMessageDelay;
			if (!string.IsNullOrEmpty(this.dialogue[this.index]))
			{
				this.waitForBubble = this.WaitForBubbleToFinish(yieldInstruction);
				CoroutineUtil.Start(this.waitForBubble);
			}
		}
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x0000503B File Offset: 0x0000323B
	public IEnumerator WaitForBubbleToFinish(YieldInstruction bubble)
	{
		while (DialogueManager.d.isInBubbleDialogue)
		{
			yield return null;
		}
		this.nextMessageTime = Time.time + this.minMessageDelay;
		this.waitForBubble = null;
		yield break;
	}

	// Token: 0x040005CC RID: 1484
	public float minimumDistance;

	// Token: 0x040005CD RID: 1485
	private float minimumDistanceSqr;

	// Token: 0x040005CE RID: 1486
	public bool onEnter;

	// Token: 0x040005CF RID: 1487
	public bool onStay = true;

	// Token: 0x040005D0 RID: 1488
	public bool onExit;

	// Token: 0x040005D1 RID: 1489
	public bool cancelOnExit = true;

	// Token: 0x040005D2 RID: 1490
	public float minMessageDelay = 4f;

	// Token: 0x040005D3 RID: 1491
	private float nextMessageTime = -1f;

	// Token: 0x040005D4 RID: 1492
	public bool whileInDialogue;

	// Token: 0x040005D5 RID: 1493
	public bool interruptBubble;

	// Token: 0x040005D6 RID: 1494
	public bool hasInput;

	// Token: 0x040005D7 RID: 1495
	[Space]
	public MultilingualTextDocument document;

	// Token: 0x040005D8 RID: 1496
	[ChunkLookup("document")]
	public string[] dialogue;

	// Token: 0x040005D9 RID: 1497
	private int index;

	// Token: 0x040005DA RID: 1498
	public bool loop = true;

	// Token: 0x040005DB RID: 1499
	public DialogueActor[] actors;

	// Token: 0x040005DC RID: 1500
	private IEnumerator waitForBubble;
}
