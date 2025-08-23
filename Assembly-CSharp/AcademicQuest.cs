using System;
using System.Collections;
using UnityEngine;

public class AcademicQuest : MonoBehaviour
{
	// (get) Token: 0x06000D55 RID: 3413 RVA: 0x0000C3C1 File Offset: 0x0000A5C1
	private string StateID
	{
		get
		{
			return "AcademicState";
		}
	}

	// (get) Token: 0x06000D56 RID: 3414 RVA: 0x0000C3C8 File Offset: 0x0000A5C8
	// (set) Token: 0x06000D57 RID: 3415 RVA: 0x0000C3DB File Offset: 0x0000A5DB
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

	// Token: 0x06000D58 RID: 3416 RVA: 0x0000C3EE File Offset: 0x0000A5EE
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x0004AE60 File Offset: 0x00049060
	private void UpdateState()
	{
		int state = this.State;
		this.aTextAlert.SetActive(state == 0);
		this.aDog.SetActive(state < 10);
		this.dRambling.SetActive(state == 20);
		this.dDog.SetActive(state >= 20);
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x0004AEB8 File Offset: 0x000490B8
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

	// Token: 0x06000D5B RID: 3419 RVA: 0x0004AF10 File Offset: 0x00049110
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

	// Token: 0x06000D5C RID: 3420 RVA: 0x0000C3F6 File Offset: 0x0000A5F6
	public void TextAlert()
	{
		base.StartCoroutine(this.RunTextAlert());
		this.State = 1;
		this.UpdateState();
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x0000C412 File Offset: 0x0000A612
	private IEnumerator RunTextAlert()
	{
		yield return DialogueManager.d.SmallPhone("Academic_TextAlert", this.dogProfiles, true);
		yield break;
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x0000C421 File Offset: 0x0000A621
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

	// Token: 0x06000D5F RID: 3423 RVA: 0x0000C430 File Offset: 0x0000A630
	private IEnumerator RunConversationC()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Academic_C1", null, DialogueManager.DialogueBoxBackground.Standard, true));
		yield return base.StartCoroutine(DialogueManager.d.LoadChunkPhone("Academic_C2", this.dogProfiles, true, true, null));
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Academic_C3", null, DialogueManager.DialogueBoxBackground.Standard, true));
		this.State = 20;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000D60 RID: 3424 RVA: 0x0000C43F File Offset: 0x0000A63F
	private IEnumerator RunConversationD()
	{
		DialogueManager.d.CancelBubble();
		this.dRambling.SetActive(false);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Academic_D1", this.dActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.State = 30;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000D61 RID: 3425 RVA: 0x0000C44E File Offset: 0x0000A64E
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
