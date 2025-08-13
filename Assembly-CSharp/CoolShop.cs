using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002BC RID: 700
public class CoolShop : MonoBehaviour
{
	// Token: 0x06000DAF RID: 3503 RVA: 0x00002229 File Offset: 0x00000429
	public void Introduction()
	{
	}

	// Token: 0x06000DB0 RID: 3504 RVA: 0x00002229 File Offset: 0x00000429
	public void ShopLauncher()
	{
	}

	// Token: 0x06000DB1 RID: 3505 RVA: 0x0000C5A7 File Offset: 0x0000A7A7
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

	// Token: 0x040011E7 RID: 4583
	public QuestStates stateMachine;

	// Token: 0x040011E8 RID: 4584
	public Shop shop;

	// Token: 0x040011E9 RID: 4585
	public DialogueActor[] dialogueActors;

	// Token: 0x040011EA RID: 4586
	public MultilingualTextDocument document;
}
