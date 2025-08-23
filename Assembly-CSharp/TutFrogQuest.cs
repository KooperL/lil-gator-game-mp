using System;
using System.Collections;
using UnityEngine;

public class TutFrogQuest : MonoBehaviour
{
	// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x0000CFD0 File Offset: 0x0000B1D0
	private string StateID
	{
		get
		{
			return "TutFrogState";
		}
	}

	// Token: 0x06000ED6 RID: 3798 RVA: 0x0004E564 File Offset: 0x0004C764
	private void Start()
	{
		int num = GameData.g.ReadInt(this.StateID, 0);
		this.earlyFrog.SetActive(num == 0);
		this.frog.SetActive(num != 0);
	}

	// Token: 0x06000ED7 RID: 3799 RVA: 0x0000CFD7 File Offset: 0x0000B1D7
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000ED8 RID: 3800 RVA: 0x0000CFE6 File Offset: 0x0000B1E6
	private IEnumerator RunConversation()
	{
		Game.DialogueDepth++;
		int state = GameData.g.ReadInt(this.StateID, 0);
		switch (state)
		{
		case 0:
		case 1:
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_FrogQuest1a", this.earlyActors, DialogueManager.DialogueBoxBackground.Standard, true));
			this.camera1.SetActive(true);
			this.earlyFrog.SetActive(false);
			this.frog.SetActive(true);
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_FrogQuest1b", null, DialogueManager.DialogueBoxBackground.Standard, true));
			this.camera1.SetActive(false);
			state = 2;
			break;
		case 2:
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_FrogQuest2", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			ItemManager.i.UnlockItem("Shield1");
			state = 3;
			break;
		case 3:
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_DogQuest3", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			break;
		default:
			yield return null;
			break;
		}
		GameData.g.Write(this.StateID, state);
		Game.DialogueDepth--;
		yield break;
	}

	public DialogueActor[] earlyActors;

	public DialogueActor[] actors;

	public GameObject earlyFrog;

	public GameObject frog;

	public GameObject camera1;
}
