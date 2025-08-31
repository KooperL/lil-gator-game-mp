using System;
using System.Collections;
using UnityEngine;

public class AcademicQuest : MonoBehaviour
{
	// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00037FBD File Offset: 0x000361BD
	private string StateID
	{
		get
		{
			return "AcademicState";
		}
	}

	// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00037FC4 File Offset: 0x000361C4
	// (set) Token: 0x06000B3F RID: 2879 RVA: 0x00037FD7 File Offset: 0x000361D7
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

	// Token: 0x06000B40 RID: 2880 RVA: 0x00037FEA File Offset: 0x000361EA
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000B41 RID: 2881 RVA: 0x00037FF4 File Offset: 0x000361F4
	private void UpdateState()
	{
		int state = this.State;
		this.aTextAlert.SetActive(state == 0);
		this.aDog.SetActive(state < 10);
		this.dRambling.SetActive(state == 20);
		this.dDog.SetActive(state >= 20);
	}

	// Token: 0x06000B42 RID: 2882 RVA: 0x0003804C File Offset: 0x0003624C
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

	// Token: 0x06000B43 RID: 2883 RVA: 0x000380A4 File Offset: 0x000362A4
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

	// Token: 0x06000B44 RID: 2884 RVA: 0x000380EB File Offset: 0x000362EB
	public void TextAlert()
	{
		base.StartCoroutine(this.RunTextAlert());
		this.State = 1;
		this.UpdateState();
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x00038107 File Offset: 0x00036307
	private IEnumerator RunTextAlert()
	{
		yield return DialogueManager.d.SmallPhone("Academic_TextAlert", this.dogProfiles, true);
		yield break;
	}

	// Token: 0x06000B46 RID: 2886 RVA: 0x00038116 File Offset: 0x00036316
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

	// Token: 0x06000B47 RID: 2887 RVA: 0x00038125 File Offset: 0x00036325
	private IEnumerator RunConversationC()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Academic_C1", null, DialogueManager.DialogueBoxBackground.Standard, true));
		yield return base.StartCoroutine(DialogueManager.d.LoadChunkPhone("Academic_C2", this.dogProfiles, true, true, null));
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Academic_C3", null, DialogueManager.DialogueBoxBackground.Standard, true));
		this.State = 20;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x00038134 File Offset: 0x00036334
	private IEnumerator RunConversationD()
	{
		DialogueManager.d.CancelBubble();
		this.dRambling.SetActive(false);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Academic_D1", this.dActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.State = 30;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x00038143 File Offset: 0x00036343
	private IEnumerator RunConversationE()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Academic_E1", this.dActors, DialogueManager.DialogueBoxBackground.Standard, true));
		yield break;
	}

	public CharacterProfile[] dogProfiles;

	[Header("State A")]
	public GameObject aTextAlert;

	public GameObject aDog;

	public DialogueActor[] aActors;

	[Header("State B")]
	public AcademicSubQuest[] academicSubQuests;

	[Header("State D")]
	public GameObject dRambling;

	public GameObject dDog;

	public DialogueActor[] dActors;
}
