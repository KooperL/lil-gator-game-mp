using System;
using System.Collections;
using UnityEngine;

public class DialogueBubbleTrigger : MonoBehaviour
{
	// Token: 0x06000437 RID: 1079 RVA: 0x00029CBC File Offset: 0x00027EBC
	private void OnValidate()
	{
		DialogueActor dialogueActor;
		if ((this.actors == null || this.actors.Length == 0) && base.TryGetComponent<DialogueActor>(out dialogueActor))
		{
			this.actors = new DialogueActor[1];
			this.actors[0] = dialogueActor;
		}
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x000051E7 File Offset: 0x000033E7
	[ContextMenu("Fix Actor")]
	public void FixActor()
	{
		this.actors = new DialogueActor[1];
		this.actors[0] = base.GetComponent<DialogueActor>();
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x00005203 File Offset: 0x00003403
	private void Awake()
	{
		this.minimumDistanceSqr = this.minimumDistance * this.minimumDistance;
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x00005218 File Offset: 0x00003418
	private void OnTriggerEnter(Collider other)
	{
		if (this.onEnter)
		{
			this.Trigger();
		}
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x00005228 File Offset: 0x00003428
	private void OnTriggerStay(Collider other)
	{
		if (this.onStay)
		{
			this.Trigger();
		}
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x00005238 File Offset: 0x00003438
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

	// Token: 0x0600043D RID: 1085 RVA: 0x00029CFC File Offset: 0x00027EFC
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

	// Token: 0x0600043E RID: 1086 RVA: 0x0000526E File Offset: 0x0000346E
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

	public float minimumDistance;

	private float minimumDistanceSqr;

	public bool onEnter;

	public bool onStay = true;

	public bool onExit;

	public bool cancelOnExit = true;

	public float minMessageDelay = 4f;

	private float nextMessageTime = -1f;

	public bool whileInDialogue;

	public bool interruptBubble;

	public bool hasInput;

	[Space]
	public MultilingualTextDocument document;

	[ChunkLookup("document")]
	public string[] dialogue;

	private int index;

	public bool loop = true;

	public DialogueActor[] actors;

	private IEnumerator waitForBubble;
}
