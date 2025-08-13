using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000107 RID: 263
public class SwimBullDialogue : MonoBehaviour, Interaction
{
	// Token: 0x17000080 RID: 128
	// (get) Token: 0x06000508 RID: 1288 RVA: 0x00005AD2 File Offset: 0x00003CD2
	// (set) Token: 0x06000509 RID: 1289 RVA: 0x00005AE4 File Offset: 0x00003CE4
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

	// Token: 0x0600050A RID: 1290 RVA: 0x00005AF6 File Offset: 0x00003CF6
	private void Start()
	{
		if (this.State == 3)
		{
			this.bullFloofy.SetActive(true);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x00005B19 File Offset: 0x00003D19
	public void Interact()
	{
		base.StartCoroutine(this.RunDialogue());
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x00005B28 File Offset: 0x00003D28
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

	// Token: 0x0600050D RID: 1293 RVA: 0x00005B37 File Offset: 0x00003D37
	public void Splash()
	{
		EffectsManager.e.Splash(this.splashPosition.position, 0.8f);
	}

	// Token: 0x04000706 RID: 1798
	public DialogueActor[] actors;

	// Token: 0x04000707 RID: 1799
	public Animator bullAnimator;

	// Token: 0x04000708 RID: 1800
	public GameObject bullFloofy;

	// Token: 0x04000709 RID: 1801
	public TownNPC[] townNpcs;

	// Token: 0x0400070A RID: 1802
	public GameObject virtualCamera;

	// Token: 0x0400070B RID: 1803
	public Transform splashPosition;

	// Token: 0x0400070C RID: 1804
	private const string key = "NPCSwimBull";
}
