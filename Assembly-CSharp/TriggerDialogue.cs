using System;
using System.Collections;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
	// Token: 0x06000452 RID: 1106 RVA: 0x00018B85 File Offset: 0x00016D85
	private void OnTriggerEnter(Collider other)
	{
		if (this.onEnter)
		{
			base.StartCoroutine(this.RunDialogue());
		}
	}

	// Token: 0x06000453 RID: 1107 RVA: 0x00018B9C File Offset: 0x00016D9C
	private void OnTriggerExit(Collider other)
	{
		if (this.onExit)
		{
			base.StartCoroutine(this.RunDialogue());
		}
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x00018BB3 File Offset: 0x00016DB3
	private IEnumerator RunDialogue()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.dialogueChunkName, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
		if (this.deactivateAfterward)
		{
			base.gameObject.SetActive(false);
		}
		yield break;
	}

	public bool onEnter;

	public bool onExit;

	public bool deactivateAfterward = true;

	public DialogueActor[] actors;

	public string dialogueChunkName;
}
