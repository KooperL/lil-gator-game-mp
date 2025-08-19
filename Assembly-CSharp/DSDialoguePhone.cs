using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/Dialogue (Phone)")]
public class DSDialoguePhone : DialogueSequence, ICueable
{
	// Token: 0x060005B2 RID: 1458 RVA: 0x0000615B File Offset: 0x0000435B
	public void SetCamera(GameObject camera)
	{
		this.camera = camera;
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x0002F95C File Offset: 0x0002DB5C
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

	// Token: 0x060005B4 RID: 1460 RVA: 0x0002FA0C File Offset: 0x0002DC0C
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

	// Token: 0x060005B5 RID: 1461 RVA: 0x0002FA6C File Offset: 0x0002DC6C
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

	// Token: 0x060005B6 RID: 1462 RVA: 0x00006164 File Offset: 0x00004364
	public override void Deactivate()
	{
		base.Deactivate();
		if (this.cueData != null && this.cueData.Length != 0)
		{
			DialogueUtil.StopLastCue(this.cueData, this.camera);
		}
	}

	public MultilingualTextDocument document;

	[ChunkLookup("document")]
	public string dialogue;

	public CharacterProfile[] characters;

	public DialogueUtil.CueData[] cueData;

	public bool precueFirstCue;

	public bool displayNames = true;

	public bool clearPhoneAfter = true;

	public Sprite[] images;

	private GameObject camera;
}
