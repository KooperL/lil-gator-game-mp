using System;
using System.Collections;
using UnityEngine;

public class TheaterQuest : MonoBehaviour
{
	// (get) Token: 0x06000E98 RID: 3736 RVA: 0x0000CDDF File Offset: 0x0000AFDF
	private string StateID
	{
		get
		{
			return "TheaterState";
		}
	}

	// (get) Token: 0x06000E99 RID: 3737 RVA: 0x0000CDE6 File Offset: 0x0000AFE6
	// (set) Token: 0x06000E9A RID: 3738 RVA: 0x0000CDF9 File Offset: 0x0000AFF9
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

	// Token: 0x06000E9B RID: 3739 RVA: 0x0000CE0C File Offset: 0x0000B00C
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000E9C RID: 3740 RVA: 0x0004D8F8 File Offset: 0x0004BAF8
	private void UpdateState()
	{
		int state = this.State;
		this.aTextAlert.SetActive(state == 0);
		this.aRamble.SetActive(state < 10);
		this.aCousin.SetActive(state < 10);
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x0000CE14 File Offset: 0x0000B014
	public void TextAlert()
	{
		base.StartCoroutine(DialogueManager.d.LoadChunkPhone("Theater_TextAlert", this.frogProfiles, true, true, null));
		this.State = 1;
		this.UpdateState();
	}

	// Token: 0x06000E9E RID: 3742 RVA: 0x0000CE42 File Offset: 0x0000B042
	public void Trigger()
	{
		int state = this.State;
	}

	// Token: 0x06000E9F RID: 3743 RVA: 0x0000CE4B File Offset: 0x0000B04B
	public IEnumerator RunConversationA()
	{
		this.aRamble.SetActive(false);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Theater_A1", this.aActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.aCousin.SetActive(false);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Theater_A2", this.aActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.State = 10;
		this.UpdateState();
		this.CheckSubQuests();
		yield break;
	}

	// Token: 0x06000EA0 RID: 3744 RVA: 0x0000CE5A File Offset: 0x0000B05A
	public void CheckSubQuests()
	{
		base.StartCoroutine(DialogueManager.d.LoadChunkPhone("Theater_B_Finish", this.frogProfiles, true, true, null));
		this.State = 20;
		this.UpdateState();
	}

	// Token: 0x06000EA1 RID: 3745 RVA: 0x0000CE89 File Offset: 0x0000B089
	private IEnumerator RunConversationC()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Theater_C1", this.aActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.State = 30;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000EA2 RID: 3746 RVA: 0x0000CE98 File Offset: 0x0000B098
	private IEnumerator RunConversationD()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Theater_D1", this.aActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.State = 40;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000EA3 RID: 3747 RVA: 0x0000CEA7 File Offset: 0x0000B0A7
	private IEnumerator RunConversationE()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Theater_E1", this.aActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.State = 50;
		this.UpdateState();
		yield break;
	}

	public CharacterProfile[] frogProfiles;

	[Header("State A")]
	public GameObject aTextAlert;

	public GameObject aRamble;

	public GameObject aCousin;

	public DialogueActor[] aActors;
}
