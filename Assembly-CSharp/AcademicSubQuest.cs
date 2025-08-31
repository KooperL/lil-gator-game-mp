using System;
using System.Collections;
using UnityEngine;

public class AcademicSubQuest : MonoBehaviour
{
	// (get) Token: 0x06000B4B RID: 2891 RVA: 0x0003815A File Offset: 0x0003635A
	// (set) Token: 0x06000B4C RID: 2892 RVA: 0x0003816D File Offset: 0x0003636D
	private int State
	{
		get
		{
			return GameData.g.ReadInt(this.stateID, 0);
		}
		set
		{
			GameData.g.Write(this.stateID, value);
		}
	}

	// (get) Token: 0x06000B4D RID: 2893 RVA: 0x00038180 File Offset: 0x00036380
	public bool IsComplete
	{
		get
		{
			return this.State == 1;
		}
	}

	// Token: 0x06000B4E RID: 2894 RVA: 0x0003818B File Offset: 0x0003638B
	private void OnValidate()
	{
		if (this.academicQuest == null)
		{
			this.academicQuest = Object.FindObjectOfType<AcademicQuest>();
		}
	}

	// Token: 0x06000B4F RID: 2895 RVA: 0x000381A6 File Offset: 0x000363A6
	public void Conversation()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000B50 RID: 2896 RVA: 0x000381B5 File Offset: 0x000363B5
	public IEnumerator RunConversation()
	{
		Game.DialogueDepth++;
		if (this.academicQuest.State < 10)
		{
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Academic_BTemp-1", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
		}
		else
		{
			int state = this.State;
			if (state != 0)
			{
				if (state == 1)
				{
					yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Academic_BTempDone", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
				}
			}
			else
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Academic_BTemp0", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
				yield return Blackout.FadeIn();
				this.State = 1;
				this.UpdateState();
				yield return Blackout.FadeOut();
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Academic_BTemp1", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
				this.academicQuest.CheckSubQuests();
			}
		}
		Game.DialogueDepth--;
		yield break;
	}

	// Token: 0x06000B51 RID: 2897 RVA: 0x000381C4 File Offset: 0x000363C4
	public void UpdateState()
	{
	}

	public string stateID;

	public AcademicQuest academicQuest;

	public DialogueActor[] actors;
}
