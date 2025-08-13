using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000211 RID: 529
public class CoolQuest : MonoBehaviour
{
	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x06000B62 RID: 2914 RVA: 0x00038321 File Offset: 0x00036521
	private string StateID
	{
		get
		{
			return "CoolState";
		}
	}

	// Token: 0x170000BA RID: 186
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

	// Token: 0x04000F1C RID: 3868
	public static CoolQuest c;

	// Token: 0x04000F1D RID: 3869
	public CharacterProfile[] horseProfiles;

	// Token: 0x04000F1E RID: 3870
	public CharacterProfile[] unknownProfiles;

	// Token: 0x04000F1F RID: 3871
	public GameObject boringCoolKids;

	// Token: 0x04000F20 RID: 3872
	public GameObject funCoolKids;

	// Token: 0x04000F21 RID: 3873
	public GameObject splashPadWater;

	// Token: 0x04000F22 RID: 3874
	[Header("State A")]
	public GameObject aTextAlert;

	// Token: 0x04000F23 RID: 3875
	public DialogueActor[] aActors;

	// Token: 0x04000F24 RID: 3876
	public DialogueActor[] aWolf;

	// Token: 0x04000F25 RID: 3877
	public DialogueActor[] aBoar;

	// Token: 0x04000F26 RID: 3878
	public DialogueActor[] aGoose;

	// Token: 0x04000F27 RID: 3879
	public GameObject flashback;

	// Token: 0x04000F28 RID: 3880
	[Header("State B")]
	public GameObject bTrigger;

	// Token: 0x04000F29 RID: 3881
	[Header("State C")]
	public WaterPump[] waterPumps;

	// Token: 0x04000F2A RID: 3882
	public UnityEvent onReward;

	// Token: 0x04000F2B RID: 3883
	[Header("State D")]
	public DialogueActor[] dActors;

	// Token: 0x04000F2C RID: 3884
	public GameObject dTrigger;

	// Token: 0x04000F2D RID: 3885
	public GameObject dCameraSplash;

	// Token: 0x04000F2E RID: 3886
	public GameObject dCameraMountain;

	// Token: 0x04000F2F RID: 3887
	public GameObject dCameraJarl;

	// Token: 0x04000F30 RID: 3888
	public GameObject dHorse;

	// Token: 0x04000F31 RID: 3889
	[Header("State E")]
	public DialogueActor[] eActors;

	// Token: 0x04000F32 RID: 3890
	public DialogueActor[] eWolf;

	// Token: 0x04000F33 RID: 3891
	public DialogueActor[] eBoar;

	// Token: 0x04000F34 RID: 3892
	public DialogueActor[] eGoose;

	// Token: 0x04000F35 RID: 3893
	public DialogueActor[] eHorse;
}
