using System;
using System.Collections;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
	// Token: 0x06000556 RID: 1366 RVA: 0x00005E32 File Offset: 0x00004032
	private void OnTriggerEnter(Collider other)
	{
		if (this.onEnter)
		{
			base.StartCoroutine(this.RunDialogue());
		}
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x00005E49 File Offset: 0x00004049
	private void OnTriggerExit(Collider other)
	{
		if (this.onExit)
		{
			base.StartCoroutine(this.RunDialogue());
		}
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x00005E60 File Offset: 0x00004060
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
