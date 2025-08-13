using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002A5 RID: 677
public class AcademicSubQuest : MonoBehaviour
{
	// Token: 0x17000157 RID: 343
	// (get) Token: 0x06000D34 RID: 3380 RVA: 0x0000C1BE File Offset: 0x0000A3BE
	// (set) Token: 0x06000D35 RID: 3381 RVA: 0x0000C1D1 File Offset: 0x0000A3D1
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

	// Token: 0x17000158 RID: 344
	// (get) Token: 0x06000D36 RID: 3382 RVA: 0x0000C1E4 File Offset: 0x0000A3E4
	public bool IsComplete
	{
		get
		{
			return this.State == 1;
		}
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x0000C1EF File Offset: 0x0000A3EF
	private void OnValidate()
	{
		if (this.academicQuest == null)
		{
			this.academicQuest = Object.FindObjectOfType<AcademicQuest>();
		}
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x0000C20A File Offset: 0x0000A40A
	public void Conversation()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000D39 RID: 3385 RVA: 0x0000C219 File Offset: 0x0000A419
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

	// Token: 0x06000D3A RID: 3386 RVA: 0x00002229 File Offset: 0x00000429
	public void UpdateState()
	{
	}

	// Token: 0x0400117A RID: 4474
	public string stateID;

	// Token: 0x0400117B RID: 4475
	public AcademicQuest academicQuest;

	// Token: 0x0400117C RID: 4476
	public DialogueActor[] actors;
}
