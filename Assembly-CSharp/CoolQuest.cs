using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CoolQuest : MonoBehaviour
{
	// (get) Token: 0x06000DAF RID: 3503 RVA: 0x0000C634 File Offset: 0x0000A834
	private string StateID
	{
		get
		{
			return "CoolState";
		}
	}

	// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x0000C63B File Offset: 0x0000A83B
	// (set) Token: 0x06000DB1 RID: 3505 RVA: 0x0000C64E File Offset: 0x0000A84E
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

	// Token: 0x06000DB2 RID: 3506 RVA: 0x0000C661 File Offset: 0x0000A861
	private void OnEnable()
	{
		CoolQuest.c = this;
	}

	// Token: 0x06000DB3 RID: 3507 RVA: 0x0000C669 File Offset: 0x0000A869
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000DB4 RID: 3508 RVA: 0x0004B5F4 File Offset: 0x000497F4
	private void UpdateState()
	{
		int state = this.State;
		this.aTextAlert.SetActive(state == 0);
		this.bTrigger.SetActive(state == 10);
		this.dHorse.SetActive(state == 30);
		this.boringCoolKids.SetActive(state < 40);
		this.funCoolKids.SetActive(state >= 40);
	}

	// Token: 0x06000DB5 RID: 3509 RVA: 0x0000C671 File Offset: 0x0000A871
	public void Wolf()
	{
		if (this.State < 40)
		{
			base.StartCoroutine(this.WolfA());
			return;
		}
		DialogueManager.d.Bubble("Cool_E_Wolf", this.eWolf, 0f, false, true, true);
	}

	// Token: 0x06000DB6 RID: 3510 RVA: 0x0000C6A9 File Offset: 0x0000A8A9
	private IEnumerator WolfA()
	{
		if (this.State < 10)
		{
			yield return base.StartCoroutine(this.RunConversationA());
		}
		do
		{
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_A_Wolf", this.aWolf, DialogueManager.DialogueBoxBackground.Standard, true));
			switch (DialogueManager.optionChosen)
			{
			case 0:
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_A_Wolf_0", this.aWolf, DialogueManager.DialogueBoxBackground.Standard, true));
				break;
			case 1:
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_A_Wolf_1", this.aWolf, DialogueManager.DialogueBoxBackground.Standard, true));
				break;
			case 2:
				yield return DialogueManager.d.Bubble("Cool_A_Wolf_2", this.aWolf, 0f, false, true, true);
				break;
			}
		}
		while (DialogueManager.optionChosen != 2);
		yield break;
	}

	// Token: 0x06000DB7 RID: 3511 RVA: 0x0000C6B8 File Offset: 0x0000A8B8
	public void Boar()
	{
		if (this.State < 40)
		{
			base.StartCoroutine(this.BoarA());
			return;
		}
		DialogueManager.d.Bubble("Cool_E_Boar", this.eBoar, 0f, false, true, true);
	}

	// Token: 0x06000DB8 RID: 3512 RVA: 0x0000C6F0 File Offset: 0x0000A8F0
	private IEnumerator BoarA()
	{
		if (this.State < 10)
		{
			yield return base.StartCoroutine(this.RunConversationA());
		}
		do
		{
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_A_Boar", this.aBoar, DialogueManager.DialogueBoxBackground.Standard, true));
			switch (DialogueManager.optionChosen)
			{
			case 0:
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_A_Boar_0", this.aBoar, DialogueManager.DialogueBoxBackground.Standard, true));
				break;
			case 1:
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_A_Boar_1", this.aBoar, DialogueManager.DialogueBoxBackground.Standard, true));
				break;
			case 2:
				yield return DialogueManager.d.Bubble("Cool_A_Boar_2", this.aBoar, 0f, false, true, true);
				break;
			}
		}
		while (DialogueManager.optionChosen != 2);
		yield break;
	}

	// Token: 0x06000DB9 RID: 3513 RVA: 0x0000C6FF File Offset: 0x0000A8FF
	public void Goose()
	{
		if (this.State < 40)
		{
			base.StartCoroutine(this.GooseA());
			return;
		}
		DialogueManager.d.Bubble("Cool_E_Goose", this.eGoose, 0f, false, true, true);
	}

	// Token: 0x06000DBA RID: 3514 RVA: 0x0000C737 File Offset: 0x0000A937
	private IEnumerator GooseA()
	{
		if (this.State < 10)
		{
			yield return base.StartCoroutine(this.RunConversationA());
		}
		do
		{
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_A_Goose", this.aGoose, DialogueManager.DialogueBoxBackground.Standard, true));
			switch (DialogueManager.optionChosen)
			{
			case 0:
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_A_Goose_0", this.aGoose, DialogueManager.DialogueBoxBackground.Standard, true));
				break;
			case 1:
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_A_Goose_1", this.aGoose, DialogueManager.DialogueBoxBackground.Standard, true));
				break;
			case 2:
				yield return DialogueManager.d.Bubble("Cool_A_Goose_2", this.aGoose, 0f, false, true, true);
				break;
			}
		}
		while (DialogueManager.optionChosen != 2);
		yield break;
	}

	// Token: 0x06000DBB RID: 3515 RVA: 0x0004B65C File Offset: 0x0004985C
	public void Horse()
	{
		int state = this.State;
		if (state == 30)
		{
			base.StartCoroutine(this.RunConversationD());
			return;
		}
		if (state == 40)
		{
			base.StartCoroutine(this.RunConversationE());
			return;
		}
		DialogueManager.d.Bubble("Cool_E_Horse", this.eHorse, 0f, false, true, true);
	}

	// Token: 0x06000DBC RID: 3516 RVA: 0x0000C746 File Offset: 0x0000A946
	public void TextAlert()
	{
		base.StartCoroutine(this.RunTextAlert());
		this.State = 1;
		this.UpdateState();
	}

	// Token: 0x06000DBD RID: 3517 RVA: 0x0000C762 File Offset: 0x0000A962
	private IEnumerator RunTextAlert()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunkPhone("Cool_TextAlert1", this.unknownProfiles, true, true, null));
		yield return DialogueManager.d.Bubble("Cool_TextAlert2", null, 0f, true, true, true);
		yield break;
	}

	// Token: 0x06000DBE RID: 3518 RVA: 0x0000C771 File Offset: 0x0000A971
	public IEnumerator RunConversationA()
	{
		Game.DialogueDepth++;
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_A1", this.aActors, DialogueManager.DialogueBoxBackground.Standard, true));
		yield return Blackout.FadeIn();
		this.flashback.SetActive(true);
		yield return Blackout.FadeOut();
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_A2", this.aActors, DialogueManager.DialogueBoxBackground.Standard, true));
		yield return Blackout.FadeIn();
		this.flashback.SetActive(false);
		yield return Blackout.FadeOut();
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_A3", this.aActors, DialogueManager.DialogueBoxBackground.Standard, true));
		Game.DialogueDepth--;
		this.State = 10;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000DBF RID: 3519 RVA: 0x0000C780 File Offset: 0x0000A980
	public void ConversationB()
	{
		base.StartCoroutine(this.RunConversationB());
	}

	// Token: 0x06000DC0 RID: 3520 RVA: 0x0000C78F File Offset: 0x0000A98F
	private IEnumerator RunConversationB()
	{
		yield return DialogueManager.d.Bubble("Cool_B1", null, 0f, true, true, true);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunkPhone("Cool_B2", this.unknownProfiles, true, true, null));
		this.State = 20;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000DC1 RID: 3521 RVA: 0x0000C79E File Offset: 0x0000A99E
	public IEnumerator CheckPumps()
	{
		int num = 0;
		for (int i = 0; i < this.waterPumps.Length; i++)
		{
			if (this.waterPumps[i].activated)
			{
				num++;
			}
		}
		switch (num)
		{
		case 1:
			if (this.State < 21)
			{
				this.State = 21;
				yield return DialogueManager.d.Bubble("Cool_C1_1", null, 0f, false, true, true);
				yield return DialogueManager.d.SmallPhone("Cool_C1_2", this.unknownProfiles, false);
			}
			break;
		case 2:
			if (this.State < 22)
			{
				this.State = 22;
				yield return DialogueManager.d.Bubble("Cool_C2_1", null, 0f, false, true, true);
				yield return DialogueManager.d.SmallPhone("Cool_C2_2", this.unknownProfiles, false);
			}
			break;
		case 3:
			if (this.State < 30)
			{
				this.State = 30;
				this.UpdateState();
				yield return DialogueManager.d.Bubble("Cool_C3_1", null, 0f, false, true, true);
				yield return DialogueManager.d.SmallPhone("Cool_C3_2", this.unknownProfiles, false);
				yield return DialogueManager.d.Bubble("Cool_C3_3", null, 0f, false, true, true);
			}
			break;
		default:
			yield return null;
			break;
		}
		yield break;
	}

	// Token: 0x06000DC2 RID: 3522 RVA: 0x0000C7AD File Offset: 0x0000A9AD
	private IEnumerator RunConversationD()
	{
		Game.DialogueDepth++;
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_D1", this.dActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.dCameraSplash.SetActive(true);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_D2", this.dActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.splashPadWater.SetActive(true);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_D3", this.aActors, DialogueManager.DialogueBoxBackground.Standard, true));
		yield return Blackout.FadeIn();
		this.boringCoolKids.SetActive(false);
		yield return Blackout.FadeOut();
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_D4", this.dActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.dCameraMountain.SetActive(true);
		this.dCameraSplash.SetActive(false);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_D5", this.dActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.funCoolKids.SetActive(true);
		this.dHorse.SetActive(false);
		this.dCameraJarl.SetActive(true);
		this.dCameraMountain.SetActive(false);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_D6", this.dActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.dCameraJarl.SetActive(false);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_D7", this.dActors, DialogueManager.DialogueBoxBackground.Standard, true));
		Game.DialogueDepth--;
		this.State = 40;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000DC3 RID: 3523 RVA: 0x0000C7BC File Offset: 0x0000A9BC
	private IEnumerator RunConversationE()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_E1", this.eActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.onReward.Invoke();
		this.State = 50;
		this.UpdateState();
		yield break;
	}

	public static CoolQuest c;

	public CharacterProfile[] horseProfiles;

	public CharacterProfile[] unknownProfiles;

	public GameObject boringCoolKids;

	public GameObject funCoolKids;

	public GameObject splashPadWater;

	[Header("State A")]
	public GameObject aTextAlert;

	public DialogueActor[] aActors;

	public DialogueActor[] aWolf;

	public DialogueActor[] aBoar;

	public DialogueActor[] aGoose;

	public GameObject flashback;

	[Header("State B")]
	public GameObject bTrigger;

	[Header("State C")]
	public WaterPump[] waterPumps;

	public UnityEvent onReward;

	[Header("State D")]
	public DialogueActor[] dActors;

	public GameObject dTrigger;

	public GameObject dCameraSplash;

	public GameObject dCameraMountain;

	public GameObject dCameraJarl;

	public GameObject dHorse;

	[Header("State E")]
	public DialogueActor[] eActors;

	public DialogueActor[] eWolf;

	public DialogueActor[] eBoar;

	public DialogueActor[] eGoose;

	public DialogueActor[] eHorse;
}
