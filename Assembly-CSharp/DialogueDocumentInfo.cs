using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDocumentInfo : MonoBehaviour
{
	// Token: 0x0600056A RID: 1386 RVA: 0x00005EE0 File Offset: 0x000040E0
	private void OnEnable()
	{
		DialogueDocumentInfo.d = this;
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x00005EE8 File Offset: 0x000040E8
	private void OnDisable()
	{
		if (DialogueDocumentInfo.d == this)
		{
			DialogueDocumentInfo.d = null;
		}
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x0002EB50 File Offset: 0x0002CD50
	public void LoadDocumentInfo(DialogueChunk chunk)
	{
		MultilingualTextDocument multilingualTextDocument = this.FindDocumentForChunk(chunk);
		if (multilingualTextDocument == null)
		{
			return;
		}
		this.documentName.text = this.locTransfer.GetLocDocumentName(multilingualTextDocument);
		this.chunkName.text = chunk.name;
		this.root.SetActive(true);
		if (!this.waitingForDialogueToEnd)
		{
			this.waitingForDialogueToEnd = true;
			base.StartCoroutine(this.WaitForDialogueToEnd());
		}
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x00005EFD File Offset: 0x000040FD
	private void ClearDocumentInfo()
	{
		this.root.SetActive(false);
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x0002EBC0 File Offset: 0x0002CDC0
	private MultilingualTextDocument FindDocumentForChunk(DialogueChunk chunk)
	{
		foreach (MultilingualTextDocument multilingualTextDocument in this.documents)
		{
			if (multilingualTextDocument.chunks.Contains(chunk))
			{
				return multilingualTextDocument;
			}
		}
		return null;
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x00005F0B File Offset: 0x0000410B
	private IEnumerator WaitForDialogueToEnd()
	{
		while (Game.State == GameState.Dialogue || DialogueManager.d.isInBubbleDialogue)
		{
			yield return null;
		}
		this.ClearDocumentInfo();
		this.waitingForDialogueToEnd = false;
		yield break;
	}

	public static DialogueDocumentInfo d;

	public LocTransfer locTransfer;

	public MultilingualTextDocument[] documents;

	public GameObject root;

	public Text documentName;

	public Text chunkName;

	private bool waitingForDialogueToEnd;
}
