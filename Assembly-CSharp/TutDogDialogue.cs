using System;
using System.Collections;
using UnityEngine;

public class TutDogDialogue : MonoBehaviour
{
	// (get) Token: 0x06000EBD RID: 3773 RVA: 0x0000CF31 File Offset: 0x0000B131
	private string StateID
	{
		get
		{
			return "TutDogState";
		}
	}

	// (get) Token: 0x06000EBE RID: 3774 RVA: 0x0000CF38 File Offset: 0x0000B138
	// (set) Token: 0x06000EBF RID: 3775 RVA: 0x0000CF4B File Offset: 0x0000B14B
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

	// Token: 0x06000EC0 RID: 3776 RVA: 0x0000CF5E File Offset: 0x0000B15E
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000EC1 RID: 3777 RVA: 0x0004DCC8 File Offset: 0x0004BEC8
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

	// Token: 0x06000EC2 RID: 3778 RVA: 0x0000CF66 File Offset: 0x0000B166
	public void InteractDog()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000EC3 RID: 3779 RVA: 0x0000CF75 File Offset: 0x0000B175
	public void InteractSword()
	{
		base.StartCoroutine(this.FindSword());
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x0000CF84 File Offset: 0x0000B184
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

	// Token: 0x06000EC5 RID: 3781 RVA: 0x0000CF93 File Offset: 0x0000B193
	private IEnumerator FindSword()
	{
		this.sword.SetActive(false);
		yield return ItemManager.i.UnlockItem("Sword1");
		this.State = 3;
		this.UpdateState();
		yield break;
	}

	// Token: 0x06000EC6 RID: 3782 RVA: 0x0004DD98 File Offset: 0x0004BF98
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

	public GameObject groveWithSword;

	public GameObject groveNoSword;

	public GameObject dogStudying;

	public DialogueActor[] dogStudyingActors;

	public GameObject dogFantasy;

	public DialogueActor[] dogFantasyActors;

	public GameObject dogAttacked;

	public DialogueActor[] dogAttackedActors;

	public GameObject sword;

	public GameObject helpDialogue;

	public BreakableObject[] enemies;
}
