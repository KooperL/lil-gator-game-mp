using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000D4 RID: 212
[AddComponentMenu("Dialogue Sequence/Dialogue")]
public class DSDialogue : DialogueSequence, ICueable
{
	// Token: 0x0600047D RID: 1149 RVA: 0x000193A0 File Offset: 0x000175A0
	public void SetCamera(GameObject camera)
	{
		this.camera = camera;
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x000193AC File Offset: 0x000175AC
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

	// Token: 0x0600047F RID: 1151 RVA: 0x0001946C File Offset: 0x0001766C
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

	// Token: 0x06000480 RID: 1152 RVA: 0x0001951C File Offset: 0x0001771C
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

	// Token: 0x06000481 RID: 1153 RVA: 0x0001957C File Offset: 0x0001777C
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

	// Token: 0x06000482 RID: 1154 RVA: 0x0001965B File Offset: 0x0001785B
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

	// Token: 0x04000643 RID: 1603
	public MultilingualTextDocument document;

	// Token: 0x04000644 RID: 1604
	[ChunkLookup("document")]
	public string dialogue;

	// Token: 0x04000645 RID: 1605
	public DialogueActor[] actors;

	// Token: 0x04000646 RID: 1606
	[HideInInspector]
	public UnityEvent[] cues;

	// Token: 0x04000647 RID: 1607
	public DialogueUtil.CueData[] cueData;

	// Token: 0x04000648 RID: 1608
	public bool precueFirstCue;

	// Token: 0x04000649 RID: 1609
	public bool presetPosition = true;

	// Token: 0x0400064A RID: 1610
	public DialogueManager.DialogueBoxBackground background;

	// Token: 0x0400064B RID: 1611
	public bool setDialogueCamera = true;

	// Token: 0x0400064C RID: 1612
	private GameObject camera;
}
