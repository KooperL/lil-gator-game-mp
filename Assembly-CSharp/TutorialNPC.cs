using System;
using System.Collections;
using UnityEngine;

public class TutorialNPC : MonoBehaviour, Interaction
{
	// Token: 0x06000560 RID: 1376 RVA: 0x00005E95 File Offset: 0x00004095
	public void Interact()
	{
		base.StartCoroutine(this.RunConversation());
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x00005EA4 File Offset: 0x000040A4
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

	private static int tutorialCount;

	public string dialogueChunkPreface;

	public DialogueActor[] actors;

	public string dialogueChunkName;

	public int dialogueCount = 1;

	private int dialogueIndex;

	public string dialogueChunkCompletedName;

	public int dialogueAfterCount = 1;

	private int dialogueAfterIndex;

	public string fetchItemName;

	public bool giveFreeItem;
}
