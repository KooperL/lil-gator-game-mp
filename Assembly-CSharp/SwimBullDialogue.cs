using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000C3 RID: 195
public class SwimBullDialogue : MonoBehaviour, Interaction
{
	// Token: 0x1700003E RID: 62
	// (get) Token: 0x0600043D RID: 1085 RVA: 0x00018667 File Offset: 0x00016867
	// (set) Token: 0x0600043E RID: 1086 RVA: 0x00018679 File Offset: 0x00016879
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

	// Token: 0x0600043F RID: 1087 RVA: 0x0001868B File Offset: 0x0001688B
	private void Start()
	{
		if (this.State == 3)
		{
			this.bullFloofy.SetActive(true);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x000186AE File Offset: 0x000168AE
	public void Interact()
	{
		base.StartCoroutine(this.RunDialogue());
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x000186BD File Offset: 0x000168BD
	private IEnumerator RunDialogue()
	{
		this.virtualCamera.SetActive(true);
		Game.DialogueDepth++;
		switch (this.State)
		{
		case 0:
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("NPCSwimBull1", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			this.State = 1;
			break;
		case 1:
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("NPCSwimBull1a", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			break;
		case 2:
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("NPCSwimBull2", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			this.bullAnimator.SetBool("Action1", true);
			yield return new WaitForSeconds(2.3f);
			this.bullFloofy.SetActive(true);
			base.gameObject.SetActive(false);
			this.State = 3;
			break;
		}
		Game.DialogueDepth--;
		this.virtualCamera.SetActive(false);
		yield break;
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x000186CC File Offset: 0x000168CC
	public void Splash()
	{
		EffectsManager.e.Splash(this.splashPosition.position, 0.8f);
	}

	// Token: 0x040005F3 RID: 1523
	public DialogueActor[] actors;

	// Token: 0x040005F4 RID: 1524
	public Animator bullAnimator;

	// Token: 0x040005F5 RID: 1525
	public GameObject bullFloofy;

	// Token: 0x040005F6 RID: 1526
	public TownNPC[] townNpcs;

	// Token: 0x040005F7 RID: 1527
	public GameObject virtualCamera;

	// Token: 0x040005F8 RID: 1528
	public Transform splashPosition;

	// Token: 0x040005F9 RID: 1529
	private const string key = "NPCSwimBull";
}
