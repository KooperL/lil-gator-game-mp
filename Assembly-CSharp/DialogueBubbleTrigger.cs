using System;
using System.Collections;
using UnityEngine;

public class DialogueBubbleTrigger : MonoBehaviour
{
	// Token: 0x0600039F RID: 927 RVA: 0x0001580C File Offset: 0x00013A0C
	private void OnValidate()
	{
		DialogueActor dialogueActor;
		if ((this.actors == null || this.actors.Length == 0) && base.TryGetComponent<DialogueActor>(out dialogueActor))
		{
			this.actors = new DialogueActor[1];
			this.actors[0] = dialogueActor;
		}
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x00015849 File Offset: 0x00013A49
	[ContextMenu("Fix Actor")]
	public void FixActor()
	{
		this.actors = new DialogueActor[1];
		this.actors[0] = base.GetComponent<DialogueActor>();
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x00015865 File Offset: 0x00013A65
	private void Awake()
	{
		this.minimumDistanceSqr = this.minimumDistance * this.minimumDistance;
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x0001587A File Offset: 0x00013A7A
	private void OnTriggerEnter(Collider other)
	{
		if (this.onEnter)
		{
			this.Trigger();
		}
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x0001588A File Offset: 0x00013A8A
	private void OnTriggerStay(Collider other)
	{
		if (this.onStay)
		{
			this.Trigger();
		}
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x0001589A File Offset: 0x00013A9A
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

	// Token: 0x060003A5 RID: 933 RVA: 0x000158D0 File Offset: 0x00013AD0
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

	// Token: 0x060003A6 RID: 934 RVA: 0x00015A68 File Offset: 0x00013C68
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
