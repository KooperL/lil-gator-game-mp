using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000123 RID: 291
[AddComponentMenu("Dialogue Sequence/Dialogue (Phone)")]
public class DSDialoguePhone : DialogueSequence, ICueable
{
	// Token: 0x06000578 RID: 1400 RVA: 0x00005E95 File Offset: 0x00004095
	public void SetCamera(GameObject camera)
	{
		this.camera = camera;
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x0002E284 File Offset: 0x0002C484
	public override YieldInstruction Run()
	{
		IEnumerator enumerator;
		if (this.document == null)
		{
			enumerator = DialogueManager.d.LoadChunkPhone(this.dialogue, this.characters, this.clearPhoneAfter, this.displayNames, this.images);
		}
		else
		{
			enumerator = DialogueManager.d.LoadChunkPhone(this.document.FetchChunk(this.dialogue), this.characters, this.clearPhoneAfter, this.displayNames, this.images);
		}
		if (this.cueData != null && this.cueData.Length != 0)
		{
			return CoroutineUtil.Start(DialogueUtil.RunWithCues(enumerator, this.cueData, this.camera, this.precueFirstCue, this));
		}
		return CoroutineUtil.Start(enumerator);
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x0002E334 File Offset: 0x0002C534
	[ContextMenu("Sync Cues")]
	private void SyncCues()
	{
		this.cueData = DialogueUtil.UpdateCues(this.document, this.dialogue, this.cueData);
		if (this.cueData.Length != 0)
		{
			this.precueFirstCue = DialogueUtil.CheckForPrequeue(this.document, this.dialogue, this.cueData[0]);
			return;
		}
		this.precueFirstCue = false;
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x0002E394 File Offset: 0x0002C594
	public override void Activate()
	{
		if (this.precueFirstCue && this.cueData.Length != 0)
		{
			this.cueData[0].onCue.Invoke();
			GameObject[] cueObjects = this.cueData[0].cueObjects;
			for (int i = 0; i < cueObjects.Length; i++)
			{
				cueObjects[i].SetActive(true);
			}
			if (this.cueData[0].setCamera)
			{
				if (this.camera != null)
				{
					this.camera.SetActive(false);
				}
				this.camera = this.cueData[0].camera;
				if (this.camera != null)
				{
					this.camera.SetActive(true);
				}
			}
		}
		base.Activate();
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x00005E9E File Offset: 0x0000409E
	public override void Deactivate()
	{
		base.Deactivate();
		if (this.cueData != null && this.cueData.Length != 0)
		{
			DialogueUtil.StopLastCue(this.cueData, this.camera);
		}
	}

	// Token: 0x04000785 RID: 1925
	public MultilingualTextDocument document;

	// Token: 0x04000786 RID: 1926
	[ChunkLookup("document")]
	public string dialogue;

	// Token: 0x04000787 RID: 1927
	public CharacterProfile[] characters;

	// Token: 0x04000788 RID: 1928
	public DialogueUtil.CueData[] cueData;

	// Token: 0x04000789 RID: 1929
	public bool precueFirstCue;

	// Token: 0x0400078A RID: 1930
	public bool displayNames = true;

	// Token: 0x0400078B RID: 1931
	public bool clearPhoneAfter = true;

	// Token: 0x0400078C RID: 1932
	public Sprite[] images;

	// Token: 0x0400078D RID: 1933
	private GameObject camera;
}
