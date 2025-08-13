using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200022A RID: 554
public class TutDogDialogue : MonoBehaviour
{
	// Token: 0x170000CA RID: 202
	// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x000393B1 File Offset: 0x000375B1
	private string StateID
	{
		get
		{
			return "TutDogState";
		}
	}

	// Token: 0x170000CB RID: 203
	// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x000393B8 File Offset: 0x000375B8
	// (set) Token: 0x06000BF2 RID: 3058 RVA: 0x000393CB File Offset: 0x000375CB
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

	// Token: 0x06000BF3 RID: 3059 RVA: 0x000393DE File Offset: 0x000375DE
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000BF4 RID: 3060 RVA: 0x000393E8 File Offset: 0x000375E8
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

	// Token: 0x06000BF5 RID: 3061 RVA: 0x000394B6 File Offset: 0x000376B6
	public void InteractDog()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000BF6 RID: 3062 RVA: 0x000394C5 File Offset: 0x000376C5
	public void InteractSword()
	{
		base.StartCoroutine(this.FindSword());
	}

	// Token: 0x06000BF7 RID: 3063 RVA: 0x000394D4 File Offset: 0x000376D4
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

	// Token: 0x06000BF8 RID: 3064 RVA: 0x000394E3 File Offset: 0x000376E3
	private IEnumerator FindSword()
	{
		this.sword.SetActive(false);
		yield return ItemManager.i.UnlockItem("Sword1");
		this.State = 3;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000BF9 RID: 3065 RVA: 0x000394F4 File Offset: 0x000376F4
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

	// Token: 0x04000FB9 RID: 4025
	public GameObject groveWithSword;

	// Token: 0x04000FBA RID: 4026
	public GameObject groveNoSword;

	// Token: 0x04000FBB RID: 4027
	public GameObject dogStudying;

	// Token: 0x04000FBC RID: 4028
	public DialogueActor[] dogStudyingActors;

	// Token: 0x04000FBD RID: 4029
	public GameObject dogFantasy;

	// Token: 0x04000FBE RID: 4030
	public DialogueActor[] dogFantasyActors;

	// Token: 0x04000FBF RID: 4031
	public GameObject dogAttacked;

	// Token: 0x04000FC0 RID: 4032
	public DialogueActor[] dogAttackedActors;

	// Token: 0x04000FC1 RID: 4033
	public GameObject sword;

	// Token: 0x04000FC2 RID: 4034
	public GameObject helpDialogue;

	// Token: 0x04000FC3 RID: 4035
	public BreakableObject[] enemies;
}
