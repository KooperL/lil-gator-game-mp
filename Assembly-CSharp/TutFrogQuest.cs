using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002E4 RID: 740
public class TutFrogQuest : MonoBehaviour
{
	// Token: 0x170001A5 RID: 421
	// (get) Token: 0x06000E88 RID: 3720 RVA: 0x0000CCBE File Offset: 0x0000AEBE
	private string StateID
	{
		get
		{
			return "TutFrogState";
		}
	}

	// Token: 0x06000E89 RID: 3721 RVA: 0x0004C714 File Offset: 0x0004A914
	private void Start()
	{
		int num = GameData.g.ReadInt(this.StateID, 0);
		this.earlyFrog.SetActive(num == 0);
		this.frog.SetActive(num != 0);
	}

	// Token: 0x06000E8A RID: 3722 RVA: 0x0000CCC5 File Offset: 0x0000AEC5
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000E8B RID: 3723 RVA: 0x0000CCD4 File Offset: 0x0000AED4
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

	// Token: 0x040012AA RID: 4778
	public DialogueActor[] earlyActors;

	// Token: 0x040012AB RID: 4779
	public DialogueActor[] actors;

	// Token: 0x040012AC RID: 4780
	public GameObject earlyFrog;

	// Token: 0x040012AD RID: 4781
	public GameObject frog;

	// Token: 0x040012AE RID: 4782
	public GameObject camera1;
}
