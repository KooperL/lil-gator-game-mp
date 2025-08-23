using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Dialogue Sequence/Dialogue")]
public class DSDialogue : DialogueSequence, ICueable
{
	// Token: 0x060005A8 RID: 1448 RVA: 0x000060EA File Offset: 0x000042EA
	public void SetCamera(GameObject camera)
	{
		this.camera = camera;
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x0002F660 File Offset: 0x0002D860
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

	// Token: 0x060005AA RID: 1450 RVA: 0x0002F720 File Offset: 0x0002D920
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

	// Token: 0x060005AB RID: 1451 RVA: 0x0002F7D0 File Offset: 0x0002D9D0
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

	// Token: 0x060005AC RID: 1452 RVA: 0x0002F830 File Offset: 0x0002DA30
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

	// Token: 0x060005AD RID: 1453 RVA: 0x000060F3 File Offset: 0x000042F3
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

	public MultilingualTextDocument document;

	[ChunkLookup("document")]
	public string dialogue;

	public DialogueActor[] actors;

	[HideInInspector]
	public UnityEvent[] cues;

	public DialogueUtil.CueData[] cueData;

	public bool precueFirstCue;

	public bool presetPosition = true;

	public DialogueManager.DialogueBoxBackground background;

	public bool setDialogueCamera = true;

	private GameObject camera;
}
