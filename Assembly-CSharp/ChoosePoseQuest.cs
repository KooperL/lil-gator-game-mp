using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020002D3 RID: 723
public class ChoosePoseQuest : MonoBehaviour
{
	// Token: 0x17000187 RID: 391
	// (get) Token: 0x06000E15 RID: 3605 RVA: 0x0000C8D6 File Offset: 0x0000AAD6
	// (set) Token: 0x06000E16 RID: 3606 RVA: 0x0000C8E9 File Offset: 0x0000AAE9
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

	// Token: 0x17000188 RID: 392
	// (get) Token: 0x06000E17 RID: 3607 RVA: 0x0000C8FC File Offset: 0x0000AAFC
	// (set) Token: 0x06000E18 RID: 3608 RVA: 0x0000C90F File Offset: 0x0000AB0F
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

	// Token: 0x06000E19 RID: 3609 RVA: 0x0000C922 File Offset: 0x0000AB22
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000E1A RID: 3610 RVA: 0x0004B720 File Offset: 0x00049920
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

	// Token: 0x06000E1B RID: 3611 RVA: 0x0000C92A File Offset: 0x0000AB2A
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x0000C939 File Offset: 0x0000AB39
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

	// Token: 0x04001249 RID: 4681
	public string id;

	// Token: 0x0400124A RID: 4682
	public string poseId;

	// Token: 0x0400124B RID: 4683
	public string[] poses;

	// Token: 0x0400124C RID: 4684
	public DialogueActor[] actors;

	// Token: 0x0400124D RID: 4685
	public GameObject crier;

	// Token: 0x0400124E RID: 4686
	public string beforeState;

	// Token: 0x0400124F RID: 4687
	public string promptText;

	// Token: 0x04001250 RID: 4688
	public string repeatText;

	// Token: 0x04001251 RID: 4689
	public string rewardText;

	// Token: 0x04001252 RID: 4690
	public UnityEvent onReward;

	// Token: 0x04001253 RID: 4691
	public string afterText;

	// Token: 0x04001254 RID: 4692
	public UnityEvent onAfter;
}
