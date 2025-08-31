using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDocumentInfo : MonoBehaviour
{
	// Token: 0x0600045A RID: 1114 RVA: 0x00018C07 File Offset: 0x00016E07
	private void OnEnable()
	{
		DialogueDocumentInfo.d = this;
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x00018C0F File Offset: 0x00016E0F
	private void OnDisable()
	{
		if (DialogueDocumentInfo.d == this)
		{
			DialogueDocumentInfo.d = null;
		}
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x00018C24 File Offset: 0x00016E24
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

	// Token: 0x0600045D RID: 1117 RVA: 0x00018C93 File Offset: 0x00016E93
	private void ClearDocumentInfo()
	{
		this.root.SetActive(false);
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x00018CA4 File Offset: 0x00016EA4
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

	// Token: 0x0600045F RID: 1119 RVA: 0x00018CDB File Offset: 0x00016EDB
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
