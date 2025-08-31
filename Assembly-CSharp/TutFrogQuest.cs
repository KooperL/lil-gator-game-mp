using System;
using System.Collections;
using UnityEngine;

public class TutFrogQuest : MonoBehaviour
{
	// (get) Token: 0x06000BFB RID: 3067 RVA: 0x000395FA File Offset: 0x000377FA
	private string StateID
	{
		get
		{
			return "TutFrogState";
		}
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x00039604 File Offset: 0x00037804
	private void Start()
	{
		int num = GameData.g.ReadInt(this.StateID, 0);
		this.earlyFrog.SetActive(num == 0);
		this.frog.SetActive(num != 0);
	}

	// Token: 0x06000BFD RID: 3069 RVA: 0x00039641 File Offset: 0x00037841
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000BFE RID: 3070 RVA: 0x00039650 File Offset: 0x00037850
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
