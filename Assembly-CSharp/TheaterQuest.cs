using System;
using System.Collections;
using UnityEngine;

public class TheaterQuest : MonoBehaviour
{
	// (get) Token: 0x06000E98 RID: 3736 RVA: 0x0000CDF4 File Offset: 0x0000AFF4
	private string StateID
	{
		get
		{
			return "TheaterState";
		}
	}

	// (get) Token: 0x06000E99 RID: 3737 RVA: 0x0000CDFB File Offset: 0x0000AFFB
	// (set) Token: 0x06000E9A RID: 3738 RVA: 0x0000CE0E File Offset: 0x0000B00E
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

	// Token: 0x06000E9B RID: 3739 RVA: 0x0000CE21 File Offset: 0x0000B021
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000E9C RID: 3740 RVA: 0x0004DA8C File Offset: 0x0004BC8C
	private void UpdateState()
	{
		int state = this.State;
		this.aTextAlert.SetActive(state == 0);
		this.aRamble.SetActive(state < 10);
		this.aCousin.SetActive(state < 10);
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x0000CE29 File Offset: 0x0000B029
	public void TextAlert()
	{
		base.StartCoroutine(DialogueManager.d.LoadChunkPhone("Theater_TextAlert", this.frogProfiles, true, true, null));
		this.State = 1;
		this.UpdateState();
	}

	// Token: 0x06000E9E RID: 3742 RVA: 0x0000CE57 File Offset: 0x0000B057
	public void Trigger()
	{
		int state = this.State;
	}

	// Token: 0x06000E9F RID: 3743 RVA: 0x0000CE60 File Offset: 0x0000B060
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

	// Token: 0x06000EA0 RID: 3744 RVA: 0x0000CE6F File Offset: 0x0000B06F
	public void CheckSubQuests()
	{
		base.StartCoroutine(DialogueManager.d.LoadChunkPhone("Theater_B_Finish", this.frogProfiles, true, true, null));
		this.State = 20;
		this.UpdateState();
	}

	// Token: 0x06000EA1 RID: 3745 RVA: 0x0000CE9E File Offset: 0x0000B09E
	private IEnumerator RunConversationC()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Theater_C1", this.aActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.State = 30;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000EA2 RID: 3746 RVA: 0x0000CEAD File Offset: 0x0000B0AD
	private IEnumerator RunConversationD()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Theater_D1", this.aActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.State = 40;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000EA3 RID: 3747 RVA: 0x0000CEBC File Offset: 0x0000B0BC
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
