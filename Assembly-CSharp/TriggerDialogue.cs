using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200010D RID: 269
public class TriggerDialogue : MonoBehaviour
{
	// Token: 0x06000529 RID: 1321 RVA: 0x00005BBD File Offset: 0x00003DBD
	private void OnTriggerEnter(Collider other)
	{
		if (this.onEnter)
		{
			base.StartCoroutine(this.RunDialogue());
		}
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x00005BD4 File Offset: 0x00003DD4
	private void OnTriggerExit(Collider other)
	{
		if (this.onExit)
		{
			base.StartCoroutine(this.RunDialogue());
		}
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x00005BEB File Offset: 0x00003DEB
	private IEnumerator RunDialogue()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.dialogueChunkName, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
		if (this.deactivateAfterward)
		{
			base.gameObject.SetActive(false);
		}
		yield break;
	}

	// Token: 0x04000724 RID: 1828
	public bool onEnter;

	// Token: 0x04000725 RID: 1829
	public bool onExit;

	// Token: 0x04000726 RID: 1830
	public bool deactivateAfterward = true;

	// Token: 0x04000727 RID: 1831
	public DialogueActor[] actors;

	// Token: 0x04000728 RID: 1832
	public string dialogueChunkName;
}
