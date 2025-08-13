using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200010F RID: 271
public class TutorialNPC : MonoBehaviour, Interaction
{
	// Token: 0x06000533 RID: 1331 RVA: 0x00005C20 File Offset: 0x00003E20
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x00005C2F File Offset: 0x00003E2F
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

	// Token: 0x0400072C RID: 1836
	private static int tutorialCount;

	// Token: 0x0400072D RID: 1837
	public string dialogueChunkPreface;

	// Token: 0x0400072E RID: 1838
	public DialogueActor[] actors;

	// Token: 0x0400072F RID: 1839
	public string dialogueChunkName;

	// Token: 0x04000730 RID: 1840
	public int dialogueCount = 1;

	// Token: 0x04000731 RID: 1841
	private int dialogueIndex;

	// Token: 0x04000732 RID: 1842
	public string dialogueChunkCompletedName;

	// Token: 0x04000733 RID: 1843
	public int dialogueAfterCount = 1;

	// Token: 0x04000734 RID: 1844
	private int dialogueAfterIndex;

	// Token: 0x04000735 RID: 1845
	public string fetchItemName;

	// Token: 0x04000736 RID: 1846
	public bool giveFreeItem;
}
