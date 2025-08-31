using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CoolQuest : MonoBehaviour
{
	// (get) Token: 0x06000B62 RID: 2914 RVA: 0x00038321 File Offset: 0x00036521
	private string StateID
	{
		get
		{
			return "CoolState";
		}
	}

	// (get) Token: 0x06000B63 RID: 2915 RVA: 0x00038328 File Offset: 0x00036528
	// (set) Token: 0x06000B64 RID: 2916 RVA: 0x0003833B File Offset: 0x0003653B
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

	// Token: 0x06000B65 RID: 2917 RVA: 0x0003834E File Offset: 0x0003654E
	private void OnEnable()
	{
		CoolQuest.c = this;
	}

	// Token: 0x06000B66 RID: 2918 RVA: 0x00038356 File Offset: 0x00036556
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x00038360 File Offset: 0x00036560
	private void UpdateState()
	{
		int state = this.State;
		this.aTextAlert.SetActive(state == 0);
		this.bTrigger.SetActive(state == 10);
		this.dHorse.SetActive(state == 30);
		this.boringCoolKids.SetActive(state < 40);
		this.funCoolKids.SetActive(state >= 40);
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x000383C6 File Offset: 0x000365C6
	public void Wolf()
	{
		if (this.State < 40)
		{
			base.StartCoroutine(this.WolfA());
			return;
		}
		DialogueManager.d.Bubble("Cool_E_Wolf", this.eWolf, 0f, false, true, true);
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x000383FE File Offset: 0x000365FE
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

	// Token: 0x06000B6A RID: 2922 RVA: 0x0003840D File Offset: 0x0003660D
	public void Boar()
	{
		if (this.State < 40)
		{
			base.StartCoroutine(this.BoarA());
			return;
		}
		DialogueManager.d.Bubble("Cool_E_Boar", this.eBoar, 0f, false, true, true);
	}

	// Token: 0x06000B6B RID: 2923 RVA: 0x00038445 File Offset: 0x00036645
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

	// Token: 0x06000B6C RID: 2924 RVA: 0x00038454 File Offset: 0x00036654
	public void Goose()
	{
		if (this.State < 40)
		{
			base.StartCoroutine(this.GooseA());
			return;
		}
		DialogueManager.d.Bubble("Cool_E_Goose", this.eGoose, 0f, false, true, true);
	}

	// Token: 0x06000B6D RID: 2925 RVA: 0x0003848C File Offset: 0x0003668C
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

	// Token: 0x06000B6E RID: 2926 RVA: 0x0003849C File Offset: 0x0003669C
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

	// Token: 0x06000B6F RID: 2927 RVA: 0x000384F4 File Offset: 0x000366F4
	public void TextAlert()
	{
		base.StartCoroutine(this.RunTextAlert());
		this.State = 1;
		this.UpdateState();
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x00038510 File Offset: 0x00036710
	private IEnumerator RunTextAlert()
	{
		yield return base.StartCoroutine(DialogueManager.d.LoadChunkPhone("Cool_TextAlert1", this.unknownProfiles, true, true, null));
		yield return DialogueManager.d.Bubble("Cool_TextAlert2", null, 0f, true, true, true);
		yield break;
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x0003851F File Offset: 0x0003671F
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

	// Token: 0x06000B72 RID: 2930 RVA: 0x0003852E File Offset: 0x0003672E
	public void ConversationB()
	{
		base.StartCoroutine(this.RunConversationB());
	}

	// Token: 0x06000B73 RID: 2931 RVA: 0x0003853D File Offset: 0x0003673D
	private IEnumerator RunConversationB()
	{
		yield return DialogueManager.d.Bubble("Cool_B1", null, 0f, true, true, true);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunkPhone("Cool_B2", this.unknownProfiles, true, true, null));
		this.State = 20;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000B74 RID: 2932 RVA: 0x0003854C File Offset: 0x0003674C
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

	// Token: 0x06000B75 RID: 2933 RVA: 0x0003855B File Offset: 0x0003675B
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

	// Token: 0x06000B76 RID: 2934 RVA: 0x0003856A File Offset: 0x0003676A
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
