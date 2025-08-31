using System;
using System.Collections;
using UnityEngine;

public class TheaterQuest : MonoBehaviour
{
	// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0003928F File Offset: 0x0003748F
	private string StateID
	{
		get
		{
			return "TheaterState";
		}
	}

	// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x00039296 File Offset: 0x00037496
	// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x000392A9 File Offset: 0x000374A9
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

	// Token: 0x06000BE6 RID: 3046 RVA: 0x000392BC File Offset: 0x000374BC
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000BE7 RID: 3047 RVA: 0x000392C4 File Offset: 0x000374C4
	private void UpdateState()
	{
		int state = this.State;
		this.aTextAlert.SetActive(state == 0);
		this.aRamble.SetActive(state < 10);
		this.aCousin.SetActive(state < 10);
	}

	// Token: 0x06000BE8 RID: 3048 RVA: 0x00039307 File Offset: 0x00037507
	public void TextAlert()
	{
		base.StartCoroutine(DialogueManager.d.LoadChunkPhone("Theater_TextAlert", this.frogProfiles, true, true, null));
		this.State = 1;
		this.UpdateState();
	}

	// Token: 0x06000BE9 RID: 3049 RVA: 0x00039335 File Offset: 0x00037535
	public void Trigger()
	{
		int state = this.State;
	}

	// Token: 0x06000BEA RID: 3050 RVA: 0x0003933E File Offset: 0x0003753E
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

	// Token: 0x06000BEB RID: 3051 RVA: 0x0003934D File Offset: 0x0003754D
	public void CheckSubQuests()
	{
		base.StartCoroutine(DialogueManager.d.LoadChunkPhone("Theater_B_Finish", this.frogProfiles, true, true, null));
		this.State = 20;
		this.UpdateState();
	}

	// Token: 0x06000BEC RID: 3052 RVA: 0x0003937C File Offset: 0x0003757C
	private IEnumerator RunConversationC()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Theater_C1", this.aActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.State = 30;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000BED RID: 3053 RVA: 0x0003938B File Offset: 0x0003758B
	private IEnumerator RunConversationD()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Theater_D1", this.aActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.State = 40;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000BEE RID: 3054 RVA: 0x0003939A File Offset: 0x0003759A
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
