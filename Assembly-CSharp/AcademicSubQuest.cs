using System;
using System.Collections;
using UnityEngine;

public class AcademicSubQuest : MonoBehaviour
{
	// (get) Token: 0x06000D81 RID: 3457 RVA: 0x0000C4D0 File Offset: 0x0000A6D0
	// (set) Token: 0x06000D82 RID: 3458 RVA: 0x0000C4E3 File Offset: 0x0000A6E3
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

	// (get) Token: 0x06000D83 RID: 3459 RVA: 0x0000C4F6 File Offset: 0x0000A6F6
	public bool IsComplete
	{
		get
		{
			return this.State == 1;
		}
	}

	// Token: 0x06000D84 RID: 3460 RVA: 0x0000C501 File Offset: 0x0000A701
	private void OnValidate()
	{
		if (this.academicQuest == null)
		{
			this.academicQuest = global::UnityEngine.Object.FindObjectOfType<AcademicQuest>();
		}
	}

	// Token: 0x06000D85 RID: 3461 RVA: 0x0000C51C File Offset: 0x0000A71C
	public void Conversation()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000D86 RID: 3462 RVA: 0x0000C52B File Offset: 0x0000A72B
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

	// Token: 0x06000D87 RID: 3463 RVA: 0x00002229 File Offset: 0x00000429
	public void UpdateState()
	{
	}

	public string stateID;

	public AcademicQuest academicQuest;

	public DialogueActor[] actors;
}
