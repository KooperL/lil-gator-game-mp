using System;
using System.Collections;
using UnityEngine;

public class CoolShop : MonoBehaviour
{
	// Token: 0x06000DFB RID: 3579 RVA: 0x00002229 File Offset: 0x00000429
	public void Introduction()
	{
	}

	// Token: 0x06000DFC RID: 3580 RVA: 0x00002229 File Offset: 0x00000429
	public void ShopLauncher()
	{
	}

	// Token: 0x06000DFD RID: 3581 RVA: 0x0000C89A File Offset: 0x0000AA9A
	private IEnumerator RunConversation()
	{
		if (this.stateMachine.StateID == 0)
		{
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_Shop_Intro1", this.dialogueActors, DialogueManager.DialogueBoxBackground.Standard, true));
			if (DialogueManager.optionChosen == 1)
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_Shop_Intro1B", this.dialogueActors, DialogueManager.DialogueBoxBackground.Standard, true));
				yield break;
			}
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_Shop_Intro2", this.dialogueActors, DialogueManager.DialogueBoxBackground.Standard, true));
		}
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_Shop", this.dialogueActors, DialogueManager.DialogueBoxBackground.Standard, true));
		switch (DialogueManager.optionChosen)
		{
		case 0:
			this.shop.Activate();
			break;
		case 1:
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Cool_Shop_Talk", this.dialogueActors, DialogueManager.DialogueBoxBackground.Standard, true));
			break;
		case 2:
			DialogueManager.d.Bubble("Cool_Shop_Cancel", this.dialogueActors, 0f, false, true, true);
			break;
		}
		yield break;
	}

	public QuestStates stateMachine;

	public Shop shop;

	public DialogueActor[] dialogueActors;

	public MultilingualTextDocument document;
}
