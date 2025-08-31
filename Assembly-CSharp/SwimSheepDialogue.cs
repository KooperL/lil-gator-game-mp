using System;
using System.Collections;
using UnityEngine;

public class SwimSheepDialogue : MonoBehaviour, Interaction
{
	// (get) Token: 0x06000444 RID: 1092 RVA: 0x000186F0 File Offset: 0x000168F0
	// (set) Token: 0x06000445 RID: 1093 RVA: 0x00018702 File Offset: 0x00016902
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

	// Token: 0x06000446 RID: 1094 RVA: 0x00018714 File Offset: 0x00016914
	public void Interact()
	{
		base.StartCoroutine(this.RunDialogue());
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x00018723 File Offset: 0x00016923
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
