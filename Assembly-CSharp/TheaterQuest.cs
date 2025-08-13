using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002DC RID: 732
public class TheaterQuest : MonoBehaviour
{
	// Token: 0x17000195 RID: 405
	// (get) Token: 0x06000E4C RID: 3660 RVA: 0x0000CAEC File Offset: 0x0000ACEC
	private string StateID
	{
		get
		{
			return "TheaterState";
		}
	}

	// Token: 0x17000196 RID: 406
	// (get) Token: 0x06000E4D RID: 3661 RVA: 0x0000CAF3 File Offset: 0x0000ACF3
	// (set) Token: 0x06000E4E RID: 3662 RVA: 0x0000CB06 File Offset: 0x0000AD06
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

	// Token: 0x06000E4F RID: 3663 RVA: 0x0000CB19 File Offset: 0x0000AD19
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000E50 RID: 3664 RVA: 0x0004BF04 File Offset: 0x0004A104
	private void UpdateState()
	{
		int state = this.State;
		this.aTextAlert.SetActive(state == 0);
		this.aRamble.SetActive(state < 10);
		this.aCousin.SetActive(state < 10);
	}

	// Token: 0x06000E51 RID: 3665 RVA: 0x0000CB21 File Offset: 0x0000AD21
	public void TextAlert()
	{
		base.StartCoroutine(DialogueManager.d.LoadChunkPhone("Theater_TextAlert", this.frogProfiles, true, true, null));
		this.State = 1;
		this.UpdateState();
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x0000CB4F File Offset: 0x0000AD4F
	public void Trigger()
	{
		int state = this.State;
	}

	// Token: 0x06000E53 RID: 3667 RVA: 0x0000CB58 File Offset: 0x0000AD58
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

	// Token: 0x06000E54 RID: 3668 RVA: 0x0000CB67 File Offset: 0x0000AD67
	public void CheckSubQuests()
	{
		base.StartCoroutine(DialogueManager.d.LoadChunkPhone("Theater_B_Finish", this.frogProfiles, true, true, null));
		this.State = 20;
		this.UpdateState();
	}

	// Token: 0x06000E55 RID: 3669 RVA: 0x0000CB96 File Offset: 0x0000AD96
	private IEnumerator RunConversationC()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Theater_C1", this.aActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.State = 30;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000E56 RID: 3670 RVA: 0x0000CBA5 File Offset: 0x0000ADA5
	private IEnumerator RunConversationD()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Theater_D1", this.aActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.State = 40;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000E57 RID: 3671 RVA: 0x0000CBB4 File Offset: 0x0000ADB4
	private IEnumerator RunConversationE()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Theater_E1", this.aActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.State = 50;
		this.UpdateState();
		yield break;
	}

	// Token: 0x04001288 RID: 4744
	public CharacterProfile[] frogProfiles;

	// Token: 0x04001289 RID: 4745
	[Header("State A")]
	public GameObject aTextAlert;

	// Token: 0x0400128A RID: 4746
	public GameObject aRamble;

	// Token: 0x0400128B RID: 4747
	public GameObject aCousin;

	// Token: 0x0400128C RID: 4748
	public DialogueActor[] aActors;
}
