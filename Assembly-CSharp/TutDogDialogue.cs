using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002E1 RID: 737
public class TutDogDialogue : MonoBehaviour
{
	// Token: 0x1700019F RID: 415
	// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0000CC1F File Offset: 0x0000AE1F
	private string StateID
	{
		get
		{
			return "TutDogState";
		}
	}

	// Token: 0x170001A0 RID: 416
	// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0000CC26 File Offset: 0x0000AE26
	// (set) Token: 0x06000E73 RID: 3699 RVA: 0x0000CC39 File Offset: 0x0000AE39
	private int State
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

	// Token: 0x06000E74 RID: 3700 RVA: 0x0000CC4C File Offset: 0x0000AE4C
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x0004C164 File Offset: 0x0004A364
	private void UpdateState()
	{
		int state = this.State;
		this.groveWithSword.SetActive(state >= 2);
		this.groveNoSword.SetActive(!this.groveWithSword.activeSelf);
		this.sword.SetActive(state == 2);
		this.dogStudying.SetActive(state < 2);
		this.dogAttacked.SetActive(state == 3 || state == 4);
		this.dogFantasy.SetActive(state >= 2 && !this.dogAttacked.activeSelf);
		for (int i = 0; i < this.enemies.Length; i++)
		{
			this.enemies[i].gameObject.SetActive(state >= 3);
		}
		this.helpDialogue.SetActive(state == 3);
	}

	// Token: 0x06000E76 RID: 3702 RVA: 0x0000CC54 File Offset: 0x0000AE54
	public void InteractDog()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000E77 RID: 3703 RVA: 0x0000CC63 File Offset: 0x0000AE63
	public void InteractSword()
	{
		base.StartCoroutine(this.FindSword());
	}

	// Token: 0x06000E78 RID: 3704 RVA: 0x0000CC72 File Offset: 0x0000AE72
	private IEnumerator RunConversation()
	{
		Game.DialogueDepth++;
		int num = this.State;
		if (num == 2 && ItemManager.i.IsItemUnlocked("Sword1"))
		{
			num = 3;
		}
		switch (num)
		{
		case 0:
		case 1:
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_DogQuest1a", this.dogStudyingActors, DialogueManager.DialogueBoxBackground.Standard, true));
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_DogQuest1b", this.dogStudyingActors, DialogueManager.DialogueBoxBackground.Standard, true));
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_DogQuest1c", this.dogStudyingActors, DialogueManager.DialogueBoxBackground.Standard, true));
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_DogQuest1d", this.dogStudyingActors, DialogueManager.DialogueBoxBackground.Standard, true));
			yield return Blackout.FadeIn();
			this.State = 2;
			this.UpdateState();
			yield return Blackout.FadeOut();
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_DogQuest1e", this.dogFantasyActors, DialogueManager.DialogueBoxBackground.Standard, true));
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_DogQuest1f", this.dogFantasyActors, DialogueManager.DialogueBoxBackground.Standard, true));
			break;
		case 2:
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_DogQuest2", this.dogFantasyActors, DialogueManager.DialogueBoxBackground.Standard, true));
			break;
		case 3:
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_DogQuest3", this.dogAttackedActors, DialogueManager.DialogueBoxBackground.Standard, true));
			break;
		case 4:
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_DogQuest4", this.dogAttackedActors, DialogueManager.DialogueBoxBackground.Standard, true));
			break;
		case 5:
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_DogQuest5", this.dogFantasyActors, DialogueManager.DialogueBoxBackground.Standard, true));
			this.State = 6;
			break;
		case 6:
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_DogQuest6", this.dogFantasyActors, DialogueManager.DialogueBoxBackground.Standard, true));
			break;
		default:
			yield return null;
			break;
		}
		Game.DialogueDepth--;
		yield break;
	}

	// Token: 0x06000E79 RID: 3705 RVA: 0x0000CC81 File Offset: 0x0000AE81
	private IEnumerator FindSword()
	{
		this.sword.SetActive(false);
		yield return ItemManager.i.UnlockItem("Sword1");
		this.State = 3;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000E7A RID: 3706 RVA: 0x0004C234 File Offset: 0x0004A434
	public void DefeatEnemy()
	{
		int num = 0;
		for (int i = 0; i < this.enemies.Length; i++)
		{
			if (!this.enemies[i].isBroken)
			{
				num++;
			}
		}
		if (num == 0)
		{
			this.State = 5;
			this.UpdateState();
		}
		else if (this.State != 4)
		{
			this.State = 4;
			this.UpdateState();
		}
		if (DialogueManager.d.CanAcceptBubbleDialogue)
		{
			switch (num)
			{
			case 0:
				DialogueManager.d.Bubble("Tutorial_DogQuest4_Bubble4", this.dogFantasyActors, 0f, false, true, true);
				break;
			case 1:
				DialogueManager.d.Bubble("Tutorial_DogQuest4_Bubble3", this.dogAttackedActors, 0f, false, true, true);
				return;
			case 2:
				DialogueManager.d.Bubble("Tutorial_DogQuest4_Bubble2", this.dogAttackedActors, 0f, false, true, true);
				return;
			case 3:
				DialogueManager.d.Bubble("Tutorial_DogQuest4_Bubble1", this.dogAttackedActors, 0f, false, true, true);
				return;
			default:
				return;
			}
		}
	}

	// Token: 0x04001299 RID: 4761
	public GameObject groveWithSword;

	// Token: 0x0400129A RID: 4762
	public GameObject groveNoSword;

	// Token: 0x0400129B RID: 4763
	public GameObject dogStudying;

	// Token: 0x0400129C RID: 4764
	public DialogueActor[] dogStudyingActors;

	// Token: 0x0400129D RID: 4765
	public GameObject dogFantasy;

	// Token: 0x0400129E RID: 4766
	public DialogueActor[] dogFantasyActors;

	// Token: 0x0400129F RID: 4767
	public GameObject dogAttacked;

	// Token: 0x040012A0 RID: 4768
	public DialogueActor[] dogAttackedActors;

	// Token: 0x040012A1 RID: 4769
	public GameObject sword;

	// Token: 0x040012A2 RID: 4770
	public GameObject helpDialogue;

	// Token: 0x040012A3 RID: 4771
	public BreakableObject[] enemies;
}
