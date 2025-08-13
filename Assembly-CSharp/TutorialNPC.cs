using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000C8 RID: 200
public class TutorialNPC : MonoBehaviour, Interaction
{
	// Token: 0x06000456 RID: 1110 RVA: 0x00018BD1 File Offset: 0x00016DD1
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x00018BE0 File Offset: 0x00016DE0
	private IEnumerator RunConversation()
	{
		TutorialNPC.tutorialCount = GameData.g.ReadInt("TutorialsSpokenTo", 0);
		this.dialogueIndex = GameData.g.ReadInt(this.dialogueChunkName, 0);
		if (this.dialogueIndex == 0)
		{
			if (this.dialogueChunkPreface != "")
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.dialogueChunkPreface, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_NPCIntro" + TutorialNPC.tutorialCount.ToString("0"), this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			TutorialNPC.tutorialCount++;
			GameData.g.Write("TutorialsSpokenTo", TutorialNPC.tutorialCount);
		}
		if (this.dialogueIndex > 10 || ItemManager.i.IsItemUnlocked(this.fetchItemName))
		{
			this.dialogueAfterIndex = Mathf.Min(this.dialogueAfterCount, this.dialogueIndex + 11);
			if (this.dialogueAfterCount > 1)
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.dialogueChunkCompletedName + this.dialogueAfterIndex.ToString("0"), this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			else
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.dialogueChunkCompletedName, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
		}
		else
		{
			this.dialogueIndex = Mathf.Min(this.dialogueCount, this.dialogueIndex + 1);
			if (this.dialogueCount > 1)
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.dialogueChunkName + this.dialogueIndex.ToString("0"), this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			else
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.dialogueChunkName, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			if (this.giveFreeItem)
			{
				ItemManager.i.UnlockItem(this.fetchItemName);
			}
		}
		GameData.g.Write(this.dialogueChunkName, this.dialogueIndex);
		yield break;
	}

	// Token: 0x04000610 RID: 1552
	private static int tutorialCount;

	// Token: 0x04000611 RID: 1553
	public string dialogueChunkPreface;

	// Token: 0x04000612 RID: 1554
	public DialogueActor[] actors;

	// Token: 0x04000613 RID: 1555
	public string dialogueChunkName;

	// Token: 0x04000614 RID: 1556
	public int dialogueCount = 1;

	// Token: 0x04000615 RID: 1557
	private int dialogueIndex;

	// Token: 0x04000616 RID: 1558
	public string dialogueChunkCompletedName;

	// Token: 0x04000617 RID: 1559
	public int dialogueAfterCount = 1;

	// Token: 0x04000618 RID: 1560
	private int dialogueAfterIndex;

	// Token: 0x04000619 RID: 1561
	public string fetchItemName;

	// Token: 0x0400061A RID: 1562
	public bool giveFreeItem;
}
