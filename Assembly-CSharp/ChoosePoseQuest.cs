using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ChoosePoseQuest : MonoBehaviour
{
	// (get) Token: 0x06000E61 RID: 3681 RVA: 0x0000CBC9 File Offset: 0x0000ADC9
	// (set) Token: 0x06000E62 RID: 3682 RVA: 0x0000CBDC File Offset: 0x0000ADDC
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

	// (get) Token: 0x06000E63 RID: 3683 RVA: 0x0000CBEF File Offset: 0x0000ADEF
	// (set) Token: 0x06000E64 RID: 3684 RVA: 0x0000CC02 File Offset: 0x0000AE02
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

	// Token: 0x06000E65 RID: 3685 RVA: 0x0000CC15 File Offset: 0x0000AE15
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000E66 RID: 3686 RVA: 0x0004D114 File Offset: 0x0004B314
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

	// Token: 0x06000E67 RID: 3687 RVA: 0x0000CC1D File Offset: 0x0000AE1D
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000E68 RID: 3688 RVA: 0x0000CC2C File Offset: 0x0000AE2C
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

	public string id;

	public string poseId;

	public string[] poses;

	public DialogueActor[] actors;

	public GameObject crier;

	public string beforeState;

	public string promptText;

	public string repeatText;

	public string rewardText;

	public UnityEvent onReward;

	public string afterText;

	public UnityEvent onAfter;
}
