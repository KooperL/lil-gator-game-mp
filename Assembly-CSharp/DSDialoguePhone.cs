using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000D7 RID: 215
[AddComponentMenu("Dialogue Sequence/Dialogue (Phone)")]
public class DSDialoguePhone : DialogueSequence, ICueable
{
	// Token: 0x06000487 RID: 1159 RVA: 0x00019751 File Offset: 0x00017951
	public void SetCamera(GameObject camera)
	{
		this.camera = camera;
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x0001975C File Offset: 0x0001795C
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

	// Token: 0x06000489 RID: 1161 RVA: 0x0001980C File Offset: 0x00017A0C
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

	// Token: 0x0600048A RID: 1162 RVA: 0x0001986C File Offset: 0x00017A6C
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

	// Token: 0x0600048B RID: 1163 RVA: 0x00019933 File Offset: 0x00017B33
	public override void Deactivate()
	{
		base.Deactivate();
		if (this.cueData != null && this.cueData.Length != 0)
		{
			DialogueUtil.StopLastCue(this.cueData, this.camera);
		}
	}

	// Token: 0x04000654 RID: 1620
	public MultilingualTextDocument document;

	// Token: 0x04000655 RID: 1621
	[ChunkLookup("document")]
	public string dialogue;

	// Token: 0x04000656 RID: 1622
	public CharacterProfile[] characters;

	// Token: 0x04000657 RID: 1623
	public DialogueUtil.CueData[] cueData;

	// Token: 0x04000658 RID: 1624
	public bool precueFirstCue;

	// Token: 0x04000659 RID: 1625
	public bool displayNames = true;

	// Token: 0x0400065A RID: 1626
	public bool clearPhoneAfter = true;

	// Token: 0x0400065B RID: 1627
	public Sprite[] images;

	// Token: 0x0400065C RID: 1628
	private GameObject camera;
}
