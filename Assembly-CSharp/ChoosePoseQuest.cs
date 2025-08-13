using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000224 RID: 548
public class ChoosePoseQuest : MonoBehaviour
{
	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x00038EB5 File Offset: 0x000370B5
	// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x00038EC8 File Offset: 0x000370C8
	private bool State
	{
		get
		{
			return GameData.g.ReadBool(this.id, false);
		}
		set
		{
			GameData.g.Write(this.id, value);
		}
	}

	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x00038EDB File Offset: 0x000370DB
	// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x00038EEE File Offset: 0x000370EE
	private int Pose
	{
		get
		{
			return GameData.g.ReadInt(this.poseId, 0);
		}
		set
		{
			GameData.g.Write(this.poseId, value);
		}
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x00038F01 File Offset: 0x00037101
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x00038F0C File Offset: 0x0003710C
	private void UpdateState()
	{
		bool state = this.State;
		if (this.crier != null)
		{
			this.crier.SetActive(!state);
		}
		foreach (DialogueActor dialogueActor in this.actors)
		{
			if (state)
			{
				dialogueActor.SetStateString(this.poses[this.Pose]);
			}
			else if (!string.IsNullOrEmpty(this.beforeState))
			{
				dialogueActor.SetStateString(this.beforeState);
			}
		}
	}

	// Token: 0x06000BC6 RID: 3014 RVA: 0x00038F87 File Offset: 0x00037187
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x00038F96 File Offset: 0x00037196
	private IEnumerator RunConversation()
	{
		DialogueManager.d.CancelBubble();
		if (!this.State)
		{
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.promptText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			while (DialogueManager.optionChosen >= this.poses.Length)
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.repeatText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			this.Pose = DialogueManager.optionChosen;
			this.State = true;
			this.UpdateState();
			if (!string.IsNullOrEmpty(this.rewardText))
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.rewardText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			this.onReward.Invoke();
		}
		else
		{
			if (!string.IsNullOrEmpty(this.afterText))
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.afterText, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			this.onAfter.Invoke();
		}
		yield return null;
		yield break;
	}

	// Token: 0x04000F86 RID: 3974
	public string id;

	// Token: 0x04000F87 RID: 3975
	public string poseId;

	// Token: 0x04000F88 RID: 3976
	public string[] poses;

	// Token: 0x04000F89 RID: 3977
	public DialogueActor[] actors;

	// Token: 0x04000F8A RID: 3978
	public GameObject crier;

	// Token: 0x04000F8B RID: 3979
	public string beforeState;

	// Token: 0x04000F8C RID: 3980
	public string promptText;

	// Token: 0x04000F8D RID: 3981
	public string repeatText;

	// Token: 0x04000F8E RID: 3982
	public string rewardText;

	// Token: 0x04000F8F RID: 3983
	public UnityEvent onReward;

	// Token: 0x04000F90 RID: 3984
	public string afterText;

	// Token: 0x04000F91 RID: 3985
	public UnityEvent onAfter;
}
