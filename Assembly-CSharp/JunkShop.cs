using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000040 RID: 64
public class JunkShop : MonoBehaviour
{
	// Token: 0x060000DC RID: 220 RVA: 0x0001A204 File Offset: 0x00018404
	private void OnValidate()
	{
		if (this.itemGet == null)
		{
			this.itemGet = Object.FindObjectOfType<UIItemGet>();
		}
		if (this.uiItemResource == null || (this.itemResource != null && this.uiItemResource.itemResource != this.itemResource))
		{
			foreach (UIItemResource uiitemResource in Object.FindObjectsOfType<UIItemResource>())
			{
				if (uiitemResource.itemResource == this.itemResource)
				{
					this.uiItemResource = uiitemResource;
					return;
				}
			}
		}
	}

	// Token: 0x060000DD RID: 221 RVA: 0x00002B92 File Offset: 0x00000D92
	private void Start()
	{
		this.displayedItems = new int[] { -1, -1, -1 };
		this.UpdateInventory();
	}

	// Token: 0x060000DE RID: 222 RVA: 0x00002BB1 File Offset: 0x00000DB1
	public void RunShopDialogue()
	{
		CoroutineUtil.Start(this.RunShopDialogueSequence());
	}

	// Token: 0x060000DF RID: 223 RVA: 0x00002BBF File Offset: 0x00000DBF
	private IEnumerator RunShopDialogueSequence()
	{
		Game.DialogueDepth++;
		this.itemResource.ForceShow = true;
		GameObject[] array = this.shopStateObjects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(true);
		}
		yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(this.shopIntro), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
		DialogueManager.optionChosen = -1;
		CoroutineUtil.Start(DialogueManager.d.RunDialogueOptions(this.GetChoiceList()));
		int selectedOption = 0;
		while (DialogueManager.optionChosen == -1)
		{
			int currentlySelectedIndex = DialogueOptions.CurrentlySelectedIndex;
			for (int j = 0; j < this.cameras.Length; j++)
			{
				this.cameras[j].SetActive(j == currentlySelectedIndex - 1);
			}
			if (currentlySelectedIndex != selectedOption)
			{
				if (currentlySelectedIndex == 0)
				{
					this.uiItemResource.ClearPrice();
				}
				else
				{
					this.uiItemResource.SetPrice(this.shopItems[this.displayedItems[currentlySelectedIndex - 1]].cost);
				}
			}
			yield return null;
		}
		array = this.cameras;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(false);
		}
		if (DialogueManager.optionChosen == 0)
		{
			this.uiItemResource.ClearPrice();
			this.itemResource.ForceShow = false;
			yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(this.cancelDialogue), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
		}
		else
		{
			int num = DialogueManager.optionChosen - 1;
			JunkShop.ShopItem shopItem = this.shopItems[this.displayedItems[num]];
			if (this.itemResource.Amount >= shopItem.cost)
			{
				this.uiItemResource.ClearPrice();
				this.itemResource.ForceShow = false;
				this.itemResource.Amount -= shopItem.cost;
				shopItem.item.IsUnlocked = true;
				ItemManager.i.EquipItem(shopItem.item, true);
				this.UpdateInventory();
				yield return CoroutineUtil.Start(this.itemGet.RunSequence(shopItem.item.sprite, shopItem.item.DisplayName, this.document.FetchChunk(shopItem.unlockChunk), this.actors));
			}
			else
			{
				yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(this.notEnoughDialogue), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
				this.uiItemResource.ClearPrice();
				this.itemResource.ForceShow = false;
			}
		}
		array = this.shopStateObjects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(false);
		}
		if (this.displayedItemCount == 0)
		{
			yield return DialogueManager.d.LoadChunk(this.document.FetchChunk(this.soldOutDialogue), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false);
			yield return this.stateMachine.ProgressState(-1);
		}
		Game.DialogueDepth--;
		yield break;
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x0001A294 File Offset: 0x00018494
	private string[] GetChoiceList()
	{
		string[] array = new string[this.displayedItemCount + 1];
		array[0] = this.document.FetchString(this.cancelChoice, Language.English);
		for (int i = 0; i < this.displayedItemCount; i++)
		{
			array[i + 1] = this.document.FetchString(this.shopItems[this.displayedItems[i]].choiceDisplay, Language.English);
		}
		return array;
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x0001A300 File Offset: 0x00018500
	private void UpdateInventory()
	{
		for (int i = 0; i < this.displayedItemCount; i++)
		{
			this.shopItems[this.displayedItems[i]].gameObject.SetActive(false);
			this.displayedItems[i] = -1;
		}
		this.displayedItemCount = this.itemSlots.Length;
		for (int j = 0; j < this.itemSlots.Length; j++)
		{
			int num = 9999;
			for (int k = 0; k < this.shopItems.Length; k++)
			{
				if (!this.shopItems[k].isHidden && !this.shopItems[k].item.IsUnlocked && this.shopItems[k].priority <= num)
				{
					bool flag = false;
					for (int l = 0; l < j; l++)
					{
						if (this.displayedItems[l] == k)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						this.displayedItems[j] = k;
						num = this.shopItems[k].priority;
					}
				}
			}
			if (this.displayedItems[j] == -1)
			{
				this.displayedItemCount = j;
				return;
			}
			this.shopItems[this.displayedItems[j]].gameObject.transform.position = this.itemSlots[j].transform.position;
			this.shopItems[this.displayedItems[j]].gameObject.SetActive(true);
		}
	}

	// Token: 0x0400013C RID: 316
	public MultilingualTextDocument document;

	// Token: 0x0400013D RID: 317
	public JunkShop.ShopItem[] shopItems;

	// Token: 0x0400013E RID: 318
	public Transform[] itemSlots;

	// Token: 0x0400013F RID: 319
	private int[] displayedItems;

	// Token: 0x04000140 RID: 320
	private int displayedItemCount;

	// Token: 0x04000141 RID: 321
	[ChunkLookup("document")]
	public string shopIntro;

	// Token: 0x04000142 RID: 322
	[TextLookup("document")]
	public string cancelChoice;

	// Token: 0x04000143 RID: 323
	[ChunkLookup("document")]
	public string cancelDialogue;

	// Token: 0x04000144 RID: 324
	[ChunkLookup("document")]
	public string notEnoughDialogue;

	// Token: 0x04000145 RID: 325
	[ChunkLookup("document")]
	public string soldOutDialogue;

	// Token: 0x04000146 RID: 326
	public QuestStates stateMachine;

	// Token: 0x04000147 RID: 327
	public DialogueActor[] actors;

	// Token: 0x04000148 RID: 328
	public ItemResource itemResource;

	// Token: 0x04000149 RID: 329
	public UIItemResource uiItemResource;

	// Token: 0x0400014A RID: 330
	public UIItemGet itemGet;

	// Token: 0x0400014B RID: 331
	public GameObject[] shopStateObjects;

	// Token: 0x0400014C RID: 332
	public GameObject[] cameras;

	// Token: 0x02000041 RID: 65
	[Serializable]
	public struct ShopItem
	{
		// Token: 0x0400014D RID: 333
		[TextLookup("document")]
		public string choiceDisplay;

		// Token: 0x0400014E RID: 334
		public ItemObject item;

		// Token: 0x0400014F RID: 335
		public int cost;

		// Token: 0x04000150 RID: 336
		public int priority;

		// Token: 0x04000151 RID: 337
		public GameObject gameObject;

		// Token: 0x04000152 RID: 338
		[ChunkLookup("document")]
		public string unlockChunk;

		// Token: 0x04000153 RID: 339
		public bool isHidden;
	}
}
