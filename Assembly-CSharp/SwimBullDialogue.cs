using System;
using System.Collections;
using UnityEngine;

public class SwimBullDialogue : MonoBehaviour, Interaction
{
	// (get) Token: 0x06000535 RID: 1333 RVA: 0x00005D47 File Offset: 0x00003F47
	// (set) Token: 0x06000536 RID: 1334 RVA: 0x00005D59 File Offset: 0x00003F59
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

	// Token: 0x06000537 RID: 1335 RVA: 0x00005D6B File Offset: 0x00003F6B
	private void Start()
	{
		if (this.State == 3)
		{
			this.bullFloofy.SetActive(true);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x00005D8E File Offset: 0x00003F8E
	public void Interact()
	{
		base.StartCoroutine(this.RunDialogue());
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x00005D9D File Offset: 0x00003F9D
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

	// Token: 0x0600053A RID: 1338 RVA: 0x00005DAC File Offset: 0x00003FAC
	public void Splash()
	{
		EffectsManager.e.Splash(this.splashPosition.position, 0.8f);
	}

	public DialogueActor[] actors;

	public Animator bullAnimator;

	public GameObject bullFloofy;

	public TownNPC[] townNpcs;

	public GameObject virtualCamera;

	public Transform splashPosition;

	private const string key = "NPCSwimBull";
}
