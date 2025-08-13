using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020002B2 RID: 690
public class CoolQuest : MonoBehaviour
{
	// Token: 0x17000163 RID: 355
	// (get) Token: 0x06000D63 RID: 3427 RVA: 0x0000C341 File Offset: 0x0000A541
	private string StateID
	{
		get
		{
			return "CoolState";
		}
	}

	// Token: 0x17000164 RID: 356
	// (get) Token: 0x06000D64 RID: 3428 RVA: 0x0000C348 File Offset: 0x0000A548
	// (set) Token: 0x06000D65 RID: 3429 RVA: 0x0000C35B File Offset: 0x0000A55B
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

	// Token: 0x06000D66 RID: 3430 RVA: 0x0000C36E File Offset: 0x0000A56E
	private void OnEnable()
	{
		CoolQuest.c = this;
	}

	// Token: 0x06000D67 RID: 3431 RVA: 0x0000C376 File Offset: 0x0000A576
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000D68 RID: 3432 RVA: 0x00049C00 File Offset: 0x00047E00
	private void UpdateState()
	{
		int state = this.State;
		this.aTextAlert.SetActive(state == 0);
		this.bTrigger.SetActive(state == 10);
		this.dHorse.SetActive(state == 30);
		this.boringCoolKids.SetActive(state < 40);
		this.funCoolKids.SetActive(state >= 40);
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x0000C37E File Offset: 0x0000A57E
	public void Wolf()
	{
		if (this.State < 40)
		{
			base.StartCoroutine(this.WolfA());
			return;
		}
		DialogueManager.d.Bubble("Cool_E_Wolf", this.eWolf, 0f, false, true, true);
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x0000C3B6 File Offset: 0x0000A5B6
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

	// Token: 0x06000D6B RID: 3435 RVA: 0x0000C3C5 File Offset: 0x0000A5C5
	public void Boar()
	{
		if (this.State < 40)
		{
			base.StartCoroutine(this.BoarA());
			return;
		}
		DialogueManager.d.Bubble("Cool_E_Boar", this.eBoar, 0f, false, true, true);
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x0000C3FD File Offset: 0x0000A5FD
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

	// Token: 0x06000D6D RID: 3437 RVA: 0x0000C40C File Offset: 0x0000A60C
	public void Goose()
	{
		if (this.State < 40)
		{
			base.StartCoroutine(this.GooseA());
			return;
		}
		DialogueManager.d.Bubble("Cool_E_Goose", this.eGoose, 0f, false, true, true);
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x0000C444 File Offset: 0x0000A644
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

	// Token: 0x06000D6F RID: 3439 RVA: 0x00049C68 File Offset: 0x00047E68
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

	// Token: 0x06000D70 RID: 3440 RVA: 0x0000C453 File Offset: 0x0000A653
	public void TextAlert()
	{
		base.StartCoroutine(this.RunTextAlert());
		this.State = 1;
		this.UpdateState();
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x0000C46F File Offset: 0x0000A66F
	private IEnumerator RunTextAlert()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunkPhone("Cool_TextAlert1", this.unknownProfiles, true, true, null));
		yield return DialogueManager.d.Bubble("Cool_TextAlert2", null, 0f, true, true, true);
		yield break;
	}

	// Token: 0x06000D72 RID: 3442 RVA: 0x0000C47E File Offset: 0x0000A67E
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

	// Token: 0x06000D73 RID: 3443 RVA: 0x0000C48D File Offset: 0x0000A68D
	public void ConversationB()
	{
		base.StartCoroutine(this.RunConversationB());
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x0000C49C File Offset: 0x0000A69C
	private IEnumerator RunConversationB()
	{
		yield return DialogueManager.d.Bubble("Cool_B1", null, 0f, true, true, true);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunkPhone("Cool_B2", this.unknownProfiles, true, true, null));
		this.State = 20;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000D75 RID: 3445 RVA: 0x0000C4AB File Offset: 0x0000A6AB
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

	// Token: 0x06000D76 RID: 3446 RVA: 0x0000C4BA File Offset: 0x0000A6BA
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

	// Token: 0x06000D77 RID: 3447 RVA: 0x0000C4C9 File Offset: 0x0000A6C9
	private IEnumerator RunConversationE()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_E1", this.eActors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.onReward.Invoke();
		this.State = 50;
		this.UpdateState();
		yield break;
	}

	// Token: 0x040011B2 RID: 4530
	public static CoolQuest c;

	// Token: 0x040011B3 RID: 4531
	public CharacterProfile[] horseProfiles;

	// Token: 0x040011B4 RID: 4532
	public CharacterProfile[] unknownProfiles;

	// Token: 0x040011B5 RID: 4533
	public GameObject boringCoolKids;

	// Token: 0x040011B6 RID: 4534
	public GameObject funCoolKids;

	// Token: 0x040011B7 RID: 4535
	public GameObject splashPadWater;

	// Token: 0x040011B8 RID: 4536
	[Header("State A")]
	public GameObject aTextAlert;

	// Token: 0x040011B9 RID: 4537
	public DialogueActor[] aActors;

	// Token: 0x040011BA RID: 4538
	public DialogueActor[] aWolf;

	// Token: 0x040011BB RID: 4539
	public DialogueActor[] aBoar;

	// Token: 0x040011BC RID: 4540
	public DialogueActor[] aGoose;

	// Token: 0x040011BD RID: 4541
	public GameObject flashback;

	// Token: 0x040011BE RID: 4542
	[Header("State B")]
	public GameObject bTrigger;

	// Token: 0x040011BF RID: 4543
	[Header("State C")]
	public WaterPump[] waterPumps;

	// Token: 0x040011C0 RID: 4544
	public UnityEvent onReward;

	// Token: 0x040011C1 RID: 4545
	[Header("State D")]
	public DialogueActor[] dActors;

	// Token: 0x040011C2 RID: 4546
	public GameObject dTrigger;

	// Token: 0x040011C3 RID: 4547
	public GameObject dCameraSplash;

	// Token: 0x040011C4 RID: 4548
	public GameObject dCameraMountain;

	// Token: 0x040011C5 RID: 4549
	public GameObject dCameraJarl;

	// Token: 0x040011C6 RID: 4550
	public GameObject dHorse;

	// Token: 0x040011C7 RID: 4551
	[Header("State E")]
	public DialogueActor[] eActors;

	// Token: 0x040011C8 RID: 4552
	public DialogueActor[] eWolf;

	// Token: 0x040011C9 RID: 4553
	public DialogueActor[] eBoar;

	// Token: 0x040011CA RID: 4554
	public DialogueActor[] eGoose;

	// Token: 0x040011CB RID: 4555
	public DialogueActor[] eHorse;
}
