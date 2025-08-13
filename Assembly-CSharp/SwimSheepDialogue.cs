using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000109 RID: 265
public class SwimSheepDialogue : MonoBehaviour, Interaction
{
	// Token: 0x17000083 RID: 131
	// (get) Token: 0x06000515 RID: 1301 RVA: 0x00005AD2 File Offset: 0x00003CD2
	// (set) Token: 0x06000516 RID: 1302 RVA: 0x00005AE4 File Offset: 0x00003CE4
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

	// Token: 0x06000517 RID: 1303 RVA: 0x00005B6A File Offset: 0x00003D6A
	public void Interact()
	{
		base.StartCoroutine(this.RunDialogue());
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x00005B79 File Offset: 0x00003D79
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

	// Token: 0x04000710 RID: 1808
	public DialogueActor[] actors;

	// Token: 0x04000711 RID: 1809
	public GameObject virtualCamera;

	// Token: 0x04000712 RID: 1810
	private const string key = "NPCSwimBull";
}
