using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000034 RID: 52
public class JunkShop : MonoBehaviour
{
	// Token: 0x060000C9 RID: 201 RVA: 0x00005B40 File Offset: 0x00003D40
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

	// Token: 0x060000CA RID: 202 RVA: 0x00005BCD File Offset: 0x00003DCD
	private void Start()
	{
		this.displayedItems = new int[] { -1, -1, -1 };
		this.UpdateInventory();
	}

	// Token: 0x060000CB RID: 203 RVA: 0x00005BEC File Offset: 0x00003DEC
	public void RunShopDialogue()
	{
		CoroutineUtil.Start(this.RunShopDialogueSequence());
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00005BFA File Offset: 0x00003DFA
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

	// Token: 0x060000CD RID: 205 RVA: 0x00005C0C File Offset: 0x00003E0C
	private string[] GetChoiceList()
	{
		string[] array = new string[this.displayedItemCount + 1];
		array[0] = this.document.FetchString(this.cancelChoice, Language.Auto);
		for (int i = 0; i < this.displayedItemCount; i++)
		{
			array[i + 1] = this.document.FetchString(this.shopItems[this.displayedItems[i]].choiceDisplay, Language.Auto);
		}
		return array;
	}

	// Token: 0x060000CE RID: 206 RVA: 0x00005C7C File Offset: 0x00003E7C
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

	// Token: 0x0400010F RID: 271
	public MultilingualTextDocument document;

	// Token: 0x04000110 RID: 272
	public JunkShop.ShopItem[] shopItems;

	// Token: 0x04000111 RID: 273
	public Transform[] itemSlots;

	// Token: 0x04000112 RID: 274
	private int[] displayedItems;

	// Token: 0x04000113 RID: 275
	private int displayedItemCount;

	// Token: 0x04000114 RID: 276
	[ChunkLookup("document")]
	public string shopIntro;

	// Token: 0x04000115 RID: 277
	[TextLookup("document")]
	public string cancelChoice;

	// Token: 0x04000116 RID: 278
	[ChunkLookup("document")]
	public string cancelDialogue;

	// Token: 0x04000117 RID: 279
	[ChunkLookup("document")]
	public string notEnoughDialogue;

	// Token: 0x04000118 RID: 280
	[ChunkLookup("document")]
	public string soldOutDialogue;

	// Token: 0x04000119 RID: 281
	public QuestStates stateMachine;

	// Token: 0x0400011A RID: 282
	public DialogueActor[] actors;

	// Token: 0x0400011B RID: 283
	public ItemResource itemResource;

	// Token: 0x0400011C RID: 284
	public UIItemResource uiItemResource;

	// Token: 0x0400011D RID: 285
	public UIItemGet itemGet;

	// Token: 0x0400011E RID: 286
	public GameObject[] shopStateObjects;

	// Token: 0x0400011F RID: 287
	public GameObject[] cameras;

	// Token: 0x0200035B RID: 859
	[Serializable]
	public struct ShopItem
	{
		// Token: 0x040019FF RID: 6655
		[TextLookup("document")]
		public string choiceDisplay;

		// Token: 0x04001A00 RID: 6656
		public ItemObject item;

		// Token: 0x04001A01 RID: 6657
		public int cost;

		// Token: 0x04001A02 RID: 6658
		public int priority;

		// Token: 0x04001A03 RID: 6659
		public GameObject gameObject;

		// Token: 0x04001A04 RID: 6660
		[ChunkLookup("document")]
		public string unlockChunk;

		// Token: 0x04001A05 RID: 6661
		public bool isHidden;
	}
}
