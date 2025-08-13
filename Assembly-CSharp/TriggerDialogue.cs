using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000C7 RID: 199
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

	// Token: 0x0400060B RID: 1547
	public bool onEnter;

	// Token: 0x0400060C RID: 1548
	public bool onExit;

	// Token: 0x0400060D RID: 1549
	public bool deactivateAfterward = true;

	// Token: 0x0400060E RID: 1550
	public DialogueActor[] actors;

	// Token: 0x0400060F RID: 1551
	public string dialogueChunkName;
}
