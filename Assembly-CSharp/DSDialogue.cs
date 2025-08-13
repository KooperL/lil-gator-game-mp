using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000120 RID: 288
[AddComponentMenu("Dialogue Sequence/Dialogue")]
public class DSDialogue : DialogueSequence, ICueable
{
	// Token: 0x0600056E RID: 1390 RVA: 0x00005E24 File Offset: 0x00004024
	public void SetCamera(GameObject camera)
	{
		this.camera = camera;
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x0002DF50 File Offset: 0x0002C150
	public override YieldInstruction Run()
	{
		if (string.IsNullOrEmpty(this.dialogue))
		{
			return null;
		}
		IEnumerator enumerator;
		if (this.document == null)
		{
			enumerator = DialogueManager.d.LoadChunk(this.dialogue, this.actors, this.background, true);
		}
		else
		{
			enumerator = DialogueManager.d.LoadChunk(this.document.FetchChunk(this.dialogue), this.actors, this.background, true, this.setDialogueCamera && !this.presetPosition, false, false);
		}
		if (this.cueData != null && this.cueData.Length != 0)
		{
			return CoroutineUtil.Start(DialogueUtil.RunWithCues(enumerator, this.cueData, this.camera, this.precueFirstCue, this));
		}
		return CoroutineUtil.Start(enumerator);
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x0002E010 File Offset: 0x0002C210
	public void OnValidate()
	{
		if (this.document == null)
		{
			DSDialogue component = base.GetComponent<DSDialogue>();
			if (component != null && component != this && component.document != null)
			{
				this.document = component.document;
			}
		}
		if (this.cues == null && this.cueData != null)
		{
			this.cueData = new DialogueUtil.CueData[this.cues.Length];
			for (int i = 0; i < this.cues.Length; i++)
			{
				this.cueData[i] = default(DialogueUtil.CueData);
				this.cueData[i].onCue = this.cues[i];
			}
		}
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x0002E0C0 File Offset: 0x0002C2C0
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

	// Token: 0x06000572 RID: 1394 RVA: 0x0002E120 File Offset: 0x0002C320
	public override void Activate()
	{
		if (this.presetPosition)
		{
			DialogueManager.d.SetDialogueCamera(this.actors);
		}
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

	// Token: 0x06000573 RID: 1395 RVA: 0x00005E2D File Offset: 0x0000402D
	public override void Deactivate()
	{
		base.Deactivate();
		if (this.presetPosition)
		{
			DialogueManager.d.ClearDialogueCamera();
		}
		if (this.cueData != null && this.cueData.Length != 0)
		{
			DialogueUtil.StopLastCue(this.cueData, this.camera);
		}
	}

	// Token: 0x04000774 RID: 1908
	public MultilingualTextDocument document;

	// Token: 0x04000775 RID: 1909
	[ChunkLookup("document")]
	public string dialogue;

	// Token: 0x04000776 RID: 1910
	public DialogueActor[] actors;

	// Token: 0x04000777 RID: 1911
	[HideInInspector]
	public UnityEvent[] cues;

	// Token: 0x04000778 RID: 1912
	public DialogueUtil.CueData[] cueData;

	// Token: 0x04000779 RID: 1913
	public bool precueFirstCue;

	// Token: 0x0400077A RID: 1914
	public bool presetPosition = true;

	// Token: 0x0400077B RID: 1915
	public DialogueManager.DialogueBoxBackground background;

	// Token: 0x0400077C RID: 1916
	public bool setDialogueCamera = true;

	// Token: 0x0400077D RID: 1917
	private GameObject camera;
}
