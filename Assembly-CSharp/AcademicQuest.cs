using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200029F RID: 671
public class AcademicQuest : MonoBehaviour
{
	// Token: 0x1700014B RID: 331
	// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0000C0AF File Offset: 0x0000A2AF
	private string StateID
	{
		get
		{
			return "AcademicState";
		}
	}

	// Token: 0x1700014C RID: 332
	// (get) Token: 0x06000D09 RID: 3337 RVA: 0x0000C0B6 File Offset: 0x0000A2B6
	// (set) Token: 0x06000D0A RID: 3338 RVA: 0x0000C0C9 File Offset: 0x0000A2C9
	public int State
	{
		get
		{
			return GameData.g.ReadInt(this.StateID, 0);
		}
		set
		{
			GameData.g.Write(this.StateID, value);
		}
	}

	// Token: 0x06000D0B RID: 3339 RVA: 0x0000C0DC File Offset: 0x0000A2DC
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000D0C RID: 3340 RVA: 0x00049010 File Offset: 0x00047210
	private void UpdateState()
	{
		int state = this.State;
		this.aTextAlert.SetActive(state == 0);
		this.aDog.SetActive(state < 10);
		this.dRambling.SetActive(state == 20);
		this.dDog.SetActive(state >= 20);
	}

	// Token: 0x06000D0D RID: 3341 RVA: 0x00049068 File Offset: 0x00047268
	public void CheckSubQuests()
	{
		int num = 0;
		for (int i = 0; i < this.academicSubQuests.Length; i++)
		{
			if (this.academicSubQuests[i].IsComplete)
			{
				num++;
			}
		}
		if (num == this.academicSubQuests.Length && this.State < 20)
		{
			base.StartCoroutine(this.RunConversationC());
		}
	}

	// Token: 0x06000D0E RID: 3342 RVA: 0x000490C0 File Offset: 0x000472C0
	public void Conversation()
	{
		int state = this.State;
		if (state < 10)
		{
			base.StartCoroutine(this.RunConversationA());
			return;
		}
		if (state < 30)
		{
			base.StartCoroutine(this.RunConversationD());
			return;
		}
		base.StartCoroutine(this.RunConversationE());
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x0000C0E4 File Offset: 0x0000A2E4
	public void TextAlert()
	{
		base.StartCoroutine(this.RunTextAlert());
		this.State = 1;
		this.UpdateState();
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x0000C100 File Offset: 0x0000A300
	private IEnumerator RunTextAlert()
	{
		yield return DialogueManager.d.SmallPhone("Academic_TextAlert", this.dogProfiles, true);
		yield break;
	}

	// Token: 0x06000D11 RID: 3345 RVA: 0x0000C10F File Offset: 0x0000A30F
	public IEnumerator RunConversationA()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Academic_A1", this.aActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.aDog.SetActive(false);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Academic_A2", this.aActors, DialogueManager.DialogueBoxBackground.Standard, true));
		yield return base.StartCoroutine(DialogueManager.d.LoadChunkPhone("Academic_A3", this.dogProfiles, true, true, null));
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Academic_A4", this.aActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.State = 10;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000D12 RID: 3346 RVA: 0x0000C11E File Offset: 0x0000A31E
	private IEnumerator RunConversationC()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Academic_C1", null, DialogueManager.DialogueBoxBackground.Standard, true));
		yield return base.StartCoroutine(DialogueManager.d.LoadChunkPhone("Academic_C2", this.dogProfiles, true, true, null));
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Academic_C3", null, DialogueManager.DialogueBoxBackground.Standard, true));
		this.State = 20;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000D13 RID: 3347 RVA: 0x0000C12D File Offset: 0x0000A32D
	private IEnumerator RunConversationD()
	{
		DialogueManager.d.CancelBubble();
		this.dRambling.SetActive(false);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Academic_D1", this.dActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.State = 30;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000D14 RID: 3348 RVA: 0x0000C13C File Offset: 0x0000A33C
	private IEnumerator RunConversationE()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Academic_E1", this.dActors, DialogueManager.DialogueBoxBackground.Standard, true));
		yield break;
	}

	// Token: 0x04001163 RID: 4451
	public CharacterProfile[] dogProfiles;

	// Token: 0x04001164 RID: 4452
	[Header("State A")]
	public GameObject aTextAlert;

	// Token: 0x04001165 RID: 4453
	public GameObject aDog;

	// Token: 0x04001166 RID: 4454
	public DialogueActor[] aActors;

	// Token: 0x04001167 RID: 4455
	[Header("State B")]
	public AcademicSubQuest[] academicSubQuests;

	// Token: 0x04001168 RID: 4456
	[Header("State D")]
	public GameObject dRambling;

	// Token: 0x04001169 RID: 4457
	public GameObject dDog;

	// Token: 0x0400116A RID: 4458
	public DialogueActor[] dActors;
}
