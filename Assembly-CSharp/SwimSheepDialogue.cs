using System;
using System.Collections;
using UnityEngine;

public class SwimSheepDialogue : MonoBehaviour, Interaction
{
	// (get) Token: 0x06000542 RID: 1346 RVA: 0x00005D47 File Offset: 0x00003F47
	// (set) Token: 0x06000543 RID: 1347 RVA: 0x00005D59 File Offset: 0x00003F59
	private int State
	{
		get
		{
			return GameData.g.ReadInt("NPCSwimBull", 0);
		}
		set
		{
			GameData.g.Write("NPCSwimBull", value);
		}
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x00005DDF File Offset: 0x00003FDF
	public void Interact()
	{
		base.StartCoroutine(this.RunDialogue());
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x00005DEE File Offset: 0x00003FEE
	private IEnumerator RunDialogue()
	{
		switch (this.State)
		{
		case 0:
			this.virtualCamera.SetActive(true);
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("NPCSwimSheep1a", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			this.virtualCamera.SetActive(false);
			break;
		case 1:
			this.virtualCamera.SetActive(true);
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("NPCSwimSheep1b", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			this.virtualCamera.SetActive(false);
			this.State = 2;
			break;
		case 2:
			this.virtualCamera.SetActive(true);
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("NPCSwimSheep1c", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			this.virtualCamera.SetActive(false);
			break;
		case 3:
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("NPCSwimSheep2", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			break;
		}
		yield break;
	}

	public DialogueActor[] actors;

	public GameObject virtualCamera;

	private const string key = "NPCSwimBull";
}
